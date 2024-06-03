using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace TinyRasterizer
{
    internal class Util
    {
        public static int float_to_int(float num)
        {
            //acredito que esse seja o comportamento da versao do artigo
            return Convert.ToInt32(MathF.Floor(num));
        }

        public static Raylib_cs.Color To_Raylib_Color(System.Drawing.Color color)
        {
            return new Raylib_cs.Color(color.R, color.G, color.B, color.A);
        }

        public static Vector2[] BoundingBox(Vector3[] triangle)
        {
            if (triangle.Length != 3) { throw new ArgumentException("Triangle must have 3 elements!"); }

            var res = new Vector2[4];

            float minX = float.MaxValue;
            float maxX = 0;
            float minY = float.MaxValue;
            float maxY = 0;

            foreach (var v in triangle)
            {
                if (v.X < minX) minX = v.X;
                if (v.Y < minY) minY = v.Y;

                if (v.X > maxX) maxX = v.X;
                if (v.Y > maxY) maxY = v.Y;
            }

            res[0] = new Vector2(minX, minY);
            res[1] = new Vector2(minX, maxY);
            res[2] = new Vector2(maxX, minY);
            res[3] = new Vector2(maxX, maxY);

            return res;
        }

        public static bool IsPointInside(Vector2 p, Vector3[] triangle)
        {
            var A = triangle[0];
            var B = triangle[1];
            var C = triangle[2];

            float denominator = ((B.Y - C.Y) * (A.X - C.X) + (C.X - B.X) * (A.Y - C.Y));
            float alpha = ((B.Y - C.Y) * (p.X - C.X) + (C.X - B.X) * (p.Y - C.Y)) / denominator;
            float beta = ((C.Y - A.Y) * (p.X - C.X) + (A.X - C.X) * (p.Y - C.Y)) / denominator;
            float gamma = 1.0f - alpha - beta;

            return alpha > 0 && beta > 0 && gamma > 0;
        }

        public static Vector3 CalculateTriangleNormal(Vector3[] triangle)
        {

            Vector3 n = Vector3.Cross(triangle[2] - triangle[0], triangle[1] - triangle[0]);
            return Vector3.Normalize(n);
        }

       

        public static Vector3[] OrderTriangleCounterClockwise(Vector3[] triangle)
        {
            if (triangle.Length != 3)
            {
                throw new ArgumentException("The input array must contain exactly 3 vertices.");
            }

            // Calculate the normal vector of the triangle
            Vector3 edge1 = triangle[1] - triangle[0];
            Vector3 edge2 = triangle[2] - triangle[0];
            Vector3 normal = Vector3.Cross(edge1, edge2);
            

            // Check if the vertices are already in counter-clockwise order
            Vector3 crossProduct = Vector3.Cross(edge1, edge2);
            if (Vector3.Dot(crossProduct, normal) >= 0)
            {
                return triangle; // The vertices are already counter-clockwise
            }
            else
            {
                // Swap two vertices to change to counter-clockwise
                Vector3 temp = triangle[1];
                triangle[1] = triangle[2];
                triangle[2] = temp;
                return triangle;
            }
        }
    }
}
