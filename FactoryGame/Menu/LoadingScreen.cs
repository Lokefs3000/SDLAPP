using LonelyHill.Audio;
using LonelyHill.Graphics;
using LonelyHill.Math;
using LonelyHill.UI;
using LonelyHill.Utlity;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryGame
{
    public class LoadingScreen
    {
        private Texture loading = null;
        private Image loadingImg = null;

        float yMove = 0.0f;

        public LoadingScreen()
        {
            loading = new Texture(FileSystem.GetSource() + "/content/ui/loading.png");
            loadingImg = new Image();
            loadingImg.texture = loading;
            loadingImg.SetTexture();
        }

        public void Primary()
        {
            float scaleY = Program.GetEngine().GetScaleVector().y;
            float scaleX = Program.GetEngine().GetScaleVector().x;
            Vector2 size = Program.GetEngine().GetSize();

            yMove += Program.GetEngine().Delta;

            if (!Program.GetEngine().rendererClass.Renderables.Contains(loadingImg))
            {
                Program.GetEngine().rendererClass.Renderables.Add(loadingImg);
            }

            float yOff = (float)System.Math.Abs(System.Math.Sin(yMove / 4.0f)) * -10.0f * scaleY;

            float aspect = Program.GetEngine().GetScaleAspect();
            loadingImg.transform.Scale = new Vector3(5.0f * aspect, 5.0f * aspect);
            loadingImg.transform.Position = new Vector3((size.x / 2.0f) - ((loadingImg.texture.textureSurface.w * loadingImg.transform.Scale.x) / 2.0f), (size.y / 2.0f) - ((loadingImg.texture.textureSurface.h * loadingImg.transform.Scale.y) / 2.0f) + yOff, 0.0f);
        }

        public void RemoveFromList()
        {
            Program.GetEngine().rendererClass.Remove(loadingImg);
        }

        public void Cleanup()
        {
            loading.Cleanup();
        }
    }
}
