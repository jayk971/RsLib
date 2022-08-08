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
        Red,
        Green,
        Blue,
        Yellow,
        Cyan,
        Magenta
    };

    #region OpenGL Graphic Controller

    public class GLGraphicController : OpenGLControl
    {
        #region Color Code
        private static float[][] col = new float[][] {							    // Array For Box Colors
			new float[] {1.0f, 1.0f, 1.0f},                                         // White
            new float[] {0.0f, 0.0f, 0.0f},                                         // Black
            new float[] {0.5f, 0.5f, 0.5f},                                         // Gray
            new float[] {(float)0xD3/0xFF, (float)0xD3/0xFF, (float)0xD3/0xFF},     // BrightGray
            new float[] {(float)0xF5/0xFF, (float)0xF5/0xFF, (float)0xF5/0xFF},     // LightWhite
            new float[] {1.0f, 0.0f, 0.0f},                                         // Red
            new float[] {0.0f, 1.0f, 0.0f},                                         // Green,
            new float[] {0.0f, 0.0f, 1.0f},                                         // Blue
            new float[] {1.0f, 1.0f, 0.0f},                                         // Yellow
            new float[] {0.0f, 1.0f, 1.0f},                                         // Cyan
            new float[] {1.0f, 0.0f, 1.0f},                                         // Magenta            
		};
        #endregion

        private const int g_mouse_free = 0;
        private const int g_mouse_lock_p1 = 1;
        private const int g_mouse_lock_p2 = 2;

        private Form1 FrmMain;

        private int mouseMoveCount = 0;

        #region 繪圖模型定義
        
        private static uint lstAxis;
        private static uint lstCloud1;
        private static uint lstCloud2;
        private static uint lstCloud3;
        //private static uint lstCloud4;
        private static uint lstLine1;
        //private static uint lstLine2;
        //private static uint lstLine3;
        //private static uint lstLine4;
        private static uint lstMesh1;
        private static uint lstMesh2;
        private static uint lstInputCloud;
        //private static uint lstProcessedCloud1;
        //private static uint lstProcessedCloud2;
        //private static uint lstBoundaryLine1;
        //private static uint lstBoundaryLine2;
        //private static uint lstBoundaryLine3;
        private static uint lstMesh;
        private static uint lstNormal1;
        private static uint lstNormal2;
        private static uint lstPathProfile;
        private static uint lstPathProfile_2mm;
        private static uint lstPathProfile_ContactPoint;
        private static uint lstPathProfile_OriginBiteline;
        //private static uint lstPathBase;
        private static uint lstBoundaryBox1;
        private static uint lstBoundaryBox2;
        private static uint lstInputCloud2;
        //private static uint lstDivideLines;
        private static uint lstPrincepalYAxis;
        private static uint lstPrincepalXAxis;
        //private static uint lstTangents;
        //private static uint lstSpaceNodes;
        //private static uint lstTipMesh;
        private static uint lstBodyMesh;
        //private static uint lstTailMesh;
        private static uint lstIntersecLines;
        private static uint lstGeodesicLines;
        private static uint lstGeodesic2D;
        //private static uint lstSearchVolume;

        private static uint lstIntersecPoints;
        private static uint lstRobotPos;
        //private static uint lstRobotPosVad;
        private static uint lstDivideTriangle;
        //private static uint lstPathSingle;
        private static uint lstFeatureCloud;
        private static uint lstVOI;
        private static uint lstCurvatureCloud;
        //private static uint lstSegment;
        private static uint lstGoldenBitelineWholeShoe;
        private static uint lstGoldenBitelinePlusData;
        private static uint lstGoldenBitelineVOI;
        private static uint lstGoldenBitelineFeatureMap;
        private static uint lstGoldenBitelineCanidates;
        private static uint lstGoldenBitelineOnWholeShoe;
        private static uint lstGoldenBitelineOnPlus;
        private static uint lstSelectedPoint;
        private static uint lstOrderDisplayPoint;
        private static uint lstBitelineVec;
        //private static uint lstBitelineTriangles;
        private static uint lstDivideRegionPoints;
        private static uint lstPlanes;
        private static uint lstShoeFeatureRawCloud;
        private static uint lstShoeFeatureVOICloud;
        private static uint lstShoeFeatureGoldenFeatureCloud;
        private static uint lstShoeFeatureVOI;

        public bool blnShowAxis = true;
        public bool blnShowCloud1 = false;
        public bool blnShowCloud2 = false;
        public bool blnShowCloud3 = false;
        //public bool blnShowCloud4 = false;
        public bool blnShowLine1 = false;
        //public bool blnShowLine2 = false;
        //public bool blnShowLine3 = false;
        //public bool blnShowLine4 = false;
        public bool blnShowMesh1 = false;
        public bool blnShowMesh2 = false;
        public bool blnShowInputCloud = false;
        //public bool blnShowProcessedCloud1 = false;
        //public bool blnShowProcessedCloud2 = false;
        //public bool blnShowBoundaryLine1 = false;
        //public bool blnShowBoundaryLine2 = false;
        //public bool blnShowBoundaryLine3 = false;
        public bool blnShowMesh = false;
        public bool blnShowNormal1 = false;
        public bool blnShowNormal2 = false;
        public bool blnShowPathProfile = false;
        public bool blnShowPathProfile_2mm = false;
        public bool blnShowPathProfile_OriginBiteline = false;

        //public bool blnShowPathBase = false;
        public bool blnShowBoundaryBox1 = false;
        public bool blnShowBoundaryBox2 = false;
        public bool blnShowInputCloud2 = false;
        //public bool blnShowDivideLines = false;
        public bool blnShowPrincepalYAxis = false;
        public bool blnShowPrincepalXAxis = false;
        //public bool blnShowTangents = false;
        //public bool blnShowSpaceNodes = false;
        //public bool blnShowTipMesh = false;
        public bool blnShowBodyMesh = false;
        //public bool blnShowTailMesh = false;
        public bool blnShowIntersecLines = false;
        public bool blnShowGeodesicLines = false;
        public bool blnShowIntersecPoints = false;
        public bool blnShowRobotPos = false;
        //public bool blnShowRobotPosVad = false;
        public bool blnShowDivideTriangle = false;
        //public bool blnShowSinglePath = false;
        public bool blnShowFeaturesCloud = false;
        public bool blnShowVOI = false;
        public bool blnShowCurvatureCloud = false;
        //public bool blnShowSegment = false;
        public bool blnShowGoldenBitelineWholeShoe = false;
        public bool blnShowGoldenBitelinePlusData = false;
        public bool blnShowGoldenBitelineVOI = false;
        public bool blnShowGoldenBitelineFeatureMap = false;
        public bool blnShowGoldenBitelineCanidates = false;
        public bool blnShowGoldenBitelineOnWholeShoe = false;
        public bool blnShowGoldenBitelineOnPlus = false;
        public bool blnShowSelectedPoint = false;
        public bool blnShowOrderDisplayPoint = false;
        public bool blnShowBitelineVec = false;
        //public bool blnShowBitelineTriangle = false;
        public bool blnShowDivideRegionPoints = false;
        public bool blnShowPlanes = false;
        public bool blnShowShoeFeatureRawCloud = false;
        public bool blnShowShoeFeatureVOICloud = false;
        public bool blnShowShoeFeatureGoldenFeatureCloud = false;
        public bool blnShowShoeFeatureVOI = false;
        
        #endregion

        #region 繪圖參數
        
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

        public float[] BgColor = { 0.1f, 0.1f, 0.1f, 0.1f };

        #endregion

        public GLGraphicController(Form1 frm) : base()
        {
            FrmMain = frm;
            GC.Collect();
        }

        public void DisableAllDisplay()
        {
            blnShowCloud1 = false;
            blnShowCloud2 = false;
            blnShowCloud3 = false;
            //blnShowCloud4 = false;
            blnShowLine1 = false;
            //blnShowLine2 = false;
            //blnShowLine3 = false;
            //blnShowLine4 = false;
            blnShowMesh1 = false;
            blnShowMesh2 = false;
            blnShowInputCloud = false;
            //blnShowProcessedCloud1 = false;
            //blnShowProcessedCloud2 = false;
            //blnShowBoundaryLine1 = false;
            //blnShowBoundaryLine2 = false;
            //blnShowBoundaryLine3 = false;
            blnShowMesh = false;
            blnShowNormal1 = false;
            blnShowNormal2 = false;
            blnShowPathProfile = false;
            //blnShowPathBase = false;
            blnShowBoundaryBox1 = false;
            blnShowBoundaryBox2 = false;
            blnShowInputCloud2 = false;
            //blnShowDivideLines = false;
            blnShowPrincepalYAxis = false;
            blnShowPrincepalXAxis = false;
            //blnShowTangents = false;
            //blnShowSpaceNodes = false;
            //blnShowTipMesh = false;
            blnShowBodyMesh = false;
            //blnShowTailMesh = false;
            blnShowIntersecLines = false;
            blnShowGeodesicLines = false;
            blnShowIntersecPoints = false;
            blnShowRobotPos = false;
            //blnShowRobotPosVad = false;
            blnShowDivideTriangle = false;
            //blnShowSinglePath = false;
            blnShowFeaturesCloud = false;
            blnShowVOI = false;
            blnShowCurvatureCloud = false;
            //blnShowSegment = false;
            blnShowGoldenBitelineWholeShoe = false;
            blnShowGoldenBitelinePlusData = false;
            blnShowGoldenBitelineVOI = false;
            blnShowGoldenBitelineFeatureMap = false;
            blnShowGoldenBitelineCanidates = false;
            blnShowGoldenBitelineOnWholeShoe = false;
            blnShowGoldenBitelineOnPlus = false;
            blnShowSelectedPoint = false;
            blnShowOrderDisplayPoint = false;
            //blnShowBitelineTriangle = false;
            blnShowDivideRegionPoints = false;
            blnShowPlanes = false;
            blnShowShoeFeatureGoldenFeatureCloud = false;
            blnShowShoeFeatureVOI = false;
            blnShowShoeFeatureVOICloud = false;
            blnShowShoeFeatureRawCloud = false;
        }

        private void SelectColor(uint ColorId)
        {
            GL.glColor3d(col[ColorId][0],
                            col[ColorId][1],
                            col[ColorId][2]
                        );
        }

        protected override void InitGLContext()
        {
            GL.glShadeModel(GL.GL_SMOOTH);								// enable smooth shading            
            GL.glClearColor(0.1f, 0.1f, 0.1f, 0.1f);		
            GL.glClearDepth(1.0f);										// depth buffer setup           

            GL.glEnable(GL.GL_POINT_SMOOTH);                            // 開啟畫點反鋸齒
            GL.glEnable(GL.GL_LINE_SMOOTH);                             // 開啟畫線反鋸齒
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA); // 開啟透明度模式
            GL.glEnable(GL.GL_BLEND);

            GL.glHint(GL.GL_PERSPECTIVE_CORRECTION_HINT, GL.GL_NICEST);	// nice perspective calculations          

            lstAxis = GL.glGenLists(11);

            lstCloud1 = lstAxis + 1;
            lstCloud2 = lstAxis + 2;
            lstCloud3 = lstAxis + 3;
            //lstCloud4 = lstAxis + 4;
            lstLine1 = lstAxis + 5;
            //lstLine2 = lstAxis + 6;
            //lstLine3 = lstAxis + 7;
            //lstLine4 = lstAxis + 8;
            lstMesh1 = lstAxis + 9;
            lstMesh2 = lstAxis + 10;
            lstInputCloud = lstAxis + 11;
            //lstProcessedCloud1 = lstAxis + 12;
            //lstProcessedCloud2 = lstAxis + 13;
            //lstBoundaryLine1 = lstAxis + 14;
            //lstBoundaryLine2 = lstAxis + 15;
            //lstBoundaryLine3 = lstAxis + 16;
            lstMesh = lstAxis + 17;
            lstNormal1 = lstAxis + 18;
            lstNormal2 = lstAxis + 19;
            lstPathProfile = lstAxis + 20;
            //lstPathBase = lstAxis + 21;
            lstBoundaryBox1 = lstAxis + 22;
            lstBoundaryBox2 = lstAxis + 23;
            lstInputCloud2 = lstAxis + 24;
            //lstDivideLines = lstAxis + 25;
            lstPrincepalYAxis = lstAxis + 26;
            lstPrincepalXAxis = lstAxis + 27;
            //lstTangents = lstAxis + 28;
            //lstSpaceNodes = lstAxis + 29;
            //lstTipMesh = lstAxis + 30;
            lstBodyMesh = lstAxis + 31;
            //lstTailMesh = lstAxis + 32;
            lstIntersecLines = lstAxis + 33;
            lstGeodesicLines = lstAxis + 34;
            lstIntersecPoints = lstAxis + 35;
            lstRobotPos = lstAxis + 36;
            //lstRobotPosVad = lstAxis + 37;
            lstDivideTriangle = lstAxis + 38;
            //lstPathSingle = lstAxis + 39;
            lstFeatureCloud = lstAxis + 40;
            lstVOI = lstAxis + 41;
            lstCurvatureCloud = lstAxis + 42;
            //lstSegment = lstAxis + 43;
            lstGoldenBitelineWholeShoe = lstAxis + 44;
            lstGoldenBitelinePlusData = lstAxis + 45;
            lstGoldenBitelineVOI = lstAxis + 46;
            lstGoldenBitelineFeatureMap = lstAxis + 47;
            lstGoldenBitelineCanidates = lstAxis + 48;
            lstGoldenBitelineOnWholeShoe = lstAxis + 49;
            lstGoldenBitelineOnPlus = lstAxis + 50;
            lstSelectedPoint = lstAxis + 51;
            lstOrderDisplayPoint = lstAxis + 52;
            lstBitelineVec = lstAxis + 53;
            //lstBitelineTriangles = lstAxis + 54;
            lstDivideRegionPoints = lstAxis + 55;
            lstPlanes = lstAxis + 56;
            lstShoeFeatureGoldenFeatureCloud = lstAxis + 57;
            lstShoeFeatureRawCloud = lstAxis + 58;
            lstShoeFeatureVOI = lstAxis + 59;
            lstShoeFeatureVOICloud = lstAxis + 60;
            lstGeodesic2D = lstAxis + 61;
            lstPathProfile_2mm = lstAxis + 62;
            lstPathProfile_ContactPoint = lstAxis + 63;
            lstPathProfile_OriginBiteline = lstAxis + 64;

            BuildXYZAxis(20.0);

            LastRot.setIdentity(); // Reset Rotation
            ThisRot.setIdentity(); // Reset Rotation
            ThisRot.get_Renamed(matrix);

            LastScale = 1.0f;
            ThisScale = 1.0f;

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
            //GL.gluOrtho2D(-400, 400, -400, 400);

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

        public void Draw()
        {
            try
            {
                GL.glClearColor(BgColor[0], BgColor[1], BgColor[2], BgColor[3]);		

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

                if (blnShowCloud1)
                { GL.glCallList(lstCloud1); }

                if (blnShowCloud2)
                { GL.glCallList(lstCloud2); }

                if (blnShowCloud3)
                { GL.glCallList(lstCloud3); }

                //if (blnShowCloud4)
                //{ GL.glCallList(lstCloud4); }

                if (blnShowLine1)
                { GL.glCallList(lstLine1); }

                //if (blnShowLine2)
                //{ GL.glCallList(lstLine2); }

                //if (blnShowLine3)
                //{ GL.glCallList(lstLine3); }

                //if (blnShowLine4)
                //{ GL.glCallList(lstLine4); }

                if (blnShowMesh1)
                { GL.glCallList(lstMesh1); }

                if (blnShowMesh2)
                { GL.glCallList(lstMesh2); }

                if (blnShowInputCloud)
                { GL.glCallList(lstInputCloud); }

                //if (blnShowProcessedCloud1)
                //{ GL.glCallList(lstProcessedCloud1); }

                //if (blnShowProcessedCloud2)
                //{ GL.glCallList(lstProcessedCloud2); }
     
                //if (blnShowBoundaryLine1)
                //{ GL.glCallList(lstBoundaryLine1); }
                
                //if (blnShowBoundaryLine2)
                //{ GL.glCallList(lstBoundaryLine2); }

                //if (blnShowBoundaryLine3)
                //{ GL.glCallList(lstBoundaryLine3); }

                if (blnShowMesh)
                { GL.glCallList(lstMesh); }

                if (blnShowNormal1)
                { GL.glCallList(lstNormal1); }

                if (blnShowNormal2)
                { GL.glCallList(lstNormal2); }

                if (blnShowPathProfile)
                { GL.glCallList(lstPathProfile); }

                if (blnShowPathProfile_2mm)
                { GL.glCallList(lstPathProfile_2mm); }

                if (blnShowPathProfile_2mm)
                { GL.glCallList(lstPathProfile_ContactPoint); }

                if (blnShowPathProfile_OriginBiteline)
                { GL.glCallList(lstPathProfile_OriginBiteline); }
                //if (blnShowPathBase)
                //{ GL.glCallList(lstPathBase); }

                if (blnShowBoundaryBox1)
                { GL.glCallList(lstBoundaryBox1); }

                if (blnShowBoundaryBox2)
                { GL.glCallList(lstBoundaryBox2); }

                if (blnShowInputCloud2)
                { GL.glCallList(lstInputCloud2); }

                //if (blnShowDivideLines)
                //{ GL.glCallList(lstDivideLines); }

                if (blnShowPrincepalYAxis)
                { GL.glCallList(lstPrincepalYAxis); }

                if (blnShowPrincepalXAxis)
                { GL.glCallList(lstPrincepalXAxis); }

                //if (blnShowTangents)
                //{ GL.glCallList(lstTangents); }

                //if (blnShowSpaceNodes)
                //{ GL.glCallList(lstSpaceNodes); }

                //if (blnShowTipMesh)
                //{ GL.glCallList(lstTipMesh); }

                if (blnShowBodyMesh)
                { GL.glCallList(lstBodyMesh); }

                //if (blnShowTailMesh)
                //{ GL.glCallList(lstTailMesh); }

                if (blnShowIntersecLines)
                { GL.glCallList(lstIntersecLines); }

                if (blnShowGeodesicLines)
                { GL.glCallList(lstGeodesicLines); }

                if (blnShowGeodesicLines)
                { GL.glCallList(lstGeodesic2D); }


                if (blnShowIntersecPoints)
                { GL.glCallList(lstIntersecPoints); }

                if (blnShowRobotPos)
                { GL.glCallList(lstRobotPos); }

                //if (blnShowRobotPosVad)
                //{ GL.glCallList(lstRobotPosVad); }

                if (blnShowDivideTriangle)
                { GL.glCallList(lstDivideTriangle); }

                //if (blnShowSinglePath)
                //{ GL.glCallList(lstPathSingle); }

                if (blnShowFeaturesCloud)
                { GL.glCallList(lstFeatureCloud); }

                if (blnShowVOI)
                { GL.glCallList(lstVOI); }

                //if (blnShowVOI)
                //{ GL.glCallList(lstSearchVolume); }

                if (blnShowCurvatureCloud)
                { GL.glCallList(lstCurvatureCloud); }

                //if (blnShowSegment)
                //{ GL.glCallList(lstSegment); }

                if (blnShowGoldenBitelineWholeShoe)
                { GL.glCallList(lstGoldenBitelineWholeShoe); }

                if (blnShowGoldenBitelinePlusData)
                { GL.glCallList(lstGoldenBitelinePlusData); }

                if (blnShowGoldenBitelineVOI)
                { GL.glCallList(lstGoldenBitelineVOI); }

                if (blnShowGoldenBitelineFeatureMap)
                { GL.glCallList(lstGoldenBitelineFeatureMap); }

                if (blnShowGoldenBitelineCanidates)
                { GL.glCallList(lstGoldenBitelineCanidates); }

                if (blnShowGoldenBitelineOnWholeShoe)
                { GL.glCallList(lstGoldenBitelineOnWholeShoe); }

                if (blnShowGoldenBitelineOnPlus)
                { GL.glCallList(lstGoldenBitelineOnPlus); }

                if (blnShowSelectedPoint)
                { GL.glCallList(lstSelectedPoint); }

                if (blnShowOrderDisplayPoint)
                { GL.glCallList(lstOrderDisplayPoint); }

                if (blnShowBitelineVec)
                { GL.glCallList(lstBitelineVec); }

                //if (blnShowBitelineTriangle)
                //{ GL.glCallList(lstBitelineTriangles); }

                if (blnShowDivideRegionPoints)
                { GL.glCallList(lstDivideRegionPoints); }

                if (blnShowPlanes)
                { GL.glCallList(lstPlanes); }

                if (blnShowShoeFeatureGoldenFeatureCloud)
                { GL.glCallList(lstShoeFeatureGoldenFeatureCloud); }

                if (blnShowShoeFeatureRawCloud)
                { GL.glCallList(lstShoeFeatureRawCloud); }

                if (blnShowShoeFeatureVOICloud)
                { GL.glCallList(lstShoeFeatureVOICloud); }

                if (blnShowShoeFeatureVOI)
                { GL.glCallList(lstShoeFeatureVOI); }

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

        private void ProcessSelection(int xpos, int ypos)
        {
            if (blnShowCloud2)
            {
                int buffer_length = 64;
                uint[] select_buffer = new uint[buffer_length];
                int[] viewport = new int[4];

                GL.glSelectBuffer(buffer_length, select_buffer);
                GL.glGetIntegerv(GL.GL_VIEWPORT, viewport);
             
                GL.glMatrixMode(GL.GL_PROJECTION);
                
                GL.glPushMatrix();
                
                GL.glRenderMode(GL.GL_SELECT);

                GL.glLoadIdentity();
         
                GL.gluPickMatrix(xpos, viewport[3] - ypos + viewport[1], 2, 2, viewport);
          
                double aspect = (double)width / height;
                if (aspect >= 1.0)
                    GL.glOrtho(-view_range * aspect, view_range * aspect, -view_range, view_range, -8 * view_range, 8 * view_range);
                else
                    GL.glOrtho(-view_range, view_range, -view_range / aspect, view_range / aspect, -8 * view_range, 8 * view_range);
              
                GL.glMatrixMode(GL.GL_MODELVIEW);
                GL.glLoadIdentity();
                
                GL.glScaled(scale, scale, scale);
                GL.glTranslated(0.0, -100.0, -150.0);      // 模型旋轉中心位置
             
                DrawStrobelCloudForSelection();
            
                int hits = GL.glRenderMode(GL.GL_RENDER);

                if (hits == 1)
                {
                    // do something
                    double x = 0;
                    x += 1;
                }


                GL.glMatrixMode(GL.GL_PROJECTION);
                GL.glPopMatrix();

                GL.glMatrixMode(GL.GL_MODELVIEW);
        
            }
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

        private void glOnMouseClick(object sender, MouseEventArgs e)
        {
            Point tempAux = new Point(e.X, e.Y);
            mouseEndDrag = tempAux;
            /*
            if (FrmMain.IsPointSelectionStart)
                MouseHitCheck(tempAux, true);
             */
        }

        private void MouseHitCheck(Point mousePt, bool isClick)
        {
            /*
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

            minDist = CalculateNearestP(FrmMain.WholeShoeScanData, Pnear, Pfar, out Pnearest);

            FrmMain.UpdateSelectionPoint(Pnearest);
             */
        }

        private void glOnMouseMove(object sender, MouseEventArgs e)
        {
            mouseMoveCount++;
            Point tempAux = new Point(e.X, e.Y);
            mouseEndDrag = tempAux;

            //每移動20次才計算一次，減輕CPU負擔
            if (mouseMoveCount % 20 == 0)
                mouseMoveCount = 0;


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
            // ProcessSelection(e.X, e.Y);
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
        }

        private void glOnRightMouseUp(object sender, MouseEventArgs e)
        {
            isRightDrag = false;
            this.reset();
            this.Draw();
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

        #region build display lists inside source
        public void BuildSelectedPoint(Point3D SelectP)
        {
            if (!blnShowSelectedPoint)
                return;

            GL.glDeleteLists(lstSelectedPoint, 1);
            GL.glNewList(lstSelectedPoint, GL.GL_COMPILE);


            GL.glPointSize(20.0f);

            GL.glBegin(GL.GL_POINTS);


            GL.glColor3d(col[(uint)GLColor.Red][0],
                    col[(uint)GLColor.Red][1],
                    col[(uint)GLColor.Red][2]);


            GL.glVertex3d(SelectP.X, SelectP.Y, SelectP.Z);

            GL.glEnd();

            GL.glEndList();
        }

        public void BuildPlanes(Point3D center, Vector3D xy_vector, double width)
        {
            if (!blnShowPlanes)
                return;

            GL.glDeleteLists(lstPlanes, 1);
            GL.glNewList(lstPlanes, GL.GL_COMPILE);

            Point3D a = new Point3D(center.X + xy_vector.X * width / 2, center.Y + xy_vector.Y * width / 2, center.Z - width / 2);
            Point3D b = new Point3D(center.X + xy_vector.X * width / 2, center.Y + xy_vector.Y * width / 2, center.Z + width / 2);
            Point3D c = new Point3D(center.X - xy_vector.X * width / 2, center.Y - xy_vector.Y * width / 2, center.Z + width / 2);
            Point3D d = new Point3D(center.X - xy_vector.X * width / 2, center.Y - xy_vector.Y * width / 2, center.Z - width / 2);

            GL.glPointSize((float)5.0);

            GL.glColor3d(col[(uint)GLColor.Green][0],
                            col[(uint)GLColor.Green][1],
                            col[(uint)GLColor.Green][2]);

            GL.glBegin(GL.GL_POINTS);

            GL.glVertex3d(a.X, a.Y, a.Z);
            GL.glVertex3d(b.X, b.Y, b.Z);
            GL.glVertex3d(c.X, c.Y, c.Z);
            GL.glVertex3d(d.X, d.Y, d.Z);

            GL.glEnd();

            GL.glColor3d(col[(uint)GLColor.Magenta][0],
                            col[(uint)GLColor.Magenta][1],
                            col[(uint)GLColor.Magenta][2]);

            GL.glLineWidth(0.5f);

            GL.glBegin(GL.GL_LINES);

            GL.glVertex3d(a.X, a.Y, a.Z);
            GL.glVertex3d(b.X, b.Y, b.Z);

            GL.glVertex3d(b.X, b.Y, b.Z);
            GL.glVertex3d(c.X, c.Y, c.Z);

            GL.glVertex3d(c.X, c.Y, c.Z);
            GL.glVertex3d(d.X, d.Y, d.Z);

            GL.glVertex3d(d.X, d.Y, d.Z);
            GL.glVertex3d(a.X, a.Y, a.Z);

            GL.glEnd();

            GL.glColor4d(245.0 / 255,
                         245.0 / 255,
                         220.0 / 255,
                         0.1
                         );

            GL.glBegin(GL.GL_QUADS);

            GL.glVertex3d(a.X, a.Y, a.Z);
            GL.glVertex3d(b.X, b.Y, b.Z);
            GL.glVertex3d(c.X, c.Y, c.Z);
            GL.glVertex3d(d.X, d.Y, d.Z);

            GL.glEnd();

            GL.glEndList();
        }

        public void BuildDivideRegionPoints()
        {
            if (!blnShowDivideRegionPoints)
                return;

            GL.glDeleteLists(lstDivideRegionPoints, 1);
            GL.glNewList(lstDivideRegionPoints, GL.GL_COMPILE);

            lock (FrmMain.CurrentSelectPoints)
            {
                GL.glPointSize(20.0f);

                GL.glBegin(GL.GL_POINTS);

                for (int i = 0; i < FrmMain.CurrentSelectPoints.Count; i++)
                {
                    if (i == 0 || i == FrmMain.CurrentSelectPoints.Count - 1)
                    {
                        GL.glColor3d(col[(uint)GLColor.Red][0],
                              col[(uint)GLColor.Red][1],
                              col[(uint)GLColor.Red][2]);
                    }
                    else
                    {
                        GL.glColor3d(col[(uint)GLColor.Yellow][0],
                             col[(uint)GLColor.Yellow][1],
                             col[(uint)GLColor.Yellow][2]);
                    }

                    GL.glVertex3d(FrmMain.CurrentSelectPoints[i].X, FrmMain.CurrentSelectPoints[i].Y, FrmMain.CurrentSelectPoints[i].Z);
                }
                GL.glEnd();
            }
            GL.glEndList();
        }

        //public void BuildBitelineTriangles()
        //{
        //    if (!blnShowBitelineTriangle)
        //        return;

        //    GL.glDeleteLists(lstBitelineTriangles, 1);
        //    GL.glNewList(lstBitelineTriangles, GL.GL_COMPILE);

        //    lock (FrmMain.SampleProfiles)
        //    {

        //        GL.glColor3d(col[(uint)GLColor.Gray][0],
        //                        col[(uint)GLColor.Gray][1],
        //                        col[(uint)GLColor.Gray][2]);

        //        foreach (List<Point3D> triangle in FrmMain.SampleProfiles)
        //        {
        //            GL.glBegin(GL.GL_LINE_LOOP);

        //            GL.glVertex3d(triangle[0].X, triangle[0].Y, triangle[0].Z);
        //            GL.glVertex3d(triangle[1].X, triangle[1].Y, triangle[1].Z);
        //            GL.glVertex3d(triangle[2].X, triangle[2].Y, triangle[2].Z);
        //            GL.glVertex3d(triangle[0].X, triangle[0].Y, triangle[0].Z);

        //            GL.glEnd();
        //        }
        //    }

        //    GL.glEndList();
        //}

        //public void BuildSegments()
        //{
        //    if (!blnShowSegment)
        //        return;

        //    GL.glDeleteLists(lstSegment, 1);
        //    GL.glNewList(lstSegment, GL.GL_COMPILE);

        //    lock (FrmMain.OffsetSegment)
        //    {
        //        GL.glPointSize(5.0f);
        //        GL.glColor3d(1.0, 1.0, 0);

        //        GL.glBegin(GL.GL_POINTS);

        //        foreach (KeyValuePair<Polyline, Polyline> line in FrmMain.OffsetSegment)
        //        {
        //            GL.glVertex3d(line.Value.start.X, line.Value.start.Y, line.Value.start.Z);
        //            GL.glVertex3d(line.Value.end.X, line.Value.end.Y, line.Value.end.Z);
        //        }

        //        GL.glEnd();



        //        GL.glBegin(GL.GL_LINES);

        //        SelectColor((uint)GLColor.Green);

        //        foreach (KeyValuePair<Polyline, Polyline> line in FrmMain.OffsetSegment)
        //        {
        //            GL.glVertex3d(line.Value.start.X, line.Value.start.Y, line.Value.start.Z);

        //            GL.glVertex3d(line.Value.end.X, line.Value.end.Y, line.Value.end.Z);
        //        }


        //        GL.glEnd();

        //        GL.glBegin(GL.GL_LINES);

        //        SelectColor((uint)GLColor.LightWhite);

        //        foreach (KeyValuePair<Polyline, Polyline> line in FrmMain.OffsetSegment)
        //        {
        //            GL.glVertex3d(line.Key.start.X, line.Key.start.Y, line.Key.start.Z);

        //            GL.glVertex3d(line.Value.start.X, line.Value.start.Y, line.Value.start.Z);
        //        }


        //        GL.glEnd();
        //        GL.glBegin(GL.GL_LINES);

        //        SelectColor((uint)GLColor.LightWhite);

        //        foreach (KeyValuePair<Polyline, Polyline> line in FrmMain.OffsetSegment)
        //        {
        //            GL.glVertex3d(line.Key.end.X, line.Key.end.Y, line.Key.end.Z);

        //            GL.glVertex3d(line.Value.end.X, line.Value.end.Y, line.Value.end.Z);
        //        }


        //        GL.glEnd();
        //    }

        //    GL.glEndList();
        //}


        public void BuildCurvatureCloud()
        {
            if (!blnShowCurvatureCloud)
                return;

            GL.glDeleteLists(lstCurvatureCloud, 1);
            GL.glNewList(lstCurvatureCloud, GL.GL_COMPILE);

            GL.glPointSize(3.0f);

            GL.glBegin(GL.GL_POINTS);
            lock (FrmMain.CurvatureCloud)
            {
                foreach (Point3DWithCurvature p_c in FrmMain.CurvatureCloud)
                {

                    double ratio = 2 * p_c.Curvature;
                    double b = Math.Max(0, 1 - ratio);
                    double r = Math.Max(0, ratio - 1);
                    double g = 1 - b - r;

                    GL.glColor3d(r, g, b);

                    GL.glVertex3d(p_c.Point.X,
                                  p_c.Point.Y,
                                  p_c.Point.Z
                    );
                }
            }

            GL.glEnd();
            GL.glEndList();
        }

        public void BuildVOI()
        {
            if (!blnShowVOI)
                return;

            GL.glDeleteLists(lstVOI, 1);
            GL.glNewList(lstVOI, GL.GL_COMPILE);

            GL.glColor4d(245.0 / 255,
                         245.0 / 255,
                         220.0 / 255,
                         0.1);

            lock (FrmMain.VolumesOfInterest)
            {
                foreach (VOI VolumeOfInterest in FrmMain.VolumesOfInterest)
                {
                    GL.glColor3d(col[(uint)GLColor.Cyan][0],
                                    col[(uint)GLColor.Cyan][1],
                                    col[(uint)GLColor.Cyan][2]);

                    GL.glLineWidth(0.5f);

                    double xmin = VolumeOfInterest.VOICloudXMin;
                    double xmax = VolumeOfInterest.VOICloudXMax;
                    double ymin = VolumeOfInterest.VOICloudYMin;
                    double ymax = VolumeOfInterest.VOICloudYMax;
                    double zmin = VolumeOfInterest.VOICloudZMin;
                    double zmax = VolumeOfInterest.VOICloudZMax;

                    Point3D P1 = VolumeOfInterest.CubePointA;
                    Point3D P2 = VolumeOfInterest.CubePointD;
                    Point3D P3 = VolumeOfInterest.CubePointC;
                    Point3D P5 = VolumeOfInterest.CubePointB;

                    Point3D P4 = VolumeOfInterest.CubePointF;
                    Point3D P6 = VolumeOfInterest.CubePointG;
                    Point3D P7 = VolumeOfInterest.CubePointH;
                    Point3D P8 = VolumeOfInterest.CubePointE;

                    GL.glBegin(GL.GL_LINES);

                    GL.glVertex3d(P1.X, P1.Y, P1.Z);
                    GL.glVertex3d(P2.X, P2.Y, P2.Z);

                    GL.glVertex3d(P1.X, P1.Y, P1.Z);
                    GL.glVertex3d(P5.X, P5.Y, P5.Z);

                    GL.glVertex3d(P6.X, P6.Y, P6.Z);
                    GL.glVertex3d(P2.X, P2.Y, P2.Z);

                    GL.glVertex3d(P5.X, P5.Y, P5.Z);
                    GL.glVertex3d(P6.X, P6.Y, P6.Z);

                    GL.glVertex3d(P7.X, P7.Y, P7.Z);
                    GL.glVertex3d(P8.X, P8.Y, P8.Z);

                    GL.glVertex3d(P7.X, P7.Y, P7.Z);
                    GL.glVertex3d(P5.X, P5.Y, P5.Z);

                    GL.glVertex3d(P8.X, P8.Y, P8.Z);
                    GL.glVertex3d(P6.X, P6.Y, P6.Z);

                    GL.glVertex3d(P3.X, P3.Y, P3.Z);
                    GL.glVertex3d(P4.X, P4.Y, P4.Z);

                    GL.glVertex3d(P3.X, P3.Y, P3.Z);
                    GL.glVertex3d(P1.X, P1.Y, P1.Z);

                    GL.glVertex3d(P4.X, P4.Y, P4.Z);
                    GL.glVertex3d(P2.X, P2.Y, P2.Z);

                    GL.glVertex3d(P4.X, P4.Y, P4.Z);
                    GL.glVertex3d(P8.X, P8.Y, P8.Z);

                    GL.glVertex3d(P3.X, P3.Y, P3.Z);
                    GL.glVertex3d(P7.X, P7.Y, P7.Z);

                    GL.glEnd();

                    GL.glPointSize((float)5.0);

                    GL.glColor4d(1,
                 0,
                 0,
                 1.0);

                    GL.glBegin(GL.GL_POINTS);

                    GL.glVertex3d(P1.X, P1.Y, P1.Z);
                    GL.glVertex3d(P2.X, P2.Y, P2.Z);
                    GL.glVertex3d(P3.X, P3.Y, P3.Z);
                    GL.glVertex3d(P4.X, P4.Y, P4.Z);
                    GL.glVertex3d(P5.X, P5.Y, P5.Z);
                    GL.glVertex3d(P6.X, P6.Y, P6.Z);
                    GL.glVertex3d(P7.X, P7.Y, P7.Z);
                    GL.glVertex3d(P8.X, P8.Y, P8.Z);

                    GL.glEnd();

                    GL.glColor4d(245.0 / 255,
                                 245.0 / 255,
                                 220.0 / 255,
                                 0.1
                                 );

                    GL.glBegin(GL.GL_QUADS);

                    GL.glVertex3d(P1.X, P1.Y, P1.Z);
                    GL.glVertex3d(P2.X, P2.Y, P2.Z);
                    GL.glVertex3d(P4.X, P4.Y, P4.Z);
                    GL.glVertex3d(P3.X, P3.Y, P3.Z);

                    GL.glEnd();

                    GL.glBegin(GL.GL_QUADS);

                    GL.glVertex3d(P5.X, P5.Y, P5.Z);
                    GL.glVertex3d(P6.X, P6.Y, P6.Z);
                    GL.glVertex3d(P8.X, P8.Y, P8.Z);
                    GL.glVertex3d(P7.X, P7.Y, P7.Z);

                    GL.glEnd();

                    GL.glBegin(GL.GL_QUADS);

                    GL.glVertex3d(P1.X, P1.Y, P1.Z);
                    GL.glVertex3d(P5.X, P5.Y, P5.Z);
                    GL.glVertex3d(P6.X, P6.Y, P6.Z);
                    GL.glVertex3d(P2.X, P2.Y, P2.Z);

                    GL.glEnd();

                    GL.glBegin(GL.GL_QUADS);

                    GL.glVertex3d(P3.X, P3.Y, P3.Z);
                    GL.glVertex3d(P7.X, P7.Y, P7.Z);
                    GL.glVertex3d(P8.X, P8.Y, P8.Z);
                    GL.glVertex3d(P4.X, P4.Y, P4.Z);

                    GL.glEnd();

                    GL.glBegin(GL.GL_QUADS);

                    GL.glVertex3d(P1.X, P1.Y, P1.Z);
                    GL.glVertex3d(P5.X, P5.Y, P5.Z);
                    GL.glVertex3d(P7.X, P7.Y, P7.Z);
                    GL.glVertex3d(P3.X, P3.Y, P3.Z);

                    GL.glEnd();

                    GL.glBegin(GL.GL_QUADS);

                    GL.glVertex3d(P2.X, P2.Y, P2.Z);
                    GL.glVertex3d(P6.X, P6.Y, P6.Z);
                    GL.glVertex3d(P8.X, P8.Y, P8.Z);
                    GL.glVertex3d(P4.X, P4.Y, P4.Z);

                    GL.glEnd();
                }
            }

            GL.glEndList();
        }

        public void BuildRobotPos()
        {
            if (!blnShowRobotPos)
                return;

            GL.glDeleteLists(lstRobotPos, 1);
            GL.glNewList(lstRobotPos, GL.GL_COMPILE);

            GL.glLineWidth(2.0f);

            lock (FrmMain.RobotPos)
            {
                foreach (Dictionary<Point3D, List<Vector3D>> robot_poses in FrmMain.RobotPos)
                {
                    foreach (KeyValuePair<Point3D, List<Vector3D>> pos in robot_poses)
                    {
                        Point3D origin = pos.Key;

                        Vector3D x_axis = pos.Value[0];
                        Vector3D y_axis = pos.Value[1];
                        Vector3D z_axis = pos.Value[2];

                        double axis_length = 2;

                        GL.glBegin(GL.GL_LINES);

                        SelectColor((uint)GLColor.Red);
                        GL.glVertex3d(origin.X, origin.Y, origin.Z);

                        GL.glVertex3d(origin.X + x_axis.X * axis_length, origin.Y + x_axis.Y * axis_length, origin.Z + x_axis.Z * axis_length);

                        SelectColor((uint)GLColor.Green);
                        GL.glVertex3d(origin.X, origin.Y, origin.Z);

                        GL.glVertex3d(origin.X + y_axis.X * axis_length, origin.Y + y_axis.Y * axis_length, origin.Z + y_axis.Z * axis_length);

                        SelectColor((uint)GLColor.Blue);
                        GL.glVertex3d(origin.X, origin.Y, origin.Z);

                        GL.glVertex3d(origin.X + z_axis.X * axis_length, origin.Y + z_axis.Y * axis_length, origin.Z + z_axis.Z * axis_length);
                        GL.glEnd();

                    }
                }
            }

            GL.glEndList();
        }

        //public void BuildRobotPosVad()
        //{
        //    if (!blnShowRobotPosVad)
        //        return;

        //    GL.glDeleteLists(lstRobotPosVad, 1);
        //    GL.glNewList(lstRobotPosVad, GL.GL_COMPILE);

        //    GL.glLineWidth(2.0f);

        //    lock (FrmMain.RobotPosVad)
        //    {
        //        foreach (Dictionary<Point3D, List<Vector3D>> robot_poses in FrmMain.RobotPosVad)
        //        {
        //            foreach (KeyValuePair<Point3D, List<Vector3D>> pos in robot_poses)
        //            {

        //                Point3D origin = pos.Key;

        //                Vector3D x_axis = pos.Value[0];
        //                Vector3D y_axis = pos.Value[1];
        //                Vector3D z_axis = pos.Value[2];

        //                double axis_length = 1;

        //                GL.glBegin(GL.GL_LINES);

        //                SelectColor((uint)GLColor.Red);
        //                GL.glVertex3d(origin.X, origin.Y, origin.Z);

        //                GL.glVertex3d(origin.X + x_axis.X * axis_length, origin.Y + x_axis.Y * axis_length, origin.Z + x_axis.Z * axis_length);

        //                SelectColor((uint)GLColor.Green);
        //                GL.glVertex3d(origin.X, origin.Y, origin.Z);

        //                GL.glVertex3d(origin.X + y_axis.X * axis_length, origin.Y + y_axis.Y * axis_length, origin.Z + y_axis.Z * axis_length);

        //                SelectColor((uint)GLColor.Blue);
        //                GL.glVertex3d(origin.X, origin.Y, origin.Z);

        //                GL.glVertex3d(origin.X + z_axis.X * axis_length, origin.Y + z_axis.Y * axis_length, origin.Z + z_axis.Z * axis_length);
        //                GL.glEnd();

        //            }
        //        }

        //    }


        //    GL.glEndList();

        //}

        private void BuildXYZAxis(double AxisLength)
        {
            GL.glNewList(lstAxis, GL.GL_COMPILE);

            //座標軸
            GL.glLineWidth(2.0f);
            GL.glBegin(GL.GL_LINES);

            SelectColor((uint)GLColor.Red);
            GL.glVertex3d(0.0, 0.0, 0.0);
            GL.glVertex3d(AxisLength, 0.0, 0.0);

            SelectColor((uint)GLColor.Green);
            GL.glVertex3d(0.0, 0.0, 0.0);
            GL.glVertex3d(0.0, AxisLength, 0.0);

            SelectColor((uint)GLColor.Blue);
            GL.glVertex3d(0.0, 0.0, 0.0);
            GL.glVertex3d(0.0, 0.0, AxisLength);
            GL.glEnd();

            GL.glEndList();
        }

        public void BuildFeatureCloud()
        {
            if (!blnShowFeaturesCloud)
                return;

            GL.glDeleteLists(lstFeatureCloud, 1);
            GL.glNewList(lstFeatureCloud, GL.GL_COMPILE);

            GL.glColor3d(col[(uint)GLColor.BrightGray][0],
                                            col[(uint)GLColor.BrightGray][1],
                                            col[(uint)GLColor.BrightGray][2]);


            GL.glPointSize(1.2f);

            GL.glBegin(GL.GL_POINTS);

            lock (FrmMain.InputCloud)
            {
                foreach (Point3D p in FrmMain.BuffingPathGenerator.FilterCloud)
                {
                    GL.glVertex3d(p.X, p.Y, p.Z);
                }
            }

            GL.glEnd();
            GL.glEndList();
        }

        public void BuildInputCloud()
        {
            if (!blnShowInputCloud)
                return;

            GL.glDeleteLists(lstInputCloud, 1);

            GL.glNewList(lstInputCloud, GL.GL_COMPILE);

            GL.glColor3d(col[(uint)GLColor.BrightGray][0],
                            col[(uint)GLColor.BrightGray][1],
                            col[(uint)GLColor.BrightGray][2]);
            GL.glPointSize(1.0f);
            
            GL.glBegin(GL.GL_POINTS);
            
            lock (FrmMain.InputCloud)
            {
                foreach (Point3D p in FrmMain.InputCloud)
                {
                    GL.glVertex3d(p.X, p.Y, p.Z);
                }
            }

            GL.glEnd();

            GL.glEndList();
        }

        public void BuildReferenceCloud()
        {
            if (!blnShowCloud1)
                return;

            GL.glDeleteLists(lstCloud1, 1);
            GL.glNewList(lstCloud1, GL.GL_COMPILE);

            GL.glColor3d(col[(uint)GLColor.Yellow][0],
                            col[(uint)GLColor.Yellow][1],
                            col[(uint)GLColor.Yellow][2]);

            GL.glPointSize(4f);

            GL.glBegin(GL.GL_POINTS);
            lock (FrmMain.ReferenceCloud)
            {
                foreach (Point3D p in FrmMain.ReferenceCloud)
                {
                    GL.glVertex3d(p.X, p.Y, p.Z);
                }
            }
            GL.glEnd();
            GL.glEndList();
        }

        public void DrawStrobelCloudForSelection()
        {
            if (!blnShowCloud2)
                return;

            GL.glColor3d(col[(uint)GLColor.Red][0],
                            col[(uint)GLColor.Red][1],
                            col[(uint)GLColor.Red][2]);

            GL.glPointSize(16f);

            GL.glInitNames();
            GL.glPushName(0);
         
        }


        public void BuildStrobelCloud()
        {
            if (!blnShowCloud2)
                return;

            GL.glDeleteLists(lstCloud2, 1);
            GL.glNewList(lstCloud2, GL.GL_COMPILE);

            GL.glColor3d(col[(uint)GLColor.Yellow][0],
                            col[(uint)GLColor.Yellow][1],
                            col[(uint)GLColor.Yellow][2]);

            GL.glPointSize(16f);

            GL.glBegin(GL.GL_POINTS);

            lock (FrmMain.StitchingFeatureCloud)
            {
                foreach (Point3D pt in FrmMain.StitchingFeatureCloud)
                {
                    GL.glVertex3d(pt.X, pt.Y, pt.Z);
                }
            }

            GL.glEnd();
            GL.glEndList();
        }

        public void BuildDodgeProfiles()
        {
            if (!blnShowCloud3)
                return;

            GL.glDeleteLists(lstCloud3, 1);
            GL.glNewList(lstCloud3, GL.GL_COMPILE);


            GL.glColor3d(139.0 / 255.0,
                           0.0 / 255.0,
                            0.0 / 255.0);

            GL.glPointSize(5f);

            GL.glBegin(GL.GL_POINTS);
            lock (FrmMain.DodgeProfiles)
            {
                foreach (Point3D p in FrmMain.DodgeProfiles)
                {
                    GL.glVertex3d(p.X, p.Y, p.Z);
                }
            }
            GL.glEnd();
            GL.glEndList();
        }

        //public void BuildSingleProfiles()
        //{
        //    if (!blnShowCloud4)
        //        return;

        //    GL.glDeleteLists(lstCloud4, 1);
        //    GL.glNewList(lstCloud4, GL.GL_COMPILE);


        //    GL.glColor3d(148.0 / 255.0,
        //                   0.0 / 255.0,
        //                    211.0 / 255.0);


        //    GL.glPointSize(5f);

        //    GL.glBegin(GL.GL_POINTS);
            
        //    lock (FrmMain.SingleProfile)
        //    {
        //        foreach (Point3D p in FrmMain.SingleProfile)
        //        {
        //            GL.glVertex3d(p.X, p.Y, p.Z);
        //        }
        //    }

        //    GL.glEnd();
            
        //    GL.glColor3d(220.0 / 255.0,
        //                   20.0 / 255.0,
        //                    60.0 / 255.0);

        //    GL.glPointSize(50f);

        //    GL.glBegin(GL.GL_POINTS);
        //    GL.glVertex3d(FrmMain.CurrentSelectStrobelPoint.X,FrmMain.CurrentSelectStrobelPoint.Y,FrmMain.CurrentSelectStrobelPoint.Z);
        //    GL.glEnd();

        //    GL.glColor3d(100.0 / 255.0,
        //                 149.0 / 255.0,
        //                 237.0 / 255.0);

        //    GL.glPointSize(50f);
        //    GL.glBegin(GL.GL_POINTS);

        //    foreach (KeyValuePair<int, int> pair in FrmMain.SpecifyStrobelPointIndexs)
        //    {
        //        Point3D pt = FrmMain.BuffingPathGenerator.LUDataCrawler.ScanProfiles[pair.Key].Points[pair.Value];
        //        GL.glVertex3d(pt.X, pt.Y, pt.Z);
        //    }

        //    GL.glEnd();
        //    GL.glEndList();
        //}

        public void BuildIntersecPointCloud()
        {
            if (!blnShowIntersecPoints)
                return;

            GL.glDeleteLists(lstIntersecPoints, 1);
            GL.glNewList(lstIntersecPoints, GL.GL_COMPILE);

            GL.glColor3d(col[(uint)GLColor.Red][0],
                            col[(uint)GLColor.Red][1],
                            col[(uint)GLColor.Red][2]);

            GL.glPointSize(16f);

            GL.glBegin(GL.GL_POINTS);
            lock (FrmMain.IntersecPoint)
            {
                foreach (Point3D p in FrmMain.IntersecPoint
                    )
                {
                    if ( p != null )
                        GL.glVertex3d(p.X, p.Y, p.Z);
                }
            }
            GL.glEnd();
            GL.glEndList();
        }



        //public void BuildProfileCloud()
        //{
        //    if (!blnShowProcessedCloud1)
        //        return;

        //    GL.glDeleteLists(lstProcessedCloud1, 1);
        //    GL.glNewList(lstProcessedCloud1, GL.GL_COMPILE);

        //    GL.glColor3d(col[(uint)GLColor.Blue][0],
        //                    col[(uint)GLColor.Blue][1],
        //                    col[(uint)GLColor.Blue][2]);

        //    GL.glPointSize(2f);

        //    GL.glBegin(GL.GL_POINTS);
        //    lock (FrmMain.ProfileCloud)
        //    {
        //        foreach (Point3D p in FrmMain.ProfileCloud)
        //        {
        //            GL.glVertex3d(p.X, p.Y, p.Z);
        //        }
        //    }
        //    GL.glEnd();
        //    GL.glEndList();
        //}



        //public void BuildPointsNeedToBeBuff()
        //{
        //    if (!blnShowProcessedCloud2)
        //        return;

        //    GL.glDeleteLists(lstProcessedCloud2, 1);
        //    GL.glNewList(lstProcessedCloud2, GL.GL_COMPILE);


        //    GL.glColor3d(255.0 / 255.0,
        //                   69.0 / 255.0,
        //                    0.0 / 255.0);

        //    GL.glPointSize(2f);

        //    GL.glBegin(GL.GL_POINTS);
        //    lock (FrmMain.PointsNeedToBeBuff)
        //    {
        //        foreach (Point3D p in FrmMain.PointsNeedToBeBuff)
        //        {
        //            GL.glVertex3d(p.X, p.Y, p.Z);
        //        }
        //    }
        //    GL.glEnd();
        //    GL.glEndList();
        //}



        //public void BuildTipMesh()
        //{
        //    if (!blnShowTipMesh)
        //        return;

        //    GL.glDeleteLists(lstTipMesh, 1);
        //    GL.glNewList(lstTipMesh, GL.GL_COMPILE);

        //    GL.glLineWidth(0.5f);

        //    lock (FrmMain.TipMesh)
        //    {
        //        foreach (KeyValuePair<int, HashSet<Triangle>> tris in FrmMain.TipMesh.triangles)
        //        {
        //            foreach (Triangle triangle in tris.Value)
        //            {
        //                if (triangle.vertices.Count == 3)
        //                {

        //                    GL.glColor3d(col[(uint)GLColor.Gray][0],
        //                                    col[(uint)GLColor.Gray][1],
        //                                    col[(uint)GLColor.Gray][2]);


        //                    GL.glBegin(GL.GL_LINE_LOOP);

        //                    GL.glVertex3d(triangle.vertices[0].X, triangle.vertices[0].Y, triangle.vertices[0].Z);
        //                    GL.glVertex3d(triangle.vertices[1].X, triangle.vertices[1].Y, triangle.vertices[1].Z);
        //                    GL.glVertex3d(triangle.vertices[2].X, triangle.vertices[2].Y, triangle.vertices[2].Z);
        //                    GL.glVertex3d(triangle.vertices[0].X, triangle.vertices[0].Y, triangle.vertices[0].Z);

        //                    GL.glEnd();

        //                }

        //            }
        //        }

        //    }

        //    GL.glEndList();
        //}

        public void BuildBodyMesh()
        {
            if (!blnShowBodyMesh)
                return;

            GL.glDeleteLists(lstBodyMesh, 1);
            GL.glNewList(lstBodyMesh, GL.GL_COMPILE);

            GL.glLineWidth(0.5f);

            lock (FrmMain.BodyMesh)
            {
                foreach (KeyValuePair<int, HashSet<Triangle>> tris in FrmMain.BodyMesh.triangles)
                {
                    foreach (Triangle triangle in tris.Value)
                    {
                        
                        if (triangle.vertices.Count == 3)
                        {
                            GL.glColor3d(col[(uint)GLColor.Gray][0],
                                            col[(uint)GLColor.Gray][1],
                                            col[(uint)GLColor.Gray][2]);


                            GL.glBegin(GL.GL_LINE_LOOP);

                            GL.glVertex3d(triangle.vertices[0].X, triangle.vertices[0].Y, triangle.vertices[0].Z);
                            GL.glVertex3d(triangle.vertices[1].X, triangle.vertices[1].Y, triangle.vertices[1].Z);
                            GL.glVertex3d(triangle.vertices[2].X, triangle.vertices[2].Y, triangle.vertices[2].Z);
                            GL.glVertex3d(triangle.vertices[0].X, triangle.vertices[0].Y, triangle.vertices[0].Z);

                            GL.glEnd();

                        }
                        
                        // DRAW NORMAL VECTOR
                        /*
                        Point3D cog = Point3D.CenterOfGravity(triangle.vertices);

                        Point3D cog_end = new Point3D(cog.X + 1.0 * triangle.normal.X, cog.Y + 1.0 * triangle.normal.Y, cog.Z + 1.0 * triangle.normal.Z);

                        GL.glColor3d(col[(uint)GLColor.Red][0],
                                         col[(uint)GLColor.Red][1],
                                         col[(uint)GLColor.Red][2]);

                        GL.glBegin(GL.GL_LINE_LOOP);
                        
                        GL.glVertex3d(cog.X, cog.Y, cog.Z);

                        GL.glVertex3d(cog_end.X, cog_end.Y, cog_end.Z);

                        GL.glVertex3d(cog.X, cog.Y, cog.Z);
                        GL.glEnd();
                        */
                    }
                }

            }

            GL.glEndList();
        }

        //public void BuildTailMesh()
        //{
        //    if (!blnShowTailMesh)
        //        return;

        //    GL.glDeleteLists(lstTailMesh, 1);
        //    GL.glNewList(lstTailMesh, GL.GL_COMPILE);

        //    GL.glLineWidth(0.5f);

        //    lock (FrmMain.BodyMesh)
        //    {
        //        foreach (KeyValuePair<int, HashSet<Triangle>> tris in FrmMain.TailMesh.triangles)
        //        {
        //            foreach (Triangle triangle in tris.Value)
        //            {
        //                if (triangle.vertices.Count == 3)
        //                {

        //                    GL.glColor3d(col[(uint)GLColor.Gray][0],
        //                                    col[(uint)GLColor.Gray][1],
        //                                    col[(uint)GLColor.Gray][2]);


        //                    GL.glBegin(GL.GL_LINE_LOOP);

        //                    GL.glVertex3d(triangle.vertices[0].X, triangle.vertices[0].Y, triangle.vertices[0].Z);
        //                    GL.glVertex3d(triangle.vertices[1].X, triangle.vertices[1].Y, triangle.vertices[1].Z);
        //                    GL.glVertex3d(triangle.vertices[2].X, triangle.vertices[2].Y, triangle.vertices[2].Z);
        //                    GL.glVertex3d(triangle.vertices[0].X, triangle.vertices[0].Y, triangle.vertices[0].Z);

        //                    GL.glEnd();

        //                }

        //            }
        //        }

        //    }

        //    GL.glEndList();
        //}

        public void BuildMesh()
        {
            if (!blnShowMesh)
                return;

            GL.glDeleteLists(lstMesh, 1);
            GL.glNewList(lstMesh, GL.GL_COMPILE);

            GL.glLineWidth(0.5f);
            
            lock (FrmMain.Mesh)
            {
                foreach (KeyValuePair<int, HashSet<Triangle>> tris in FrmMain.Mesh.triangles)
                {
                    foreach (Triangle triangle in tris.Value)
                    {
                        if (triangle.vertices.Count == 3)
                        {

                            GL.glColor3d(col[(uint)GLColor.Gray][0],
                                            col[(uint)GLColor.Gray][1],
                                            col[(uint)GLColor.Gray][2]);


                            GL.glBegin(GL.GL_LINE_LOOP);

                            GL.glVertex3d(triangle.vertices[0].X, triangle.vertices[0].Y, triangle.vertices[0].Z);
                            GL.glVertex3d(triangle.vertices[1].X, triangle.vertices[1].Y, triangle.vertices[1].Z);
                            GL.glVertex3d(triangle.vertices[2].X, triangle.vertices[2].Y, triangle.vertices[2].Z);
                            GL.glVertex3d(triangle.vertices[0].X, triangle.vertices[0].Y, triangle.vertices[0].Z);

                            GL.glEnd();

                        }

                    }
                }

            }

            GL.glEndList();
        }

        //public void BuildPathSingle()
        //{
        //    if (!blnShowSinglePath)
        //        return;

        //    GL.glDeleteLists(lstPathSingle, 1);
        //    GL.glNewList(lstPathSingle, GL.GL_COMPILE);

        //    lock (FrmMain.OtherPath)
        //    {
        //        // normal
        //        GL.glLineWidth(1.5f);

        //        double length = 5.0;

        //        GL.glColor3d(col[(uint)GLColor.Cyan][0],
        //                        col[(uint)GLColor.Cyan][1],
        //                        col[(uint)GLColor.Cyan][2]);

        //        GL.glBegin(GL.GL_LINES);

        //            foreach (PathPoint vec in FrmMain.OtherPath)
        //            {
        //                GL.glVertex3d(vec.pos.X, vec.pos.Y, vec.pos.Z);
        //                GL.glVertex3d(vec.pos.X + length * vec.normal.X, vec.pos.Y + length * vec.normal.Y, vec.pos.Z + length * vec.normal.Z);

        //            }
                


        //        // path 
        //        GL.glColor3d(col[(uint)GLColor.Green][0],
        //                        col[(uint)GLColor.Green][1],
        //                        col[(uint)GLColor.Green][2]);

        //        for (int i = 0; i < FrmMain.OtherPath.Count; i++)
        //            {
        //                int index1 = i + 1;
        //                if (index1 == FrmMain.OtherPath.Count)
        //                    index1 = 0;

        //                GL.glVertex3d(FrmMain.OtherPath[i].pos.X,
        //                              FrmMain.OtherPath[i].pos.Y,
        //                              FrmMain.OtherPath[i].pos.Z
        //                  );
        //                GL.glVertex3d(FrmMain.OtherPath[index1].pos.X,
        //                              FrmMain.OtherPath[index1].pos.Y,
        //                              FrmMain.OtherPath[index1].pos.Z
        //                );
        //            }
                

        //        GL.glEnd();

        //    }

        //    GL.glEndList();
        //}


        //public void BuildPathTangent()
        //{
        //    if (!blnShowTangents)
        //        return;

        //    GL.glDeleteLists(lstTangents, 1);
        //    GL.glNewList(lstTangents, GL.GL_COMPILE);

        //    lock (FrmMain.Tangents)
        //    {

        //        // normal
        //        GL.glLineWidth(1.5f);

        //        double length = 5.0;


        //        GL.glColor3d(col[(uint)GLColor.Red][0],
        //                        col[(uint)GLColor.Red][1],
        //                        col[(uint)GLColor.Red][2]);


        //        GL.glBegin(GL.GL_LINES);

        //            foreach (PathPoint vec in FrmMain.Tangents)
        //            {
        //                GL.glVertex3d(vec.pos.X, vec.pos.Y, vec.pos.Z);
        //                GL.glVertex3d(vec.pos.X + length * vec.normal.X, vec.pos.Y + length * vec.normal.Y, vec.pos.Z + length * vec.normal.Z);

        //            }
                
        //        GL.glEnd();

        //    }

        //    GL.glEndList();
        //}

        public void BuildGeodesicLines()
        {
            if (!blnShowGeodesicLines)
                return;

            GL.glDeleteLists(lstGeodesicLines, 1);
            GL.glNewList(lstGeodesicLines, GL.GL_COMPILE);

            GL.glColor3d(255.0 / 255.0,
                            20.0 / 255.0,
                            147.0 / 255.0);

            GL.glLineWidth(0.5f);

            lock (FrmMain.GeodesicLines)
            {
                GL.glBegin(GL.GL_LINES);

                foreach (List<Point3D> geodesicLine in FrmMain.GeodesicLines)
                {
                    for (int i = 0; i < geodesicLine.Count - 1; i++)
                    {

                        GL.glVertex3d(geodesicLine[i].X,
                                      geodesicLine[i].Y,
                                      geodesicLine[i].Z
                        );

                        GL.glVertex3d(geodesicLine[i + 1].X,
                                      geodesicLine[i + 1].Y,
                                      geodesicLine[i + 1].Z
                        );
                    }
                }
                GL.glEnd();
                
                GL.glColor3d(255.0 / 255.0,
                            0,
                            0);

                GL.glPointSize((float)5.0);

                GL.glBegin(GL.GL_POINTS);

                foreach (List<Point3D> geodesicLine in FrmMain.GeodesicLines)
                {
                    for (int i = 0; i < geodesicLine.Count; i++)
                    {
                        GL.glVertex3d(geodesicLine[i].X,
                                   geodesicLine[i].Y,
                                   geodesicLine[i].Z
                     );

                    }
                }


                GL.glEnd();



            }

            GL.glEndList();
        }
        public void BuildGeodesic2D()
        {
            if (!blnShowGeodesicLines)
                return;

            GL.glDeleteLists(lstGeodesic2D, 1);
            GL.glNewList(lstGeodesic2D, GL.GL_COMPILE);

            GL.glColor3d(255.0 / 255.0,
               69.0 / 255.0,
                0.0 / 255.0);

            GL.glLineWidth(2f);

            lock (FrmMain.Geodesic2D)
            {

                foreach (Polyline geodesic2DLine in FrmMain.Geodesic2D)
                {

                    GL.glBegin(GL.GL_LINES);
                    GL.glVertex3d(geodesic2DLine.start.X,
                                      geodesic2DLine.start.Y,
                                      geodesic2DLine.start.Z
                        );

                    GL.glVertex3d(geodesic2DLine.end.X,
                                  geodesic2DLine.end.Y,
                                  geodesic2DLine.end.Z
                    );
                    GL.glEnd();
                    GL.glBegin(GL.GL_LINES);
                    GL.glVertex3d(geodesic2DLine.end.X,
                              geodesic2DLine.end.Y,
                              geodesic2DLine.end.Z
                );
                    GL.glVertex3d(geodesic2DLine.end.X,
                                  geodesic2DLine.end.Y,
                                  geodesic2DLine.end.Z + 100
                    );
                    GL.glEnd();
                }
         

                GL.glColor3d(255.0 / 255.0,
                            0,
                            0);

                GL.glPointSize((float)2.0);

                GL.glBegin(GL.GL_POINTS);

                foreach (Polyline geodesic2DLine in FrmMain.Geodesic2D)
                {

                    GL.glVertex3d(geodesic2DLine.start.X,
                                      geodesic2DLine.start.Y,
                                      geodesic2DLine.start.Z
                        );

                    GL.glVertex3d(geodesic2DLine.end.X,
                                  geodesic2DLine.end.Y,
                                  geodesic2DLine.end.Z
                    );

                    GL.glVertex3d(geodesic2DLine.end.X,
                                geodesic2DLine.end.Y,
                                geodesic2DLine.end.Z + 100
                    );
                }


                GL.glEnd();



            }

            GL.glEndList();
        }
        //public void BuildSearchVolume()
        //{
        //    if (!blnShowVOI)
        //        return;

        //    GL.glDeleteLists(lstSearchVolume, 1);
        //    GL.glNewList(lstSearchVolume, GL.GL_COMPILE);

        //    GL.glColor4d(col[(uint)GLColor.Green][0],
        //                col[(uint)GLColor.Green][1],
        //                col[(uint)GLColor.Green][2],
        //                0.5);

        //    GL.glLineWidth(2f);

        //    lock (FrmMain.SearchVolume)
        //    {

        //        foreach (List<Point3D> p in FrmMain.SearchVolume)
        //        {
        //            GL.glBegin(GL.GL_QUADS);
        //            GL.glVertex3d(p[0].X,p[0].Y,p[0].Z);
        //            GL.glVertex3d(p[0].X, p[0].Y, p[1].Z);
        //            GL.glVertex3d(p[1].X, p[0].Y, p[1].Z);
        //            GL.glVertex3d(p[1].X, p[0].Y, p[0].Z);
        //            GL.glEnd();
        //        }
        //    }

        //    GL.glEndList();
        //}

        public void BuildIntersecLines()
        {
            if (!blnShowIntersecLines)
                return;

            GL.glDeleteLists(lstIntersecLines, 1);
            GL.glNewList(lstIntersecLines, GL.GL_COMPILE);

            GL.glColor3d(255.0 / 255.0,
                            20.0 / 255.0,
                            147.0 / 255.0);

            GL.glLineWidth(3.0f);

            lock (FrmMain.IntersecLines)
            {
                GL.glBegin(GL.GL_LINES);

                foreach (List<Point3D> intersecLine in FrmMain.IntersecLines)
                {
                    for (int i = 0; i < intersecLine.Count - 1; i++)
                    {

                        GL.glVertex3d(intersecLine[i].X,
                                      intersecLine[i].Y,
                                      intersecLine[i].Z
                        );

                        GL.glVertex3d(intersecLine[i + 1].X,
                                      intersecLine[i + 1].Y,
                                      intersecLine[i + 1].Z
                        );
                    }
                }
                GL.glEnd();
            }

            GL.glEndList();
        }



        public void BuildPathProfile()
        {
            if (!blnShowPathProfile)
                return;

            GL.glDeleteLists(lstPathProfile, 1);
            GL.glNewList(lstPathProfile, GL.GL_COMPILE);

            lock (FrmMain.PathProfile)
            {

                // normal
                GL.glLineWidth(1.5f);

                double length = 5.0;


                GL.glColor3d(col[(uint)GLColor.Cyan][0],
                                col[(uint)GLColor.Cyan][1],
                                col[(uint)GLColor.Cyan][2]);


                GL.glBegin(GL.GL_LINES);

                foreach (List<PathPoint> profile in FrmMain.PathProfile)
                {
                    foreach (PathPoint vec in profile)
                    {
                        GL.glVertex3d(vec.pos.X, vec.pos.Y, vec.pos.Z);
                        GL.glVertex3d(vec.pos.X + length * vec.normal.X, vec.pos.Y + length * vec.normal.Y, vec.pos.Z + length * vec.normal.Z);

                    }
                }


               // path 
               GL.glColor3d(col[(uint)GLColor.Green][0],
                               col[(uint)GLColor.Green][1],
                               col[(uint)GLColor.Green][2]);
               
               foreach (List<PathPoint> profile in FrmMain.PathProfile)
               {
                   for (int i = 0; i < profile.Count; i++)
                   {
                       int index1 = i + 1;
                       if (index1 == profile.Count)
                           index1 = 0;

                       GL.glVertex3d(profile[i].pos.X,
                                     profile[i].pos.Y,
                                     profile[i].pos.Z
                         );
                       GL.glVertex3d(profile[index1].pos.X,
                                     profile[index1].pos.Y,
                                     profile[index1].pos.Z
                       );
                   }
               }
               // bitelineVec 
               GL.glColor3d(col[(uint)GLColor.Red][0],
                               col[(uint)GLColor.Red][1],
                               col[(uint)GLColor.Red][2]);
               length = 1;
               foreach (List<PathPoint> profile in FrmMain.PathProfile)
               {
                   foreach (PathPoint vec in profile)
                   {
                       GL.glVertex3d(vec.pos.X, vec.pos.Y, vec.pos.Z);
                       GL.glVertex3d(vec.pos.X + length * vec.bitelineVec.X, vec.pos.Y + length * vec.bitelineVec.Y, vec.pos.Z + length * vec.bitelineVec.Z);

                   }
               }


                GL.glEnd();
                  
            }

            GL.glEndList();
        }
        public void BuildPathProfile_ContactPoint()
        {
            if (!blnShowPathProfile_2mm)
                return;

            GL.glDeleteLists(lstPathProfile_ContactPoint, 1);
            GL.glNewList(lstPathProfile_ContactPoint, GL.GL_COMPILE);

            lock (FrmMain.PathProfile_ContactPoint)
            {

                // normal
                GL.glLineWidth(1.5f);



                

                foreach (List<PathPoint> profile in FrmMain.PathProfile_ContactPoint)
                {

                    for (int i = 0; i < profile.Count; i++)
                    {
                        if (profile[i].pos.flag)
                        {
                            GL.glColor4d(col[(uint)GLColor.Magenta][0],
                                        col[(uint)GLColor.Magenta][1],
                                        col[(uint)GLColor.Magenta][2],
                                        1);
                            int index1 = i + 1;
                            if (index1 == profile.Count)
                                index1 = 0;
                            GL.glBegin(GL.GL_LINES);
                            GL.glVertex3d(profile[i].pos.X,
                                          profile[i].pos.Y,
                                          profile[i].pos.Z
                              );
                            GL.glVertex3d(profile[index1].pos.X,
                                          profile[index1].pos.Y,
                                          profile[index1].pos.Z
                            );
                            GL.glEnd();
                        }
                        else
                        {
                            GL.glColor4d(col[(uint)GLColor.Red][0],
                                        col[(uint)GLColor.Red][1],
                                        col[(uint)GLColor.Red][2],
                                        1);
                            GL.glPointSize((float)6.0);
                            GL.glBegin(GL.GL_POINTS);
                            GL.glVertex3d(profile[i].pos.X,
                                          profile[i].pos.Y,
                                          profile[i].pos.Z
                              );
                            GL.glEnd();
                        }
                        //int index1 = i + 1;
                        //if (index1 == profile.Count)
                        //    index1 = 0;
                        //GL.glBegin(GL.GL_LINES);
                        //GL.glVertex3d(profile[i].pos.X,
                        //              profile[i].pos.Y,
                        //              profile[i].pos.Z
                        //  );
                        //GL.glVertex3d(profile[index1].pos.X,
                        //              profile[index1].pos.Y,
                        //              profile[index1].pos.Z
                        //);
                        //GL.glEnd();
                    }
                }
                

            }

            GL.glEndList();
        }
        public void BuildPathProfile_OriginBiteline()
        {
            if (!blnShowPathProfile_OriginBiteline)
                return;

            GL.glDeleteLists(lstPathProfile_OriginBiteline, 1);
            GL.glNewList(lstPathProfile_OriginBiteline, GL.GL_COMPILE);

            lock (FrmMain.PathProfile_OriginBiteline)
            {

                // normal
                GL.glLineWidth(1.5f);





                foreach (List<PathPoint> profile in FrmMain.PathProfile_OriginBiteline)
                {

                    for (int i = 0; i < profile.Count; i++)
                    {

                        GL.glColor4d(col[(uint)GLColor.Yellow][0],
                                    col[(uint)GLColor.Yellow][1],
                                    col[(uint)GLColor.Yellow][2],
                                    1);
                        int index1 = i + 1;
                        if (index1 == profile.Count)
                            index1 = 0;
                        GL.glBegin(GL.GL_LINES);
                        GL.glVertex3d(profile[i].pos.X,
                                        profile[i].pos.Y,
                                        profile[i].pos.Z
                            );
                        GL.glVertex3d(profile[index1].pos.X,
                                        profile[index1].pos.Y,
                                        profile[index1].pos.Z
                        );
                        GL.glEnd();

                    }
                }


            }

            GL.glEndList();
        }

        public void BuildPathProfile_2mm(double Height)
        {

            if (!blnShowPathProfile_2mm)
                return;

            GL.glDeleteLists(lstPathProfile_2mm, 1);
            GL.glNewList(lstPathProfile_2mm, GL.GL_COMPILE);

            lock (FrmMain.PathProfile_UnderContactPoint)
            {

                // normal
                GL.glLineWidth(1.5f);

                double HalfWidth = 2;//2*Math.Sqrt(2);
                
                //GL.glColor3d(col[(uint)GLColor.Red][0],
                //                col[(uint)GLColor.Red][1],
                //                col[(uint)GLColor.Red][2]);
#if !m
                foreach (List<PathPoint> profile in FrmMain.PathProfile_2mm)
                {
                    for (int i = 0; i < profile.Count; i++)
                    {
                        Point3D P0 = new Point3D(profile[i].pos.X, profile[i].pos.Y, profile[i].pos.Z);
                        Point3D P1 = new Point3D();
                        Point3D P2 = new Point3D();
                        Point3D P3 = new Point3D();
                        Point3D P4 = new Point3D();
                        /*

                         * P2-----------------------P3
                         * |                        |
                         * |                        |
                         * |                        |
                         * |                        |
                         * P1-----------P0---------P4
                         * 

                        */
                        //Vector3D RealBitelineVec = new Vector3D(profile[i].bitelineVec.X, profile[i].bitelineVec.Y, 0);
                        //RealBitelineVec = RealBitelineVec.UnitVector();
                        //Vector3D RealSurfaceVec = Vector3D.Cross(RealBitelineVec, profile[i].normal);
                        //RealSurfaceVec = RealSurfaceVec.UnitVector();

                        PathPoint ContactP = profile[i].DeepClone();

                        if (ContactP.ModifyQuants.ZOffsetByHand >= -1 && ContactP.ModifyQuants.ZOffsetByHand <= 2)
                        {
                            HalfWidth = 2;
                            //HalfWidth = Math.Sqrt(9 - Math.Pow(1 + ContactP.ModifyQuants.ZOffsetByHand, 2));
                        }
                        else if (ContactP.ModifyQuants.ZOffsetByHand < -1)
                        {
                            HalfWidth = 2;
                            //HalfWidth = 3;
                        }
                        else
                        {
                            HalfWidth = 0;
                        }

                        P1.X = P0.X + profile[i].bitelineVec.X * HalfWidth;
                        P1.Y = P0.Y + profile[i].bitelineVec.Y * HalfWidth;
                        P1.Z = P0.Z + profile[i].bitelineVec.Z * HalfWidth;

                        P2.X = P1.X + profile[i].surfaceVec.X * Height;
                        P2.Y = P1.Y + profile[i].surfaceVec.Y * Height;
                        P2.Z = P1.Z + profile[i].surfaceVec.Z * Height;

                        P3.X = P2.X + profile[i].bitelineVec.X * -2 * HalfWidth;
                        P3.Y = P2.Y + profile[i].bitelineVec.Y * -2 * HalfWidth;
                        P3.Z = P2.Z + profile[i].bitelineVec.Z * -2 * HalfWidth;

                        P4.X = P3.X + profile[i].surfaceVec.X * -1 * Height;
                        P4.Y = P3.Y + profile[i].surfaceVec.Y * -1 * Height;
                        P4.Z = P3.Z + profile[i].surfaceVec.Z * -1 * Height;


                        GL.glColor4d(col[(uint)GLColor.LightWhite][0],
                                    col[(uint)GLColor.LightWhite][1],
                                    col[(uint)GLColor.LightWhite][2],
                                    0.1);
                        GL.glBegin(GL.GL_QUADS);
                        GL.glVertex3d(P1.X, P1.Y, P1.Z);
                        GL.glVertex3d(P2.X, P2.Y, P2.Z);
                        GL.glVertex3d(P3.X, P3.Y, P3.Z);
                        GL.glVertex3d(P4.X, P4.Y, P4.Z);
                        GL.glEnd();



                        //GL.glColor4d(col[(uint)GLColor.Red][0],
                        //            col[(uint)GLColor.Red][1],
                        //            col[(uint)GLColor.Red][2],
                        //            1);
                        //GL.glBegin(GL.GL_LINES);
                        //GL.glVertex3d(P2.X, P2.Y, P2.Z);
                        //GL.glVertex3d(P3.X, P3.Y, P3.Z);
                        //GL.glEnd();


                        //GL.glColor4d(col[(uint)GLColor.Red][0],
                        //            col[(uint)GLColor.Red][1],
                        //            col[(uint)GLColor.Red][2],
                        //            1);
                        //GL.glBegin(GL.GL_LINES);
                        //GL.glVertex3d(P1.X, P1.Y, P1.Z);
                        //GL.glVertex3d(P4.X, P4.Y, P4.Z);
                        //GL.glEnd();
                    }
                }       
               

                GL.glPointSize((float)6.0);
                GL.glBegin(GL.GL_POINTS);
                foreach (List<PathPoint> profile in FrmMain.PathProfile_2mm)
                {
                    for (int i = 0; i < profile.Count; i++)
                    {
                        GL.glVertex3d(profile[i].pos.X,
                                      profile[i].pos.Y,
                                      profile[i].pos.Z
                          );
                    }
                }
                GL.glEnd();
#endif

                foreach (List<PathPoint> profile in FrmMain.PathProfile_UnderContactPoint)
                {

                    for (int i = 0; i < profile.Count; i++)
                    {

                        GL.glColor4d(col[(uint)GLColor.Magenta][0],
                                    col[(uint)GLColor.Magenta][1],
                                    col[(uint)GLColor.Magenta][2],
                                    1);


                        int index1 = i + 1;
                        if (index1 == profile.Count)
                            index1 = 0;
                        GL.glBegin(GL.GL_LINES);
                        GL.glVertex3d(profile[i].pos.X,
                                      profile[i].pos.Y,
                                      profile[i].pos.Z
                          );
                        GL.glVertex3d(profile[index1].pos.X,
                                      profile[index1].pos.Y,
                                      profile[index1].pos.Z
                        );
                        GL.glEnd();

                        //GL.glPointSize((float)6.0);
                        //GL.glBegin(GL.GL_POINTS);
                        //GL.glVertex3d(profile[i].pos.X,
                        //              profile[i].pos.Y,
                        //              profile[i].pos.Z
                        //  );
                        //GL.glEnd();
                    }
                }
                //foreach (List<PathPoint> profile in FrmMain.PathProfile_FixShiftError)
                //{
                //    for (int i = 0; i < profile.Count; i++)
                //    {

                //        GL.glColor4d(col[(uint)GLColor.Yellow][0],
                //                    col[(uint)GLColor.Yellow][1],
                //                    col[(uint)GLColor.Yellow][2],
                //                    1);


                //        int index1 = i + 1;
                //        if (index1 == profile.Count)
                //            index1 = 0;
                //        GL.glBegin(GL.GL_LINES);
                //        GL.glVertex3d(profile[i].pos.X,
                //                      profile[i].pos.Y,
                //                      profile[i].pos.Z
                //          );
                //        GL.glVertex3d(profile[index1].pos.X,
                //                      profile[index1].pos.Y,
                //                      profile[index1].pos.Z
                //        );
                //        GL.glEnd();

                        
                //        //GL.glPointSize((float)6.0);
                //        //GL.glBegin(GL.GL_POINTS);
                //        //GL.glVertex3d(profile[i].pos.X,
                //        //              profile[i].pos.Y,
                //        //              profile[i].pos.Z
                //        //  );
                //        //GL.glEnd();
                //    }
                //}

            }

            GL.glEndList();

        }

        //public void BuildPathBase()
        //{
        //    if (!blnShowPathBase)
        //        return;

        //    GL.glDeleteLists(lstPathBase, 1);
        //    GL.glNewList(lstPathBase, GL.GL_COMPILE);

        //    lock (FrmMain.PathBase)
        //    {

        //        GL.glColor3d(col[(uint)GLColor.Cyan][0],
        //                        col[(uint)GLColor.Cyan][1],
        //                        col[(uint)GLColor.Cyan][2]);
        //        // normal
        //        GL.glLineWidth(1.5f);

        //        double length = 3.0;

        //        GL.glBegin(GL.GL_LINES);

        //        foreach (List<PathPoint> profile in FrmMain.PathBase)
        //        {
        //            foreach (PathPoint vec in profile)
        //            {
        //                GL.glVertex3d(vec.pos.X, vec.pos.Y, vec.pos.Z);
        //                GL.glVertex3d(vec.pos.X + length * vec.normal.X, vec.pos.Y + length * vec.normal.Y, vec.pos.Z + length * vec.normal.Z);

        //            }
        //        }


        //        GL.glColor3d(col[(uint)GLColor.Green][0],
        //                        col[(uint)GLColor.Green][1],
        //                        col[(uint)GLColor.Green][2]);

        //        // path 
            
        //        foreach (List<PathPoint> profile in FrmMain.PathBase)
        //        {
        //            for (int i = 0; i < profile.Count; i++)
        //            {
        //                int index1 = i + 1;
        //                if (index1 == profile.Count)
        //                    index1 = 0;

        //                GL.glVertex3d(profile[i].pos.X,
        //                              profile[i].pos.Y,
        //                              profile[i].pos.Z
        //                  );
        //                GL.glVertex3d(profile[index1].pos.X,
        //                              profile[index1].pos.Y,
        //                              profile[index1].pos.Z
        //                );
        //            }
        //        }
          
        //        GL.glEnd();
              
        //    }

        //    GL.glEndList();
        //}

        
        //public void BuildBoundaryLine1()
        //{
        //    if (!blnShowBoundaryLine1)
        //        return;

        //    GL.glDeleteLists(lstBoundaryLine1, 1);
        //    GL.glNewList(lstBoundaryLine1, GL.GL_COMPILE);

        //    GL.glColor3d(col[(uint)GLColor.Magenta][0],
        //                    col[(uint)GLColor.Magenta][1],
        //                    col[(uint)GLColor.Magenta][2]);

        //    GL.glLineWidth(1.0f);
       

        //    lock (FrmMain.BoundaryLine1)
        //    {
        //        GL.glBegin(GL.GL_LINES);
        //        for (int i = 0; i < FrmMain.BoundaryLine1.Count; i++)
        //        {
        //            int index1 = i + 1;
        //            if (index1 == FrmMain.BoundaryLine1.Count)
        //                index1 = 0;

        //            GL.glVertex3d(FrmMain.BoundaryLine1[i].X,
        //                          FrmMain.BoundaryLine1[i].Y,
        //                          FrmMain.BoundaryLine1[i].Z
        //              );
        //            GL.glVertex3d(FrmMain.BoundaryLine1[index1].X,
        //                          FrmMain.BoundaryLine1[index1].Y,
        //                          FrmMain.BoundaryLine1[index1].Z
        //            );
        //        }


        //        GL.glEnd();

        //        GL.glColor3d(0,1,1);

        //        GL.glPointSize((float)5.0);

        //        GL.glBegin(GL.GL_POINTS);

        //          for (int i = 0; i < FrmMain.BoundaryLine1.Count; i++)
        //          {
        //                GL.glVertex3d(FrmMain.BoundaryLine1[i].X,
        //                               FrmMain.BoundaryLine1[i].Y,
        //                               FrmMain.BoundaryLine1[i].Z
        //                );

        //          }

        //        GL.glEnd();
        //    }

        //    GL.glEndList();
        //}

        //public void BuildBoundaryLine2()
        //{
        //    if (!blnShowBoundaryLine2)
        //        return;

        //    GL.glDeleteLists(lstBoundaryLine2, 1);
        //    GL.glNewList(lstBoundaryLine2, GL.GL_COMPILE);

        //    GL.glColor3d(85.0/255.0,
        //                    107.0/255.0,
        //                    47.0/255.0);

        //    GL.glLineWidth(3.0f);
        //    GL.glBegin(GL.GL_LINES);

        //    lock (FrmMain.BoundaryLine2)
        //    {
        //        for (int i = 0; i < FrmMain.BoundaryLine2.Count; i++)
        //        {
        //            int index1 = i + 1;
        //            if (index1 == FrmMain.BoundaryLine2.Count)
        //                index1 = 0;

        //            GL.glVertex3d(FrmMain.BoundaryLine2[i].X,
        //                          FrmMain.BoundaryLine2[i].Y,
        //                          FrmMain.BoundaryLine2[i].Z
        //            );

        //            GL.glVertex3d(FrmMain.BoundaryLine2[index1].X,
        //                          FrmMain.BoundaryLine2[index1].Y,
        //                          FrmMain.BoundaryLine2[index1].Z
        //            );
        //        }
        //    }

        //    GL.glEnd();
        //    GL.glEndList();
        //}


        //public void BuildBoundaryLine3()
        //{
        //    if (!blnShowBoundaryLine3)
        //        return;

        //    GL.glDeleteLists(lstBoundaryLine3, 1);
        //    GL.glNewList(lstBoundaryLine3, GL.GL_COMPILE);

        //    GL.glColor3d(240.0 / 255.0,
        //                    128.0 / 255.0,
        //                    128.0 / 255.0);

        //    GL.glLineWidth(2.0f);
        //    GL.glBegin(GL.GL_LINES);

        //    lock (FrmMain.BoundaryLine3)
        //    {
        //        for (int i = 0; i < FrmMain.BoundaryLine3.Count; i++)
        //        {
        //            int index1 = i + 1;
        //            if (index1 == FrmMain.BoundaryLine3.Count)
        //                index1 = 0;

        //            GL.glVertex3d(FrmMain.BoundaryLine3[i].X,
        //                          FrmMain.BoundaryLine3[i].Y,
        //                          FrmMain.BoundaryLine3[i].Z
        //            );

        //            GL.glVertex3d(FrmMain.BoundaryLine3[index1].X,
        //                          FrmMain.BoundaryLine3[index1].Y,
        //                          FrmMain.BoundaryLine3[index1].Z
        //            );
        //        }
        //    }

        //    GL.glEnd();
        //    GL.glEndList();
        //}


        public void BuildLine1()
        {
            if (!blnShowLine1)
                return;

            GL.glDeleteLists(lstLine1, 1);
            GL.glNewList(lstLine1, GL.GL_COMPILE);
                
            GL.glColor3d(255.0/255.0,
                            20.0/255.0,
                            147.0/255.0);

            GL.glLineWidth(3.0f);
            GL.glBegin(GL.GL_LINES);

            lock (FrmMain.StitchingFeatureLine)
            {
                for (int i = 0; i < FrmMain.StitchingFeatureLine.Count ; i++)
                {

                    int index = i + 1;

                    if (i == FrmMain.StitchingFeatureLine.Count - 1)
                        index = 0;

                    GL.glVertex3d(FrmMain.StitchingFeatureLine[i].X,
                                  FrmMain.StitchingFeatureLine[i].Y,
                                  FrmMain.StitchingFeatureLine[i].Z
                    );

                    GL.glVertex3d(FrmMain.StitchingFeatureLine[index].X,
                                  FrmMain.StitchingFeatureLine[index].Y,
                                  FrmMain.StitchingFeatureLine[index].Z
                    );
                }
            }



            GL.glEnd();
            GL.glEndList();
        }

        //public void BuildLine2()
        //{
        //    if (!blnShowLine2)
        //        return;

        //    GL.glDeleteLists(lstLine2, 1);
        //    GL.glNewList(lstLine2, GL.GL_COMPILE);

        //    GL.glColor3d(255.0 / 255.0,
        //                    20.0 / 255.0,
        //                    147.0 / 255.0);

        //    GL.glLineWidth(3.0f);
        //    GL.glBegin(GL.GL_LINES);

        //    lock (FrmMain.IntersecLine2)
        //    {
        //        for (int i = 0; i < FrmMain.IntersecLine2.Count - 1; i++)
        //        {

        //            GL.glVertex3d(FrmMain.IntersecLine2[i].X,
        //                          FrmMain.IntersecLine2[i].Y,
        //                          FrmMain.IntersecLine2[i].Z
        //            );

        //            GL.glVertex3d(FrmMain.IntersecLine2[i + 1].X,
        //                          FrmMain.IntersecLine2[i + 1].Y,
        //                          FrmMain.IntersecLine2[i + 1].Z
        //            );
        //        }
        //    }

        //    GL.glEnd();
        //    GL.glEndList();
        //}

        //public void BuildLine3()
        //{
        //    if (!blnShowLine3)
        //        return;

        //    GL.glDeleteLists(lstLine3, 1);
        //    GL.glNewList(lstLine3, GL.GL_COMPILE);

        //    GL.glColor3d(255.0 / 255.0,
        //                    20.0 / 255.0,
        //                    147.0 / 255.0);

        //    GL.glLineWidth(3.0f);
        //    GL.glBegin(GL.GL_LINES);

        //    lock (FrmMain.IntersecLine3)
        //    {
        //        for (int i = 0; i < FrmMain.IntersecLine3.Count - 1; i++)
        //        {

        //            GL.glVertex3d(FrmMain.IntersecLine3[i].X,
        //                          FrmMain.IntersecLine3[i].Y,
        //                          FrmMain.IntersecLine3[i].Z
        //            );

        //            GL.glVertex3d(FrmMain.IntersecLine3[i + 1].X,
        //                          FrmMain.IntersecLine3[i + 1].Y,
        //                          FrmMain.IntersecLine3[i + 1].Z
        //            );
        //        }
        //    }

        //    GL.glEnd();
        //    GL.glEndList();
        //}

        //public void BuildLine4()
        //{
        //    if (!blnShowLine4)
        //        return;

        //    GL.glDeleteLists(lstLine4, 1);
        //    GL.glNewList(lstLine4, GL.GL_COMPILE);

        //    GL.glColor3d(255.0 / 255.0,
        //                    20.0 / 255.0,
        //                    147.0 / 255.0);

        //    GL.glLineWidth(3.0f);
        //    GL.glBegin(GL.GL_LINES);

        //    lock (FrmMain.IntersecLine4)
        //    {
        //        for (int i = 0; i < FrmMain.IntersecLine4.Count - 1; i++)
        //        {

        //            GL.glVertex3d(FrmMain.IntersecLine4[i].X,
        //                          FrmMain.IntersecLine4[i].Y,
        //                          FrmMain.IntersecLine4[i].Z
        //            );

        //            GL.glVertex3d(FrmMain.IntersecLine4[i + 1].X,
        //                          FrmMain.IntersecLine4[i + 1].Y,
        //                          FrmMain.IntersecLine4[i + 1].Z
        //            );
        //        }
        //    }

        //    GL.glEnd();
        //    GL.glEndList();
        //}


        public void BuildDivideTriangle()
        {
            if (!blnShowDivideTriangle)
                return;

            GL.glDeleteLists(lstDivideTriangle, 1);
            GL.glNewList(lstDivideTriangle, GL.GL_COMPILE);

            GL.glColor4d(250.0 / 255.0,
                            250.0 / 255.0,
                            210.0 / 255.0,0.3);


            GL.glBegin(GL.GL_TRIANGLES);

            lock (FrmMain.DivideTriangle)
            {
                if (FrmMain.DivideTriangle.Count >= 3)
                {
                    GL.glVertex3d(FrmMain.DivideTriangle[0].X,
                               FrmMain.DivideTriangle[0].Y,
                               FrmMain.DivideTriangle[0].Z
                    );

                    GL.glVertex3d(FrmMain.DivideTriangle[1].X,
                                  FrmMain.DivideTriangle[1].Y,
                                  FrmMain.DivideTriangle[1].Z
                    );

                    GL.glVertex3d(FrmMain.DivideTriangle[2].X,
                                FrmMain.DivideTriangle[2].Y,
                                FrmMain.DivideTriangle[2].Z
                   );
                }
            }

            GL.glEnd();
            GL.glEndList();
        }

        //public void BuildDivideLines()
        //{
        //    if (!blnShowDivideLines)
        //        return;

        //    GL.glDeleteLists(lstDivideLines, 1);
        //    GL.glNewList(lstDivideLines, GL.GL_COMPILE);

        //    GL.glColor3d(col[(uint)GLColor.Red][0],
        //                    col[(uint)GLColor.Red][1],
        //                    col[(uint)GLColor.Red][2]);

        //    GL.glLineWidth(1.0f);
        //    GL.glBegin(GL.GL_LINES);

        //    lock (FrmMain.DivideLines)
        //    {
        //        for (int i = 0; i < FrmMain.DivideLines.Count; i++)
        //        {

        //            GL.glVertex3d(FrmMain.DivideLines[i].Key.X,
        //                          FrmMain.DivideLines[i].Key.Y,
        //                          FrmMain.DivideLines[i].Key.Z
        //              );
        //            GL.glVertex3d(FrmMain.DivideLines[i].Value.X,
        //                          FrmMain.DivideLines[i].Value.Y,
        //                          FrmMain.DivideLines[i].Value.Z
        //            );
        //        }
        //    }

        //    GL.glEnd();
        //    GL.glEndList();

        //}

        public void BuildPrincalpleYAxis()
        {
            if (!blnShowPrincepalYAxis)
                return;

            GL.glDeleteLists(lstPrincepalYAxis, 1);
            GL.glNewList(lstPrincepalYAxis, GL.GL_COMPILE);

            GL.glColor3d(col[(uint)GLColor.Gray][0],
                            col[(uint)GLColor.Gray][1],
                            col[(uint)GLColor.Gray][2]);


            GL.glLineWidth(2.0f);
            GL.glBegin(GL.GL_LINES);

            lock (FrmMain.PrincepalAxisY)
            {

                foreach (KeyValuePair<Point3D, Point3D> pair in FrmMain.PrincepalAxisY)
                {
                    GL.glVertex3d(pair.Key.X,
                                       pair.Key.Y,
                                      pair.Key.Z
                    );

                    GL.glVertex3d(pair.Value.X,
                                  pair.Value.Y,
                                  pair.Value.Z
                    );
                }
            }

            GL.glEnd();
            GL.glEndList();
        }


        public void BuildPrincalpleXAxis()
        {
            if (!blnShowPrincepalXAxis)
                return;

            GL.glDeleteLists(lstPrincepalXAxis, 1);
            GL.glNewList(lstPrincepalXAxis, GL.GL_COMPILE);

            GL.glColor3d(col[(uint)GLColor.Gray][0],
                            col[(uint)GLColor.Gray][1],
                            col[(uint)GLColor.Gray][2]);


            GL.glLineWidth(2.0f);
            GL.glBegin(GL.GL_LINES);

            lock (FrmMain.PrincepalAxisXs)
            {
                for (int i = 0; i < FrmMain.PrincepalAxisXs.Count; i++)
                {

                    GL.glVertex3d(FrmMain.PrincepalAxisXs[i].Key.X,
                                  FrmMain.PrincepalAxisXs[i].Key.Y,
                                  FrmMain.PrincepalAxisXs[i].Key.Z
                      );
                    GL.glVertex3d(FrmMain.PrincepalAxisXs[i].Value.X,
                                  FrmMain.PrincepalAxisXs[i].Value.Y,
                                  FrmMain.PrincepalAxisXs[i].Value.Z
                    );
                }
               
            }

            GL.glEnd();
            GL.glEndList();
        }

        //public void BuildSpaceNodes()
        //{
        //    if (!blnShowSpaceNodes)
        //        return;

        //    GL.glDeleteLists(lstSpaceNodes, 1);
        //    GL.glNewList(lstSpaceNodes, GL.GL_COMPILE);

        //    GL.glColor3d(col[(uint)GLColor.BrightGray][0],
        //                    col[(uint)GLColor.BrightGray][1],
        //                    col[(uint)GLColor.BrightGray][2]);
        //    GL.glLineWidth(0.1f);
           
        //    lock (FrmMain.SpaceNodes)
        //    {

        //        foreach (FeatureNode node in FrmMain.SpaceNodes)
        //        {
        //            GL.glBegin(GL.GL_LINE_STRIP); 

        //            GL.glVertex3d(node.Indice_NX_NY_NZ.X,
        //                          node.Indice_NX_NY_NZ.Y,
        //                          node.Indice_NX_NY_NZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_PX_NY_NZ.X,
        //                          node.Indice_PX_NY_NZ.Y,
        //                          node.Indice_PX_NY_NZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_PX_PY_NZ.X,
        //                          node.Indice_PX_PY_NZ.Y,
        //                          node.Indice_PX_PY_NZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_NX_PY_NZ.X,
        //                          node.Indice_NX_PY_NZ.Y,
        //                          node.Indice_NX_PY_NZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_NX_NY_NZ.X,
        //                     node.Indice_NX_NY_NZ.Y,
        //                     node.Indice_NX_NY_NZ.Z
        //            );

        //            GL.glEnd();

        //            GL.glBegin(GL.GL_LINE_STRIP);

        //            GL.glVertex3d(node.Indice_NX_NY_PZ.X,
        //                          node.Indice_NX_NY_PZ.Y,
        //                          node.Indice_NX_NY_PZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_PX_NY_PZ.X,
        //                          node.Indice_PX_NY_PZ.Y,
        //                          node.Indice_PX_NY_PZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_PX_PY_PZ.X,
        //                          node.Indice_PX_PY_PZ.Y,
        //                          node.Indice_PX_PY_PZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_NX_PY_PZ.X,
        //                          node.Indice_NX_PY_PZ.Y,
        //                          node.Indice_NX_PY_PZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_NX_NY_PZ.X,
        //                     node.Indice_NX_NY_PZ.Y,
        //                     node.Indice_NX_NY_PZ.Z
        //            );

        //            GL.glEnd();


        //            GL.glBegin(GL.GL_LINE_STRIP);

        //            GL.glVertex3d(node.Indice_NX_NY_NZ.X,
        //                     node.Indice_NX_NY_NZ.Y,
        //                     node.Indice_NX_NY_NZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_NX_NY_PZ.X,
        //                     node.Indice_NX_NY_PZ.Y,
        //                     node.Indice_NX_NY_PZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_NX_PY_PZ.X,
        //                     node.Indice_NX_PY_PZ.Y,
        //                     node.Indice_NX_PY_PZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_NX_PY_NZ.X,
        //                     node.Indice_NX_PY_NZ.Y,
        //                     node.Indice_NX_PY_NZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_NX_NY_NZ.X,
        //                     node.Indice_NX_NY_NZ.Y,
        //                     node.Indice_NX_NY_NZ.Z
        //            );

        //            GL.glEnd();


        //            GL.glBegin(GL.GL_LINE_STRIP);

        //            GL.glVertex3d(node.Indice_PX_NY_NZ.X,
        //                     node.Indice_PX_NY_NZ.Y,
        //                     node.Indice_PX_NY_NZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_PX_NY_PZ.X,
        //                     node.Indice_PX_NY_PZ.Y,
        //                     node.Indice_PX_NY_PZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_PX_PY_PZ.X,
        //                     node.Indice_PX_PY_PZ.Y,
        //                     node.Indice_PX_PY_PZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_PX_PY_NZ.X,
        //                     node.Indice_PX_PY_NZ.Y,
        //                     node.Indice_PX_PY_NZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_PX_NY_NZ.X,
        //                     node.Indice_PX_NY_NZ.Y,
        //                     node.Indice_PX_NY_NZ.Z
        //            );

        //            GL.glEnd();


        //            GL.glBegin(GL.GL_LINE_STRIP);

        //            GL.glVertex3d(node.Indice_NX_NY_NZ.X,
        //                     node.Indice_NX_NY_NZ.Y,
        //                     node.Indice_NX_NY_NZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_PX_NY_NZ.X,
        //                     node.Indice_PX_NY_NZ.Y,
        //                     node.Indice_PX_NY_NZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_PX_NY_PZ.X,
        //                     node.Indice_PX_NY_PZ.Y,
        //                     node.Indice_PX_NY_PZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_NX_NY_PZ.X,
        //                     node.Indice_NX_NY_PZ.Y,
        //                     node.Indice_NX_NY_PZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_NX_NY_NZ.X,
        //                          node.Indice_NX_NY_NZ.Y,
        //                          node.Indice_NX_NY_NZ.Z
        //            );

        //            GL.glEnd();


        //            GL.glBegin(GL.GL_LINE_STRIP);

        //            GL.glVertex3d(node.Indice_NX_PY_NZ.X,
        //                     node.Indice_NX_PY_NZ.Y,
        //                     node.Indice_NX_PY_NZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_PX_PY_NZ.X,
        //                     node.Indice_PX_PY_NZ.Y,
        //                     node.Indice_PX_PY_NZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_PX_PY_PZ.X,
        //                     node.Indice_PX_PY_PZ.Y,
        //                     node.Indice_PX_PY_PZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_NX_PY_PZ.X,
        //                     node.Indice_NX_PY_PZ.Y,
        //                     node.Indice_NX_PY_PZ.Z
        //            );

        //            GL.glVertex3d(node.Indice_NX_PY_NZ.X,
        //                          node.Indice_NX_PY_NZ.Y,
        //                          node.Indice_NX_PY_NZ.Z
        //            );

        //            GL.glEnd();

        //        }
        //    }

        //    GL.glEndList();
        //}

        #endregion

    }

    #endregion

    #region MouseControl
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
    #endregion MouseControl

}