using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Nitkin.AnkhRedmine.Dal;
using Nitkin.AnkhRedmine.Dal.Interfaces;
using Nitkin.AnkhRemine.Domain;
using DataGridView=System.Windows.Forms.DataGridView;
using DataGridViewColumn=System.Windows.Forms.DataGridViewColumn;


namespace Nitkin.Ankh.Redmine.Extension.IssueTracker.Forms
{
    public partial class IssuesListView : UserControl, IIssueListForm
    {
        private readonly IProjectDao projectDao = new ProjectDao();
        private readonly IIssueDao issueDao = new IssueDao();
        private IUsersLocalSettingsDao localSettingsDao = new UsersLocalSettingsDao();
        private UsersLocalSettings usersLocalSettings;
        private AnkhRepository repository;
        public IssuesListView()
        {
            InitializeComponent();
            Exception error;
            usersLocalSettings = localSettingsDao.GetUsersLocalSettings(out error);
            if (error != null)
                MessageBox.Show(
                    "Error occured while decrypting password (upgrade from old version was not completed). Go to Issue repository configuration");
            cbIssueStatus.SelectedIndex = 0;
            dgvList.AutoGenerateColumns = false;
        }
        internal IssuesListView(AnkhRepository repos)
            : this()
        {
            repository = repos;
            object value = null;
            //repository.CustomProperties.TryGetValue(AnkhRepository.PROPERTY_USEPROXY, out value);
            //if (value!=null&&(bool)value)
            //{
            //    projectDao.SetProxy((bool)repository.CustomProperties[AnkhRepository.PROPERTY_USEPROXY]
            //        , (string)repository.CustomProperties[AnkhRepository.PROPERTY_PROXYURL]
            //        , (string)repository.CustomProperties[AnkhRepository.PROPERTY_PROXYUSER]
            //        , (string)repository.CustomProperties[AnkhRepository.PROPERTY_PROXYPWD]
            //        , (int)repository.CustomProperties[AnkhRepository.PROPERTY_PROXYPORT]);
            //    issueDao.SetProxy((bool)repository.CustomProperties[AnkhRepository.PROPERTY_USEPROXY]
            //        , (string)repository.CustomProperties[AnkhRepository.PROPERTY_PROXYURL]
            //        , (string)repository.CustomProperties[AnkhRepository.PROPERTY_PROXYUSER]
            //        , (string)repository.CustomProperties[AnkhRepository.PROPERTY_PROXYPWD]
            //        , (int)repository.CustomProperties[AnkhRepository.PROPERTY_PROXYPORT]);
            //}

        }

        private void IssuesListView_Load(object sender, System.EventArgs e)
        {
            PopulateProjects();
            SetProject(int.Parse(repository.CustomProperties[AnkhRepository.PROPERTY_DEFAULT_PROJECT] as string));
            PopulateList();
        }

        private void SetProject(int projectId)
        {
            int index = 0;
            foreach (var item in cbProjects.Items)
            {
                if ((item as ProjectJson).Id == projectId)
                {
                    cbProjects.SelectedIndex = index;
                    break;
                }
                index++;
            }
        }

