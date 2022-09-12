using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RsLib.AlarmMgr;
using System.Threading;
namespace AlarmManager
{
    public partial class Form1 : Form
    {
        AlarmControl ac = new AlarmControl();
        AlarmBriefInfoControl abc = new AlarmBriefInfoControl();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ac.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(ac, 0, 0);
            abc.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(abc, 0, 1);

            Array ttt = Enum.GetValues(typeof(ErrorCode));
            string[] tstt = Enum.GetNames(typeof(ErrorCode));
            List<int> sss = new List<int>();
            for(int i = 0; i < ttt.Length;i++)
            {
                object s =  ttt.GetValue(i);
                Type f =  s.GetType();
                sss.Add((int)s);
            }
            //List<int> sss = ttt.ToList();

            AlarmHistory.CreateNewTableFile(sss);
        }

        bool iii = false;
        private void button1_Click(object sender, EventArgs e)
        {
            bool tt = false;
            tt = SpinWait.SpinUntil(() => true, 10000);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
    public enum ErrorCode : int
    {        /// <summary>
             /// Alarm Code Section
             /// </summary>
        Exception = -1,
        NoError = 0,

        #region 系統流程相關 1000
        #region Alarm
        TestAlarm = 1000,
        UndefinedException = 1001,
        SystemInitialFail = 1002,
        SystemAutoRunFail = 1003,
        EMOActive = 1004,
        SafetyChainBreak = 1005,
        FacilityPressureLow = 1006,

        AvsDLInitFail = 1007,
        PowerOFF = 1008,
        #endregion
        #region Warning
        PCPerformanceLow = 1500,
        HDStorageLow = 1501,
        #endregion
        #endregion

        #region 演算法 控制邏輯 帳料系統 2000
        #region Alarm
        AvlSetModelException = 2002,
        SetScanCloudException = 2003,
        AvlLoadImageException = 2004,
        AvlFindMarkException = 2005,
        CalculateMark2DDisException = 2006,
        Show2DResultFail = 2007,
        FindMarkException = 2008,
        ResultObjectCountNotZero = 2009,
        MaterialDetectedButResultCountZero = 2010,
        DelayQObjectCountNotZero = 2011,
        DelayObjectWaitResultTimeOut = 2012,
        DelayObjectNotMatchResult = 2013,
        YolactError = 2014,
        FileDetectButInputCountZero = 2015,
        YolactExited = 2016,
        #endregion

        #region Warning
        CalculationDelay = 2501,
        ModelIdentifyFileTimeOut = 2502,
        DelayObjectWaitResultDelay = 2503,
        YolactWarn = 2504,
        #endregion
        #endregion

        #region 視覺系統 3000
        #region Alarm
        X8kError = 3001,
        X8kNotRun = 3002,
        X8kNotReady = 3003,
        #endregion
        #region Warning

        #endregion

        #endregion

        #region 伺服 IO 4000
        #region Alarm
        PCI7433InitialFail = 4001,
        PCI7234InitialFail = 4002,
        ReadDiException = 4003,
        WriteDoException = 4004,
        ReadDiFail = 4005,
        WriteDoFail = 4006,
        BlockDownTimeOut = 4007,
        BlockUpTimeOut = 4008,
        RejecterBackwardTimeOut = 4009,
        RejecterForwardTimeOut = 4010,
        NGClassifyBackwardTimeOut = 4011,
        NGClassifyForwardTimeOut = 4012,

        OutMotorAlarm = 4013,
        InMotorAlarm = 4014,
        OutMotorAlarmResetFail = 4015,
        InMotorAlarmResetFail = 4016,

        #endregion
        #region Warning

        #endregion
        #endregion

        #region Robot 5000
        #region Alarm

        #endregion
        #region Warning
        #endregion
        #endregion

        #region 參數 設定檔 6000
        #region Alarm
        SystemConfigFileLost = 6001,
        RecipeFileNotExist = 6002,
        AvlModelFolderNotExist = 6003,
        ResultLogSaveException = 6004,
        Draw2DMarkException = 6005,
        SetMarkRegionException = 6006,
        LocateMarkException = 6007,
        ModelIdBatFileNotExist = 6008,

        #endregion

        #region Warning


        #endregion
        #endregion

        #region 網路通訊 7000
        #region Alarm
        FTPUploadTimeOut = 7001,
        #endregion
        #region Warning
        FTPDelay = 7501,

        #endregion
        #endregion

        #region 製程 8000
        #region Alarm
        OutConveyorSensorOffTimeOut = 8001,
        InConveyorSensorOffTimeOut = 8002,
        MaterialDelayTimeOut = 8003,
        #endregion
        #region Warning
        ImageCannotDelete = 8501,

        #endregion
        #endregion

    }

}
