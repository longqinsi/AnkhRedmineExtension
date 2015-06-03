using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Ankh.ExtensionPoints.IssueTracker;
using Nitkin.Ankh.Redmine.Extension.IssueTracker.Forms;
using Nitkin.AnkhRemine.Domain;

namespace Nitkin.Ankh.Redmine.Extension.IssueTracker
{
    /// <summary>
    /// Represents an Issue Repository
    /// </summary>
    internal class AnkhRepository : IssueRepository, IWin32Window, IDisposable
    {
        //public static readonly string PROPERTY_USERNAME = "username";
        //public static readonly string PROPERTY_PASSCODE = "passcode";
        public static readonly string PROPERTY_DEFAULT_PROJECT = "defaultproject";
        public static readonly string PROPERTY_COMMIT_FIXES_PATTERN = "commitfixespattern";
        public static readonly string PROPERTY_COMMIT_REFS_PATTERN = "commitrefspattern";
        //public static readonly string PROPERTY_USEPROXY = "useproxy"; 
        //public static readonly string PROPERTY_PROXYURL = "proxyurl"; 
        //public static readonly string PROPERTY_PROXYPORT = "proxyport";
        //public static readonly string PROPERTY_PROXYUSER = "proxyuser";
        //public static readonly string PROPERTY_PROXYPWD = "proxypwd";


        Uri _uri;
        string _repositoryId;
        IDictionary<string, object> _properties;
        IssuesListView _control;

        public static IssueRepository Create(IssueRepositorySettings settings)
        {
            if (settings != null)
            {
                return new AnkhRepository(settings.RepositoryUri, settings.RepositoryId, settings.CustomProperties);
            }
            return null;
        }

        public AnkhRepository(Uri uri, string repositoryId, IDictionary<string, object> properties)
            : base(AppConstants.ConnectorName)
        {
            _uri = uri;
            _repositoryId = repositoryId;
            _properties = properties;
        }

        /// <summary>
        /// Gets the repository connection URL
        /// </summary>
        public override Uri RepositoryUri
        {
            get { return _uri; }
        }

        /// <summary>
        /// Gets the repository id hosted on the RepositoryUri
        /// </summary>
        /// <remarks>optional</remarks>
        public override string RepositoryId
        {
            get { return _repositoryId; }
        }

        /// <summary>
        /// Gets the custom properties specific to this type of connector
        /// </summary>
        public override IDictionary<string, object> CustomProperties
        {
            get { return _properties; }
        }

        /// <summary>
        /// Gets the repository label
        /// </summary>
        public override string Label
        {
            get { return RepositoryId ?? (RepositoryUri == null ? string.Empty : RepositoryUri.ToString()); }
        }

        [Obsolete("Please return a (compiled) regex from IssueIdRegex")]
        public override string IssueIdPattern
        {
            get
            {
                // reg expression to recognize issue id's within a text (i.e commit log message)
                // for example:
                // Text -> Sample id001, #id002 and id003
                // Resolved Issue Ids -> id001, id002, id003
                // How to test: 
                // 1. Set the current Issue repository to be this.
                // 2. Type a commit message in Pending Changes message box that would match this pattern
                // 3. See that issue ids are colorized, and "open issue" context option is available
                return @"[Ss]ample?:?\s*(#\s*)?(?<id>id\d+)(\s*(,|and)\s*(#\s*)?(?<id>id\d+))*";
            }
        }

