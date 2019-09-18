
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
        const string dataEngRootDir = @"\data_eng";
        const string dataJapRootDir = @"\data_jap";
        const string dataVieRootDir = @"\data_vie";
        const string game00000 = @"\GAME00000";
        const string eventScripts = @"\event\script";

        //NepEntryManager MainGameEntryManager;

        const string recordSeparator =
            //"\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015" +
            //"\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015" +
            //"\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015" +
            //"\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015";
            "----------------------------------------";

        Dictionary<string, Record> Entry;
        string CurrentEntryPath;

        public Form1()
        {
            InitializeComponent();

            //MainGameEntryManager = new NepEntryManager(defaultRootDir);





            btnUpdate.Enabled = false;
            btnSaveEntry.Enabled = false;

            NepErrCode err = VerifyDirectoryStruct();

            if (err != NepErrCode.NoError)
            {
                Console.WriteLine(NepError.GetErrorString(err));
            }

            treeDirStruct.ExpandAll();

            Record r = new Record();


            string k = tbTextVie.Text;




            //treeDirStruct.Nodes.Add("root", "nep_rb1_data");
            //treeDirStruct.Nodes["root"].Nodes.Add("game00000", "GAME00000");









        }

        









        private NepErrCode VerifyDirectoryStruct()
        {
            string rootDir = defaultRootDir;
            tbRootDirectory.Text = rootDir;

            string currentDir = rootDir;

            // ===== root =====
            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}"))
            {
                return NepErrCode.RootDirNotFound;
            }
            treeDirStruct.Nodes.Add("root", "nep_rb1");
            currentDir += rootDir;

            // ===== data =====
            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataEngRootDir}"))
            {
                return NepErrCode.EngDirNotFound;
            }
            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataJapRootDir}"))
            {
                return NepErrCode.JapDirNotFound;
            }
            treeDirStruct.Nodes["root"].Nodes.Add("data", "data");
            currentDir += "data";

            // ===== GAME00000 =====
            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataEngRootDir}{game00000}"))
            {
                return NepErrCode.GameScriptDirNotFound;
            }
            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataJapRootDir}{game00000}"))
            {
                return NepErrCode.GameScriptDirNotFound;
            }
            treeDirStruct.Nodes.Find("data", true)[0].Nodes.Add("game00", "GAME00000");
            currentDir += "GAME00000";

            // ===== event\script =====
            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataEngRootDir}{game00000}{eventScripts}"))
            {
                return NepErrCode.GameScriptDirNotFound;
            }
            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataJapRootDir}{game00000}{eventScripts}"))
            {
                return NepErrCode.GameScriptDirNotFound;
            }
            treeDirStruct.Nodes.Find("game00", true)[0].Nodes.Add("event", "event");
            treeDirStruct.Nodes.Find("event", true)[0].Nodes.Add("script", "script");

            // ===== scripts =====
            DirectoryInfo[] dirEng = new DirectoryInfo($"{rootDir}{nepRb1RootDir}{dataEngRootDir}{game00000}{eventScripts}").GetDirectories();
            DirectoryInfo[] dirJap = new DirectoryInfo($"{rootDir}{nepRb1RootDir}{dataJapRootDir}{game00000}{eventScripts}").GetDirectories();

            List<string> folderEng = new List<string>();
            foreach (DirectoryInfo info in dirEng)
            {
                folderEng.Add(info.Name);
            }
            List<string> folderJap = new List<string>();
            foreach (DirectoryInfo info in dirJap)
            {
                folderJap.Add(info.Name);
            }

            List<string> empty = new List<string>();
            List<string> gamescripts = new List<string>();
            List<string> mismatchEng = new List<string>();
            List<string> mismatchJap = new List<string>();
            foreach (string name in folderEng)
            {
                if (File.Exists($"{rootDir}{nepRb1RootDir}{dataEngRootDir}{game00000}{eventScripts}\\{name}\\main.cl3.txt"))
                {
                    bool b = folderJap.Remove(name);
                    if (b)
                    {
                        gamescripts.Add(name);
                        treeDirStruct.Nodes.Find("script", true)[0].Nodes.Add(name, name);
                    }
                    else
                    {
                        mismatchEng.Add(name);
                    }
                }
                else
                {
                    empty.Add(name);
                    folderJap.Remove(name);
                    continue;
                }
            }
            mismatchJap.AddRange(folderJap);

            // TODO: allow mismatch entries edit;
            if (mismatchEng.Count > 0)
            {
                string mm = "";
                foreach (string s in mismatchEng)
                    mm += s + "\r\n";
                Console.WriteLine($"Mismatch found in data_eng:\r\n{mm}");
            }
            if (mismatchJap.Count > 0)
            {
                string mm = "";
                foreach (string s in mismatchJap)
                    mm += s + "\r\n";
                Console.WriteLine($"Mismatch found in data_jap:\r\n{mm}");
            }
            if (empty.Count > 0)
            {
                string mm = "";
                foreach (string s in empty)
                    mm += s + "\r\n";
                Console.WriteLine($"{empty.Count} empty entries found: \r\n{mm}");
            }


            // ===== SYSTEM00000 =====
            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataEngRootDir}\\SYSTEM00000"))
            {
                return NepErrCode.GameScriptDirNotFound;
            }
            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataJapRootDir}\\SYSTEM00000"))
            {
                return NepErrCode.GameScriptDirNotFound;
            }
            treeDirStruct.Nodes["root"].Nodes["data"].Nodes.Add("sys00", "SYSTEM00000");





            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataVieRootDir}"))
            {
                Console.WriteLine("Translation Root directory not found and will be created.");
                Directory.CreateDirectory($"{rootDir}{nepRb1RootDir}{dataVieRootDir}");
            }
            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataVieRootDir}{game00000}"))
            {
                Console.WriteLine("Translation Game Script directory not found and will be created.");
                Directory.CreateDirectory($"{rootDir}{nepRb1RootDir}{dataVieRootDir}{game00000}");
            }
            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataVieRootDir}{game00000}{eventScripts}"))
            {
                Console.WriteLine("Translation Game Script script directory not found and will be created.");
                Directory.CreateDirectory($"{rootDir}{nepRb1RootDir}{dataVieRootDir}{game00000}{eventScripts}");
            }
            DirectoryInfo gamescriptInfo = new DirectoryInfo($"{rootDir}{nepRb1RootDir}{dataVieRootDir}{game00000}{eventScripts}");
            int count = 0;
            foreach (string s in gamescripts)
            {
                if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataVieRootDir}{game00000}{eventScripts}\\{s}"))
                {
                    ++count;
                    Directory.CreateDirectory($"{rootDir}{nepRb1RootDir}{dataVieRootDir}{game00000}{eventScripts}\\{s}");
                }
            }
            Console.WriteLine($"Written {count} entries in gamescript.");

            if (!Directory.Exists($"{rootDir}{nepRb1RootDir}{dataVieRootDir}SYSTEM00000\\"))
            {
                Console.WriteLine("Translation System Script directory not found and will be created.");
                Directory.CreateDirectory($"{rootDir}{nepRb1RootDir}{dataVieRootDir}SYSTEM00000\\");
            }




            return NepErrCode.NoError;
        }

        private NepErrCode PopulateData()
        {


            return NepErrCode.NoError;
        }

        private void CrawlData()
        {

        }

        private void LoadEntry(string _path)
        {
            // entry to be load is ALWAYS a match entry;

            Entry = new Dictionary<string, Record>();
            {
                string s = $"{defaultRootDir}\\{_path}\\main.cl3.txt".Replace("data", "data_eng");
                string[] engs = File.ReadAllLines(s, Encoding.GetEncoding("shift_jis"));
                if (engs.Length > 0)
                {
                    string id = "";
                    int index = 0;
                    Record record = null;
                    do
                    {
                        string line = engs[index];
                        if (line.StartsWith("\u2015\u2015\u2015\u2015\u2015")) // "―――――";
                        {
                            if (record != null)
                            {
                                Entry[record.Id] = record;
                            }
                            record = new Record();
                            id = line.Replace('\u2015', ' ').Trim();
                            record.Id = id;
                        }
                        else
                        {
                            record.TextEng += line + "\r\n";
                        }
                        ++index;
                    } while (!id.Equals("EOF"));
                }
            }

            {
                string s = $"{defaultRootDir}\\{_path}\\main.cl3.txt".Replace("data", "data_jap");
                string[] japs = File.ReadAllLines(s, Encoding.GetEncoding("shift_jis"));
                if (japs.Length > 0)
                {
                    string id = "";
                    int index = 0;
                    Record record = null;
                    do
                    {
                        string line = japs[index];

                        if (line.StartsWith("\u2015\u2015\u2015\u2015\u2015")) // "―――――";
                        {
                            if (record != null)
                            {
                                if (Entry.ContainsKey(record.Id))
                                {
                                    Entry[record.Id].TextJap = record.TextJap;
                                }
                                else
                                {
                                    Entry[record.Id] = record;
                                }
                            }
                            record = new Record();
                            id = line.Replace('\u2015', ' ').Trim();
                            record.Id = id;
                        }
                        else
                        {
                            record.TextJap += line + "\r\n";
                        }
                        ++index;
                    } while (!id.Equals("EOF"));
                }
            }

            {
                string s = $"{defaultRootDir}\\{_path}\\main.cl3.txt".Replace("data", "data_vie");
                if (File.Exists(s))
                {
                    string[] vies = File.ReadAllLines(s, Encoding.UTF8);
                    if (vies.Length > 0)
                    {
                        string id = "";
                        int index = 0;
                        Record record = null;
                        do
                        {
                            string line = vies[index];

                            //if (line.StartsWith("\u2015\u2015\u2015\u2015\u2015")) // "―――――";
                            if (line.StartsWith("-----")) // "-----";
                            {
                                if (record != null)
                                {
                                    if (Entry.ContainsKey(record.Id))
                                    {
                                        record.TextVie = record.TextVie.TrimEnd(new char[] { '\r', '\n' });
                                        Entry[record.Id].TextVie = record.TextVie;
                                    }
                                    else
                                    {
                                        Entry[record.Id] = record;
                                    }
                                }
                                record = new Record();
                                id = line.Replace('-', ' ').Trim();
                                record.Id = id;
                            }
                            else
                            {
                                record.TextVie += line + "\r\n";
                            }
                            ++index;
                        } while (!id.Equals("EOF"));
                    }
                }
                else
                {
                    Console.WriteLine($"File {s} doesn't exist and will be created.");
                    FileStream fs = File.Create(s);
                    fs.Close();
                }
            }



            foreach (string key in Entry.Keys)
            {
                Record r = Entry[key];
                dgvWorkspace.Rows.Add(r.Id, r.TextEng, r.TextJap, r.TextVie);
            }
        }

        private void SaveCurrentEntry(string _path)
        {
            // of course what will be saved is just translated text;

            string s = $"{defaultRootDir}\\{_path}\\main.cl3.txt".Replace("data", "data_vie");

            string[] values = new string[Entry.Values.Count];

            StringBuilder sb = new StringBuilder();
            foreach (string key in Entry.Keys)
            {
                Record r = Entry[key];
                sb.AppendLine($"{recordSeparator} {r.Id}");
                sb.AppendLine(r.TextVie);
            }
            sb.Append($"{recordSeparator} EOF");

            string str = sb.ToString();
            File.WriteAllText(s, str, Encoding.UTF8);
        }

        private string GetNodePath(TreeNode _node)
        {
            string res = "";
            TreeNode node = _node;
            do
            {
                res = node.Text + "\\" + res;
                node = node.Parent;
            } while (node != null);

            return res;
        }

        private void ApplyRecord()
        {
            Entry[(string)dgvWorkspace.SelectedRows[0].Cells[0].Value].TextVie = tbTextVie.Text;
            dgvWorkspace.SelectedRows[0].Cells[3].Value = tbTextVie.Text;
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

        private void treeDirStruct_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count <= 0)
            {
                if (CurrentEntryPath != null && !CurrentEntryPath.Equals(""))
                {
                    SaveCurrentEntry(CurrentEntryPath);
                }
                dgvWorkspace.Rows.Clear();
                CurrentEntryPath = e.Node.FullPath;
                LoadEntry(CurrentEntryPath);
                btnSaveEntry.Enabled = true;
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
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ApplyRecord();
            NextRecord();
        }

        private void btnSaveEntry_Click(object sender, EventArgs e)
        {
            SaveCurrentEntry(CurrentEntryPath);
        }

        bool keydowncancel = false;
        private void tbTextVie_KeyDown(object sender, KeyEventArgs e)
        {

            if (dgvWorkspace.SelectedRows.Count > 0)
            {
                if (ModifierKeys == Keys.Control && e.KeyCode == Keys.Enter)
                {

                    ApplyRecord();
                    NextRecord();
                    keydowncancel = true;
                    //e.SuppressKeyPress
                }
                else if (ModifierKeys == Keys.Shift && e.KeyCode == Keys.Enter)
                {

                    ApplyRecord();
                    PreviousRecord();
                    keydowncancel = true;
                }
                else
                {
                    keydowncancel = false;
                }
            }
        }

        private void tbTextVie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (keydowncancel)
                e.Handled = true;
        }
    }
}
