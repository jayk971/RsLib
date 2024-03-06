using Accord.Collections;
using Accord.Math;
using Accord.Statistics.Analysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
namespace RsLib.PointCloudLib
{

    public enum CoordinateType : int
    {
        X = 0,
        Y,
        Z,
        Vx,
        Vy,
        Vz,
        Rx,
        Ry,
        Rz,
        Q1,
        Q2,
        Q3,
        Q4
    }
    public static class PointCloudCommon
    {

        private static Vector4 V1000 = new Vector4(1F, 0F, 0F, 0F);
        private static Vector4 V0100 = new Vector4(0F, 1F, 0F, 0F);
        private static Vector4 V0010 = new Vector4(0F, 0F, 1F, 0F);
        private static Vector4 V0001 = new Vector4(0F, 0F, 0F, 1F);

        public static Vector3D VoX = new Vector3D(1, 0, 0);
        public static Vector3D VoY = new Vector3D(0, 1, 0);
        public static Vector3D VoZ = new Vector3D(0, 0, 1);

        public static Point3D Po = new Point3D();
#if m
        public static PointV3D ProjectToSurface(double targetX,double targetY, double targetZ,KDTree<int> targetTree, double searchRadius = 5.0)
        {
            Point3D target = new Point3D(targetX, targetY, targetZ);
            PointV3D p = new PointV3D();
            PointCloud surfaceCloud = GetNearestPointCloud(targetTree, target, searchRadius);
            while (surfaceCloud.Count < 10)
            {
                searchRadius++;
                surfaceCloud = GetNearestPointCloud(targetTree, target, searchRadius);
                if (searchRadius >= 20) break;
            }
            if (surfaceCloud.Count > 10)
            {
                try
                {
                    PCA(surfaceCloud, out Vector3D vX, out Vector3D vY, out Vector3D vZ, out Point3D center);
                    RsPlane plane = new RsPlane(vZ, center);
                    Point3D projectP = plane.ProjectPOnPlane(target);
                    p.SetXYZ(projectP.X, projectP.Y, projectP.Z);
                    double dot = Vector3D.Dot(vZ, Vector3D.ZAxis);
                    if (dot < 0) vZ.Reverse();
                    p.Vz = vZ.GetUnitVector();
                    return p;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else return new PointV3D(target);
        }
#endif
        public static PointV3D ProjectToSurface(double targetX, double targetY, double targetZ, KDTree<int> targetTree,int searchCloudCount, double searchStartRadius,double searchEndRadius)
        {
            Point3D target = new Point3D(targetX, targetY, targetZ);
            PointV3D p = new PointV3D();
            PointCloud surfaceCloud = GetNearestPointCloud(targetTree, target, searchStartRadius);
            while (surfaceCloud.Count < searchCloudCount)
            {
                searchStartRadius++;
                surfaceCloud = GetNearestPointCloud(targetTree, target, searchStartRadius);
                if (searchStartRadius >= searchEndRadius) break;
            }
            if (surfaceCloud.Count >= searchCloudCount)
            {
                try
                {
                    PCA(surfaceCloud, out Vector3D vX, out Vector3D vY, out Vector3D vZ, out Point3D center);
                    RsPlane plane = new RsPlane(vZ, center);
                    Point3D projectP = plane.ProjectPOnPlane(target);
                    p.SetXYZ(projectP.X, projectP.Y, projectP.Z);
                    double dot = Vector3D.Dot(vZ, Vector3D.ZAxis);
                    if (dot < 0) vZ.Reverse();
                    p.Vz = vZ.GetUnitVector();
                    return p;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else return new PointV3D(target);
        }
        public static PointV3D ProjectToSurface(double targetX, double targetY, double targetZ, 
            double normalX,double normalY,double normalZ,
            KDTree<int> targetTree, 
            int searchCloudCount, double searchStartRadius, double searchEndRadius)
        {
            Point3D target = new Point3D(targetX, targetY, targetZ);
            PointV3D p = new PointV3D();
            PointCloud surfaceCloud = GetNearestPointCloud(targetTree, target, searchStartRadius);
            while (surfaceCloud.Count < searchCloudCount)
            {
                searchStartRadius++;
                surfaceCloud = GetNearestPointCloud(targetTree, target, searchStartRadius);
                if (searchStartRadius >= searchEndRadius) break;
            }
            if (surfaceCloud.Count >= searchCloudCount)
            {
                try
                {
                    PCA(surfaceCloud, out Vector3D vX, out Vector3D vY, out Vector3D vZ, out Point3D center);
                    Vector3D surfaceNormal = new Vector3D(normalX, normalY, normalZ);
                    if (surfaceNormal.L == 0) surfaceNormal = vZ;
                    RsPlane plane = new RsPlane(surfaceNormal, center);
                    Point3D projectP = plane.ProjectPOnPlane(target);
                    p.SetXYZ(projectP.X, projectP.Y, projectP.Z);
                    double dot = Vector3D.Dot(vZ, Vector3D.ZAxis);
                    if (dot < 0) vZ.Reverse();
                    p.Vz = vZ.GetUnitVector();
                    return p;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else return new PointV3D(target);
        }
        public static PointV3D ProjectToSurface(double targetX, double targetY, double targetZ,
            List<Vector3D> candidateVector,
            KDTree<int> targetTree,
            int searchCloudCount, double searchStartRadius, double searchEndRadius)
        {
            Point3D target = new Point3D(targetX, targetY, targetZ);
            PointV3D p = new PointV3D();
            PointCloud surfaceCloud = GetNearestPointCloud(targetTree, target, searchStartRadius);
            while (surfaceCloud.Count < searchCloudCount)
            {
                searchStartRadius++;
                surfaceCloud = GetNearestPointCloud(targetTree, target, searchStartRadius);
                if (searchStartRadius >= searchEndRadius) break;
            }
            if (surfaceCloud.Count >= searchCloudCount)
            {
                try
                {
                    PCA(surfaceCloud, out Vector3D vX, out Vector3D vY, out Vector3D vZ, out Point3D center);
                    RsPlane plane = new RsPlane(vZ, center);
                    double minDis = double.MaxValue;
                    for (int i = 0; i < candidateVector.Count; i++)
                    {
                        Point3D projectP = plane.ProjectPOnPlane(target,candidateVector[i]);
                        if(projectP != null)
                        {
                            double calDis = Point3D.Distance(target, projectP);
                            if(calDis <=minDis)
                            {
                                minDis = calDis;
                                p.SetXYZ(projectP.X, projectP.Y, projectP.Z);
                            }
                        }
                    }
                    double dot = Vector3D.Dot(vZ, Vector3D.ZAxis);
                    if (dot < 0) vZ.Reverse();
                    p.Vz = vZ.GetUnitVector();
                    return p;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else return new PointV3D(target);
        }
        public static Point3D ProjectToSurface(double targetX, double targetY, double targetZ,
List<Vector3D> candidateVector,
KDTree<int> targetTree,
int searchCloudCount, double searchStartRadius, double searchEndRadius, double searchRadiusStep,
double searchLength, int searchLengthSplitRange)
        {
            if (searchLengthSplitRange <= 0) searchLengthSplitRange = 1;
            if (searchRadiusStep <= 0) searchRadiusStep = 0.1;
            double minDis = double.MaxValue;
            Point3D p = new Point3D(targetX, targetY, targetZ);
            bool foundCloudCountOK = false;
            for (int i = 0; i < candidateVector.Count; i++)
            {
                foundCloudCountOK = false;
                minDis = double.MaxValue;
                while (foundCloudCountOK == false)
                {
                    if (searchStartRadius >= searchEndRadius) break;
                    for (int j = 0; j < searchLengthSplitRange; j++)
                    {
                        Vector3D targetV = candidateVector[i].GetUnitVector();
                        Point3D target = new Point3D(targetX, targetY, targetZ);
                        Point3D foundTarget = target + targetV * j * (searchLength / searchLengthSplitRange);
                        PointCloud surfaceCloud = GetNearestPointCloud(targetTree, foundTarget, searchStartRadius);

                        if (surfaceCloud.Count >= searchCloudCount)
                        {
                            PCA(surfaceCloud, out Vector3D vX, out Vector3D vY, out Vector3D vZ, out Point3D center);
                            RsPlane plane = new RsPlane(vZ, center);
                            Point3D projectP = plane.ProjectPOnPlane(target, targetV);
                            if(Point3D.Distance(projectP, target) < minDis)
                            {
                                p.SetXYZ(projectP.X, projectP.Y, projectP.Z);
                                minDis = Point3D.Distance(projectP, target);
                            }
                            foundCloudCountOK = true;
                        }
                    }
                    searchStartRadius += searchRadiusStep;

                }
            }
            return p;
        }
        public static Point3D ProjectToNearCloud(double targetX, double targetY, double targetZ,
    List<Vector3D> candidateVector,
    KDTree<int> targetTree,
    int searchCloudCount, double searchStartRadius, double searchEndRadius,double searchRadiusStep,
    double searchLength, int searchLengthSplitRange)
        {
            if (searchLengthSplitRange <= 0) searchLengthSplitRange = 1;
            if (searchRadiusStep <= 0) searchRadiusStep = 0.1;
            double minDis = double.MaxValue;
            Point3D p = new Point3D(targetX, targetY, targetZ);
            for (int i = 0; i < candidateVector.Count; i++)
            {
                for (int j = 0; j < searchLengthSplitRange; j++)
                {
                    Vector3D targetV = candidateVector[i].GetUnitVector();
                    Point3D target = new Point3D(targetX, targetY, targetZ);
                    Point3D foundTarget = target + targetV * j * (searchLength / searchLengthSplitRange);
                    PointCloud surfaceCloud = GetNearestPointCloud(targetTree, foundTarget, searchStartRadius);
                    while (surfaceCloud.Count < searchCloudCount)
                    {
                        searchStartRadius+= searchRadiusStep;
                        surfaceCloud = GetNearestPointCloud(targetTree, target, searchStartRadius);
                        if (searchStartRadius >= searchEndRadius) break;
                    }
                    if(surfaceCloud.Count >= searchCloudCount)
                    {
                        Point3D avgP = surfaceCloud.Average;
                        double calD = Point3D.Distance(target, avgP);
                        if(calD <=minDis)
                        {
                            minDis = calD;
                            p = avgP;
                        }
                    }
                }
            }
            return p;
        }
        public static void SaveXYZArray(double[] xArr,double[] yArr,double[] zArr,string filePath)
        {
            bool arrayEqual = (xArr.Length == yArr.Length) & (xArr.Length == zArr.Length);
            if(arrayEqual)
            {
                using (StreamWriter sw = new StreamWriter(filePath,true,Encoding.Default,65535))
                {
                    for (int i = 0; i < xArr.Length; i++)
                    {
                        double x = xArr[i];
                        double y = yArr[i];
                        double z = zArr[i];
                        sw.WriteLine($"{x:F2} {y:F2} {z:F2}");
                    }
                }
            }
        }
        public static void LoadXYZToArray(string filePath,char cplitChar,out double[] xArr,out double[] yArr,out double[] zArr)
        {
            List<double> xList = new List<double>();
            List<double> yList = new List<double>();
            List<double> zList = new List<double>();

            xArr = new double[0];
            yArr = new double[0];
            zArr = new double[0];


            using (StreamReader sr = new StreamReader(filePath))
            {
                while(!sr.EndOfStream)
                {
                    string readData = sr.ReadLine();
                    string[] splitData = readData.Split(cplitChar);
                    if(splitData.Length>=3)
                    {
                        if (double.TryParse(splitData[0], out double x) == false) continue;
                        if (double.TryParse(splitData[1], out double y) == false) continue;
                        if (double.TryParse(splitData[2], out double z) == false) continue;

                        xList.Add(x);
                        yList.Add(y);
                        zList.Add(z);
                    }
                }
            }

            xArr = xList.ToArray();
            yArr = yList.ToArray();
            zArr = zList.ToArray();
        }
        public static void LoadXYZToArray(string filePath, char cplitChar, out double[] xArr, out double[] yArr, out double[] zArr,out KDTree<int> tree)
        {
            List<double> xList = new List<double>();
            List<double> yList = new List<double>();
            List<double> zList = new List<double>();

            xArr = new double[0];
            yArr = new double[0];
            zArr = new double[0];

            int count = 0;
            tree = new KDTree<int>(3);
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string readData = sr.ReadLine();
                    string[] splitData = readData.Split(cplitChar);
                    if (splitData.Length >= 3)
                    {
                        if (double.TryParse(splitData[0], out double x) == false) continue;
                        if (double.TryParse(splitData[1], out double y) == false) continue;
                        if (double.TryParse(splitData[2], out double z) == false) continue;

                        xList.Add(x);
                        yList.Add(y);
                        zList.Add(z);
                        tree.Add(new double[] { x, y, z }, count);

                        count++;
                    }
                }
            }

            xArr = xList.ToArray();
            yArr = yList.ToArray();
            zArr = zList.ToArray();
        }

        public static void LoadXYZToArray(string filePath, char cplitChar, int downSample,out double[] xArr, out double[] yArr, out double[] zArr)
        {
            List<double> xList = new List<double>();
            List<double> yList = new List<double>();
            List<double> zList = new List<double>();

            xArr = new double[0];
            yArr = new double[0];
            zArr = new double[0];
            int dSampleRate = downSample > 0? downSample : 1;

            using (StreamReader sr = new StreamReader(filePath))
            {
                int ptCount = 0;
                while (!sr.EndOfStream)
                {
                    string readData = sr.ReadLine();
                    string[] splitData = readData.Split(cplitChar);
                    if (splitData.Length >= 3)
                    {
                        if (double.TryParse(splitData[0], out double x) == false) continue;
                        if (double.TryParse(splitData[1], out double y) == false) continue;
                        if (double.TryParse(splitData[2], out double z) == false) continue;
                        ptCount++;
                        if (ptCount % dSampleRate != 0) continue;
                        xList.Add(x);
                        yList.Add(y);
                        zList.Add(z);
                        
                    }
                }
            }

            xArr = xList.ToArray();
            yArr = yList.ToArray();
            zArr = zList.ToArray();
        }

        public static void LoadOPTToXYZIndexNormalArray(string filePath, 
            char cplitChar, 
            out double[] xArr, 
            out double[] yArr, 
            out double[] zArr,
            out double[] nXArr,
            out double[] nYArr,
            out double[] nZArr,
            out int[] indexArr)
        {

            LoadOPTToXYZIndexNormalList(filePath, cplitChar,
                out List<double> x,
                out List<double> y,
                out List<double> z,
                out List<double> zX,
                out List<double> zY,
                out List<double> zZ,
                out List<int> index);

            xArr = x.ToArray();
            yArr = y.ToArray();
            zArr = z.ToArray();
            nXArr = zX.ToArray();
            nYArr = zY.ToArray();
            nZArr = zZ.ToArray();
            indexArr = index.ToArray();
        }
        public static void LoadOPTToXYZIndexNormalList(string filePath,
    char cplitChar,
    out List<double> xList,
    out List<double> yList,
    out List<double> zList,
    out List<double> nxList,
    out List<double> nyList,
    out List<double> nzList,
    out List<int> indexList)
        {
            xList = new List<double>();
            yList = new List<double>();
            zList = new List<double>();
            nxList = new List<double>();
            nyList = new List<double>();
            nzList = new List<double>();
            indexList = new List<int>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                int currIndex = 0;
                while (!sr.EndOfStream)
                {
                    string readData = sr.ReadLine();
                    if (readData == "") currIndex++;
                    string[] splitData = readData.Split(cplitChar);
                    if (splitData.Length >= 3)
                    {
                        if (double.TryParse(splitData[0], out double x) == false) continue;
                        if (double.TryParse(splitData[1], out double y) == false) continue;
                        if (double.TryParse(splitData[2], out double z) == false) continue;
                        if (double.TryParse(splitData[3], out double nX) == false) continue;
                        if (double.TryParse(splitData[4], out double nY) == false) continue;
                        if (double.TryParse(splitData[5], out double nZ) == false) continue;

                        xList.Add(x);
                        yList.Add(y);
                        zList.Add(z);
                        nxList.Add(nX);
                        nyList.Add(nY);
                        nzList.Add(nZ);
                        indexList.Add(currIndex);
                    }
                }
            }
        }
        public static void SaveOPTFromXYZIndexNormalArray(string filePath,
    double[] xArr,
    double[] yArr,
    double[] zArr,
    double[] nXArr,
    double[] nYArr,
    double[] nZArr,
    int[] indexArr,bool useThread = false)
        {
            if (xArr.Length != yArr.Length ||
                xArr.Length != zArr.Length ||
                xArr.Length != nXArr.Length ||
                xArr.Length != nYArr.Length ||
                xArr.Length != nZArr.Length ||
                xArr.Length != indexArr.Length) return;
            Tuple<double[], double[], double[], double[], double[], double[]> arrays = new Tuple<double[], double[], double[], double[], double[], double[]>(xArr, yArr, zArr, nXArr, nYArr, nZArr);
            Tuple<string, Tuple<double[], double[], double[], double[], double[], double[]>, int[]> finalTuple = new Tuple<string, Tuple<double[], double[], double[], double[], double[], double[]>, int[]>(filePath, arrays, indexArr);

            if (useThread)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(SaveOPTFromXYZIndexNormalArray), finalTuple);
            }
            else
            {
                SaveOPTFromXYZIndexNormalArray(finalTuple);
            }
        }
        private static void SaveOPTFromXYZIndexNormalArray(object obj)
        {

            Tuple<string,Tuple<double[], double[], double[], double[], double[], double[]>, int[]> tupleObj = (Tuple<string, Tuple<double[], double[], double[], double[], double[], double[]>, int[]>)obj;
            
            string filePath = tupleObj.Item1;

            double[] xArr = tupleObj.Item2.Item1;
            double[] yArr = tupleObj.Item2.Item2;
            double[] zArr = tupleObj.Item2.Item3;

            double[] nXArr = tupleObj.Item2.Item4;
            double[] nYArr = tupleObj.Item2.Item5;
            double[] nZArr = tupleObj.Item2.Item6;

            int[] indexArr = tupleObj.Item3;

            int id = 0;
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default, 65535))
            {
                for (int i = 0; i < xArr.Length; i++)
                {
                    if (id != indexArr[i])
                    {
                        id = indexArr[i];
                        sw.WriteLine("");
                    }
                    string writeLine = string.Format("{0:F2},{1:F2},{2:F2},{3:F3},{4:F3},{5:F3}",
                        xArr[i],
                        yArr[i],
                        zArr[i],
                        nXArr[i],
                        nYArr[i],
                        nZArr[i]);
                    sw.WriteLine(writeLine);
                }
            }

        }
        /// <summary>
        /// 使用 PCA 取得坐標系 3 個向量
        /// </summary>
        /// <param name="P">點</param>
        /// <param name="VX">坐標系 X 向量</param>
        /// <param name="VY">坐標系 Y 向量</param>
        /// <param name="VZ">坐標系 Z 向量</param>
        public static void PCA(List<double[]> P, out Vector3D VX, out Vector3D VY, out Vector3D VZ)
        {
            double[][] data = P.ToArray<double[]>();
            var method = PrincipalComponentMethod.Center;
            PrincipalComponentAnalysis pca = new PrincipalComponentAnalysis(method);

            pca.Learn(data);

            VX = new Vector3D(pca.ComponentVectors[0][0], pca.ComponentVectors[0][1], pca.ComponentVectors[0][2]);
            VY = new Vector3D(pca.ComponentVectors[1][0], pca.ComponentVectors[1][1], pca.ComponentVectors[1][2]);
            VZ = new Vector3D(pca.ComponentVectors[2][0], pca.ComponentVectors[2][1], pca.ComponentVectors[2][2]);
        }
        /// <summary>
        /// 使用 PCA 取得法向量
        /// </summary>
        /// <param name="Cloud">目標點雲</param>
        /// <returns>法向量</returns>
        public static Vector3D PCA(PointCloud Cloud)
        {
            List<double[]> temp = new List<double[]>();
            for (int i = 0; i < Cloud.Count; i++)
            {
                temp.Add(new double[] { Cloud.Points[i].X, Cloud.Points[i].Y, Cloud.Points[i].Z });
            }
            double[][] data = temp.ToArray<double[]>();
            var method = PrincipalComponentMethod.Center;
            PrincipalComponentAnalysis pca = new PrincipalComponentAnalysis(method);

            pca.Learn(data);

            double[] output1 = pca.ComponentVectors[2];
            return new Vector3D(output1[0], output1[1], output1[2]);
        }
        /// <summary>
        /// 使用 PCA 取得特徵值
        /// </summary>
        /// <param name="Cloud"></param>
        /// <returns>EigenValue,SingularityValue</returns>
        public static void PCA(PointCloud Cloud, out double[] eigenValue, out double[] singularityValue)
        {
            List<double[]> temp = new List<double[]>();
            for (int i = 0; i < Cloud.Count; i++)
            {
                temp.Add(new double[] { Cloud.Points[i].X, Cloud.Points[i].Y, Cloud.Points[i].Z });
            }
            double[][] data = temp.ToArray<double[]>();
            var method = PrincipalComponentMethod.Center;
            PrincipalComponentAnalysis pca = new PrincipalComponentAnalysis(method);

            pca.Learn(data);
            eigenValue = pca.Eigenvalues;
            singularityValue = pca.SingularValues;
        }

        /// <summary>
        /// 使用 PCA 取得 X Y Z 向量
        /// </summary>
        /// <param name="Cloud">目標點雲</param>
        /// <param name="VX">X 軸向量</param>
        /// <param name="VY">Y 軸向量</param>
        /// <param name="VZ">Z 軸向量</param>
        public static void PCA(PointCloud Cloud, out Vector3D VX, out Vector3D VY, out Vector3D VZ)
        {
            List<double[]> temp = new List<double[]>();
            for (int i = 0; i < Cloud.Count; i++)
            {
                temp.Add(new double[] { Cloud.Points[i].X, Cloud.Points[i].Y, Cloud.Points[i].Z });
            }
            double[][] data = temp.ToArray<double[]>();
            var method = PrincipalComponentMethod.Center;
            PrincipalComponentAnalysis pca = new PrincipalComponentAnalysis(method);

            pca.Learn(data);
            VX = new Vector3D(pca.ComponentVectors[0][0], pca.ComponentVectors[0][1], pca.ComponentVectors[0][2]);
            VY = new Vector3D(pca.ComponentVectors[1][0], pca.ComponentVectors[1][1], pca.ComponentVectors[1][2]);
            VZ = new Vector3D(pca.ComponentVectors[2][0], pca.ComponentVectors[2][1], pca.ComponentVectors[2][2]);

        }
        /// <summary>
        /// 使用 PCA 取得 X Y Z 向量 及中心點
        /// </summary>
        /// <param name="Cloud">目標點雲</param>
        /// <param name="VX">X 軸向量</param>
        /// <param name="VY">Y 軸向量</param>
        /// <param name="VZ">Z 軸向量</param>
        /// <param name="Center">中心點</param>
        public static void PCA(PointCloud Cloud, out Vector3D VX, out Vector3D VY, out Vector3D VZ, out Point3D Center)
        {
            List<double[]> temp = new List<double[]>();
            for (int i = 0; i < Cloud.Count; i++)
            {
                temp.Add(new double[] { Cloud.Points[i].X, Cloud.Points[i].Y, Cloud.Points[i].Z });
            }
            double[][] data = temp.ToArray<double[]>();
            var method = PrincipalComponentMethod.Center;
            PrincipalComponentAnalysis pca = new PrincipalComponentAnalysis(method);

            pca.Learn(data);

            Center = new Point3D(pca.Means[0], pca.Means[1], pca.Means[2]);
            VX = new Vector3D(pca.ComponentVectors[0][0], pca.ComponentVectors[0][1], pca.ComponentVectors[0][2]);
            VY = new Vector3D(pca.ComponentVectors[1][0], pca.ComponentVectors[1][1], pca.ComponentVectors[1][2]);
            VZ = new Vector3D(pca.ComponentVectors[2][0], pca.ComponentVectors[2][1], pca.ComponentVectors[2][2]);

        }
        private static PointCloud Shift(PointCloud Cloud, Matrix4x4 Shift)
        {
            PointCloud Output = new PointCloud();

            for (int i = 0; i < Cloud.Count; i++)
            {
                Vector4 P = new Vector4((float)Cloud.Points[i].X, (float)Cloud.Points[i].Y, (float)Cloud.Points[i].Z, 1);
                Vector4 AfterShift = Matrix4x4.Multiply(Shift, P);
                //Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, AfterShift);
                Point3D OutputP = new Point3D(AfterShift.X, AfterShift.Y, AfterShift.Z, Cloud.Points[i]);

                Output.Add(OutputP);
            }
            return Output;
        }
        /// <summary>
        /// 平移點雲
        /// </summary>
        /// <param name="Cloud">目標點雲</param>
        /// <param name="x">平移 X 距離</param>
        /// <param name="y">平移 Y 距離</param>
        /// <param name="z">平移 Z 距離</param>
        /// <returns>移動後點雲</returns>
        public static PointCloud Shift(PointCloud Cloud, double x, double y, double z)
        {
            Matrix4x4 Shift = GetShiftMatrix(x, y, z);
            PointCloud Output = new PointCloud();

            for (int i = 0; i < Cloud.Count; i++)
            {
                Vector4 P = new Vector4((float)Cloud.Points[i].X, (float)Cloud.Points[i].Y, (float)Cloud.Points[i].Z, 1);
                Vector4 AfterShift = Matrix4x4.Multiply(Shift, P);
                //Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, AfterShift);
                Point3D OutputP = new Point3D(AfterShift.X, AfterShift.Y, AfterShift.Z, Cloud.Points[i]);

                Output.Add(OutputP);
            }
            return Output;
        }
        private static Point3D Shift(Point3D Input, Matrix4x4 Shift)
        {

            Vector4 P = new Vector4((float)Input.X, (float)Input.Y, (float)Input.Z, 1);
            Vector4 AfterShift = Matrix4x4.Multiply(Shift, P);
            //Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, AfterShift);
            Point3D OutputP = new Point3D(AfterShift.X, AfterShift.Y, AfterShift.Z, Input);

            return OutputP;
        }
        /// <summary>
        /// 平移點資料
        /// </summary>
        /// <param name="Input">目標點位</param>
        /// <param name="x">平移 X 距離</param>
        /// <param name="y">平移 Y 距離</param>
        /// <param name="z">平移 Z 距離</param>
        /// <returns>移動後點位</returns>
        public static Point3D Shift(Point3D Input, double x, double y, double z)
        {
            Matrix4x4 Shift = GetShiftMatrix(x, y, z);
            Vector4 P = new Vector4((float)Input.X, (float)Input.Y, (float)Input.Z, 1);
            Vector4 AfterShift = Matrix4x4.Multiply(Shift, P);
            //Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, AfterShift);
            Point3D OutputP = new Point3D(AfterShift.X, AfterShift.Y, AfterShift.Z, Input);

            return OutputP;
        }

        private static PointCloud ShiftRotate(PointCloud Cloud, Matrix4x4 Shift, Matrix4x4 Rotate)
        {
            PointCloud Output = new PointCloud();

            for (int i = 0; i < Cloud.Count; i++)
            {
                Vector4 P = new Vector4((float)Cloud.Points[i].X, (float)Cloud.Points[i].Y, (float)Cloud.Points[i].Z, 1);
                Vector4 AfterShift = Matrix4x4.Multiply(Shift, P);
                Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, AfterShift);
                Point3D OutputP = new Point3D(AfterRotate.X, AfterRotate.Y, AfterRotate.Z, Cloud.Points[i]);

                Output.Add(OutputP);
            }
            return Output;
        }
        /// <summary>
        /// 點雲先平移再旋轉
        /// </summary>
        /// <param name="Cloud">目標點雲</param>
        /// <param name="x">平移 X 距離</param>
        /// <param name="y">平移 Y 距離</param>
        /// <param name="z">平移 Z 距離</param>
        /// <param name="Vx_Before">旋轉前 X 軸向量</param>
        /// <param name="Vy_Before">旋轉前 Y 軸向量</param>
        /// <param name="Vz_Before">旋轉前 Z 軸向量</param>
        /// <param name="Vx_After">旋轉後 X 軸向量</param>
        /// <param name="Vy_After">旋轉後 Y 軸向量</param>
        /// <param name="Vz_After">旋轉後 Z 軸向量</param>
        /// <returns>先平移再旋轉後點雲</returns>
        public static PointCloud ShiftRotate(PointCloud Cloud, double x, double y, double z, Vector3D Vx_Before, Vector3D Vy_Before, Vector3D Vz_Before, Vector3D Vx_After, Vector3D Vy_After, Vector3D Vz_After)
        {
            PointCloud Output = new PointCloud();
            Matrix4x4 Shift = GetShiftMatrix(x, y, z);
            Matrix4x4 Rotate = GetRotateMatrix(Vx_Before, Vy_Before, Vz_Before, Vx_After, Vy_After, Vz_After);
            for (int i = 0; i < Cloud.Count; i++)
            {
                Vector4 P = new Vector4((float)Cloud.Points[i].X, (float)Cloud.Points[i].Y, (float)Cloud.Points[i].Z, 1);
                Vector4 AfterShift = Matrix4x4.Multiply(Shift, P);
                Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, AfterShift);
                Point3D OutputP = new Point3D(AfterRotate.X, AfterRotate.Y, AfterRotate.Z, Cloud.Points[i]);

                Output.Add(OutputP);
            }
            return Output;
        }

