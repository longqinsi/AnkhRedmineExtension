namespace Nitkin.Ankh.Redmine.Extension.IssueTracker.Forms
{
	partial class ConfigurationPage
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.Label urlLabel;
            System.Windows.Forms.Label userNameLabel;
            System.Windows.Forms.Label passwordLabel;
            this._url = new System.Windows.Forms.TextBox();
            this._user = new System.Windows.Forms.TextBox();
            this._password = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlProject = new System.Windows.Forms.Panel();
            this.cbProjects = new System.Windows.Forms.ComboBox();
            this.btnProxy = new System.Windows.Forms.Button();
            this.btnCommitPattern = new System.Windows.Forms.Button();
            urlLabel = new System.Windows.Forms.Label();
            userNameLabel = new System.Windows.Forms.Label();
            passwordLabel = new System.Windows.Forms.Label();
            this.pnlProject.SuspendLayout();
            this.SuspendLayout();
            // 
            // urlLabel
            // 
            urlLabel.Location = new System.Drawing.Point(11, 12);
            urlLabel.Name = "urlLabel";
            urlLabel.Size = new System.Drawing.Size(150, 20);
            urlLabel.TabIndex = 0;
            urlLabel.Text = "Issue Repository URL";
            urlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // userNameLabel
            // 
            userNameLabel.Location = new System.Drawing.Point(11, 43);
            userNameLabel.Name = "userNameLabel";
            userNameLabel.Size = new System.Drawing.Size(150, 20);
            userNameLabel.TabIndex = 2;
            userNameLabel.Text = "User Name";
            userNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // passwordLabel
            // 
            passwordLabel.Location = new System.Drawing.Point(11, 74);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new System.Drawing.Size(150, 20);
            passwordLabel.TabIndex = 4;
            passwordLabel.Text = "Password";
            passwordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _url
            // 
            this._url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._url.Location = new System.Drawing.Point(172, 12);
            this._url.Name = "_url";
            this._url.Size = new System.Drawing.Size(281, 20);
            this._url.TabIndex = 1;
            this._url.TextChanged += new System.EventHandler(this.UrlModified);
            // 
            // _user
            // 
            this._user.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._user.Location = new System.Drawing.Point(172, 43);
            this._user.Name = "_user";
            this._user.Size = new System.Drawing.Size(281, 20);
            this._user.TabIndex = 3;
            // 
            // _password
            // 
            this._password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._password.Location = new System.Drawing.Point(172, 74);
            this._password.Name = "_password";
            this._password.PasswordChar = '*';
            this._password.Size = new System.Drawing.Size(281, 20);
            this._password.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Default project";
            // 
            // pnlProject
            // 
            this.pnlProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProject.Controls.Add(this.cbProjects);
            this.pnlProject.Controls.Add(this.label1);
            this.pnlProject.Enabled = false;
            this.pnlProject.Location = new System.Drawing.Point(0, 165);
            this.pnlProject.Name = "pnlProject";
            this.pnlProject.Size = new System.Drawing.Size(465, 61);
            this.pnlProject.TabIndex = 8;
            // 
            // cbProjects
            // 
            this.cbProjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProjects.FormattingEnabled = true;
            this.cbProjects.Location = new System.Drawing.Point(172, 17);
            this.cbProjects.Name = "cbProjects";
            this.cbProjects.Size = new System.Drawing.Size(281, 21);
            this.cbProjects.TabIndex = 8;
            // 
            // btnProxy
            // 
            this.btnProxy.Location = new System.Drawing.Point(14, 97);
            this.btnProxy.Name = "btnProxy";
            this.btnProxy.Size = new System.Drawing.Size(75, 23);
            this.btnProxy.TabIndex = 9;
            this.btnProxy.Text = "Proxy";
            this.btnProxy.UseVisualStyleBackColor = true;
            this.btnProxy.Click += new System.EventHandler(this.btnProxy_Click);
            // 
            // btnCommitPattern
            // 
            this.btnCommitPattern.Location = new System.Drawing.Point(3, 228);
            this.btnCommitPattern.Name = "btnCommitPattern";
            this.btnCommitPattern.Size = new System.Drawing.Size(166, 23);
            this.btnCommitPattern.TabIndex = 10;
            this.btnCommitPattern.Text = "Commit pattern";
            this.btnCommitPattern.UseVisualStyleBackColor = true;
            this.btnCommitPattern.Click += new System.EventHandler(this.btnCommitPattern_Click);
            // 
            // ConfigurationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCommitPattern);
            this.Controls.Add(this.btnProxy);
            this.Controls.Add(this.pnlProject);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._password);
            this.Controls.Add(passwordLabel);
            this.Controls.Add(this._user);
            this.Controls.Add(userNameLabel);
            this.Controls.Add(this._url);
            this.Controls.Add(urlLabel);
            this.Name = "ConfigurationPage";
            this.Size = new System.Drawing.Size(465, 254);
            this.pnlProject.ResumeLayout(false);
            this.pnlProject.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _url;
		private System.Windows.Forms.TextBox _user;
		private System.Windows.Forms.TextBox _password;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlProject;
        private System.Windows.Forms.ComboBox cbProjects;
        private System.Windows.Forms.Button btnProxy;
        private System.Windows.Forms.Button btnCommitPattern;
	}
}
