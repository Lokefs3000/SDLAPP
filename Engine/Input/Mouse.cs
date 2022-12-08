using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Input
{
    public class Mouse
    {
        public int MouseX = 0;
        public int MouseY = 0;

        public float MouseAccelX = 0;
        public float MouseAccelY = 0;

        private int LastMouseX = 0;
        private int LastMouseY = 0;

        public bool LeftButtonDown = false;
        public bool RightButtonDown = false;
        public bool MiddleButtonDown = false;

        public void Update(SDL.SDL_Event e)
        {
            SDL.SDL_GetMouseState(out MouseX, out MouseY);

            switch (e.type)
            {
                case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    LeftButtonDown = (e.button.button == SDL.SDL_BUTTON_LEFT);
                    RightButtonDown = (e.button.button == SDL.SDL_BUTTON_RIGHT);
                    MiddleButtonDown = (e.button.button == SDL.SDL_BUTTON_MIDDLE);
                    break;
                case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                    if (e.button.button == SDL.SDL_BUTTON_LEFT)
                        LeftButtonDown = false;
                    if (e.button.button == SDL.SDL_BUTTON_RIGHT)
                        RightButtonDown = false;
                    if (e.button.button == SDL.SDL_BUTTON_MIDDLE)
                        MiddleButtonDown = false;
                    break;
            }

            MouseAccelX = (float)LastMouseX - (float)MouseX;
            MouseAccelY = (float)LastMouseY - (float)MouseY;

            LastMouseX = MouseX;
            LastMouseY = MouseY;
        }
    }
}
