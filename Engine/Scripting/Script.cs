using LonelyHill.Core;
using System.Threading;
using NLua;
using KeraLua;
using System.Text;

namespace LonelyHill.Scripting
{
    public class Script
    {
        public string path { get; private set; } = "";
        private Thread thread;

        public bool IsActive { get; private set; } = false;

        public Script(string path)
        {
            this.path = path;
        }

        public void Call()
        {
            Engine.logger.info("Loading new lua thread from path:", path);
            Cleanup();

            Engine.Instance.scripting.GetState().State.Encoding = Encoding.UTF8;

            thread = new Thread(runlua);
            thread.Start();
        }

        public void Stop()
        {
            thread.Abort();
        }

        private void runlua()
        {
            IsActive = true;

            Engine.Instance.scripting.GetState().DoFile(path);

            IsActive = false;
            Thread.CurrentThread.Abort();
        }

        public void Cleanup()
        {
            if (thread != null)
            {
                thread.Abort(true);
                thread = null;
            }
        }
    }
}
