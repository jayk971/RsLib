using RsLib.PointCloudLib;
using System;
using System.Collections.Generic;
using System.IO;
namespace RsLib.ConvertKeyBMP
{
    public static class KeyRawCSV
    {
        public static readonly string Extension = "_HRaw.csv";
        const string _dataInfo_Start = "DataInfoStart";
        const string _dataInfo_End = "DataInfoEnd";
        const string _dataInfo_DataPerRow = "DataPerRow";
        const string _dataInfo_Pitch = "Pitch";
        const string _dataInfo_TotalRowCount = "TotalRowCount";
        const string _dataInfo_ZUnit = "ZUnit";
        const double _noData = -999;
        static uint _dataPerProfile = 1600;
        static double _pitch = 0.15;
        static uint _profileCount = 3500;
        static double _zUnit = 8;
        static List<ushort> _heightData = new List<ushort>();
        static string splitDataInfo(string rowString)
        {
            string[] split = rowString.Split(',');
            if (split.Length == 2) return split[1];
            else return string.Empty;
        }
        public static PointCloud LoadHeightRawData(string filePath, int downSampleX, int downSampleY)
        {
            _heightData.Clear();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string writeData = sr.ReadLine();

                    if (writeData == _dataInfo_Start)
                    {
                        string dataPerProfile = splitDataInfo(sr.ReadLine());
                        _dataPerProfile = uint.Parse(dataPerProfile);
                        string pitch = splitDataInfo(sr.ReadLine());
                        _pitch = double.Parse(pitch);
                        string profileCount = splitDataInfo(sr.ReadLine());
                        _profileCount = uint.Parse(profileCount);
                        string zUnit = splitDataInfo(sr.ReadLine());
                        _zUnit = double.Parse(zUnit);
                        sr.ReadLine();
                    }

                    string[] splitData = writeData.Split(',');
                    for (int i = 0; i < splitData.Length; i++)
                    {
                        ushort parseValue = 0;
                        if (ushort.TryParse(splitData[i], out parseValue))
                        {
                            _heightData.Add(parseValue);
                        }
                    }
                }
            }
            return saveHeightPointCloud(downSampleX, downSampleY);
        }
        static PointCloud saveHeightPointCloud(int downSampleX, int downSampleY)
        {
            PointCloud p = new PointCloud();
            for (int i = 0; i < _heightData.Count; i++)
            {
                int x = i % (int)_dataPerProfile;
                int y = i / (int)_dataPerProfile;

                if (y % downSampleY != 0) continue;
                if (x % downSampleX != 0) continue;
                ushort us = _heightData[i];
                if (us == 0) continue;

                double xx = GetXValue(x);
                double yy = GetYValue(y);
                double zz = GetZValue(x, y);
                if (zz == _noData) continue;
                else
                {
                    p.Add(new Point3D(xx, yy, zz));
                }
            }
            return p;

        }
        static double GetXValue(int xIndex) => Math.Round(xIndex * _pitch, 2);
        static double GetXValue(double xIndex) => Math.Round(xIndex * _pitch, 2);
        static double GetYValue(int yIndex) => Math.Round((_profileCount - 1 - yIndex) * _pitch, 2);
        static double GetYValue(double yIndex) => Math.Round((_profileCount - 1 - yIndex) * _pitch, 2);
        /// <summary>
        /// 從高度陣列中轉換實際高度
        /// </summary>
        /// <param name="xIndex">X 像素座標</param>
        /// <param name="yIndex">Y 像素座標</param>
        /// <param name="isYIndexFromCropImage"> Y 像素座標是否偏移, true : 從已裁切的圖片取得像素座標, false : 從未裁切的圖片取得像素座標</param>
        /// <returns>Tuple <x, y, z></returns>
        static double GetZValue(int xIndex, int yIndex)
        {
            int dataIndex = yIndex * (int)_dataPerProfile + xIndex;
            ushort z = _heightData[dataIndex];
            double Z = _noData;

            if (z == 0) Z = _noData;
            else Z = convertHeightData(z);

            return Z;
        }
        static double convertHeightData(ushort rawData, int roundDigit = 2) => Math.Round(((double)rawData - 32768) * _zUnit / 1000, roundDigit);
    }

    public static class KeyRawBMP
    {

    }

}
