using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using CsGL.OpenGL;

namespace Automation
{
    public enum GLColor : uint
    {
        White = 0,
        Black,
        Gray,
        BrightGray,
        LightWhite,
        DarkRed,
        Red,
        BrightRed,
        Green,
        BrightGreen,
        DarkBlue,
        Blue,
        BrightBlue,
        Yellow,
        DarkOrange,
        Orange,
        BrightOrange,
        Cyan,
        BrightCyan,
        DarkMagenta,
        Magenta,
        BrightMagenta,
        Purple
    };

    #region OpenGL Control Class
    public class OGL : OpenGLControl
    {
        #region Color Code
        private static float[][] col = new float[][] {							    // Array For Box Colors
			new float[] {1.0f, 1.0f, 1.0f},                                         // White
            new float[] {0.0f, 0.0f, 0.0f},                                         // Black
            new float[] {0.5f, 0.5f, 0.5f},                                         // Gray
            new float[] {(float)0xD3/0xFF, (float)0xD3/0xFF, (float)0xD3/0xFF},     // BrightGray
            new float[] {(float)0xF5/0xFF, (float)0xF5/0xFF, (float)0xF5/0xFF},     // LightWhite

            new float[] {0.74f, 0.0f, 0.0f},                                        // Dark Red
            new float[] {1.0f, 0.0f, 0.0f},                                         // Red
            new float[] {1.0f, 0.33f, 0.33f},                                       // Bright Red
            
            new float[] {0.0f, 1.0f, 0.0f},                                         // Green,
            new float[] {0.6f, 1.0f, 0.0f},                                         // BrightGreen,            

            new float[] {0.0f, 0.0f, 0.5f},                                         // Dark Blue
            new float[] {0.3f, 0.3f, 1.0f},                                         // Blue
            new float[] {0.5f, 0.75f, 1.0f},                                        // Bright Blue

            new float[] {1.0f, 1.0f, 0.0f},                                         // Yellow
            
            new float[] {0.7f, 0.35f,0.0f},                                         // Dark Orange
            new float[] {1.0f, 0.5f, 0.0f},                                         // Orange
            new float[] {1.0f, 0.58f, 0.38f},                                       // Bright Orange
            
            new float[] {0.0f, 1.0f, 1.0f},                                         // Cyan
            new float[] {0.7f, 1.0f, 1.0f},                                         // Light Cyan
            
            new float[] {0.7f, 0.0f, 0.7f},                                         // Dark Magenta
            new float[] {1.0f, 0.0f, 1.0f},                                         // Magenta            
            new float[] {1.0f, 0.4f, 1.0f},                                         // Bright Magenta
            new float[] {0.5f, 0.0f, 1.0f}                                          // Purple
		};
        public static int[] GetRGB8(uint ColorId)
        {
            float[] fRGB = new float[3];
            int[] iRGB = new int[3];
            for (int i = 0; i < 3; i++)
            {
                fRGB[i] = col[ColorId][i];
                iRGB[i] = (int)Math.Round((double)fRGB[i] * 255);
            }
            return iRGB;
        }
        #endregion
        private const int g_mouse_free = 0;
        private const int g_mouse_lock_p1 = 1;
        private const int g_mouse_lock_p2 = 2;

        private FormMain frmMain;
        #region Members
        #region Polyline
        private static uint lstPolyline1;
        private static uint lstPolyline2;
        private static uint lstPolyline3;
        private static uint lstPolyline4;
        private static uint lstPolyline5;
        #endregion Polyline
        #region Point Set
        private static uint lstPoints1;
        private static uint lstPoints2;
        #endregion Point Set
        #region Opt Path
        private static uint lstOptPath1;
        private static uint lstOptPath2;
        private static uint lstOptPath3;

        #endregion Opt Path
        #region Other
        private static uint lstAxis;        // 座標軸
        private static uint lstUser;        // User資料
        private static uint lstHitPoint;    // 滑鼠點擊點
        #endregion Other
        #region Enable/Disable flag
        public bool blnShowAxis = true;
        public bool blnShowPointCloud1 = false;
        public bool blnShowPointCloud2 = false;
        public bool blnShowPointCloud3 = false;
        public bool blnShowPointCloud4 = false;
        public bool blnShowPointCloud5 = false;
        public bool blnShowPoints1 = false;
        public bool blnShowPoints2 = false;
        public bool blnShowOptPath1 = false;
        public bool blnShowOptPath2 = false;
        public bool blnShowOptPath3 = false;
        public bool blnShowUser = false;
        public bool blnShowHitPoint = false;
        public bool blnShowHitP1 = false;
        public bool blnShowHitP2 = false;
        #endregion Enable/Disable flag
        #region Mouse measurement param
        private int mouseMeasureStatus = g_mouse_free;  // 滑鼠量測狀態
        private int mouseMoveCount = 0;
        private List<Point3D> data = new List<Point3D>();
        #endregion Mouse measurement param
        #endregion Members
        #region Graphic Params
        private Point mouseStartDrag;
        private Point mouseEndDrag;
        private static int height = 0;
        private static int width = 0;

        private Point3D HitP1 = new Point3D();
        private Point3D HitP2 = new Point3D();

        private System.Object matrixLock = new System.Object();
        private arcball arcBall = new arcball(300.0f, 300.0f);
        private float[] matrix = new float[16];

        private int[] m_viewport = new int[4];
        private double[] m_modelMatrix = new double[16];
        private double[] m_projMatrix = new double[16];

        private float ThisScale = 1.0f;
        private float LastScale = 1.0f;
        private Matrix4f LastRot = new Matrix4f();
        private Matrix4f ThisRot = new Matrix4f();

        private double startX = 0.0;
        private double startY = 0.0;
        private double endX = 0.0;
        private double endY = 0.0;
        private double moveX = 0.0;
        private double moveY = 0.0;
        private double tx = 0.0;
        private double ty = 0.0;
        private double scale = 1.0;

        private double view_range = 300;

        private static bool isLeftDrag = false;
        private static bool isRightDrag = false;
        private static bool isMiddleDrag = false;
        #endregion Graphic Params

