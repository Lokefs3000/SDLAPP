using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Math
{
    public class Transform
    {
        public Vector3 Position = new Vector3();
        public Vector3 Rotation = new Vector3();
        public Vector3 Scale = new Vector3(1.0f, 1.0f, 1.0f);

        public SDL.SDL_Rect ToRectSrc(float width, float height)
        {
            var rect = new SDL.SDL_Rect();
            rect.x = 0;
            rect.y = 0;
            rect.w = (int)width;
            rect.h = (int)height;
            return rect;
        }

        public SDL.SDL_Rect ToRectDest(float width, float height)
        {
            var rect = new SDL.SDL_Rect();
            rect.x = (int)Position.x;
            rect.y = (int)Position.y;
            rect.w = (int)(width * Scale.x);
            rect.h = (int)(height * Scale.y);
            return rect;
        }
    }
}
