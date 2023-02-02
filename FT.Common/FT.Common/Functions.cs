using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

using System.Text;
using System.Diagnostics;
using System.Threading;
using RsLib.LogMgr;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Net;
namespace RsLib.Common
{
    public class FT_StopWatch:Stopwatch
    {        /// <summary>
             /// T1 Elapsed Time + T2 Elapsed Time
             /// </summary>
             /// <param name="t1">Stopwatch 1</param>
             /// <param name="t2">Stopwatch 2</param>
             /// <returns></returns>

        public static TimeSpan operator +(FT_StopWatch t1, FT_StopWatch t2) => t1.Elapsed + t2.Elapsed;
        /// <summary>
        /// T1 Elapsed Time - T2 Elapsed Time
        /// </summary>
        /// <param name="t1">Stopwatch 1</param>
        /// <param name="t2">Stopwatch 2</param>
        /// <returns></returns>
        public static TimeSpan operator -(FT_StopWatch t1, FT_StopWatch t2) => t1.Elapsed - t2.Elapsed;
        public string ToString_HHmmss() => $"{Elapsed.Hours:00}:{Elapsed.Minutes:00}:{Elapsed.Seconds:00}";
    }
    public class FT_Functions
    {
        public static bool IsIPLegal(string ip)
        {
            IPAddress parsedIP = new IPAddress(new byte[] { 127, 0, 0, 1 });
            bool isIPlegal = IPAddress.TryParse(ip, out parsedIP);
            return isIPlegal;
        }
        public static bool PingOK(string ip,int byteSize,bool enableWriteToLog = false)
        {
            bool isIPlegal = IsIPLegal(ip);

            if (isIPlegal)
            {
                if (byteSize >= 65000) byteSize = 64999;
                byte[] pingByte = new byte[byteSize];
                Random rd = new Random();
                rd.NextBytes(pingByte);
                Ping ping = new Ping();
                PingReply pingReply = ping.Send(ip, 200, pingByte);
                if (pingReply.Status == IPStatus.Success)
                {
                    if(enableWriteToLog) Log.Add($"Ping {ip} - {pingReply.RoundtripTime} ms.", MsgLevel.Trace);
                    return true;
                }
                else
                {
                    if (enableWriteToLog) Log.Add($"Ping {ip} fail. Due to {pingReply.Status}.", MsgLevel.Warn);
                    return false;
                }
            }
            else
            {
                if (enableWriteToLog) Log.Add($"IP \"{ip}\" is illegal.", MsgLevel.Alarm);
                return false;
            }
        }
        public static void OpenFolder(string folderPath)
        {
            string ExplorerFile = "c:\\Windows\\explorer.exe";
            System.Diagnostics.Process.Start(ExplorerFile, folderPath);
        }
        public static List<ShoeSize> GetAllShoeSizeEnum()
        {
            List<ShoeSize> output = new List<ShoeSize>();
            string[] name = Enum.GetNames(typeof(ShoeSize));
            for(int i = 0; i < name.Length; i++)
            {
                ShoeSize ss = (ShoeSize)Enum.Parse(typeof(ShoeSize), name[i]);
                output.Add(ss);
            }
            return output;
        }
        public static List<ShoeSizeIndex> GetAllShoeSizeIndexEnum()
        {
            List<ShoeSizeIndex> output = new List<ShoeSizeIndex>();
            string[] name = Enum.GetNames(typeof(ShoeSizeIndex));
            for (int i = 0; i < name.Length; i++)
            {
                ShoeSizeIndex ssi = (ShoeSizeIndex)Enum.Parse(typeof(ShoeSizeIndex), name[i]);
                output.Add(ssi);
            }
            return output;
        }
        /// <summary>
        /// 將尺寸 enum 改為字串, 若有半號則有 T
        /// </summary>
        /// <param name="size">ShoeSize Enum</param>
        /// <returns>size string. ex: 9, 9T</returns>
        public static string ShoeSize2StringWithT(ShoeSize size)
        {
            int i = (int)size;
            int a = i / 10;
            int b = i % 10;
            string s = a.ToString();
            if (b == 5) s += "T";
            return s;
        }
        /// <summary>
        /// 將尺寸 enum 以及左右腳合成唯一序號enum
        /// </summary>
        /// <param name="Size">尺寸 enum</param>
        /// <param name="isRight">左右腳</param>
        /// <returns></returns>
        public static ShoeSizeIndex SizeSide2Index(ShoeSize Size, bool? isRight)
        {
            if (isRight == null)
            {
                switch (Size)
                {
                    case ShoeSize._30:

                        return ShoeSizeIndex._30_RL;

                    case ShoeSize._35:
                        return ShoeSizeIndex._35_RL;

                    case ShoeSize._40:
                        return ShoeSizeIndex._40_RL;

                    case ShoeSize._45:
                        return ShoeSizeIndex._45_RL;

                    case ShoeSize._50:
                        return ShoeSizeIndex._50_RL;

                    case ShoeSize._55:
                        return ShoeSizeIndex._55_RL;

                    case ShoeSize._60:
                        return ShoeSizeIndex._60_RL;

                    case ShoeSize._65:
                        return ShoeSizeIndex._65_RL;

                    case ShoeSize._70:
                        return ShoeSizeIndex._70_RL;

                    case ShoeSize._75:
                        return ShoeSizeIndex._75_RL;

                    case ShoeSize._80:
                        return ShoeSizeIndex._80_RL;

                    case ShoeSize._85:
                        return ShoeSizeIndex._85_RL;

                    case ShoeSize._90:
                        return ShoeSizeIndex._90_RL;

                    case ShoeSize._95:
                        return ShoeSizeIndex._95_RL;

                    case ShoeSize._100:
                        return ShoeSizeIndex._100_RL;

                    case ShoeSize._105:
                        return ShoeSizeIndex._105_RL;

                    case ShoeSize._110:
                        return ShoeSizeIndex._110_RL;

                    case ShoeSize._115:
                        return ShoeSizeIndex._115_RL;

                    case ShoeSize._120:
                        return ShoeSizeIndex._120_RL;

                    case ShoeSize._125:
                        return ShoeSizeIndex._125_RL;

                    case ShoeSize._130:
                        return ShoeSizeIndex._130_RL;

                    case ShoeSize._135:
                        return ShoeSizeIndex._135_RL;

                    case ShoeSize._140:
                        return ShoeSizeIndex._140_RL;

                    case ShoeSize._150:
                        return ShoeSizeIndex._150_RL;

                    case ShoeSize._160:
                        return ShoeSizeIndex._160_RL;

                    case ShoeSize._170:
                        return ShoeSizeIndex._170_RL;

                    case ShoeSize._180:
                        return ShoeSizeIndex._180_RL;

                    case ShoeSize._190:
                        return ShoeSizeIndex._190_RL;

                    case ShoeSize._200:
                        return ShoeSizeIndex._200_RL;

                    case ShoeSize._210:
                        return ShoeSizeIndex._210_RL;

                    case ShoeSize._220:
                        return ShoeSizeIndex._220_RL;

                    case ShoeSize._230:
                        return ShoeSizeIndex._230_RL;
                    default:
                        return ShoeSizeIndex._90_RL;

                }
            }
            else
            {
                switch (Size)
                {
                    case ShoeSize._30:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._30_L;
                        else
                            return ShoeSizeIndex._30_R;

                    case ShoeSize._35:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._35_L;
                        else
                            return ShoeSizeIndex._35_R;

                    case ShoeSize._40:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._40_L;
                        else
                            return ShoeSizeIndex._40_R;

                    case ShoeSize._45:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._45_L;
                        else
                            return ShoeSizeIndex._45_R;

                    case ShoeSize._50:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._50_L;
                        else
                            return ShoeSizeIndex._50_R;

                    case ShoeSize._55:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._55_L;
                        else
                            return ShoeSizeIndex._55_R;

                    case ShoeSize._60:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._60_L;
                        else
                            return ShoeSizeIndex._60_R;

                    case ShoeSize._65:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._65_L;
                        else
                            return ShoeSizeIndex._65_R;

                    case ShoeSize._70:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._70_L;
                        else
                            return ShoeSizeIndex._70_R;

                    case ShoeSize._75:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._75_L;
                        else
                            return ShoeSizeIndex._75_R;

                    case ShoeSize._80:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._80_L;
                        else
                            return ShoeSizeIndex._80_R;

                    case ShoeSize._85:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._85_L;
                        else
                            return ShoeSizeIndex._85_R;

                    case ShoeSize._90:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._90_L;
                        else
                            return ShoeSizeIndex._90_R;

                    case ShoeSize._95:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._95_L;
                        else
                            return ShoeSizeIndex._95_R;

                    case ShoeSize._100:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._100_L;
                        else
                            return ShoeSizeIndex._100_R;

                    case ShoeSize._105:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._105_L;
                        else
                            return ShoeSizeIndex._105_R;

                    case ShoeSize._110:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._110_L;
                        else
                            return ShoeSizeIndex._110_R;

                    case ShoeSize._115:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._115_L;
                        else
                            return ShoeSizeIndex._115_R;

                    case ShoeSize._120:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._120_L;
                        else
                            return ShoeSizeIndex._120_R;

                    case ShoeSize._125:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._125_L;
                        else
                            return ShoeSizeIndex._125_R;

                    case ShoeSize._130:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._130_L;
                        else
                            return ShoeSizeIndex._130_R;

                    case ShoeSize._135:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._135_L;
                        else
                            return ShoeSizeIndex._135_R;

                    case ShoeSize._140:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._140_L;
                        else
                            return ShoeSizeIndex._140_R;

                    case ShoeSize._150:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._150_L;
                        else
                            return ShoeSizeIndex._150_R;
                    case ShoeSize._160:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._160_L;
                        else
                            return ShoeSizeIndex._160_R;
                    case ShoeSize._170:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._170_L;
                        else
                            return ShoeSizeIndex._170_R;
                    case ShoeSize._180:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._180_L;
                        else
                            return ShoeSizeIndex._180_R;

                    case ShoeSize._190:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._190_L;
                        else
                            return ShoeSizeIndex._190_R;

                    case ShoeSize._200:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._200_L;
                        else
                            return ShoeSizeIndex._200_R;
                    case ShoeSize._210:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._210_L;
                        else
                            return ShoeSizeIndex._210_R;
                    case ShoeSize._220:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._220_L;
                        else
                            return ShoeSizeIndex._220_R;
                    case ShoeSize._230:

                        if (!(bool)isRight)
                            return ShoeSizeIndex._230_L;
                        else
                            return ShoeSizeIndex._230_R;
                    default:
                        if (!(bool)isRight)
                            return ShoeSizeIndex._90_L;
                        else
                            return ShoeSizeIndex._90_R;

                }
            }
        }
        /// <summary>
        /// 區分唯一序號 enum 的左右腳
        /// </summary>
        /// <param name="Index">唯一序號 enum</param>
        /// <returns>true : Right // false : Left</returns>
        public static bool? SizeIndex2Side(ShoeSizeIndex Index)
        {
            if ((int)Index % 3 == 0)
            {
                return false;
            }
            else if ((int)Index % 3 == 1)
            {
                return true;
            }
            else
            {
                return null;
            }
        }

