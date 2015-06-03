using System;
using System.Windows.Forms;
using Ankh.ExtensionPoints.IssueTracker;
using Nitkin.AnkhRedmine.Dal;
using Nitkin.AnkhRedmine.Dal.Interfaces;
using Nitkin.AnkhRemine.Domain;

namespace Nitkin.Ankh.Redmine.Extension.IssueTracker.Forms
{
    /// <summary>
    /// UI for Issue repository configuration
    /// </summary>
    public partial class ConfigurationPage : UserControl
    {
        private IProjectDao projectDao = new ProjectDao();
        private IUsersLocalSettingsDao localSettingsDao = new UsersLocalSettingsDao();
        private UsersLocalSettings usersLocalSettings;
        IssueRepositorySettings _currentSettings;
        private bool _processedSettings = false;
        private bool _canProcessSettings = false;
        private bool proxySettingsChanged = false;

        private string proxyUrl;
        private string proxyUser;
        private string proxyPwd;
        private bool useProxy;
        private int proxyPort;

        private string commitFixesPattern = "<%Id%> <%SpentTime%>";
        private string commitRefsPattern = "<%Id%> <%SpentTime%>";

        public ConfigurationPage()
        {
            InitializeComponent();
            Exception error;
            usersLocalSettings = localSettingsDao.GetUsersLocalSettings(out error);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _canProcessSettings = true;
            SelectSettings();
        }

        internal IssueRepositorySettings Settings
        {
            get
            {
                return CreateRepositorySettings();
            }
            set
            {
                _currentSettings = value;
                SelectSettings();
            }
        }

        internal AnkhRepository CreateRepositorySettings()
        {
            AnkhRepository repo = null;
            Uri repoUri;
            string uriString = _url.Text.Trim();
            uriString = uriString.EndsWith("/") ? uriString : uriString + "/";
            if (Uri.TryCreate(uriString, UriKind.Absolute, out repoUri))
            {
                repo = new AnkhRepository(repoUri, null, null);
                usersLocalSettings.UserName = _user.Text.Trim();
                usersLocalSettings.UserPassword =_password.Text.Trim();
                repo.SetProperty(AnkhRepository.PROPERTY_COMMIT_FIXES_PATTERN, commitFixesPattern);
                repo.SetProperty(AnkhRepository.PROPERTY_COMMIT_REFS_PATTERN, commitRefsPattern);
                if (cbProjects.DataSource != null)
                    repo.SetProperty(AnkhRepository.PROPERTY_DEFAULT_PROJECT, (cbProjects.SelectedValue as ProjectJson).Id);
                if (proxySettingsChanged)
                {
                    usersLocalSettings.ProxyUrl = proxyUrl;
                    usersLocalSettings.ProxyUser = proxyUser;
                    usersLocalSettings.ProxyPwd = proxyPwd;
                    usersLocalSettings.ProxyPort = proxyPort;
                    usersLocalSettings.UseProxy = useProxy;
                }
                localSettingsDao.SaveUsersLocalSettings(usersLocalSettings);
            }
            return repo;
        }

