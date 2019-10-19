﻿// ;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExwSharp
{
    public enum LogTarget
    {
        None = 0,
        Console = 1 << 0,
        File = 1 << 1,
    }

    public sealed class Logger
    {
        private static readonly object padlock = new object();
        private static Logger s_Instance = null;
        private static Logger Instance
        {
            get
            {
                lock (padlock)
                {
                    if (s_Instance == null)
                        s_Instance = new Logger();
                }
                return s_Instance;
            }
        }

        public static LogTarget LogTarget = LogTarget.None;
        public static string LogTargetDir { get; set; }
        private static string LogTargetFile { get; set; }
        private static List<string> BatchLogMessages { get; set; }

        static Logger()
        {
            DateTime dt = DateTime.Now;
            LogTarget = LogTarget.None;
            LogTargetDir = Directory.GetCurrentDirectory();
            LogTargetFile = string.Format("{0:00}-{1:00}-{2:00}_{3:00}-{4:00}.log", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
            BatchLogMessages = new List<string>();

            File.Create($"{LogTargetDir}\\{LogTargetFile}").Close();
        }

        ~Logger()
        {
            BatchLogMessages.Clear();
        }

        public static void Log(string _msg)
        {
            Log(_msg, LogTarget);
        }

        public static void Log(string _msg, LogTarget _overridedLogTarget)
        {
            BatchLog(_msg);
            FlushBatchLog(_overridedLogTarget);
        }

        public static void BatchLog(string _msg)
        {
            BatchLogMessages.Add(FormatLogMessage(_msg));
        }

        public static void FlushBatchLog()
        {
            FlushBatchLog(LogTarget);
        }

        public static void FlushBatchLog(LogTarget _overridedLogTarget)
        {

            if (_overridedLogTarget == LogTarget.None)
                return;

            StringBuilder sb = new StringBuilder();
            foreach (string s in BatchLogMessages)
                sb.Append($"{s}\r\n");

            string messages = sb.ToString();
            if (((int)_overridedLogTarget & (int)LogTarget.Console) > 0)
            {
                Console.Write(messages);
            }
            if (((int)_overridedLogTarget & (int)LogTarget.File) > 0)
            {
                lock (padlock)
                {
                    File.AppendAllText($"{LogTargetDir}\\{LogTargetFile}", messages, Encoding.UTF8);
                }
            }

            BatchLogMessages.Clear();
        }

        private static string FormatLogMessage(string _msg)
        {
            DateTime dt = DateTime.Now;
            return string.Format("[{0:00}:{1:00}:{2:00} {3}.{4:00}.{5:00}]: {6}",
                dt.Hour, dt.Minute, dt.Second, dt.Year, dt.Month, dt.Day, _msg);
        }
    }
}