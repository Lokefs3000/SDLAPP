using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Debug
{
    public class Logger
    {
        public string format = "%sevcolor[yyyy:&m&m:dd:hh:mm:ss:ssss][%sevstr/%func]%sevcolorend - %msg";

        const string AsciiColorEnd = "\u001b[0m";

        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        public static void InitializeLogger()
        {
            var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
            if (!GetConsoleMode(iStdOut, out uint outConsoleMode))
            {
                Console.WriteLine("failed to get output console mode");
                Console.ReadKey();
                return;
            }

            outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
            if (!SetConsoleMode(iStdOut, outConsoleMode))
            {
                Console.WriteLine($"failed to set output console mode, error code: {GetLastError()}");
                Console.ReadKey();
                return;
            }
        }

        public enum Level
        {
            Info,
            Warn,
            Error,
            Fatal
        }

        private string construct(string[] strings)
        {
            string basestr = "";

            for (int i = 0; i < strings.Length; i++)
            {
                basestr += strings[i] + " ";
            }

            if (basestr.Length > 0)
            {
                basestr = basestr.Substring(0, basestr.Length - 1);
            }

            return basestr;
        }

        private string getasciicolor(string color)
        {
            switch (color)
            {
                case "white":
                    return "\u001b[37m";
                case "red":
                    return "\u001b[31m";
                case "green":
                    return "\u001b[32m";
                case "yellow":
                    return "\u001b[33m";
                case "blue":
                    return "\u001b[34m";
                case "brightred":
                    return "\u001b[31;1m";
                case "brightgreen":
                    return "\u001b[32;1m";
                case "brightyellow":
                    return "\u001b[33;1m";
                case "brightblue":
                    return "\u001b[34;1m";
                default:
                    return "\u001b[37m";
            }
        }

        private string makeformat(string reflection, string msg, Level level)
        {
            string formatted = format;
            formatted = formatted.Replace("%func", reflection);

            string infoCol = "white";
            string warnCol = "yellow";
            string errorCol = "brightred";
            string fatalCol = "red";

            formatted = formatted.Replace("%sevcolorend", AsciiColorEnd);

            switch (level)
            {
                case Level.Info:
                    formatted = formatted.Replace("%sevstr", "INFO");
                    formatted = formatted.Replace("%sevcolor", getasciicolor(infoCol));
                    break;
                case Level.Warn:
                    formatted = formatted.Replace("%sevstr", "WARN");
                    formatted = formatted.Replace("%sevcolor", getasciicolor(warnCol));
                    break;
                case Level.Error:
                    formatted = formatted.Replace("%sevstr", "ERROR");
                    formatted = formatted.Replace("%sevcolor", getasciicolor(errorCol));
                    break;
                case Level.Fatal:
                    formatted = formatted.Replace("%sevstr", "FATAL");
                    formatted = formatted.Replace("%sevcolor", getasciicolor(fatalCol));
                    break;
                default:
                    break;
            }

            DateTime time = DateTime.Now;
            formatted = formatted.Replace("yyyy", time.Year.ToString());
            formatted = formatted.Replace("&m&m", time.Month.ToString());
            formatted = formatted.Replace("dd", time.Day.ToString());
            formatted = formatted.Replace("hh", time.Hour.ToString());
            formatted = formatted.Replace("mm", time.Minute.ToString());
            formatted = formatted.Replace("ss", time.Second.ToString());
            formatted = formatted.Replace("ssss", time.Millisecond.ToString());

            formatted = formatted.Replace("%msg", msg);

            return formatted;
        }

        public void info(params string[] message)
        {
            StackTrace stackTrace = new StackTrace();
            string form = makeformat(stackTrace.GetFrame(1).GetMethod().Name, construct(message), Level.Info);

            Console.WriteLine(form);
        }

        public void warn(params string[] message)
        {
            StackTrace stackTrace = new StackTrace();
            string form = makeformat(stackTrace.GetFrame(1).GetMethod().Name, construct(message), Level.Warn);

            Console.WriteLine(form);
        }

        public void error(params string[] message)
        {
            StackTrace stackTrace = new StackTrace();
            string form = makeformat(stackTrace.GetFrame(1).GetMethod().Name, construct(message), Level.Error);

            Console.WriteLine(form);
        }

        public void fatal(params string[] message)
        {
            StackTrace stackTrace = new StackTrace();
            string form = makeformat(stackTrace.GetFrame(1).GetMethod().Name, construct(message), Level.Fatal);

            Console.WriteLine(form);
        }
    }
}
