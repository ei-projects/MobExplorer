using System;
using System.Drawing;
using System.Windows.Forms;

namespace MobExplorer.Forms
{
    public partial class SettingsForm : Form
    {
        public bool scriptShowAtStartup;
        public Color scriptBgColor;
        public string scriptSyntax;
        public SettingsForm()
        {
            InitializeComponent();
            scriptShowAtStartup = false;
            scriptBgColor = Color.White;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            pictureBox1.BackColor = colorDialog1.Color;
        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = scriptShowAtStartup;
            pictureBox1.BackColor = scriptBgColor;
            textBox1.Text = scriptSyntax;
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            scriptShowAtStartup = checkBox1.Checked;
            scriptBgColor = pictureBox1.BackColor;
            scriptSyntax = textBox1.Text;
        }
    }
}
