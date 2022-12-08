using LonelyHill.Core;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LonelyHill.UI
{
    public class Font
    {
        public IntPtr font { get; private set; }

        public Dictionary<char, IntPtr> glyphs;
        public Dictionary<char, SDL.SDL_Rect> glyphRects;

        public static string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789><!" + '"' + "#¤%&()=?`|}][{€$£@½/'*¨~,.;: ";

        public Font(string path, int size = 24) {
            font = SDL_ttf.TTF_OpenFont(path, size);
            if (font == null)
            {
                Engine.logger.error("Failed to load font! Error: " + SDL_ttf.TTF_GetError());
                return;
            }

            glyphs = new Dictionary<char, IntPtr>();
            glyphRects = new Dictionary<char, SDL.SDL_Rect>();

            for (int i = 0; i < letters.Length; i++)
            {
                if (!glyphs.ContainsKey(letters[i]))
                {
                    IntPtr glyph = SDL_ttf.TTF_RenderGlyph_Solid(font, letters[i], new SDL.SDL_Color() { r = 255, g = 255, b = 255, a = 255 });
                    IntPtr texture = SDL.SDL_CreateTextureFromSurface(Engine.Instance.renderer, glyph);
                    SDL.SDL_Surface s = Marshal.PtrToStructure<SDL.SDL_Surface>(glyph);

                    SDL.SDL_Rect rect = new SDL.SDL_Rect();
                    rect.x = 0;
                    rect.y = 0;
                    rect.w = s.w;
                    rect.h = s.h;

                    glyphs.Add(letters[i], texture);
                    glyphRects.Add(letters[i], rect);

                    SDL.SDL_FreeSurface(glyph);
                }
            }
        }

        public void Cleanup()
        {
            SDL_ttf.TTF_CloseFont(font);

            for (int i = 0; i < letters.Length; i++)
            {
                SDL.SDL_DestroyTexture(glyphs[letters[i]]);
            }
        }
    }
}
