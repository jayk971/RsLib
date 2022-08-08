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
        #endregion Opt Path
        #region Other
        private static uint lstAxis;        // 座標軸
        private static uint lstUser;        // User資料
        private static uint lstHitPoint;    // 滑鼠點擊點
        #endregion Other
        #region Enable/Disable flag
        public bool blnShowAxis = true;
        public bool blnShowPolyline1 = false;
        public bool blnShowPolyline2 = false;
        public bool blnShowPolyline3 = false;
        public bool blnShowPolyline4 = false;
        public bool blnShowPolyline5 = false;
        public bool blnShowPoints1 = false;
        public bool blnShowPoints2 = false;
        public bool blnShowOptPath1 = false;
        public bool blnShowOptPath2 = false;

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

        public OGL(FormMain frm) : base()
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
            lstAxis = GL.glGenLists(12);            
            lstPolyline1 = lstAxis + 1;
            lstPolyline2 = lstAxis + 2;
            lstPolyline3 = lstAxis + 3;
            lstPolyline4 = lstAxis + 4;
            lstPolyline5 = lstAxis + 5;
            lstPoints1 = lstAxis + 6;
            lstPoints2 = lstAxis + 7;
            lstOptPath1 = lstAxis + 8;
            lstOptPath2 = lstAxis + 9;
            lstUser = lstAxis + 10;
            lstHitPoint = lstAxis + 11;

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
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
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
        private void SetColor(uint ColorId)
        {
            GL.glColor3d(   col[ColorId][0],
                            col[ColorId][1],
                            col[ColorId][2]
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
                GL.glTranslated(0.0, -100.0,  -150.0);      // 模型旋轉中心位置

                #region Call lists
                if (blnShowAxis) { GL.glCallList(lstAxis); }                
                if (blnShowPolyline1) { GL.glCallList(lstPolyline1); }
                if (blnShowPolyline2) { GL.glCallList(lstPolyline2); }                
                if (blnShowPolyline3) { GL.glCallList(lstPolyline3); }
                if (blnShowPolyline4) { GL.glCallList(lstPolyline4); }
                if (blnShowPolyline5) { GL.glCallList(lstPolyline5); }
                if (blnShowPoints1) { GL.glCallList(lstPoints1); }
                if (blnShowPoints2) { GL.glCallList(lstPoints2); }
                if (blnShowOptPath1) { GL.glCallList(lstOptPath1); }
                if (blnShowOptPath2) { GL.glCallList(lstOptPath2); }
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
        public void reset()
        {
            lock (matrixLock)
            {
                LastRot.setIdentity();                                // Reset Rotation
                ThisRot.setIdentity();                                // Reset Rotation
            }

            moveX = -tx;
            moveY = -ty;
            scale = 1.0;
            
            Draw();
        }

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
            Point tempAux = new Point(e.X, e.Y);
            mouseEndDrag = tempAux;

            if (blnShowHitPoint)
            {
                MouseHitCheck(tempAux, true);
            }
        }

        private void glOnMouseMove(object sender, MouseEventArgs e)
        {
            mouseMoveCount++;
            Point tempAux = new Point(e.X, e.Y);
            mouseEndDrag = tempAux;

            //每移動20次才計算一次，減輕CPU負擔
            if (mouseMoveCount % 20 == 0)
                mouseMoveCount = 0;
            if (blnShowHitPoint && mouseMoveCount == 0)
            {
                MouseHitCheck(tempAux, false);
            }

            if (isLeftDrag)
            {
                this.left_drag(tempAux);
                this.Draw();
            }

            if (isRightDrag)
            {
                this.Draw();
            }

            if (isMiddleDrag)
            {
                this.middle_drag(tempAux);
                this.Draw();
            }
        }

        private void glOnMouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta;

            //改變scale
            if (delta > 0)
            {
                scale *= (1 + 0.002 * delta);
                this.Draw();
            }
            else if (delta < 0)
            {
                scale /= (1 - 0.002 * delta);
                this.Draw();
            }
            else
            {
                scale = 1.0;
            }
        }

        private void glOnMouseEnter(object sender, EventArgs e)
        {
            if (!this.Focused)
                this.Focus();
        }
        private void glOnMouseLeave(object sender, EventArgs e)
        {
            if (this.Focused)
                this.Parent.Focus();
        }

        private void glOnLeftMouseDown(object sender, MouseEventArgs e)
        {
            isLeftDrag = true;
            Point tempAux = new Point(e.X, e.Y);
            this.startDrag(tempAux);
            this.Draw();
        }

        private void glOnLeftMouseUp(object sender, MouseEventArgs e)
        {
            isLeftDrag = false;
        }

        private void glOnRightMouseDown(object sender, MouseEventArgs e)
        {
            isRightDrag = true;
            Point tempAux = new Point(e.X, e.Y);
            this.startDrag(tempAux);
            this.Draw();
        }

        private void glOnRightMouseUp(object sender, MouseEventArgs e)
        {
            isRightDrag = false;
            this.reset();
        }

        private void glOnMiddleMouseDown(object sender, MouseEventArgs e)
        {
            isMiddleDrag = true;
            Point tempAux = new Point(e.X, e.Y);
            this.startDrag(tempAux);

            GetMousePos(mouseStartDrag, out startX, out startY);
                       
            this.Draw();
        }

        private void glOnMiddleMouseUp(object sender, MouseEventArgs e)
        {
            isMiddleDrag = false;
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