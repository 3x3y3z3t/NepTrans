// ;

namespace NepTrans
{
    public struct NepError
    {
        public static NepError NoError = new NepError()
        {
            ErrorCode = NepErrCode.NoError,
            ExtraInfo = "",
        };

        public NepErrCode ErrorCode;
        public string ExtraInfo;

        public NepError(NepErrCode _errCode, string _info = "")
        {
            ErrorCode = _errCode;
            ExtraInfo = _info;
        }

        public override string ToString()
        {
            return $"Error {ErrorCode}({(int)ErrorCode}): {ExtraInfo}";
        }
    }

    public enum NepErrCode
    {
        NoError = 0,

        DirectoryNotSet = 1,
        DirectoryNotFound = 2,
        RootDirNotFound = 3,

        EngDirNotFound = 11,
        JapDirNotFound = 12,
        VieDirNotFound = 13,
        GameScriptDirNotFound = 14,
        SystemScriptDirNotFound = 15,

        NoData = 21,


    }
}
