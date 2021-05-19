using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIR_Online
{
    public partial class DataBlockPanel : UserControl
    {
        public int BlockNumber;
        int selectedIdx;
        
        public DataBlockPanel(int _blocknumber,int _selectedIdx)
        {
            
            InitializeComponent();
            BlockNumber = _blocknumber;
            selectedIdx = _selectedIdx;
            InitializeDataBlockPanel();
        }

        public void InitializeDataBlockPanel()
        {
            for (int i = 0; i < BlockNumber; i++)
            {
                DataBlockOrderCB.Items.Add( i);
                DataBlockLbl.Text = i.ToString();
            }
            DataBlockLbl.Text = (selectedIdx+1).ToString();
            DataBlockOrderCB.SelectedIndex = selectedIdx;

        }





        struct StringObjectPair<T>
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

    }

}
