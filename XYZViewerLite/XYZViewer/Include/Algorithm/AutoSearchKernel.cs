using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// for File I/O
using System.IO;


namespace Automation
{
    public class AutoSearchKernel
    {
        // 掃描資料
        public List<List<Point3D>> polyline1;
        public List<List<Point3D>> polyline2;
        public List<List<Point3D>> polyline3;
        public List<List<Point3D>> polyline4;
        public List<List<Point3D>> polyline5;
        public List<Point3D> points1;
        public List<Point3D> points2;

        // 噴糊路徑資料
        public List<List<Position3D>> opt1;
        public List<List<Position3D>> opt2;
        public List<List<Position3D>> opt3;
        public List<Point3D> Pboundary = new List<Point3D>();
        public Point3D pToe = new Point3D();
        public Point3D pHeel = new Point3D();

        public AutoSearchKernel()
        {
            polyline1 = new List<List<Point3D>>();
            polyline2 = new List<List<Point3D>>();
            polyline3 = new List<List<Point3D>>();
            polyline4 = new List<List<Point3D>>();
            polyline5 = new List<List<Point3D>>();
            points1 = new List<Point3D>();
            points2 = new List<Point3D>();

            opt1 = new List<List<Position3D>>();
            opt2 = new List<List<Position3D>>();
            opt3 = new List<List<Position3D>>();
            ClearData();
        }
        public void ClearData()
        {
            polyline1.Clear();
            polyline2.Clear();
            polyline3.Clear();
            polyline4.Clear();
            points1.Clear();
            points2.Clear();
            opt1.Clear();
            opt2.Clear();
            opt3.Clear();
        }   

    }
}