        public OGL(FormMain frm)
            : base()
        {
            frmMain = frm;
            GC.Collect();
        }
        protected override void InitGLContext()
        {
            GL.glShadeModel(GL.GL_SMOOTH);								// enable smooth shading            
            GL.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);					// black background
            GL.glClearDepth(1.0f);										// depth buffer setup           

            GL.glEnable(GL.GL_POINT_SMOOTH);                            // 開啟畫點反鋸齒
            GL.glEnable(GL.GL_LINE_SMOOTH);                             // 開啟畫線反鋸齒
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA); // 開啟透明度模式
            GL.glEnable(GL.GL_BLEND);

            GL.glHint(GL.GL_PERSPECTIVE_CORRECTION_HINT, GL.GL_NICEST);	// nice perspective calculations          

            // Generate Display List ID
            lstAxis = GL.glGenLists(13);
            lstPolyline1 = lstAxis + 1;
            lstPolyline2 = lstAxis + 2;
            lstPolyline3 = lstAxis + 3;
            lstPolyline4 = lstAxis + 4;
            lstPolyline5 = lstAxis + 5;
            lstPoints1 = lstAxis + 6;
            lstPoints2 = lstAxis + 7;
            lstOptPath1 = lstAxis + 8;
            lstOptPath2 = lstAxis + 9;
            lstOptPath3 = lstAxis + 10;

            lstUser = lstAxis + 11;
            lstHitPoint = lstAxis + 12;

            BuildXYZAxis(20.0);

            LastRot.setIdentity(); // Reset Rotation
            ThisRot.setIdentity(); // Reset Rotation
            ThisRot.get_Renamed(matrix);

            LastScale = 1.0f;
            ThisScale = 1.0f;

            MouseControl mouseControl = new MouseControl(this);
            mouseControl.AddControl(this);
            mouseControl.LeftMouseDown += new MouseEventHandler(glOnLeftMouseDown);
            mouseControl.LeftMouseUp += new MouseEventHandler(glOnLeftMouseUp);

            mouseControl.RightMouseDown += new MouseEventHandler(glOnRightMouseDown);
            mouseControl.RightMouseUp += new MouseEventHandler(glOnRightMouseUp);

            mouseControl.MiddleMouseDown += new MouseEventHandler(glOnMiddleMouseDown);
            mouseControl.MiddleMouseUp += new MouseEventHandler(glOnMiddleMouseUp);

