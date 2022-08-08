/// <summary>
/// Version and Date:   2014.9.29
/// Writer:             Eason Lin
/// Description:        AutoSearch Data Access for AutoSearch Mark1
/// </summary>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// for File I/O
using System.IO;

namespace Automation
{
    public class AutoSearchData
    {
        public AutoSearchKernel autoSearch;

        #region File Save

        public bool Write3DPointDataToOFFFile(string strFile, List<Point3D> points)
        {
            if (points == null)
                return false;

            if (points.Count == 0)
                return false;

            StreamWriter filewriter;
            try
            {
                filewriter = new StreamWriter(strFile);

                filewriter.WriteLine("OFF");

                filewriter.WriteLine("{0} 0 0", points.Count);

                for (int i = 0; i < points.Count; i++)
                {
                    filewriter.WriteLine("{0:F3} {1:F3} {2:F3}", points[i].X, points[i].Y, points[i].Z);
                }

                filewriter.Close();
                return true;
            }
            catch (System.Exception e)
            {
                string err_str = e.ToString();
                //MessageBox.Show(err_str);
                return false;
            }
        }

        /// <summary>
        /// Save scan.xyz (to Philip Ma's program)
        /// </summary>
        public bool WriteScanData(string strFile)
        {
            StreamWriter filewriter;

            if (autoSearch.points1 == null)
                return false;

            if (autoSearch.points1.Count == 0)
                return false;

            if (autoSearch.polyline2 == null)
                return false;

            if (autoSearch.polyline2.Count == 0)
                return false;

            try
            {
                filewriter = new StreamWriter(strFile);
                // ============= Biteline 資料 ==============
                for (int i = 0; i < autoSearch.points1.Count; i++)
                {
                    filewriter.WriteLine("{0:F3} {1:F3} {2:F3}", autoSearch.points1[i].X, autoSearch.points1[i].Y, autoSearch.points1[i].Z);
                }
                filewriter.WriteLine(""); // 空一行
                filewriter.WriteLine(""); // 空一行
                filewriter.WriteLine(""); // 空一行
                // ============= Upper 資料 ==============
                bool blank_flag = false;
                for (int i = 0; i < autoSearch.polyline2.Count; i++)
                {
                    // 空行模式啟用時，每條線的起始點前空一行（沒有點的線不需要）
                    if (blank_flag && autoSearch.polyline2[i].Count != 0)
                        filewriter.WriteLine("");

                    // 判斷何時是否要啟用空行模式：找到第一條有點的線，則啟用
                    if (!blank_flag && autoSearch.polyline2[i].Count != 0)
                        blank_flag = true;

                    for (int p = 0; p < autoSearch.polyline2[i].Count; p++)
                    {
                        filewriter.WriteLine("{0:F3} {1:F3} {2:F3}", autoSearch.polyline2[i][p].X, autoSearch.polyline2[i][p].Y, autoSearch.polyline2[i][p].Z);
                    }
                }

                filewriter.Close();
                return true;
            }
            catch (System.Exception e)
            {
                string err_str = e.ToString();
                MessageBox.Show(err_str);
                return false;
            }
        }
        /// <summary>
        /// Save 3D point cloud to file
        /// </summary>
        public bool Write3DPointDataToFile(string strFile, List<Point3D> points)
        {
            if (points == null)
                return false;

            if (points.Count == 0)
                return false;

            StreamWriter filewriter;
            try
            {
                filewriter = new StreamWriter(strFile);

                for (int i = 0; i < points.Count; i++)
                {
                    filewriter.WriteLine("{0:F3} {1:F3} {2:F3}", points[i].X, points[i].Y, points[i].Z);
                }
                filewriter.Close();
                return true;
            }
            catch (System.Exception e)
            {
                string err_str = e.ToString();
                MessageBox.Show(err_str);
                return false;
            }
        }

        ///// <summary>
        ///// Save 3D profile (line by line) to file
        ///// </summary>
        //public bool Write3DProfileDataToFile(string strFile, List<List<Point3D>> profile)
        //{
        //    if (profile == null)
        //        return false;

        //    if (profile.Count == 0)
        //        return false;

        //    StreamWriter filewriter;
        //    try
        //    {
        //        filewriter = new StreamWriter(strFile);

        //        bool blank_flag = false;
        //        for (int i = 0; i < profile.Count; i++)
        //        {
        //            // add blank row, between change line
        //            if (blank_flag && profile[i].Count != 0)
        //                filewriter.WriteLine("");

