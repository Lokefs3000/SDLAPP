using LonelyHill.Core;
using LonelyHill.Graphics;
using LonelyHill.Utlity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Layout
{
    public class UILayout
    {
        public Dictionary<string, Image> UiElementImages;
        public Dictionary<string, Texture> UiElementTextures;

        public UILayout(string path) {
            UiElementImages = new Dictionary<string, Image>();
            UiElementTextures = new Dictionary<string, Texture>();

            string[] code = File.ReadAllLines(path);

            int tWidth = 0;
            int tHeight = 0;
            string tPath = "";
            string tName = "";
            bool tCenter = false;
            bool tAutoSize = false;

            string generatingCurr = "";

            for (int i = 0; i < code.Length; i++)
            {
                string[] splt = code[i].Split(' ');
                
                if (splt[0] == "TEXTURE")
                {
                    generatingCurr = "texture";
                    tName = splt[1];
                }
                else if (splt[0] == "IMAGE")
                {
                    generatingCurr = "image";
                    tName = splt[1];
                }
                else if (splt[0] == "END")
                {
                    if (generatingCurr == "texture")
                    {
                        Texture texture = new Texture(tPath);
                        UiElementTextures.Add(tName, texture);
                    }
                    if (generatingCurr == "image")
                    {
                        Image image = new Image();
                        UiElementImages.Add(tName, image);
                        image.Width = tWidth;
                        image.Height = tHeight;
                        image.Autoscale = tAutoSize;
                        image.CenterAligned = tCenter;
                    }

                    generatingCurr = "";
                }
                else if (generatingCurr == "texture")
                {
                    if (splt[0] == "PATH")
                    {
                        tPath = FileSystem.GetSource() + "/" + splt[1];
                    }
                }
                else if (generatingCurr == "image")
                {
                    if (splt[0] == "CENTERED")
                    {
                        tCenter = bool.Parse(splt[1]);
                    }
                    if (splt[0] == "AUTOSCALE")
                    {
                        tAutoSize = bool.Parse(splt[1]);
                    }
                }
            }
        }

        public Image RetrieveImage(string key)
        {
            Console.WriteLine(key);
            Console.WriteLine(UiElementImages.Keys.ElementAt(0));
            return UiElementImages[key];
        }

        public Texture RetrieveTexture(string key)
        {
            return UiElementTextures[key];
        }

        public void Cleanup()
        {
            for (int i = 0; i < UiElementTextures.Count; i++)
            {
                UiElementTextures.Values.ElementAt(i).Cleanup();
            }

            UiElementImages.Clear();
            UiElementTextures.Clear();

            if (Engine.Instance.container.Layouts.Contains(this))
            {
                Engine.Instance.container.Layouts.Remove(this);
            }
        }
    }
}
