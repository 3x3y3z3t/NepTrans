// ;
using ExwSharp;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NepTrans
{
    public partial class Form1 : Form
    {
        const string defaultRootDir = @"D:\AllNepPj";
        const string nepRb1RootDir = @"\nep_rb1";

        NepEntryManager MainGameEntryManager;


        Entry CurrentEntry = null;

        public Form1()
        {
            InitializeComponent();

            tbRootDirectory.Text = defaultRootDir; // this implicitly called InitEntryManager();
            //InitEntryManager();




            btnUpdate.Enabled = false;
            btnSaveEntry.Enabled = false;
            btnKeepOrg.Enabled = false;


            treeDirStruct.ExpandAll();



        }

        private bool InitEntryManager()
        {
            try
            {
                MainGameEntryManager = new NepEntryManager("nep_rb1", tbRootDirectory.Text, nepRb1RootDir);
                treeDirStruct.Nodes.Clear();
                treeDirStruct.Nodes.Add(MainGameEntryManager.EntriesTree);

                SetupProgressDisplay();
                UpdateProgressDisplay();

                return true;
            }
            catch (Exception _e)
            {
                Logger.Log(_e.Message);
                return false;
            }
        }

        private bool SetupProgressDisplay()
        {
            int gameCount = MainGameEntryManager.GameScriptRecordCount;
            int systemCount = MainGameEntryManager.SystemScriptRecordCount;
            int overallCount = gameCount + systemCount;

            if (gameCount == 0 && systemCount == 0)
                return false; // do nothing, use initial setup (50%);

            if (gameCount == 0)
            {
                pbGameScript.Width = pbGameScript.MinimumSize.Width;
                pbSystemScript.Width = pbOverall.Width - pbGameScript.Width - 6;
            }
            else if (systemCount == 0)
            {
                pbSystemScript.Width = pbSystemScript.MinimumSize.Width;
                pbGameScript.Width = pbOverall.Width - pbSystemScript.Width - 6;
            }
            else
            {
                float percent = (float)gameCount / overallCount;
                int pbGameScriptW = (int)(pbOverall.Width * percent);
                if (pbGameScriptW < pbGameScript.MinimumSize.Width)
                    pbGameScriptW = pbGameScript.MinimumSize.Width;

                int pbSystemScriptW = pbOverall.Width - pbGameScriptW - 6;
                if (pbSystemScriptW < pbSystemScript.MinimumSize.Width)
                {
                    pbSystemScriptW = pbSystemScript.MinimumSize.Width;
                    pbGameScriptW = pbOverall.Width - pbSystemScriptW - 6;
                }
                pbGameScript.Width = pbGameScriptW;
                pbSystemScript.Width = pbSystemScriptW;
            }
            pbSystemScript.Location = new Point(pbGameScript.Location.X + pbGameScript.Width + 6, pbSystemScript.Location.Y);
            lblSystemScriptHeader.Location = new Point(pbSystemScript.Location.X, lblSystemScriptHeader.Location.Y);
            lblSystemScriptStat.Location = new Point(lblSystemScriptHeader.Location.X + 80, lblSystemScriptStat.Location.Y);

            pbOverall.Maximum = overallCount;
            pbGameScript.Maximum = gameCount;
            pbSystemScript.Maximum = systemCount;

            return true;
        }

        private bool UpdateProgressDisplay()
        {
            int gameCount = MainGameEntryManager.GameScriptRecordCount;
            int systemCount = MainGameEntryManager.SystemScriptRecordCount;
            int overallCount = gameCount + systemCount;
            int gameCompleteCount = MainGameEntryManager.GameScriptCompletedRecordCount;
            int systemCompleteCount = MainGameEntryManager.SystemScriptCompletedRecordCount;
            int overallCompleteCount = gameCompleteCount + systemCompleteCount;

            try
            {
                pbOverall.Value = overallCompleteCount;
                pbGameScript.Value = gameCompleteCount;
                pbSystemScript.Value = systemCompleteCount;

                lblOverallStat.Text = string.Format("{0}/{1} ({2:F2}%)", overallCompleteCount, overallCount, (float)overallCompleteCount / overallCount * 100);
                lblGameScriptStat.Text = string.Format("{0}/{1} ({2:F2}%)", gameCompleteCount, gameCount, (float)gameCompleteCount / gameCount * 100);
                lblSystemScriptStat.Text = string.Format("{0}/{1} ({2:F2}%)", systemCompleteCount, systemCount, (float)systemCompleteCount / systemCount * 100);

                UpdateProgressDisplayCurrentEntry();
            }
            catch (Exception ex)
            {
                Logger.BatchLog($"Exception: {ex.Message}");
                Logger.BatchLog($"Additional Data: \r\n{ex.Data}");
                Logger.BatchLog($"Stacktrace: \r\n{ex.StackTrace}");
            }

            return true;
        }

        private bool UpdateProgressDisplayCurrentEntry()
        {
            if (CurrentEntry == null)
            {
                pbCurEntryStat.Maximum = 0;
                pbCurEntryStat.Value = 0;
                lblCurEntryStat.Text = "0/0 (0%)";
            }
            else
            {
                int count = CurrentEntry.RecordCount;
                int completed = CurrentEntry.CompletedRecordCount;
                pbCurEntryStat.Maximum = count;
                pbCurEntryStat.Value = completed;
                lblCurEntryStat.Text = string.Format("{0}/{1} ({2:F2}%)", completed, count, (float)completed / count * 100);
            }

            return true;
        }

        private bool IsAllCapsOrEmpty(string str)
        {
            if (string.IsNullOrEmpty(str))
                return true;
            foreach (char c in str)
            {
                if (char.IsLower(c))
                    return false;
            }
            return true;
        }

        private void ApplyRecord()
        {
            if (CurrentEntry == null)
                return;

            Record record = CurrentEntry.GetRecord((string)dgvWorkspace.SelectedRows[0].Cells[0].Value);
            if (record == null)
            {
                Logger.Log("This record should not be null. Make sure you commit the record before switching to other entry.");
                return;
            }
            if (cbAppendRecordInfo.Checked)
            {
                if (!IsAllCapsOrEmpty(tbTextVie.Text))
                    if (!Regex.IsMatch(tbTextVie.Text, @" \[.+\-\>\d+\]"))
                        tbTextVie.Text += $" [{CurrentEntry.Name}->{record.Id}]";
            }
            record.TextVie = tbTextVie.Text;
            CurrentEntry.UpdateRecord(record);
            dgvWorkspace.SelectedRows[0].Cells[3].Value = tbTextVie.Text;
            MainGameEntryManager.UpdateEntry(CurrentEntry);
            UpdateProgressDisplay();
        }

        private void NextRecord()
        {
            int index = dgvWorkspace.SelectedRows[0].Index;
            if (index < dgvWorkspace.Rows.Count - 1)
                ++index;
            dgvWorkspace.ClearSelection();
            dgvWorkspace.Rows[index].Selected = true;
        }

        private void PreviousRecord()
        {
            int index = dgvWorkspace.SelectedRows[0].Index;
            if (index > 0)
                --index;
            dgvWorkspace.ClearSelection();
            dgvWorkspace.Rows[index].Selected = true;
        }

        private void SaveProject()
        {
            if (MainGameEntryManager == null)
            {
                return;
            }
            Logger.Log("========================================");
            Logger.Log("    Saving data...");
            NepError err = MainGameEntryManager.SaveData();
            Logger.Log(err.ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.Log("");
            SaveProject();
            Logger.Log("========================================");
            Logger.Log("    Closing form...");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.Log("===== Form Closed =====");
            Logger.Log("========================================");
        }

        private void treeDirStruct_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // TODO: bug here: program crash if tree is not properly populated;
            if (e.Node.Nodes.Count <= 0)
            {
                dgvWorkspace.Rows.Clear();
                EntryType type = EntryType.None;

                if (e.Node.FullPath.Contains("GAME00000"))
                    type = EntryType.GameScript;
                else if (e.Node.FullPath.Contains("SYSTEM00000"))
                    type = EntryType.SystemScript;
                else
                    Logger.Log("You shouldn't be here!");
                Entry entry = MainGameEntryManager.GetEntry(e.Node.Name, type);
                if (entry == null)
                {
                    Logger.Log("Selected node is not a valid entry.");
                }
                else
                {
                    //ApplyRecord();
                    foreach (string key in entry.Records.Keys)
                    {
                        Record r = entry.Records[key];
                        dgvWorkspace.Rows.Add(r.Id, r.TextEng, r.TextJap, r.TextVie);
                    }
                    CurrentEntry = entry;
                    btnSaveEntry.Enabled = true;
                    UpdateProgressDisplay();
                    return;
                }
            }

            dgvWorkspace.Rows.Clear();
            CurrentEntry = null;
            btnSaveEntry.Enabled = false;
            UpdateProgressDisplay();
        }

        private void treeDirStruct_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node == null)
                return;

            bool selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;
            bool hot = (e.State & TreeNodeStates.Hot) == TreeNodeStates.Hot;
            //Console.WriteLine(hot);

            // we need to do owner drawing only on a selected node
            // and when the treeview is unfocused, else let the OS do it for us
            if (selected && !e.Node.TreeView.Focused)
            {
                Font font = e.Node.NodeFont ?? e.Node.TreeView.Font;
                if (hot)
                    font = new Font(font, FontStyle.Underline);
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, SystemColors.HighlightText, TextFormatFlags.GlyphOverhangPadding);
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void dgvWorkspace_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvWorkspace.SelectedRows.Count > 0)
            {
                tbTextEng.Text = (string)dgvWorkspace.SelectedRows[0].Cells[1].Value;
                tbTextJap.Text = (string)dgvWorkspace.SelectedRows[0].Cells[2].Value;
                tbTextVie.Text = (string)dgvWorkspace.SelectedRows[0].Cells[3].Value;
                btnUpdate.Enabled = true;
                btnKeepOrg.Enabled = true;
                ApplyRecord();
                if (cbAppendRecordInfo.Checked)
                {

                }
            }
            else
            {
                tbTextEng.Text = "";
                tbTextJap.Text = "";
                tbTextVie.Text = "";
                btnUpdate.Enabled = false;
                btnKeepOrg.Enabled = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ApplyRecord();
            //NextRecord();
        }

        private void btnSaveEntry_Click(object sender, EventArgs e)
        {
            ApplyRecord();
        }

        private void btSaveProj_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        private void tbTextVie_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.Control && e.KeyCode == Keys.A)
            {
                tbTextVie.SelectAll();
                e.SuppressKeyPress = true;
                return;
            }
            if (dgvWorkspace.SelectedRows.Count > 0)
            {
                if (ModifierKeys == Keys.Control && e.KeyCode == Keys.Enter)
                {

                    ApplyRecord();
                    NextRecord();
                    e.SuppressKeyPress = true;
                }
                else if (ModifierKeys == Keys.Shift && e.KeyCode == Keys.Enter)
                {
                    ApplyRecord();
                    PreviousRecord();
                    e.SuppressKeyPress = true;
                }
                else if (ModifierKeys == Keys.Alt && e.KeyCode == Keys.Enter)
                {
                    ApplyRecord();
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void btnKeepOrg_Click(object sender, EventArgs e)
        {
            if (CurrentEntry == null)
            {
                Logger.Log("btnKeepOrg_Click() -> \"You shouldn't be here.\"");
                return;
            }

            if (!tbTextEng.Text.Equals(tbTextJap.Text))
            {
                Logger.Log("btnKeepOrg_Click() -> Original text doesn't match, use at your own risk.");
            }
            tbTextVie.Text = tbTextEng.Text;
            ApplyRecord();
            NextRecord();
        }

        private void btnSummaryReport_Click(object sender, EventArgs e)
        {
            using (Form f = new SummaryReportForm(MainGameEntryManager))
            {
                DialogResult result = f.ShowDialog();
                Logger.Log($"Summary Dialog returns '{result}'.");
            }
        }

        private void btnAutofill_Click(object sender, EventArgs e)
        {
            Point p = MainGameEntryManager.AutofillRecord();
            //Point p = new Point(15, 350);
            DialogResult result = MessageBox.Show(this,
                $"Autofilled {p.X} over {p.Y} records with duplicated data.\r\nPlease reload Entry for changes to take efect.",
                "Autofill", MessageBoxButtons.OK);
            Logger.Log($"Autofill MsgBox returns '{result}'.");
            UpdateProgressDisplay();
            //MainGameEntryManager.SaveData();
        }

        private void btnCoopyToClipboard_Click(object sender, EventArgs e)
        {
            if (!tbTextVie.Text.Equals(string.Empty))
            {
                string text = tbTextVie.Text.Replace("\r\n", "\\n\r\n");
                Clipboard.SetText(text);
            }
        }

        private void btnSelectRootDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbRootDirectory.Text = fbd.SelectedPath;
            }
        }

        private void tbRootDirectory_TextChanged(object sender, EventArgs e)
        {
            InitEntryManager();
        }
    }
}