        //            // do not add blank, if the new line does not contain any point data
        //            if (!blank_flag && profile[i].Count != 0)
        //                blank_flag = true;

        //            for (int p = 0; p < profile[i].Count; p++)
        //            {
        //                filewriter.WriteLine("{0:F3} {1:F3} {2:F3}", profile[i][p].X, profile[i][p].Y, profile[i][p].Z);
        //            }
        //        }
        //        filewriter.Close();
        //        return true;
        //    }
        //    catch (System.Exception e)
        //    {
        //        string err_str = e.ToString();
        //        MessageBox.Show(err_str);
        //        return false;
        //    }
        //}
        /// <summary>
        /// Save 3D profile (line by line) to file
        /// </summary>
        public bool Write3DProfileDataToFile(string strFile, List<List<Point3D>> profile)
        {
            if (profile == null)
                return false;

            if (profile.Count == 0)
                return false;

            StreamWriter filewriter;
            try
            {
                filewriter = new StreamWriter(strFile);

                for (int i = 0; i < profile.Count; i++)
                {
                    filewriter.WriteLine("");

                    for (int p = 0; p < profile[i].Count; p++)
                    {
                        filewriter.WriteLine("{0:F3} {1:F3} {2:F3}", profile[i][p].X, profile[i][p].Y, profile[i][p].Z);
                    }
                }
                filewriter.Close();
                return true;
            }
            catch (System.Exception e)
            {
                string err_str = e.ToString();
                MessageBox.Show(err_str);
                return false;
            }
        }

        /// <summary>
        /// Save 3D profile (line by line) to file, including blank line
        /// </summary>
        public bool Write3DProfileDataToFile_IncludeBlankLine(string strFile, List<List<Point3D>> profile)
        {
            if (profile == null)
                return false;

            if (profile.Count == 0)
                return false;

            StreamWriter filewriter;
            try
            {
                filewriter = new StreamWriter(strFile);

                for (int i = 0; i < profile.Count; i++)
                {
                    //add "L" when starting a new line, whatever the new line contains data or not.
                    filewriter.WriteLine("L");

                    for (int p = 0; p < profile[i].Count; p++)
                    {
                        filewriter.WriteLine("{0:F3} {1:F3} {2:F3}", profile[i][p].X, profile[i][p].Y, profile[i][p].Z);
                    }
                }
                filewriter.Close();
                return true;
            }
            catch (System.Exception e)
            {
                string err_str = e.ToString();
                MessageBox.Show(err_str);
                return false;
            }
        }
        #endregion

        #region File Load
        public int Get3DProfilePointCount(List<List<Point3D>> profile)
        {
            int count = 0;
            for (int i = 0; i < profile.Count; i++)
            {
                count += profile[i].Count;
            }
            return count;
        }

        ///// <summary>
        ///// Read 3D profile (line by line) from file
        ///// </summary>
        //public bool Read3DProfileDataFromFile(string strFile, out List<List<Point3D>> profile)
        //{
        //    List<List<Point3D>> prf = new List<List<Point3D>>();

        //    if (!File.Exists(strFile))
        //    {
        //        profile = null;
        //        return false;
        //    }

        //    //讀取全部字串
        //    List<string> stringList = MyFile.ReadAllLines(strFile);

        //    bool blank_flag = false;
        //    prf.Add(new List<Point3D>());
        //    for (int i = 0; i < stringList.Count; i++)
        //    {
        //        bool IsNullLine = MyFile.NullLine(stringList[i]);
        //        if (IsNullLine)
        //        {
        //            if (!blank_flag)
        //            {
        //                // 遇到空行加入一條新線，但連續空行則不加
        //                prf.Add(new List<Point3D>());
        //                blank_flag = true;
        //            }
        //        }
        //        else
        //        {
        //            blank_flag = false;
        //            string delimStr = " ,";
        //            char[] delimiter = delimStr.ToCharArray();
        //            string[] split = null;

        //            split = stringList[i].Split(delimiter, 3);

