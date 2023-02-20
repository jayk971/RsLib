using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
namespace RsLib.Common
{
    public static class CSVReader
    {
        public static List<string[]> Load(string filePath,int expectColumn,bool enableSplit)
        {
            List<string[]> output = new List<string[]>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while(!sr.EndOfStream)
                {
                    string readData = sr.ReadLine();
                    if(enableSplit)
                    {
                        string[] splitData = readData.Split(',');
                        if(splitData.Length == expectColumn)
                        {
                            output.Add(splitData);
                        }
                    }
                    else
                    {
                        output.Add(new string[] { readData });
                    }
                }
            }

            return output;
        }
        public static List<string[]> Load(string filePath, int expectColumn, bool enableSplit,int startReadIndex,int readLength)
        {
            List<string[]> output = new List<string[]>();
            int rowCount = 0;
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string readData = sr.ReadLine();
                    if (rowCount >= startReadIndex && rowCount < startReadIndex + readLength)
                    {
                        if (enableSplit)
                        {
                            string[] splitData = readData.Split(',');
                            if (splitData.Length == expectColumn)
                            {
                                output.Add(splitData);
                            }
                        }
                        else
                        {
                            output.Add(new string[] { readData });
                        }
                    }
                    if (rowCount >= startReadIndex + readLength) break;
                    rowCount++;
                }
            }

            return output;
        }

    }
}