        private static Point3D ShiftRotate(Point3D Input, Matrix4x4 Shift, Matrix4x4 Rotate)
        {

            Vector4 P = new Vector4((float)Input.X, (float)Input.Y, (float)Input.Z, 1);
            Vector4 AfterShift = Matrix4x4.Multiply(Shift, P);
            Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, AfterShift);
            Point3D OutputP = new Point3D(AfterRotate.X, AfterRotate.Y, AfterRotate.Z, Input);

            return OutputP;
        }
        /// <summary>
        /// 點位先平移再旋轉
        /// </summary>
        /// <param name="Input">目標點位</param>
        /// <param name="x">平移 X 距離</param>
        /// <param name="y">平移 Y 距離</param>
        /// <param name="z">平移 Z 距離</param>
        /// <param name="Vx_Before">旋轉前 X 軸向量</param>
        /// <param name="Vy_Before">旋轉前 Y 軸向量</param>
        /// <param name="Vz_Before">旋轉前 Z 軸向量</param>
        /// <param name="Vx_After">旋轉後 X 軸向量</param>
        /// <param name="Vy_After">旋轉後 Y 軸向量</param>
        /// <param name="Vz_After">旋轉後 Z 軸向量</param>
        /// <returns>先平移再旋轉後點位資料</returns>
        public static Point3D ShiftRotate(Point3D Input, double x, double y, double z, Vector3D Vx_Before, Vector3D Vy_Before, Vector3D Vz_Before, Vector3D Vx_After, Vector3D Vy_After, Vector3D Vz_After)
        {
            Matrix4x4 Shift = GetShiftMatrix(x, y, z);
            Matrix4x4 Rotate = GetRotateMatrix(Vx_Before, Vy_Before, Vz_Before, Vx_After, Vy_After, Vz_After);

            Vector4 P = new Vector4((float)Input.X, (float)Input.Y, (float)Input.Z, 1);
            Vector4 AfterShift = Matrix4x4.Multiply(Shift, P);
            Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, AfterShift);
            Point3D OutputP = new Point3D(AfterRotate.X, AfterRotate.Y, AfterRotate.Z, Input);

            return OutputP;
        }

        private static PointCloud Rotate(PointCloud Cloud, Matrix4x4 Rotate)
        {
            PointCloud Output = new PointCloud();

            for (int i = 0; i < Cloud.Count; i++)
            {
                Vector4 P = new Vector4((float)Cloud.Points[i].X, (float)Cloud.Points[i].Y, (float)Cloud.Points[i].Z, 1);
                Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, P);
                Point3D OutputP = new Point3D(AfterRotate.X, AfterRotate.Y, AfterRotate.Z, Cloud.Points[i]);

                Output.Add(OutputP);
            }
            return Output;
        }
        /// <summary>
        /// 旋轉點雲
        /// </summary>
        /// <param name="Cloud">目標點雲</param>
        /// <param name="Vx_Before">旋轉前 X 軸向量</param>
        /// <param name="Vy_Before">旋轉前 Y 軸向量</param>
        /// <param name="Vz_Before">旋轉前 Z 軸向量</param>
        /// <param name="Vx_After">旋轉後 X 軸向量</param>
        /// <param name="Vy_After">旋轉後 Y 軸向量</param>
        /// <param name="Vz_After">旋轉後 Z 軸向量</param>
        /// <returns>旋轉後點雲</returns>
        public static PointCloud Rotate(PointCloud Cloud, Vector3D Vx_Before, Vector3D Vy_Before, Vector3D Vz_Before, Vector3D Vx_After, Vector3D Vy_After, Vector3D Vz_After)
        {
            PointCloud Output = new PointCloud();
            Matrix4x4 Rotate = GetRotateMatrix(Vx_Before, Vy_Before, Vz_Before, Vx_After, Vy_After, Vz_After);

            for (int i = 0; i < Cloud.Count; i++)
            {
                Vector4 P = new Vector4((float)Cloud.Points[i].X, (float)Cloud.Points[i].Y, (float)Cloud.Points[i].Z, 1);
                Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, P);
                Point3D OutputP = new Point3D(AfterRotate.X, AfterRotate.Y, AfterRotate.Z, Cloud.Points[i]);

                Output.Add(OutputP);
            }
            return Output;
        }
        /// <summary>
        /// 旋轉點雲
        /// </summary>
        /// <param name="Cloud">目標點雲</param>
        /// <param name="RefVec">旋轉軸</param>
        /// <param name="Angle">旋轉角度</param>
        /// <returns>旋轉後點雲</returns>
        public static PointCloud Rotate(PointCloud Cloud, Vector3D RefVec, double Angle)
        {
            PointCloud Output = new PointCloud();

            for (int i = 0; i < Cloud.Count; i++)
            {
                Point3D OutputP = Rotate(Cloud.Points[i], RefVec, Angle);

                Output.Add(OutputP);
            }
            return Output;
        }
        private static Point3D Rotate(Point3D Input, Matrix4x4 Rotate)
        {

            Vector4 P = new Vector4((float)Input.X, (float)Input.Y, (float)Input.Z, 1);
            Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, P);
            Point3D OutputP = new Point3D(AfterRotate.X, AfterRotate.Y, AfterRotate.Z, Input);


            return OutputP;
        }
        /// <summary>
        /// 旋轉點雲
        /// </summary>
        /// <param name="Input">目標點位</param>
        /// <param name="Vx_Before">旋轉前 X 軸向量</param>
        /// <param name="Vy_Before">旋轉前 Y 軸向量</param>
        /// <param name="Vz_Before">旋轉前 Z 軸向量</param>
        /// <param name="Vx_After">旋轉後 X 軸向量</param>
        /// <param name="Vy_After">旋轉後 Y 軸向量</param>
        /// <param name="Vz_After">旋轉後 Z 軸向量</param>
        /// <returns>旋轉後點位</returns>
        public static Point3D Rotate(Point3D Input, Vector3D Vx_Before, Vector3D Vy_Before, Vector3D Vz_Before, Vector3D Vx_After, Vector3D Vy_After, Vector3D Vz_After)
        {
            Matrix4x4 Rotate = GetRotateMatrix(Vx_Before, Vy_Before, Vz_Before, Vx_After, Vy_After, Vz_After);

            Vector4 P = new Vector4((float)Input.X, (float)Input.Y, (float)Input.Z, 1);
            Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, P);
            Point3D OutputP = new Point3D(AfterRotate.X, AfterRotate.Y, AfterRotate.Z, Input);


            return OutputP;
        }
        /// <summary>
        /// 旋轉點位
        /// </summary>
        /// <param name="Input">目標點位</param>
        /// <param name="RefVec">旋轉軸</param>
        /// <param name="Angle">旋轉角度</param>
        /// <returns>旋轉後點位</returns>
        public static Point3D Rotate(Point3D Input, Vector3D RefVec, double Angle)
        {

            Vector3D Target = new Vector3D(Input.X, Input.Y, Input.Z);
            Vector3D NewV = RodriguesRotateFormula(Target, RefVec, Angle);
            Point3D OutputP = new Point3D(NewV.X, NewV.Y, NewV.Z, Input);


            return OutputP;
        }
        /// <summary>
        /// 旋轉向量
        /// </summary>
        /// <param name="Input">目標向量</param>
        /// <param name="RefVec">旋轉軸</param>
        /// <param name="Angle">旋轉角度</param>
        /// <returns>旋轉後向量</returns>
        public static Vector3D Rotate(Vector3D Input, Vector3D RefVec, double Angle)
        {
            Vector3D NewV = RodriguesRotateFormula(Input, RefVec, Angle);
            return NewV;
        }
        private static PointCloud RotateShift(PointCloud Cloud, Matrix4x4 Shift, Matrix4x4 Rotate)
        {
            PointCloud Output = new PointCloud();

            for (int i = 0; i < Cloud.Count; i++)
            {
                Vector4 P = new Vector4((float)Cloud.Points[i].X, (float)Cloud.Points[i].Y, (float)Cloud.Points[i].Z, 1);
                Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, P);
                Vector4 AfterShift = Matrix4x4.Multiply(Shift, AfterRotate);
                Point3D OutputP = new Point3D(AfterShift.X, AfterShift.Y, AfterShift.Z, Cloud.Points[i]);

                Output.Add(OutputP);
            }
            return Output;
        }
        /// <summary>
        /// 點雲先旋轉再平移
        /// </summary>
        /// <param name="Cloud">目標點雲</param>
        /// <param name="x">平移 X 距離</param>
        /// <param name="y">平移 Y 距離</param>
        /// <param name="z">平移 Z 距離</param>
        /// <param name="Vx_Before">旋轉前 X 軸向量</param>
        /// <param name="Vy_Before">旋轉前 Y 軸向量</param>
        /// <param name="Vz_Before">旋轉前 Z 軸向量</param>
        /// <param name="Vx_After">旋轉後 X 軸向量</param>
        /// <param name="Vy_After">旋轉後 Y 軸向量</param>
        /// <param name="Vz_After">旋轉後 Z 軸向量</param>
        /// <returns>先旋轉再平移後點雲</returns>
        public static PointCloud RotateShift(PointCloud Cloud, double x, double y, double z, Vector3D Vx_Before, Vector3D Vy_Before, Vector3D Vz_Before, Vector3D Vx_After, Vector3D Vy_After, Vector3D Vz_After)
        {
            PointCloud Output = new PointCloud();
            Matrix4x4 Shift = GetShiftMatrix(x, y, z);
            Matrix4x4 Rotate = GetRotateMatrix(Vx_Before, Vy_Before, Vz_Before, Vx_After, Vy_After, Vz_After);

            for (int i = 0; i < Cloud.Count; i++)
            {
                Vector4 P = new Vector4((float)Cloud.Points[i].X, (float)Cloud.Points[i].Y, (float)Cloud.Points[i].Z, 1);
                Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, P);
                Vector4 AfterShift = Matrix4x4.Multiply(Shift, AfterRotate);
                Point3D OutputP = new Point3D(AfterShift.X, AfterShift.Y, AfterShift.Z, Cloud.Points[i]);

                Output.Add(OutputP);
            }
            return Output;
        }
        public static PointCloud RotateShift(PointCloud Cloud, double x, double y, double z, Vector3 Vx_Before, Vector3 Vy_Before, Vector3 Vz_Before, Vector3 Vx_After, Vector3 Vy_After, Vector3 Vz_After)
        {
            PointCloud Output = new PointCloud();
            Matrix4x4 Shift = GetShiftMatrix(x, y, z);
            Matrix4x4 Rotate = GetRotateMatrix(Vx_Before, Vy_Before, Vz_Before, Vx_After, Vy_After, Vz_After);

            for (int i = 0; i < Cloud.Count; i++)
            {
                Vector4 P = new Vector4((float)Cloud.Points[i].X, (float)Cloud.Points[i].Y, (float)Cloud.Points[i].Z, 1);
                Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, P);
                Vector4 AfterShift = Matrix4x4.Multiply(Shift, AfterRotate);
                Point3D OutputP = new Point3D(AfterShift.X, AfterShift.Y, AfterShift.Z, Cloud.Points[i]);

                Output.Add(OutputP);
            }
            return Output;
        }

        public static Matrix4x4 RotateShiftMatrix(double x, double y, double z, Vector3D Vx_Before, Vector3D Vy_Before, Vector3D Vz_Before, Vector3D Vx_After, Vector3D Vy_After, Vector3D Vz_After)
        {
            Matrix4x4 Shift = GetShiftMatrix(x, y, z);
            Matrix4x4 Rotate = GetRotateMatrix(Vx_Before, Vy_Before, Vz_Before, Vx_After, Vy_After, Vz_After);
            Matrix4x4 output = Matrix4x4.Multiply(Shift, Rotate);
            return output;
        }

        /// <summary>
        /// 點雲繞 Z 軸旋轉
        /// </summary>
        /// <param name="Cloud">目標點雲</param>
        /// <param name="Before">旋轉前參考點位</param>
        /// <param name="After">旋轉後參考點位</param>
        /// <param name="RotateAngle">旋轉角度</param>
        /// <returns>旋轉後點雲</returns>
        public static PointCloud RotateShiftZaxis(PointCloud Cloud, Point3D Before, Point3D After, double RotateAngle)
        {
            PointCloud Output = new PointCloud();

            Matrix4x4 Shift = GetShiftMatrix(Before, After);
            Vector3D vx = new Vector3D(Math.Cos(RotateAngle / 180 * Math.PI), Math.Sin(RotateAngle / 180 * Math.PI), 0.0);
            Vector3D vy = new Vector3D(Math.Cos((RotateAngle + 90) / 180 * Math.PI), Math.Sin((RotateAngle + 90) / 180 * Math.PI), 0.0);

            Matrix4x4 Rotate = GetRotateMatrix(Vector3D.XAxis, Vector3D.YAxis, Vector3D.ZAxis, vx, vy, Vector3D.ZAxis);
            for (int i = 0; i < Cloud.Count; i++)
            {
                Vector4 P = new Vector4((float)Cloud.Points[i].X, (float)Cloud.Points[i].Y, (float)Cloud.Points[i].Z, 1);
                Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, P);
                Vector4 AfterShift = Matrix4x4.Multiply(Shift, AfterRotate);
                Point3D OutputP = new Point3D(AfterShift.X, AfterShift.Y, AfterShift.Z, Cloud.Points[i]);

                Output.Add(OutputP);
            }
            return Output;
        }
        /// <summary>
        /// 點繞 Z 軸旋轉
        /// </summary>
        /// <param name="inPoint">目標點</param>
        /// <param name="Before">旋轉前參考點位</param>
        /// <param name="After">旋轉後參考點位</param>
        /// <param name="RotateAngle">旋轉角度</param>
        /// <returns>旋轉後點</returns>
        public static Point3D RotateShiftZaxis(Point3D inPoint, Point3D Before, Point3D After, double RotateAngle)
        {
            Matrix4x4 Shift = GetShiftMatrix(Before, After);
            Vector3D vx = new Vector3D(Math.Cos(RotateAngle / 180 * Math.PI), Math.Sin(RotateAngle / 180 * Math.PI), 0.0);
            Vector3D vy = new Vector3D(Math.Cos((RotateAngle + 90) / 180 * Math.PI), Math.Sin((RotateAngle + 90) / 180 * Math.PI), 0.0);
            Matrix4x4 Rotate = GetRotateMatrix(Vector3D.XAxis, Vector3D.YAxis, Vector3D.ZAxis, vx, vy, Vector3D.ZAxis);
            Vector4 P = new Vector4((float)inPoint.X, (float)inPoint.Y, (float)inPoint.Z, 1);
            Vector4 AfterRotate = Matrix4x4.Multiply(Rotate, P);
            Vector4 AfterShift = Matrix4x4.Multiply(Shift, AfterRotate);
            Point3D OutputP = new Point3D(AfterShift.X, AfterShift.Y, AfterShift.Z, inPoint);
            return OutputP;
        }

        private static Matrix4x4 GetShiftMatrix(Point3D Point_Before, Point3D Point_After)
        {
            Vector4 Center = new Vector4((float)(Point_After.X - Point_Before.X), (float)(Point_After.Y - Point_Before.Y), (float)(Point_After.Z - Point_Before.Z), 1F);

            Matrix4x4 Shift = Matrix4x4.CreateFromColumns(V1000, V0100, V0010, Center);

            return Shift;
        }
        private static Matrix4x4 GetShiftMatrix(double dx, double dy, double dz)
        {
            Vector4 Center = new Vector4((float)dx, (float)dy, (float)dz, 1F);

            Matrix4x4 Shift = Matrix4x4.CreateFromColumns(V1000, V0100, V0010, Center);

            return Shift;
        }
        private static Matrix4x4 GetRotateMatrix(Vector3D Vx_Before, Vector3D Vy_Before, Vector3D Vz_Before, Vector3D Vx_After, Vector3D Vy_After, Vector3D Vz_After)
        {
            Vector3D v3x_Before_unit = Vx_Before.GetUnitVector();
            Vector3D v3y_Before_unit = Vy_Before.GetUnitVector();
            Vector3D v3z_Before_unit = Vz_Before.GetUnitVector();

            Vector3 v3x_Before = new Vector3((float)v3x_Before_unit.X, (float)v3x_Before_unit.Y, (float)v3x_Before_unit.Z);
            Vector3 v3y_Before = new Vector3((float)v3y_Before_unit.X, (float)v3y_Before_unit.Y, (float)v3y_Before_unit.Z);
            Vector3 v3z_Before = new Vector3((float)v3z_Before_unit.X, (float)v3z_Before_unit.Y, (float)v3z_Before_unit.Z);

            Vector3D v3x_After_unit = Vx_After.GetUnitVector();
            Vector3D v3y_After_unit = Vy_After.GetUnitVector();
            Vector3D v3z_After_unit = Vz_After.GetUnitVector();


            Vector3 v3x_After = new Vector3((float)v3x_After_unit.X, (float)v3x_After_unit.Y, (float)v3x_After_unit.Z);
            Vector3 v3y_After = new Vector3((float)v3y_After_unit.X, (float)v3y_After_unit.Y, (float)v3y_After_unit.Z);
            Vector3 v3z_After = new Vector3((float)v3z_After_unit.X, (float)v3z_After_unit.Y, (float)v3z_After_unit.Z);

            Matrix3x3 Rotate_Before = Matrix3x3.CreateFromColumns(v3x_Before, v3y_Before, v3z_Before);
            Matrix3x3 Rotate_After = Matrix3x3.CreateFromColumns(v3x_After, v3y_After, v3z_After);

            Matrix3x3 T = Matrix3x3.Multiply(Rotate_After, Rotate_Before.Inverse());

            Matrix4x4 tt = Matrix4x4.CreateFromRotation(T);
            return Matrix4x4.CreateFromRotation(T);
        }
        private static Matrix4x4 GetRotateMatrix(Vector3 Vx_Before, Vector3 Vy_Before, Vector3 Vz_Before, Vector3 Vx_After, Vector3 Vy_After, Vector3 Vz_After)
        {
            Matrix3x3 Rotate_Before = Matrix3x3.CreateFromColumns(Vx_Before, Vy_Before, Vz_Before);
            Matrix3x3 Rotate_After = Matrix3x3.CreateFromColumns(Vx_After, Vy_After, Vz_After);

            Matrix3x3 T = Matrix3x3.Multiply(Rotate_After, Rotate_Before.Inverse());

            Matrix4x4 tt = Matrix4x4.CreateFromRotation(T);
            return Matrix4x4.CreateFromRotation(T);
        }
        private static Matrix4x4 MatrixTrans(Point3D Shift, bool Rev = false)
        {
            float X, Y, Z;
            if (Rev)
            {
                X = (float)-Shift.X;
                Y = (float)-Shift.Y;
                Z = (float)-Shift.Z;
            }
            else
            {
                X = (float)-Shift.X;
                Y = (float)-Shift.Y;
                Z = (float)-Shift.Z;
            }

            Vector4 C1 = new Vector4(1, 0, 0, X);
            Vector4 C2 = new Vector4(0, 1, 0, Y);
            Vector4 C3 = new Vector4(0, 0, 1, Z);
            Vector4 C4 = new Vector4(0, 0, 0, 1);
            return Matrix4x4.CreateFromColumns(C1, C2, C3, C4);
        }
        private static Matrix4x4 MatrixTrans(double X, double Y, double Z)
        {
            Vector4 C1 = new Vector4(1, 0, 0, (float)X);
            Vector4 C2 = new Vector4(0, 1, 0, (float)Y);
            Vector4 C3 = new Vector4(0, 0, 1, (float)Z);
            Vector4 C4 = new Vector4(0, 0, 0, 1);
            return Matrix4x4.CreateFromColumns(C1, C2, C3, C4);
        }
        private static Matrix4x4 MatrixRotate(Vector3D X, Vector3D Y, Vector3D Z)
        {
            Vector3 R1 = new Vector3((float)X.X, (float)X.Y, (float)X.Z);
            Vector3 R2 = new Vector3((float)Y.X, (float)Y.Y, (float)Y.Z);
            Vector3 R3 = new Vector3((float)Z.X, (float)Z.Y, (float)Z.Z);

            Matrix3x3 temp = Matrix3x3.CreateFromColumns(R1, R2, R3);

            return Matrix4x4.CreateFromRotation(temp);
        }
        private static Vector3D RodriguesRotateFormula(Vector3D RotateVec, Vector3D RefAxis, double Angle)
        {
            Vector3D Output = new Vector3D();

            double CosTheta = Math.Cos(Angle / 180 * Math.PI);
            double SinTheta = Math.Sin(Angle / 180 * Math.PI);

            RefAxis.UnitVector();

            Vector3D FirstCal = new Vector3D();

            FirstCal.X = RotateVec.X * CosTheta;
            FirstCal.Y = RotateVec.Y * CosTheta;
            FirstCal.Z = RotateVec.Z * CosTheta;

            Vector3D kv = Vector3D.Cross(RefAxis, RotateVec);
            kv.X = kv.X * SinTheta;
            kv.Y = kv.Y * SinTheta;
            kv.Z = kv.Z * SinTheta;

            Vector3D ThirdCal = new Vector3D();
            double DotValue = Vector3D.Dot(RefAxis, RotateVec);
            ThirdCal.X = DotValue * RefAxis.X * (1 - CosTheta);
            ThirdCal.Y = DotValue * RefAxis.Y * (1 - CosTheta);
            ThirdCal.Z = DotValue * RefAxis.Z * (1 - CosTheta);


            Output.X = FirstCal.X + kv.X + ThirdCal.X;
            Output.Y = FirstCal.Y + kv.Y + ThirdCal.Y;
            Output.Z = FirstCal.Z + kv.Z + ThirdCal.Z;


            return Output;
        }


        /// <summary>
        /// 取得最靠近目標點範圍的特定數量點雲
        /// </summary>
        /// <param name="InputTree">目標點雲的 KD Tree</param>
        /// <param name="TargetPoint">目標點</param>
        /// <param name="count">要取得多少最靠近目標點的資料</param>
        /// <returns>最靠近目標點的特定數量點雲</returns>
        public static PointCloud GetNearestPointCloud(KDTree<Point3D> InputTree, Point3D TargetPoint, int count)
        {
            List<NodeDistance<KDTreeNode<Point3D>>> temp = InputTree.Nearest(new double[] { TargetPoint.X, TargetPoint.Y, TargetPoint.Z }, count).ToList<NodeDistance<KDTreeNode<Point3D>>>();

            PointCloud Output = new PointCloud();
            for (int j = 0; j < temp.Count; j++)
            {
                Point3D P = new Point3D(temp[j].Node.Position[0], temp[j].Node.Position[1], temp[j].Node.Position[2]);
                Output.Add(P);
            }
            return Output;
        }

        public static PointCloud GetPointCloudWithXY(KDTree<Point3D> InputTree, 
            double x,
            double y,
            double startSearchRadius,
            double endSearchRadius,
            double startSearchZ,
            double endSearchZ,
            uint stepRadius = 10,
            uint stepZ = 10)
        {
            PointCloud Output = new PointCloud();
            double startRadius = startSearchRadius;
            double diffRadius = (endSearchRadius - startSearchRadius) / stepRadius;
            double startZ = startSearchZ;
            double diffZ = (endSearchZ - startSearchZ) / stepZ;
            if(startSearchRadius <=0 || endSearchRadius <=0)
            {
                throw new Exception("Search radius <=0.");
            }
            if(endSearchZ <= startSearchZ)
            {
                throw new Exception("End search z value <= start search z value.");
            }
            while (true)
            {
                List<NodeDistance<KDTreeNode<Point3D>>> temp = InputTree.Nearest(new double[] { x, y, startZ }, startRadius).ToList();

                if (temp.Count == 0)
                {
                    startZ += diffZ;
                    if(startZ >= endSearchZ)
                    {
                        if(startRadius >= endSearchRadius)
                        {
                            break;
                        }
                        else
                        {
                            startRadius += diffRadius;
                            startZ = startSearchZ;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < temp.Count; j++)
                    {
                        Point3D P = new Point3D(temp[j].Node.Position[0], temp[j].Node.Position[1], temp[j].Node.Position[2]);
                        Output.Add(P);
                    }
                    break;
                }
            }
            return Output;
        }
        /// <summary>
        /// 取得最靠近目標點半徑 Radius 的點雲
        /// </summary>
        /// <param name="InputTree">目標點雲的 KD Tree</param>
        /// <param name="TargetPoint">目標點</param>
        /// <param name="Radius">搜尋半徑</param>
        /// <returns>最靠近目標點半徑 Radius 的點雲</returns>
        public static PointCloud GetNearestPointCloud(KDTree<Point3D> InputTree, Point3D TargetPoint, double Radius)
        {
            List<NodeDistance<KDTreeNode<Point3D>>> temp = InputTree.Nearest(new double[] { TargetPoint.X, TargetPoint.Y, TargetPoint.Z }, Radius).ToList<NodeDistance<KDTreeNode<Point3D>>>();

            PointCloud Output = new PointCloud();
            for (int j = 0; j < temp.Count; j++)
            {
                Point3D P = new Point3D(temp[j].Node.Position[0], temp[j].Node.Position[1], temp[j].Node.Position[2]);
                Output.Add(P);
            }
            return Output;
        }
        /// <summary>
        /// 取得最靠近目標點半徑 Radius 的點雲
        /// </summary>
        /// <param name="InputTree">目標點雲的 KD Tree</param>
        /// <param name="TargetPoint">目標點</param>
        /// <param name="Radius">搜尋半徑</param>
        /// <returns>最靠近目標點半徑 Radius 的點雲</returns>
        public static PointCloud GetNearestPointCloud(KDTree<Point3D> InputTree, double[] TargetPoint, double Radius)
        {
            List<NodeDistance<KDTreeNode<Point3D>>> temp = InputTree.Nearest(TargetPoint, Radius).ToList<NodeDistance<KDTreeNode<Point3D>>>();

            PointCloud Output = new PointCloud();
            for (int j = 0; j < temp.Count; j++)
            {
                Point3D P = new Point3D(temp[j].Node.Position[0], temp[j].Node.Position[1], temp[j].Node.Position[2]);
                Output.Add(P);
            }
            return Output;
        }
        /// <summary>
        /// 取得最靠近目標點半徑 Radius 的點雲
        /// </summary>
        /// <param name="InputTree">目標點雲的 KD Tree</param>
        /// <param name="TargetPoint">目標點</param>
        /// <param name="Radius">搜尋半徑</param>
        /// <param name="PointIndex">最靠近目標點半徑 Radius 的點雲編號</param>
        /// <returns>最靠近目標點半徑 Radius 的點雲</returns>
        public static PointCloud GetNearestPointCloud(KDTree<int> InputTree, Point3D TargetPoint, double Radius, out List<int> PointIndex)
        {
            PointIndex = new List<int>();
            List<NodeDistance<KDTreeNode<int>>> temp = InputTree.Nearest(new double[] { TargetPoint.X, TargetPoint.Y, TargetPoint.Z }, Radius).ToList<NodeDistance<KDTreeNode<int>>>();

            PointCloud Output = new PointCloud();
            for (int j = 0; j < temp.Count; j++)
            {
                Point3D P = new Point3D(Math.Round(temp[j].Node.Position[0], 2),
                    Math.Round(temp[j].Node.Position[1], 2),
                    Math.Round(temp[j].Node.Position[2], 2));
                Output.Add(P);
                PointIndex.Add(temp[j].Node.Value);
            }
            return Output;
        }
        /// <summary>
        /// 取得最靠近目標點的特定數量點雲
        /// </summary>
        /// <param name="InputTree">目標點雲編號的 KD Tree</param>
        /// <param name="TargetPoint">目標點</param>
        /// <param name="count">搜尋數量</param>
        /// <param name="PointIndex">最靠近目標點半徑 Radius 的點雲編號</param>
        /// <returns>最靠近目標點的特定數量點雲</returns>
        public static PointCloud GetNearestPointCloud(KDTree<int> InputTree, Point3D TargetPoint, int count, out List<int> PointIndex)
        {
            PointIndex = new List<int>();
            List<NodeDistance<KDTreeNode<int>>> temp = InputTree.Nearest(new double[] { TargetPoint.X, TargetPoint.Y, TargetPoint.Z }, count).ToList<NodeDistance<KDTreeNode<int>>>();

            PointCloud Output = new PointCloud();
            for (int j = 0; j < temp.Count; j++)
            {
                Point3D P = new Point3D(temp[j].Node.Position[0], temp[j].Node.Position[1], temp[j].Node.Position[2]);
                Output.Add(P);
                PointIndex.Add(temp[j].Node.Value);
            }
            return Output;
        }
        /// <summary>
        /// 取得最靠近目標點的特定數量點雲
        /// </summary>
        /// <param name="InputTree">目標點雲編號的 KD Tree</param>
        /// <param name="TargetPoint">目標點</param>
        /// <param name="count">搜尋數量</param>
        /// <returns>最靠近目標點的特定數量點雲</returns>
        public static PointCloud GetNearestPointCloud(KDTree<int> InputTree, Point3D TargetPoint, int count)
        {
            List<NodeDistance<KDTreeNode<int>>> temp = InputTree.Nearest(new double[] { TargetPoint.X, TargetPoint.Y, TargetPoint.Z }, count).ToList<NodeDistance<KDTreeNode<int>>>();

            PointCloud Output = new PointCloud();
            for (int j = 0; j < temp.Count; j++)
            {
                Point3D P = new Point3D(temp[j].Node.Position[0], temp[j].Node.Position[1], temp[j].Node.Position[2]);
                Output.Add(P);
            }
            return Output;
        }
        /// <summary>
        /// 取得最靠近目標點半徑 Radius 的點雲
        /// </summary>
        /// <param name="InputTree">目標點雲編號的 KD Tree</param>
        /// <param name="TargetPoint">目標點</param>
        /// <param name="Radius">搜尋半徑</param>
        /// <returns>最靠近目標點半徑 Radius 的點雲</returns>
        public static PointCloud GetNearestPointCloud(KDTree<int> InputTree, Point3D TargetPoint, double Radius)
        {
            List<NodeDistance<KDTreeNode<int>>> temp = InputTree.Nearest(new double[] { TargetPoint.X, TargetPoint.Y, TargetPoint.Z }, Radius).ToList<NodeDistance<KDTreeNode<int>>>();

            PointCloud Output = new PointCloud();
            for (int j = 0; j < temp.Count; j++)
            {
                Point3D P = new Point3D(temp[j].Node.Position[0], temp[j].Node.Position[1], temp[j].Node.Position[2]);
                Output.Add(P);
            }
            return Output;
        }
        /// <summary>
        /// 取得目標點半徑 Radius 內所有點雲最遠點
        /// </summary>
        /// <param name="InputTree">目標點雲編號的樹</param>
        /// <param name="TargetPoint">目標點</param>
        /// <param name="Radius">搜尋半徑</param>
        /// <returns>最遠點</returns>
        public static Point3D GetFarestPoint(KDTree<int> InputTree, Point3D TargetPoint, double Radius)
        {
            List<NodeDistance<KDTreeNode<int>>> temp = InputTree.Nearest(new double[] { TargetPoint.X, TargetPoint.Y, TargetPoint.Z }, Radius).ToList<NodeDistance<KDTreeNode<int>>>();

            PointCloud Output = new PointCloud();
            int FarIndex = 0;
            double FarDis = double.MinValue;
            for (int j = 0; j < temp.Count; j++)
            {
                if (temp[j].Distance > FarDis)
                {
                    FarDis = temp[j].Distance;
                    FarIndex = j;
                }
            }
            Point3D P = new Point3D(temp[FarIndex].Node.Position[0], temp[FarIndex].Node.Position[1], temp[FarIndex].Node.Position[2]);
            return P;
        }
        /// <summary>
        /// 廢棄
        /// </summary>
        /// <param name="InputTree"></param>
        /// <param name="TargetPoint"></param>
        /// <param name="Radius"></param>
        /// <returns></returns>
        private static Point3D GetNearestPoint(KDTree<int> InputTree, Point3D TargetPoint, double Radius)
        {
            List<NodeDistance<KDTreeNode<int>>> temp = InputTree.Nearest(new double[] { TargetPoint.X, TargetPoint.Y, TargetPoint.Z }, Radius).ToList<NodeDistance<KDTreeNode<int>>>();

            PointCloud Output = new PointCloud();
            int MinIndex = 0;
            double MinDis = double.MaxValue;
            for (int j = 0; j < temp.Count; j++)
            {
                if (temp[j].Distance < MinDis)
                {
                    MinDis = temp[j].Distance;
                    MinIndex = j;
                }
            }
            Point3D P = new Point3D(temp[MinIndex].Node.Position[0], temp[MinIndex].Node.Position[1], temp[MinIndex].Node.Position[2]);
            return P;
        }
        /// <summary>
        /// 取得目標點雲內最靠近目標點的點位
        /// </summary>
        /// <param name="InputTree">目標點雲編號的樹</param>
        /// <param name="TargetPoint">目標點</param>
        /// <returns>最靠近目標點的點位</returns>
        public static Point3D GetNearestPoint(KDTree<int> InputTree, Point3D TargetPoint)
        {
            List<NodeDistance<KDTreeNode<int>>> temp = InputTree.Nearest(new double[] { TargetPoint.X, TargetPoint.Y, TargetPoint.Z }, 1).ToList<NodeDistance<KDTreeNode<int>>>();
            Point3D P = new Point3D(temp[0].Node.Position[0], temp[0].Node.Position[1], temp[0].Node.Position[2]);
            return P;
        }
        public static int GetInRadiusPoint(KDTree<int> InputTree, Point3D TargetPoint, double radius)
        {
            List<NodeDistance<KDTreeNode<int>>> temp = InputTree.Nearest(new double[] { TargetPoint.X, TargetPoint.Y, TargetPoint.Z }, radius).ToList<NodeDistance<KDTreeNode<int>>>();
            if (temp.Count == 0) return -1;
            else return temp[0].Node.Value;
        }

        public static Point3D GetNearestPoint(KDTree<int> InputTree, Point3D TargetPoint, out double MinDis)
        {
            MinDis = double.MinValue;
            List<NodeDistance<KDTreeNode<int>>> temp = InputTree.Nearest(new double[] { TargetPoint.X, TargetPoint.Y, TargetPoint.Z }, 1).ToList<NodeDistance<KDTreeNode<int>>>();
            MinDis = temp[0].Distance;
            Point3D P = new Point3D(temp[0].Node.Position[0], temp[0].Node.Position[1], temp[0].Node.Position[2]);
            return P;
        }
        public static int GetNearestPointIndex(KDTree<int> InputTree, Point3D TargetPoint, out double MinDis)
        {
            MinDis = double.MinValue;
            List<NodeDistance<KDTreeNode<int>>> temp = InputTree.Nearest(new double[] { TargetPoint.X, TargetPoint.Y, TargetPoint.Z }, 1).ToList<NodeDistance<KDTreeNode<int>>>();
            MinDis = temp[0].Distance;
            return temp[0].Node.Value;
        }
        /// <summary>
        /// 從文字 X Y Z 字串轉換點位資料
        /// </summary>
        /// <param name="str_x">X 資料字串</param>
        /// <param name="str_y">Y 資料字串</param>
        /// <param name="str_z">Z 資料字串</param>
        /// <returns>轉換後點位</returns>
        public static Point3D ParsePoint3D(string str_x, string str_y, string str_z)
        {
            double ParseX, ParseY, ParseZ;

            double.TryParse(str_x, out ParseX);
            double.TryParse(str_y, out ParseY);
            double.TryParse(str_z, out ParseZ);

            return new Point3D(ParseX, ParseY, ParseZ);

        }/// <summary>
         /// 根據版線判斷左右腳
         /// </summary>
         /// <param name="Edge">版線</param>
         /// <param name="IsBottomUnit">是否為底</param>
         /// <returns>True : 右腳 , False : 左腳</returns>
        public static bool IsRight(Polyline Edge, bool IsBottomUnit)
        {
            int TipIndex = 0;
            int HeelIndex = 0;
            double maxY = -9999;
            double minY = 9999;

            for (int i = 0; i < Edge.Count; i++)  // 多圈biteline裡的第 [i] 圈
            {
                if (Edge.Points[i].Y >= maxY)             // 第 [i] 圈 biteline 裡第 j 點 的 Y 座標
                {
                    maxY = Edge.Points[i].Y;
                    TipIndex = i;
                }
                if (Edge.Points[i].Y <= minY)
                {
                    minY = Edge.Points[i].Y;
                    HeelIndex = i;
                }
            }
            #region  判斷InputUnitVSingleLine左右腳

            double b = 0;
            double c = 0;

            b = (Edge.Points[TipIndex].X - Edge.Points[HeelIndex].X) / (Edge.Points[TipIndex].Y - Edge.Points[HeelIndex].Y);
            c = Edge.Points[TipIndex].X - b * Edge.Points[TipIndex].Y;

            // 取出中間前 2/4 點位, 平均距離
            double distanceBetweenPointAndLine;

            double firstHalfAverageDistance = 0;
            double firstHalfDistance = 0;
            int firstHalfPointNumber = 0;
            double firstHalfMinimumDistance = double.MaxValue;
            int firstHalfMinimumDistanceIndex = -1;

            double secondHalfAverageDistance = 0;
            double secondHalfDistance = 0;
            int secondHalfPointNumber = 0;
            double secondHalfMinimumDistance = double.MaxValue;
            int secondHalfMinimumDistanceIndex = -1;
            Vector3D RefV = new Vector3D(Edge.Points[HeelIndex], Edge.Points[TipIndex]);
            RefV.Z = 0;
            // 鞋子左半邊
            for (int i = 0; i < Edge.Count; i++)
            {
                if (i == HeelIndex || i == TipIndex) continue;



                if ((Edge.Points[i].Y > Edge.Points[HeelIndex].Y + (Edge.Points[TipIndex].Y - Edge.Points[HeelIndex].Y) / 4) && (Edge.Points[i].Y < Edge.Points[HeelIndex].Y + (Edge.Points[TipIndex].Y - Edge.Points[HeelIndex].Y) / 2))
                {
                    distanceBetweenPointAndLine = Math.Abs(-Edge.Points[i].X + b * Edge.Points[i].Y + c) / Math.Sqrt(1 + b * b);

                    Vector3D CalV = new Vector3D(Edge.Points[HeelIndex], Edge.Points[i]);
                    CalV.Z = 0;
                    Vector3D CrossV = Vector3D.Cross(RefV, CalV);
                    if (CrossV.Z > 0) // left 
                    {
                        firstHalfDistance = firstHalfDistance + distanceBetweenPointAndLine;
                        firstHalfPointNumber = firstHalfPointNumber + 1;
                        if (distanceBetweenPointAndLine < firstHalfMinimumDistance)
                        {
                            firstHalfMinimumDistance = distanceBetweenPointAndLine;
                            firstHalfMinimumDistanceIndex = i;
                        }
                    }
                    else // right
                    {
                        secondHalfDistance = secondHalfDistance + distanceBetweenPointAndLine;
                        secondHalfPointNumber = secondHalfPointNumber + 1;
                        if (distanceBetweenPointAndLine < secondHalfMinimumDistance)
                        {
                            secondHalfMinimumDistance = distanceBetweenPointAndLine;
                            secondHalfMinimumDistanceIndex = i;
                        }
                    }

                }
            }

            firstHalfAverageDistance = firstHalfDistance / firstHalfPointNumber;

            secondHalfAverageDistance = secondHalfDistance / secondHalfPointNumber;

            // end of 取出中間前 2/4 點位, 平均距離
            if (firstHalfAverageDistance > secondHalfAverageDistance)
            {
                if (IsBottomUnit) return false;
                else return true;
            }// right  
            else
            {
                if (IsBottomUnit) return true;
                else return false;
            }//left

            # endregion
        }
        /// <summary>
        /// 計算兩向量的夾角
        /// </summary>
        /// <param name="V1">向量 1</param>
        /// <param name="V2">向量 2</param>
        /// <returns>夾角</returns>
        public static double CalVectorAngle(Vector3D V1, Vector3D V2)
        {
            double DotV = Vector3D.Dot(V1, V2);
            Vector3D CrossV = Vector3D.Cross(V1, V2);
            //double L1 = V1.L;
            //double L2 = V2.L;

            double TanTheta = Math.Atan2(CrossV.L, DotV);
            //double Theta = Math.Acos(TanTheta);
            double Angle = Math.Round(TanTheta / Math.PI * 180, 1);
            return Angle;
        }
        public static Point3D GetIntersect(Point3D L1P1, Point3D L1P2, Point3D L2P1, Point3D L2P2)
        {
            Vector3D L1 = new Vector3D(L1P1, L1P2);
            Vector3D L2 = new Vector3D(L2P1, L2P2);

            double a = L1.X;
            double b = L1.Y;
            double c = L1.Z;

            double a_ = L2.X;
            double b_ = L2.Y;
            double c_ = L2.Z;

            double x0 = L1P1.X;
            double y0 = L1P1.Y;
            double z0 = L1P1.Z;

            double x0_ = L2P1.X;
            double y0_ = L2P1.Y;
            double z0_ = L2P1.Z;

            double A = b * a_ / a - b_;
            double B = (y0_ - y0) - b / a * (x0_ - x0);
            double t_ = B / A;
            double t = (x0_ + a_ * t_ - x0) / a;

            double x = x0 + a * t;
            double y = y0 + b * t;
            double z = z0 + c * t;
            double x_ = x0_ + a_ * t_;
            double y_ = y0_ + b_ * t_;
            double z_ = z0_ + c_ * t_;

            double z_avg = (z + z_) / 2.0;

            return new Point3D(x, y, z_avg);
        }
        public static double[,] CalculateTransformMatrix(Point3D x1,
            Point3D x2,
            Point3D y1,
            Point3D y2,
            Point3D afterP,
            Vector3D vx_,
            Vector3D vy_,
            Vector3D vz_)
        {
            Point3D i_P = GetIntersect(x1, x2, y1, y2);

            Vector3 vx_temp = new Vector3((float)(x2.X - i_P.X), (float)(x2.Y - i_P.Y), (float)(x2.Z - i_P.Z));
            vx_temp.Normalize();
            Vector3 vy = new Vector3((float)(y2.X - i_P.X), (float)(y2.Y - i_P.Y), (float)(y2.Z - i_P.Z));
            vy.Normalize();
            Vector3 vz = Vector3.Cross(vx_temp, vy);
            vz.Normalize();
            Vector3 vx = Vector3.Cross(vy, vz);
            vx.Normalize();
            //double dot = Vector3.Dot(vx, vy);
            //double dot1 = Vector3.Dot(vx, vz);

            //Vector3D shift = new Vector3D(i_P, afterP);

            RotateRigidBody local_rotate_image = new RotateRigidBody(vx, vy, vz);
            local_rotate_image.Name = "local_rotate_image";
            Shift local_shift_image = new Shift(i_P.X, i_P.Y, i_P.Z);
            local_shift_image.Name = "local_shift_image";
            RotateAxis local_rotate_robot = new RotateAxis(vx_, vy_, vz_);
            local_rotate_robot.Name = "local_rotate_robot";
            Shift local_shift_robot = new Shift(afterP.X, afterP.Y, afterP.Z);
            local_shift_robot.Name = "local_shift_robot";

            // 下面的矩陣是由後面往前乘
            Matrix4x4 final = local_shift_robot.FinalMatrix4 * local_rotate_robot.FinalMatrix4 * local_rotate_image.GetMatrixInverse() * local_shift_image.GetMatrixInverse();


            return Matrix4x4ToArray(final);
        }
        public static double[,] CalculateTransformMatrix(Point3D x1,
    Point3D x2,
    Point3D y1,
    Point3D y2,
    Point3D afterP,
    Vector3D vx_,
    Vector3D vy_,
    Vector3D vz_,
    ref Point3D IntersectedPoint)
        {
            IntersectedPoint = GetIntersect(x1, x2, y1, y2);
            Point3D i_P = IntersectedPoint;

            Vector3 vx_temp = new Vector3((float)(x2.X - i_P.X), (float)(x2.Y - i_P.Y), (float)(x2.Z - i_P.Z));
            vx_temp.Normalize();
            Vector3 vy = new Vector3((float)(y2.X - i_P.X), (float)(y2.Y - i_P.Y), (float)(y2.Z - i_P.Z));
            vy.Normalize();
            Vector3 vz = Vector3.Cross(vx_temp, vy);
            vz.Normalize();
            Vector3 vx = Vector3.Cross(vy, vz);
            vx.Normalize();
            //double dot = Vector3.Dot(vx, vy);
            //double dot1 = Vector3.Dot(vx, vz);

            //Vector3D shift = new Vector3D(i_P, afterP);

            RotateRigidBody local_rotate_image = new RotateRigidBody(vx, vy, vz);
            local_rotate_image.Name = "local_rotate_image";

            Shift local_shift_image = new Shift(i_P.X, i_P.Y, i_P.Z);
            local_shift_image.Name = "local_shift_image";
            RotateAxis local_rotate_robot = new RotateAxis(vx_, vy_, vz_);
            local_rotate_robot.Name = "local_rotate_robot";
            Shift local_shift_robot = new Shift(afterP.X, afterP.Y, afterP.Z);
            local_shift_robot.Name = "local_shift_robot";

            // 下面的矩陣是由後面往前乘
            Matrix4x4 final = local_shift_robot.FinalMatrix4 * local_rotate_robot.FinalMatrix4 * local_rotate_image.GetMatrixInverse() * local_shift_image.GetMatrixInverse();


            return Matrix4x4ToArray(final);
        }
        public static double[,] Matrix4x4ToArray(Matrix4x4 m)
        {
            double[,] arr = new double[4, 4];
            arr[0, 0] = m.V00;
            arr[0, 1] = m.V01;
            arr[0, 2] = m.V02;
            arr[0, 3] = m.V03;
            arr[1, 0] = m.V10;
            arr[1, 1] = m.V11;
            arr[1, 2] = m.V12;
            arr[1, 3] = m.V13;
            arr[2, 0] = m.V20;
            arr[2, 1] = m.V21;
            arr[2, 2] = m.V22;
            arr[2, 3] = m.V23;
            arr[3, 0] = m.V30;
            arr[3, 1] = m.V31;
            arr[3, 2] = m.V32;
            arr[3, 3] = m.V33;
            return arr;
        }
        public static float[,] Matrix4x4ToFloatArray(Matrix4x4 m)
        {
            float[,] arr = new float[4, 4];
            arr[0, 0] = m.V00;
            arr[0, 1] = m.V01;
            arr[0, 2] = m.V02;
            arr[0, 3] = m.V03;
            arr[1, 0] = m.V10;
            arr[1, 1] = m.V11;
            arr[1, 2] = m.V12;
            arr[1, 3] = m.V13;
            arr[2, 0] = m.V20;
            arr[2, 1] = m.V21;
            arr[2, 2] = m.V22;
            arr[2, 3] = m.V23;
            arr[3, 0] = m.V30;
            arr[3, 1] = m.V31;
            arr[3, 2] = m.V32;
            arr[3, 3] = m.V33;
            return arr;
        }
        public static string Matrix4x4ToString(Matrix4x4 m,char splitChar = ',')
        {
            string output = $"{m.V00:F3}{splitChar}{m.V01:F3}{splitChar}{m.V02:F3}{splitChar}{m.V03:F3}\n" +
                $"{m.V10:F3}{splitChar}{m.V11:F3}{splitChar}{m.V12:F3}{splitChar}{m.V13:F3}\n" +
                $"{m.V20:F3}{splitChar}{m.V21:F3}{splitChar}{m.V22:F3}{splitChar}{m.V23:F3}\n" +
                $"{m.V30:F3}{splitChar}{m.V31:F3}{splitChar}{m.V32:F3}{splitChar}{m.V33:F3}";
            return output;
        }
        public static Matrix4x4 ParseMatrix4x4(string s, char splitChar = ',')
        {
            Matrix4x4 m = Matrix4x4.Identity;
            string[] splitData1 = s.Split('\n');
            if(splitData1.Length==4)
            {
                string[] row0 = splitData1[0].Split(splitChar);
                string[] row1 = splitData1[1].Split(splitChar);
                string[] row2 = splitData1[2].Split(splitChar);
                string[] row3 = splitData1[3].Split(splitChar);

                if(row0.Length ==4 && row1.Length == 4 && row2.Length == 4 && row3.Length == 4)
                {
                    float.TryParse(row0[0], out float v00);
                    float.TryParse(row0[1], out float v01);
                    float.TryParse(row0[2], out float v02);
                    float.TryParse(row0[3], out float v03);

                    float.TryParse(row1[0], out float v10);
                    float.TryParse(row1[1], out float v11);
                    float.TryParse(row1[2], out float v12);
                    float.TryParse(row1[3], out float v13);

                    float.TryParse(row2[0], out float v20);
                    float.TryParse(row2[1], out float v21);
                    float.TryParse(row2[2], out float v22);
                    float.TryParse(row2[3], out float v23);

                    float.TryParse(row3[0], out float v30);
                    float.TryParse(row3[1], out float v31);
                    float.TryParse(row3[2], out float v32);
                    float.TryParse(row3[3], out float v33);

                    m.V00 = v00;
                    m.V01 = v01;
                    m.V02 = v02;
                    m.V03 = v03;

                    m.V10 = v10;
                    m.V11 = v11;
                    m.V12 = v12;
                    m.V13 = v13;

                    m.V20 = v20;
                    m.V21 = v21;
                    m.V22 = v22;
                    m.V23 = v23;

                    m.V30 = v30;
                    m.V31 = v31;
                    m.V32 = v32;
                    m.V33 = v33;


                }
            }
            return m;

        }
        public static Matrix4x4 ArrayToMatrix4x4(double[] matrix4x4)
        {
            if (matrix4x4.Length != 16 ) return Matrix4x4.Identity;
            else
            {
                Matrix4x4 output;
                output.V00 = (float)matrix4x4[0];
                output.V01 = (float)matrix4x4[4];
                output.V02 = (float)matrix4x4[8];
                output.V03 = (float)matrix4x4[12];

                output.V10 = (float)matrix4x4[1];
                output.V11 = (float)matrix4x4[5];
                output.V12 = (float)matrix4x4[9];
                output.V13 = (float)matrix4x4[13];

                output.V20 = (float)matrix4x4[2];
                output.V21 = (float)matrix4x4[6];
                output.V22 = (float)matrix4x4[10];
                output.V23 = (float)matrix4x4[14];

                output.V30 = (float)matrix4x4[3];
                output.V31 = (float)matrix4x4[7];
                output.V32 = (float)matrix4x4[11];
                output.V33 = (float)matrix4x4[15];
                return output;
            }

        }

        public static Matrix4x4 ArrayToMatrix4x4(double[,] matrix4x4)
        {
            if (matrix4x4.GetLength(0) != 4 || matrix4x4.GetLength(1) != 4) return Matrix4x4.Identity;
            else
            {
                Matrix4x4 output;
                output.V00 = (float)matrix4x4[0, 0];
                output.V01 = (float)matrix4x4[0, 1];
                output.V02 = (float)matrix4x4[0, 2];
                output.V03 = (float)matrix4x4[0, 3];

                output.V10 = (float)matrix4x4[1, 0];
                output.V11 = (float)matrix4x4[1, 1];
                output.V12 = (float)matrix4x4[1, 2];
                output.V13 = (float)matrix4x4[1, 3];

                output.V20 = (float)matrix4x4[2, 0];
                output.V21 = (float)matrix4x4[2, 1];
                output.V22 = (float)matrix4x4[2, 2];
                output.V23 = (float)matrix4x4[2, 3];

                output.V30 = (float)matrix4x4[3, 0];
                output.V31 = (float)matrix4x4[3, 1];
                output.V32 = (float)matrix4x4[3, 2];
                output.V33 = (float)matrix4x4[3, 3];
                return output;
            }

        }
        public static void SaveMatrix4x4(Matrix4x4 m, string filePath,char splitChar = ',')
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                sw.WriteLine($"{m.V00}{splitChar}{m.V01}{splitChar}{m.V02}{splitChar}{m.V03}");
                sw.WriteLine($"{m.V10}{splitChar}{m.V11}{splitChar}{m.V12}{splitChar}{m.V13}");
                sw.WriteLine($"{m.V20}{splitChar}{m.V21}{splitChar}{m.V22}{splitChar}{m.V23}");
                sw.WriteLine($"{m.V30}{splitChar}{m.V31}{splitChar}{m.V32}{splitChar}{m.V33}");
                sw.Flush();
            }
        }
        public static void SaveMatrix4x4(double[,] matrix4x4, string filePath, char splitChar)
        {
            if (matrix4x4.GetLength(0) != 4 || matrix4x4.GetLength(1) != 4) return;
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                sw.WriteLine($"{matrix4x4[0, 0]}{splitChar}{matrix4x4[0, 1]}{splitChar}{matrix4x4[0, 2]}{splitChar}{matrix4x4[0, 3]}");
                sw.WriteLine($"{matrix4x4[1, 0]}{splitChar}{matrix4x4[1, 1]}{splitChar}{matrix4x4[1, 2]}{splitChar}{matrix4x4[1, 3]}");
                sw.WriteLine($"{matrix4x4[2, 0]}{splitChar}{matrix4x4[2, 1]}{splitChar}{matrix4x4[2, 2]}{splitChar}{matrix4x4[2, 3]}");
                sw.WriteLine($"{matrix4x4[3, 0]}{splitChar}{matrix4x4[3, 1]}{splitChar}{matrix4x4[3, 2]}{splitChar}{matrix4x4[3, 3]}");
                sw.Flush();
            }
        }
        public static double[,] LoadMatrix4x4ArrayFromFile(string filePath)
        {
            double[,] output = new double[4, 4];
            if (File.Exists(filePath))
            {
                string ext = Path.GetExtension(filePath);
                using (StreamReader sr = new StreamReader(filePath))
                {
                    if (ext.ToUpper() == ".M44")
                    {
                        string readData = sr.ReadToEnd();
                        Matrix4x4 m = LoadMatrix4x4(readData);
                        output = Matrix4x4ToArray(m);
                    }
                    else if (ext.ToUpper() == ".M44D")
                    {
                        string row = "";
                        for (int i = 0; i < 4; i++)
                        {
                            row += (sr.ReadLine() + "\r\n");
                        }
                        Matrix4x4 m = LoadMatrix4x4(row);
                        output = Matrix4x4ToArray(m);

                    }
                    else
                    {
                        Matrix4x4 m = Matrix4x4.Identity;
                        output = Matrix4x4ToArray(m);

                    }
                }
            }
            else
            {
                Matrix4x4 m = Matrix4x4.Identity;
                output = Matrix4x4ToArray(m);
            }
            return output;

        }
        public static double[,] LoadMatrix4x4ArrayFromHalconDatFile(string filePath,out Tuple<double,double,double,double,double,double> shiftRotate)
        {
            double[,] output = new double[4, 4];
            double rx = 0, ry = 0, rz = 0, tx = 0, ty = 0, tz = 0;

            if (File.Exists(filePath))
            {
                string ext = Path.GetExtension(filePath);
                CoordMatrix coordMatrix = new CoordMatrix();
                using (StreamReader sr = new StreamReader(filePath))
                {
                    if (ext.ToUpper() == ".DAT")
                    {
                        while (!sr.EndOfStream)
                        {
                            string readData = sr.ReadLine();
                            if (readData == "") continue;
                            char firstChar = readData[0];
                            string[] splitData;
                            if (firstChar == 'r')
                            {
                                splitData = readData.Split(' ');
                                if (splitData.Length >= 4)
                                {
                                    rx = double.Parse(splitData[1]);
                                    ry = double.Parse(splitData[2]);
                                    rz = double.Parse(splitData[3]);
                                    RotateRigidBody r = new RotateRigidBody();
                                    r.AddRotateSeq(eRefAxis.Z, rz / 180.0 * Math.PI);
                                    r.AddRotateSeq(eRefAxis.Y, ry / 180.0 * Math.PI);
                                    r.AddRotateSeq(eRefAxis.X, rx / 180.0 * Math.PI);
                                    coordMatrix.AddSeq(r);
                                }
                            }
                            else if (firstChar == 't')
                            {
                                splitData = readData.Split(' ');
                                if (splitData.Length >= 4)
                                {
                                    tx = double.Parse(splitData[1]);
                                    ty = double.Parse(splitData[2]);
                                    tz = double.Parse(splitData[3]);
                                    Shift s = new Shift(tx, ty, tz);
                                    coordMatrix.AddSeq(s);
                                }

                            }
                            else continue;
                        }
                        coordMatrix.EndAddMatrix();
                        output = Matrix4x4ToArray(coordMatrix.FinalMatrix4);
                    }
                    else
                    {
                        Matrix4x4 m = Matrix4x4.Identity;
                        output = Matrix4x4ToArray(m);

                    }
                }
            }
            else
            {
                Matrix4x4 m = Matrix4x4.Identity;
                output = Matrix4x4ToArray(m);
            }
            shiftRotate = new Tuple<double, double, double, double, double, double>(tx, ty, tz, rx, ry, rz);

            return output;

        }
        public static void LoadMatrix4x4ArrayFromHalconDatFile(string filePath,out Matrix4x4 m, out Tuple<double, double, double, double, double, double> shiftRotate)
        {
            
            double rx = 0, ry = 0, rz = 0, tx = 0, ty = 0, tz = 0;
            m = Matrix4x4.Identity;
            if (File.Exists(filePath))
            {
                string ext = Path.GetExtension(filePath);
                CoordMatrix coordMatrix = new CoordMatrix();
                using (StreamReader sr = new StreamReader(filePath))
                {
                    if (ext.ToUpper() == ".DAT")
                    {
                        while (!sr.EndOfStream)
                        {
                            string readData = sr.ReadLine();
                            if (readData == "") continue;
                            char firstChar = readData[0];
                            string[] splitData;
                            if (firstChar == 'r')
                            {
                                splitData = readData.Split(' ');
                                if (splitData.Length >= 4)
                                {
                                    rx = double.Parse(splitData[1]);
                                    ry = double.Parse(splitData[2]);
                                    rz = double.Parse(splitData[3]);
                                    RotateRigidBody r = new RotateRigidBody();
                                    r.AddRotateSeq(eRefAxis.Z, rz / 180.0 *Math.PI);
                                    r.AddRotateSeq(eRefAxis.Y, ry / 180.0 * Math.PI);
                                    r.AddRotateSeq(eRefAxis.X, rx / 180.0 * Math.PI);
                                    coordMatrix.AddSeq(r);
                                }
                            }
                            else if (firstChar == 't')
                            {
                                splitData = readData.Split(' ');
                                if (splitData.Length >= 4)
                                {
                                    tx = double.Parse(splitData[1]);
                                    ty = double.Parse(splitData[2]);
                                    tz = double.Parse(splitData[3]);
                                    Shift s = new Shift(tx, ty, tz);
                                    coordMatrix.AddSeq(s);
                                }

                            }
                            else continue;
                        }
                        coordMatrix.EndAddMatrix();
                        m = coordMatrix.FinalMatrix4;
                    }
                }
            }
            else
            {

            }
            shiftRotate = new Tuple<double, double, double, double, double, double>(tx, ty, tz, rx, ry, rz);
        }

        public static double[,] LoadMatrix4x4ArrayFromHalconDatFile(string filePath)
        {
            double[,] output = new double[4, 4];
            if (File.Exists(filePath))
            {
                string ext = Path.GetExtension(filePath);
                CoordMatrix coordMatrix = new CoordMatrix();
                using (StreamReader sr = new StreamReader(filePath))
                {
                    if (ext.ToUpper() == ".DAT")
                    {
                        while(! sr.EndOfStream)
                        {
                            string readData = sr.ReadLine();
                            if (readData == "") continue;
                            char firstChar = readData[0];
                            string[] splitData;
                            if (firstChar == 'r')
                            {
                                splitData = readData.Split(' ');
                                if(splitData.Length>=4)
                                {
                                    double rx = double.Parse(splitData[1]);
                                    double ry = double.Parse(splitData[2]);
                                    double rz = double.Parse(splitData[3]);
                                    RotateRigidBody r = new RotateRigidBody();
                                    r.AddRotateSeq(eRefAxis.Z, rz / 180.0 * Math.PI);
                                    r.AddRotateSeq(eRefAxis.Y, ry / 180.0 * Math.PI);
                                    r.AddRotateSeq(eRefAxis.X, rx / 180.0 * Math.PI);
                                    coordMatrix.AddSeq(r);
                                }
                            }
                            else if (firstChar == 't')
                            {
                                splitData = readData.Split(' ');
                                if (splitData.Length >= 4)
                                {
                                    double tx = double.Parse(splitData[1]);
                                    double ty = double.Parse(splitData[2]);
                                    double tz = double.Parse(splitData[3]);
                                    Shift s = new Shift(tx, ty, tz);
                                    coordMatrix.AddSeq(s);
                                }

                            }
                            else continue;
                        }
                        coordMatrix.EndAddMatrix();
                        output = Matrix4x4ToArray(coordMatrix.FinalMatrix4);
                    }
                    else
                    {
                        Matrix4x4 m = Matrix4x4.Identity;
                        output = Matrix4x4ToArray(m);

                    }
                }
            }
            else
            {
                Matrix4x4 m = Matrix4x4.Identity;
                output = Matrix4x4ToArray(m);
            }
            return output;

        }
        public static double[,] LoadMatrix4x4ArrayFromHalconDatFile(string filePath,
            double compensate_dX,
            double compensate_dY,
            double compensate_dZ,
            double compensate_rX,
            double compensate_rY,
            double compensate_rZ)
        {
            double[,] output = new double[4, 4];
            if (File.Exists(filePath))
            {
                string ext = Path.GetExtension(filePath);
                CoordMatrix coordMatrix = new CoordMatrix();
                using (StreamReader sr = new StreamReader(filePath))
                {
                    if (ext.ToUpper() == ".DAT")
                    {
                        while (!sr.EndOfStream)
                        {
                            string readData = sr.ReadLine();
                            if (readData == "") continue;
                            char firstChar = readData[0];
                            string[] splitData;
                            if (firstChar == 'r')
                            {
                                splitData = readData.Split(' ');
                                if (splitData.Length >= 4)
                                {
                                    double rx = double.Parse(splitData[1]);
                                    double ry = double.Parse(splitData[2]);
                                    double rz = double.Parse(splitData[3]);
                                    RotateRigidBody r = new RotateRigidBody();
                                    r.AddRotateSeq(eRefAxis.Z, rz /180.0 *Math.PI);
                                    r.AddRotateSeq(eRefAxis.Y, ry / 180.0 * Math.PI);
                                    r.AddRotateSeq(eRefAxis.X, rx / 180.0 * Math.PI);
                                    coordMatrix.AddSeq(r);


                                }
                            }
                            else if (firstChar == 't')
                            {
                                splitData = readData.Split(' ');
                                if (splitData.Length >= 4)
                                {
                                    double tx = double.Parse(splitData[1]);
                                    double ty = double.Parse(splitData[2]);
                                    double tz = double.Parse(splitData[3]);
                                    Shift s = new Shift(tx, ty, tz);
                                    coordMatrix.AddSeq(s);


                                }

                            }
                            else continue;
                        }
                        RotateRigidBody r_Compensate = new RotateRigidBody();
                        r_Compensate.AddRotateSeq(eRefAxis.Z, compensate_rZ);
                        r_Compensate.AddRotateSeq(eRefAxis.Y, compensate_rY);
                        r_Compensate.AddRotateSeq(eRefAxis.X, compensate_rX);
                        coordMatrix.AddSeq(r_Compensate);

                        Shift s_Compensate = new Shift(compensate_dX, compensate_dY, compensate_dZ);
                        coordMatrix.AddSeq(s_Compensate);

                        coordMatrix.EndAddMatrix();
                        output = Matrix4x4ToArray(coordMatrix.FinalMatrix4);
                    }
                    else
                    {
                        Matrix4x4 m = Matrix4x4.Identity;
                        output = Matrix4x4ToArray(m);

                    }
                }
            }
            else
            {
                Matrix4x4 m = Matrix4x4.Identity;
                output = Matrix4x4ToArray(m);
            }
            return output;

        }
        public static Matrix4x4 LoadMatrix4x4FromFile(string filePath)
        {
            Matrix4x4 m;
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string readData = sr.ReadToEnd();
                    m = LoadMatrix4x4(readData);
                }
            }
            else m = Matrix4x4.Identity;
            return m;
        }
        public static Matrix4x4 LoadMatrix4x4(string data)
        {
            Matrix4x4 m = Matrix4x4.Identity;
            string[] rows = data.Split('\n');
            if (rows.Length >= 4)
            {
                string[] row0 = rows[0].Replace("\r", "").Split(',');
                string[] row1 = rows[1].Replace("\r", "").Split(',');
                string[] row2 = rows[2].Replace("\r", "").Split(',');
                string[] row3 = rows[3].Replace("\r", "").Split(',');

                if (row0.Length == 4 && row1.Length == 4 && row2.Length == 4 && row3.Length == 4)
                {
                    m.V00 = float.Parse(row0[0]);
                    m.V01 = float.Parse(row0[1]);
                    m.V02 = float.Parse(row0[2]);
                    m.V03 = float.Parse(row0[3]);

                    m.V10 = float.Parse(row1[0]);
                    m.V11 = float.Parse(row1[1]);
                    m.V12 = float.Parse(row1[2]);
                    m.V13 = float.Parse(row1[3]);

                    m.V20 = float.Parse(row2[0]);
                    m.V21 = float.Parse(row2[1]);
                    m.V22 = float.Parse(row2[2]);
                    m.V23 = float.Parse(row2[3]);

                    m.V30 = float.Parse(row3[0]);
                    m.V31 = float.Parse(row3[1]);
                    m.V32 = float.Parse(row3[2]);
                    m.V33 = float.Parse(row3[3]);
                }
            }
            return m;
        }
        public static string Matrix4x4ToString(double[,] matrix4x4, int digit = 2)
        {
            if (matrix4x4.GetLength(0) != 4 || matrix4x4.GetLength(1) != 4) return "";
            string output = "";
            output += $"{Math.Round(matrix4x4[0, 0], digit)}\t{Math.Round(matrix4x4[0, 1], digit)}\t{Math.Round(matrix4x4[0, 2], digit)}\t{Math.Round(matrix4x4[0, 3], digit)}\n";
            output += $"{Math.Round(matrix4x4[1, 0], digit)}\t{Math.Round(matrix4x4[1, 1], digit)}\t{Math.Round(matrix4x4[1, 2], digit)}\t{Math.Round(matrix4x4[1, 3], digit)}\n";
            output += $"{Math.Round(matrix4x4[2, 0], digit)}\t{Math.Round(matrix4x4[2, 1], digit)}\t{Math.Round(matrix4x4[2, 2], digit)}\t{Math.Round(matrix4x4[2, 3], digit)}\n";
            output += $"{Math.Round(matrix4x4[3, 0], digit)}\t{Math.Round(matrix4x4[3, 1], digit)}\t{Math.Round(matrix4x4[3, 2], digit)}\t{Math.Round(matrix4x4[3, 3], digit)}\n";
            return output;
        }
    }

    public class Resampling
    {
        /// <summary>
        /// 有序3D點重取樣插補，忽略原有的點以完全重新取樣
        /// </summary>
        /// <param name="Pin">輸入點位</param>
        /// <param name="Pout">取樣後點位</param>
        /// <param name="ExpectedSampleDistance">取樣距離</param>
        /// <param name="IsClosed">是否為封閉</param>
        /// <param name="isKeepLast">是否保留最後一點</param>
        /// <returns>False : 資料點不足</returns>
        public static bool ReSample_SkipOriginalPoint(List<Point3D> Pin, List<Point3D> Pout, double ExpectedSampleDistance, bool IsClosed, bool isKeepLast = false)
        {
            // 資料點不足
            if (Pin.Count < 3 || Pin == null || Pout == null)
                return false;

            // 備份Pin
            List<Point3D> Ptemp = new List<Point3D>();
            Ptemp.Clear();
            for (int i = 0; i < Pin.Count; i++)
            {
                Ptemp.Add(Pin[i]);
            }
            Pout.Clear();

            Point3D p = new Point3D();
            Point3D p_prev = new Point3D();
            Point3D p_next = new Point3D();

            double TotalLength = 0.0;       // 曲線總長
            int estimated_sample_num = 0;   // 估計的取樣數
            double sample_distance = 0.0;   // 取樣間隔                      

            // 估計曲線總長
            Ptemp[0].Dt = Point3D.Distance(Ptemp[0], Ptemp[Ptemp.Count - 1]);
            TotalLength = Ptemp[0].Dt;
            for (int i = 0; i < Ptemp.Count - 1; i++)
            {
                p_prev = Ptemp[i];
                p_next = Ptemp[i + 1];
                p_next.Dt = Point3D.Distance(p_prev, p_next);    //將計算到的分段長度存入每一點以備後續使用
                TotalLength += p_next.Dt;
            }

            // 重新計算適合的取樣間隔
            // 經過計算後，sample_distance <= ExpectedSampleDistance
            estimated_sample_num = (int)Math.Ceiling(TotalLength / ExpectedSampleDistance);
            sample_distance = TotalLength / estimated_sample_num;

            // ==================================================== 等距離取樣演算法 +
            double accumulative_length = 0.0;   //累加的計算長度
            Vector3D uV = new Vector3D();         //單位方向向量

            lock (Pout)
            {

                //先加入首點
                Pout.Add(new Point3D(Ptemp[0]));

                int MaxCount = 0;
                if (IsClosed)
                    MaxCount = Ptemp.Count;       //如果要完成封閉曲線，則跑滿全部的點
                else
                    MaxCount = Ptemp.Count - 1;   //反之只跑到N-1點

                //逐點累進計算
                for (int i = 0; i < MaxCount; i++)
                {
                    //拆點啟始點
                    p_prev = Ptemp[i];

                    //拆點結束點
                    if (i == Ptemp.Count - 1)
                        p_next = Ptemp[0];    //欲完成封閉曲線，所以最後一次拆點的結束點是P[0]
                    else
                        p_next = Ptemp[i + 1];

                    // 當累進距離超過取樣距離，進行取樣
                    if (accumulative_length + p_next.Dt > sample_distance)
                    {
                        // 先計算從這點到下點之方向向量
                        uV.X = p_next.X - p_prev.X;
                        uV.Y = p_next.Y - p_prev.Y;
                        uV.Z = p_next.Z - p_prev.Z;
                        uV.X /= p_next.Dt;
                        uV.Y /= p_next.Dt;
                        uV.Z /= p_next.Dt;

                        // 計算拆點長度
                        double cut_length = sample_distance - accumulative_length;
                        // 從這點出發，在朝向下一點的方向，拆點長度的位置上插入一個新取樣點
                        p = new Point3D();
                        p.X = p_prev.X + cut_length * uV.X;
                        p.Y = p_prev.Y + cut_length * uV.Y;
                        p.Z = p_prev.Z + cut_length * uV.Z;
                        p.flag = p_prev.flag;
                        Pout.Add(p);

                        // 重複檢驗在這點到下點的線段上還可以再插入幾個取樣點
                        double remain_length = p_next.Dt - cut_length;
                        int remain_num = (int)Math.Ceiling(remain_length / sample_distance);
                        // 如果還可以繼續取樣，則進入此迴圈
                        for (int j = 1; j < remain_num; j++)
                        {
                            // 計算拆點長度
                            cut_length += sample_distance;
                            // 從這點出發，在朝向下一點的方向，拆點長度的位置上插入一個新取樣點
                            p = new Point3D();
                            p.X = p_prev.X + cut_length * uV.X;
                            p.Y = p_prev.Y + cut_length * uV.Y;
                            p.Z = p_prev.Z + cut_length * uV.Z;
                            p.flag = p_prev.flag;
                            Pout.Add(p);
                        }

                        // 更新累進距離，但是已經取樣拆點過的長度要扣掉
                        accumulative_length = p_next.Dt - cut_length;
                    }
                    else
                    {
                        // 當累進距離沒有達到取樣距離，不取樣
                        // 僅將這點到下點之長度加入累進距離
                        accumulative_length += p_next.Dt;
                    }


                }

                if (IsClosed)
                {
                    //關於封閉曲線的尾點處理
                    Point3D p_start = Ptemp[0];
                    Point3D p_end = Ptemp[Ptemp.Count - 1];
                    if (Point3D.Distance(p_start, p_end) < 0.25 * sample_distance)
                    {
                        // 當最後一點與第一點的距離太近（小於預期的取樣距離的0.25倍）
                        // 則將最後一點刪掉
                        Pout.Remove(p_end);
                    }
                }
                else if (isKeepLast)
                {
                    Pout.Add(Ptemp[Ptemp.Count - 1]);

                    Point3D p_end = Ptemp[Ptemp.Count - 1];
                    Point3D p_before_end = Ptemp[Ptemp.Count - 2];

                    if (Point3D.Distance(p_before_end, p_end) < 0.25 * sample_distance)
                    {
                        // 當最後一點與第一點的距離太近（小於預期的取樣距離的0.25倍）
                        // 則將最後一點刪掉
                        Pout.Remove(p_before_end);
                    }
                }

            }
            // ==================================================== 等距離取樣演算法 -

            return true;
        }
        /// <summary>
        /// 有序3D點重取樣插補。只有距離夠大需要插補的地方才插補，並保留原始點
        /// </summary>
        /// <param name="Pin">輸入點位</param>
        /// <param name="Pout">重新取樣後點位</param>
        /// <param name="MinDistance">最小點距</param>
        /// <param name="MaxDistance">最大點距</param>
        /// <param name="IsClosed">是否封閉</param>
        /// <returns>False : 資料點不足</returns>
        public static bool ReSample_KeepOriginalPoint(List<Point3D> Pin, List<Point3D> Pout, double MinDistance, double MaxDistance, bool IsClosed)
        {
            // 資料點不足
            if (Pin.Count < 20 || Pin == null || Pout == null)
                return false;

            Pout.Clear();

            Point3D p = new Point3D();
            Point3D p_prev = new Point3D();
            Point3D p_next = new Point3D();
            Vector3D uV = new Vector3D();           //單位方向向量

            //將計算到的分段長度存入每一點以備後續使用
            for (int i = 0; i < Pin.Count - 1; i++)
            {
                p_prev = Pin[i];
                p_next = Pin[i + 1];
                p_next.Dt = Point3D.Distance(p_prev, p_next);
            }

            // ==================================================== 等距離取樣演算法 +         
            lock (Pout)
            {
                //先加入首點
                Pout.Add(new Point3D(Pin[0]));

                int MaxCount = 0;
                if (IsClosed)
                    MaxCount = Pin.Count;       //如果要完成封閉曲線，則跑滿全部的點
                else
                    MaxCount = Pin.Count - 1;   //反之只跑到N-1點


                for (int i = 0; i < MaxCount; i++)
                {
                    //拆點啟始點
                    p_prev = Pin[i];

                    //拆點結束點
                    if (i == Pin.Count - 1)
                    {
                        p_next = Pin[0];    //欲完成封閉曲線，所以最後一次拆點的結束點是P[0]
                        p_next.Dt = Point3D.Distance(p_prev, p_next);
                    }
                    else
                        p_next = Pin[i + 1];

                    // 當prev點跟next點之間的距離在適合取樣的範圍內，進行取樣
                    if (p_next.Dt > MinDistance && p_next.Dt < MaxDistance)
                    {
                        // 先計算從這點到下點之方向向量
                        uV.X = p_next.X - p_prev.X;
                        uV.Y = p_next.Y - p_prev.Y;
                        uV.Z = p_next.Z - p_prev.Z;
                        uV.X /= p_next.Dt;
                        uV.Y /= p_next.Dt;
                        uV.Z /= p_next.Dt;

                        double cut_length = MinDistance;                    // prev到拆點位置的長度
                        double remain_length = p_next.Dt - cut_length;      // 拆點位置到next的長度

                        if (remain_length < MinDistance / 3.0)
                        {
                            //如果拆點位置已經離下一點很近，則放棄拆點，直接將next點加入
                            Pout.Add(new Point3D(p_next));
                        }
                        else
                        {
                            // 從prev出發，在朝向下一點的方向，cut_length長度的位置上插入一個新取樣點
                            p = new Point3D();
                            p.X = p_prev.X + cut_length * uV.X;
                            p.Y = p_prev.Y + cut_length * uV.Y;
                            p.Z = p_prev.Z + cut_length * uV.Z;
                            Pout.Add(p);

                            // 檢驗在這點到下點的線段上還可以再插入幾個取樣點
                            int remain_num = (int)Math.Ceiling(remain_length / MinDistance);

                            // 如果還可以繼續取樣，則進入此迴圈
                            for (int j = 1; j < remain_num; j++)
                            {
                                // 計算拆點長度
                                cut_length += MinDistance;
                                remain_length = p_next.Dt - cut_length;

                                if (remain_length < MinDistance / 3.0)
                                {
                                    //如果拆點位置已經離下一點很近，則放棄拆點，直接將next點加入
                                    Pout.Add(new Point3D(p_next));
                                    break;
                                }

                                // 從prev出發，在朝向下一點的方向，cut_length長度的位置上插入一個新取樣點
                                p = new Point3D();
                                p.X = p_prev.X + cut_length * uV.X;
                                p.Y = p_prev.Y + cut_length * uV.Y;
                                p.Z = p_prev.Z + cut_length * uV.Z;
                                Pout.Add(p);
                            }
                        }
                    }
                    else
                    {
                        // 不取樣直接將next點加入
                        Pout.Add(new Point3D(p_next));
                    }
                }
            }
            // ==================================================== 等距離取樣演算法 -

            return true;
        }

    }

}