        const string publickey = "20220704";
        const string secretkey = "Ryann@FT";
        public static string EncryptString(string textToEncrypt)
        {
            try
            {
                string ToReturn = "";
                byte[] secretkeyByte = { };
                secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool DecryptString(string textToDecrypt,out string returnStr)
        {
            try
            {
                returnStr = "";
                byte[] privatekeyByte = { };
                privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    returnStr = encoding.GetString(ms.ToArray());
                }
                return true;
            }
            catch (Exception ex)
            {
                returnStr = "Error";
                return false;
            }
        }

        static bool? isFirewallEnabled(string testProfile)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey($"System\\CurrentControlSet\\Services\\SharedAccess\\Parameters\\FirewallPolicy\\{testProfile}"))
                {
                    if (key == null)
                    {
                        return false;
                    }
                    else
                    {
                        Object o = key.GetValue("EnableFirewall");
                        if (o == null)
                        {
                            return false;
                        }
                        else
                        {
                            int firewall = (int)o;
                            if (firewall == 1)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static bool IsPrivateFirewallEnabled()
        {
            bool? isEnabled = isFirewallEnabled("StandardProfile");
            if (isEnabled == null) return true;
            else return (bool)isEnabled;
        }
        public static bool IsPublicFirewallEnabled()
        {
            bool? isEnabled = isFirewallEnabled("PublicProfile");
            if (isEnabled == null) return true;
            else return (bool)isEnabled;
        }
        public static bool IsDomainFirewallEnabled()
        {
            bool? isEnabled = isFirewallEnabled("DomainProfile");
            if (isEnabled == null) return true;
            else return (bool)isEnabled;
        }
        public static bool IsFirewallEnabled()
        {
            return IsPrivateFirewallEnabled() && IsPublicFirewallEnabled() && IsDomainFirewallEnabled();
        }
        public static bool IsTimeOut(double elapsedMilliSecond,Func<bool> waitCondition)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool timeOutResult = false;
            while(true)
            {
                double elapseTime = stopwatch.ElapsedMilliseconds;
                bool isOK = waitCondition();
                if (isOK)
                {
                    timeOutResult = false;
                    break;
                }
                if(elapseTime >= elapsedMilliSecond)
                {
                    timeOutResult = true;
                    break;
                }
                SpinWait.SpinUntil(() => false, 10);
            }
            stopwatch.Reset();
            return timeOutResult;
        }
        /// <summary>
        /// Return is time out
        /// </summary>
        /// <param name="elapsedMilliSecond"></param>
        /// <param name="waitCondition"></param>
        /// <param name="waitStatus"></param>
        /// <returns>-2 : time out. -1 : intime</returns>
        public static int IsTimeOut(double elapsedMilliSecond, Func<bool> waitCondition,bool waitStatus)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int retCode = -2;
            //bool timeOutResult = true;
            while (stopwatch.ElapsedMilliseconds <= elapsedMilliSecond)
            {
                bool isOK = waitCondition();
                if (isOK == waitStatus)
                {
                    retCode = -1;
                    //timeOutResult = false;
                    break;
                }
                SpinWait.SpinUntil(() => false, 10);
            }
            stopwatch.Reset();
            return retCode;
        }
        /// <summary>
        /// Return is time out
        /// </summary>
        /// <param name="elapsedMilliSecond"></param>
        /// <param name="waitCondition"></param>
        /// <param name="waitStatus"></param>
        /// <param name="breakCondition"></param>
        /// <returns>-2 : time out. -1 : intime. 0 : due to break condition</returns>
        public static int IsTimeOut(double elapsedMilliSecond, Func<bool> waitCondition, bool waitStatus,Func<bool> breakCondition)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int retCode = -2;
            //bool timeOutResult = true;
            while (stopwatch.ElapsedMilliseconds <= elapsedMilliSecond)
            {
                bool isOK = waitCondition();
                if (isOK == waitStatus)
                {
                    retCode = -1;
                    //timeOutResult = false;
                    break;
                }
                if (breakCondition())
                {
                    retCode = 0;
                    //timeOutResult = true;
                    break;
                }
                SpinWait.SpinUntil(() => false, 10);
            }
            stopwatch.Reset();
            return retCode;
        }
        /// <summary>
        /// Return is time out
        /// </summary>
        /// <param name="elapsedMilliSecond"></param>
        /// <param name="waitCondition"></param>
        /// <param name="waitStatus"></param>
        /// <param name="breakCondition"></param>
        /// <returns>-2 : time out. -1 : intime. >=0 : due to break condition</returns>
        public static int IsTimeOut(double elapsedMilliSecond, Func<bool> waitCondition, bool waitStatus, List<Func<bool>> breakCondition)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int retCode = -2;
            //bool timeOutResult = true;
            while (stopwatch.ElapsedMilliseconds <= elapsedMilliSecond)
            {
                bool isOK = waitCondition();
                if (isOK == waitStatus)
                {
                    retCode = -1;
                    break;
                }
                for (int i = 0; i < breakCondition.Count; i++)
                {
                    if (breakCondition[i]())
                    {
                        retCode = i;
                        break;
                    }
                }
                if (retCode >= 0) break;
                SpinWait.SpinUntil(() => false, 10);
            }
            stopwatch.Reset();
            return retCode;
        }

