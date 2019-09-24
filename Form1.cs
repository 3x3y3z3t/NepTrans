
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
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

            MainGameEntryManager = new NepEntryManager("nep_rb1", defaultRootDir, nepRb1RootDir);

            treeDirStruct.Nodes.Add(MainGameEntryManager.EntriesTree);


            SetupProgressDisplay();
            UpdateProgressDisplay();


            btnUpdate.Enabled = false;
            btnSaveEntry.Enabled = false;
            btnKeepOrg.Enabled = false;


            treeDirStruct.ExpandAll();

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

            pbOverall.Value = overallCompleteCount;
            pbGameScript.Value = gameCompleteCount;
            pbSystemScript.Value = systemCompleteCount;

            lblOverallStat.Text = string.Format("{0}/{1} ({2:F2}%)", overallCompleteCount, overallCount, (float)overallCompleteCount / overallCount * 100);
            lblGameScriptStat.Text = string.Format("{0}/{1} ({2:F2}%)", gameCompleteCount, gameCount, (float)gameCompleteCount / gameCount * 100);
            lblSystemScriptStat.Text = string.Format("{0}/{1} ({2:F2}%)", systemCompleteCount, systemCount, (float)systemCompleteCount / systemCount * 100);

            UpdateProgressDisplayCurrentEntry();

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

        private void ApplyRecord()
        {
            if (CurrentEntry == null)
                return;

            Record record = CurrentEntry.GetRecord((string)dgvWorkspace.SelectedRows[0].Cells[0].Value);
            if (record == null)
            {
                Console.WriteLine("This record should not be null. Make sure you commit the record before switching to other entry.");
                return;
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
            Console.WriteLine("======================");
            Console.WriteLine("    Saving data...");
            NepError err = MainGameEntryManager.SaveData();
            Console.WriteLine(err);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveProject();
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
                    Console.WriteLine("You shouldn't be here!");
                Entry entry = MainGameEntryManager.GetEntry(e.Node.Name, type);
                if (entry == null)
                {
                    Console.WriteLine("Selected node is not a valid entry.");
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
            }
        }

        private void btnKeepOrg_Click(object sender, EventArgs e)
        {
            if (CurrentEntry == null)
            {
                Console.WriteLine("btnKeepOrg_Click() -> \"You shouldn't be here.\"");
                return;
            }

            if (!tbTextEng.Text.Equals(tbTextJap.Text))
            {
                Console.WriteLine("btnKeepOrg_Click() -> Original text doesn't match, use at your own risk.");
            }
            tbTextVie.Text = tbTextEng.Text;
            ApplyRecord();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Form f = new SummaryReportForm(MainGameEntryManager))
            {
                DialogResult result = f.ShowDialog();
                Console.WriteLine($"Summary Dialog returns '{result}'.");
            }
        }
    }
}
