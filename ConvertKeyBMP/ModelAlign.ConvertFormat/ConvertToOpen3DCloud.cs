using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using RsLib.ConvertKeyBMP ;

namespace ConvertOpen3D
{
    using XYZArray = Tuple<double[], double[], double[]>;
    public static class ConvertToOpen3DCloud
    {
        public static XYZArray FromOPTFile(string xyzFilePath, char splitSeparator)
        {
            double[] x = new double[] { };
            double[] y = new double[] { };
            double[] z = new double[] { };

            if (!File.Exists(xyzFilePath)) return null;
            using (StreamReader sr = new StreamReader(xyzFilePath))
            {
                string readData = "";
                List<double> l_x = new List<double>();
                List<double> l_y = new List<double>();
                List<double> l_z = new List<double>();

                while (!sr.EndOfStream)
                {
                    readData = sr.ReadLine();
                    if (readData == "") continue;
                    string[] splitData = readData.Split(splitSeparator);
                    if (splitData.Length < 3) continue;
                    double parseValue = -999;
                    if (double.TryParse(splitData[0], out parseValue)) l_x.Add(parseValue);
                    parseValue = -999;
                    if (double.TryParse(splitData[1], out parseValue)) l_y.Add(parseValue);
                    parseValue = -999;
                    if (double.TryParse(splitData[2], out parseValue)) l_z.Add(parseValue);
                }
                x = l_x.ToArray();
                y = l_y.ToArray();
                z = l_z.ToArray();
            }
            return new XYZArray(x, y, z);
        }

