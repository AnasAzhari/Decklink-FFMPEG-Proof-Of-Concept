using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIRModel.Eventing;
using SIRModel.Online;

namespace SIR_Online
{
    public partial class StartDivingSession : Form
    {
        public Structure SelectedStructure = null;
        public StartDivingSession()
        {
            InitializeComponent();
        }
        public void InitializeStructureList()
        {
            StructureList.DataSource = null;
            StructureList.Items.Clear();
            //StructureList.DataSource = new BindingList<Structure>(SIROnlineSession.Instance.CurrentProject.structures);
            foreach (Structure item in SIROnlineSession.Instance.CurrentProject.structures)
            {
                StructureList.Items.Add(new StringObjectPair<Structure>(item.StructureName, item));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SIROnlineSession.Instance.CurrentStructure = SelectedStructure;
        }
        public struct StringObjectPair<T>
        {
            public string name;
            public T value;

            public StringObjectPair(string name, T value)
            {
                this.name = name;
                this.value = value;
            }

            public override string ToString()
            {
                return name;
            }
        }

        private void StructureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedStructure = ((StringObjectPair<Structure>)StructureList.SelectedItem).value;
        }

        private void StartDivingSession_Shown(object sender, EventArgs e)
        {
            InitializeStructureList();
        }
    }
    //    public NewDivingSession()
    //    {
    //        InitializeComponent();
    //    }
    //    public Structure SelectedStructure = null;
    //    private void NewDivingSession_Load(object sender, EventArgs e)
    //    {
    //        CurrentProjectLbl.Text = SIROnlineSession.Instance.CurrentProject.ProjectName;
    //        //InitializeStructureList();
    //    }
    //    public void InitializeStructureList()
    //    {
    //        StructureList.DataSource = null;
    //        StructureList.Items.Clear();
    //        //StructureList.DataSource = new BindingList<Structure>(SIROnlineSession.Instance.CurrentProject.structures);
    //        foreach (Structure item in SIROnlineSession.Instance.CurrentProject.structures)
    //        {
    //            StructureList.Items.Add(new StringObjectPair<Structure>(item.StructureName, item));
    //        }
    //    }

    //    //public void InitialiseDiveListBox()
    //    //{

    //    //    listBox1.Items.Clear();
    //    //    listBox1.DataSource = null;
    //    //    if(SelectedStructure != null)
    //    //    {
    //    //        listBox1.Items.Clear();
    //    //        listBox1.DataSource = new BindingList<Dive>(SelectedStructure.DiveList);
    //    //    }

    //    //}

    //    private void StructureList_SelectedIndexChanged(object sender, EventArgs e)
    //    {

    //        SelectedStructure = ((StringObjectPair<Structure>)StructureList.SelectedItem).value;
    //        Console.WriteLine(" selected structure :" + SelectedStructure.StructureName);
    //    }

    //    private void AddNewDiveBtn_Click(object sender, EventArgs e)
    //    {

    //    }




    //    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    //    {

    //    }

    //    private void NewDivingSession_FormClosing(object sender, FormClosingEventArgs e)
    //    {
    //        if (e.CloseReason == CloseReason.UserClosing)
    //        {
    //            e.Cancel = true;
    //            this.Hide();
    //        }
    //    }

    //    private void NewDivingSession_Shown(object sender, EventArgs e)
    //    {
    //        InitializeStructureList();
    //    }
    //    public struct StringObjectPair<T>
    //    {
    //        public string name;
    //        public T value;

    //        public StringObjectPair(string name, T value)
    //        {
    //            this.name = name;
    //            this.value = value;
    //        }

    //        public override string ToString()
    //        {
    //            return name;
    //        }
    //    }

    //    private void button1_Click(object sender, EventArgs e)
    //    {
    //        SIROnlineSession.Instance.CurrentStructure = SelectedStructure;
    //        this.Hide();
    //    }
    //}

}
