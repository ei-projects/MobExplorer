using GuiLib;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MobExplorer
{
    public partial class StringEditForm : Form
    {
        AutocompleteMenu popupMenu;
        string textBackup;
        public void setDescriptionFile(string descriptionFile)
        {
            try
            {
                textBox1.DescriptionFile = descriptionFile;
                textBox1.Text += "";
            }
            catch
            {
                textBox1.DescriptionFile = "";
            }
            textBox1.Text += "";
        }
        public StringEditForm()
        {
            InitializeComponent();
            textBox1.AcceptsTab = true;
            GuiHelper.setSelfDir();
            GuiHelper.restorePrevDir();
            setDescriptionFile("ei_syntax.xml");
            popupMenu = new AutocompleteMenu(textBox1);
            popupMenu.SearchPattern = @"[a-zA-Z]";
            popupMenu.AllowTabKey = true;
            popupMenu.ToolTipDuration = 15000;
            BuildAutocompleteMenu();
        }
        private string getMethodName(string refs)
        {
            int pos = refs.IndexOf('(');
            if (pos >= 0)
                return refs.Substring(0, pos);
            return refs;
        }
        private string formatReference(string refs)
        {
            string result = "";
            refs = refs.Replace("``", "\n");
            if (refs[0] == '\"' && refs[refs.Length - 1] == '\"')
                refs = refs.Substring(1, refs.Length - 2);
            int len = 0, maxLen = 70;
            for (int i = 0; i < refs.Length; i++)
            {
                len++;
                if (refs[i] == '\n')
                    len = 0;

                if (len > maxLen && (refs[i] == ' ' ||
                    (i + 2 < refs.Length && refs.Substring(i, 2) == ". ")))
                {
                    if (refs[i] == '.')
                    {
                        result += refs[i];
                        i++;
                    }
                    result += '\n';
                    len = 0;
                }
                else
                    result += refs[i];
            }
            return result;
        }
        public class ProcedureItem : AutocompleteItem
        {
            private string lowerText;
            public ProcedureItem(string text, int imageIndex = -1)
                : base(text, imageIndex)
            {
                lowerText = text.ToLower();
            }

            public override CompareResult Compare(string fragmentText)
            {
                fragmentText = fragmentText.ToLower();
                if (lowerText.StartsWith(fragmentText))
                    return CompareResult.VisibleAndSelected;
                else if ( //lowerText[0] == fragmentText[0] ||
                    (fragmentText.Length >= 3 && lowerText.IndexOf(fragmentText) >= 0))
                {
                    return CompareResult.Visible;
                }
                return CompareResult.Hidden;
            }
        }
        private void BuildAutocompleteMenu()
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();
            string[] keywords = { "GlobalVars", "DeclareScript", "Script", "WorldScript", "object", "float", "string", "group", "if", "then" };
            foreach (var item in keywords)
                items.Add(new AutocompleteItem(item) { });

            try
            {
                GuiHelper.setSelfDir();
                StreamReader reader = new StreamReader("script_refs.txt", Encoding.GetEncoding("windows-1251"));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split('\t');
                    items.Add(new ProcedureItem(getMethodName(line[0]))
                    {
                        ToolTipTitle = line[0],
                        ToolTipText = formatReference(line[1]),
                    });
                }
                GuiHelper.restorePrevDir();
            }
            catch
            {
                string[] standardProcedures = { "ActivateTrap", "Add", "AddLoot", "AddMob", "AddObject", "AddRectToArea", "AddRoundToArea", "AddUnitToParty", "AddUnitToServer", "AddUnitUnderControl", "AlarmPosX", "AlarmPosY", "AlarmTime", "Any", "AttachParticles", "AttachParticleSource", "Attack", "BlockUnit", "Cast", "CastSpellPoint", "CastSpellUnit", "ConsoleFloat", "ConsoleString", "CopyItems", "CopyLoot", "CopyStats", "Crawl", "CreateFX", "CreateFXSource", "CreateLightning", "CreateParticleSource", "CreateParty", "CreatePointLight", "CreateRandomizeFXSource", "DeleteArea", "DeleteFXSource", "DeleteLightning", "DeleteParticleSource", "DeletePointLight", "DistanceUnitPoint", "DistanceUnitUnit", "Div", "EnableLever", "EraseQuestItem", "Every", "FixItems", "FixWorldTime", "Follow", "For", "ForIf", "GetAIClass", "GetBSZValue", "GetDiplomacy", "GetFutureX", "GetFutureY", "GetLeader", "GetLeverState", "GetLootItemsCount", "GetMercsNumber", "GetMoney", "GetObject", "GetObjectByID", "GetObjectByName", "GetObjectID", "GetPlayer", "GetPlayerUnits", "GetUnitOfPlayer", "GetWorldTime", "GetX", "GetY", "GetZ", "GetZValue", "GiveDexterity", "GiveIntelligence", "GiveItem", "GiveMoney", "GiveQuestItem", "GiveSkill", "GiveStrength", "GiveUnitQuestItem", "GiveUnitSpell", "GodMode", "GroupAdd", "GroupCross", "GroupHas", "GroupSee", "GroupSize", "GroupSub", "GSDelVar", "GSGetVar", "GSSetVar", "GSSetVarMax", "Guard", "HaveItem", "HideObject", "HP", "Idle", "InflictDamage", "InvokeAlarm", "IsAlarm", "IsAlive", "IsCameraPlaying", "IsDead", "IsEnemy", "IsEqual", "IsEqualString", "IsGreater", "IsInArea", "IsInSquare", "IsLess", "IsNight", "IsPlayerInDanger", "IsPlayerInSafety", "IsUnitBlocked", "IsUnitInWater", "IsUnitVisible", "KillScript", "KillUnit", "LeaveToZone", "Lie", "Mana", "MaxHP", "MaxMana", "MoveParticleSource", "MovePointLight", "MoveToObject", "MoveToPoint", "Mul", "Not", "PlayAnimation", "PlayCamera", "PlayerSee", "PlayFX", "PlayMovie", "QFinish", "QObjArea", "QObjGetItem", "QObjKillGroup", "QObjKillUnit", "QObjSeeObject", "QObjSeeUnit", "QObjUse", "QStart", "QuestComplete", "Random", "RecalcMercBriefings", "RedeployParty", "RemoveObject", "RemoveObjectFromServer", "RemoveParty", "RemoveQuestItem", "RemoveUnitFromControl", "RemoveUnitFromParty", "RemoveUnitFromServer", "ResetTarget", "Rest", "RotateTo", "Run", "RunWorldTime", "SendEvent", "SendStringEvent", "Sentry", "SetCameraOrientation", "SetCameraPosition", "SetCP", "SetCPFast", "SetCurrentParty", "SetDiplomacy", "SetDirectionToObject", "SetEnemy", "SetParticleSourceSize", "SetPlayer", "SetPlayerAggression", "SetScience", "SetSpellAggression", "SetSunLight", "SetWaterLevel", "SetWind", "ShowBitmap", "ShowCredits", "Sleep", "SleepUntil", "SleepUntilIdle", "Stand", "StartAnimation", "Sub", "SwitchLeverState", "SwitchLeverStateEx", "UMAg", "UMAggression", "UMClear", "UMCorpseWatcher", "UMFear", "UMFollow", "UMGuard", "UMGuardEx", "UMPatrol", "UMPatrolAddPoint", "UMPatrolAddPointLook", "UMPatrolClear", "UMPlayer", "UMRevenge", "UMSentry", "Walk", "CreateRandomizedFXSource", "PlayMusic", "SetBackGroundColor", "UMStandard", "UMSuspection", "UnitInSquare", "UnitSee", "WaitEndAnimation", "WaitSegment", "WasLooted" };
                foreach (var item in standardProcedures)
                    items.Add(new ProcedureItem(item) { });
            }
            popupMenu.Items.SetAutocompleteItems(items);
        }

        private string getSelfPath()
        {
            string fullPath = Application.ExecutablePath;
            int pos = fullPath.LastIndexOf('\\');
            fullPath = fullPath.Substring(0, pos);
            return fullPath;
        }

        private void checkSyntax()
        {
            string dir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(getSelfPath());
            string output;

            try
            {
                StreamWriter file = new StreamWriter("script.txt");
                file.Write("GlobalVars(Heroes:group)");
                file.Write(textBox1.Text);
                file.Close();

                Process p = new Process();
                // Redirect the output stream of the child process.
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = "eisc_con.exe";
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
            }
            catch (Exception e)
            {
                try { File.Delete("script.txt"); }
                catch { }

                Directory.SetCurrentDirectory(dir);
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                try { File.Delete("script.txt"); }
                catch { }

                return;
            }

            try { File.Delete("script.txt"); }
            catch { }

            int line = 0;
            Color color = textBox1.CurrentLineColor;
            try
            {
                if (output.Substring(0, 5) == "Line ")
                {
                    string line_str = output.Substring(5, output.IndexOf(':') - 5);
                    line = Int32.Parse(line_str);
                    if (line > 0)
                    {
                        textBox1[line - 1].LastVisit = DateTime.Now;
                        textBox1.Navigate(line - 1);
                        color = textBox1.CurrentLineColor;
                        textBox1.CurrentLineColor = Color.Red;
                    }
                    else
                        output = "No errors were found";
                }
            }
            catch { }

            MessageForm.Show(output, "Result");
            textBox1.CurrentLineColor = color;
            Directory.SetCurrentDirectory(dir);
        }

        private void StringEditForm_Load(object sender, EventArgs e)
        {
            textBox1.ClearUndo();
            textBox1.Select();
            textBackup = textBox1.Text;
        }
        private void gotoReference()
        {
            string text = textBox1.Text;
            int pos = textBox1.SelectionStart;
            int pos1, pos2;
            pos1 = pos2 = pos;
            while (pos1 >= 0 && Regex.IsMatch(text[pos1].ToString(), @"[a-zA-Z#_0-9]"))
                --pos1;
            while (pos2 < text.Length && Regex.IsMatch(text[pos2].ToString(), @"[a-zA-Z#_0-9]"))
                ++pos2;
            ++pos1;
            string id = text.Substring(pos1, pos2 - pos1);
            List<int> lines = textBox1.FindLines(@"^\s*Script\s+" + id + @"\s*\(?\s*$", RegexOptions.Multiline);
            if (lines.Count > 0)
            {
                textBox1[lines[0]].LastVisit = DateTime.Now;
                textBox1.Navigate(lines[0]);
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                e.Handled = e.SuppressKeyPress = true;
                Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else if (e.Alt && e.KeyCode == Keys.Enter)
            {
                if (WindowState == FormWindowState.Maximized)
                    WindowState = FormWindowState.Normal;
                else
                    WindowState = FormWindowState.Maximized;
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F9)
            {
                checkSyntax();
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Apps)
            {
                Point point = textBox1.PositionToPoint(textBox1.SelectionStart);
                point.Y += textBox1.CharHeight;
                cmMain.Show(textBox1.PointToScreen(point));
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F12)
            {
                gotoReference();
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.OemMinus)
            {
                textBox1.NavigateBackward();
                e.Handled = e.SuppressKeyPress = true;
            }

            //else if (e.Control && )
            //{
            //    textBox1.NavigateBackward();
            //    e.Handled = e.SuppressKeyPress = true;
            //}


            //else if (e.KeyCode == Keys.Tab)
            //{
            //    textBox1.SuspendLayout();
            //    var insertText = "    ";
            //    var selectionIndex = textBox1.SelectionStart;
            //    textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            //    textBox1.SelectionStart = selectionIndex + insertText.Length;
            //    e.Handled = e.SuppressKeyPress = true;
            //    textBox1.ResumeLayout();
            //}
        }
        private void button3_Click(object sender, EventArgs e)
        {
            checkSyntax();
        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Selection.SelectAll();
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.UndoEnabled)
                textBox1.Undo();
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.RedoEnabled)
                textBox1.Redo();
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.ShowFindDialog();
        }
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.ShowReplaceDialog();
        }
        private void commentSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.InsertLinePrefix("//");
        }
        private void uncommentSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.RemoveLinePrefix("//");
        }

        private void goToReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gotoReference();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                cmMain.Show(Cursor.Position);
        }

        private void StringEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
                return;

            if (textBackup == textBox1.Text ||
                MessageBox.Show("Do you really want to discard changes?", "Warning",
                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                e.Cancel = true;
            }
        }

    }
}
