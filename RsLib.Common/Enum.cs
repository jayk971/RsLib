﻿namespace RsLib.Common
{
    public enum eCoordPlane : int
    {
        None = -1,
        XY = 0,
        XZ,
        YZ,
        YX,
        ZX,
        ZY
    }
    public enum eMachineStatus : int
    {
        NotInitial = 0,      //尚未初始化
        InitialBusy,     //初始化中
        Manual,       //已初始化，進入手動模式
        Auto,            //自動模式
        Run,             //啟動運轉
    }
    public enum eUser : int
    {
        OP = 0,
        Engineer,
        Administrator,
    }
    public enum eTimeOutType : int
    {
        TimeOut = -2,
        InTime = -1,
    }
    public partial struct Error
    {
        public enum System : int 
        {
            Exception = -1,
            NoError = 0,
            #region 系統流程相關 1000
            SysyemAlarm = 1000,
            #region Alarm
            UndefinedException,
            SystemInitialFail,
            SwitchRunModeFail,
            EMOActive,
            SafetyChainBreak,
            FacilityPressureLow,
            SystemConfigLost,
            #endregion
            SystemWarn = 1500,
            #region Warning
            PCPerformanceLow,
            HDStorageLow,
            #endregion
            #endregion
        }

    }

}