        private void Populate()
        {
            var currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            PopulateProjects();

            PopulateList();
            this.Cursor = currentCursor;

        }
        private void PopulateList()
        {
            try
            {
                IList<IssueJson> list = new List<IssueJson>();


                if (cbIssueStatus.SelectedIndex == 0)
                    list = issueDao.GetIssuesByProjectId(repository.RepositoryUri,
                                                                             usersLocalSettings.UserName,
                                                                             usersLocalSettings.UserPassword,
                                                                             (cbProjects.SelectedValue as ProjectJson).Id, chkbMe.Checked).Issues;
                else if (cbIssueStatus.SelectedIndex == 1)
                {
                    list = issueDao.GetClosedIssuesByProjectId(repository.RepositoryUri,
                                                                            usersLocalSettings.UserName,
                                                                             usersLocalSettings.UserPassword,
                                                                             (cbProjects.SelectedValue as ProjectJson).Id, chkbMe.Checked).Issues;
                }
                else
                {
                    list = issueDao.GetAllIssuesByProjectId(repository.RepositoryUri,
                                                            usersLocalSettings.UserName,
                                                                             usersLocalSettings.UserPassword,
                                                            (cbProjects.SelectedValue as ProjectJson).Id, chkbMe.Checked).Issues;

                }
                dgvList.DataSource = list;
                foreach (DataGridViewRow row in dgvList.Rows)
                {

                    (row.Cells["columnFixes"] as DataGridViewCheckBoxCell).Value = false;
                    (row.Cells["columnRefs"] as DataGridViewCheckBoxCell).Value = false;
                }
                dgvList.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error updating issues list{0}Check connection settings{0}{1}", Environment.NewLine, ex.Message));
                dgvList.Enabled = false;
            }
        }

        private void PopulateProjects()
        {
            try
            {
                ProjectJson pr = cbProjects.SelectedValue as ProjectJson;
                cbProjects.DataSource = projectDao.GetProjects(repository.RepositoryUri,
                                      usersLocalSettings.UserName,
                                                                             usersLocalSettings.UserPassword);


                if (pr != null)
                    SetProject(pr.Id);
                dgvList.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error updating projects list{0}Check connection settings{0}{1}", Environment.NewLine, ex.Message));
                dgvList.Enabled = false;
            }

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {

            Populate();
        }


        public IList<IssueFacade> GetFixesIssues()
        {
            IList<IssueFacade> retList = new List<IssueFacade>();
            CurrencyManager currencyManager = (CurrencyManager)BindingContext[dgvList.DataSource, dgvList.DataMember];
            foreach (DataGridViewRow row in dgvList.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells["columnFixes"] as DataGridViewCheckBoxCell;
                if (cell.Value is Boolean && (bool)cell.Value)
                {
                    retList.Add(new IssueFacade
                                    {
                                        IssueJson = currencyManager.List[row.Index] as IssueJson,
                                        SpentTime = row.Cells["SpentTime"].Value as string
                                    });
                }
            }
            return retList;
        }

        public IList<IssueFacade> GetRefsIssues()
        {
            IList<IssueFacade> retList = new List<IssueFacade>();
            CurrencyManager currencyManager = (CurrencyManager)BindingContext[dgvList.DataSource, dgvList.DataMember];
            foreach (DataGridViewRow row in dgvList.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells["columnRefs"] as DataGridViewCheckBoxCell;
                if (cell.Value is Boolean && (bool)cell.Value)
                {
                    retList.Add(new IssueFacade
                    {
                        IssueJson = currencyManager.List[row.Index] as IssueJson,
                        SpentTime = row.Cells["SpentTime"].Value as string
                    });
                }
            }
            return retList;
        }


        private void dgvList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvList.IsCurrentCellDirty)
            {
                dgvList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (dgvList.Columns[e.ColumnIndex].DataPropertyName.Contains("."))
                e.Value = GetValue(dgvList.Rows[e.RowIndex].DataBoundItem, dgvList.Columns[e.ColumnIndex].DataPropertyName);



        }
        protected object GetValue(object obj, string propName)
        {

