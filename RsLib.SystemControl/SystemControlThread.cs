using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RsLib.Common;
using RsLib.AlarmMgr;
using RsLib.BaseType;
using RsLib.LogMgr;
namespace RsLib.SystemControl
{
    public static class SystemCore
    {
        #region Stopwatch
        static Stopwatch _RunTime = new Stopwatch();
        public static string RunTimeString => _RunTime.Elapsed.ToString(@"hh\:mm\:ss");
        public static TimeSpan RunTime => _RunTime.Elapsed;
        public static double RunTime_sec => RunTime.TotalSeconds;

        static Stopwatch _DownTime = new Stopwatch();
        public static string DownTimeString => _DownTime.Elapsed.ToString(@"hh\:mm\:ss");
        public static TimeSpan DownTime => _DownTime.Elapsed;
        public static double DownTime_sec => DownTime.TotalSeconds;

        static Stopwatch _IdleTime = new Stopwatch();
        public static string IdleTimeString => _IdleTime.Elapsed.ToString(@"hh\:mm\:ss");
        public static TimeSpan IdleTime => _IdleTime.Elapsed;
        public static double IdleTime_sec => IdleTime.TotalSeconds;

        static Stopwatch _TempIdleTime = new Stopwatch();
        public static TimeSpan TempIdleTime => _TempIdleTime.Elapsed;
        public static double TempIdleTime_sec => TempIdleTime.TotalSeconds;
        #endregion
        public static bool IsCalculateBusy = false;
        public static string MachineStatus { get; private set; } = "Not Initial";
        public static User CurrentUser { get; private set; } = User.OP;
        public static int IdleTimeLimit { get; set; } = 60000;

        public static bool IsEmo { get; private set; } = false;

        static bool _IsAlarm = false;
        public static bool IsAlarm
        {
            get { return _IsAlarm; }
            set
            {
                _IsAlarm = value;
                AlarmOccured?.Invoke(value);
                if (value) alarmHandle();
            }
        }

        static bool _IsWarning = false;
        public static bool IsWarning
        {
            get { return _IsWarning; }
            set
            {
                _IsWarning = value;
                WarningOccured?.Invoke(value);
                if (value) warningHandle();
            }
        }
        public static int ThisMonth { get; private set; }
        public static int ThisDay { get; private set; }

        #region event
        public static event Action AfterTodayDateChanged;
        public static event Action BeforeTodayDateChanged;
        public static event Action<bool> AlarmOccured;
        public static event Action<bool> WarningOccured;
        public static event Action<MachineStatus, MachineStatus> BeforeStatusChanged;
        public static event Action<MachineStatus> AfterStatusChanged;
        #endregion

        static BasicThread _Core = new BasicThread();
        public static void Initial(Action beforInitial)
        {
            if (beforInitial != null) beforInitial();
            _Core.Initial("SystemCore", null, inLoopFunction, null, 500);
            AlarmHistory.AlarmQueueUpdated += ErrorHistory_ErrorQueueUpdated;
            checkToday();
            _Core.Start();
            _Core.BeforeStatusChanged += _Core_BeforeStatusChanged;
            _Core.AfterStatusChanged += _Core_AfterStatusChanged;
            _Core.ChangeSystemStatus(Common.MachineStatus.InitialBusy);
        }

        private static void _Core_BeforeStatusChanged(MachineStatus before, MachineStatus after)
        {
            BeforeStatusChanged?.Invoke(before, after);
        }

        private static void _Core_AfterStatusChanged(MachineStatus current)
        {
            AfterStatusChanged?.Invoke(current);
        }

