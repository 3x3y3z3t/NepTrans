// ;

using System.Collections.Generic;

namespace NepTrans
{
    public struct NepError
    {
        static readonly Dictionary<NepErrCode, string> ErrorString = new Dictionary<NepErrCode, string>
        {
            {NepErrCode.NoError, "No Error."},
        };
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

        public static string GetErrorString(NepErrCode _errCode)
        {
            try
            {
                return ErrorString[_errCode];
            }
            catch (KeyNotFoundException)
            {
                return $"There is no description for this error: {_errCode}";
            }
        }

        public override string ToString()
        {
            return $"Error {ErrorCode}: {ExtraInfo}";
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