        //            prf[prf.Count - 1].Add(new Point3D(double.Parse(split[0]),
        //                                                double.Parse(split[1]),
        //                                                double.Parse(split[2])
        //                                            )
        //                                );
        //        }
        //    }
        //    profile = prf;
        //    return true;
        //}
        /// <summary>
        /// Read 3D profile (line by line) from file
        /// </summary>
		public bool Read3DProfileDataFromFile(string strFile, out List<List<Point3D>> profile)
        {
            List<List<Point3D>> list = new List<List<Point3D>>();
            if (!File.Exists(strFile))
            {
                profile = null;
                return false;
            }
            List<string> list2 = MyFile.ReadAllLines(strFile);
            int num = 0;
            string ext = Path.GetExtension(strFile).ToUpper();
            for (int i = 0; i < list2.Count; i++)
            {
                if (MyFile.NullLine(list2[i]))
                {
                    list.Add(new List<Point3D>());
                    num++;
                    continue;
                }
                string text = " ,";
                if (ext == ".PLY") text = " ";
                char[] separator = text.ToCharArray();
                string[] array = null;
                array = list2[i].Split(separator, 3);
                if (num == 0)
                {
                    list.Add(new List<Point3D>());
                    num++;
                }
                list[list.Count - 1].Add(new Point3D(double.Parse(array[0]), double.Parse(array[1]), double.Parse(array[2])));
            }
            profile = list;
            return true;
        }


        /// <summary>
        /// Read 3D profile (line by line) from file, including blank line
        /// </summary>
        public bool Read3DProfileDataFromFile_IncludeBlankLine(string strFile, out List<List<Point3D>> profile)
        {
            List<List<Point3D>> prf = new List<List<Point3D>>();

            if (!File.Exists(strFile))
            {
                profile = null;
                return false;
            }

            //讀取全部字串
            List<string> stringList = MyFile.ReadAllLines(strFile);


            for (int i = 0; i < stringList.Count; i++)
            {
                if (stringList[i] == "L")
                {
                    prf.Add(new List<Point3D>());
                }
                else
                {
                    string delimStr = " ,";
                    char[] delimiter = delimStr.ToCharArray();
                    string[] split = null;

                    split = stringList[i].Split(delimiter, 3);

                    prf[prf.Count - 1].Add(new Point3D(double.Parse(split[0]),
                                                        double.Parse(split[1]),
                                                        double.Parse(split[2])
                                                    )
                                        );
                }
            }
            profile = prf;
            return true;
        }

        /// <summary>
        /// Read 3D point cloud from file
        /// </summary>
        public bool Read3DPointDataFromFile(string strFile, out List<Point3D> pCloud)
        {
            List<Point3D> p = new List<Point3D>();

            if (!File.Exists(strFile))
            {
                pCloud = p;
                return false;
            }

            //讀取全部字串
            List<string> stringList = MyFile.ReadAllLines(strFile);

            for (int i = 0; i < stringList.Count; i++)
            {
                bool IsNullLine = MyFile.NullLine(stringList[i]);
                if (!IsNullLine)
                {
                    if (stringList[i].Length < 12)
                    {
                        continue;
                    }

                    string delimStr = " ,";
                    char[] delimiter = delimStr.ToCharArray();
                    string[] split = null;

                    split = stringList[i].Split(delimiter);
                    p.Add(new Point3D(double.Parse(split[0]),
                                                double.Parse(split[1]),
                                                double.Parse(split[2])
                                            )
                         );
                }
            }
            pCloud = p;
            return true;
        }

        /// <summary>
        ///Read Philip Ma's file (*.OPT)
        /// </summary>
        public bool Read3DOptDataFromFile(string strFile, out List<List<Position3D>> opt)
        {
            List<List<Position3D>> p = new List<List<Position3D>>();

            if (!File.Exists(strFile))
            {
                opt = null;
                return false;
            }

            //    //讀取全部字串
            List<string> stringList = MyFile.ReadAllLines(strFile);

            bool blank_flag = false;
            p.Add(new List<Position3D>());
            for (int i = 0; i < stringList.Count; i++)
            {
                bool IsNullLine = MyFile.NullLine(stringList[i]);
                if (IsNullLine)
                {
                    if (!blank_flag)
                    {
                        // 遇到空行加入一條新線，但連續空行則不加
                        p.Add(new List<Position3D>());
                        blank_flag = true;
                    }
                }
                else
                {
                    blank_flag = false;
                    string delimStr = " ,";
                    char[] delimiter = delimStr.ToCharArray();
                    string[] split = null;

                    split = stringList[i].Split(delimiter);

                    p[p.Count - 1].Add(new Position3D(double.Parse(split[0]),
                                                        double.Parse(split[1]),
                                                        double.Parse(split[2]),
                                                        double.Parse(split[3]),
                                                        double.Parse(split[4]),
                                                        double.Parse(split[5])
                                                    )
                                        );
                }
            }
            opt = p;
            return true;

        }

        #endregion

    }
}
