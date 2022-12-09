using LonelyHill.Utlity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryGame
{
    public class ConsoleReader
    {
        public Thread ReaderThread = null;
        public string CurrentCMD = "";

        public ConsoleReader()
        {
            ReaderThread = new Thread(Reader);
            ReaderThread.Start();
        }

        public void End()
        {
            ReaderThread.Abort();
        }

        public void Reader()
        {
            CurrentCMD = Console.ReadLine().ToLower();

            if (CurrentCMD == "reload_uilayouts")
            {
                Program.GetEngine().container.Reload();
                Console.WriteLine("Reloading layouts");
            }
            else if (CurrentCMD == "restart")
            {
                Process.Start(Environment.CurrentDirectory + "/FactoryGame.exe");
                Program.IsRunning = false;
                Console.WriteLine("Restarting..");
                Environment.Exit(0);
                return;
            }

            Reader();
        }
    }
}
