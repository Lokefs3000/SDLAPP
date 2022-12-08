using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Layout
{
    public class LayoutContainer
    {
        public List<UILayout> Layouts = new List<UILayout>();

        public UILayout CreateUILayout(string path)
        {
            UILayout layout = new UILayout(path);
            Layouts.Add(layout);
            return layout;
        }

        public void Cleanup()
        {
            for (int i = 0; i < Layouts.Count; i++)
            {
                Layouts[i].Cleanup();
            }

           Layouts.Clear();
        }
    }
}
