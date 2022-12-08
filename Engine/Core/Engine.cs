using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LonelyHill.Audio;
using LonelyHill.Debug;
using LonelyHill.Event;
using LonelyHill.Input;
using LonelyHill.Layout;
using LonelyHill.Math;
using LonelyHill.Scripting;
using SDL2;

namespace LonelyHill.Core
{
    public class Engine
    {
        public static Engine Instance { get; private set; }

        public static Logger logger { get; } = new Logger();

        public IntPtr window { get; private set; }
        public IntPtr surface { get; private set; }
        public IntPtr renderer { get; private set; }
        public IntPtr opengl { get; private set; }

        public Renderer rendererClass { get; private set; }

        public Mouse mouse { get; private set; }
        public Keyboard keyboard { get; private set; }

        public LuaInit scripting { get; private set; }

        public LayoutContainer container { get; private set; }

        public float Delta { get; private set; } = 0.0f;
        private ulong lastDelta = 0;

        public float GlobalAudioVolume = 100.0f;

        SDL.SDL_Event e;

        private int width;
        private int height;

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowPos(
        IntPtr handle,
        IntPtr handleAfter,
        int x,
        int y,
        int cx,
        int cy,
        uint flags
        );
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr child, IntPtr newParent);
        [DllImport("user32.dll")]
        private static extern IntPtr ShowWindow(IntPtr handle, int command);

        private delegate void Viewport(int x, int y, int width, int height);
        private delegate void ClearColor(float r, float g, float b, float a);
        private delegate void Clear(uint flags);

        public Engine(int width, int height, string title, Panel sdlPanel = null)
        {
            this.width = width;
            this.height = height;

            Logger.InitializeLogger();

            SDL.SDL_SetHint(SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");
            
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_AUDIO) < 0)
            {
                logger.fatal("Failed to initialize SDL!");
                return;
            }

            if (sdlPanel == null)
                window = SDL.SDL_CreateWindow(title, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, width, height, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
            else
                window = SDL.SDL_CreateWindow(string.Empty, 0, 0, sdlPanel.Width, sdlPanel.Height, SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS | SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL);
            if (window == null)
            {
                logger.fatal("Failed to create window!");
                return;
            }

            opengl = SDL.SDL_GL_CreateContext(window);

            if (sdlPanel != null)
            {
                var glViewport = (Viewport)Marshal.GetDelegateForFunctionPointer(
                    SDL.SDL_GL_GetProcAddress("glViewport"),
                    typeof(Viewport)
                );
                var glClearColor = (ClearColor)Marshal.GetDelegateForFunctionPointer(
                    SDL.SDL_GL_GetProcAddress("glClearColor"),
                    typeof(ClearColor)
                );
                var glClear = (Clear)Marshal.GetDelegateForFunctionPointer(
                    SDL.SDL_GL_GetProcAddress("glClear"),
                    typeof(Clear)
                );

                glViewport(0, 0, sdlPanel.Width, sdlPanel.Height);
                glClearColor(1.0f, 0.0f, 0.0f, 1.0f);
                glClear(0x4000);
                SDL.SDL_GL_SwapWindow(opengl);

                SDL.SDL_SysWMinfo info = new SDL.SDL_SysWMinfo();
                SDL.SDL_GetWindowWMInfo(window, ref info);
                IntPtr winHandle = info.info.win.window;

                SetWindowPos(
                    winHandle,
                    sdlPanel.Handle,
                    0,
                    0,
                    0,
                    0,
                    0x0401 // NOSIZE | SHOWWINDOW
                );

                // Attach the SDL2 window to the panel
                SetParent(winHandle, sdlPanel.Handle);
                ShowWindow(winHandle, 1); // SHOWNORMAL
            }

            surface = SDL.SDL_GetWindowSurface(window);

            logger.info("Succesfully created SDL window.");

            renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            mouse = new Mouse();
            keyboard = new Keyboard();

            if (SDL_ttf.TTF_Init() < 0)
            {
                logger.fatal("Failed to initialize SDL TTF!");
                return;

            }

            if (SDL_mixer.Mix_OpenAudio(44100, SDL_mixer.MIX_DEFAULT_FORMAT, 2, 4096) < 0)
            {
                logger.fatal("Failed to initialize SDL Mixer!");
                return;

            }
            SDL_mixer.Mix_AllocateChannels(32);

            rendererClass = new Renderer();
            container = new LayoutContainer();

            Instance = this;

            scripting = new LuaInit();
        }

        public PolledEvent GetEvents()
        {
            SDL.SDL_PollEvent(out e);
            PolledEvent ev = new PolledEvent();
            ev.sdlEvent = e;
            return ev;
        }

        public void UpdateEngine(SDL.SDL_Event e)
        {
            mouse.Update(e);
            keyboard.Update(e);

            ulong start = SDL.SDL_GetPerformanceCounter();

            if (AudioVar.channelsInUse.Count > 32)
            {
                logger.warn("More then 32 audio channels in use, consider lowering used audio or increasing audio limit!");
            }

            //SDL_mixer.Mix_Volume(-1, (int)Mathf.Clamp(GlobalAudioVolume, SDL_mixer.MIX_MAX_VOLUME, 0.0f));

            Delta = (start - SDL.SDL_GetPerformanceCounter()) / (float)SDL.SDL_GetPerformanceFrequency() / 10000000000000.0f;
        }

        public void Close()
        {
            logger.info("Stopping engine..");

            scripting.Cleanup();
            container.Cleanup();
            AudioManager.GlobalClean();

            SDL_ttf.TTF_Quit();
            SDL_mixer.Mix_Quit();
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_FreeSurface(surface);
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
        }

        public Vector2 GetSize()
        {
            int w, h;
            SDL.SDL_GetWindowSize(window, out w, out h);
            return new Vector2(w, h);
        }

        public Vector2 GetScaleVector()
        {
            Vector2 size = GetSize();
            return new Vector2(size.x / width, size.y / height);
        }

        public float GetScaleAspect()
        {
            Vector2 scale = GetScaleVector();
            return (scale.y + scale.x) / 2.0f;
        }
    }
}