        public static void SetInitialDone()
        {
            SwitchToManual();
        }
        static void inLoopFunction()
        {
            checkToday();

            if (IsAlarm)
            {
                downTimeCount();
            }
            else
            {
                switch (_Core.CurrentStatus)
                {
                    case Common.MachineStatus.NotInitial:
                        resetTimer();
                        break;

                    case Common.MachineStatus.InitialBusy:

                        break;

                    case Common.MachineStatus.Manual:

                        idleTimeCount();
                        IsCalculateBusy = false;
                        break;

                    case Common.MachineStatus.Auto:

                        idleTimeCount();
                        IsCalculateBusy = false;
                        break;

                    case Common.MachineStatus.Run:

                        if (IsCalculateBusy == false)
                        {
                            if (_IdleTime.IsRunning == false)
                            {
                                if (_TempIdleTime.IsRunning == false) _TempIdleTime.Restart();
                                else
                                {
                                    if (TempIdleTime.TotalMilliseconds >= IdleTimeLimit)
                                    {
                                        idleTimeCount();
                                    }
                                }
                            }
                        }
                        else runTimeCount();
                        break;

                    default:
                        downTimeCount();
                        break;
                }
            }
        }
        static void resetTimer()
        {
            if (DownTime_sec != 0) _DownTime.Reset();
            if (IdleTime_sec != 0) _IdleTime.Reset();
            if (RunTime_sec != 0) _RunTime.Reset();
            if (TempIdleTime_sec != 0) _TempIdleTime.Reset();
            IsCalculateBusy = false;
        }
        static void restartTimer()
        {
            if (_DownTime.IsRunning) _DownTime.Restart();
            if (_IdleTime.IsRunning) _IdleTime.Restart();
            if (_RunTime.IsRunning) _RunTime.Restart();
            if (_TempIdleTime.IsRunning) _TempIdleTime.Restart();
        }
        static void checkToday()
        {
            if (ThisMonth != DateTime.Now.Month || ThisDay != DateTime.Now.Day)
            {
                ThisMonth = DateTime.Now.Month;
                ThisDay = DateTime.Now.Day;
                if (_Core.CurrentStatus >= Common.MachineStatus.Manual)
                {
                    BeforeTodayDateChanged?.Invoke();
                    restartTimer();
                }
                AfterTodayDateChanged?.Invoke();
            }
        }
        static void runTimeCount()
        {
            MachineStatus = "Run";

            if (_RunTime.IsRunning == false) _RunTime.Start();
            if (_DownTime.IsRunning == true) _DownTime.Stop();
            if (_IdleTime.IsRunning == true) _IdleTime.Stop();
            if (_TempIdleTime.IsRunning == true) _TempIdleTime.Stop();
        }
        static void downTimeCount()
        {
            MachineStatus = "Down";
            if (_RunTime.IsRunning == true) _RunTime.Stop();
            if (_DownTime.IsRunning == false) _DownTime.Start();
            if (_IdleTime.IsRunning == true) _IdleTime.Stop();
            if (_TempIdleTime.IsRunning == true) _TempIdleTime.Stop();

        }
        static void idleTimeCount()
        {
            MachineStatus = "Idle";

            if (_RunTime.IsRunning == true) _RunTime.Stop();
            if (_DownTime.IsRunning == true) _DownTime.Stop();
            if (_IdleTime.IsRunning == false) _IdleTime.Start();
        }
        static void ErrorHistory_ErrorQueueUpdated(LockQueue<AlarmItem> errors)
        {
            List<AlarmItem> ErrorList = errors.ToList();
            if (ErrorList.Count == 0)
            {
                IsAlarm = false;
                IsWarning = false;
            }
            else
            {
                for (int i = 0; i < ErrorList.Count; i++)
                {
                    AlarmItem _Item = ErrorList[i];

                    if (_Item.Level == MsgLevel.Alarm) IsAlarm = true;
                    if (_Item.Level == MsgLevel.Warn) IsWarning = true;
                }
            }

        }
        static void alarmHandle()
        {
            if (_Core.CurrentStatus == Common.MachineStatus.Run)
            {
                SwitchToAuto();
            }
        }
        static void warningHandle()
        {

        }
        public static void SwitchToManual()
        {
            if(_Core.CurrentStatus == Common.MachineStatus.InitialBusy || _Core.CurrentStatus == Common.MachineStatus.Auto)
                _Core.ChangeSystemStatus(Common.MachineStatus.Manual);
        }
        public static void SwitchToAuto()
        {
            if(_Core.CurrentStatus == Common.MachineStatus.Manual || _Core.CurrentStatus == Common.MachineStatus.Run)
                _Core.ChangeSystemStatus(Common.MachineStatus.Auto);
        }
        public static void SwitchToRun()
        {
            if (_Core.CurrentStatus != Common.MachineStatus.Auto) return;

            if (IsAlarm)
            {
                AlarmHistory.Add(Error.System.SwitchRunModeFail, "Alarm exist");
                return;
            }

            _Core.ChangeSystemStatus(Common.MachineStatus.Run);
            SpinWait.SpinUntil(() => false, 100);

        }

        public static void Exit()
        {
            _Core.ChangeSystemStatus(Common.MachineStatus.NotInitial);
            _Core.Stop();
        }
    }


    public class BasicThread
    {
        public MachineStatus CurrentStatus { get; private set; } = MachineStatus.NotInitial;
        public bool IsAlarm { get; set; } = false;
        public bool IsWarning { get; set; } = false;
        public bool IsEMO { get; set; } = false;
        public bool IsSafetyChainBroken { get; set; } = false;
        public bool IsDebug { get;  set; } = false;
        public string Name { get; private set; } = "";
        bool _EnableTd = false;
        public bool IsTdRunning { get; private set; } = false;
        public string Status { get; protected set; } = "Not Initial";
        Action BeforeLoop = null;
        Action InLoop = null;
        Action AfterLoop = null;
        public uint LoopInterval { get; private set; } = 100;
        public bool IsInitialized { get; private set; } = false;
        public event Action<MachineStatus,MachineStatus> BeforeStatusChanged;
        public event Action<MachineStatus> AfterStatusChanged;
        public void Initial(string name, Action beforeLoop, Action inLoop, Action afterLoop, uint loopInterval)
        {
            Name = name;
            BeforeLoop = beforeLoop;
            InLoop = inLoop;
            AfterLoop = afterLoop;
            LoopInterval = loopInterval <= 0 ? 1 : loopInterval;
            IsInitialized = true;
        }
        public bool Start()
        {
            if (IsTdRunning) return false;
            if (IsInitialized == false) return false;
            _EnableTd = true;
            ThreadPool.QueueUserWorkItem(Run);
            return true;
        }
        public void Stop()
        {
            if (IsInitialized == false) return;
            if (IsTdRunning == false) return;
            _EnableTd = false;
            SpinWait.SpinUntil(() => !IsTdRunning, 1000);
        }
        public void SetStatus(string status)
        {
            Status = status;
        }
        public virtual void Run(object obj)
        {
            if (BeforeLoop != null) BeforeLoop();
            Status = $"Thread {Name} Starting";
            while (_EnableTd)
            {
                IsTdRunning = true;
                Status = "Running";
                if (InLoop != null) InLoop();
                SpinWait.SpinUntil(() => false, (int)LoopInterval);
            }
            if (AfterLoop != null) AfterLoop();
            Status = $"Thread {Name} Stopped";
            IsTdRunning = false;
        }
        public void ChangeSystemStatus(MachineStatus systemStatus)
        {
            BeforeStatusChanged?.Invoke(CurrentStatus,systemStatus);
            CurrentStatus = systemStatus;
            AfterStatusChanged?.Invoke(CurrentStatus);
        }

    }
}
