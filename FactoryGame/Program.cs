using FactoryGame.Menu;
using LonelyHill.Audio;
using LonelyHill.Core;
using LonelyHill.Event;
using LonelyHill.Graphics;
using LonelyHill.Scripting;
using LonelyHill.UI;
using LonelyHill.Utlity;
using SDL2;
using System;
using System.IO;

/*
TODO
Lua,
CustomFPS,
9SliceRewrite,
TextHoverRewrite,
...
*/

namespace FactoryGame
{
    public enum Location
    {
        Unkown,
        Introduction,
        Menu,
        NewGame,
        Loading,
        Overworld
    }

    public class Program
    {
        public static Location location = Location.Unkown;
        public static Font font;

        public static bool IsRunning = true;

        public static SettingsMenu settingsMenu;

        public static Audio buttonHoverAudio;
        public static Audio buttonPressAudio;

        public static float AudioConst = 18.28571428571429f;

        public static Texture pixel;
        public static Texture gray_button;
        public static SlicedTexture gray_textbox;

        public static Audio bgMusic;

        public static void lua_setLoc(string loc)
        {
            switch (loc)
            {
                case "introduction":
                    location = Location.Introduction;
                    break;
                case "menu":
                    location = Location.Menu;
                    break;
                case "newgame":
                    location = Location.NewGame;
                    break;
                case "loading":
                    location = Location.Loading;
                    break;
                case "unkown":
                    location = Location.Unkown;
                    break;
                default:
                    location = Location.Unkown;
                    break;
            }

            Engine.logger.info("Moving location to:", location.ToString());
        }

        static void Main(string[] args)
        {
            string r = File.ReadAllText(FileSystem.GetSource() + "/gamever");
            File.WriteAllText(FileSystem.GetSource() + "/gamever", (int.Parse(r) + 1).ToString());

            Engine engine = new Engine(880, 680, "Factory Game");
            engine.GlobalAudioVolume = AudioConst * 5.0f;

            font = new Font(FileSystem.GetSource() + "/content/font/orange_kid.ttf");

            location = Location.Menu;

            pixel = new Texture(FileSystem.GetSource() + "/content/ui/pixel.png");
            gray_button = new Texture(FileSystem.GetSource() + "/content/ui/gray_button.png");
            gray_textbox = new SlicedTexture(FileSystem.GetSource() + "/content/ui/textbox_gray_9slice.png");

            Introduction introduction = new Introduction();
            MainMenu mainMenu = new MainMenu();
            NewGame newGame = new NewGame();
            settingsMenu = new SettingsMenu();
            LoadingScreen loadingScreen = new LoadingScreen();

            buttonHoverAudio = new Audio(FileSystem.GetSource() + "/content/sound/button_hover.wav");
            buttonPressAudio = new Audio(FileSystem.GetSource() + "/content/sound/button_press.wav");
            bgMusic = new Audio(FileSystem.GetSource() + "/content/sound/menu_bg_temp.wav");
            bgMusic.ChangeVolume(50.0f);

            engine.scripting.SetGlobal("SetLocation", new Action<string>(lua_setLoc));

            //Script entrypoint = engine.scripting.CreateScript(FileSystem.GetSource() + "/content/script/entrypoint.lua");
            //entrypoint.Call();

            Text text = new Text();
            text.font = font;
            text.text = "LOADING";
            engine.rendererClass.Renderables.Add(text);

            ConsoleReader reader = new ConsoleReader();

            while (IsRunning)
            {
                PolledEvent ev = engine.GetEvents();
                if (ev.sdlEvent.type == SDL.SDL_EventType.SDL_QUIT) IsRunning = false;

                text.text = "CPU: x" + Environment.ProcessorCount + "\\OS: " + Environment.OSVersion.VersionString + "\\Build: " + Environment.Version.Build + "\\Machine Name: " + Environment.MachineName;

                SDL.SDL_RenderClear(Engine.Instance.renderer);

                switch (location)
                {
                    case Location.Unkown:
                        break;
                    case Location.Introduction:
                        introduction.IntroductionPrimary();
                        break;
                    case Location.Menu:
                        break;
                    case Location.NewGame:
                        
                        break;
                    case Location.Loading:
                        break;
                    case Location.Overworld:
                        break;
                    default:
                        Engine.logger.warn("Location error!");
                        break;
                }

                bgMusic.ChangeVolume(50.0f);

                engine.rendererClass.Render();
                settingsMenu.Primary();

                SDL.SDL_RenderPresent(Engine.Instance.renderer);
                engine.UpdateEngine(ev.sdlEvent);
            }

            reader.End();

            font.Cleanup();
            mainMenu.Cleanup();

            buttonHoverAudio.Cleanup();
            buttonPressAudio.Cleanup();
            bgMusic.Cleanup();

            pixel.Cleanup();
            gray_button.Cleanup();
            gray_textbox.Cleanup();

            settingsMenu.Cleanup();
            loadingScreen.Cleanup();
            mainMenu.Cleanup();
            newGame.Cleanup();

            engine.Close();
        }

        private static void CheckMusicBGRunning(Audio audio, bool restart, bool fade = false, int ms = 0)
        {
            if (!audio.IsPlaying() && restart)
            {
                audio.Play(fade, ms);
            }
        }

        private static void StopMusicIfRun(Audio audio, bool fade = false, int ms = 0)
        {
            if (audio.IsPlaying())
            {
                audio.Stop(fade, ms);
            }
        }

        public static void AddRenderable(Renderable renderable)
        {
            Engine.Instance.rendererClass.Renderables.Add(renderable);
        }

        public static Engine GetEngine()
        {
            return Engine.Instance;
        }
    }
}
