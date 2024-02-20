using MathNet.Numerics.LinearAlgebra.Factorization;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
namespace RsLib.PointCloudLib
{
    public class Mesh
    {
        public List<Point3D> Vertices { get; private set; } = new List<Point3D>();
        public List<int> Indices { get; private set; } = new List<int>();

        public void ExportMeshToPLY(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write header
                writer.WriteLine("ply");
                writer.WriteLine("format ascii 1.0");
                writer.WriteLine($"element vertex {Vertices.Count}");
                writer.WriteLine("property float x");
                writer.WriteLine("property float y");
                writer.WriteLine("property float z");
                writer.WriteLine($"element face {Vertices.Count / 3}");
                writer.WriteLine("property list uchar int vertex_index");
                writer.WriteLine("end_header");

                // Write vertices
                foreach (Point3D vertex in Vertices)
                {
                    writer.WriteLine($"{vertex.X} {vertex.Y} {vertex.Z}");
                }

                // Write faces (triangles)
                for (int i = 0; i < Indices.Count; i += 3)
                {
                    writer.WriteLine($"3 {Indices[i]} {Indices[i + 1]} {Indices[i + 2]}");
                }
            }
        }
        public void BuildMeshFromPointCloud(PointCloud pointCloud, float epsilon)
        {
            // Step 1: Find the convex hull of the point cloud
            PointCloud convexHull = FindConvexHull(pointCloud);

            // Step 2: Triangulate the convex hull
            Triangulate(convexHull, epsilon);
        }

        private void Triangulate(PointCloud convexHull, float epsilon)
        {
            // Iterate over each triangle in the convex hull
            for (int i = 0; i < convexHull.Count; i++)
            {
                Point3D p0 = convexHull.Points[i];
                Point3D p1 = convexHull.Points[(i + 1) % convexHull.Count];
                Point3D p2 = convexHull.Points[(i + 2) % convexHull.Count];
                Vector3D v1 = new Vector3D(p0, p1);
                Vector3D v2 = new Vector3D(p0, p2);
                // Calculate normal of the triangle
                Vector3D normal = Vector3D.Cross(v1,v2);

                // Find the farthest point from the plane of the triangle
                Point3D farthestPoint = FindFarthestPoint(convexHull, normal, p0, p1, p2);

                // If the farthest point is within epsilon distance from the plane, skip it
                Vector3D testV = new Vector3D(p0, farthestPoint);
                if (Vector3D.Dot(normal, testV) < epsilon)
                    continue;

                // Add vertices to the mesh
                // Add vertices to the mesh
                Vertices.Add(p0);
                Vertices.Add(p1);
                Vertices.Add(p2);

                // Add indices for the triangle
                Indices.Add(Vertices.Count - 3);
                Indices.Add(Vertices.Count - 2);
                Indices.Add(Vertices.Count - 1);
            }
        }

        private Point3D FindFarthestPoint(PointCloud pointCloud, Vector3D normal, Point3D p0, Point3D p1, Point3D p2)
        {
            float maxDistance = float.MinValue;
            Point3D farthestPoint = new Point3D();

            foreach (Point3D point in pointCloud.Points)
            {
                if (point == p0 || point == p1 || point == p2)
                    continue;

                Vector3D testV = new Vector3D(p0, point);
                float distance = (float)Vector3D.Dot(normal, testV);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    farthestPoint = point;
                }
            }

