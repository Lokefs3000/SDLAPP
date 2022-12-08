using LonelyHill.Core;
using LonelyHill.Graphics;
using LonelyHill.Input;
using LonelyHill.Math;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.UI
{
    public class Button : Renderable
    {
        public Transform transform { get; } = new Transform();
        public Texture texture;

        public int width = 100;
        public int height = 100;

        public bool IsHovered { get; private set; } = false;
        public bool IsPressed { get; private set; } = false;
        public bool IsPressedThisFrame { get; private set; } = false;

        private bool IsPressedLastFrame = false;

        public override void Update()
        {
            bool x = (Engine.Instance.mouse.MouseX >= transform.Position.x && Engine.Instance.mouse.MouseX < transform.Position.x + width);
            bool y = (Engine.Instance.mouse.MouseY >= transform.Position.y && Engine.Instance.mouse.MouseY < transform.Position.y + height);

            IsHovered = (x && y);
            IsPressed = (IsHovered && Engine.Instance.mouse.LeftButtonDown);

            if (IsPressed && !IsPressedLastFrame)
            {
                IsPressedThisFrame = true;
            }
            else
            {
                IsPressedThisFrame = false;
            }

            IsPressedLastFrame = IsPressed;
        }

        public override void Render()
        {
            var dest = transform.ToRectDest(width, height);
            var src = transform.ToRectSrc(texture.textureSurface.w, texture.textureSurface.h);
            var center = new SDL.SDL_Point() { x = width / 2, y = height / 2 }; 

            SDL.SDL_RenderCopyEx(Engine.Instance.renderer, texture.texturePtr, ref src, ref dest, transform.Rotation.z, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }
    }
}
