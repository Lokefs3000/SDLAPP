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
    public class SlicedTextBox : TextBox
    {
        public SlicedImage slicedimage { get; private set; }

        public SlicedTextBox()
        {
            slicedimage = new SlicedImage();
        }

        public override void Update()
        {
            slicedimage.transform.Position = transform.Position;
            slicedimage.transform.Scale = transform.Scale;
            slicedimage.Width = Width;
            slicedimage.Height = Height;

            slicedimage.Update();
            
            if (slicedimage != null && slicedimage.slicedtexture != null)
            {
                if (slicedimage.IsHovered)
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

            base.Update();
        }

        public override void Render()
        {
            slicedimage.Render();
            primaryText.Render();
        }
    }
}
