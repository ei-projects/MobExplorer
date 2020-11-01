using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MobExplorer
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", Application.ProductName);
            richTextBox1.ReadOnly = true;
            this.richTextBox1.Rtf =
            @"{\rtf1\ansi\ansicpg1251\deff0\deflang1049\deflangfe1049\deftab708{\fonttbl{\f0\froman\fprq2\fcharset204" +
            @"{\*\fname Times New Roman;}Times New Roman CYR;}{\f1\fswiss\fprq2\fcharset204 Calibri;}}" +
            @"{\colortbl ;\red0\green0\blue255;}{\*\generator Msftedit 5.41.21.2510;}\viewkind4\uc1\pard\sl276\slmult1\b\f0\fs24" +
            @"Mob Explorer " + Application.ProductVersion +
            @"\par\par\b0\fs20 This is a program to explore and edit mob files.\par " +
            FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright +
            @"\par\pard Support e-mail: {\fldrslt{\cf1\ul demoth@yandex.ru}}" +
            @"\cf0\ulnone\f0\fs20\par\par MobFile description by MobReversingTool\par" +
            @"(more information on {\fldrslt{\ul\cf1 www.gipat.ru}}\f0\fs20 )\par" +
            @"\pard\sa200\sl276\slmult1\f1\fs22}";
        }
        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            okButton.Focus();
        }
    }
}
