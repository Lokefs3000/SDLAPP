using LonelyHill.Core;
using LonelyHill.Graphics;
using LonelyHill.Math;
using LonelyHill.Utlity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LonelyHill.Layout
{
    public class UILayout
    {
        public Dictionary<string, Image> UiElementImages;
        public Dictionary<string, Texture> UiElementTextures;

        private string path = "";

        public UILayout(string path) {
            this.path = path;

            UiElementImages = new Dictionary<string, Image>();
            UiElementTextures = new Dictionary<string, Texture>();

            string[] code = File.ReadAllLines(path);

            int tWidth = 0;
            int tHeight = 0;
            string tPath = "";
            string tName = "";
            bool tCenter = false;
            bool tAutoSize = false;
            bool tAutoPos = false;
            string tTexture = "";
            Vector3 tScale = new Vector3(1, 1, 1);
            Vector3 tPosition = new Vector3();

            string generatingCurr = "";

            for (int i = 0; i < code.Length; i++)
            {
                string[] splt = code[i].Split(' ');
                
                if (splt[0] == "TEXTURE" && generatingCurr == "")
                {
                    generatingCurr = "texture";
                    tName = splt[1];
                }
                else if (splt[0] == "IMAGE" && generatingCurr == "")
                {
                    generatingCurr = "image";
                    tName = splt[1];
                }
                else if (splt[0] == "END")
                {
                    if (generatingCurr == "texture" && !UiElementTextures.ContainsKey(tName))
                    {
                        Texture texture = new Texture(tPath);
                        UiElementTextures.Add(tName, texture);
                        Thread.Sleep(10);
                    }
                    if (generatingCurr == "image")
                    {
                        Image image = new Image();
                        UiElementImages.Add(tName, image);
                        image.Width = tWidth;
                        image.Height = tHeight;
                        image.Autoscale = tAutoSize;
                        image.CenterAligned = tCenter;
                        image.transform.Scale = tScale;
                        image.transform.Position = tPosition;
                        image.AutoPosition = tAutoPos;

                        if (tTexture != "")
                        {
                            image.texture = UiElementTextures[tTexture];
                        }
                    }

                    generatingCurr = "";
                    tName = "";
                    tWidth = 0;
                    tHeight = 0;
                    tAutoSize = false;
                    tCenter = false;
                    tTexture = "";
                    tPath = "";
                    tScale = new Vector3(1, 1, 1);
                    tPosition = new Vector3();
                    tAutoPos = false;
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
                    if (splt[0] == "TEXTURE")
                    {
                        tTexture = splt[1];
                    }
                    if (splt[0] == "SCALE")
                    {
                        tScale = new Vector3(float.Parse(splt[1]), float.Parse(splt[2]), float.Parse(splt[3]));
                    }
                    if (splt[0] == "POSITION")
                    {
                        tPosition = new Vector3(float.Parse(splt[1]), float.Parse(splt[2]), float.Parse(splt[3]));
                    }
                    if (splt[0] == "AUTOPOSITION")
                    {
                        tAutoPos = bool.Parse(splt[1]);
                    }
                }
            }
        }

        public string GetPath()
        {
            return path;
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
