using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// for DLL
using System.Runtime.InteropServices;

// for MessageBox
using System.Windows.Forms;

// for File I/O
using System.IO;


public class MyFile
{

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


    public static void WriteIniValue(string filePath, string section, string key, string val)
    {
        // 若是檔案不存在
        if (!File.Exists(filePath))
        {
            MessageBox.Show("Cannot find the following file!\r\r" + filePath, "Error");
            return;
        }

        // 若有檔案存在即使section或key有誤也會照樣寫入
        WritePrivateProfileString(section, key, val, filePath);
    }

    public static bool ReadIniValue(string filePath, string section, string key, out string val)
    {
        StringBuilder temp = new StringBuilder(255);
        int intStringLength = GetPrivateProfileString(section, key, "", temp, 255, filePath);

        if (intStringLength > 0)
        {
            val = temp.ToString();
            return true;
        }
        else
        {
            MessageBox.Show("Reading IniFile Error!");
            val = "";
            return false;
        }
    }
    
    public static void CreateFile(string filePath, bool deleteExisted)
    {
        FileStream fs;

        if (File.Exists(filePath))
        {
            if (deleteExisted)
            {
                File.Delete(filePath);
                fs = File.Create(filePath);
                fs.Close();
            }
        }
        else
        {
            fs = File.Create(filePath);
            fs.Close();
        }
    }

    public static void CreateFolder(string dirPath)
    {
        if (!Directory.Exists(dirPath))
        {
            // 路徑中不存在的資料夾會順便建立
            Directory.CreateDirectory(dirPath);
        }
    }

    public static void AppendLine(string filePath, string str)
    {
        FileStream fs;
        StreamWriter sw;

        fs = File.OpenWrite(filePath);
        fs.Seek(0, SeekOrigin.End);

        sw = new StreamWriter(fs);
        sw.WriteLine(str);

        sw.Close();
        fs.Close();
    }

    public static void AppendAllLines(string filePath, List<string> stringList)
    {
        FileStream fs;
        StreamWriter sw;

        fs = File.OpenWrite(filePath);
        fs.Seek(0, SeekOrigin.End);

        sw = new StreamWriter(fs);

        for (int i = 0; i < stringList.Count; i++)
        {
            sw.WriteLine(stringList[i]);
        }

        sw.Close();
        fs.Close();
    }

    public static List<string> ReadAllLines(string filePath)
    {
        List<string> list = new List<string>();
        FileStream fileStream = File.OpenRead(filePath);
        string ext = Path.GetExtension(filePath).ToUpper();
        if (ext == ".PLY")
        {
            using (StreamReader sr = new StreamReader(fileStream))
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
                    }
                    if (readData == "end_header") break;
                }
                int pointIndex = 0;
                while (!sr.EndOfStream)
                {
                    readData = sr.ReadLine();
                    list.Add(readData);
                    pointIndex++;
                    if (pointIndex >= pointCount) break;
                }
            }
        }
        else
        {
            StreamReader streamReader = new StreamReader(fileStream);
            while (streamReader.Peek() >= 0)
            {
                list.Add(streamReader.ReadLine());
            }
            streamReader.Close();
            fileStream.Close();
        }

        return list;
    }

    public static string[] GetAllFilePaths(string folderPath)
    {
        return Directory.GetFiles(folderPath);
    }

    /// <summary>
    /// 判斷該字串是否為空行
    /// </summary>
    public static bool NullLine(String s)
    {
        if (s == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}