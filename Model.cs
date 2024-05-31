using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TinyRasterizer
{
    

    internal class Model
    {

        private List<Vector3> verts;
        private List<Face> faces;

        public List<Vector3> Verts { get { return verts ; } }
        public List<Face> Faces { get { return faces; } }

        public Model(string fileName)
        {
            verts = new List<Vector3>();            
            faces = new List<Face>();
            
            foreach (var line in File.ReadLines(fileName))
            {
                if (line.StartsWith("v "))
                {
                    var parts = line.Replace('.', ',').Split(' ');

                    Vector3 v = new Vector3(
                        Convert.ToSingle(parts[1]),
                        Convert.ToSingle(parts[2]),
                        Convert.ToSingle(parts[3]));

                    verts.Add(v);
                }
                else if (line.StartsWith("f "))
                {
                    var parts = line.Split(' ');

                    var f = new Face();

                    try { 

                        f.Vertex1 = Verts.ElementAt(Convert.ToInt32(parts[1].Split('/')[0])-1);
                        f.Vertex2 = Verts.ElementAt(Convert.ToInt32(parts[2].Split('/')[0])-1);
                        f.Vertex3 = Verts.ElementAt(Convert.ToInt32(parts[3].Split('/')[0])-1);


                        faces.Add(f);

                    }
                    catch (Exception ex) { }


                }
            }
        }
    }

    struct Face
    {
        public Vector3 Vertex1;
        public Vector3 Vertex2;
        public Vector3 Vertex3;
        //public float Normal;
        //public float Texture;
    }
}
