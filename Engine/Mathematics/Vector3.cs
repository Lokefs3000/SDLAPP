using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Math
{
    public class Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x = 0.0f, float y = 0.0f, float z = 0.0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 Zero()
        {
            return new Vector3();
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x + right.x, left.y + right.y, left.z + right.z);
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x - right.x, left.y - right.y, left.z - right.z);
        }

        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x * right.x, left.y * right.y, left.z * right.z);
        }

        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x / right.x, left.y / right.y, left.z / right.z);
        }
    }
}