        /// <summary>
        /// populate UI with existing settings
        /// </summary>
        private void SelectSettings()
        {
            if (_currentSettings != null
                && !_processedSettings
                && _canProcessSettings)
            {
                try
                {
                    _url.Text = _currentSettings.RepositoryUri.ToString();
                    _user.Text = usersLocalSettings.UserName;
                    _password.Text = usersLocalSettings.UserPassword;
                    proxyUrl = usersLocalSettings.ProxyUrl;
                    proxyUser = usersLocalSettings.ProxyUser;
                    proxyPwd = usersLocalSettings.ProxyPwd;
                    proxyPort = usersLocalSettings.ProxyPort;
                    useProxy = usersLocalSettings.UseProxy;
                    object value;
                    if (_currentSettings.CustomProperties!=null&&_currentSettings.CustomProperties.TryGetValue(AnkhRepository.PROPERTY_COMMIT_FIXES_PATTERN, out value))
                    {
                        if (value != null)
                        {
                            commitFixesPattern = value.ToString();
                        }
                    }
                    if (_currentSettings.CustomProperties != null && _currentSettings.CustomProperties.TryGetValue(AnkhRepository.PROPERTY_COMMIT_REFS_PATTERN, out value))
                    {
                        if (value != null)
                        {
                            commitRefsPattern = value.ToString();
                        }
                    }
                    //if (_currentSettings.CustomProperties != null)
                    //{
                    //    object value;
                    //    if (_currentSettings.CustomProperties.TryGetValue(AnkhRepository.PROPERTY_USERNAME, out value))
                    //    {
                    //        if (value != null)
                    //        {
                    //            _user.Text = value.ToString();
                    //        }
                    //    }
                    //    value = null;
                    //    if (_currentSettings.CustomProperties.TryGetValue(AnkhRepository.PROPERTY_PASSCODE, out value))
                    //    {
                    //        if (value != null)
                    //        {
                    //            _password.Text = value.ToString();
                    //        }
                    //    }
                    //    value = null;
                    //    if (_currentSettings.CustomProperties.TryGetValue(AnkhRepository.PROPERTY_PROXYURL, out value))
                    //    {
                    //        if (value != null)
                    //        {
                    //            proxyUrl = value.ToString();
                    //        }
                    //    }
                    //    value = null;
                    //    if (_currentSettings.CustomProperties.TryGetValue(AnkhRepository.PROPERTY_PROXYUSER, out value))
                    //    {
                    //        if (value != null)
                    //        {
                    //            proxyUser = value.ToString();
                    //        }
                    //    }
                    //    value = null;
                    //    if (_currentSettings.CustomProperties.TryGetValue(AnkhRepository.PROPERTY_PROXYPWD, out value))
                    //    {
                    //        if (value != null)
                    //        {
                    //            proxyPwd = value.ToString();
                    //        }
                    //    }
                    //    value = null;
                    //    if (_currentSettings.CustomProperties.TryGetValue(AnkhRepository.PROPERTY_PROXYPORT, out value))
                    //    {
                    //        if (value != null)
                    //        {
                    //            proxyPort = int.Parse(value.ToString());
                    //        }
                    //    }
                    //    value = null;
                    //    if (_currentSettings.CustomProperties.TryGetValue(AnkhRepository.PROPERTY_USEPROXY, out value))
                    //    {
                    //        if (value != null)
                    //        {
                    //            useProxy = bool.Parse(value.ToString());
                    //        }
                    //    }
                    //}
                }
                finally
                {
                    _processedSettings = true;
                }
            }
        }

        #region UI Events

        private void UrlModified(object sender, EventArgs e)
        {
            RaisePageEvent();
        }

        #endregion

        /// <summary>
        /// Raise an event to notify listeners about the user changes
        /// </summary>
        private void RaisePageEvent()
        {
            ConfigPageEventArgs args = new ConfigPageEventArgs();
            try
            {
                args.IsComplete = IsPageComplete;
            }
            catch (Exception exc)
            {
                args.IsComplete = false;
                args.Exception = exc;
            }
            if (OnPageEvent != null)
            {
                OnPageEvent(this, args);
            }
        }

        private bool IsPageComplete
        {
            get
            {
                string urlString = _url.Text.Trim();
                Uri uri;
                return Uri.TryCreate(urlString, UriKind.Absolute, out uri);
                // add additional checks if required
            }
        }

        public event EventHandler<ConfigPageEventArgs> OnPageEvent;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //if (useProxy)
                //    projectDao.SetProxy(useProxy, proxyUrl, proxyUser, proxyPwd, proxyPort);
                //else
                //{
                //    projectDao.ClearProxy();
                //}

                cbProjects.DataSource = projectDao.GetProjects(new Uri(_url.Text.EndsWith("/") ? _url.Text : _url.Text+"/"),
                                           _user.Text,
                                           _password.Text);
                if (!pnlProject.Enabled && cbProjects.Items.Count > 0)
                    cbProjects.SelectedIndex = 0;
                pnlProject.Enabled = true;
            }
            catch (Exception ex)
            {
                pnlProject.Enabled = false;
                MessageBox.Show(String.Format("Error updating projects list{0}Check connection settings{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        private void btnProxy_Click(object sender, EventArgs e)
        {
            ProxySettings proxySettingsPage = new ProxySettings();
            proxySettingsPage.UseProxy = useProxy;
            proxySettingsPage.ProxyUrl = proxyUrl;
            proxySettingsPage.ProxyPort = proxyPort;
            proxySettingsPage.ProxyUser = proxyUser;
            proxySettingsPage.ProxyPwd = proxyPwd;
            proxySettingsPage.ShowDialog();
            if (proxySettingsPage.DialogResult == DialogResult.OK)
            {
                var v = proxySettingsPage as IProxyForm;
                proxyUrl = v.ProxyUrl;
                proxyUser = v.ProxyUser;
                proxyPwd = v.ProxyPwd;
                proxyPort = v.ProxyPort;
                useProxy = v.UseProxy;
            }
        }

        private void btnCommitPattern_Click(object sender, EventArgs e)
        {
            CommitPatternEditor form=new CommitPatternEditor();
            form.CommitFixesPattern = commitFixesPattern;
            form.CommitRefsPattern = commitRefsPattern;
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                commitFixesPattern = form.CommitFixesPattern;
                commitRefsPattern = form.CommitRefsPattern;
            }
        }
    }
}
