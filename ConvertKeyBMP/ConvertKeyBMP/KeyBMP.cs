using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

using RsLib.LogMgr;
using System.Diagnostics;
namespace RsLib.ConvertKeyBMP
{
    public class KeyBMP
    {
        static KeyBMPConfig config = new KeyBMPConfig();
        static double[,] cloudArray;
        public const double NoData = -999;
        static int height = 0;
        public static int Height { get => height; }
        static int width = 0;
        public static int Width { get => width; }
        public static string HeightExt { get => config.HeightFileExt; }
        public static string IntensityExt { get => config.IntensityFileExt; }
        public static void Init()
        {
            if(!config.IsInit) config.LoadYaml();
        }
        
        public static void SetGap(double XGap,double YGap)
        {
            config.XGap = Math.Abs(XGap);
            config.YGap = Math.Abs(YGap);
            config.SaveYaml();
        }
        public static void SetHeightRange(double max,double min)
        {
            config.MaxH = max > min ? max : min;
            config.MinH = min < max ? min : max;
            config.SaveYaml();
        }
        static bool checkFileStatus(string file_path)
        {
            if (!File.Exists(file_path)) return false;
            string file_name = Path.GetFileName(file_path);
            if (!file_name.Contains(config.HeightFileExt.Split('.')[0])) return false;

            return true;
        }
        public static void Load(string file_path)
        {
            try
            {
                Log.Add($"Loading Height Image : {file_path}", MsgLevel.Trace);
                if (!checkFileStatus(file_path))
                {
                    Log.Add($"Image : {file_path} Not Found", MsgLevel.Warn);
                    return;
                }
                Bitmap bmp = new Bitmap(file_path);
                height = bmp.Height;
                width = bmp.Width;
                Rectangle rec = new Rectangle(0, 0, width, height);
                BitmapData bData = bmp.LockBits(rec, ImageLockMode.ReadOnly, bmp.PixelFormat);
                IntPtr ptr_scan0 = bData.Scan0;
                int size = bData.Stride * bData.Height;
                byte[] data = new byte[size];

                cloudArray = new double[height, width];
                Marshal.Copy(ptr_scan0, data, 0, size);

                for (int y = 0; y < bData.Height; y++)
                {
                    for (int x = 0; x < bData.Stride; x += 3)
                    {
                        byte B = data[y * bData.Stride + x];
                        byte G = data[y * bData.Stride + x + 1];
                        byte R = data[y * bData.Stride + x + 2];


                        if (B == 0 && G == 0 && R == 0)
                        {
                            cloudArray[y, x / 3] = NoData;
                            continue;
                        }
                        else
                        {
                            int testValue = RGBtoInt(R, G, B);
                            double zz = IntToHeight(testValue);
                            cloudArray[y, x / 3] = zz;
                        }
                    }
                }
                bmp.UnlockBits(bData);
                bmp = null;
                bData = null;
                Log.Add($"Height Image Load Finished.", MsgLevel.Trace);
            }
            catch(Exception ex)
            {
                Log.Add("Load height image exception.", MsgLevel.Alarm, ex);
            }


        }
        public static void LoadInt(string file_path)
        {
            try
            {
                Log.Add($"Loading Height Int Image : {file_path}", MsgLevel.Trace);
                if (!checkFileStatus(file_path))
                {
                    Log.Add($"Image : {file_path} Not Found", MsgLevel.Warn);
                    return;
                }
                Bitmap bmp = new Bitmap(file_path);
                height = bmp.Height;
                width = bmp.Width;

                Rectangle rec = new Rectangle(0, 0, width, height);
                BitmapData bData = bmp.LockBits(rec, ImageLockMode.ReadOnly, bmp.PixelFormat);
                IntPtr ptr_scan0 = bData.Scan0;
                int size = bData.Stride * bData.Height;
                byte[] data = new byte[size];

                cloudArray = new double[height, width];
                Marshal.Copy(ptr_scan0, data, 0, size);

                for (int y = 0; y < bData.Height; y++)
                {
                    for (int x = 0; x < bData.Stride; x += 3)
                    {
                        byte B = data[y * bData.Stride + x];
                        byte G = data[y * bData.Stride + x + 1];
                        byte R = data[y * bData.Stride + x + 2];


                        if (B == 0 && G == 0 && R == 0)
                        {
                            cloudArray[y, x / 3] = NoData;
                            continue;
                        }
                        else
                        {
                            int testValue = RGBtoInt(R, G, B);
                            cloudArray[y, x / 3] = testValue;
                        }
                    }
                }
                bmp.UnlockBits(bData);
                bmp = null;
                bData = null;
                Log.Add($"Height Image Load Finished.", MsgLevel.Trace);
            }
            catch(Exception ex)
            {
                Log.Add("Load height integer image exception.", MsgLevel.Alarm, ex);
            }
        }

