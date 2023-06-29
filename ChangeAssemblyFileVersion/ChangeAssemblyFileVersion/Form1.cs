using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using YamlDotNet.Serialization;
using RsLib.LogMgr;
namespace ChangeAssemblyFileVersion
{
    public partial class Form1 : Form
    {
        string currentSlnFile = "";
        string selectName = "";
        string currentSlnFolder => Path.GetDirectoryName(currentSlnFile);
        Dictionary<string,AssemblyVersion> assembly = new Dictionary<string,AssemblyVersion>();
        Form2 f2 = new Form2();
        SystemConfig config = new SystemConfig();
        LogControl _LogCtrl = new LogControl();
        public Form1()
        {
            Log.EnableUpdateUI = false;
            InitializeComponent();
            _LogCtrl.Dock = DockStyle.Fill;
            panel1.Controls.Add( _LogCtrl );


            f2.VersionUpdated += F2_VersionUpdated;
            config.FileLoaded += Config_FileLoaded;
            config.LoadYaml();
            Log.Start();
            Log.EnableUpdateUI = true;

        }

        private void Config_FileLoaded()
        {
            comboBox1.Items.Clear();

            if (config.slnFiles.Count >0)
            {
                for(int i = 0; i < config.slnFiles.Count; i++)
                {
                    comboBox1.Items.Add(config.slnFiles[i]);
                }
                comboBox1.SelectedIndex = 0;
                currentSlnFile = comboBox1.SelectedItem.ToString();
                parseSolutionFile();

            }
        }

        private void F2_VersionUpdated(string name ,int arg1, int arg2, int arg3, int arg4)
        {
            if(assembly.ContainsKey(name))
            {
                assembly[selectName].Main = arg1;
                assembly[selectName].Sub = arg2;
                assembly[selectName].Build = arg3;
                assembly[selectName].Revise = arg4;
                assembly[selectName].WriteToFile();
                parseSolutionFile();
            }
        }

