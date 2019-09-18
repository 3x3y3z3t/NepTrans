// ;
using System;
using System.Collections.Generic;
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
        public string SystemScript = @"\SYSTEM00000";

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

        public Entry GetEntry(string _name, EntryType _type = EntryType.None)
        {
            if (_type == EntryType.GameScript)
            {
                if (GameScriptEntries.ContainsKey(_name))
                    return GameScriptEntries[_name];
            }
            else if (_type == EntryType.SystemScript)
            {
                if (SystemScriptEntries.ContainsKey(_name))
                    return SystemScriptEntries[_name];
            }
            return null;
        }
        #endregion

        public NepEntryManager(string _name, string _rootPath)
        {
            GameScriptEntries = new Dictionary<string, Entry>();
            SystemScriptEntries = new Dictionary<string, Entry>();
            EntriesTree = new TreeNode("_zero_");

            Name = _name;
            RootDirectory = _rootPath;

            {
                NepError error = ValidateDirectoryStructure();
                if (error.ErrorCode != NepErrCode.NoError)
                {
                    Console.WriteLine(error);
                    throw new Exception($"Entry Manager '{Name}' instantiation failure.");
                }
            }

            {
                // CURRENT FLOW: inside here;
                NepError error = PopulateData();
                if (error.ErrorCode != NepErrCode.NoError)
                {
                    Console.WriteLine(error);

                }
            }
        }

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
            EntriesTree.Nodes.Add("root", "nep_rb1");

            // ===== data =====
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataEngRootDir}"))
                return new NepError(NepErrCode.DirectoryNotFound, @"English Data directory not found (expected \\data_eng).");
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataJapRootDir}"))
                return new NepError(NepErrCode.DirectoryNotFound, @"Japanese Data directory not found (expected \\data_jap).");
            EntriesTree.Nodes.Find("root", true)[0].Nodes.Add("data", "data");

            // ===== GAME00000 =====
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataEngRootDir}{GameEventScript}"))
                return new NepError(NepErrCode.DirectoryNotFound, @"Main Game script English directory not found (expected \\GAME00000\event\script).");
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataJapRootDir}{GameEventScript}"))
                return new NepError(NepErrCode.DirectoryNotFound, @"Main Game script Japaanese directory not found (expected \\GAME00000\event\script).");
            EntriesTree.Nodes.Find("data", true)[0].Nodes.Add("game00", "GAME00000").Nodes.Add("evt", "event").Nodes.Add("script", "script");

            // ===== scripts =====
            // ValidateData() only verify directory structures, not scripts validation, to improve performance;

            // ===== SYSTEM00000 =====
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataEngRootDir}{SystemScript}"))
                return new NepError(NepErrCode.DirectoryNotFound, @"System script English directory not found (expected \\SYSTEM00000).");
            if (!Directory.Exists($"{RootDirectory}{NepDataRootDir}{DataJapRootDir}{SystemScript}"))
                return new NepError(NepErrCode.DirectoryNotFound, @"System script Japaanese directory not found (expected \\SYSTEM00000).");
            EntriesTree.Nodes.Find("data", true)[0].Nodes.Add("sys00", "SYSTEM00000");

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

            return NepError.NoError; // TODO: remove this;

            // ===== GAME00000 =====



            // ===== SYSTEM00000 =====

        }

        /*
        private NepErrCode VerifyDirectoryStruct()
        {
            string rootDir = RootDirectory;
            tbRootDirectory.Text = rootDir;

            string currentDir = rootDir;

            // ===== root =====

            // ===== data =====

            // ===== GAME00000 =====

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




            // ===== data_vie =====


            return NepErrCode.NoError;
        }*/
    }
}