        public override void PreCommit(PreCommitArgs args)
        {
            bool cancel = true; // perform issue related pre-commit checks

            string commitFixesPattern = "";
            string commitRefsPattern = "";
            object value;
            if (_properties != null && _properties.TryGetValue(AnkhRepository.PROPERTY_COMMIT_FIXES_PATTERN, out value))
            {
                if (value != null)
                {
                    commitFixesPattern = value.ToString();
                }
            }
            if (_properties != null && _properties.TryGetValue(AnkhRepository.PROPERTY_COMMIT_REFS_PATTERN, out value))
            {
                if (value != null)
                {
                    commitRefsPattern = value.ToString();
                }
            }
            // modify commit message here
            string originalMessage = args.CommitMessage ?? string.Empty;
            // get _control.SelectedIssues
            // modify original message to include issue info in the message
            string commitMessage = (string.IsNullOrEmpty(originalMessage)) ? originalMessage : originalMessage + "\r\n ";
            //bool first = true;
            if (_control == null)
            {
                string message = string.Format("AnkhRedmineExtention is configured for this solution!{0}Do you want to make commit ignoring AnkhRedmineExtention?{0}If you press OK no references about ticket from Redmine will be added to commit's comment.{0}For proper use of AnkhRedmine extention press Cancel and make commit from \"Issue\" tab of \"Pending Changes\" window (see readme.txt for more info)", Environment.NewLine);
                var result = MessageBox.Show(message, "AnkhRedmine is configured for this solution.", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    args.Cancel = false;
                    return;
                }
                else
                {
                    args.Cancel = true;
                    return;
                }

            }
            foreach (IssueFacade issue in (_control as IIssueListForm).GetFixesIssues())
            {

                //if (first)
                {
                    string id = "fixes #" + issue.IssueJson.Id.ToString(CultureInfo.InvariantCulture);
                    commitMessage = commitMessage + ReplaceIdInMessage(commitFixesPattern, id);
                    commitMessage = AppendSpentTime(commitMessage, issue);
                    commitMessage = ReplaceIssueSubjectMessage(commitMessage, issue);
                    commitMessage = ReplaceIssueAuthorMessage(commitMessage, issue);
                    commitMessage = commitMessage + "\r\n ";
                    // first = false;
                }
                //else
                //{
                //    string id = ", fixes #" + issue.IssueJson.Id.ToString(CultureInfo.InvariantCulture);
                //    commitMessage = commitMessage + ReplaceIdInMessage(commitFixesPattern, id);
                //    //sb.Append(", #" + issue.IssueJson.Id.ToString(CultureInfo.InvariantCulture));
                //    commitMessage = AppendSpentTime(commitMessage, issue);
                //}
            }
            foreach (IssueFacade issue in (_control as IIssueListForm).GetRefsIssues())
            {
                //if (first)
                {
                    string id = "refs #" + issue.IssueJson.Id.ToString(CultureInfo.InvariantCulture);
                    commitMessage = commitMessage + ReplaceIdInMessage(commitRefsPattern, id);
                    commitMessage = AppendSpentTime(commitMessage, issue);
                    commitMessage = ReplaceIssueSubjectMessage(commitMessage, issue);
                    commitMessage = ReplaceIssueAuthorMessage(commitMessage, issue);
                    commitMessage = commitMessage + "\r\n ";
                    // first = false;
                }
                //else
                //{
                //    string id = ", refs #" + issue.IssueJson.Id.ToString(CultureInfo.InvariantCulture);
                //    commitMessage = commitMessage + ReplaceIdInMessage(commitRefsPattern, id);
                //    commitMessage = AppendSpentTime(commitMessage, issue);
                //}
            }
            args.CommitMessage = commitMessage;
            cancel = false;

            args.Cancel = cancel; // true if "some" pre-commit check fails
        }
        private string ReplaceIdInMessage(string input, string id)
        {
            Regex regex = new Regex("<%Id%>");
            return regex.Replace(input, id);
        }
        private string AppendSpentTime(string commitMessage, IssueFacade issueFacade)
        {
            Regex re = new Regex("<%SpentTime%>");
            if (!String.IsNullOrEmpty(issueFacade.SpentTime) && issueFacade.SpentTime != "0H 0m")
            {

                commitMessage = re.Replace(commitMessage, "@" + issueFacade.SpentTime.ToLower().Replace(" ", ""));
                //commitMessage=commitMessage+(" @" + issueFacade.SpentTime.ToLower().Replace(" ", "") /*.TrimEnd(new char[] { 'm' })*/);
            }
            else
            {
                commitMessage = re.Replace(commitMessage, "");
            }
            return commitMessage;
        }
        private string ReplaceIssueSubjectMessage(string commitMessage, IssueFacade issueFacade)
        {
            Regex re = new Regex("<%IssueSubject%>");
            return re.Replace(commitMessage, issueFacade.IssueJson.Subject);
        }
        private string ReplaceIssueAuthorMessage(string commitMessage, IssueFacade issueFacade)
        {
            Regex re = new Regex("<%IssueAuthor%>");
            return re.Replace(commitMessage, issueFacade.IssueJson.Author.Name);
        }

        public override void PostCommit(PostCommitArgs args)
        {
            // post-process selected issues after commit is done
            // i.e. change issue status, add a comment to the issue(s) about commit info etc.
            long committedRevision = args.Revision;
            string commitMessage = args.CommitMessage;

            base.PostCommit(args);
        }

        /// <summary>
        /// Show issue details
        /// </summary>
        /// <param name="issueId"></param>
        public override void NavigateTo(string issueId)
        {
            // show issue details
            if (!string.IsNullOrEmpty(issueId))
            {
                string message = string.Format("{0} is an issue in {1}", issueId, RepositoryUri.ToString());
                MessageBox.Show(message, "Navigate to Issue", MessageBoxButtons.OK);
            }
        }

        internal void SetProperty(string property, object value)
        {
            if (string.IsNullOrEmpty(property)) { return; }
            if (CustomProperties == null)
            {
                _properties = new Dictionary<string, object>();
            }
            _properties.Add(property, value);
        }

        #region IWin32Window Members

        public IntPtr Handle
        {
            get { return Control.Handle; }
        }

        #endregion

        internal IssuesListView Control
        {
            get
            {
                if (_control == null)
                {
                    _control = CreateControl();
                }
                return _control;
            }
        }

        private IssuesListView CreateControl()
        {
            return new Forms.IssuesListView(this);
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_control != null && !_control.IsDisposed && !_control.Disposing)
            {
                _control.Dispose();
            }
            _control = null;
        }

        #endregion
    }
}
