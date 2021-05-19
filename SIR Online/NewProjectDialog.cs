using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIRModel.Online;
using SIRModel.Eventing;

namespace SIR_Online
{
    public partial class NewProjectDialog : Form
    {
        public NewProjectDialog()
        {
            InitializeComponent();
            
        }
        
        List<Structure> structureList;
        List<OnlineProject> projectList;
        private void NewProjectDialog_Load(object sender, EventArgs e)
        {
            folderBD = new FolderBrowserDialog();
            structureList = new List<Structure>();
            projectList = new List<OnlineProject>();


        }

        private void NewProjectDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
        string ChoosenProjectName;

        private void CreateProjectBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SelectedProjectTB.Text))
            {
                SelectedProjectTB.Text = SelectedProjectTB.Text;

            }
        }
        FolderBrowserDialog folderBD;

        private void OpenFileLocationBTN_Click(object sender, EventArgs e)
        {
            FileLocationTB.Enabled = true;
            DialogResult ret = STAShowDialog(folderBD);
            string location = folderBD.SelectedPath;
            if (ret == DialogResult.OK)
            {
                FileLocationTB.Text = location;
                FileLocationTB.Enabled = false;
            }
        }

        private void AddProjectTB_TextChanged(object sender, EventArgs e)
        {

        }
        private void OpenBackupLocationBTN_Click(object sender, EventArgs e)
        {
            BackupLocationTB.Enabled = true;
            DialogResult ret = STAShowDialog(folderBD);
            string location = folderBD.SelectedPath;
            if (ret == DialogResult.OK)
            {
                BackupLocationTB.Text = location;
                BackupLocationTB.Enabled = false;
            }
        }

        private void CreatePjctBtn_Click(object sender, EventArgs e)
        {
            //structureList.Clear();
            bool isLeftEmpty = false;
            foreach (Control item in groupBox1.Controls)
            {
                if (item.GetType() == typeof(System.Windows.Forms.TextBox))
                {
                    if (string.IsNullOrWhiteSpace(item.Text))
                    {
                        isLeftEmpty = true;
                    }
                }
                //Console.WriteLine(" control type :" + item.GetType());
                if (item.GetType() == typeof(System.Windows.Forms.ComboBox))
                {
                    ComboBox cb = (ComboBox)item;
                    if (cb.Items == null)
                    {
                        isLeftEmpty = true;
                    }
                }

            }
            if (isLeftEmpty)
            {
                MessageBox.Show("Fields cannot be left empty");
            }
            else
            {
                OnlineProject onlineProject = new OnlineProject(SelectedProjectTB.Text, VesselTB.Text, ClientTB.Text, ContractorTB.Text, SystemTB.Text, LocationTB.Text, FileLocationTB.Text, BackupLocationTB.Text,structureList,new List<EventLog>());
                Console.WriteLine(" TB file location : " + FileLocationTB.Text);
                Console.WriteLine(" file location :" + onlineProject.FileLocation);
                Console.WriteLine(" backup file location :" + onlineProject.BackupFileLocation);
                bool Isthesamename = false;
                for (int i = 0; i < projectList.Count; i++)
                {
                    if (projectList[i].ProjectName == onlineProject.ProjectName)
                        Isthesamename = true;
                }
                         
                if (Isthesamename)
                {
                    
                    MessageBox.Show(" Project With the name given already exist");
                    return;
                }
                else
                {
                    //projectList.Add(onlineProject);
                    //ProjectListBox.DataSource = null;
                    //ProjectListBox.Items.Clear();
                    //ProjectListBox.DataSource = new BindingList<OnlineProject>(projectList);
                    SIROnlineSession.Instance.CurrentProject = onlineProject;
                    foreach (Structure item in onlineProject.structures)
                    {
                        SIROnlineSession.Instance.MakeFoldersByStructure(item);
                    }
                    SIROnlineSession.Instance.SaveProject();
                    this.Hide();

                    //SIROnlineMainForm.SetCurrentProject(onlineProject);
                }               
            }
            //structureList.Clear();
        }

        private void StructureCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #region Handle STA

        public class DialogState
        {
            public DialogResult result;
            public FolderBrowserDialog dialog;

            public void ThreadProcShowDialog()
            {
                result = dialog.ShowDialog();
            }
        }
        private DialogResult STAShowDialog(FolderBrowserDialog dialog)
        {
            DialogState state = new DialogState();
            state.dialog = dialog;
            System.Threading.Thread t = new System.Threading.Thread(state.ThreadProcShowDialog);

            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            t.Join();
            return state.result;
        }


        #endregion

        private void AddStructureBtn_Click(object sender, EventArgs e)
        {
            using (var dialog = new AddStructure())
            {

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    if (!structureList.Contains(dialog.CreatedStructure))
                    {
                        structureList.Add(dialog.CreatedStructure);
                        StructureCB.Items.Add(dialog.CreatedStructure);
                        StructureCB.SelectedIndex = StructureCB.Items.Count - 1;
                        //StructureCB.DisplayMember = "StructureName";
                        // StructureCB.Items.Add(new StringObjectPair<Structure>(dialog.CreatedStructure.StructureName, dialog.CreatedStructure));
                        Console.WriteLine(" structure list count :" + StructureCB.Items.Count);
                        Console.WriteLine(" structure list count :" + structureList.Count);
                    }


                }
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
