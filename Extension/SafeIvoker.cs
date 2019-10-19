// ;
using System;
using System.ComponentModel;

namespace ExwSharp
{
    public static class SafeInvoker
    {
        public static TResult SafeInvoke<T, TResult>(this T _t, Func<T, TResult> _func) where T : ISynchronizeInvoke
        {
            if (_t.InvokeRequired)
            {
                IAsyncResult result = _t.BeginInvoke(_func, new object[] { _t });
                object endResult = _t.EndInvoke(result);
                return (TResult)endResult;
            }
            else
                return _func(_t);
        }

        public static void SafeInvoke<T>(this T _t, Action<T> _action) where T : ISynchronizeInvoke
        {
            if (_t.InvokeRequired)
                _t.BeginInvoke(_action, new object[] { _t });
            else
                _action(_t);
        }
    }
}
