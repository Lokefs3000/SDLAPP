using LonelyHill.Graphics;
using LonelyHill.Math;
using LonelyHill.UI;
using LonelyHill.Utlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FactoryGame.Menu
{
    public class SettingsMenu
    {
        private Text back;
        private Text audioVolume;
        private Text settings;

        private Image solid;

        public bool Visible = false;

        private string selectedItem = "back";

        public SettingsMenu()
        {
            back = new Text();
            back.font = Program.font;
            back.text = "Back";

            audioVolume = new Text();
            audioVolume.font = Program.font;
            audioVolume.text = "Volume: ";

            settings = new Text();
            settings.font = Program.font;
            settings.text = "Settings";

            solid = new Image();
            solid.texture = Program.pixel;
        }

        public void Primary()
        {
            if (!Visible)
                return;

            float scaleY = Program.GetEngine().GetScaleVector().y;

            Vector2 windowSize = Program.GetEngine().GetSize();
            solid.transform.Scale = new Vector3(windowSize.x, windowSize.y, 1.0f);
            solid.transform.Position = new Vector3(0.0f, 0.0f, 0.0f);
            solid.Color = new Vector4(0.0f, 0.0f, 0.0f, 180.0f);
            solid.Render();

            back.transform.Position = new Vector3(20.0f, Program.GetEngine().GetSize().y - (60.0f * scaleY));
            back.Scale = 1.6f * scaleY;
            back.Render();

            settings.transform.Position = new Vector3(20.0f, -6.0f * scaleY);
            settings.Scale = 1.6f * scaleY;
            settings.Render();

            audioVolume.transform.Position = new Vector3(20.0f, 30.0f * scaleY);
            audioVolume.Scale = 1.6f * scaleY;
            audioVolume.Render();

            solid.transform.Scale = new Vector3(13.0f, audioVolume.TotalHeight - 5.0f, 1.0f);
            solid.Render();

            int maxHover = -1;
            bool hoverEditable = true;

            for (int i = 0; i < 7; i++)
            {
                if (System.Math.Round(Program.GetEngine().GlobalAudioVolume) < (int)((float)i * Program.AudioConst))
                    solid.Color = new Vector4(100.0f, 100.0f, 100.0f, 100.0f);
                else {
                    solid.Color = new Vector4(255.0f, 255.0f, 255.0f, 255.0f);
                    
                    if (hoverEditable)
                    {
                        maxHover = i;
                    }
                }

                solid.transform.Position = new Vector3(20.0f + (audioVolume.TotalWidth * audioVolume.Scale + (i * (solid.transform.Scale.x + 7.0f))), 47.0f * scaleY);

                solid.Render();
                solid.Update();

                if (Program.GetEngine().mouse.LeftButtonDown)
                {
                    if (solid.IsHovered)
                    {
                        maxHover = i;
                        hoverEditable = false;
                    }
                }
            }

            if (System.Math.Round(Program.AudioConst * (float)maxHover) != System.Math.Round(Program.GetEngine().GlobalAudioVolume))
            {
                Program.buttonHoverAudio.Play();
            }

            Program.GetEngine().GlobalAudioVolume = Program.AudioConst * (float)maxHover;

            if (back.IsHovered)
            {
                if (selectedItem != "back")
                {
                    Program.buttonHoverAudio.Play();
                }

                selectedItem = "back";

                if (Program.GetEngine().mouse.LeftButtonDown)
                {
                    Visible = false;
                }
            }

            switch (selectedItem)
            {
                case "back":
                    back.text = ">Back";
                    break;
                default:
                    back.text = "Back";
                    break;
            }
        }

        public void Cleanup()
        {
            Program.GetEngine().rendererClass.Remove(back);
            back = null;

            Program.GetEngine().rendererClass.Remove(audioVolume);
            audioVolume = null;

            solid = null;
        }
    }
}
