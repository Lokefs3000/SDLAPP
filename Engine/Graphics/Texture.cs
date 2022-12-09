using LonelyHill.Core;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (!File.Exists(path))
            {
                Engine.logger.error("File at path:", path, ", doesnt exist!");
                return;
            }

            texturePtr = SDL_image.IMG_LoadTexture(Engine.Instance.renderer, path);

            string err = SDL_image.IMG_GetError();

            if (err == " ")
            {
                Engine.logger.error("Failed to load texture:", path, ", SDL_image error:", err);
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
