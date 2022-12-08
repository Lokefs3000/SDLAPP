using LonelyHill.Core;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Graphics
{
    public class Texture
    {
        public IntPtr texturePtr { get; private set; }
        public SDL.SDL_Surface textureSurface { get; private set; }
        
        public Texture(String path)
        {
            texturePtr = SDL_image.IMG_LoadTexture(Engine.Instance.renderer, path);

            if (texturePtr == null)
            {
                Engine.logger.error("Failed to load texture: ", path);
                return;
            }

            textureSurface = Marshal.PtrToStructure<SDL.SDL_Surface>(texturePtr);
        }

        public void Cleanup()
        {
            SDL.SDL_DestroyTexture(texturePtr);
        }
    }
}
