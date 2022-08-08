using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// for Application.StartupPath
using System.Windows.Forms;


/// <summary>
/// 備忘錄
/// </summary>
public class MyFunc
{
    #region System
    public static string GetStartupPath()
    {
        return Application.StartupPath;
    }

    public static int GetTickCount()
    {
        return System.Environment.TickCount;
    }
    #endregion

    #region UI
    /// <summary>
    /// Description: 顯示格式化錯誤訊息對話框
    /// Date: 2013.7.26
    /// </summary>
    /// <param name="strMainErr">主要錯誤，顯示在視窗標題</param>
    /// <param name="strSubErr">錯誤類別</param>
    /// <param name="strContent">錯誤內容</param>
    public static void ErrorMessage(string strMainErr, string strSubErr, string strContent)
    {
        string str = String.Format("{0}: {1}", strSubErr, strContent);
        MessageBox.Show(str, strMainErr);
    }
    #endregion

    #region Math
        public static double DegreeToRadian(double angle)
    {
        return Math.PI * angle / 180.0;
    }

    public static double RadianToDegree(double angle)
    {
        return angle * (180.0 / Math.PI);
    }

    /// <summary>
    /// 給定Matrix[3][4]，解3x3聯立方程式。回傳方程式係數解XYZ[3]。
    /// 	Suppose we have equations : a1 * x + b1 * y + c1 * z = d1
    ///                                 a2 * x + b2 * y + c2 * z = d2
    ///                                 a3 * x + b3 * y + c3 * z = d3
    ///     input array {   { a1, b1, c1, d1},
    ///                     { a2, b2, c2, d2},     
    ///                     { a3, b3, c3, d3}   }	   will get {x, y, z}
    /// </summary>
    public static double[] Solve3x3Equation(double[,] Matrix3x4)
    {
	    double[] Determinant=new double[4]{0, 0, 0, 0};

        Determinant[0] =  Matrix3x4[0,0]*(Matrix3x4[1,1]*Matrix3x4[2,2]-Matrix3x4[2,1]*Matrix3x4[1,2]);
        Determinant[0] -= Matrix3x4[0,1]*(Matrix3x4[1,0]*Matrix3x4[2,2]-Matrix3x4[2,0]*Matrix3x4[1,2]);
        Determinant[0] += Matrix3x4[0,2]*(Matrix3x4[1,0]*Matrix3x4[2,1]-Matrix3x4[2,0]*Matrix3x4[1,1]);

	    Determinant[1] =  Matrix3x4[0,3]*(Matrix3x4[1,1]*Matrix3x4[2,2]-Matrix3x4[2,1]*Matrix3x4[1,2]);
        Determinant[1] -= Matrix3x4[0,1]*(Matrix3x4[1,3]*Matrix3x4[2,2]-Matrix3x4[2,3]*Matrix3x4[1,2]);
        Determinant[1] += Matrix3x4[0,2]*(Matrix3x4[1,3]*Matrix3x4[2,1]-Matrix3x4[2,3]*Matrix3x4[1,1]);

        Determinant[2] =  Matrix3x4[0,0]*(Matrix3x4[1,3]*Matrix3x4[2,2]-Matrix3x4[2,3]*Matrix3x4[1,2]);
        Determinant[2] -= Matrix3x4[0,3]*(Matrix3x4[1,0]*Matrix3x4[2,2]-Matrix3x4[2,0]*Matrix3x4[1,2]);
        Determinant[2] += Matrix3x4[0,2]*(Matrix3x4[1,0]*Matrix3x4[2,3]-Matrix3x4[2,0]*Matrix3x4[1,3]);

	    Determinant[3] =  Matrix3x4[0,0]*(Matrix3x4[1,1]*Matrix3x4[2,3]-Matrix3x4[2,1]*Matrix3x4[1,3]);
        Determinant[3] -= Matrix3x4[0,1]*(Matrix3x4[1,0]*Matrix3x4[2,3]-Matrix3x4[2,0]*Matrix3x4[1,3]);
        Determinant[3] += Matrix3x4[0,3]*(Matrix3x4[1,0]*Matrix3x4[2,1]-Matrix3x4[2,0]*Matrix3x4[1,1]);

        double[] XYZ = new double[3];
	    XYZ[0]= Determinant[1] / Determinant[0];
	    XYZ[1]= Determinant[2] / Determinant[0];
	    XYZ[2]= Determinant[3] / Determinant[0];

        return XYZ;
    }
    #endregion

}
