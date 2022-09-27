using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RsLib.PointCloud;
using Accord.Math;
namespace TestForm
{
    public partial class Form1 : Form
    {
        Point3DControl p3c = new Point3DControl("Test");
        
        public Form1()
        {
            InitializeComponent();
            p3c.Dock = DockStyle.Fill;
            panel1.Controls.Add(p3c);
        }

        private void button1_Click(object sender, EventArgs e)
        {
#if m
            PointCloud brick = new PointCloud();
            //brick.Add(new Point3D(0, 0, 10));
            //brick.Add(new Point3D(200, 100, 10));
            //brick.Add(new Point3D(200, 0, 10));
            //brick.Add(new Point3D(0, 100, 10));
            Point3D p0 = new Point3D(89.7, 43, 14.9);
            Point3D p1 = new Point3D(103.5, 109.2, 15.2);
            Point3D p2 = new Point3D(78.6, 123, 14.8);

            FTPlane plane = new FTPlane(p0,p1,p2);
            

            brick.LoadFromFile("d:\\test\\brick.xyz", true);
            Point3D L1P1 = new Point3D(72, 35.7, 14.4);
            Point3D L1P2 = new Point3D(113.1,35.2,15);
            Point3D L2P1 = new Point3D(68.6,39.0,14.4);
            Point3D L2P2 = new Point3D(67.9,132.8,14.6);
            Point3D i_P =  m_Func.GetIntersect(L1P1, L1P2, L2P1, L2P2);
            
            Vector3 vx_temp = new Vector3((float)( L1P2.X - i_P.X),(float)( L1P2.Y - i_P.Y),(float) ( L1P2.Z- i_P.Z));
            vx_temp.Normalize();
            Vector3 vy = new Vector3((float)(L2P2.X- i_P.X ), (float)( L2P2.Y- i_P.Y), (float)(L2P2.Z- i_P.Z ));
            vy.Normalize();
            Vector3 vz = Vector3.Cross(vx_temp, vy);
            vz.Normalize();
            Vector3 vx = Vector3.Cross(vy, vz);
            vx.Normalize();
            double dot = Vector3.Dot(vx, vy);
            double dot1 = Vector3.Dot(vx, vz);

            Point3D afterP = new Point3D(50, -23.99, 41);
            Vector3D shift = new Vector3D(i_P, afterP);
            Vector3 vx_ = new Vector3(0, 1, 0);
            Vector3 vy_ = new Vector3(-1, 0, 0);
            Vector3 vz_ = new Vector3(0, 0, 1);

            PointCloud afterShift = m_Func.Shift(brick, -i_P.X, -i_P.Y, -i_P.Z);
            PointCloud afterbrick =  m_Func.RotateShift(afterShift, afterP.X, afterP.Y, afterP.Z, vx, vy, vz, vx_, vy_, vz_);
            afterShift.Save("d:\\test\\afterShift.xyz");
            afterbrick.Save("d:\\test\\afterCloud.xyz");
#else
            PointCloud brick = new PointCloud();
            Point3D p0 = new Point3D(89.7, 43, 14.9);
            Point3D p1 = new Point3D(103.5, 109.2, 15.2);
            Point3D p2 = new Point3D(78.6, 123, 14.8);

            FTPlane plane = new FTPlane(p0, p1, p2);


            brick.LoadFromFile("D:\\Test\\Brick\\220428_143104_IMG_HEIGHT.xyz", true);
            Point3D L1P1 = new Point3D(114.15, 41.55, 15.06);
            Point3D L1P2 = new Point3D(157.5,41.1,15.7);
            Point3D L2P1 = new Point3D(114.15,45.45,15.07);
            Point3D L2P2 = new Point3D(115.05,138.0,15.07);
            Point3D i_P = m_Func.GetIntersect(L1P1, L1P2, L2P1, L2P2);

            Vector3 vx_temp = new Vector3((float)(L1P2.X - i_P.X), (float)(L1P2.Y - i_P.Y), (float)(L1P2.Z - i_P.Z));
            vx_temp.Normalize();
            Vector3 vy = new Vector3((float)(L2P2.X - i_P.X), (float)(L2P2.Y - i_P.Y), (float)(L2P2.Z - i_P.Z));
            vy.Normalize();
            Vector3 vz = Vector3.Cross(vx_temp, vy);
            vz.Normalize();
            Vector3 vx = Vector3.Cross(vy, vz);
            vx.Normalize();
            double dot = Vector3.Dot(vx, vy);
            double dot1 = Vector3.Dot(vx, vz);

            Point3D afterP = new Point3D(50, -23.99, 41);
            Vector3D shift = new Vector3D(i_P, afterP);
            Vector3 vx_ = new Vector3(0, 1, 0);
            Vector3 vy_ = new Vector3(-1, 0, 0);
            Vector3 vz_ = new Vector3(0, 0, 1);

            PointCloud afterShift = m_Func.Shift(brick, -i_P.X, -i_P.Y, -i_P.Z);
            PointCloud afterbrick = m_Func.RotateShift(afterShift, afterP.X, afterP.Y, afterP.Z, vx, vy, vz, vx_, vy_, vz_);
            afterShift.Save("D:\\Test\\Brick\\afterShift.xyz");
            afterbrick.Save("D:\\Test\\Brick\\afterCloud.xyz");

            Rotate local_rotate_image = new Rotate(vx, vy, vz);
            Shift local_shift_image = new Shift(i_P.X, i_P.Y, i_P.Z);

            Rotate local_rotate_robot = new Rotate(vx_, vy_, vz_);
            Shift local_shift_robot = new Shift(afterP.X, afterP.Y, afterP.Z);

            Matrix4x4 final = local_shift_robot.FinalMatrix4 * local_rotate_robot.FinalMatrix4 * local_rotate_image.FinalMatrix4Inverse * local_shift_image.FinalMatrix4Inverse;

           
            double[,] m =  m_Func.Matrix4x4ToArray(final);
            m_Func.SaveMatrix4x4(m, "d:\\test\\2.m44");
            PointCloud p = brick.Multiply(final);
            p.Save("D:\\Test\\Brick\\afterMatrix.xyz");

#endif
        }

        private void button2_Click(object sender, EventArgs e)
        {
            testMatrics();

        }

        void testMatrics()
        {

            List<CoordMatrix> matrics = new List<CoordMatrix>();
            Shift s1 = new Shift(-118.877, -237.762, 0);
            Rotate r1 = new Rotate();
            r1.AddRotateSeq(RefAxis.Z, 3.259);
            r1.CalculateFinalMatrix();
            Shift s2 = new Shift(113.328,236.398,0);

            matrics.Add(s1);
            matrics.Add(r1);
            matrics.Add(s2);
            Point3D pp =  p3c.P.Multiply(matrics);

        }
    }
}
