using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Math
{
    public class Vector2
    {
        public float x;
        public float y;

        public Vector2(float x = 0.0f, float y = 0.0f)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 Zero()
        {
            return new Vector2();
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x + right.x, left.y + right.y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x - right.x, left.y - right.y);
        }

        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x * right.x, left.y * right.y);
        }

        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x / right.x, left.y / right.y);
        }
    }
}
