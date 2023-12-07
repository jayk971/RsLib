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
using System.ComponentModel;

namespace RsLib.PointCloudLib
{
    public class ICPMatch
    {
        public double RMS { get; private set; }= 0.0;
        public double Fitness { get; private set; } = 0.0;

        GeometryPointCloud Model = null;

        GeometryPointCloud AlignedTarget = null;
        public Matrix4x4 AlignMatrix { get; private set; }
        public ICPSetting Setting { get; set; } = new ICPSetting();
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
            Model.EstimateNormals(5.0);
        }
        public void SetModel(PointCloud ptCloud)
        {
            Model = ToGeometry(ptCloud);
            Model.EstimateNormals(5.0);
        }
        public bool Match(float[,]mechMindArrayX, float[,] mechMindArrayY, float[,] mechMindArrayZ)
        {
            GeometryPointCloud targetGeo = ToGeometry(mechMindArrayX, mechMindArrayY, mechMindArrayZ);
            return Match(targetGeo);

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
            targetGeo.EstimateNormals(5.0);

            Registration.Match_ColorPointCloud MatchICP = new Registration.Match_ColorPointCloud();
            MatchICP.autofinetue_fitness = Setting.autofinetue_fitness;
            MatchICP.autofinetue_rmse = Setting.autofinetue_rmse;
            MatchICP.autofinetune_enable = Setting.autofinetune_enable;
            MatchICP.autofinetune_max_corr_dist = Setting.autofinetune_max_corr_dist;
            MatchICP.autofinetune_max_iter_per_cycle = Setting.autofinetune_max_iter_per_cycle;

            MatchICP.max_corr_dist = Setting.max_corr_dist;
            MatchICP.max_cycle_timeout_ms = Setting.max_cycle_timeout_ms;
            MatchICP.max_iter_per_cycle = Setting.max_iter_per_cycle;
            MatchICP.max_rmse = Setting.max_rmse;
            MatchICP.max_rmse_diff = Setting.max_rmse_diff;
            MatchICP.min_fitness = Setting.min_fitness;



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
        private GeometryPointCloud ToGeometry(float[,] mechMindX, float[,] mechMindY, float[,] mechMindZ)
        {
            GeometryPointCloud pcloud;

            if (mechMindX == null || mechMindX.GetTotalLength() == 0)
            {
                return new GeometryPointCloud();
            }
            if(mechMindX.GetTotalLength() != mechMindY.GetTotalLength() || mechMindX.GetTotalLength() != mechMindZ.GetTotalLength())
            {
                return new GeometryPointCloud();
            }
            List<int> R = new List<int>();
            List<int> G = new List<int>();
            List<int> B = new List<int>();
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            List<double> Z = new List<double>();

            for (int i = 0; i < mechMindX.GetLength(0); i++)
            {
                for (int j = 0; j < mechMindX.GetLength(1); j++)
                {

                    X.Add(mechMindX[i, j]);
                    Y.Add(mechMindY[i, j]);
                    Z.Add(mechMindZ[i, j]);

                    R.Add(255);
                    G.Add(255);
                    B.Add(255);
                }
            }

            pcloud = new GeometryPointCloud(X.ToArray(), Y.ToArray(), Z.ToArray(), R.ToArray(), G.ToArray(), B.ToArray());

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
    [Serializable]
    public class ICPSetting
    {
        [Category("Normal")]
        [DisplayName("Max RMSe")]
        public double max_rmse { get; set; } = 1.75;
        [Category("Normal")]
        [DisplayName("Max RMSe Difference")]
        public double max_rmse_diff { get; set; } = 0.0001;
        [Category("Normal")]
        [DisplayName("Min Fitness")]
        public double min_fitness { get; set; } = 0.7;
        [Category("Normal")]
        [DisplayName("Max Iteration Per Cycle")]
        public int max_iter_per_cycle { get; set; } = 30;
        [Category("Normal")]
        [DisplayName("Max Correction Distance")]
        public double max_corr_dist { get; set; } = 5.0;
        [Category("Normal")]
        [DisplayName("Max Cycle Timeout (ms)")]
        public int max_cycle_timeout_ms { get; set; } = 1000;
        [Category("Auto Finetune")]
        [DisplayName("Enable")]
        public bool autofinetune_enable { get; set; } = false;
        [Category("Auto Finetune")]
        [DisplayName("Fitenss")]
        public double autofinetue_fitness { get; set; } = 0.75;
        [Category("Auto Finetune")]
        [DisplayName("RMSe")]

        public double autofinetue_rmse { get; set; } = 1.8;
        [Category("Auto Finetune")]
        [DisplayName("Max Iteration Per Cycle")]

        public int autofinetune_max_iter_per_cycle { get; set; } = 30;
        [Category("Auto Finetune")]
        [DisplayName("Max Correction Distance")]

        public double autofinetune_max_corr_dist { get; set; } = 2.5;
    }
}