            return farthestPoint;
        }
        public PointCloud FindConvexHull(PointCloud cloud)
        {
            if (cloud.Count < 4)
                return cloud;

            List<Point3D> hull = new List<Point3D>();

            // Find leftmost and rightmost points
            int minIndex = 0, maxIndex = 0;
            for (int i = 1; i < cloud.Count; i++)
            {
                if (cloud.Points[i].X < cloud.Points[minIndex].X)
                    minIndex = i;
                if (cloud.Points[i].X > cloud.Points[maxIndex].X)
                    maxIndex = i;
            }

            Point3D A = cloud.Points[minIndex];
            Point3D B = cloud.Points[maxIndex];
            hull.Add(A);
            hull.Add(B);
            cloud.Points.RemoveAt(minIndex);
            cloud.Points.RemoveAt(maxIndex > minIndex ? maxIndex - 1 : maxIndex);

            List<Point3D> leftSet = new List<Point3D>();
            List<Point3D> rightSet = new List<Point3D>();

            // Split points into two sets
            for (int i = 0; i < cloud.Count; i++)
            {
                Point3D p = cloud.Points[i];
                if (PointLocation(A, B, p) == 1)
                    leftSet.Add(p);
                else if (PointLocation(A, B, p) == -1)
                    rightSet.Add(p);
            }

            // Recursively find the convex hull for each set
            HullSet(A, B, rightSet, hull);
            HullSet(B, A, leftSet, hull);

            PointCloud output = new PointCloud();
            output.Points.AddRange(hull);
            return output;
        }
        private void HullSet(Point3D A, Point3D B, List<Point3D> set, List<Point3D> hull)
        {
            if (set.Count == 0) return;

            // Find the point farthest from the line AB
            double maxDistance = double.MinValue;
            Point3D P = null;
            foreach (Point3D point in set)
            {
                Vector3D v1 = new Vector3D(A, B);
                Vector3D v2 = new Vector3D(A, point);
                double distance = Vector3D.Cross(v1, v2).L;
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    P = point;
                }
            }

            if (P == null) return;

            // Extract the farthest point from the set
            set.Remove(P);
            hull.Add(P);

            // Partition the set into points on the left and right of line AB
            List<Point3D> leftSet = new List<Point3D>();
            List<Point3D> rightSet = new List<Point3D>();
            foreach (Point3D point in set)
            {
                int location = PointLocation(A, P, point);
                if (location > 0)
                    leftSet.Add(point);
                else if (location < 0)
                    rightSet.Add(point);
            }

            // Recursively call HullSet for the subsets
            HullSet(A, P, leftSet, hull);
            HullSet(P, B, rightSet, hull);
        }
#if m
        private void HullSet(Point3D A, Point3D B, List<Point3D> set, List<Point3D> hull)
        {
            int insertPosition = hull.IndexOf(B);
            if (set.Count == 0) return;
            if (set.Count == 1)
            {
                Point3D p = set[0];
                set.RemoveAt(0);
                hull.Insert(insertPosition, p);
                return;
            }

            float dist = float.MinValue;
            int furthestPoint = -1;
            for (int i = 0; i < set.Count; i++)
            {
                Point3D p = set[i];
                float distance = SignedDistance(A, B, p);
                if (distance > dist)
                {
                    dist = distance;
                    furthestPoint = i;
                }
            }

            Point3D P = set[furthestPoint];
            set.RemoveAt(furthestPoint);
            hull.Insert(insertPosition, P);

            // Determine which side of AP the other points are on
            List<Point3D> leftSetAP = new List<Point3D>();
            for (int i = 0; i < set.Count; i++)
            {
                Point3D M = set[i];
                if (PointLocation(A, P, M) == 1)
                {
                    leftSetAP.Add(M);
                }
            }

            // Determine which side of PB the other points are on
            List<Point3D> leftSetPB = new List<Point3D>();
            for (int i = 0; i < set.Count; i++)
            {
                Point3D M = set[i];
                if (PointLocation(P, B, M) == 1)
                {
                    leftSetPB.Add(M);
                }
            }

            HullSet(A, P, leftSetAP, hull);
            HullSet(P, B, leftSetPB, hull);
        }
#endif
        private int PointLocation(Point3D A, Point3D B, Point3D P)
        {
            double val = (P.Y - A.Y) * (B.X - A.X) - (B.Y - A.Y) * (P.X - A.X);
            if (val > 0)
                return 1; // Left side
            else if (val < 0)
                return -1; // Right side
            else
                return 0; // On the line
        }

        private float SignedDistance(Point3D A, Point3D B, Point3D P)
        {
            Vector3D vB = new Vector3D(B.X, B.Y, B.Z);
            Vector3D vA = new Vector3D(A.X, A.Y, A.Z);
            Vector3D vP = new Vector3D(P.X, P.Y, P.Z);


            return (float)Vector3D.Cross(vB - vA, vP - vA).L;
        }
    }
}