            string[] nestedPropNames = propName.Split('.');
            object value = null;
            try
            {
                value = obj.GetType().GetProperty(nestedPropNames[0]).GetValue(obj, null); ;
                for (int i = 1; value != null && i < nestedPropNames.Length; i++)
                {
                    value = value.GetType().GetProperty(nestedPropNames[i]).GetValue(value, null);
                }
            }
            catch
            {
                return null;
            }
            return value;
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvList.Columns[e.ColumnIndex].Name == "columnFixes")
            {
                DataGridViewCheckBoxCell cellRefs =
                    (DataGridViewCheckBoxCell)dgvList.
                    Rows[e.RowIndex].Cells["columnRefs"];

                DataGridViewCheckBoxCell checkCell =
                    (DataGridViewCheckBoxCell)dgvList.
                    Rows[e.RowIndex].Cells["columnFixes"];
                if ((Boolean)checkCell.Value)
                {
                    cellRefs.Value = false;
                    ReEnableSpentTimeControl(dgvList.Rows[e.RowIndex],true);
                }
                else
                {
                    ReEnableSpentTimeControl(dgvList.Rows[e.RowIndex], false);
                }
                dgvList.Invalidate();
            }
            else if (dgvList.Columns[e.ColumnIndex].Name == "columnRefs")
            {
                DataGridViewCheckBoxCell cellRefs =
                    (DataGridViewCheckBoxCell)dgvList.
                    Rows[e.RowIndex].Cells["columnFixes"];

                DataGridViewCheckBoxCell checkCell =
                    (DataGridViewCheckBoxCell)dgvList.
                    Rows[e.RowIndex].Cells["columnRefs"];
                if ((Boolean)checkCell.Value)
                {
                    cellRefs.Value = false;
                    ReEnableSpentTimeControl(dgvList.Rows[e.RowIndex],true);
                }
                else
                {
                    ReEnableSpentTimeControl(dgvList.Rows[e.RowIndex], false);
                }
                dgvList.Invalidate();
            }
        }
        private void ReEnableSpentTimeControl(DataGridViewRow row,bool enabled)
        {
            row.Cells["SpentTime"].ReadOnly = !enabled;
        }
    }
   
    public class CalendarColumn : DataGridViewColumn
    {
        public CalendarColumn()
            : base(new CalendarCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class CalendarCell : DataGridViewTextBoxCell
    {

        public CalendarCell()
            : base()
        {
            // Use the short date format.
            //this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            CalendarEditingControl ctl =
                DataGridView.EditingControl as CalendarEditingControl;
            if (this.Value == null)
                this.Value = "0H0m";
            ctl.SetControlValue(this.Value);
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(CalendarEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.
                return typeof(String);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return "0H0m";
            }
        }
    }

    class CalendarEditingControl : TimeSpentControl, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;
        
        public CalendarEditingControl()
        {
            //this.Format = DateTimePickerFormat.Short;
            this.nudHours.ValueChanged += new EventHandler(nud_ValueChanged);
            this.nudMinutes.ValueChanged += new EventHandler(nud_ValueChanged);
            
            //this.Child = hostedTimePicker;
        }

        void nud_ValueChanged(object sender, EventArgs e)
        {
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
        }
        public void SetControlValue(object value)
        {
            if (value is String)
            {
                SetStringToNud((String)value);
            }
        }
        // Implements the IDataGridViewEditingControl.EditingControlFormattedValue 
        // property.
        public object EditingControlFormattedValue
        {
            get
            {
                return GetStringValue();
            }
            set
            {
                if (value is String)
                {
                   SetStringToNud((String)value);
                }
            }
        }
        private string GetStringValue()
        {
            return string.Format("{0}H {1}m", nudHours.Value, nudMinutes.Value);
        }
        private void SetStringToNud(string value)
        {
            nudHours.Value = int.Parse(value.Substring(0, value.IndexOf("H")).Trim());
            nudMinutes.Value = int.Parse(value.Substring(value.IndexOf("H") + 1, value.Length - value.IndexOf("H")-2).Trim());
        }
        // Implements the 
        // IDataGridViewEditingControl.GetEditingControlFormattedValue method.
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        // Implements the 
        // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
        }

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex 
        // property.
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey 
        // method.
        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return false;
            }
        }

        // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit 
        // method.
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }

        // Implements the IDataGridViewEditingControl
        // .RepositionEditingControlOnValueChange property.
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlDataGridView property.
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlValueChanged property.
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingPanelCursor property.
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        
    }



}
