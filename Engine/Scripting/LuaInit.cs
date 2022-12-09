using LonelyHill.Core;
using LonelyHill.Layout;
using LonelyHill.Math;
using LonelyHill.Utlity;
using LonelyHill.Audio;
using NLua;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LonelyHill.Scripting
{
    public class LuaInit
    {
        public static Lua state;

        public List<Script> scripts = new List<Script>();

        public LuaInit()
        {
            state = new Lua();

            state.NewTable("Logger");
            state["Logger.Info"] = new Action<string[]>(Engine.logger.info);
            state["Logger.Warn"] = new Action<string[]>(Engine.logger.warn);
            state["Logger.Error"] = new Action<string[]>(Engine.logger.error);
            state["Logger.Fatal"] = new Action<string[]>(Engine.logger.fatal);
            
            state.NewTable("Layout");
            state["Layout.CreateUILayout"] = new Func<string, UILayout>(Engine.Instance.container.CreateUILayout);

            state.NewTable("Window");
            state["Window.GetScaleVector"] = new Func<Vector2>(Engine.Instance.GetScaleVector);

            state.NewTable("AudioManager");
            state["AudioManager.Create"] = new Func<string, Audio.Audio>(AudioManager.Create);
            state["AudioManager.CleanAudio"] = new Action<Audio.Audio>(AudioManager.CleanAudio);

            state["getContent"] = new Func<string>(getContent);
            state["wait"] = new Action<float>(wait);
            state["AbortThread"] = new Func<int>(AbortThread);

            state.UseTraceback = true;
        }

        public void SetGlobal(string key, Action<string> action)
        {
            state[key] = action;
        }

        public Lua GetState()
        {
            return state;
        }

        public Script CreateScript(string path)
        {
            Script script = new Script(path);
            scripts.Add(script);
            return script;
        }

        public void Cleanup()
        {
            for (int i = 0; i < scripts.Count; i++)
            {
                scripts[i].Cleanup();
            }
        }

        public void wait(float seconds)
        {
            Thread.Sleep((int)System.Math.Round(seconds * 1000.0f));
        }

        public int AbortThread()
        {
            Thread.CurrentThread.Abort();
            return 0;
        }

        public string getContent()
        {
            return FileSystem.GetSource() + "/content/";
        }
    }
}
