using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using MobLib;
using MobExplorer.Forms;
using System.Drawing;

namespace MobExplorer
{
    public partial class MainForm : Form
    {
        private List<MobFile> MobFiles;
        private MobFile CurrentMob;
        private MobFileSection[] SectionBuffer;

        private SettingsForm Settings;
        private StringEditForm StringEdit;
        private NumberEditForm NumberEdit;
        private AboutBox About;
        private UnitStatsEditForm UnitStatsEdit;

        private bool scriptShowAtStartup;
        private Color scriptBgColor;
        private string scriptSyntax;

        public MainForm()
        {
            InitializeComponent();
        }
        private void loadSettings()
        {
            scriptShowAtStartup = Convert.ToBoolean(Registry.GetValue(@"HKEY_CURRENT_USER\Software\MobExplorer",
                "ScriptAtStartup", 0));
            scriptBgColor = Color.FromArgb(Convert.ToInt32(Registry.GetValue(@"HKEY_CURRENT_USER\Software\MobExplorer",
                "ScriptBgColor", Color.White.ToArgb())));
            scriptSyntax = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\MobExplorer",
                "ScriptSyntax", "ei_syntax.xml");
            StringEdit.textBox1.BackColor = scriptBgColor;
            StringEdit.setDescriptionFile(scriptSyntax);
        }
        private void saveSettings()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\MobExplorer",
                "ScriptAtStartup", Convert.ToInt32(scriptShowAtStartup));
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\MobExplorer",
                "ScriptBgColor", scriptBgColor.ToArgb());
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\MobExplorer",
                "ScriptSyntax", scriptSyntax);
        }
        private ArrayList getRecentlyList()
        {
            String[] value = (String[])Registry.GetValue(@"HKEY_CURRENT_USER\Software\MobExplorer",
                "RecentList", new String[] { });
            if (value == null)
                value = new String[] { };

            return new ArrayList(value);
        }

        private void addFileToRecentlyList(string filePath)
        {
            ArrayList recentList = getRecentlyList();
            int index = recentList.IndexOf(filePath);
            if (index >= 0)
                recentList.RemoveAt(index);
            recentList.Insert(0, filePath);

            if (recentList.Count > 10)
                recentList.RemoveRange(10, recentList.Count - 10);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\MobExplorer",
                "RecentList", recentList.ToArray(typeof(string)));
            initRecentlyList();
        }
        private void recentPathClick(object sender, EventArgs e)
        {
            string filename = ((ToolStripMenuItem)sender).Text;
            openMob(filename);
        }

        private void openMob(string path)
        {
            var mob = new MobFile();
            ErrorCodes result = mob.OpenMobFile(path);
            if (result == ErrorCodes.OK)
            {
                MobFiles.Add(mob);
                addFileToRecentlyList(path);
                CurrentMob = mob;
                if (scriptShowAtStartup)
                {
                    if (MobHelper.openSectionById(mob, SectionId.ID_OBJECTDBFILE) &&
                        (MobHelper.openSectionById(mob, SectionId.ID_SS_TEXT) || MobHelper.openSectionById(mob, SectionId.ID_SS_TEXT_OLD)))
                    {
                        if (EditSection())
                            CurrentMob.Changed = true;
                        else
                            CurrentMob.CurrentSection = mob.CurrentSection.Owner;
                    }
                }
            }
            else
            {
                if (result == ErrorCodes.CannotOpen)
                    MessageBox.Show("Cannot open " + path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Invalid mob " + path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CurrentMob = null;
            }
            ShowCurrentSection();
        }
        private void initRecentlyList()
        {
            ArrayList recentList = getRecentlyList();
            recentToolStripMenuItem.DropDownItems.Clear();

            for (int i = 0; i < recentList.Count; i++)
                recentToolStripMenuItem.DropDownItems.Add((string)recentList[i], null, recentPathClick);

            if (recentToolStripMenuItem.DropDownItems.Count == 0)
                recentToolStripMenuItem.DropDownItems.Add("-");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MobFiles = new List<MobFile>();
            CurrentMob = null;
            listView1.AllowColumnReorder = true;
            StringEdit = new StringEditForm();
            NumberEdit = new NumberEditForm();
            Settings = new SettingsForm();
            About = new AboutBox();
            UnitStatsEdit = new UnitStatsEditForm();

            loadSettings();

            Text = String.Format("MobExplorer {0}", Application.ProductVersion);
            initRecentlyList();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                for (int i = 1; i < args.Length; ++i)
                    openMob(args[i]);
            }

            ShowCurrentSection();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < openFileDialog1.FileNames.Length; ++i)
                {
                    if (!System.IO.File.Exists(openFileDialog1.FileNames[i]))
                        continue;
                    openMob(openFileDialog1.FileNames[i]);
                }
            }
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentMob != null)
                MobFiles.Remove(CurrentMob);
            else
            {
                for (int i = listView1.SelectedItems.Count - 1; i >= 0; --i)
                {
                    MobFiles.RemoveAt(listView1.SelectedIndices[i]);
                    listView1.SelectedItems[i].Remove();
                }
            }
            CurrentMob = null;
            ShowCurrentSection();
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveMobFile();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentMob != null)
            {
                CurrentMob.SaveMobFile(CurrentMob.Filename);
                ShowCurrentSection();
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About.ShowDialog();
        }

        private void returnSection()
        {
            if (CurrentMob == null || CurrentMob.CurrentSection == null)
                return;
            if (CurrentMob.CurrentSection.Owner == null)
                CurrentMob = null;
            else
                CurrentMob.ReturnSection();
            ShowCurrentSection();
        }
        private int getRealIndex(int listViewIndex)
        {
            int index = -1;
            try
            {
                index = Convert.ToInt32(listView1.Items[listViewIndex].SubItems[1].Text) - 1;
            }
            catch { }
            return index;
        }
        private void openlistViewItem()
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            int index = listView1.SelectedItems[0].Index;
            int sectionIndex = getRealIndex(index);
            if (CurrentMob == null)
                CurrentMob = MobFiles[index];
            else if (index == 0 || sectionIndex < 0)
            {
                returnSection();
                return;
            }
            else if (CurrentMob.CurrentSection.Items[sectionIndex].info.Type == SectionType.ST_REC)
            {
                if (CurrentMob.CurrentSection.UserValue == null || ((int[])CurrentMob.CurrentSection.UserValue).Length == 0)
                    CurrentMob.CurrentSection.UserValue = new int[1];
                ((int[])CurrentMob.CurrentSection.UserValue)[0] = listView1.TopItem.Index;
                //MessageBox.Show(listView1.TopItem.Text);

                CurrentMob.CurrentSection = CurrentMob.CurrentSection.Items[sectionIndex];
                CurrentMob.CurrentSection.ReadSubsections();
            }
            else
            {
                var sec = CurrentMob.CurrentSection;
                CurrentMob.CurrentSection = CurrentMob.CurrentSection.Items[sectionIndex];
                if (!EditSection())
                {
                    CurrentMob.CurrentSection = sec;
                    return;
                }
                else
                    CurrentMob.Changed = true;

            }

            ShowCurrentSection();
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            openlistViewItem();
        }
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    if (!e.Control)
                        break;
                    switch (listView1.View)
                    {
                        case View.Details: listView1.View = View.LargeIcon; break;
                        case View.LargeIcon: listView1.View = View.List; break;
                        case View.List: listView1.View = View.Details; FixColumns(); break;
                    }
                    break;
                case Keys.Enter:
                    openlistViewItem();
                    break;
                case Keys.Back:
                    returnSection();
                    break;
            }
        }
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < path.Length; ++i)
                {
                    if (!System.IO.File.Exists(path[i]))
                        continue;
                    openMob(path[i]);
                }
            }

        }
        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();
            listView1.Sort();
            listView1.ListViewItemSorter = lvwColumnSorter;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView1.Sort();
        }

        private void FixColumns()
        {
            if (listView1.View != View.Details) return;
            listView1.Columns.Clear();
            if (CurrentMob == null) listView1.Columns.Add("Mob-File Name");
            else
            {
                listView1.Columns.Add("Section Name");
                listView1.Columns.Add("Index");
                listView1.Columns.Add("Section Type");
                listView1.Columns.Add("Section Size");
                if (CurrentMob.CurrentSection != null && CurrentMob.CurrentSection.info != null &&
                    CurrentMob.CurrentSection.info.Id == SectionId.ID_OBJECTSECTION)
                {
                    listView1.Columns.Add("Object ID");
                    listView1.Columns.Add("Object Name");
                    listView1.Columns.Add("Object Prototype");
                }
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private MobFileObjectInfo GetObjectInfo(MobFileSection section)
        {
            if (!section.Readed)
                section.ReadSubsections();
            MobFileObjectInfo obj = new MobFileObjectInfo();
            ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            int delta, int_val = -1;
            string str_val = "";
            for (int i = 0; i < section.Items.Count; ++i)
            {
                SectionType type = section.Items[i].info.Type;

                delta = BitConverter.ToInt32(section.Items[i].Data, 4) - 8;
                if (type == SectionType.ST_STRING)
                    str_val = encoding.GetString(section.Items[i].Data, 8, delta);
                else if (type == SectionType.ST_DWORD)
                    int_val = BitConverter.ToInt32(section.Items[i].Data, 8);
                switch (section.Items[i].info.Id)
                {
                    case SectionId.ID_OBJNAME: obj.Name = str_val; break;
                    case SectionId.ID_UNIT_PROTOTYPE: obj.Prototype = str_val; break;
                    case SectionId.ID_NID: obj.Id = int_val.ToString(); break;
                }
            }

            return obj;
        }
        private void ShowCurrentSection()
        {
            listView1.Clear();
            if (MobFiles.Count == 0)
            {
                FixColumns();
                statusStrip1.Items[0].Text = "0";
                statusStrip1.Items[1].Text = "";
                return;
            }
            listView1.ListViewItemSorter = null;
            listView1.SuspendLayout();

            if (CurrentMob == null)
            {
                statusStrip1.Items[0].Text = MobFiles.Count.ToString();
                statusStrip1.Items[1].Text = "";
                for (int i = 0; i < MobFiles.Count; ++i)
                    listView1.Items.Add(MobFiles[i].Filename.Substring(MobFiles[i].Filename.LastIndexOf('\\') + 1), 0);
            }
            else
            {
                MobFileSectionInfo parent_info = null;
                if (CurrentMob.CurrentSection.Owner != null)
                    parent_info = CurrentMob.CurrentSection.info;

                statusStrip1.Items[0].Text = CurrentMob.CurrentSection.Items.Count.ToString();
                statusStrip1.Items[1].Text = CurrentMob.Filename;
                if (CurrentMob.Changed) statusStrip1.Items[1].Text += " *";
                listView1.Items.Add("Up", 3);
                for (int i = 0; i < CurrentMob.CurrentSection.Items.Count; ++i)
                {
                    MobFileSectionInfo info = CurrentMob.CurrentSection.Items[i].info;
                    string name = info.Id.ToString("g");
                    switch (info.Type)
                    {
                        case SectionType.ST_REC:
                            listView1.Items.Add(name, 0);
                            break;
                        case SectionType.ST_SCRIPT:
                        case SectionType.ST_SCRIPT_ENC:
                        case SectionType.ST_STRING:
                            listView1.Items.Add(name, 2);
                            break;
                        case SectionType.ST_UNK:
                            listView1.Items.Add(name, 4);
                            break;

                        default:
                            listView1.Items.Add(name, 1);
                            break;
                    }

                    listView1.Items[i + 1].SubItems.Add((i + 1).ToString());
                    listView1.Items[i + 1].SubItems.Add(info.Type.ToString());
                    listView1.Items[i + 1].SubItems.Add(BitConverter.ToInt32(
                        CurrentMob.CurrentSection.Items[i].Data, 4).ToString());

                    if (parent_info != null && parent_info.Id == SectionId.ID_OBJECTSECTION)
                    {
                        MobFileObjectInfo obj = GetObjectInfo(CurrentMob.CurrentSection.Items[i]);
                        listView1.Items[i + 1].SubItems.Add(obj.Id);
                        listView1.Items[i + 1].SubItems.Add(obj.Name);
                        listView1.Items[i + 1].SubItems.Add(obj.Prototype);
                    }
                }
            }
            FixColumns();
            listView1.ResumeLayout();

            if (CurrentMob != null && CurrentMob.CurrentSection.UserValue != null && ((int[])CurrentMob.CurrentSection.UserValue).Length > 0)
            {
                listView1.TopItem = listView1.Items[((int[])CurrentMob.CurrentSection.UserValue)[0]];
                listView1.TopItem = listView1.Items[((int[])CurrentMob.CurrentSection.UserValue)[0]];
                listView1.TopItem = listView1.Items[((int[])CurrentMob.CurrentSection.UserValue)[0]];
                //MessageBox.Show(listView1.Items[CurrentMob.CurrentSection.UserValue[0]].Text + " <-> " + listView1.TopItem.Text);
            }
        }
        private void Delete()
        {
            int size = 0;
            var toRemove = new List<int>();
            if (CurrentMob != null && listView1.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listView1.SelectedIndices.Count; ++i)
                {
                    int index = getRealIndex(listView1.SelectedIndices[i]);
                    if (index >= 0)
                    {
                        size += BitConverter.ToInt32(CurrentMob.CurrentSection.Items[index].Data, 4);
                        toRemove.Add(index);
                    }
                }

                toRemove.Sort((a, b) => b.CompareTo(a));
                for (var i = 0; i < toRemove.Count; i++)
                    CurrentMob.CurrentSection.Items.RemoveAt(toRemove[i]);

                CurrentMob.FixSize(-size);
                ShowCurrentSection();
            }
        }
        private void Copy()
        {
            int count = listView1.SelectedIndices.Count;
            if (CurrentMob == null || count <= 0) return;
            if (listView1.Items[0].Selected)
            {
                listView1.Items[0].Selected = false;
                --count;
            }
            if (count <= 0) return;
            SectionBuffer = new MobFileSection[count];
            for (int i = 0; i < count; ++i)
                SectionBuffer[i] = CurrentMob.CurrentSection.Items[getRealIndex(listView1.SelectedIndices[i])];

        }
        private void Paste()
        {
            if (CurrentMob == null || SectionBuffer == null || SectionBuffer.Length <= 0) return;
            MobFileSection cur = CurrentMob.CurrentSection, newsect;
            int delta = 0;
            for (int i = 0; i < SectionBuffer.Length; ++i)
            {
                newsect = SectionBuffer[i].Clone(cur);
                CurrentMob.CurrentSection.Items.Add(newsect);
                delta += BitConverter.ToInt32(newsect.Data, 4);
            }
            CurrentMob.FixSize(delta);
            CurrentMob.Changed = true;
            ShowCurrentSection();
        }
        private bool EditSection()
        {
            MobFileSection cur = CurrentMob.CurrentSection;
            var encoding = Encoding.GetEncoding(1251);
            MobFileSectionInfo info = cur.info;

            switch (info.Type)
            {
                case SectionType.ST_STRING:
                case SectionType.ST_SCRIPT:
                case SectionType.ST_SCRIPT_ENC:
                    StringEdit.textBox1.Text = cur.toString();
                    if (StringEdit.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return false;

                    cur.setString(StringEdit.textBox1.Text);
                    break;

                case SectionType.ST_BYTE:
                    NumberEdit.type = Edit_Type.BYTE;
                    NumberEdit.textBox1.Text = cur.toNumber().ToString();
                    if (NumberEdit.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return false;
                    cur.setNumber(byte.Parse(NumberEdit.textBox1.Text));
                    break;

                case SectionType.ST_DWORD:
                    NumberEdit.type = Edit_Type.DWORD;
                    NumberEdit.textBox1.Text = cur.toNumber().ToString();
                    if (NumberEdit.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return false;
                    cur.setNumber(uint.Parse(NumberEdit.textBox1.Text));
                    break;

                case SectionType.ST_FLOAT:
                    NumberEdit.type = Edit_Type.FLOAT;
                    NumberEdit.textBox1.Text = cur.toNumber().ToString();
                    if (NumberEdit.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return false;
                    cur.setNumber(float.Parse(NumberEdit.textBox1.Text));
                    break;

                case SectionType.ST_STR_S06:
                    UnitStatsEdit.Data = cur.Data;
                    UnitStatsEdit.ShowDialog();
                    break;

                default:
                    return false;
            }

            CurrentMob.ReturnSection();
            return true;
        }
        private void SaveMobFile()
        {
            MobFile mob = null;
            if (CurrentMob != null) mob = CurrentMob;
            else if (listView1.SelectedItems.Count > 0) mob = MobFiles[listView1.SelectedIndices[0]];
            if (mob == null) return;
            saveFileDialog1.InitialDirectory = mob.Filename.Substring(0, mob.Filename.LastIndexOf('\\'));
            saveFileDialog1.FileName = mob.Filename.Substring(mob.Filename.LastIndexOf('\\') + 1);
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                mob.SaveMobFile(saveFileDialog1.FileName);
            ShowCurrentSection();
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.scriptBgColor = scriptBgColor;
            Settings.scriptShowAtStartup = scriptShowAtStartup;
            Settings.scriptSyntax = scriptSyntax;
            Settings.ShowDialog();
            scriptBgColor = Settings.scriptBgColor;
            scriptShowAtStartup = Settings.scriptShowAtStartup;
            scriptSyntax = Settings.scriptSyntax;
            saveSettings();
            loadSettings();
        }
    }

    internal static class SafeNativeMethods
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);
    }

    public sealed class NaturalStringComparer : IComparer<string>
    {
        public int Compare(string a, string b)
        {
            return SafeNativeMethods.StrCmpLogicalW(a, b);
        }
    }

    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int ColumnToSort;
        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder OrderOfSort;
        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare;

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;
            NaturalStringComparer comparer = new NaturalStringComparer();

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            if (listviewX.Text == "Up")
                return -1;
            else if (listviewY.Text == "Up")
                return 1;

            string a = listviewX.SubItems[ColumnToSort].Text, b = listviewY.SubItems[ColumnToSort].Text;
            if (a == "")
                compareResult = 1;
            else if (b == "")
                compareResult = -1;
            else
                compareResult = comparer.Compare(a, b);

            if (compareResult == 0)
            {
                try
                {
                    compareResult = ObjectCompare.Compare(Convert.ToInt32(listviewX.SubItems[1].Text), Convert.ToInt32(listviewY.SubItems[1].Text));
                }
                catch
                {
                    compareResult = ObjectCompare.Compare(listviewX.SubItems[0].Text, listviewY.SubItems[0].Text);
                }
            }

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }

    }
}

