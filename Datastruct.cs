// ;
using System.Collections.Generic;

namespace NepTrans
{
    public enum EntryType
    {
        None = 0,
        GameScript,
        SystemScript,
    }

    public class Record
    {
        public string Filename; // filename is the full path, relative to the Root Directory;
        public string Id;
        public string TextJap;
        public string TextEng;
        public string TextVie;

        public Record()
        {
            Filename = "";
            Id = "";
            TextJap = "";
            TextEng = "";
            TextVie = "";
        }

        public Record(string _filename, string _id, string _textJap, string _textEng, string _textVie = "")
        {
            Filename = _filename;
            Id = _id;
            TextJap = _textJap;
            TextEng = _textEng;
            TextVie = _textVie;
        }
    }

    public class Entry
    {
        public EntryType Type;
        public string Path;
        public string Name;
        private Dictionary<string, Record> Records;
        public int RecordCount { get { return Records.Count; } }
        public int CompletedRecordCount
        {
            get
            {
                int count = 0;
                foreach (Record r in Records.Values)
                {
                    if (r.TextVie != null && !r.TextVie.Equals(""))
                        ++count;
                }
                return count;
            }
        }

        public Entry(string _path, string _name, EntryType _type)
        {
            Type = _type;
            Path = _path;
            Name = _name;
            Records = new Dictionary<string, Record>();
        }

        public void AddRecord(Record _record)
        {

        }

        public Record GetRecord(string _id)
        {
            if (Records.ContainsKey(_id))
                return Records[_id];
            return null;
        }
    }
}
