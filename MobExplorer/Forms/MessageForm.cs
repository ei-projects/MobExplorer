using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MobExplorer
{
    public partial class MessageForm : Form
    {
        public MessageForm()
        {
            InitializeComponent();
        }
        static public DialogResult Show(string text, string caption)
        {
            MessageForm form = new MessageForm();
            form.Text = caption;
            form.label1.Text = text;
            return form.ShowDialog();
        }
    }
}