        /// <summary>
        /// 唯一序號 enum 轉成 鞋子尺寸 enum
        /// </summary>
        /// <param name="Index">唯一序號 enum</param>
        /// <returns>鞋子尺寸 enum</returns>
        public static ShoeSize SizeIndex2Size(ShoeSizeIndex Index)
        {
            switch (Index)
            {
                case ShoeSizeIndex._30_L:
                    return ShoeSize._30;
                case ShoeSizeIndex._30_R:
                    return ShoeSize._30;
                case ShoeSizeIndex._30_RL:
                    return ShoeSize._30;

                case ShoeSizeIndex._35_L:
                    return ShoeSize._35;
                case ShoeSizeIndex._35_R:
                    return ShoeSize._35;
                case ShoeSizeIndex._35_RL:
                    return ShoeSize._35;

                case ShoeSizeIndex._40_L:
                    return ShoeSize._40;
                case ShoeSizeIndex._40_R:
                    return ShoeSize._40;
                case ShoeSizeIndex._40_RL:
                    return ShoeSize._40;

                case ShoeSizeIndex._45_L:
                    return ShoeSize._45;
                case ShoeSizeIndex._45_R:
                    return ShoeSize._45;
                case ShoeSizeIndex._45_RL:
                    return ShoeSize._45;

                case ShoeSizeIndex._50_L:
                    return ShoeSize._50;
                case ShoeSizeIndex._50_R:
                    return ShoeSize._50;
                case ShoeSizeIndex._50_RL:
                    return ShoeSize._50;

                case ShoeSizeIndex._55_L:
                    return ShoeSize._55;
                case ShoeSizeIndex._55_R:
                    return ShoeSize._55;
                case ShoeSizeIndex._55_RL:
                    return ShoeSize._55;

                case ShoeSizeIndex._60_L:
                    return ShoeSize._60;
                case ShoeSizeIndex._60_R:
                    return ShoeSize._60;
                case ShoeSizeIndex._60_RL:
                    return ShoeSize._60;

                case ShoeSizeIndex._65_L:
                    return ShoeSize._65;
                case ShoeSizeIndex._65_R:
                    return ShoeSize._65;
                case ShoeSizeIndex._65_RL:
                    return ShoeSize._65;

                case ShoeSizeIndex._70_L:
                    return ShoeSize._70;
                case ShoeSizeIndex._70_R:
                    return ShoeSize._70;
                case ShoeSizeIndex._70_RL:
                    return ShoeSize._70;

                case ShoeSizeIndex._75_L:
                    return ShoeSize._75;
                case ShoeSizeIndex._75_R:
                    return ShoeSize._75;
                case ShoeSizeIndex._75_RL:
                    return ShoeSize._75;

                case ShoeSizeIndex._80_L:
                    return ShoeSize._80;
                case ShoeSizeIndex._80_R:
                    return ShoeSize._80;
                case ShoeSizeIndex._80_RL:
                    return ShoeSize._80;

                case ShoeSizeIndex._85_L:
                    return ShoeSize._85;
                case ShoeSizeIndex._85_R:
                    return ShoeSize._85;
                case ShoeSizeIndex._85_RL:
                    return ShoeSize._85;

                case ShoeSizeIndex._90_L:
                    return ShoeSize._90;
                case ShoeSizeIndex._90_R:
                    return ShoeSize._90;
                case ShoeSizeIndex._90_RL:
                    return ShoeSize._90;

                case ShoeSizeIndex._95_L:
                    return ShoeSize._95;
                case ShoeSizeIndex._95_R:
                    return ShoeSize._95;
                case ShoeSizeIndex._95_RL:
                    return ShoeSize._95;

                case ShoeSizeIndex._100_L:
                    return ShoeSize._100;
                case ShoeSizeIndex._100_R:
                    return ShoeSize._100;
                case ShoeSizeIndex._100_RL:
                    return ShoeSize._100;

                case ShoeSizeIndex._105_L:
                    return ShoeSize._105;
                case ShoeSizeIndex._105_R:
                    return ShoeSize._105;
                case ShoeSizeIndex._105_RL:
                    return ShoeSize._105;


                case ShoeSizeIndex._110_L:
                    return ShoeSize._110;
                case ShoeSizeIndex._110_R:
                    return ShoeSize._110;
                case ShoeSizeIndex._110_RL:
                    return ShoeSize._110;

                case ShoeSizeIndex._115_L:
                    return ShoeSize._115;
                case ShoeSizeIndex._115_R:
                    return ShoeSize._115;
                case ShoeSizeIndex._115_RL:
                    return ShoeSize._115;

                case ShoeSizeIndex._120_L:
                    return ShoeSize._120;
                case ShoeSizeIndex._120_R:
                    return ShoeSize._120;
                case ShoeSizeIndex._120_RL:
                    return ShoeSize._120;

                case ShoeSizeIndex._125_L:
                    return ShoeSize._125;
                case ShoeSizeIndex._125_R:
                    return ShoeSize._125;
                case ShoeSizeIndex._125_RL:
                    return ShoeSize._125;

                case ShoeSizeIndex._130_L:
                    return ShoeSize._130;
                case ShoeSizeIndex._130_R:
                    return ShoeSize._130;
                case ShoeSizeIndex._130_RL:
                    return ShoeSize._130;

                case ShoeSizeIndex._135_L:
                    return ShoeSize._135;
                case ShoeSizeIndex._135_R:
                    return ShoeSize._135;
                case ShoeSizeIndex._135_RL:
                    return ShoeSize._135;

                case ShoeSizeIndex._140_L:
                    return ShoeSize._140;
                case ShoeSizeIndex._140_R:
                    return ShoeSize._140;
                case ShoeSizeIndex._140_RL:
                    return ShoeSize._140;

                case ShoeSizeIndex._150_L:
                    return ShoeSize._150;
                case ShoeSizeIndex._150_R:
                    return ShoeSize._150;
                case ShoeSizeIndex._150_RL:
                    return ShoeSize._150;

                case ShoeSizeIndex._160_L:
                    return ShoeSize._160;
                case ShoeSizeIndex._160_R:
                    return ShoeSize._160;
                case ShoeSizeIndex._160_RL:
                    return ShoeSize._160;

                case ShoeSizeIndex._170_L:
                    return ShoeSize._170;
                case ShoeSizeIndex._170_R:
                    return ShoeSize._170;
                case ShoeSizeIndex._170_RL:
                    return ShoeSize._170;

                case ShoeSizeIndex._180_L:
                    return ShoeSize._180;
                case ShoeSizeIndex._180_R:
                    return ShoeSize._180;
                case ShoeSizeIndex._180_RL:
                    return ShoeSize._180;

                case ShoeSizeIndex._190_L:
                    return ShoeSize._190;
                case ShoeSizeIndex._190_R:
                    return ShoeSize._190;
                case ShoeSizeIndex._190_RL:
                    return ShoeSize._190;

                case ShoeSizeIndex._200_L:
                    return ShoeSize._200;
                case ShoeSizeIndex._200_R:
                    return ShoeSize._200;
                case ShoeSizeIndex._200_RL:
                    return ShoeSize._200;

                case ShoeSizeIndex._210_L:
                    return ShoeSize._210;
                case ShoeSizeIndex._210_R:
                    return ShoeSize._210;
                case ShoeSizeIndex._210_RL:
                    return ShoeSize._210;

                case ShoeSizeIndex._220_L:
                    return ShoeSize._220;
                case ShoeSizeIndex._220_R:
                    return ShoeSize._220;
                case ShoeSizeIndex._220_RL:
                    return ShoeSize._220;

                case ShoeSizeIndex._230_L:
                    return ShoeSize._230;
                case ShoeSizeIndex._230_R:
                    return ShoeSize._230;
                case ShoeSizeIndex._230_RL:
                    return ShoeSize._230;

                default:
                    return ShoeSize._90;
            }

        }
        /// <summary>
        /// 唯一序號 int 轉 唯一序號 enum
        /// </summary>
        /// <param name="ComboxIndex">int 序號</param>
        /// <returns></returns>
        public static ShoeSizeIndex IntSizeIndex_2_SSISizeIndex(int ComboxIndex)
        {
            return (ShoeSizeIndex)ComboxIndex;
        }
        /// <summary>
        /// 確定檔案是否鎖住
        /// </summary>
        /// <param name="file">檔案路徑</param>
        /// <returns>True : 檔案鎖住, False : 檔案沒有鎖住</returns>
        public static bool IsFileLocked(string file)
        {
            try
            {
                using (File.Open(file, FileMode.Open, FileAccess.Write, FileShare.None))
                {
                    Log.Add($"{file} unlocked", MsgLevel.Trace);
                    return false;
                }
            }
            catch (IOException exception)
            {
                var errorCode = Marshal.GetHRForException(exception) & 65535;
                //Log.Add($"{file} locked", MsgLevel.Trace);
                return errorCode == 32 || errorCode == 33;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsSignalRemain(Func<bool> func, bool signalRemainStatus,double continuedTimems)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            bool isSignalRemain = false;
            while(stopWatch.Elapsed.TotalMilliseconds <= continuedTimems)
            {
                bool temp = func();
                if(temp == signalRemainStatus)
                {
                    isSignalRemain = true;
                }
                else
                {
                    isSignalRemain = false;
                    break;
                }
                SpinWait.SpinUntil(() => false, 50);
            }
            stopWatch.Stop();
            return isSignalRemain;
        }
        public static void DeleteOutOfDateFile(string folder,string filePattern,int days)
        {
            Log.Add($"Delete out of date files. limit {days} days", MsgLevel.Trace);

            string[] files = Directory.GetFiles(folder, filePattern);
            DateTime dt0 = DateTime.Now;
            foreach (string s in files)
            {
                DateTime dt1 = File.GetLastWriteTime(s);
                TimeSpan ts = dt0.Subtract(dt1);
                if (ts.TotalDays >= days)
                {
                    Log.Add($"{s}", MsgLevel.Trace);
                    File.Delete(s);
                }
            }
        }
        /// <summary>
        /// 讀取文字檔內的所有資料
        /// </summary>
        /// <param name="filePath">檔案路徑</param>
        /// <returns>文字資料</returns>
        public static List<string> ReadAllLines(string filePath)
        {
            FileStream fs;
            StreamReader sr;
            List<string> stringList = new List<string>();

            if (!File.Exists(filePath))
                return stringList;

            fs = File.OpenRead(filePath);
            sr = new StreamReader(fs);

            while (sr.Peek() >= 0)
            {
                stringList.Add(sr.ReadLine());
            }

            fs.Flush();
            fs.Close();
            fs.Dispose();
            fs = null;

            sr.Close();
            sr.Dispose();
            sr = null;



            return stringList;
        }
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static bool WriteIniValue(string filePath, string section, string key, string val)
        {
            if (!File.Exists(filePath)) File.Create(filePath);
            return WritePrivateProfileString(section, key, val, filePath);
        }
        public static bool WriteIniValue(string filePath, string section, string key, int val)
        {
            if (!File.Exists(filePath)) File.Create(filePath);
            return WritePrivateProfileString(section, key, val.ToString(), filePath);
        }
        public static bool WriteIniValue(string filePath, string section, string key, double val)
        {
            if (!File.Exists(filePath)) File.Create(filePath);
            return WritePrivateProfileString(section, key, val.ToString(), filePath);
        }
        public static bool WriteIniValue(string filePath, string section, string key, bool val)
        {
            if (!File.Exists(filePath)) File.Create(filePath);
            return WritePrivateProfileString(section, key, val.ToString(), filePath);
        }

        // 8 = BackSpace
        //13 = Enter
        //45 = -
        // 46 = .

        public static bool int_Positive_KeyPress(char KeyChar)
        {
            if (((int)KeyChar < 48 | (int)KeyChar > 57) & (int)KeyChar != 8)
                return true;
            else 
                return false;
        }
        public static bool ip_format_KeyPress(char KeyChar)
        {
            if (((int)KeyChar < 48 | (int)KeyChar > 57) & (int)KeyChar != 8 & (int)KeyChar!=46)
                return true;
            else
                return false;
        }
        public static bool double_Positive_Negative_KeyPress(char KeyChar)
        {
            if (((int)KeyChar < 48 | (int)KeyChar > 57) & (int)KeyChar != 46 & (int)KeyChar != 8 & (int)KeyChar != 45)
                return true;
            else
                return false;
        }
        // n = 110
        // y = 121
        public static bool double_Positive_Negative_NY_KeyPress(char KeyChar)
        {
            if (((int)KeyChar < 48 | (int)KeyChar > 57) & (int)KeyChar != 46 & (int)KeyChar != 8 & (int)KeyChar != 45 & (int)KeyChar != 110 /*n*/& (int)KeyChar != 121/*y*/)
                return true;
            else
                return false;
        }

        public static bool double_Positive_KeyPress(char KeyChar)
        {
            if (((int)KeyChar < 48 | (int)KeyChar > 57) & (int)KeyChar != 46 & (int)KeyChar != 8)
                return true;
            else
                return false;
        }

        public static bool double_Positive_Comma_KeyPress(char KeyChar)
        {
            if (((int)KeyChar < 48 | (int)KeyChar > 57) & (int)KeyChar != 46 & (int)KeyChar != 8 & (int)KeyChar != 44)
                return true;
            else
                return false;
        }
    }
    public static class INI
    {
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        //public static int Read(string cSection, string cKeyName, int iDefaultValue, string str_FullFileName)
        //{
        //    StringBuilder strData = new StringBuilder(256);
        //    if (GetPrivateProfileString(cSection, cKeyName, iDefaultValue.ToString(), strData, (uint)strData.Capacity, str_FullFileName) > 0)
        //        iDefaultValue = int.Parse(strData.ToString());
        //    return iDefaultValue;
        //}

