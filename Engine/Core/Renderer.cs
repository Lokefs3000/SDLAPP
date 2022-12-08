using LonelyHill.Layout;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Core
{
    public class Renderer
    {
        public List<Renderable> Renderables;

        public void Remove(Renderable renderable)
        {
            if (Renderables.Contains(renderable))
            {
                Renderables.Remove(renderable);
            }
        }

        public Renderer()
        {
            Renderables = new List<Renderable>();
        }

        public void Render()
        {
            for (int i = 0; i < Renderables.Count; i++)
            {
                Renderables[i].Render();
                Renderables[i].Update();
            }

            for (int i = 0; i < Engine.Instance.container.Layouts.Count; i++)
            {
                UILayout layout = Engine.Instance.container.Layouts[i];

                for (int j = 0; j < layout.UiElementImages.Count; j++)
                {
                    layout.UiElementImages.Values.ElementAt(j).Update();
                    layout.UiElementImages.Values.ElementAt(j).Render();
                }
            }
        }
    }
}
