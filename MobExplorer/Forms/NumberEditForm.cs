using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MobExplorer
{
    public enum Edit_Type
    {
        BYTE = 1, DWORD = 2, FLOAT = 3
    }
    public partial class NumberEditForm : Form
    {
        public Edit_Type type;
        public NumberEditForm()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = true;
            float f;
            byte b;
            uint u;
            switch (type)
            {
                case Edit_Type.BYTE: if (!byte.TryParse(textBox1.Text, out b)) flag = false; break;
                case Edit_Type.DWORD: if (!uint.TryParse(textBox1.Text, out u)) flag = false; break;
                case Edit_Type.FLOAT: if (!float.TryParse(textBox1.Text, out f)) flag = false; break;
            }
            if (flag)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
