using Accord;
using Accord.Collections;
using Accord.Math;
using RsLib.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Open3DSharp;
namespace RsLib.PointCloudLib
{
    public class ICPMatch
    {
        public double RMS { get; private set; }= 0.0;
        public double Fitness { get; private set; } = 0.0;

        GeometryPointCloud Model = null;

        GeometryPointCloud AlignedTarget = null;
        public Matrix4x4 AlignMatrix { get; private set; }
        public ICPMatch()
        {

        }
        public void SaveModel(string filePath)
        {
            if (Model != null) Model.SaveToFileXYZ(filePath);
        }
        public void SaveAlignTarget(string filePath)
        {
            if(AlignedTarget!= null) AlignedTarget.SaveToFileXYZ(filePath);
        }
        public void SaveTransformMatrix(string filePath)
        {
            PointCloudCommon.SaveMatrix4x4(AlignMatrix, filePath,' ');
        }

        public PointCloud GetAlignedPointCloud()
        {
            if (AlignedTarget == null) return null;
            AlignedTarget.GetXYZ(out double[] x, out double[] y, out double[] z);
            return new PointCloud(x, y, z);
        }
        public Tuple<double[], double[], double[]> GetAlignedXYZArray()
        {
            if (AlignedTarget == null) return null;

            AlignedTarget.GetXYZ(out double[] x, out double[] y, out double[] z);
            return new Tuple<double[], double[], double[]>(x, y, z);
        }
        public void SetModel(string filePath)
        {
            Model = ToGeometry(filePath);
            Model.EstimateNormals();

        }
        public bool Match(string targetFilePath)
        {
            GeometryPointCloud targetGeo = ToGeometry(targetFilePath);
            return Match(targetGeo);
        }
        public bool Match(LayerPointCloud layerPtCloud)
        {
            GeometryPointCloud targetGeo = ToGeometry(layerPtCloud);
            return Match(targetGeo);
        }
        public bool Match(double[] targetX, double[] targetY, double[] targetZ)
        {
            GeometryPointCloud targetGeo = ToGeometry(targetX, targetY, targetZ);
            return Match(targetGeo);
        }
        public bool Match(PointCloud targetCloud)
        {
            GeometryPointCloud targetGeo = ToGeometry(targetCloud);

            return Match(targetGeo);
        }
        private bool Match(GeometryPointCloud targetGeo)
        {
            targetGeo.EstimateNormals();

            Registration.Match_ColorPointCloud MatchICP = new Registration.Match_ColorPointCloud();
            
            MatchICP.Match(targetGeo, Model);
            Extrinsic AlignMatirx = new Extrinsic(  MatchICP.result.transform);

            AlignedTarget = new GeometryPointCloud(targetGeo);
            AlignedTarget.Transform(AlignMatirx);

            Fitness = Math.Round(MatchICP.result.fitness, 2);
            RMS = Math.Round(MatchICP.result.rmse, 2);
            AlignMatrix = PointCloudCommon.ArrayToMatrix4x4(AlignMatirx.GetArray());
            if (Fitness < 0.99) return false;
            return true;
        }
        private GeometryPointCloud ToGeometry(string filePath)
        {
            GeometryPointCloud pcloud;

            PointCloudCommon.LoadXYZToArray(filePath, ' ', out double[] x, out double[] y, out double[] z);

            if (x == null || x.Length == 0)
            {
                pcloud = new GeometryPointCloud();
                return pcloud;
            }

            int[] r, g, b;

            List<int> R = new List<int>();
            List<int> G = new List<int>();
            List<int> B = new List<int>();

            for (int i = 0; i < x.Length; i++)
            {
                R.Add(Color.White.R);
                G.Add(Color.White.G);
                B.Add(Color.White.B);
            }

            r = R.ToArray();
            g = G.ToArray();
            b = B.ToArray();

            pcloud = new GeometryPointCloud(x, y, z, r, g, b);

            return pcloud;
        }
        private GeometryPointCloud ToGeometry(double[] x, double[] y, double[] z)
        {
            GeometryPointCloud pcloud;

            if (x == null || x.Length == 0)
            {
                pcloud = new GeometryPointCloud();
                return pcloud;
            }

            int[] r, g, b;

            List<int> R = new List<int>();
            List<int> G = new List<int>();
            List<int> B = new List<int>();

            for (int i = 0; i < x.Length; i++)
            {
                R.Add(Color.White.R);
                G.Add(Color.White.G);
                B.Add(Color.White.B);
            }

            r = R.ToArray();
            g = G.ToArray();
            b = B.ToArray();

            pcloud = new GeometryPointCloud(x, y, z, r, g, b);

            return pcloud;
        }
        private GeometryPointCloud ToGeometry(PointCloud ptCloud)
        {
            GeometryPointCloud pcloud;
            if (ptCloud.Points == null || ptCloud.Count == 0)
            {
                pcloud = new GeometryPointCloud();
                return pcloud;
            }

            double[] x, y, z;
            int[] r, g, b;
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            List<double> Z = new List<double>();

            List<int> R = new List<int>();
            List<int> G = new List<int>();
            List<int> B = new List<int>();

            for (int i = 0; i < ptCloud.Points.Count; i++)
            {
                X.Add(ptCloud.Points[i].X);
                Y.Add(ptCloud.Points[i].Y);
                Z.Add(ptCloud.Points[i].Z);
                R.Add(Color.White.R);
                G.Add(Color.White.G);
                B.Add(Color.White.B);
            }

            x = X.ToArray();
            y = Y.ToArray();
            z = Z.ToArray();

            r = R.ToArray();
            g = G.ToArray();
            b = B.ToArray();

            pcloud = new GeometryPointCloud(x, y, z, r, g, b);

            return pcloud;
        }
        private GeometryPointCloud ToGeometry(LayerPointCloud LCloud)
        {
            GeometryPointCloud pcloud;
            if (LCloud == null || LCloud.LayerCount == 0)
            {
                pcloud = new GeometryPointCloud();
                return pcloud;
            }

            double[] x, y, z;
            int[] r, g, b;
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            List<double> Z = new List<double>();

            List<int> R = new List<int>();
            List<int> G = new List<int>();
            List<int> B = new List<int>();

            double maxX = double.MinValue;
            double maxY = double.MinValue;
            double maxZ = double.MinValue;

            for (int i = 0; i < LCloud.LayerCount; i++)
            {
                for (int j = 0; j < LCloud.Layers[i].Count; j++)
                {
                    //if (i % 10 != 0) continue;
                    X.Add(LCloud.Layers[i].Points[j].X);
                    Y.Add(LCloud.Layers[i].Points[j].Y);
                    Z.Add(LCloud.Layers[i].Points[j].Z);
                    R.Add(Color.White.R);
                    G.Add(Color.White.G);
                    B.Add(Color.White.B);
                    if (maxX < LCloud.Layers[i].Points[j].X) maxX = LCloud.Layers[i].Points[j].X;
                    if (maxY < LCloud.Layers[i].Points[j].Y) maxY = LCloud.Layers[i].Points[j].Y;
                    if (maxZ < LCloud.Layers[i].Points[j].Z) maxZ = LCloud.Layers[i].Points[j].Z;

                }
            }
            x = X.ToArray();
            y = Y.ToArray();
            z = Z.ToArray();

            r = R.ToArray();
            g = G.ToArray();
            b = B.ToArray();
            pcloud = new GeometryPointCloud(x, y, z, r, g, b);
            return pcloud;
        }

    }
}