        public static XYZArray FromXYZFile(string xyzFilePath, char splitSeparator)
        {
            double[] x = new double[] { };
            double[] y = new double[] { };
            double[] z = new double[] { };

            if (!File.Exists(xyzFilePath)) return null;
            using (StreamReader sr = new StreamReader(xyzFilePath))
            {
                string readData = "";
                List<double> l_x = new List<double>();
                List<double> l_y = new List<double>();
                List<double> l_z = new List<double>();

                while (!sr.EndOfStream)
                {
                    readData = sr.ReadLine();
                    if (readData == "") continue;
                    string[] splitData = readData.Split(splitSeparator);
                    if (splitData.Length < 3) continue;
                    double parseValue = -999;
                    if (double.TryParse(splitData[0], out parseValue)) l_x.Add(parseValue);
                    parseValue = -999;
                    if (double.TryParse(splitData[1], out parseValue)) l_y.Add(parseValue);
                    parseValue = -999;
                    if (double.TryParse(splitData[2], out parseValue)) l_z.Add(parseValue);
                }
                x = l_x.ToArray();
                y = l_y.ToArray();
                z = l_z.ToArray();
            }
            return new XYZArray(x, y, z);
        }
        public static XYZArray FromPLYFile(string plyFilePath)
        {
            double[] x = new double[] { };
            double[] y = new double[] { };
            double[] z = new double[] { };

            if (!File.Exists(plyFilePath)) return null;
            using (StreamReader sr = new StreamReader(plyFilePath))
            {
                string readData = "";
                int pointCount = 0;
                while (!sr.EndOfStream)
                {
                    readData = sr.ReadLine();
                    if (readData.Contains("element vertex"))
                    {
                        string[] splitData = readData.Split(' ');
                        pointCount = int.Parse(splitData[2]);
                        x = new double[pointCount];
                        y = new double[pointCount];
                        z = new double[pointCount];
                    }
                    if (readData == "end_header") break;
                }
                int pointIndex = 0;
                while (!sr.EndOfStream)
                {
                    readData = sr.ReadLine();
                    string[] splitData = readData.Split(' ');
                    double parseValue = -999;
                    if (double.TryParse(splitData[0], out parseValue)) x[pointIndex] = parseValue;
                    parseValue = -999;
                    if (double.TryParse(splitData[1], out parseValue)) y[pointIndex] = parseValue;
                    parseValue = -999;
                    if (double.TryParse(splitData[2], out parseValue)) z[pointIndex] = parseValue;
                    pointIndex++;
                    if (pointIndex >= pointCount) break;
                }
            }
            return new XYZArray(x, y, z);
        }
        public static XYZArray FromBMPFile(string bmpFilePath)
        {
            KeyBMP.Init();
            KeyBMP.Load(bmpFilePath);
            List<double> xx = new List<double>();
            List<double> yy = new List<double>();
            List<double> zz = new List<double>();

            for (int y = 0; y < KeyBMP.Height; y++)
            {
                for (int x = 0; x < KeyBMP.Width; x++)
                {
                    double _z = KeyBMP.GetZValue(x, y);
                    if (_z == KeyBMP.NoData) continue;
                    xx.Add(KeyBMP.GetXValue(x));
                    yy.Add(KeyBMP.GetYValue(y));
                    zz.Add(_z);
                }
            }
            return new XYZArray(xx.ToArray(), yy.ToArray(), zz.ToArray());
        }
        public static XYZArray FromModelFile(string modelFilePath)
        {
            double[] x = new double[] { };
            double[] y = new double[] { };
            double[] z = new double[] { };

            if (!File.Exists(modelFilePath)) return null;
            using (StreamReader sr = new StreamReader(modelFilePath))
            {
                string readData = "";
                List<double> l_x = new List<double>();
                List<double> l_y = new List<double>();
                List<double> l_z = new List<double>();

                while (!sr.EndOfStream)
                {
                    readData = sr.ReadLine();
                    string[] splitData = readData.Split(' ');
                    double parseValue = -999;
                    if (double.TryParse(splitData[0], out parseValue)) l_x.Add(parseValue);
                    parseValue = -999;
                    if (double.TryParse(splitData[1], out parseValue)) l_y.Add(parseValue);
                    parseValue = -999;
                    if (double.TryParse(splitData[2], out parseValue)) l_z.Add(parseValue);
                }
                x = l_x.ToArray();
                y = l_y.ToArray();
                z = l_z.ToArray();
            }
            return new XYZArray(x, y, z);
        }
        public static Tuple<double[], double[], double[], double[], double[], double[]> FromPathFile(string modelFilePath)
        {
            double[] x = new double[] { };
            double[] y = new double[] { };
            double[] z = new double[] { };
            double[] nx = new double[] { };
            double[] ny = new double[] { };
            double[] nz = new double[] { };
            if (!File.Exists(modelFilePath)) return null;
            using (StreamReader sr = new StreamReader(modelFilePath))
            {
                string readData = "";
                List<double> l_x = new List<double>();
                List<double> l_y = new List<double>();
                List<double> l_z = new List<double>();
                List<double> l_nx = new List<double>();
                List<double> l_ny = new List<double>();
                List<double> l_nz = new List<double>();
                while (!sr.EndOfStream)
                {
                    readData = sr.ReadLine();
                    string[] splitData = readData.Split(',');
                    double parseValue = -999;
                    if (double.TryParse(splitData[0], out parseValue)) l_x.Add(parseValue);
                    parseValue = -999;
                    if (double.TryParse(splitData[1], out parseValue)) l_y.Add(parseValue);
                    parseValue = -999;
                    if (double.TryParse(splitData[2], out parseValue)) l_z.Add(parseValue);
                    parseValue = -999;
                    if (double.TryParse(splitData[3], out parseValue)) l_nx.Add(parseValue);
                    parseValue = -999;
                    if (double.TryParse(splitData[4], out parseValue)) l_ny.Add(parseValue);
                    parseValue = -999;
                    if (double.TryParse(splitData[5], out parseValue)) l_nz.Add(parseValue);
                }
                x = l_x.ToArray();
                y = l_y.ToArray();
                z = l_z.ToArray();
                nx = l_nx.ToArray();
                ny = l_ny.ToArray();
                nz = l_nz.ToArray();
            }
            return new Tuple<double[], double[], double[], double[], double[], double[]>(x, y, z, nx, ny, nz);
        }
        public static void SaveTupleXYZ(XYZArray target, string filePath)
        {
            if (target.Item1.Length == target.Item2.Length && target.Item1.Length == target.Item3.Length)
            {
                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
                {
                    for (int i = 0; i < target.Item1.Length; i++)
                    {
                        sw.WriteLine($"{target.Item1[i]} {target.Item2[i]} {target.Item3[i]}");
                    }
                    sw.Flush();
                }
            }
            else
            {
                string msg = $"tuple array not equal x : {target.Item1.Length}, y : {target.Item2.Length}, z : {target.Item3.Length}";
                throw new Exception(msg);
            }
        }
    }
}
