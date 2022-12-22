using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
namespace RsLib.Common
{
    public static class RsLibFile
    {
        public static string MainDisk
        {
            get
            {
                if(Directory.Exists("d:\\"))
                {
                    return "d:\\";
                }
                else
                {
                    return "c:\\";
                }
            }
        }
        public static string MainFolder = MainDisk + "RLib";
        public static string Lib3rdParty = MainFolder + "\\Lib";
        public static void CheckNLog(string destFolder)
        {
            if(Directory.Exists(destFolder))
            {
                string nlogDll = $"{Lib3rdParty}\\NLog.dll";
                string nlogConfig = $"{Lib3rdParty}\\NLog.config";
                string nlogDestDll = $"{destFolder}\\NLog.dll";
                string nlogDestConfig = $"{destFolder}\\NLog.config";

                if (File.Exists(nlogDestDll) == false) File.Copy(nlogDll, nlogDestDll);
                if (File.Exists(nlogDestConfig) == false) File.Copy(nlogConfig, nlogDestConfig);
            }
        }
        public static void CheckYaml(string destFolder)
        {
            if (Directory.Exists(destFolder))
            {
                string yamlDll = $"{Lib3rdParty}\\YamlDotNet.dll";
                string yamlXml = $"{Lib3rdParty}\\YamlDotNet.xml";
                string yamlDestDll = $"{destFolder}\\YamlDotNet.dll";
                string yamlDestXml = $"{destFolder}\\YamlDotNet.xml";

                if (File.Exists(yamlDestDll) == false) File.Copy(yamlDll, yamlDestDll);
                if (File.Exists(yamlDestXml) == false) File.Copy(yamlXml, yamlDestXml);
            }
        }
        public static void CheckJson(string destFolder)
        {
            if (Directory.Exists(destFolder))
            {
                string jsonDll = $"{Lib3rdParty}\\Newtonsoft.Json.dll";
                string jsonXml = $"{Lib3rdParty}\\Newtonsoft.Json.xml";
                string jsonDestDll = $"{destFolder}\\Newtonsoft.Json.dll";
                string jsonDestXml = $"{destFolder}\\Newtonsoft.Json.xml";

                if (File.Exists(jsonDestDll) == false) File.Copy(jsonDll, jsonDestDll);
                if (File.Exists(jsonDestXml) == false) File.Copy(jsonXml, jsonDestXml);
            }
        }
        public static void CheckDotNetZip(string destFolder)
        {
            if (Directory.Exists(destFolder))
            {
                string Dll = $"{Lib3rdParty}\\DotNetZip.dll";
                string Xml = $"{Lib3rdParty}\\DotNetZip.xml";
                string DestDll = $"{destFolder}\\DotNetZip.dll";
                string DestXml = $"{destFolder}\\DotNetZip.xml";

                if (File.Exists(DestDll) == false) File.Copy(Dll, DestDll);
                if (File.Exists(DestXml) == false) File.Copy(Xml, DestXml);
            }
        }
        public static void CheckNetDxf(string destFolder)
        {
            if (Directory.Exists(destFolder))
            {
                string Dll = $"{Lib3rdParty}\\netDxf.netstandard.dll";
                string Xml = $"{Lib3rdParty}\\netDxf.netstandard.xml";
                string DestDll = $"{destFolder}\\netDxf.netstandard.dll";
                string DestXml = $"{destFolder}\\netDxf.netstandard.xml";

                if (File.Exists(DestDll) == false) File.Copy(Dll, DestDll);
                if (File.Exists(DestXml) == false) File.Copy(Xml, DestXml);
            }
        }
        public static void CheckOxyPlot(string destFolder)
        {
            if (Directory.Exists(destFolder))
            {
                string Dll = $"{Lib3rdParty}\\OxyPlot.dll";
                string Xml = $"{Lib3rdParty}\\OxyPlot.WindowsForms.dll";
                string DestDll = $"{destFolder}\\OxyPlot.dll";
                string DestXml = $"{destFolder}\\OxyPlot.WindowsForms.dll";

                if (File.Exists(DestDll) == false) File.Copy(Dll, DestDll);
                if (File.Exists(DestXml) == false) File.Copy(Xml, DestXml);
            }
        }
        public static void CheckAccord(string destFolder)
        {
            if (Directory.Exists(destFolder))
            {
                string Dll = $"{Lib3rdParty}\\Accord.dll";
                string DestDll = $"{destFolder}\\Accord.dll";

                string Dll_2 = $"{Lib3rdParty}\\Accord.MachineLearning.dll";
                string DestDll_2 = $"{destFolder}\\Accord.MachineLearning.dll";

                string Dll_3 = $"{Lib3rdParty}\\Accord.Math.Core.dll";
                string DestDll_3 = $"{destFolder}\\Accord.Math.Core.dll";

                string Dll_4 = $"{Lib3rdParty}\\Accord.Math.dll";
                string DestDll_4 = $"{destFolder}\\Accord.Math.dll";

                string Dll_5 = $"{Lib3rdParty}\\Accord.Statistics.dll";
                string DestDll_5 = $"{destFolder}\\Accord.Statistics.dll";

                if (File.Exists(DestDll) == false) File.Copy(Dll, DestDll);
                if (File.Exists(DestDll_2) == false) File.Copy(Dll_2, DestDll_2);
                if (File.Exists(DestDll_3) == false) File.Copy(Dll_3, DestDll_3);
                if (File.Exists(DestDll_4) == false) File.Copy(Dll_4, DestDll_4);
                if (File.Exists(DestDll_5) == false) File.Copy(Dll_5, DestDll_5);
            }
        }
        public static void CheckPCIDask(string destFolder)
        {
            if (Directory.Exists(destFolder))
            {
                string Dll = $"{Lib3rdParty}\\PCI-Dask64.dll";
                string DestDll = $"{destFolder}\\PCI-Dask64.dll";

                if (File.Exists(DestDll) == false) File.Copy(Dll, DestDll);
            }
        }

    }
}
