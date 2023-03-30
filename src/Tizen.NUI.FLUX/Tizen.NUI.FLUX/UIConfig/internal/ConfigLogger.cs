using System;
using System.Runtime.CompilerServices;
using Tizen.NUI.FLUX;

namespace Tizen.NUI.FLUX
{
    internal class ConfigLogger
    {
        public const int INT_NULL = 0;

        public static int GetColor(float color)
        {
            return (int)(color * 255);
        }

        public static void DebugP(string message, long d1 = NULL_VALUE, long d2 = NULL_VALUE, long d3 = NULL_VALUE, long d4 = NULL_VALUE, long d5 = NULL_VALUE,
            string s1 = null, string s2 = null, string s3 = null, string s4 = null, string s5 = null,
            double f1 = NULL_VALUE, double f2 = NULL_VALUE, double f3 = NULL_VALUE, double f4 = NULL_VALUE, double f5 = NULL_VALUE, string tag = LOG_TAG,
            [CallerFilePath] string file = "",
            [CallerMemberName] string func = "",
            [CallerLineNumber] int line = 0)
        {
            CLog.Debug(message, d1, d2, d3, d4, d5, s1, s2, s3, s4, s5, f1, f2, f3, f4, f5, tag, file, func, line);
        }

        public static void InfoP(string message, long d1 = NULL_VALUE, long d2 = NULL_VALUE, long d3 = NULL_VALUE, long d4 = NULL_VALUE, long d5 = NULL_VALUE,
            string s1 = null, string s2 = null, string s3 = null, string s4 = null, string s5 = null,
            double f1 = NULL_VALUE, double f2 = NULL_VALUE, double f3 = NULL_VALUE, double f4 = NULL_VALUE, double f5 = NULL_VALUE, string tag = LOG_TAG,
            [CallerFilePath] string file = "",
            [CallerMemberName] string func = "",
            [CallerLineNumber] int line = 0)
        {
            CLog.Info(message, d1, d2, d3, d4, d5, s1, s2, s3, s4, s5, f1, f2, f3, f4, f5, tag, file, func, line);
        }

        public static void FatalP(string message, long d1 = NULL_VALUE, long d2 = NULL_VALUE, long d3 = NULL_VALUE, long d4 = NULL_VALUE, long d5 = NULL_VALUE,
            string s1 = null, string s2 = null, string s3 = null, string s4 = null, string s5 = null,
            double f1 = NULL_VALUE, double f2 = NULL_VALUE, double f3 = NULL_VALUE, double f4 = NULL_VALUE, double f5 = NULL_VALUE, string tag = LOG_TAG,
            [CallerFilePath] string file = "",
            [CallerMemberName] string func = "",
            [CallerLineNumber] int line = 0)
        {
            CLog.Fatal(message, d1, d2, d3, d4, d5, s1, s2, s3, s4, s5, f1, f2, f3, f4, f5, tag, file, func, line);
        }

        public static void ErrorP(string message, long d1 = NULL_VALUE, long d2 = NULL_VALUE, long d3 = NULL_VALUE, long d4 = NULL_VALUE, long d5 = NULL_VALUE,
            string s1 = null, string s2 = null, string s3 = null, string s4 = null, string s5 = null,
            double f1 = NULL_VALUE, double f2 = NULL_VALUE, double f3 = NULL_VALUE, double f4 = NULL_VALUE, double f5 = NULL_VALUE, string tag = LOG_TAG,
            [CallerFilePath] string file = "",
            [CallerMemberName] string func = "",
            [CallerLineNumber] int line = 0)
        {
            CLog.Error(message, d1, d2, d3, d4, d5, s1, s2, s3, s4, s5, f1, f2, f3, f4, f5, tag, file, func, line);
        }

        public static void WarnP(string message, long d1 = NULL_VALUE, long d2 = NULL_VALUE, long d3 = NULL_VALUE, long d4 = NULL_VALUE, long d5 = NULL_VALUE,
            string s1 = null, string s2 = null, string s3 = null, string s4 = null, string s5 = null,
            double f1 = NULL_VALUE, double f2 = NULL_VALUE, double f3 = NULL_VALUE, double f4 = NULL_VALUE, double f5 = NULL_VALUE, string tag = LOG_TAG,
            [CallerFilePath] string file = "",
            [CallerMemberName] string func = "",
            [CallerLineNumber] int line = 0)
        {
            CLog.Warn(message, d1, d2, d3, d4, d5, s1, s2, s3, s4, s5, f1, f2, f3, f4, f5, tag, file, func, line);
        }

        public static string EnumToString(Enum e)
        {
            return CLog.EnumToString(e);
        }

        internal const int NULL_VALUE = 9999999;

        internal const string LOG_TAG = "Tizen.NUI.FLUX.UIConfig";
    }
}