        private void btn_SelectSLN_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "Visual Studio Solution|*.sln";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(op.FileName))
                    {
                        if(!config.slnFiles.Contains(op.FileName))
                        {
                            config.SlnAdd(op.FileName);
                        }

                    }
                }
            }
        }

        private void parseSolutionFile()
        {
            Log.Add($"Start parse solution file. {currentSlnFile}", MsgLevel.Info);
            assembly.Clear();
            Dictionary<string, string> dic_Project = new Dictionary<string, string>();
            List<string> readData = new List<string>();
            using (StreamReader sr = new StreamReader(currentSlnFile))
            {
                while (!sr.EndOfStream)
                {
                    readData.Add(sr.ReadLine());
                }
            }

            for (int i = 0; i < readData.Count; i++)
            {
                string readLine = readData[i];
                if (readLine.Contains("Project(\""))
                {
                    Tuple<string, string> temp = parseSolutionData(readLine);
                    if (dic_Project.ContainsKey(temp.Item1) == false)
                        dic_Project.Add(temp.Item1, temp.Item2);
                    else
                        Log.Add($"Dictionary already contain key \"{temp.Item1}\"", MsgLevel.Warn);

                }
            }
            loadAssemblyFile(dic_Project);
            updateTreeView();
        }

        private Tuple<string, string> parseSolutionData(string readLine)
        {
            Tuple<string, string> output;
            string[] parseEqual = readLine.Split('=');
            if (parseEqual.Length == 2)
            {
                string[] parseComma = parseEqual[1].Split(',');
                if (parseComma.Length == 3)
                {
                    output = new Tuple<string, string>(clearQuotationAndSpace(parseComma[0]),
                        clearQuotationAndSpace(parseComma[1]));
                }
                else output = new Tuple<string, string>("", "");
            }
            else output = new Tuple<string, string>("", "");

            return output;
        }

        private string clearQuotationAndSpace(string str)
        {
            string clearQuotation = str.Replace("\"", "");
            string clearSpace = clearQuotation.Replace(" ", "");
            return clearSpace;
        }
        private void loadAssemblyFile(Dictionary<string, string> dic_Project)
        {
            Log.Add($"Start load each AssemblyInfo.cs", MsgLevel.Info);
            foreach (KeyValuePair<string, string> kvp in dic_Project)
            {
                string projectFolder = Path.GetDirectoryName($"{currentSlnFolder}\\{kvp.Value}");
                string PropertiesFolder = $"{projectFolder}\\Properties";
                bool checkFolder = Directory.Exists(PropertiesFolder);
                if (checkFolder)
                {
                    string assemblyInfoFile = $"{PropertiesFolder}\\AssemblyInfo.cs";
                    parseAeemblyInfocs(assemblyInfoFile);
                }
            }
        }
        private void parseAeemblyInfocs(string filePath)
        {
            List<string> readData = new List<string>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        readData.Add(sr.ReadLine());
                    }
                }
                string name = "";
                string versionInfo = "";
                bool parseNameOK = false;
                bool parseVersionOK = false;
                for (int i = 0; i < readData.Count; i++)
                {
                    string readLine = readData[i];
                    if (readLine.Contains("assembly: AssemblyProduct("))
                    {
                        name = parseQuotation(readLine);
                        parseNameOK = true;
                    }
                    if (readLine.Contains("assembly: AssemblyFileVersion("))
                    {
                        versionInfo = parseQuotation(readLine);
                        parseVersionOK = true;
                    }
                }
                if (parseNameOK && parseVersionOK)
                {
                    assembly.Add(name,new AssemblyVersion(name, versionInfo, filePath));
                }
            }
        }
        private string parseQuotation(string str)
        {
            string[] splitData = str.Split('\"');
            if (splitData.Length == 3)
            {
                string versionStr = splitData[1];
                return versionStr;
            }
            else return "";
        }
        private void updateTreeView()
        {
            Log.Add("Update treeview", MsgLevel.Info);
            treeView1.Nodes.Clear();
            foreach (KeyValuePair<string, AssemblyVersion> kvp in assembly)
            {
                TreeNode nameNode = new TreeNode(kvp.Value.Name);
                nameNode.Name = kvp.Value.Name;
                nameNode.BackColor = Color.YellowGreen;

                TreeNode versionNode = new TreeNode(kvp.Value.VersionString);
                nameNode.Nodes.Add(versionNode);
                treeView1.Nodes.Add(nameNode);
            }
            treeView1.ExpandAll();
        }
        private void btn_UpdateAll_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, AssemblyVersion> kvp in assembly)
            {
                kvp.Value.Update();
                kvp.Value.WriteToFile();
            }
            parseSolutionFile();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if(treeView1.SelectedNode.Level == 0)
            {
                selectName = treeView1.SelectedNode.Name;
            }
            else if(treeView1.SelectedNode.Level == 1)
            {
                selectName = treeView1.SelectedNode.Parent.Name;
            }
            else
            {

            }
            if(assembly.ContainsKey(selectName))
            {
                assembly[selectName].Update();
                assembly[selectName].WriteToFile();
            }
            parseSolutionFile();
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (treeView1.SelectedNode.Level == 0)
            {
                selectName = treeView1.SelectedNode.Name;
            }
            else if (treeView1.SelectedNode.Level == 1)
            {
                selectName = treeView1.SelectedNode.Parent.Name;
            }
            else
            {

            }
            if (assembly.ContainsKey(selectName))
            {
                 f2.SetTextbox(selectName,
                    assembly[selectName].Main,
                    assembly[selectName].Sub,
                    assembly[selectName].Build,
                    assembly[selectName].Revise);
                f2.ShowDialog();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;

            currentSlnFile = comboBox1.SelectedItem.ToString();
            try
            {
                parseSolutionFile();
            }
            catch (Exception ex) 
            {
                Log.Add("Parse solution file exception.", MsgLevel.Alarm, ex);
            }
        }

        private void btn_DeleteSln_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;
            config.SlnDelete(comboBox1.SelectedIndex);
        }

        private void btn_ClearSln_Click(object sender, EventArgs e)
        {
            config.SlnClear();
        }
    }
    internal class AssemblyVersion
    {
        public string Name = "";
        public string AssemblyFilePath = "";
        public int Main = 0;
        public int Sub = 0;
        public int Build = 0;
        public int Revise = 0;
        public string VersionString => $"{Main}.{Sub}.{Build}.{Revise}";
        string writeVersionString => $"[assembly: AssemblyFileVersion(\"{VersionString}\")]";
        public AssemblyVersion(string name,string versionStr,string assemblyFile)
        {
            Name = name;
            AssemblyFilePath = assemblyFile;
            parseDot(versionStr);
        }
        void parseDot(string str)
        {
            string[] splitData = str.Split('.');
            if(splitData.Length == 4)
            {
                Main = int.Parse(splitData[0]);
                Sub = int.Parse(splitData[1]);
                Build = int.Parse(splitData[2]);
                Revise = int.Parse(splitData[3]);
            }
        }
        public void Update()
        {
            DateTime dt = DateTime.Now;
            int newBuild = (dt.Year - 2000)*1000 + dt.DayOfYear;
            if (Build != newBuild) Revise = 0;
            else Revise++;

            Build = newBuild;
        }

        public void WriteToFile()
        {
            List<string> readData = new List<string>();
            using (StreamReader sr = new StreamReader(AssemblyFilePath))
            {
                while (!sr.EndOfStream)
                {
                    string readLine = sr.ReadLine();
                    if (readLine.Contains("assembly: AssemblyFileVersion("))
                    {
                        readLine = writeVersionString;
                    }
                    readData.Add(readLine);
                }
            }
            using (StreamWriter sw = new StreamWriter(AssemblyFilePath, false, Encoding.Default))
            {
                for (int i = 0; i < readData.Count; i++)
                {
                    sw.WriteLine(readData[i]);
                }
                sw.Flush();
            }
        }
    }
    [Serializable]
    internal class SystemConfig
    {
        static readonly string folderApp = System.Environment.CurrentDirectory;
        static readonly string folderConfig = folderApp + "\\Config";
        static readonly string fileConfig = folderConfig + "\\system.yaml";

        public List<string> slnFiles = new List<string>();
        public event Action FileLoaded;
        public SystemConfig()
        {
            checkFolder();
        }
        public void SlnAdd(string filePath)
        {
            if(!slnFiles.Contains(filePath))
            {
                slnFiles.Add(filePath);
                SaveYaml();
                LoadYaml();
            }
        }
        public void SlnDelete(int index)
        {
            if(slnFiles.Count > index)
            {
                slnFiles.RemoveAt(index);
                SaveYaml();
                LoadYaml();
            }
        }
        public void SlnClear()
        {
            slnFiles.Clear();
            SaveYaml();
            LoadYaml();
        }
        public void LoadYaml()
        {
            if (!File.Exists(fileConfig))
            {
                SaveYaml();
            }
            string ReadData = "";
            using (StreamReader sr = new StreamReader(fileConfig, Encoding.Default))
            {
                ReadData = sr.ReadToEnd();
            }
            var deserializer = new DeserializerBuilder()
                .Build();

            //yml contains a string containing your YAML
            var p = deserializer.Deserialize<SystemConfig>(ReadData);

            slnFiles = p.slnFiles;
            FileLoaded?.Invoke();
        }
        public void SaveYaml()
        {
            using (StreamWriter sw = new StreamWriter(fileConfig, false, Encoding.Default))
            {
                var serializer = new SerializerBuilder()
                    .Build();
                var yaml = serializer.Serialize(this);
                sw.WriteLine(yaml);
                sw.Flush();
            }
        }
        void checkFolder()
        {
            if (!Directory.Exists(folderConfig)) Directory.CreateDirectory(folderConfig);
        }

    }

}
