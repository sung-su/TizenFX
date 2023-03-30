using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Tizen.NUI.FLUX
{
    internal class CLog
    {
        public static void Debug(string message, long d1 = NULL_VALUE, long d2 = NULL_VALUE, long d3 = NULL_VALUE, long d4 = NULL_VALUE, long d5 = NULL_VALUE,
            string s1 = null, string s2 = null, string s3 = null, string s4 = null, string s5 = null,
            double f1 = NULL_VALUE, double f2 = NULL_VALUE, double f3 = NULL_VALUE, double f4 = NULL_VALUE, double f5 = NULL_VALUE,
            string LOG_TAG = "Tizen.NUI.FLUX",
            [CallerFilePath] string file = "",
            [CallerMemberName] string func = "",
            [CallerLineNumber] int line = 0)
        {
            if (threadValue.Value == null)
            {
                threadValue.Value = ".........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!" + Thread.CurrentThread.ManagedThreadId;
            }

            int index = file.LastIndexOfAny(fileSeparator);
            string filename = ".........!.........!.........!.........!";
            CutString(filename, file, index + 1, file.Length - index - 1);

            string tmp = "null";

            if (message != null)
            {
                tmp = threadValue.Value;
                MakeString(tmp, message, d1, d2, d3, d4, d5, s1, s2, s3, s4, s5, f1, f2, f3, f4, f5);
            }

            Log.Debug(LOG_TAG, tmp, filename, func, line);
        }

        public static void Info(string message, long d1 = NULL_VALUE, long d2 = NULL_VALUE, long d3 = NULL_VALUE, long d4 = NULL_VALUE, long d5 = NULL_VALUE,
            string s1 = null, string s2 = null, string s3 = null, string s4 = null, string s5 = null,
            double f1 = NULL_VALUE, double f2 = NULL_VALUE, double f3 = NULL_VALUE, double f4 = NULL_VALUE, double f5 = NULL_VALUE,
            string LOG_TAG = "TV.FLUX",
            [CallerFilePath] string file = "",
            [CallerMemberName] string func = "",
            [CallerLineNumber] int line = 0)
        {
            if (threadValue.Value == null)
            {
                threadValue.Value = ".........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!" + Thread.CurrentThread.ManagedThreadId;
            }

            int index = file.LastIndexOfAny(fileSeparator);
            string filename = ".........!.........!.........!.........!";
            CutString(filename, file, index + 1, file.Length - index - 1);

            string tmp = "null";

            if (message != null)
            {
                tmp = threadValue.Value;
                MakeString(tmp, message, d1, d2, d3, d4, d5, s1, s2, s3, s4, s5, f1, f2, f3, f4, f5);
            }

            Log.Info(LOG_TAG, tmp, filename, func, line);
        }

        public static void Fatal(string message, long d1 = NULL_VALUE, long d2 = NULL_VALUE, long d3 = NULL_VALUE, long d4 = NULL_VALUE, long d5 = NULL_VALUE,
            string s1 = null, string s2 = null, string s3 = null, string s4 = null, string s5 = null,
            double f1 = NULL_VALUE, double f2 = NULL_VALUE, double f3 = NULL_VALUE, double f4 = NULL_VALUE, double f5 = NULL_VALUE,
            string LOG_TAG = "TV.FLUX",
            [CallerFilePath] string file = "",
            [CallerMemberName] string func = "",
            [CallerLineNumber] int line = 0)
        {
            if (threadValue.Value == null)
            {
                threadValue.Value = ".........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!" + Thread.CurrentThread.ManagedThreadId;
            }

            int index = file.LastIndexOfAny(fileSeparator);
            string filename = ".........!.........!.........!.........!";
            CutString(filename, file, index + 1, file.Length - index - 1);

            string tmp = "null";

            if (message != null)
            {
                tmp = threadValue.Value;
                MakeString(tmp, message, d1, d2, d3, d4, d5, s1, s2, s3, s4, s5, f1, f2, f3, f4, f5);
            }

            Log.Fatal(LOG_TAG, tmp, filename, func, line);
        }

        public static void Error(string message, long d1 = NULL_VALUE, long d2 = NULL_VALUE, long d3 = NULL_VALUE, long d4 = NULL_VALUE, long d5 = NULL_VALUE,
            string s1 = null, string s2 = null, string s3 = null, string s4 = null, string s5 = null,
            double f1 = NULL_VALUE, double f2 = NULL_VALUE, double f3 = NULL_VALUE, double f4 = NULL_VALUE, double f5 = NULL_VALUE,
            string LOG_TAG = "TV.FLUX",
            [CallerFilePath] string file = "",
            [CallerMemberName] string func = "",
            [CallerLineNumber] int line = 0)
        {
            if (threadValue.Value == null)
            {
                threadValue.Value = ".........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!" + Thread.CurrentThread.ManagedThreadId;
            }

            int index = file.LastIndexOfAny(fileSeparator);
            string filename = ".........!.........!.........!.........!";
            CutString(filename, file, index + 1, file.Length - index - 1);

            string tmp = "null";

            if (message != null)
            {
                tmp = threadValue.Value;
                MakeString(tmp, message, d1, d2, d3, d4, d5, s1, s2, s3, s4, s5, f1, f2, f3, f4, f5);
            }

            Log.Error(LOG_TAG, tmp, filename, func, line);
        }

        public static void Warn(string message, long d1 = NULL_VALUE, long d2 = NULL_VALUE, long d3 = NULL_VALUE, long d4 = NULL_VALUE, long d5 = NULL_VALUE,
            string s1 = null, string s2 = null, string s3 = null, string s4 = null, string s5 = null,
            double f1 = NULL_VALUE, double f2 = NULL_VALUE, double f3 = NULL_VALUE, double f4 = NULL_VALUE, double f5 = NULL_VALUE,
            string LOG_TAG = "TV.FLUX",
            [CallerFilePath] string file = "",
            [CallerMemberName] string func = "",
            [CallerLineNumber] int line = 0)
        {
            if (threadValue.Value == null)
            {
                threadValue.Value = ".........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!.........!" + Thread.CurrentThread.ManagedThreadId;
            }

            int index = file.LastIndexOfAny(fileSeparator);
            string filename = ".........!.........!.........!.........!";
            CutString(filename, file, index + 1, file.Length - index - 1);

            string tmp = "null";

            if (message != null)
            {
                tmp = threadValue.Value;
                MakeString(tmp, message, d1, d2, d3, d4, d5, s1, s2, s3, s4, s5, f1, f2, f3, f4, f5);
            }

            Log.Warn(LOG_TAG, tmp, filename, func, line);
        }

        public static void MakeString(string _dest, string _sour,
                   long d1, long d2, long d3, long d4, long d5,
                   string s1, string s2, string s3, string s4, string s5,
                   double f1, double f2, double f3, double f4, double f5)
        {
            int destLength = _dest.Length - 1;
            int didx = 0;
            int sidx = 0;
            int step = 0;

#pragma warning disable Unsafe // The code is unsafe
            unsafe
            {
                fixed (char* dest = _dest)
                fixed (char* sour = _sour)
                {
                    while (sour[sidx] != 0 && didx < destLength)
                    {
                        if (sour[sidx] == '%')
                        {
                            sidx++;

                            char formatSpecifier = sour[sidx];

                            switch (formatSpecifier)
                            {
                                case 'f': // fall through
                                case 'd': // fall through
                                    {
                                        sidx++;
                                        step = sour[sidx] - '0';

                                        long iValue = 0;
                                        double fValue = 0.0f;

                                        long underDecimalNumber = 0;
                                        int loopForFloat = 1;

                                        if (formatSpecifier == 'd')
                                        {
                                            switch (step)
                                            {
                                                case 1: iValue = d1; break;
                                                case 2: iValue = d2; break;
                                                case 3: iValue = d3; break;
                                                case 4: iValue = d4; break;
                                                case 5: iValue = d5; break;
                                            }

                                        }
                                        else if (formatSpecifier == 'f')
                                        {

                                            switch (step)
                                            {
                                                case 1: fValue = f1; break;
                                                case 2: fValue = f2; break;
                                                case 3: fValue = f3; break;
                                                case 4: fValue = f4; break;
                                                case 5: fValue = f5; break;
                                            }

                                            iValue = (int)fValue;

                                            int decimalOperand = 1;
                                            int count = DecimalPointDigit;
                                            double adjustmentValue = 0.5f;

                                            while (count-- != 0)
                                            {
                                                decimalOperand *= 10;
                                                adjustmentValue /= 10;
                                            }

                                            underDecimalNumber = (int)((fValue + adjustmentValue - iValue) * decimalOperand * ((iValue < 0) ? -1 : 1));

                                            loopForFloat = 2;
                                        }

                                        if (iValue < 0)
                                        {
                                            dest[didx++] = '-';
                                            if (didx >= destLength)
                                            {
                                                goto FINISH;
                                            }

                                            iValue = -iValue;
                                        }
                                        else if (iValue == NULL_VALUE)
                                        {
                                            sidx++;
                                            continue;
                                        }

                                        // 기본은 20자리까지만 출력
                                        int digit = 20;

                                        do
                                        {
                                            int left = didx;

                                            do
                                            {
                                                if (digit-- == 0)
                                                {
                                                    break;
                                                }

                                                dest[didx++] = (char)((iValue % 10) + '0');

                                                if (didx >= destLength)
                                                {
                                                    goto FINISH;
                                                }

                                                iValue /= 10;

                                            } while (iValue != 0);

                                            // swap digit
                                            int right = didx - 1;
                                            int size = (right - left + 1) / 2;
                                            for (int a = 0; a < size; a++, left++, right--)
                                            {
                                                char t = dest[left];
                                                dest[left] = dest[right];
                                                dest[right] = t;
                                            }

                                            if (loopForFloat == 2)
                                            {
                                                // assign under decimal value to copy
                                                dest[didx++] = '.';
                                                iValue = underDecimalNumber;

                                                // float 의 경우는 해당 자리수까지 출력
                                                digit = DecimalPointDigit;
                                            }

                                        } while (--loopForFloat != 0);

                                    }
                                    break;

                                case 's':
                                    {
                                        sidx++;
                                        step = sour[sidx] - '0';
                                        string istr = null;
                                        switch (step)
                                        {
                                            case 1: istr = s1; break;
                                            case 2: istr = s2; break;
                                            case 3: istr = s3; break;
                                            case 4: istr = s4; break;
                                            case 5: istr = s5; break;
                                        }

                                        if (istr == null)
                                        {
                                            istr = "null";
                                        }

                                        fixed (char* nstr = istr)
                                        {
                                            int idx = 0;
                                            while (nstr[idx] != 0)
                                            {
                                                dest[didx++] = nstr[idx++];
                                                if (didx >= destLength)
                                                {
                                                    goto FINISH;
                                                }
                                            }
                                        }
                                    }
                                    break;

                                case '%':
                                    {
                                        dest[didx++] = '%';
                                        if (didx >= destLength)
                                        {
                                            goto FINISH;
                                        }
                                    }
                                    break;


                                default:
                                    break;
                            }

                        }
                        else
                        {
                            dest[didx++] = sour[sidx];
                            if (didx >= destLength)
                            {
                                goto FINISH;
                            }
                        }
                        sidx++;
                    }
                FINISH:
                    dest[didx] = '\0';
                }
            }
            return;
#pragma warning restore Unsafe // The code is unsafe
        }

        internal static void CutString(string _dest, string _sour, int begin, int size)
        {
#pragma warning disable Unsafe // The code is unsafe
            unsafe
            {
                fixed (char* dest = _dest)
                fixed (char* sour = _sour)
                {
                    int didx = 0;
                    int sidx = begin;
                    for (int a = 0; a < size; a++)
                    {
                        dest[didx++] = sour[sidx++];
                    }
                    dest[didx++] = (char)0;
                }
            }
#pragma warning restore Unsafe // The code is unsafe
        }

        public static string EnumToString(Enum e)
        {
            if (e == null)
            {
                return null;
            }
            if (dicEnum.TryGetValue(e, out string ret) == false)
            {
                ret = e.ToString();
                dicEnum.TryAdd(e, ret);
            }
            return ret;
        }

        public static int DecimalPointDigit
        {
            get;
            set;
        } = 3;

        private const int NULL_VALUE = 9999999;
        private static readonly char[] fileSeparator = { '\\', '/' };
        private static readonly ConcurrentDictionary<Enum, string> dicEnum = new ConcurrentDictionary<Enum, string>();
        private static ThreadLocal<string> threadValue = new ThreadLocal<string>();
    }
}
