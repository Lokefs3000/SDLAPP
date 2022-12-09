using LonelyHill.Core;
using LonelyHill.Math;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Graphics
{
    public class Image : Renderable
    {
        public int Width = 0;
        public int Height = 0;

        private int width = 0;
        private int height = 0;

        public Texture texture;
        public Transform transform = new Transform();
        private Transform innerTrans = new Transform();
        public Vector4 Color = new Vector4(255.0f, 255.0f, 255.0f, 255.0f);

        public bool CenterAligned = false;
        public bool Autoscale = false;
        public bool AutoPosition = false;

        public Vector2 Offset = new Vector2(0, 0);

        public bool IsHovered = false;

        public void SetTexture()
        {
            if (texture.textureSurface.pixels != null)
            {
                Width = texture.textureSurface.w;
                Height = texture.textureSurface.h;

                this.width = Width;
                this.height = Height;
            }
        }

        public override void Update()
        {
            if (texture != null && Engine.Instance != null)
            {
                bool x = (Engine.Instance.mouse.MouseX >= transform.Position.x && Engine.Instance.mouse.MouseX < transform.Position.x + texture.textureSurface.w * transform.Scale.x);
                bool y = (Engine.Instance.mouse.MouseY >= transform.Position.y && Engine.Instance.mouse.MouseY < transform.Position.y + texture.textureSurface.h * transform.Scale.y);

                IsHovered = (x && y);
            }

            innerTrans.Position = transform.Position;
            innerTrans.Rotation = transform.Rotation;
            innerTrans.Scale = transform.Scale;

            if (Autoscale)
            {
                float scale = Engine.Instance.GetScaleAspect();
                width = (int)((float)Width * scale);
                height = (int)((float)Height * scale);
            }

            if (CenterAligned)
            {
                Vector2 size = Engine.Instance.GetSize();

                transform.Position = new Vector3(
                   (size.x / 2.0f) - (width * transform.Scale.x / 2.0f),
                   (size.y / 2.0f) - (height * transform.Scale.y / 2.0f),
                   0.0f);
            }

            if (AutoPosition)
            {
                Vector2 size = Engine.Instance.GetScaleVector();
                innerTrans.Position *= new Vector3(
                    size.x,
                    size.y,
                    0.0f
                    );
            }

            if (texture != null)
            {
                if (Width != texture.textureSurface.w || Height != texture.textureSurface.h)
                {
                    SetTexture();
                }
            }
        }

        public override void Render()
        {
            if (texture != null)
            {
                var src = innerTrans.ToRectSrc(texture.textureSurface.w, texture.textureSurface.h);
                var dest = innerTrans.ToRectDest(width, height);

                dest.x += (int)Offset.x;
                dest.y += (int)Offset.y;

                SDL.SDL_SetTextureColorMod(texture.texturePtr, (byte)Color.x, (byte)Color.y, (byte)Color.z);
                SDL.SDL_SetTextureAlphaMod(texture.texturePtr, (byte)Color.w);

                SDL.SDL_RenderCopy(Engine.Instance.renderer, texture.texturePtr, ref src, ref dest);

                SDL.SDL_SetTextureColorMod(texture.texturePtr, 255, 255, 255);
                SDL.SDL_SetTextureAlphaMod(texture.texturePtr, 255);
            }
        }
    }
}
