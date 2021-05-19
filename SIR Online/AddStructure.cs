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
    public partial class AddStructure : Form
    {
       // DivingType _diveType;
        //public DivingType GetCurrentDiveType
        //{
        //    get
        //    {
        //        return _diveType;
        //    }
        //}
       
        public Structure CreatedStructure;
        public AddStructure()
        {
            InitializeComponent();
           
        }
        public void InitializeRadioButton()
        {
          
        }

        bool AllCheckBoxLeftEmpty
        {
            get
            {
                if (!AirDivingChkBox.Checked && !SatDivingChkBox.Checked && !ROVChkBox.Checked)
                    return true;
                else
                    return false;
            }
        }

        public void CreateNewStructure()
        {
            CreatedStructure = new Structure(StructureNameTB.Text);
            if (AirDivingChkBox.Checked)
                CreatedStructure.AddDiveType(DivingType.Air_Diving);
            if (SatDivingChkBox.Checked)
                CreatedStructure.AddDiveType(DivingType.Sat_Diving);
            if (ROVChkBox.Checked)
                CreatedStructure.AddDiveType(DivingType.ROV);
           
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(StructureNameTB.Text) && AllCheckBoxLeftEmpty == false)
            {
                //button1.DialogResult = DialogResult.OK;
                //if (ROVRBtn.Checked)
                //{
                //    CreateNewStructure(DivingType.ROV);
                //}else if (AirDiveRBtn.Checked)
                //{
                //    CreateNewStructure(DivingType.Air_Diving);
                //}else if (SatDiveRBtn.Checked)
                //{
                //    CreateNewStructure(DivingType.Sat_Diving);
                //}
                CreateNewStructure();

            }
            else
                AddBtn.DialogResult = DialogResult.None;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
