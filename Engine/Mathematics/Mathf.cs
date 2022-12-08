using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Math
{
    public class Mathf
    {
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static float Clamp(float a, float max, float min)
        {
            return System.Math.Min(System.Math.Max(a, min), max);
        }
    }
}
