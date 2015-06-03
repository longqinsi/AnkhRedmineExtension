namespace Nitkin.Ankh.Redmine.Extension.IssueTracker.Forms
{
	partial class IssuesListView
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlButttons = new System.Windows.Forms.Panel();
            this.chkbMe = new System.Windows.Forms.CheckBox();
            this.cbIssueStatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbProjects = new System.Windows.Forms.ComboBox();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.columnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnIssue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAuthor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAssignedTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPriority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFixes = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnRefs = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SpentTime = new Nitkin.Ankh.Redmine.Extension.IssueTracker.Forms.CalendarColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calendarColumn1 = new Nitkin.Ankh.Redmine.Extension.IssueTracker.Forms.CalendarColumn();
            this.pnlButttons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnlButttons
            // 
            this.pnlButttons.Controls.Add(this.chkbMe);
            this.pnlButttons.Controls.Add(this.cbIssueStatus);
            this.pnlButttons.Controls.Add(this.label2);
            this.pnlButttons.Controls.Add(this.label1);
            this.pnlButttons.Controls.Add(this.cbProjects);
            this.pnlButttons.Controls.Add(this.btnRefresh);
            this.pnlButttons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButttons.Location = new System.Drawing.Point(0, 0);
            this.pnlButttons.Name = "pnlButttons";
            this.pnlButttons.Size = new System.Drawing.Size(1166, 46);
            this.pnlButttons.TabIndex = 2;
            // 
            // chkbMe
            // 
            this.chkbMe.AutoSize = true;
            this.chkbMe.Checked = true;
            this.chkbMe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbMe.Location = new System.Drawing.Point(698, 18);
            this.chkbMe.Name = "chkbMe";
            this.chkbMe.Size = new System.Drawing.Size(98, 17);
            this.chkbMe.TabIndex = 6;
            this.chkbMe.Text = "Assigned to me";
            this.chkbMe.UseVisualStyleBackColor = true;
            // 
            // cbIssueStatus
            // 
            this.cbIssueStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIssueStatus.FormattingEnabled = true;
            this.cbIssueStatus.Items.AddRange(new object[] {
            "open",
            "closed",
            "all"});
            this.cbIssueStatus.Location = new System.Drawing.Point(542, 11);
            this.cbIssueStatus.Name = "cbIssueStatus";
            this.cbIssueStatus.Size = new System.Drawing.Size(121, 21);
            this.cbIssueStatus.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(490, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Project";
            // 
            // cbProjects
            // 
            this.cbProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProjects.FormattingEnabled = true;
            this.cbProjects.Location = new System.Drawing.Point(167, 11);
            this.cbProjects.Name = "cbProjects";
            this.cbProjects.Size = new System.Drawing.Size(297, 21);
            this.cbProjects.TabIndex = 2;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnId,
            this.columnProject,
            this.columnIssue,
            this.columnAuthor,
            this.columnAssignedTo,
            this.columnStatus,
            this.columnPriority,
            this.columnFixes,
            this.columnRefs,
            this.SpentTime});
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvList.Location = new System.Drawing.Point(0, 46);
            this.dgvList.Name = "dgvList";
            this.dgvList.Size = new System.Drawing.Size(1166, 330);
            this.dgvList.TabIndex = 3;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvList_CellFormatting);
            this.dgvList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellValueChanged);
            this.dgvList.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvList_CurrentCellDirtyStateChanged);
            // 
            // columnId
            // 
            this.columnId.DataPropertyName = "Id";
            this.columnId.HeaderText = "Id";
            this.columnId.Name = "columnId";
            this.columnId.ReadOnly = true;
            this.columnId.Width = 25;
            // 
            // columnProject
            // 
            this.columnProject.DataPropertyName = "Project.Name";
            this.columnProject.HeaderText = "Project";
            this.columnProject.Name = "columnProject";
            this.columnProject.ReadOnly = true;
            // 
            // columnIssue
            // 
            this.columnIssue.DataPropertyName = "Subject";
            this.columnIssue.HeaderText = "Issue";
            this.columnIssue.Name = "columnIssue";
            this.columnIssue.ReadOnly = true;
            // 
            // columnAuthor
            // 
            this.columnAuthor.DataPropertyName = "Author.Name";
            this.columnAuthor.HeaderText = "Author";
            this.columnAuthor.Name = "columnAuthor";
            this.columnAuthor.ReadOnly = true;
            // 
            // columnAssignedTo
            // 
            this.columnAssignedTo.DataPropertyName = "AssignedTo.Name";
            this.columnAssignedTo.HeaderText = "Assigned to";
            this.columnAssignedTo.Name = "columnAssignedTo";
            this.columnAssignedTo.ReadOnly = true;
            // 
            // columnStatus
            // 
            this.columnStatus.DataPropertyName = "Status.Name";
            this.columnStatus.HeaderText = "Status";
            this.columnStatus.Name = "columnStatus";
            this.columnStatus.ReadOnly = true;
            // 
            // columnPriority
            // 
            this.columnPriority.DataPropertyName = "Priority.Name";
            this.columnPriority.HeaderText = "Priority";
            this.columnPriority.Name = "columnPriority";
            this.columnPriority.ReadOnly = true;
            // 
            // columnFixes
            // 
            this.columnFixes.HeaderText = "Mark fixes";
            this.columnFixes.Name = "columnFixes";
            // 
            // columnRefs
            // 
            this.columnRefs.HeaderText = "Mark refs";
            this.columnRefs.Name = "columnRefs";
            // 
            // SpentTime
            // 
            this.SpentTime.HeaderText = "Spent Time";
            this.SpentTime.Name = "SpentTime";
            this.SpentTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SpentTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SpentTime.Width = 150;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Project.Name";
            this.dataGridViewTextBoxColumn2.HeaderText = "Project";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Subject";
            this.dataGridViewTextBoxColumn3.HeaderText = "Issue";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Author.Name";
            this.dataGridViewTextBoxColumn4.HeaderText = "Author";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "AssignedTo.Name";
            this.dataGridViewTextBoxColumn5.HeaderText = "Assigned to";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Status.Name";
            this.dataGridViewTextBoxColumn6.HeaderText = "Status";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Priority.Name";
            this.dataGridViewTextBoxColumn7.HeaderText = "Priority";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // calendarColumn1
            // 
            this.calendarColumn1.HeaderText = "Spent Time";
            this.calendarColumn1.Name = "calendarColumn1";
            this.calendarColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.calendarColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IssuesListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.pnlButttons);
            this.Name = "IssuesListView";
            this.Size = new System.Drawing.Size(1166, 376);
            this.Load += new System.EventHandler(this.IssuesListView_Load);
            this.pnlButttons.ResumeLayout(false);
            this.pnlButttons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlButttons;
        private System.Windows.Forms.ComboBox cbProjects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbIssueStatus;
        private System.Windows.Forms.CheckBox chkbMe;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private CalendarColumn calendarColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProject;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnIssue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAssignedTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPriority;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnFixes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnRefs;
        private CalendarColumn SpentTime;
	}
}
