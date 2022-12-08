using LonelyHill.Core;
using LonelyHill.Graphics;
using LonelyHill.Input;
using LonelyHill.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.UI
{
    public class TextBox : Renderable
    {
        public string text = "";
        public string templateText = "";

        public Text primaryText { get; private set; }
        public Image image { get; private set; }

        public Vector4 ImageColor = new Vector4(255.0f, 255.0f, 255.0f, 255.0f);
        public Vector4 TextColor = new Vector4(255.0f, 255.0f, 255.0f, 255.0f);
        public Vector4 TemplateTextColor = new Vector4(120.0f, 120.0f, 120.0f, 255.0f);

        public int Width = 0;
        public int Height = 0;

        public Transform transform = new Transform();

        private string lastLetter = "";

        public bool Focused = false;

        public TextBox()
        {
            primaryText = new Text();
            image = new Image();
        }

        public void SetFont(Font font)
        {
            primaryText.font = font;
        }

        public void SetTexture(Texture texture)
        {
            image.texture = texture;
        }

        public override void Update()
        {
            image.Color = ImageColor;
            image.Update();
            primaryText.Update();

            image.transform.Scale = transform.Scale;
            primaryText.transform.Scale = transform.Scale;

            image.Width = Width;
            image.Height = Height;

            string key = Engine.Instance.keyboard.GetKey();

            if (Focused)
            {
                if (lastLetter != key)
                {
                    if (key == "Backspace" && text.Length > 0 && lastLetter != "Backspace")
                    {
                        text = text.Substring(0, text.Length - 1);
                    }
                    else if (key == "Space")
                    {
                        text += " ";
                    }
                    else if((Font.letters.Contains(key) || key == "LeftShift" || key == "RightShift") && lastLetter != key)
                    {
                        if (key == "LeftShift" || key == "RightShift")
                        {
                            text += key;
                        }
                        else
                        {
                            text += key.ToLower();
                        }
                    }
                }
            }

            lastLetter = key;

            if (text.Length == 0)
            {
                primaryText.text = templateText;
                primaryText.FontColor = TemplateTextColor;
            }
            else
            {
                primaryText.text = text;
                primaryText.FontColor = TextColor;
            }

            image.transform.Position = transform.Position;
            primaryText.transform.Position = transform.Position + new Vector3(5.0f, Height / 2.0f - primaryText.CalculateTextHeight() / 2.0f - 3.5f, 0.0f);
        
            if (image != null && image.texture != null)
            {
                if (image.IsHovered)
                {
                    if (Engine.Instance.mouse.LeftButtonDown)
                        Focused = true;
                }
                else
                {
                    if (Engine.Instance.mouse.LeftButtonDown)
                        Focused = false;
                }
            }
        }

        public override void Render()
        {
            image.Render();
            primaryText.Render();
        }

        public void Cleanup()
        {
            
        }
    }
}
