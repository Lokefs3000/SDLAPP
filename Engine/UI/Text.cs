using LonelyHill.Core;
using LonelyHill.Debug;
using LonelyHill.Graphics;
using LonelyHill.Math;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.UI
{
    public class Text : Renderable
    {
        public Font font;

        public int TotalWidth = 0;
        public int TotalHeight = 0;

        public Vector4 FontColor = new Vector4(255.0f, 255.0f, 255.0f, 255.0f);

        public TextAlign Align = TextAlign.Left;

        public Transform transform { get; private set; } = new Transform();

        public float Scale = 1.0f;

        public string text = "";

        public bool IsHovered { get; private set; }

        public float CalculateTextWidth()
        {
            SDL_ttf.TTF_SizeText(font.font, text, out TotalWidth, out TotalHeight);
            return TotalWidth * Scale * transform.Scale.x;
        }

        public float CalculateTextHeight()
        {
            SDL_ttf.TTF_SizeText(font.font, text, out TotalWidth, out TotalHeight);
            return TotalHeight * Scale * transform.Scale.y;
        }

        public override void Render()
        {
            if (font != null)
            {
                SDL_ttf.TTF_SizeText(font.font, text, out TotalWidth, out TotalHeight);
                int x = 0;

                for (int i = 0; i < text.Length; i++)
                {
                    if (font.glyphRects.ContainsKey(text[i]))
                    {
                        var src = transform.ToRectSrc(font.glyphRects[text[i]].w * Scale, font.glyphRects[text[i]].h * Scale);
                        var dest = transform.ToRectDest(font.glyphRects[text[i]].w * Scale, font.glyphRects[text[i]].h * Scale);

                        if (Align == TextAlign.Center)
                        {
                            dest.x -= (int)(TotalWidth / 2 * Scale);
                        }
                        else if (Align == TextAlign.Right)
                        {
                            dest.x -= (int)(TotalWidth * Scale);
                        }

                        dest.x += x;

                        x += (int)(font.glyphRects[text[i]].w * Scale);

                        SDL.SDL_SetTextureColorMod(font.glyphs[text[i]], (byte)FontColor.x, (byte)FontColor.y, (byte)FontColor.z);
                        SDL.SDL_SetTextureAlphaMod(font.glyphs[text[i]], (byte)FontColor.w);

                        SDL.SDL_RenderCopy(Engine.Instance.renderer, font.glyphs[text[i]], ref src, ref dest);

                        SDL.SDL_SetTextureColorMod(font.glyphs[text[i]], 255, 255, 255);
                        SDL.SDL_SetTextureAlphaMod(font.glyphs[text[i]], 255);
                    }
                }
            }
        }

        public override void Update()
        {
            bool xHover = (Engine.Instance.mouse.MouseX > transform.Position.x && Engine.Instance.mouse.MouseX < transform.Position.x + TotalWidth * transform.Scale.x * Scale);
            bool yHover = (Engine.Instance.mouse.MouseY > transform.Position.y && Engine.Instance.mouse.MouseY < transform.Position.y + TotalHeight * transform.Scale.y * Scale);

            IsHovered = xHover && yHover;
        }
    }

    public enum TextAlign
    {
        Left,
        Right,
        Center
    }
}
