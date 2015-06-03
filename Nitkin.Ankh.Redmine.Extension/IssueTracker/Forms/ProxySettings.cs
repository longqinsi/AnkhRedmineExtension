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
    public partial class ProxySettings : Form, IProxyForm
    {
        public ProxySettings()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void ProxySettings_Load(object sender, EventArgs e)
        {
            pnlSettings.Enabled = chkbUseProxy.Checked;
        }

        #region Implementation of IProxyForm

        public string ProxyUrl
        {
            get { return tbProxyAddress.Text.Trim(); }
            set { tbProxyAddress.Text = value; }
        }

        public string ProxyUser
        {
            get { return tbProxyUser.Text.Trim(); }
            set { tbProxyUser.Text = value; }
        }

        public string ProxyPwd
        {
            get { return tbProxyPwd.Text.Trim(); }
            set { tbProxyPwd.Text = value; }
        }

        public bool UseProxy
        {
            get { return chkbUseProxy.CheckState==CheckState.Checked; }
            set { chkbUseProxy.CheckState = value?CheckState.Checked:CheckState.Unchecked;}
        }

        public int ProxyPort
        {
            get { return (int)nudProxyPort.Value; }
            set { nudProxyPort.Value = value; }
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void chkbUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            pnlSettings.Enabled = chkbUseProxy.Checked;
        }
    }

    public interface IProxyForm
    {
        string ProxyUrl { get; set; }
        string ProxyUser { get; set; }
        string ProxyPwd { get; set; }
        bool UseProxy { get; set; }
        int ProxyPort { get; set; }

    }
}