        public static double[] FindXYZ(int pxX, int pxY)
        {
            double[] output = new double[] { NoData, NoData, NoData };
            if (pxY >= cloudArray.GetLength(0)) return output;
            if (pxX >= cloudArray.GetLength(1)) return output;

            output[0] = GetXValue(pxX);
            output[1] = GetYValue(pxY);
            output[2] = GetZValue(pxX, pxY);
            return output;
        }

        public static void SaveData(double[] x,double[] y,double[] z,string filePath)
        {
            if (x.Length != y.Length || x.Length != z.Length) return;
            using (StreamWriter sw = new StreamWriter(filePath,false,Encoding.Default))
            {
                for(int i = 0; i < x.Length;i++)
                {
                    if (z[i] == NoData) continue;
                    sw.WriteLine($"{x[i]:F2} {y[i]:F2} {z[i]:F2}");
                }
                sw.Flush();
            }
        }
        public static void SaveData(string filePath)
        {
            Log.Add($"Save XYZ File. {filePath}", MsgLevel.Trace);

            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                int yCount = cloudArray.GetLength(0);
                int xCount = cloudArray.GetLength(1);
                for (int y = 0; y < yCount; y++)
                {
                    int blankCount = 0;
                    for (int x = 0; x < xCount; x++)
                    {
                        double zValue = cloudArray[y, x];
                        if (zValue == NoData)
                        {
                            blankCount++;
                            continue;
                        }
                        double xValue = GetXValue(x);
                        double yValue = GetYValue(y);
                        
                        sw.WriteLine($"{xValue} {yValue} {zValue}");
                    }
                    if(blankCount != cloudArray.GetLength(1)) sw.WriteLine("");
                }
                sw.Flush();
            }
        }
        public static void SaveCSVData(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                int yCount = cloudArray.GetLength(0);
                int xCount = cloudArray.GetLength(1);
                for (int y = 0; y < yCount; y++)
                {
                    string writeLine = "";
                    for (int x = 0; x < xCount; x++)
                    {
                        double z = GetZValue(x, y);
                        if (z == NoData) continue;
                        writeLine += $"{z}";
                        if (x != xCount - 1) writeLine += ",";
                    }
                    sw.WriteLine($"{writeLine}");
                }
                sw.Flush();
                sw.Close();
            }
        }

        public static void SaveData(double[,] cloudArray, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                int yCount = cloudArray.GetLength(0);
                int xCount = cloudArray.GetLength(1);
                for (int y = 0; y < yCount; y++)
                {
                    int blankCount = 0;
                    for (int x = 0; x < xCount; x++)
                    {
                        double zValue = cloudArray[y, x];
                        if (zValue == NoData)
                        {
                            blankCount++;
                            continue;
                        }
                        double xValue = GetXValue(x);
                        double yValue = GetYValue(y);
                        
                        sw.WriteLine($"{xValue} {yValue} {zValue}");
                    }
                    if (blankCount != cloudArray.GetLength(1)) sw.WriteLine("");
                }
                sw.Flush();
            }
        }
        public static double GetXValue(int xIndex) => Math.Round(xIndex * config.XGap,2);
        public static double GetXValue(double xIndex) => Math.Round(xIndex * config.XGap, 2);

        public static double GetYValue(int yIndex) => Math.Round((height-1 - yIndex) * config.YGap,2);
        public static double GetYValue(double yIndex) => Math.Round((height - 1 - yIndex) * config.YGap, 2);

        public static double GetZValue(int xIndex,int yIndex) => cloudArray[yIndex,xIndex];
        public static double GetZValue(double xIndex, double yIndex)
        {
            int ceilX = (int)Math.Ceiling(xIndex);
            int floorX = (int)Math.Floor(xIndex);
             
            int ceilY = (int)Math.Ceiling(yIndex);
            int floorY = (int)Math.Floor(yIndex);

            double z1 = GetZValue(ceilX, ceilY);
            double z2 = GetZValue(ceilX, floorY);
            double z3 = GetZValue(floorX, ceilY);
            double z4 = GetZValue(floorX, floorY);

            double sum = 0;
            int sumCount = 0;
            if(z1 != NoData)
            {
                sum += z1;
                sumCount++;
            }
            if (z2 != NoData)
            {
                sum += z2;
                sumCount++;
            }
            if (z3 != NoData)
            {
                sum += z3;
                sumCount++;
            }
            if (z4 != NoData)
            {
                sum += z4;
                sumCount++;
            }
            return sum / sumCount;

        }

        public static double NearestHighest(int xIndex,int yIndex, int offset)
        {
            if (offset == 0) return GetZValue(xIndex, yIndex);
            int absOffset = Math.Abs(offset);
            int ymin = (yIndex - absOffset) > 0 ? (yIndex-absOffset) : 0;
            int ymax = (yIndex + absOffset) < Height ? (yIndex + absOffset) : Height -1;
            int xmin = (xIndex - absOffset) > 0 ? (xIndex - absOffset) : 0;
            int xmax = (xIndex + absOffset) < Width ? (xIndex + absOffset) : Width - 1;

            double zMax = double.MinValue;
            for(int y = ymin ; y<=ymax ; y++)
            {
                for(int x = xmin; x <= xmax ; x++)
                {
                    double tryZ = GetZValue(x, y);
                    if (tryZ > zMax) zMax = tryZ;
                }
            }
            return zMax;
        }
        public static double NearestHighest(double xIndex, double yIndex, int offset)
        {
            if (offset == 0) return GetZValue(xIndex, yIndex);
            int absOffset = Math.Abs(offset);
            int ymin = (yIndex - absOffset) > 0 ? (int)Math.Floor(yIndex - absOffset) : 0;
            int ymax = (yIndex + absOffset) < Height ? (int)Math.Ceiling(yIndex + absOffset) : Height - 1;
            int xmin = (xIndex - absOffset) > 0 ? (int)Math.Floor(xIndex - absOffset) : 0;
            int xmax = (xIndex + absOffset) < Width ? (int)Math.Ceiling(xIndex + absOffset) : Width - 1;

            double zMax = double.MinValue;
            for (int y = ymin; y <= ymax; y++)
            {
                for (int x = xmin; x <= xmax; x++)
                {
                    double tryZ = GetZValue(x, y);
                    if (tryZ > zMax) zMax = tryZ;
                }
            }
            return zMax;
        }

        /*
        public static bool Load(string file_path)
        {
            if (!checkFileStatus(file_path)) return false;
            Bitmap bmp = new Bitmap(file_path);
            Rectangle rec = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bData = bmp.LockBits(rec, ImageLockMode.ReadOnly, bmp.PixelFormat);
            IntPtr ptr_scan0 = bData.Scan0;
            int size = bData.Stride * bData.Height;
            byte[] data = new byte[size];
            Marshal.Copy(ptr_scan0, data, 0, size);
            zdata = new double[bmp.Width,bmp.Height];
            for (int y = 0; y < bData.Height; y++)
            {
                for (int x = 0; x < bData.Stride; x += 3)
                {
                    byte B = data[ y * bData.Stride + x];
                    byte G =data[ y * bData.Stride + x + 1];
                    byte R = data[y * bData.Stride + x + 2];

                    if (B == 0 && G == 0 && R == 0)
                    {
                        zdata[x / 3, y] = -999;
                        continue;
                    }
                    else
                    {
                        int testValue = RGBtoInt(R, G, B);
                        //double X = j / 3 * xGap;
                        //double Y = i * yGap;
                        double Z = IntToHeight(testValue);
                        zdata[x / 3, y] = Z;

                    }
                }
            }

            bmp.UnlockBits(bData);
            bmp = null;
            bData = null;

            return true;
        }
        */
        static int RGBtoInt(byte R, byte G, byte B)
        {
            int output = -1;
            output = (G << 7) | ((R & 0x07) << 4) | (B & 0x0f);
            return output;
        }
        static double IntToHeight(int value)
        {
            double output = 0;
            output = Math.Round((config.MaxH - config.MinH) / 32768 * value + config.MinH,2);
            return output;
        }

    }
    [Serializable]
    internal class KeyBMPConfig
    {
        public  string HeightFileExt = "_IMG_HEIGHT.bmp";
        public string IntensityFileExt = "_IMG_INTENSITY.bmp";

        public double XGap = 0.15;
        public double YGap = 0.15;
        public double MaxH = 60.0;
        public double MinH = -60.0;
        //public int Width = 1600;
        //public int Height = 3000;
        bool isInit = false;
        [YamlIgnore]
        public bool IsInit { get => isInit; }
        
        const string config_file_name = "keyence.cfg";


        public void LoadYaml()
        {
            Log.Add("KeyBMP Module Load Config.", MsgLevel.Trace);
            string folder = System.Environment.CurrentDirectory;
            string config_folder = $"{folder}\\Config";
            string file_path = $"{config_folder}\\{config_file_name}";

            if (!File.Exists(file_path))
            {
                Log.Add($"KeyBMP Module Config {file_path} Not Found.", MsgLevel.Warn);
                SaveYaml();
            }
            string ReadData = "";
            using (StreamReader sr = new StreamReader(file_path, Encoding.Default))
            {
                ReadData = sr.ReadToEnd();
            }
            var deserializer = new DeserializerBuilder()
                .IgnoreUnmatchedProperties()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)  // see height_in_inches in sample yml 
                .Build();

            //yml contains a string containing your YAML
            
            var p = deserializer.Deserialize<KeyBMPConfig>(ReadData);
            this.XGap = p.XGap;
            this.YGap = p.YGap;
            this.MaxH = p.MaxH;
            this.MinH = p.MinH;
            this.HeightFileExt = p.HeightFileExt;
            this.IntensityFileExt = p.IntensityFileExt;

            isInit = true;
            Log.Add($"KeyBMP Module Config {file_path} Loaded.", MsgLevel.Trace);

        }
        public void SaveYaml()
        {
            string folder = System.Environment.CurrentDirectory;
            string config_folder = $"{folder}\\Config";
            string file_path = $"{config_folder}\\{config_file_name}";

            if (!Directory.Exists(config_folder)) Directory.CreateDirectory(config_folder);
            using (StreamWriter sw = new StreamWriter(file_path, false, Encoding.Default))
            {
                var serializer = new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
                var yaml = serializer.Serialize(this);
                sw.WriteLine(yaml);
                sw.Flush();
            }
            Log.Add($"KeyBMP Module Config {file_path} Saved.", MsgLevel.Trace);

        }

    }
}