            this.MouseMove += new MouseEventHandler(glOnMouseMove);
            this.MouseWheel += new MouseEventHandler(glOnMouseWheel);
            this.MouseEnter += new EventHandler(glOnMouseEnter);
            this.MouseLeave += new EventHandler(glOnMouseLeave);
            this.MouseClick += new MouseEventHandler(glOnMouseClick);
        }
        public void SizeReset()
        {
            Size s = Size;
            height = s.Height;
            width = s.Width;

            GL.glViewport(0, 0, width, height);

            GL.glPushMatrix();
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();

            double aspect = (double)width / height;
            if (aspect >= 1.0)
                GL.glOrtho(-view_range * aspect, view_range * aspect, -view_range, view_range, -8 * view_range, 8 * view_range);
            else
                GL.glOrtho(-view_range, view_range, -view_range / aspect, view_range / aspect, -8 * view_range, 8 * view_range);

            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glPopMatrix();

            arcBall.setBounds((float)width, (float)height); //*NEW* Update mouse bounds for arcball

            Draw();

        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SizeReset();
        }
        private void SetColor(uint ColorId)
        {
            GL.glColor3d(col[ColorId][0],
                         col[ColorId][1],
                         col[ColorId][2]
                        );
        }
        private void SetColor(Color color)
        {
            GL.glColor4ub(color.R,
                         color.G,
                         color.B,
                         color.A
                        );
        }
        public void Draw()
        {
            try
            {
                lock (matrixLock)
                {
                    ThisRot.get_Renamed(matrix);
                }
                GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT); // Clear screen and DepthBuffer


                GL.glMatrixMode(GL.GL_MODELVIEW);
                GL.glLoadIdentity();
                GL.glPushMatrix();                  // NEW: Prepare Dynamic Transform                               

                GL.glMultMatrixf(matrix);           // NEW: Apply Dynamic Transform

                GL.glScaled(scale, scale, scale);
                GL.glTranslated(0.0, -100.0, -150.0);      // 模型旋轉中心位置

                #region Call lists
                if (blnShowAxis) { GL.glCallList(lstAxis); }
                if (blnShowPointCloud1) { GL.glCallList(lstPolyline1); }
                if (blnShowPointCloud2) { GL.glCallList(lstPolyline2); }
                if (blnShowPointCloud3) { GL.glCallList(lstPolyline3); }
                if (blnShowPointCloud4) { GL.glCallList(lstPolyline4); }
                if (blnShowPointCloud5) { GL.glCallList(lstPolyline5); }
                if (blnShowPoints1) { GL.glCallList(lstPoints1); }
                if (blnShowPoints2) { GL.glCallList(lstPoints2); }
                if (blnShowOptPath1) { GL.glCallList(lstOptPath1); }
                if (blnShowOptPath2) { GL.glCallList(lstOptPath2); }
                if (blnShowOptPath3) { GL.glCallList(lstOptPath3); }

                if (blnShowUser) { GL.glCallList(lstUser); }
                if (blnShowHitPoint) { GL.glCallList(lstHitPoint); }
                #endregion Call lists

                GL.glPopMatrix(); // NEW: Unapply Dynamic Transform

                //視圖平移
                GL.glMatrixMode(GL.GL_PROJECTION);
                GL.glTranslated(moveX, moveY, 0.0);
                tx += moveX;    //統計累積移動量
                ty += moveY;

                GL.glMatrixMode(GL.GL_MODELVIEW);
                moveX = 0.0;
                moveY = 0.0;

                GL.glFlush();     // Flush the GL Rendering Pipeline

                this.Invalidate();
            }
            catch
            {
                return;
            }
        }
        public void Clear()
        {
            blnShowPointCloud1 = false;
            blnShowPointCloud2 = false;
            blnShowPointCloud3 = false;
            blnShowPointCloud4 = false;
            blnShowPointCloud5 = false;
            blnShowPoints1 = false;
            blnShowPoints2 = false;
            blnShowOptPath1 = false;
            blnShowOptPath2 = false;
            blnShowOptPath3 = false;

            blnShowUser = false;
            blnShowHitPoint = false;
            Reset();
            Draw();

        }
        public void Reset()
        {
            lock (matrixLock)
            {
                LastRot.setIdentity();                                // Reset Rotation
                ThisRot.setIdentity();                                // Reset Rotation
            }

            moveX = -tx;
            moveY = -ty;
            scale = 1.0;
        }

        #region Build Models
        private void BuildXYZAxis(double AxisLength)
        {
            GL.glNewList(lstAxis, GL.GL_COMPILE);

            //座標軸
            GL.glLineWidth(2.0f);
            GL.glBegin(GL.GL_LINES);

            SetColor((uint)GLColor.Red);
            GL.glVertex3d(0.0, 0.0, 0.0);
            GL.glVertex3d(AxisLength, 0.0, 0.0);

            SetColor((uint)GLColor.Green);
            GL.glVertex3d(0.0, 0.0, 0.0);
            GL.glVertex3d(0.0, AxisLength, 0.0);

            SetColor((uint)GLColor.Blue);
            GL.glVertex3d(0.0, 0.0, 0.0);
            GL.glVertex3d(0.0, 0.0, AxisLength);
            GL.glEnd();

            GL.glEndList();
        }
        public void BuildPointCloud1(Color color)
        {
            if (!blnShowPointCloud1)
                return;

            GL.glDeleteLists(lstPolyline1, 1);
            GL.glNewList(lstPolyline1, GL.GL_COMPILE);

            SetColor(color);
            //GL.glColor3d(col[(uint)GLColor.BrightGray][0],
            //             col[(uint)GLColor.BrightGray][1],
            //             col[(uint)GLColor.BrightGray][2]);
            GL.glPointSize(1.0f);

            GL.glBegin(GL.GL_POINTS);
            lock (frmMain.autoSearch.polyline1)
            {
                for (int i = 0; i < frmMain.autoSearch.polyline1.Count; i++)
                {
                    for (int p = 0; p < frmMain.autoSearch.polyline1[i].Count; p++)
                    {
                        GL.glVertex3d(frmMain.autoSearch.polyline1[i][p].X,
                                      frmMain.autoSearch.polyline1[i][p].Y,
                                      frmMain.autoSearch.polyline1[i][p].Z
                            );
                    }
                }
            }
            GL.glEnd();
            GL.glEndList();
        }
        public void BuildPointCloud2(Color color)
        {
            if (!blnShowPointCloud2)
                return;

            GL.glDeleteLists(lstPolyline2, 1);
            GL.glNewList(lstPolyline2, GL.GL_COMPILE);

            SetColor(color);
            GL.glPointSize(1.5f);

            GL.glBegin(GL.GL_POINTS);
            lock (frmMain.autoSearch.polyline2)
            {
                for (int i = 0; i < frmMain.autoSearch.polyline2.Count; i++)
                {
                    for (int p = 0; p < frmMain.autoSearch.polyline2[i].Count; p++)
                    {
                        GL.glColor3d(frmMain.autoSearch.polyline2[i][p].R / 255, frmMain.autoSearch.polyline2[i][p].G / 255, frmMain.autoSearch.polyline2[i][p].B / 255);
                        GL.glVertex3d(frmMain.autoSearch.polyline2[i][p].X, frmMain.autoSearch.polyline2[i][p].Y, frmMain.autoSearch.polyline2[i][p].Z);
                    }
                }
            }
            GL.glEnd();
            GL.glEndList();
        }
        public void BuildPointCloud3(Color color)
        {
            if (!blnShowPointCloud3)
                return;

            GL.glDeleteLists(lstPolyline3, 1);
            GL.glNewList(lstPolyline3, GL.GL_COMPILE);

            SetColor(color);
            GL.glPointSize(1.0f);

            GL.glBegin(GL.GL_POINTS);
            lock (frmMain.autoSearch.polyline3)
            {
                for (int i = 0; i < frmMain.autoSearch.polyline3.Count; i++)
                {
                    for (int p = 0; p < frmMain.autoSearch.polyline3[i].Count; p++)
                    {
                        GL.glVertex3d(frmMain.autoSearch.polyline3[i][p].X,
                                      frmMain.autoSearch.polyline3[i][p].Y,
                                      frmMain.autoSearch.polyline3[i][p].Z
                            );
                    }
                }
            }
            GL.glEnd();
            GL.glEndList();
        }
        public void BuildPointCloud4(Color color)
        {
            if (!blnShowPointCloud4)
                return;

            GL.glDeleteLists(lstPolyline4, 1);
            GL.glNewList(lstPolyline4, GL.GL_COMPILE);

            SetColor(color);
            GL.glPointSize(1.0f);

            GL.glBegin(GL.GL_POINTS);
            lock (frmMain.autoSearch.polyline4)
            {
                for (int i = 0; i < frmMain.autoSearch.polyline4.Count; i++)
                {
                    for (int p = 0; p < frmMain.autoSearch.polyline4[i].Count; p++)
                    {
                        GL.glVertex3d(frmMain.autoSearch.polyline4[i][p].X,
                                      frmMain.autoSearch.polyline4[i][p].Y,
                                      frmMain.autoSearch.polyline4[i][p].Z
                            );
                    }
                }
            }
            GL.glEnd();
            GL.glEndList();
        }
        public void BuildPointCloud5(Color color)
        {
            if (!blnShowPointCloud5)
                return;

            GL.glDeleteLists(lstPolyline5, 1);
            GL.glNewList(lstPolyline5, GL.GL_COMPILE);

            SetColor(color);
            GL.glPointSize(1.0f);

            GL.glBegin(GL.GL_POINTS);
            lock (frmMain.autoSearch.polyline5)
            {
                for (int i = 0; i < frmMain.autoSearch.polyline5.Count; i++)
                {
                    for (int p = 0; p < frmMain.autoSearch.polyline5[i].Count; p++)
                    {
                        GL.glVertex3d(frmMain.autoSearch.polyline5[i][p].X,
                                      frmMain.autoSearch.polyline5[i][p].Y,
                                      frmMain.autoSearch.polyline5[i][p].Z
                            );
                    }
                }
            }
            GL.glEnd();
            GL.glEndList();
        }
        public void BuildPoints1(Color color)
        {
            if (!blnShowPoints1)
                return;

            GL.glDeleteLists(lstPoints1, 1);
            GL.glNewList(lstPoints1, GL.GL_COMPILE);

            //SetColor(0);
            SetColor(color);

            GL.glPointSize(5.0f);
            GL.glBegin(GL.GL_POINTS);
            lock (frmMain.autoSearch.points1)
            {
                for (int i = 0; i < frmMain.autoSearch.points1.Count; i++)
                {
                    GL.glVertex3d(frmMain.autoSearch.points1[i].X,
                                    frmMain.autoSearch.points1[i].Y,
                                    frmMain.autoSearch.points1[i].Z
                        );
                }
            }
            GL.glEnd();
            GL.glEndList();
        }
        public void BuildPoints2(Color color)
        {
            if (!blnShowPoints2)
                return;

            GL.glDeleteLists(lstPoints2, 1);
            GL.glNewList(lstPoints2, GL.GL_COMPILE);

            SetColor(color);

            GL.glPointSize(5.0f);
            GL.glBegin(GL.GL_POINTS);
            lock (frmMain.autoSearch.points2)
            {
                for (int i = 0; i < frmMain.autoSearch.points2.Count; i++)
                {
                    GL.glVertex3d(frmMain.autoSearch.points2[i].X,
                                    frmMain.autoSearch.points2[i].Y,
                                    frmMain.autoSearch.points2[i].Z
                        );
                }
            }
            GL.glEnd();
            GL.glEndList();
        }
        public void BuildOptPath1(Color color,bool isShowNormalV = false)
        {
            if (blnShowOptPath1)
            {
                OpenGL.glDeleteLists(lstOptPath1, 1);
                OpenGL.glNewList(lstOptPath1, 4864u);
                SetColor(color);
                if (isShowNormalV)
                {
                    OpenGL.glLineWidth(2f);
                    OpenGL.glBegin(1u);
                    lock (frmMain.autoSearch.opt1)
                    {
                        for (int i = 0; i < frmMain.autoSearch.opt1.Count; i++)
                        {
                            for (int j = 0; j < frmMain.autoSearch.opt1[i].Count; j++)
                            {
                                OpenGL.glVertex3d(frmMain.autoSearch.opt1[i][j].X, frmMain.autoSearch.opt1[i][j].Y, frmMain.autoSearch.opt1[i][j].Z);
                                OpenGL.glVertex3d(frmMain.autoSearch.opt1[i][j].X + 10.0 * frmMain.autoSearch.opt1[i][j].Vx, frmMain.autoSearch.opt1[i][j].Y + 10.0 * frmMain.autoSearch.opt1[i][j].Vy, frmMain.autoSearch.opt1[i][j].Z + 10.0 * frmMain.autoSearch.opt1[i][j].Vz);
                            }
                        }
                    }
                    OpenGL.glEnd();
                }
                SetColor(color);
                OpenGL.glLineWidth(1f);
                lock (frmMain.autoSearch.opt1)
                {
                    for (int k = 0; k < frmMain.autoSearch.opt1.Count; k++)
                    {
                        OpenGL.glBegin(1u);
                        for (int l = 0; l < frmMain.autoSearch.opt1[k].Count - 1; l++)
                        {
                            int i0 = l;
                            int i1 = l + 1;
                            OpenGL.glVertex3d(frmMain.autoSearch.opt1[k][i0].X, frmMain.autoSearch.opt1[k][i0].Y, frmMain.autoSearch.opt1[k][i0].Z);
                            OpenGL.glVertex3d(frmMain.autoSearch.opt1[k][i1].X, frmMain.autoSearch.opt1[k][i1].Y, frmMain.autoSearch.opt1[k][i1].Z);
                        }
                        OpenGL.glEnd();
                    }
                }
                OpenGL.glEndList();
            }
        }

        public void BuildOptPath2(Color color ,bool isShowNormalV = false)
        {
            if (blnShowOptPath2)
            {
                OpenGL.glDeleteLists(lstOptPath2, 1);
                OpenGL.glNewList(lstOptPath2, 4864u);
                if (isShowNormalV)
                {
                    SetColor(color);
                    OpenGL.glLineWidth(2f);
                    OpenGL.glBegin(1u);
                    lock (frmMain.autoSearch.opt2)
                    {
                        for (int i = 0; i < frmMain.autoSearch.opt2.Count; i++)
                        {
                            for (int j = 0; j < frmMain.autoSearch.opt2[i].Count; j++)
                            {
                                OpenGL.glVertex3d(frmMain.autoSearch.opt2[i][j].X, frmMain.autoSearch.opt2[i][j].Y, frmMain.autoSearch.opt2[i][j].Z);
                                OpenGL.glVertex3d(frmMain.autoSearch.opt2[i][j].X + 10.0 * frmMain.autoSearch.opt2[i][j].Vx, frmMain.autoSearch.opt2[i][j].Y + 10.0 * frmMain.autoSearch.opt2[i][j].Vy, frmMain.autoSearch.opt2[i][j].Z + 10.0 * frmMain.autoSearch.opt2[i][j].Vz);
                            }
                        }
                    }
                    OpenGL.glEnd();
                }
                SetColor(color);
                OpenGL.glLineWidth(1f);
                lock (frmMain.autoSearch.opt2)
                {
                    for (int k = 0; k < frmMain.autoSearch.opt2.Count; k++)
                    {
                        OpenGL.glBegin(1u);
                        for (int l = 0; l < frmMain.autoSearch.opt2[k].Count - 1; l++)
                        {
                            int i0 = l;
                            int i1 = l + 1;

                            OpenGL.glVertex3d(frmMain.autoSearch.opt2[k][i0].X, frmMain.autoSearch.opt2[k][i0].Y, frmMain.autoSearch.opt2[k][i0].Z);
                            OpenGL.glVertex3d(frmMain.autoSearch.opt2[k][i1].X, frmMain.autoSearch.opt2[k][i1].Y, frmMain.autoSearch.opt2[k][i1].Z);

                        }
                        OpenGL.glEnd();
                    }
                }
                OpenGL.glEndList();
            }
        }
        public void BuildOptPath3(Color color,bool isShowNormalV = false)
        {
            if (blnShowOptPath3)
            {
                OpenGL.glDeleteLists(lstOptPath3, 1);
                OpenGL.glNewList(lstOptPath3, 4864u);
                if (isShowNormalV)
                {
                    SetColor(color);
                    OpenGL.glLineWidth(2f);
                    OpenGL.glBegin(1u);
                    lock (frmMain.autoSearch.opt3)
                    {
                        for (int i = 0; i < frmMain.autoSearch.opt3.Count; i++)
                        {
                            for (int j = 0; j < frmMain.autoSearch.opt3[i].Count; j++)
                            {
                                OpenGL.glVertex3d(frmMain.autoSearch.opt3[i][j].X, frmMain.autoSearch.opt3[i][j].Y, frmMain.autoSearch.opt3[i][j].Z);
                                OpenGL.glVertex3d(frmMain.autoSearch.opt3[i][j].X + 10.0 * frmMain.autoSearch.opt3[i][j].Vx, frmMain.autoSearch.opt3[i][j].Y + 10.0 * frmMain.autoSearch.opt3[i][j].Vy, frmMain.autoSearch.opt3[i][j].Z + 10.0 * frmMain.autoSearch.opt3[i][j].Vz);
                            }
                        }
                    }
                    OpenGL.glEnd();
                }
                SetColor(color);
                OpenGL.glLineWidth(1f);
                lock (frmMain.autoSearch.opt3)
                {
                    for (int k = 0; k < frmMain.autoSearch.opt3.Count; k++)
                    {
                        OpenGL.glBegin(1u);
                        for (int l = 0; l < frmMain.autoSearch.opt3[k].Count - 1; l++)
                        {
                            int i0 = l;
                            int i1 = l + 1;

                            OpenGL.glVertex3d(frmMain.autoSearch.opt3[k][i0].X, frmMain.autoSearch.opt3[k][i0].Y, frmMain.autoSearch.opt3[k][i0].Z);
                            OpenGL.glVertex3d(frmMain.autoSearch.opt3[k][i1].X, frmMain.autoSearch.opt3[k][i1].Y, frmMain.autoSearch.opt3[k][i1].Z);

                        }
                        OpenGL.glEnd();
                    }
                }
                OpenGL.glEndList();
            }
        }

        public void BuildUser()
        {
            if (!blnShowUser)
                return;

            GL.glDeleteLists(lstUser, 1);
            GL.glNewList(lstUser, GL.GL_COMPILE);

            SetColor((uint)GLColor.Red);

            GL.glPointSize(4.0f);
            GL.glBegin(GL.GL_POINTS);
            lock (frmMain.autoSearch.Pboundary)
            {
                for (int i = 0; i < frmMain.autoSearch.Pboundary.Count; i++)
                {
                    GL.glVertex3d(frmMain.autoSearch.Pboundary[i].X,
                                    frmMain.autoSearch.Pboundary[i].Y,
                                    frmMain.autoSearch.Pboundary[i].Z
                        );
                }
            }
            GL.glEnd();


            GL.glPointSize(6.0f);

            GL.glBegin(GL.GL_POINTS);
            SetColor((uint)GLColor.Yellow);
            GL.glVertex3d(frmMain.autoSearch.pToe.X,
                            frmMain.autoSearch.pToe.Y,
                            frmMain.autoSearch.pToe.Z
                        );
            SetColor((uint)GLColor.Green);
            GL.glVertex3d(frmMain.autoSearch.pHeel.X,
                            frmMain.autoSearch.pHeel.Y,
                            frmMain.autoSearch.pHeel.Z
                        );
            GL.glEnd();

            GL.glEndList();
        }
        public void BuildHitPoint()
        {
            if (!blnShowHitPoint)
                return;

            GL.glDeleteLists(lstHitPoint, 1);
            GL.glNewList(lstHitPoint, GL.GL_COMPILE);

            GL.glPointSize(6.0f);
            GL.glBegin(GL.GL_POINTS);
            switch (mouseMeasureStatus)
            {
                case g_mouse_free:
                    if (blnShowHitP1)
                    {
                        SetColor((uint)GLColor.Green);
                        GL.glVertex3d(HitP1.X, HitP1.Y, HitP1.Z);
                    }
                    if (blnShowHitP2)
                    {
                        SetColor((uint)GLColor.Green);
                        GL.glVertex3d(HitP2.X, HitP2.Y, HitP2.Z);
                    }
                    break;

                case g_mouse_lock_p1:
                    if (blnShowHitP1)
                    {
                        SetColor((uint)GLColor.Red);
                        GL.glVertex3d(HitP1.X, HitP1.Y, HitP1.Z);
                    }
                    if (blnShowHitP2)
                    {
                        SetColor((uint)GLColor.Green);
                        GL.glVertex3d(HitP2.X, HitP2.Y, HitP2.Z);
                    }
                    break;

                case g_mouse_lock_p2:
                    if (blnShowHitP1)
                    {
                        SetColor((uint)GLColor.Red);
                        GL.glVertex3d(HitP1.X, HitP1.Y, HitP1.Z);
                    }
                    if (blnShowHitP2)
                    {
                        SetColor((uint)GLColor.Red);
                        GL.glVertex3d(HitP2.X, HitP2.Y, HitP2.Z);
                    }
                    break;
                default: break;
            }
            GL.glEnd();

            GL.glLineWidth(1.5f);
            GL.glBegin(GL.GL_LINES);
            if (blnShowHitP1 && blnShowHitP2)
            {
                if (mouseMeasureStatus < g_mouse_lock_p2)
                {
                    SetColor((uint)GLColor.Green);
                }
                else
                {
                    SetColor((uint)GLColor.Red);
                }
                GL.glVertex3d(HitP1.X, HitP1.Y, HitP1.Z);
                GL.glVertex3d(HitP2.X, HitP2.Y, HitP2.Z);
            }
            GL.glEnd();

            GL.glEndList();
        }
        #endregion Build Models
        #region Measurement Calculation
        public void UpdateMouseHitData()
        {
            data.Clear();
            //Add selected model into calculation database
            #region Axis
            if (blnShowAxis)
            {
                data.Add(new Point3D(0, 0, 0));
            }
            #endregion Axis
            #region Points
            if (blnShowPoints1)
            {
                lock (frmMain.autoSearch.points1)
                {
                    for (int i = 0; i < frmMain.autoSearch.points1.Count; i++)
                    {
                        data.Add(frmMain.autoSearch.points1[i]);
                    }
                }
            }
            if (blnShowPoints2)
            {
                lock (frmMain.autoSearch.points2)
                {
                    for (int i = 0; i < frmMain.autoSearch.points2.Count; i++)
                    {
                        data.Add(frmMain.autoSearch.points2[i]);
                    }
                }
            }
            #endregion Points
            #region Polylines
            if (blnShowPointCloud1)
            {
                lock (frmMain.autoSearch.polyline1)
                {
                    for (int i = 0; i < frmMain.autoSearch.polyline1.Count; i++)
                    {
                        for (int j = 0; j < frmMain.autoSearch.polyline1[i].Count; j++)
                        {
                            data.Add(frmMain.autoSearch.polyline1[i][j]);
                        }
                    }
                }
            }
            if (blnShowPointCloud2)
            {
                lock (frmMain.autoSearch.polyline2)
                {
                    for (int i = 0; i < frmMain.autoSearch.polyline2.Count; i++)
                    {
                        for (int j = 0; j < frmMain.autoSearch.polyline2[i].Count; j++)
                        {
                            data.Add(frmMain.autoSearch.polyline2[i][j]);
                        }
                    }
                }
            }
            if (blnShowPointCloud3)
            {
                lock (frmMain.autoSearch.polyline3)
                {
                    for (int i = 0; i < frmMain.autoSearch.polyline3.Count; i++)
                    {
                        for (int j = 0; j < frmMain.autoSearch.polyline3[i].Count; j++)
                        {
                            data.Add(frmMain.autoSearch.polyline3[i][j]);
                        }
                    }
                }
            }
            if (blnShowPointCloud4)
            {
                lock (frmMain.autoSearch.polyline4)
                {
                    for (int i = 0; i < frmMain.autoSearch.polyline4.Count; i++)
                    {
                        for (int j = 0; j < frmMain.autoSearch.polyline4[i].Count; j++)
                        {
                            data.Add(frmMain.autoSearch.polyline4[i][j]);
                        }
                    }
                }
            }
            if (blnShowPointCloud5)
            {
                lock (frmMain.autoSearch.polyline5)
                {
                    for (int i = 0; i < frmMain.autoSearch.polyline5.Count; i++)
                    {
                        for (int j = 0; j < frmMain.autoSearch.polyline5[i].Count; j++)
                        {
                            data.Add(frmMain.autoSearch.polyline5[i][j]);
                        }
                    }
                }
            }
            #endregion Polylines
            #region Path
            if (blnShowOptPath1)
            {
                lock (frmMain.autoSearch.opt1)
                {
                    for (int i = 0; i < frmMain.autoSearch.opt1.Count; i++)
                    {
                        for (int j = 0; j < frmMain.autoSearch.opt1[i].Count; j++)
                        {
                            Point3D p = new Point3D(frmMain.autoSearch.opt1[i][j].X,
                                frmMain.autoSearch.opt1[i][j].Y,
                                frmMain.autoSearch.opt1[i][j].Z
                                );
                            data.Add(p);
                        }
                    }
                }
            }
            if (blnShowOptPath2)
            {
                lock (frmMain.autoSearch.opt2)
                {
                    for (int i = 0; i < frmMain.autoSearch.opt2.Count; i++)
                    {
                        for (int j = 0; j < frmMain.autoSearch.opt2[i].Count; j++)
                        {
                            Point3D p = new Point3D(frmMain.autoSearch.opt2[i][j].X,
                                frmMain.autoSearch.opt2[i][j].Y,
                                frmMain.autoSearch.opt2[i][j].Z
                                );
                            data.Add(p);
                        }
                    }
                }
            }
            if (blnShowOptPath3)
            {
                lock (frmMain.autoSearch.opt3)
                {
                    for (int i = 0; i < frmMain.autoSearch.opt3.Count; i++)
                    {
                        for (int j = 0; j < frmMain.autoSearch.opt3[i].Count; j++)
                        {
                            Point3D p = new Point3D(frmMain.autoSearch.opt3[i][j].X,
                                frmMain.autoSearch.opt3[i][j].Y,
                                frmMain.autoSearch.opt3[i][j].Z
                                );
                            data.Add(p);
                        }
                    }
                }
            }

            #endregion Path
        }
        private double CalculateNearestP(List<Point3D> data, Point3D Pstart, Point3D Pend, out Point3D Pnearest)
        {
            Point3D P = new Point3D();
            double minDist = double.MaxValue;
            int hitIndex = -1;
            double D = double.MaxValue;

            for (int i = 0; i < data.Count; i++)
            {
                D = Point3D.Distance(data[i], Pstart, Pend);
                if (D < minDist)
                {
                    hitIndex = i;
                    minDist = D;
                }
            }

            if (minDist <= 1.0 && hitIndex > 0)
            {
                P.X = data[hitIndex].X;
                P.Y = data[hitIndex].Y;
                P.Z = data[hitIndex].Z;
            }

            Pnearest = P;
            return minDist;
        }
        private void MouseHitCheck(Point mousePt, bool isClick)
        {
            double x1 = 0.0;
            double y1 = 0.0;
            double z1 = 0.0;
            double x2 = 0.0;
            double y2 = 0.0;
            double z2 = 0.0;
            GetMousePosNear(mousePt, out x1, out y1, out z1);
            GetMousePosFar(mousePt, out x2, out y2, out z2);
            Point3D Pnear = new Point3D(x1, y1, z1);
            Point3D Pfar = new Point3D(x2, y2, z2);

            double minDist = double.MaxValue;
            Point3D Pnearest;
            minDist = CalculateNearestP(data, Pnear, Pfar, out Pnearest);

            //更新量測狀態
            switch (mouseMeasureStatus)
            {
                case g_mouse_free:
                    if (minDist <= 1.0)
                    {
                        blnShowHitP1 = true;
                        HitP1.X = Pnearest.X;
                        HitP1.Y = Pnearest.Y;
                        HitP1.Z = Pnearest.Z;

                        frmMain.txtX1.Text = string.Format("{0:F3}", HitP1.X);
                        frmMain.txtY1.Text = string.Format("{0:F3}", HitP1.Y);
                        frmMain.txtZ1.Text = string.Format("{0:F3}", HitP1.Z);

                        if (isClick)
                        {
                            frmMain.lblXYZ1.ForeColor = Color.Red;
                            mouseMeasureStatus = g_mouse_lock_p1;
                        }
                    }
                    else
                    {
                        frmMain.lblXYZ1.ForeColor = Color.Lime;
                        blnShowHitP1 = false;
                    }
                    break;

                case g_mouse_lock_p1:
                    if (minDist <= 1.0)
                    {
                        blnShowHitP2 = true;
                        HitP2.X = Pnearest.X;
                        HitP2.Y = Pnearest.Y;
                        HitP2.Z = Pnearest.Z;

                        frmMain.txtX2.Text = string.Format("{0:F3}", HitP2.X);
                        frmMain.txtY2.Text = string.Format("{0:F3}", HitP2.Y);
                        frmMain.txtZ2.Text = string.Format("{0:F3}", HitP2.Z);

                        if (isClick)
                        {
                            frmMain.lblXYZ2.ForeColor = Color.Red;
                            mouseMeasureStatus = g_mouse_lock_p2;
                        }
                    }
                    else
                    {
                        frmMain.lblXYZ2.ForeColor = Color.Lime;
                        blnShowHitP2 = false;
                    }
                    break;

                case g_mouse_lock_p2:
                    blnShowHitP1 = true;
                    blnShowHitP2 = true;
                    if (isClick && minDist <= 1.0)
                    {
                        frmMain.lblXYZ1.ForeColor = Color.White;
                        frmMain.lblXYZ2.ForeColor = Color.White;
                        mouseMeasureStatus = g_mouse_free;
                    }

                    break;

                default: break;
            }
            BuildHitPoint();
            frmMain.txtDistance.Text = string.Format("{0:F2}", Point3D.Distance(HitP1, HitP2));

        }
        #endregion Measurement Calculation
        #region Mouse Control
        private void startDrag(Point MousePt)
        {
            lock (matrixLock)
            {
                LastRot.set_Renamed(ThisRot); // Set Last Static Rotation To Last Dynamic One
            }
            arcBall.click(MousePt); // Update Start Vector And Prepare For Dragging

            mouseStartDrag = MousePt;
            LastScale = ThisScale;
        }

        private void left_drag(Point MousePt)
        {
            Quat4f ThisQuat = new Quat4f();

            arcBall.drag_rotation(MousePt, ThisQuat); // Update End Vector And Get Rotation As Quaternion

            lock (matrixLock)
            {
                ThisRot.Rotation = ThisQuat; // Convert Quaternion Into Matrix3fT
                ThisRot.mul(ThisRot, LastRot); // Accumulate Last Rotation Into This One
            }
        }

        private void middle_drag(Point MousePt)
        {
            GetMousePos(MousePt, out endX, out endY);
            if (double.IsNaN(endX) || double.IsNaN(endY))
            {
                endX = startX;
                endY = startY;
            }

            double s = 0.1;
            moveX = s * (endX - startX);
            moveY = s * (endY - startY);

            if (double.IsNaN(moveX) || double.IsNaN(moveY))
            {
                moveX = 0;
                moveY = 0;
            }
        }

        private void GetMousePos(Point MousePt, out double X, out double Y)
        {
            GL.glReadBuffer(GL.GL_BACK);
            //取得目前狀態
            GL.glPushMatrix();

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, m_modelMatrix);
            GL.glGetDoublev(GL.GL_PROJECTION_MATRIX, m_projMatrix);
            GL.glGetIntegerv(GL.GL_VIEWPORT, m_viewport);
            GL.glPopMatrix();

            double winX = (double)MousePt.X;
            double winY = m_viewport[3] - MousePt.Y;

            double winZ = 0.0;

            int size = Marshal.SizeOf(typeof(double));
            IntPtr p = Marshal.AllocHGlobal(size);

            GL.glReadPixels((int)winX, (int)winY, 1, 1, GL.GL_DEPTH_COMPONENT, GL.GL_FLOAT, p);

            double[] temp = new double[1];
            Marshal.Copy(p, temp, 0, 1);
            winZ = temp[0];

            //To get the 3d position with mouse:
            double x, y, z;
            GL.gluUnProject(winX, winY, winZ, m_modelMatrix, m_projMatrix, m_viewport, out x, out y, out z);

            X = x;
            Y = y;
        }

        private void GetMousePosNear(Point MousePt, out double X, out double Y, out double Z)
        {
            int[] viewport = new int[4];
            double[] modelMatrix = new double[16];
            double[] projMatrix = new double[16];

            //取得目前狀態
            GL.glPushMatrix();
            GL.glLoadIdentity();
            GL.glMultMatrixf(matrix);
            GL.glScaled(scale, scale, scale);
            GL.glTranslated(0.0, -100.0, -150.0);

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, modelMatrix);
            GL.glGetDoublev(GL.GL_PROJECTION_MATRIX, projMatrix);
            GL.glGetIntegerv(GL.GL_VIEWPORT, viewport);

            GL.glPopMatrix();

            double winX = MousePt.X;
            double winY = viewport[3] - MousePt.Y;
            double winZ = 0.0;

            //To get the 3d position with mouse:
            double x, y, z;
            GL.gluUnProject(winX, winY, winZ, modelMatrix, projMatrix, viewport, out x, out y, out z);

            X = x;
            Y = y;
            Z = z;
        }

        private void GetMousePosFar(Point MousePt, out double X, out double Y, out double Z)
        {
            int[] viewport = new int[4];
            double[] modelMatrix = new double[16];
            double[] projMatrix = new double[16];

            //取得目前狀態
            GL.glPushMatrix();
            GL.glLoadIdentity();
            GL.glMultMatrixf(matrix);
            GL.glScaled(scale, scale, scale);
            GL.glTranslated(0.0, -100.0, -150.0);

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, modelMatrix);
            GL.glGetDoublev(GL.GL_PROJECTION_MATRIX, projMatrix);
            GL.glGetIntegerv(GL.GL_VIEWPORT, viewport);

            GL.glPopMatrix();

            double winX = MousePt.X;
            double winY = viewport[3] - MousePt.Y;
            double winZ = 1.0;

            //To get the 3d position with mouse:
            double x, y, z;
            GL.gluUnProject(winX, winY, winZ, modelMatrix, projMatrix, viewport, out x, out y, out z);

            X = x;
            Y = y;
            Z = z;
        }

        private void glOnMouseClick(object sender, MouseEventArgs e)
        {
            Point mousePt = mouseEndDrag = new Point(e.X, e.Y);
            if (blnShowHitPoint)
            {
                MouseHitCheck(mousePt, isClick: true);
            }
        }

        private void glOnMouseMove(object sender, MouseEventArgs e)
        {
            mouseMoveCount++;
            Point mousePt = mouseEndDrag = new Point(e.X, e.Y);
            if (mouseMoveCount % 20 == 0)
            {
                mouseMoveCount = 0;
            }
            if (blnShowHitPoint && mouseMoveCount == 0)
            {
                //MouseHitCheck(mousePt, isClick: false);
            }
            if (isLeftDrag)
            {
                left_drag(mousePt);
            }
            _ = isRightDrag;
            if (isRightDrag)
            {
                middle_drag(mousePt);
            }
        }

        private void glOnMouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta;
            if (delta > 0)
            {
                scale *= 1.0 + 0.002 * (double)delta;
            }
            else if (delta < 0)
            {
                scale /= 1.0 - 0.002 * (double)delta;
            }
            else
            {
                scale = 1.0;
            }
        }

        private void glOnMouseEnter(object sender, EventArgs e)
        {
            if (!Focused)
            {
                Focus();
            }
        }

        private void glOnMouseLeave(object sender, EventArgs e)
        {
            if (Focused)
            {
                base.Parent.Focus();
            }
        }

        private void glOnLeftMouseDown(object sender, MouseEventArgs e)
        {
            isLeftDrag = true;
            Point mousePt = new Point(e.X, e.Y);
            startDrag(mousePt);
        }

        private void glOnLeftMouseUp(object sender, MouseEventArgs e)
        {
            isLeftDrag = false;
        }

        private void glOnRightMouseDown(object sender, MouseEventArgs e)
        {
            isRightDrag = true;
            Point mousePt = new Point(e.X, e.Y);
            startDrag(mousePt);
            GetMousePos(mouseStartDrag, out startX, out startY);

        }

        private void glOnRightMouseUp(object sender, MouseEventArgs e)
        {
            isRightDrag = false;

        }

        private void glOnMiddleMouseDown(object sender, MouseEventArgs e)
        {
            isMiddleDrag = true;
            Point mousePt = new Point(e.X, e.Y);
            startDrag(mousePt);
        }

        private void glOnMiddleMouseUp(object sender, MouseEventArgs e)
        {
            isMiddleDrag = false;
            Reset();
        }

        #endregion
    }
    #endregion OpenGL Control Class

    #region MouseControl Class
    public class MouseControl
    {
        protected Control newCtrl;
        protected MouseButtons FinalClick;

        public event EventHandler LeftClick;
        public event EventHandler RightClick;
        public event EventHandler MiddleClick;
        public event MouseEventHandler LeftMouseDown;
        public event MouseEventHandler LeftMouseUp;
        public event MouseEventHandler RightMouseDown;
        public event MouseEventHandler RightMouseUp;
        public event MouseEventHandler MiddleMouseDown;
        public event MouseEventHandler MiddleMouseUp;

        public Control Control
        {
            get { return newCtrl; }
            set
            {
                newCtrl = value;
                Initialize();
            }
        }

        public MouseControl()
        {
        }

        public MouseControl(Control ctrl)
        {
            Control = ctrl;
        }

        public void AddControl(Control ctrl)
        {
            Control = ctrl;
        }

        protected virtual void Initialize()
        {
            newCtrl.Click += new EventHandler(OnClick);
            newCtrl.MouseDown += new MouseEventHandler(OnMouseDown);
            newCtrl.MouseUp += new MouseEventHandler(OnMouseUp);

        }

        private void OnClick(object sender, EventArgs e)
        {
            switch (FinalClick)
            {
                case MouseButtons.Left:
                    if (LeftClick != null)
                    {
                        LeftClick(sender, e);
                    }
                    break;

                case MouseButtons.Right:
                    if (RightClick != null)
                    {
                        RightClick(sender, e);
                    }
                    break;
                case MouseButtons.Middle:
                    if (MiddleClick != null)
                    {
                        MiddleClick(sender, e);
                    }
                    break;
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            FinalClick = e.Button;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        if (LeftMouseDown != null)
                        {
                            LeftMouseDown(sender, e);
                        }
                        break;
                    }

                case MouseButtons.Right:
                    {
                        if (RightMouseDown != null)
                        {
                            RightMouseDown(sender, e);
                        }
                        break;
                    }
                case MouseButtons.Middle:
                    {
                        if (MiddleMouseDown != null)
                        {
                            MiddleMouseDown(sender, e);
                        }
                        break;
                    }
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        if (LeftMouseUp != null)
                        {
                            LeftMouseUp(sender, e);
                        }
                        break;
                    }

                case MouseButtons.Right:
                    {
                        if (RightMouseUp != null)
                        {
                            RightMouseUp(sender, e);
                        }
                        break;
                    }

                case MouseButtons.Middle:
                    {
                        if (MiddleMouseUp != null)
                        {
                            MiddleMouseUp(sender, e);
                        }
                        break;
                    }
            }
        }
    }
    #endregion MouseControl Class

}