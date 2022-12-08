using LonelyHill.Core;
using SDL2;
using System;
using System.Runtime.InteropServices;
using LonelyHill.Utlity;
using LonelyHill.Graphics;
using LonelyHill.UI;
using LonelyHill.Math;
using System.Diagnostics;

namespace SDLAPP
{
    public static class Program
    {
        public static Engine engine;

        static void Main(string[] args)
        {
            engine = new Engine(740, 480, "SDL C#");
            Renderer renderer = new Renderer();

            Texture texture = new Texture(FileSystem.GetSource() + "/seagul.jpg");
            Button button = new Button() { texture = texture };
            renderer.Renderables.Add(button);

            button.transform.Position = new Vector3(640.0f / 2.0f - button.width / 2.0f, 480.0f / 2.0f - button.height / 2.0f);

            Font font = new Font(FileSystem.GetSource() + "/Roboto.ttf");

            Text text = new Text();
            text.FontColor = new Vector4(255.0f, 255.0f, 255.0f, 255.0f);
            text.text = "Hello, world!";
            text.font = font;
            text.RegenTexture();
            renderer.Renderables.Add(text);

            button.width = text.fontRect.w;
            button.height = text.fontRect.h;

            while (true)
            {
                SDL.SDL_Event e = engine.GetEvents();
                if (e.type == SDL.SDL_EventType.SDL_QUIT) break;

                button.Update();

                renderer.Render();
                engine.UpdateEngine(e);
            }

            font.Cleanup();
            texture.Cleanup();
            text.Cleanup();

            engine.Close();
        }
    }
}
