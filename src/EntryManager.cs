// ;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NepTrans
{
    public class NepEntryManager
    {
        private Dictionary<string, Entry> GameScriptEntries;
        private Dictionary<string, Entry> SystemScriptEntries;
        public TreeNode EntriesTree { get; private set; }

        public string Name = null;
        public string RootDirectory = null;
        public string NepDataRootDir = null;
        public string DataEngRootDir = @"\data_eng";
        public string DataJapRootDir = @"\data_jap";
        public string DataVieRootDir = @"\data_vie";
        public string GameEventScript = @"\GAME00000\event\script";
        public string SystemScript = @"\SYSTEM00000\database";

        private const string recordSeparator =
            //"\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015" +
            //"\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015" +
            //"\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015" +
            //"\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015\u2015";
            "----------------------------------------";

        #region Count getters
        public int GameScriptRecordCount
        {
            get
            {
                int count = 0;
                foreach (Entry entry in GameScriptEntries.Values)
                    count += entry.RecordCount;
                return count;
            }
        }
        public int GameScriptCompletedRecordCount
        {
            get
            {
                int count = 0;
                foreach (Entry entry in GameScriptEntries.Values)
                {
                    count += entry.CompletedRecordCount;
                }
                return count;
            }
        }
        public int SystemScriptRecordCount
        {
            get
            {
                int count = 0;
                foreach (Entry entry in SystemScriptEntries.Values)
                    count += entry.RecordCount;
                return count;
            }
        }
        public int SystemScriptCompletedRecordCount
        {
            get
            {
                int count = 0;
                foreach (Entry entry in SystemScriptEntries.Values)
                {
                    count += entry.CompletedRecordCount;
                }
                return count;
            }
        }
        #endregion

        #region Helper method
        public bool AddEntry(Entry _entry)
        {
            if (_entry.Type == EntryType.GameScript)
            {
                if (GameScriptEntries.ContainsKey(_entry.Name))
                    return false;

                GameScriptEntries[_entry.Name] = _entry;
                return true;
            }
            if (_entry.Type == EntryType.SystemScript)
            {
                if (SystemScriptEntries.ContainsKey(_entry.Name))
                    return false;

                SystemScriptEntries[_entry.Name] = _entry;
                return true;
            }
            return false; // control falls out of block for some reason;
        }

        public bool UpdateEntry(Entry _entry)
        {
            if (_entry.Type == EntryType.GameScript)
            {
                GameScriptEntries[_entry.Name] = _entry;
                return true;
            }
            if (_entry.Type == EntryType.SystemScript)
            {
                SystemScriptEntries[_entry.Name] = _entry;
                return true;
            }
            return false;
        }

        public Entry GetEntry(string _name, EntryType _type = EntryType.None)
        {
            if (_type == EntryType.GameScript)
            {
                if (GameScriptEntries.ContainsKey(_name))
                    return GameScriptEntries[_name];
            }
            if (_type == EntryType.SystemScript)
            {
                if (SystemScriptEntries.ContainsKey(_name))
                    return SystemScriptEntries[_name];
            }
            return null;
        }
        #endregion

        public NepEntryManager(string _name, string _rootPath, string _dataPath)
        {
            GameScriptEntries = new Dictionary<string, Entry>();
            SystemScriptEntries = new Dictionary<string, Entry>();
            //EntriesTree = new TreeNode("_zero_");

            Name = _name;
            RootDirectory = _rootPath;
            NepDataRootDir = _dataPath;

            {
                NepError error = ValidateDirectoryStructure();
                if (error.ErrorCode != NepErrCode.NoError)
                {
                    Console.WriteLine(error);
                    throw new Exception($"Entry Manager '{Name}' instantiation failure.");
                }
            }

            {
                NepError error = PopulateData();
                if (error.ErrorCode != NepErrCode.NoError)
                {
                    Console.WriteLine(error);
                    throw new Exception($"Entry Manager '{Name}' data population failure. Please reopen the program.");
                }
            }
        }

        ~NepEntryManager() { }

        /// <summary>Validate the directory structure integrity.</summary>
        public NepError ValidateDirectoryStructure()
        {
            if (RootDirectory == null)
                return new NepError(NepErrCode.DirectoryNotSet);
            if (NepDataRootDir == null)
                return new NepError(NepErrCode.DirectoryNotSet);

            // ===== root =====
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}"))
                return new NepError(NepErrCode.DirectoryNotFound,
                    @"NepData root directory not found (expected \nep_xxx) where xxx is project name.");
            EntriesTree = new TreeNode(Name);

            // ===== data =====
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataEngRootDir}"))
                return new NepError(NepErrCode.DirectoryNotFound, @"English Data directory not found (expected \data_eng).");
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataJapRootDir}"))
                return new NepError(NepErrCode.DirectoryNotFound, @"Japanese Data directory not found (expected \data_jap).");
            EntriesTree.Nodes.Add("data", "data");

            // ===== GAME00000 =====
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataEngRootDir}{GameEventScript}"))
                return new NepError(NepErrCode.DirectoryNotFound, @"Main Game script English directory not found (expected \GAME00000\event\script).");
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataJapRootDir}{GameEventScript}"))
                return new NepError(NepErrCode.DirectoryNotFound, @"Main Game script Japaanese directory not found (expected \GAME00000\event\script).");
            EntriesTree.Nodes.Find("data", true)[0].Nodes.Add("game00", "GAME00000").Nodes.Add("evt", "event").Nodes.Add("script", "script");

            // ===== scripts =====
            // ValidateData() only verify directory structures, not scripts validation, to improve performance;

            // ===== SYSTEM00000 =====
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataEngRootDir}{SystemScript}"))
                return new NepError(NepErrCode.DirectoryNotFound, @"System script English directory not found (expected \SYSTEM00000\database).");
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataJapRootDir}{SystemScript}"))
                return new NepError(NepErrCode.DirectoryNotFound, @"System script Japaanese directory not found (expected \SYSTEM00000\database).");
            EntriesTree.Nodes.Find("data", true)[0].Nodes.Add("sys00", "SYSTEM00000").Nodes.Add("db", "database");

            // ===== data_vie =====
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataVieRootDir}{GameEventScript}"))
            {
                Console.WriteLine(@"Main Game script Translation directory not found and will be created.");
                Directory.CreateDirectory($"{RootDirectory}{NepDataRootDir}{DataVieRootDir}{GameEventScript}");
            }
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataVieRootDir}{SystemScript}"))
            {
                Console.WriteLine(@"System script Translation directory not found and will be created.");
                Directory.CreateDirectory($"{RootDirectory}{NepDataRootDir}{DataVieRootDir}{SystemScript}");
            }

            // ===== return =====
            return new NepError(NepErrCode.NoError, "Validation successfull.");
        }

        /// <summary> Autofill any record with duplicate data. </summary>
        /// <returns> A pair of the total number of autofilled records and total records with duplicate data. </returns>
        public Point AutofillRecord()
        {
            int count = 0;
            int total = 0;

            foreach (Entry entry in GameScriptEntries.Values)
            {
                foreach (Record record in entry.Records.Values)
                {
                    if (record.TextEng.Equals(""))
                        continue;
                    if (record.TextEng.Equals(record.TextJap))
                    {
                        ++total;
                        if (!record.TextVie.Equals(record.TextEng))
                        {
                            record.TextVie = record.TextEng;
                            ++count;
                        }
                    }
                }
            }
            foreach (Entry entry in SystemScriptEntries.Values)
            {
                foreach (Record record in entry.Records.Values)
                {
                    if (record.TextEng.Equals(""))
                        continue;
                    if (record.TextEng.Equals(record.TextJap))
                    {
                        ++total;
                        if (!record.TextVie.Equals(record.TextEng))
                        {
                            record.TextVie = record.TextEng;
                            ++count;
                        }
                    }
                }
            }
            SaveData();

            return new Point(count, total);
        }

        /// <summary>
        /// Populate the data and store them in working space.
        /// NOTE that this will not check for directory structure damage,
        /// so be sure to call ValidateDirectoryStructure() beforehand.
        /// </summary>
        public NepError PopulateData()
        {
            NepError error = ValidateDirectoryStructure();
            if (error.ErrorCode != NepErrCode.NoError)
            {
                Console.WriteLine("Error occurred when validate directory structure before populate data.");
                return error;
            }

            // ===== GAME00000 =====
            Console.WriteLine();
            if (!ParseGameScriptEntries())
                Console.WriteLine("No script found in Game Script directory. Please verify and try again.");

            // ===== SYSTEM00000 =====
            Console.WriteLine();
            if (!ParseSystemScriptEntries())
                Console.WriteLine("No script found in System Script directory. Please verify and try again.");

            Console.WriteLine("===== Data Populating Successfully =====\r\n========================================\r\n");

            return NepError.NoError; // TODO: remove this;
        }

        public NepError SaveData()
        {
            // what will be saved is just translaated text;
            // files is already created so there is no need to check file here;
            if (!SaveGameScriptEntry())
            {
                Console.WriteLine("Something goes wrong when saving Game Script entries.");
            }
            if (!SaveSystemScriptEntry())
            {
                Console.WriteLine("Something goes wrong when saving System Script entries.");
            }
            return NepError.NoError;
        }

        // method break;
        private bool ParseGameScriptEntries()
        {
            DirectoryInfo[] dE = new DirectoryInfo($"{RootDirectory}{NepDataRootDir}{DataEngRootDir}{GameEventScript}").GetDirectories();
            DirectoryInfo[] dJ = new DirectoryInfo($"{RootDirectory}{NepDataRootDir}{DataJapRootDir}{GameEventScript}").GetDirectories();

            List<string> fE = new List<string>();
            foreach (DirectoryInfo info in dE)
                fE.Add(info.Name);

            List<string> fJ = new List<string>();
            foreach (DirectoryInfo info in dJ)
                fJ.Add(info.Name);

            List<string> empties = new List<string>();
            List<string> scripts = new List<string>();
            List<string> misEng = new List<string>();
            List<string> misJap = new List<string>();

            StringBuilder dbgEmpties = new StringBuilder();
            StringBuilder dbgScripts = new StringBuilder();
            StringBuilder dbgMisEng = new StringBuilder();
            StringBuilder dbgMisJap = new StringBuilder();

            foreach (string name in fE)
            {
                if (File.Exists($"{RootDirectory}{NepDataRootDir}{DataEngRootDir}{GameEventScript}\\{name}\\main.cl3.txt"))
                {
                    if (fJ.Remove(name))
                    {
                        scripts.Add(name);
                        dbgScripts.Append($"{name}, ");
                    }
                    else
                    {
                        misEng.Add(name);
                        dbgMisEng.Append($"{name}, ");
                    }
                }
                else
                {
                    if (!File.Exists($"{RootDirectory}{NepDataRootDir}{DataJapRootDir}{GameEventScript}\\{name}\\main.cl3.txt"))
                    {
                        empties.Add(name);
                        dbgEmpties.Append($"{name}, ");
                    }
                    else
                    {
                        misJap.Add(name);
                        dbgMisJap.Append($"{name}, ");
                    }
                    fJ.Remove(name);
                }
            }

            if (scripts.Count <= 0)
                return false;

            foreach (string name in scripts)
            {
                TreeNode node = EntriesTree.Nodes.Find("script", true)[0].Nodes.Add(name, name);
                LoadGameScriptEntry($"{NepDataRootDir}\\data{GameEventScript}", name);
            }

            // TODO: populate mismatch entries as well...
            Console.WriteLine($"Parsed {scripts.Count + empties.Count + misEng.Count + misJap.Count} Game Script entries:");
            Console.WriteLine($"  {scripts.Count} valid entries - ");
            Console.WriteLine($"  {empties.Count} empty entries - {dbgEmpties.ToString()}");
            Console.WriteLine($"  {misEng.Count} mismatch \"no Jap\" entries - {dbgMisEng.ToString()}");
            Console.WriteLine($"  {misJap.Count} mismatch \"no Eng\" entries - {dbgMisJap.ToString()}");

            return true;
        }

        // method break;
        private bool ParseSystemScriptEntries()
        {
            return ParseGEntries("gbin") | ParseGEntries("gstr");


            // TODO: LoadSystemScriptEntry();
        }

        private bool ParseGEntries(string _extension)
        {
            FileInfo[] fE = new DirectoryInfo($"{RootDirectory}{NepDataRootDir}{DataEngRootDir}{SystemScript}").GetFiles($"*.{_extension}.txt");
            FileInfo[] fJ = new DirectoryInfo($"{RootDirectory}{NepDataRootDir}{DataJapRootDir}{SystemScript}").GetFiles($"*.{_extension}.txt");

            if (fE.Length != fJ.Length)
            {
                Console.WriteLine($"{_extension} entries number mismatch: {fE.Length} eng : {fJ.Length} jap.");
                // TODO: handle mismatch;
            }

            List<string> lE = new List<string>();
            foreach (FileInfo info in fE)
                lE.Add(info.Name);
            List<string> lJ = new List<string>();
            foreach (FileInfo info in fJ)
                lJ.Add(info.Name);

            List<string> scripts = new List<string>();
            List<string> misEng = new List<string>();
            List<string> misJap = new List<string>();

            StringBuilder dbgEmpties = new StringBuilder();
            StringBuilder dbgScripts = new StringBuilder();
            StringBuilder dbgMisEng = new StringBuilder();
            StringBuilder dbgMisJap = new StringBuilder();

            foreach (string name in lE)
            {
                if (lJ.Remove(name))
                {
                    scripts.Add(name);
                    dbgScripts.Append($"{name}, ");
                }
                else
                {
                    misEng.Add(name);
                    dbgMisEng.Append($"{name}, ");
                }
            }
            foreach (string name in lJ)
            {
                misJap.Add(name);
                dbgMisJap.Append($"{name}, ");
            }

            if (scripts.Count <= 0)
                return false;

            foreach (string name in scripts)
            {
                EntriesTree.Nodes.Find("db", true)[0].Nodes.Add(name, name);
                LoadSystemScriptEntry($"{NepDataRootDir}\\data{SystemScript}", name);
            }

            // TODO: populate mismatch entries as well...
            Console.WriteLine($"Parsed {scripts.Count + misEng.Count + misJap.Count} System Script ({_extension}) entries:");
            Console.WriteLine($"  {scripts.Count} valid entries - ");
            Console.WriteLine($"  {misEng.Count} mismatch \"no Jap\" entries - {dbgMisEng.ToString()}");
            Console.WriteLine($"  {misJap.Count} mismatch \"no Eng\" entries - {dbgMisJap.ToString()}");

            return true;
        }

        private bool LoadGameScriptEntry(string _path, string _name)
        {
            Entry entry = new Entry(_path, _name, EntryType.GameScript);
            {
                string se = $"{RootDirectory}\\{_path}\\{_name}\\main.cl3.txt".Replace("data", "data_eng");
                string[] engs = File.ReadAllLines(se, Encoding.GetEncoding("shift_jis"));
                if (engs.Length > 1) // use 1 to prevent EOF file;
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
                                record.TextEng = record.TextEng.TrimEnd(new char[] { '\r', '\n' });
                                entry.AddRecord(record);
                            }
                            record = new Record();
                            id = line.Trim(new char[] { ' ', '\u2015' });
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
                string sj = $"{RootDirectory}\\{_path}\\{_name}\\main.cl3.txt".Replace("data", "data_jap");
                string[] japs = File.ReadAllLines(sj, Encoding.GetEncoding("shift_jis"));
                if (japs.Length > 1)
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
                                Record r = entry.GetRecord(record.Id);
                                if (r == null)
                                {
                                    entry.AddRecord(record);
                                }
                                else
                                {
                                    r.TextJap = record.TextJap.TrimEnd(new char[] { '\r', '\n' });
                                    entry.UpdateRecord(r);
                                }
                            }
                            record = new Record();
                            id = line.Trim(new char[] { ' ', '\u2015' });
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
                string s = $"{RootDirectory}\\{_path}\\{_name}\\main.cl3.txt".Replace("data", "data_vie");
                if (File.Exists(s))
                {
                    string[] vies = File.ReadAllLines(s, Encoding.UTF8);
                    if (vies.Length > 1)
                    {
                        string id = "";
                        int index = 0;
                        Record record = null;
                        do
                        {
                            string line = vies[index];

                            //if (line.startswith("\u2015\u2015\u2015\u2015\u2015")) // "―――――";
                            if (line.StartsWith("-----")) // "-----";
                            {
                                if (record != null)
                                {
                                    Record r = entry.GetRecord(record.Id);
                                    if (r == null)
                                    {
                                        entry.AddRecord(record);
                                    }
                                    else
                                    {
                                        r.TextVie = record.TextVie.TrimEnd(new char[] { '\r', '\n' });
                                        entry.UpdateRecord(r);
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
                    Console.WriteLine($"file {s} doesn't exist and will be created.");
                    FileStream fs = File.Create(s);
                    fs.Close();
                }
            }

            GameScriptEntries.Add(entry.Name, entry);

            return true;
        }

        private bool LoadSystemScriptEntry(string _path, string _name)
        {
            Entry entry = new Entry(_path, _name, EntryType.SystemScript);
            {
                string se = $"{RootDirectory}{_path}\\{_name}".Replace("data\\", "data_eng\\");
                string[] engs = File.ReadAllLines(se, Encoding.GetEncoding("shift_jis"));
                if (engs.Length > 1) // use 1 to prevent EOF file;
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
                                record.TextEng = record.TextEng.TrimEnd(new char[] { '\r', '\n' });
                                entry.AddRecord(record);
                            }
                            record = new Record();
                            id = line.Trim(new char[] { ' ', '\u2015' });
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
                string je = $"{RootDirectory}{_path}\\{_name}".Replace("data\\", "data_jap\\");
                string[] japs = File.ReadAllLines(je, Encoding.GetEncoding("shift_jis"));
                if (japs.Length > 1)
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
                                Record r = entry.GetRecord(record.Id);
                                if (r == null)
                                {
                                    entry.AddRecord(record);
                                }
                                else
                                {
                                    r.TextJap = record.TextJap.TrimEnd(new char[] { '\r', '\n' });
                                    entry.UpdateRecord(r);
                                }
                            }
                            record = new Record();
                            id = line.Trim(new char[] { ' ', '\u2015' });
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
                string ve = $"{RootDirectory}{_path}\\{_name}".Replace("data\\", "data_vie\\");
                if (File.Exists(ve))
                {
                    string[] vies = File.ReadAllLines(ve, Encoding.UTF8);
                    if (vies.Length > 1)
                    {
                        string id = "";
                        int index = 0;
                        Record record = null;
                        do
                        {
                            string line = vies[index];
                            //if (line.startswith("\u2015\u2015\u2015\u2015\u2015")) // "―――――";
                            if (line.StartsWith("-----")) // "-----";
                            {
                                if (record != null)
                                {
                                    Record r = entry.GetRecord(record.Id);
                                    if (r == null)
                                    {
                                        entry.AddRecord(record);
                                    }
                                    else
                                    {
                                        r.TextVie = record.TextVie.TrimEnd(new char[] { '\r', '\n' });
                                        entry.UpdateRecord(r);
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
                    Console.WriteLine($"file {ve} doesn't exist and will be created.");
                    FileStream fs = File.Create(ve);
                    fs.Close();
                }
            }
            SystemScriptEntries.Add(entry.Name, entry);

            return false;
        }

        private bool SaveGameScriptEntry()
        {
            int count = 0;
            foreach (string name in GameScriptEntries.Keys)
            {
                Entry entry = GameScriptEntries[name];
                string sv = $"{RootDirectory}{NepDataRootDir}{DataVieRootDir}{GameEventScript}\\{name}\\main.cl3.txt";
                StringBuilder sb = new StringBuilder();
                foreach (string key in entry.Records.Keys)
                {
                    Record r = entry.Records[key];
                    sb.AppendLine($"{recordSeparator} {r.Id}\r\n{r.TextVie}");
                    ++count;
                }
                sb.Append($"{recordSeparator} EOF");

                File.WriteAllText(sv, sb.ToString(), Encoding.UTF8);
            }
            Console.WriteLine($"{GameScriptEntries.Count} Game Script entries written (total {count} records).");
            return true;
        }

        private bool SaveSystemScriptEntry()
        {
            int count = 0;
            foreach (string name in SystemScriptEntries.Keys)
            {
                Entry entry = SystemScriptEntries[name];
                string sv = $"{RootDirectory}{NepDataRootDir}{DataVieRootDir}{SystemScript}\\{name}";
                StringBuilder sb = new StringBuilder();
                foreach (string key in entry.Records.Keys)
                {
                    Record r = entry.Records[key];
                    sb.AppendLine($"{recordSeparator} {r.Id}\r\n{r.TextVie}");
                    ++count;
                }
                sb.Append($"{recordSeparator} EOF");

                File.WriteAllText(sv, sb.ToString(), Encoding.UTF8);
            }
            Console.WriteLine($"{SystemScriptEntries.Count} System Script entries written (total {count} records).");
            return true;
        }
    }
}
