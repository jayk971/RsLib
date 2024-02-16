using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RsLib.PointCloudLib
{
    public class Triangle
    {
        public Point3D Vertex1 { get; set; }
        public Point3D Vertex2 { get; set; }
        public Point3D Vertex3 { get; set; }

        public Triangle(Point3D vertex1, Point3D vertex2, Point3D vertex3)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vertex3;
        }
    }
}