        //public static double Read(string cSection, string cKeyName, double fDefualtValue, string str_FullFileName)
        //{
        //    StringBuilder strData = new StringBuilder(256);
        //    if (GetPrivateProfileString(cSection, cKeyName, fDefualtValue.ToString(), strData, (uint)strData.Capacity, str_FullFileName) > 0)
        //        fDefualtValue = double.Parse(strData.ToString());
        //    return fDefualtValue;
        //}

        //public static string Read(string cSection, string cKeyName, string strDefaultValue, string str_FullFileName)
        //{
        //    StringBuilder strData = new StringBuilder(256);
        //    if (GetPrivateProfileString(cSection, cKeyName, strDefaultValue, strData, (uint)strData.Capacity, str_FullFileName) > 0)
        //        strDefaultValue = strData.ToString();
        //    return strDefaultValue;
        //}

        //public static bool Read(string cSection, string cKeyName, bool bDefualtValue, string str_FullFileName)
        //{
        //    StringBuilder strData = new StringBuilder(256);
        //    if (GetPrivateProfileString(cSection, cKeyName, bDefualtValue.ToString(), strData, (uint)strData.Capacity, str_FullFileName) > 0)
        //        bDefualtValue = ((strData.ToString() == "true") || (strData.ToString() == "True") || (strData.ToString() == "TRUE") ? true : false);
        //    return bDefualtValue;
        //}
        public static bool Read(string filePath, string section, string key, Type ValType, object defaultValue,out object val)
        {
            val = null;
            if (!File.Exists(filePath)) return false;

            StringBuilder temp = new StringBuilder(255);
            uint intStringLength = GetPrivateProfileString(section, key, "", temp, 255, filePath);

            if (intStringLength > 0)
            {
                if (ValType == typeof(int))
                {
                    int IntValue = 0;
                    if (int.TryParse(temp.ToString(), out IntValue))
                    {
                        val = IntValue;
                        return true;
                    }
                    else return false;
                }
                else if (ValType == typeof(double))
                {
                    double doublevalue = 0;
                    if (double.TryParse(temp.ToString(), out doublevalue))
                    {
                        val = doublevalue;
                        return true;
                    }
                    else return false;
                }
                else if (ValType == typeof(bool))
                {
                    bool boolvalue = false;
                    if (bool.TryParse(temp.ToString(), out boolvalue))
                    {
                        val = boolvalue;
                        return true;
                    }
                    else return false;

                }
                else
                {
                    val = temp.ToString();
                    return true;
                }
            }
            else
            {
                val = defaultValue;
                return false;
            }
        }

        public static bool Write(string cSection, string cKeyName, int iDefaultValue, string str_FullFileName)
        {
            return WritePrivateProfileString(cSection, cKeyName, iDefaultValue.ToString(), str_FullFileName);
        }

        public static bool Write(string cSection, string cKeyName, double fDefaultValue, string str_FullFileName)
        {
            return WritePrivateProfileString(cSection, cKeyName, fDefaultValue.ToString(), str_FullFileName);
        }

        public static bool Write(string cSection, string cKeyName, string cDefaultValue, string str_FullFileName)
        {
            return WritePrivateProfileString(cSection, cKeyName, cDefaultValue, str_FullFileName);
        }

        public static bool Write(string cSection, string cKeyName, bool bDefaultValue, string str_FullFileName)
        {
            return WritePrivateProfileString(cSection, cKeyName, bDefaultValue.ToString(), str_FullFileName);
        }
    }

}
