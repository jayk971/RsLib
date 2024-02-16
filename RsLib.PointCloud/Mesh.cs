using MathNet.Numerics.LinearAlgebra.Factorization;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RsLib.PointCloudLib
{
    public class Mesh
    {
        static void Main(PointCloud downsampleCloud)
        {


            // Compute the gradient field
            // Replace "ComputeGradientField" with your method to compute the gradient field
            Matrix<double> gradientField = ComputeGradientField(downsampleCloud,5.0);

            // Solve the Poisson equation
            // Replace "SolvePoissonEquation" with your method to solve the Poisson equation
            Matrix<double> phi = SolvePoissonEquation(gradientField);

            // Extract iso-surface
            // Replace "ExtractIsoSurface" with your method to extract the iso-surface
            Mesh mesh = ExtractIsoSurface(phi);

            // Export mesh
            ExportMesh(mesh, "output_mesh.obj");

            Console.WriteLine("Mesh generation completed.");
        }
        static Matrix<double>[,,] ComputeGradientField3D(Matrix<double>[,,] pointCloud)
        {
            int width = pointCloud.GetLength(0);
            int height = pointCloud.GetLength(1);
            int depth = pointCloud.GetLength(2);

            var gradientField = new Matrix<double>[width, height, depth];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        double dx = (pointCloud[Math.Min(x + 1, width - 1), y, z] - pointCloud[Math.Max(x - 1, 0), y, z]) / 2.0;
                        double dy = (pointCloud[x, Math.Min(y + 1, height - 1), z] - pointCloud[x, Math.Max(y - 1, 0), z]) / 2.0;
                        double dz = (pointCloud[x, y, Math.Min(z + 1, depth - 1)] - pointCloud[x, y, Math.Max(z - 1, 0)]) / 2.0;

                        gradientField[x, y, z] = Vector<double>.Build.DenseOfArray(new double[] { dx, dy, dz });
                    }
                }
            }

            return gradientField;
        }

        static Matrix<double> ComputeCovarianceMatrix3D(Matrix<double>[,,] gradientField, int x, int y, int z, int radius)
        {
            int dimension = 3;
            int count = 0;
            var mean = Vector<double>.Build.DenseOfArray(new double[dimension]);
            var covarianceMatrix = Matrix<double>.Build.Dense(dimension, dimension);

            for (int i = Math.Max(x - radius, 0); i <= Math.Min(x + radius, gradientField.GetLength(0) - 1); i++)
            {
                for (int j = Math.Max(y - radius, 0); j <= Math.Min(y + radius, gradientField.GetLength(1) - 1); j++)
                {
                    for (int k = Math.Max(z - radius, 0); k <= Math.Min(z + radius, gradientField.GetLength(2) - 1); k++)
                    {
                        mean += gradientField[i, j, k];
                        count++;
                    }
                }
            }

            mean /= count;

            for (int i = Math.Max(x - radius, 0); i <= Math.Min(x + radius, gradientField.GetLength(0) - 1); i++)
            {
                for (int j = Math.Max(y - radius, 0); j <= Math.Min(y + radius, gradientField.GetLength(1) - 1); j++)
                {
                    for (int k = Math.Max(z - radius, 0); k <= Math.Min(z + radius, gradientField.GetLength(2) - 1); k++)
                    {
                        var deviation = gradientField[i, j, k] - mean;
                        covarianceMatrix += deviation.OuterProduct(deviation);
                    }
                }
            }

            return covarianceMatrix / (count - 1); // Unbiased estimator
        }

        static Matrix<double>[,,] SolvePoissonEquation3D(Matrix<double>[,,] gradientField)
        {
            int width = gradientField.GetLength(0);
            int height = gradientField.GetLength(1);
            int depth = gradientField.GetLength(2);
            var potentialField = new Matrix<double>[width, height, depth];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        potentialField[x, y, z] = gradientField[x, y, z] * (-1.0);
                    }
                }
            }

            return potentialField;
        }

        static List<Triangle> ExtractIsoSurface3D(Matrix<double>[,,] scalarField, double isoValue)
        {
            List<Triangle> triangles = new List<Triangle>();

            for (int x = 0; x < scalarField.GetLength(0) - 1; x++)
            {
                for (int y = 0; y < scalarField.GetLength(1) - 1; y++)
                {
                    for (int z = 0; z < scalarField.GetLength(2) - 1; z++)
                    {
                        double v0 = scalarField[x, y, z][0];
                        double v1 = scalarField[x + 1, y, z][0];
                        double v2 = scalarField[x + 1, y + 1, z][0];
                        double v3 = scalarField[x, y + 1, z][0];
                        double v4 = scalarField[x, y, z + 1][0];
                        double v5 = scalarField[x + 1, y, z + 1][0];
                        double v6 = scalarField[x + 1, y + 1, z + 1][0];
                        double v7 = scalarField[x, y + 1, z + 1][0];

                        int cellConfig = GetCellConfiguration3D(v0, v1, v2, v3, v4, v5, v6, v7, isoValue);

                        foreach (var triangle in MarchingCubesTables.TriangulationTable[cellConfig])
                        {
                            triangles.Add(triangle);
                        }
                    }
                }
            }

            return triangles;
        }

        static int GetCellConfiguration3D(double v0, double v1, double v2, double v3, double v4, double v5, double v6, double v7, double isoValue)
        {
            int index = 0;
            if (v0 < isoValue) index |= 1;
            if (v1 < isoValue) index |= 2;
            if (v2 < isoValue) index |= 4;
            if (v3 < isoValue) index |= 8;
            if (v4 < isoValue) index |= 16;
            if (v5 < isoValue) index |= 32;
            if (v6 < isoValue) index |= 64;
            if (v7 < isoValue) index |= 128;
            return index;
        }
    }
}
