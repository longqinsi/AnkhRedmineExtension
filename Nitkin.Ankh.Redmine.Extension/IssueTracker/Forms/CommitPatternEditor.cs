using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nitkin.Ankh.Redmine.Extension.IssueTracker.Forms
{
    public partial class CommitPatternEditor : Form
    {
        public string CommitFixesPattern { get; set; }
        public string CommitRefsPattern { get; set; }
        public CommitPatternEditor()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CommitFixesPattern = tbCommitFixesPattern.Text;
            CommitRefsPattern = tbRefsCommitPattern.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CommitPatternEditor_Load(object sender, EventArgs e)
        {
            tbCommitFixesPattern.Text = CommitFixesPattern;
            tbRefsCommitPattern.Text = CommitRefsPattern;
        }
    }
}
