using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Math
{
    public class Vector4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Vector4(float x = 0.0f, float y = 0.0f, float z = 0.0f, float w = 0.0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Vector4 Zero()
        {
            return new Vector4();
        }

        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4(left.x + right.x, left.y + right.y, left.z + right.z, left.w + right.w);
        }

        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4(left.x - right.x, left.y - right.y, left.z - right.z, left.w - right.w);
        }

        public static Vector4 operator *(Vector4 left, Vector4 right)
        {
            return new Vector4(left.x * right.x, left.y * right.y, left.z * right.z, left.w * right.w);
        }

        public static Vector4 operator /(Vector4 left, Vector4 right)
        {
            return new Vector4(left.x / right.x, left.y / right.y, left.z / right.z, left.w / right.w);
        }
    }
}
