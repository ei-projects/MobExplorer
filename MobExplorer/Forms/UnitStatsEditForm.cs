using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MobExplorer
{   
    
    public partial class UnitStatsEditForm : Form
    {
        //public MobUnitStats Stats;
        public byte[] Data;
        public UnitStatsEditForm()
        {
            InitializeComponent();
        }

        private void UnitStatsEditForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = BitConverter.ToSingle(Data, 28).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float f;
            if (float.TryParse(textBox1.Text, out f))
            {
                Array.Copy(BitConverter.GetBytes(f), 0, Data, 28, 4);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

    }

}
