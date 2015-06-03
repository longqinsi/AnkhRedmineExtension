namespace Nitkin.Ankh.Redmine.Extension.IssueTracker.Forms
{
    partial class ProxySettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkbUseProxy = new System.Windows.Forms.CheckBox();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudProxyPort = new System.Windows.Forms.NumericUpDown();
            this.tbProxyPwd = new System.Windows.Forms.TextBox();
            this.tbProxyUser = new System.Windows.Forms.TextBox();
            this.tbProxyAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProxyPort)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkbUseProxy
            // 
            this.chkbUseProxy.AutoSize = true;
            this.chkbUseProxy.Location = new System.Drawing.Point(13, 13);
            this.chkbUseProxy.Name = "chkbUseProxy";
            this.chkbUseProxy.Size = new System.Drawing.Size(73, 17);
            this.chkbUseProxy.TabIndex = 0;
            this.chkbUseProxy.Text = "Use proxy";
            this.chkbUseProxy.UseVisualStyleBackColor = true;
            this.chkbUseProxy.CheckedChanged += new System.EventHandler(this.chkbUseProxy_CheckedChanged);
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.label4);
            this.pnlSettings.Controls.Add(this.label3);
            this.pnlSettings.Controls.Add(this.nudProxyPort);
            this.pnlSettings.Controls.Add(this.tbProxyPwd);
            this.pnlSettings.Controls.Add(this.tbProxyUser);
            this.pnlSettings.Controls.Add(this.tbProxyAddress);
            this.pnlSettings.Controls.Add(this.label2);
            this.pnlSettings.Controls.Add(this.label1);
            this.pnlSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSettings.Location = new System.Drawing.Point(0, 42);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(491, 95);
            this.pnlSettings.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "User";
            // 
            // nudProxyPort
            // 
            this.nudProxyPort.Location = new System.Drawing.Point(81, 45);
            this.nudProxyPort.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.nudProxyPort.Name = "nudProxyPort";
            this.nudProxyPort.Size = new System.Drawing.Size(80, 20);
            this.nudProxyPort.TabIndex = 5;
            this.nudProxyPort.Value = new decimal(new int[] {
            3128,
            0,
            0,
            0});
            // 
            // tbProxyPwd
            // 
            this.tbProxyPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProxyPwd.Location = new System.Drawing.Point(312, 50);
            this.tbProxyPwd.Name = "tbProxyPwd";
            this.tbProxyPwd.Size = new System.Drawing.Size(167, 20);
            this.tbProxyPwd.TabIndex = 4;
            // 
            // tbProxyUser
            // 
            this.tbProxyUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProxyUser.Location = new System.Drawing.Point(312, 13);
            this.tbProxyUser.Name = "tbProxyUser";
            this.tbProxyUser.Size = new System.Drawing.Size(167, 20);
            this.tbProxyUser.TabIndex = 3;
            // 
            // tbProxyAddress
            // 
            this.tbProxyAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProxyAddress.Location = new System.Drawing.Point(81, 13);
            this.tbProxyAddress.Name = "tbProxyAddress";
            this.tbProxyAddress.Size = new System.Drawing.Size(163, 20);
            this.tbProxyAddress.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnOK);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 137);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(491, 39);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(404, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(289, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ProxySettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(491, 176);
            this.Controls.Add(this.pnlSettings);
            this.Controls.Add(this.chkbUseProxy);
            this.Controls.Add(this.pnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProxySettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProxySettings";
            this.Load += new System.EventHandler(this.ProxySettings_Load);
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProxyPort)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkbUseProxy;
        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudProxyPort;
        private System.Windows.Forms.TextBox tbProxyPwd;
        private System.Windows.Forms.TextBox tbProxyUser;
        private System.Windows.Forms.TextBox tbProxyAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}