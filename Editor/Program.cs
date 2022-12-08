using LonelyHill.Core;
using LonelyHill.Event;
using LonelyHill.Utlity;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    internal class Program
    {
        public static Engine engine;
        public static Renderer renderer;

        public static string Project = "";
        public static string Content = "";

        static void Main(string[] args)
        {
            string r = File.ReadAllText(FileSystem.GetSource() + "/editorver");
            File.WriteAllText(FileSystem.GetSource(), (int.Parse(r) + 1).ToString());

            engine = new Engine(1080, 680, "Editor");
            renderer = new Renderer();

            while (true)
            {
                PolledEvent ev = engine.GetEvents();
                if (ev.sdlEvent.type == SDL.SDL_EventType.SDL_QUIT) break;

                SDL.SDL_RenderClear(Engine.Instance.renderer);

                renderer.Render();

                SDL.SDL_RenderPresent(Engine.Instance.renderer);
                engine.UpdateEngine(ev.sdlEvent);
            }

            engine.Close();
        }
    }
}
