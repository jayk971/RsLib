using netDxf;
//using netDxf.Tables;
//using netDxf.Blocks;
//using netDxf.Collections;
using netDxf.Entities;
using netDxf.Objects;
using RsLib.PointCloud;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RsLib.DXF
{
    public partial class DXFReader
    {
        public string FilePath = "";
        public Dictionary<string, DXFItem> _Items = new Dictionary<string, DXFItem>();
        private Dictionary<string, Group> _DXFItems = new Dictionary<string, Group>();

        public Point2D Max = new Point2D();
        public Point2D Min = new Point2D();
        public Point2D Avg = new Point2D();

        private Matrix4 ReflectX;
        private Matrix4 ReflectY;

        public Point2D PizzaMax = new Point2D();
        public Point2D PizzaMin = new Point2D();
        public double Width
        {
            get
            {
                return Math.Round(Max.X - Min.X, 1);
            }
        }
        public double Height
        {
            get
            {
                return Math.Round(Max.Y - Min.Y, 1);
            }
        }
        public DXFReader()
        {
            FilePath = "";
            _Items = new Dictionary<string, DXFItem>();
            _DXFItems = new Dictionary<string, Group>();
            ReflectY.M11 = -1;
            ReflectY.M22 = 1;
            ReflectY.M33 = 1;
            ReflectY.M44 = 1;


            ReflectX.M11 = 1;
            ReflectX.M22 = -1;
            ReflectX.M33 = 1;
            ReflectX.M44 = 1;

        }
        public Exception LoadDXF(string DXFPath)
        {
            try
            {
                FilePath = DXFPath;

                _DXFItems.Clear();
                DxfDocument DXFdoc;

                if (!File.Exists(DXFPath)) throw new Exception("DXF File Not Exist");
                FileInfo info = new FileInfo(DXFPath);
                using (FileStream fs = info.Open(FileMode.Open))
                {
                    DXFdoc = DxfDocument.Load(fs);
                }



                foreach (Circle C in DXFdoc.Circles)
                {
                    if (_DXFItems.ContainsKey(C.Layer.Name))
                    {
                        _DXFItems[C.Layer.Name].Entities.Add(C);
                    }
                    else
                    {
                        Group group = new Group(C.Layer.Name);
                        group.Entities.Add(C);
                        _DXFItems.Add(C.Layer.Name, group);
                    }
                }
                foreach (LwPolyline L in DXFdoc.LwPolylines)
                {
                    if (_DXFItems.ContainsKey(L.Layer.Name)) _DXFItems[L.Layer.Name].Entities.Add(L);
                    else
                    {
                        Group group = new Group(L.Layer.Name);
                        group.Entities.Add(L);
                        _DXFItems.Add(L.Layer.Name, group);
                    }
                }
                foreach (netDxf.Entities.Polyline L in DXFdoc.Polylines)
                {
                    if (_DXFItems.ContainsKey(L.Layer.Name)) _DXFItems[L.Layer.Name].Entities.Add(L);
                    else
                    {
                        Group group = new Group(L.Layer.Name);
                        group.Entities.Add(L);
                        _DXFItems.Add(L.Layer.Name, group);
                    }
                }

                RenewDXFItem();




                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<RsLib.PointCloud.Polyline> Get3DPolyline()
        {
            List<RsLib.PointCloud.Polyline> Output = new List<RsLib.PointCloud.Polyline>();
            foreach (KeyValuePair<string, DXFItem> kvp in _Items)
            {
                Output.AddRange(kvp.Value.Get3DPolylines());
            }
            return Output;
        }
        public List<RsLib.PointCloud.Polyline> Get3DPolyline(string ItemName)
        {
            List<RsLib.PointCloud.Polyline> Output = new List<RsLib.PointCloud.Polyline>();

            if (_Items.ContainsKey(ItemName)) return _Items[ItemName].Get3DPolylines();
            else return null;
        }
        public void Output3DPolyline(string OutputFilePath)
        {
            foreach (KeyValuePair<string, DXFItem> kvp in _Items)
            {
                List<RsLib.PointCloud.Polyline> tmp = kvp.Value.Get3DPolylines();
                for (int i = 0; i < tmp.Count; i++)
                {
                    tmp[i].SaveAsOpt(OutputFilePath, true);
                }
            }
        }
        private void RenewDXFItem()
        {
            _Items.Clear();
            double MinXValue = double.MaxValue;
            double MinYValue = double.MaxValue;
            double MaxXValue = double.MinValue;
            double MaxYValue = double.MinValue;

            foreach (KeyValuePair<string, Group> kvp in _DXFItems)
            {
                DXFItem tempItem = new DXFItem(kvp.Key);

                for (int i = 0; i < kvp.Value.Entities.Count; i++)
                {
                    Type EntityType = kvp.Value.Entities[i].GetType();
                    DXFSegment tempSegment;
                    if (EntityType == typeof(LwPolyline))
                    {
                        LwPolyline L = (LwPolyline)kvp.Value.Entities[i];
                        tempSegment = new DXFSegment(L);
                        tempItem.Add(tempSegment);

                        if (tempSegment.Min.X <= MinXValue) MinXValue = tempSegment.Min.X;
                        if (tempSegment.Min.Y <= MinYValue) MinYValue = tempSegment.Min.Y;
                        if (tempSegment.Max.X >= MaxXValue) MaxXValue = tempSegment.Max.X;
                        if (tempSegment.Max.Y >= MaxYValue) MaxYValue = tempSegment.Max.Y;
                    }
                    if (EntityType == typeof(netDxf.Entities.Polyline))
                    {
                        netDxf.Entities.Polyline L = (netDxf.Entities.Polyline)kvp.Value.Entities[i];
                        tempSegment = new DXFSegment(L);
                        tempItem.Add(tempSegment);

                        if (tempSegment.Min.X <= MinXValue) MinXValue = tempSegment.Min.X;
                        if (tempSegment.Min.Y <= MinYValue) MinYValue = tempSegment.Min.Y;
                        if (tempSegment.Max.X >= MaxXValue) MaxXValue = tempSegment.Max.X;
                        if (tempSegment.Max.Y >= MaxYValue) MaxYValue = tempSegment.Max.Y;
                    }
                    if (EntityType == typeof(Circle))
                    {
                        Circle C = (Circle)kvp.Value.Entities[i];
                        tempSegment = new DXFSegment(C);
                        tempItem.Add(tempSegment);

                        if (tempSegment.Min.X <= MinXValue) MinXValue = tempSegment.Min.X;
                        if (tempSegment.Min.Y <= MinYValue) MinYValue = tempSegment.Min.Y;
                        if (tempSegment.Max.X >= MaxXValue) MaxXValue = tempSegment.Max.X;
                        if (tempSegment.Max.Y >= MaxYValue) MaxYValue = tempSegment.Max.Y;
                    }
                }
                _Items.Add(tempItem.FullName, tempItem);
            }

            List<string> ItemsKey = _Items.Keys.ToList();


            Max = new Point2D(MaxXValue, MaxYValue);
            Min = new Point2D(MinXValue, MinYValue);
            double AvgX = Math.Round((Max.X + Min.X) / 2, 2);
            double AvgY = Math.Round((Max.Y + Min.Y) / 2, 2);

            Avg = new Point2D(AvgX, AvgY);
            //if (_Items.ContainsKey("0"))
            //{
            //    _Items.Remove("0");
            //}
        }
        public Dictionary<int, List<Point2D>> ConvertItem(string InputItemName)
        {
            DXFItem InputItem = _Items[InputItemName];
            Dictionary<int, List<Point2D>> Output = new Dictionary<int, List<Point2D>>();
            for (int i = 0; i < InputItem._Segment.Count; i++)
            {
                List<Point2D> TempCollection = new List<Point2D>();
                for (int j = 0; j < InputItem._Segment[i]._Points.Count; j++)
                {
                    Point2D tempP = new Point2D(InputItem._Segment[i]._Points[j].X,
                        InputItem._Segment[i]._Points[j].Y,
                        InputItem._Segment[i]._Points[j].Radius, 0);
                    TempCollection.Add(tempP);
                }
                Output.Add(i, TempCollection);
            }
            return Output;
        }
        public void SaveDXF(string ModelDXFFolder, int _MirrorScale, bool IsUpsideDown, bool IsSaveOuterOnly = true)  //save for each layer by layer naming.
        {
            try
            {
                List<string> LayerNames = _Items.Keys.ToList();
                DxfDocument saving5xOriginDXF = new DxfDocument();
                object LockMe = new object();
                ParallelLoopResult PResult = Parallel.For(0, LayerNames.Count, (int k, ParallelLoopState P) =>
                {
                    string _layerName = LayerNames[k];


                    if (_layerName != "0")
                    {
                        Group ScaledEntities = new Group();
                        Group MirrorEntities = new Group();

                        DxfDocument savingDXF = new DxfDocument();
                        DxfDocument savingmirrorDXF = new DxfDocument();

                        Matrix4 ScaleMatrix = Matrix4.Scale((double)_MirrorScale);
                        Matrix4 MirrorMatrix = new Matrix4();

                        if (IsUpsideDown) MirrorMatrix = ReflectX;
                        else MirrorMatrix = ReflectY;

                        if (IsSaveOuterOnly)
                        {
                            #region Save outer region
                            Point2D Min = new Point2D();
                            Point2D Max = new Point2D();
                            EntityObject Outer = null;

                            int Count = 0;
                            foreach (EntityObject EO in _DXFItems[_layerName].Entities)
                            {
                                if (EO.GetType() == typeof(Circle))
                                {
                                    Circle C = (Circle)EO;
                                    DXFSegment temp = new DXFSegment(C);
                                    if (Count == 0)
                                    {
                                        Min.X = temp.Min.X;
                                        Min.Y = temp.Min.Y;
                                        Max.X = temp.Max.X;
                                        Max.Y = temp.Max.Y;
                                        Outer = EO;
                                    }
                                    else
                                    {
                                        if (temp.Min.X <= Min.X && temp.Min.Y <= Min.Y && temp.Max.X >= Max.X && temp.Max.Y >= Max.Y)
                                        {
                                            Min.X = temp.Min.X;
                                            Min.Y = temp.Min.Y;
                                            Max.X = temp.Max.X;
                                            Max.Y = temp.Max.Y;
                                            Outer = EO;
                                        }
                                        else if (temp.Min.X > Min.X && temp.Min.Y > Min.Y && temp.Max.X < Max.X && temp.Max.Y < Max.Y)
                                        {
                                            //比 Count 0 小
                                        }
                                        else
                                        {
                                            // 重疊
                                            EntityObject Other = (EntityObject)EO.Clone();
                                            Other.TransformBy(ScaleMatrix);
                                            Other.IsVisible = true;
                                            Other.Layer.IsVisible = true;
                                            ScaledEntities.Entities.Add(Other);
                                        }
                                    }

                                }
                                if (EO.GetType() == typeof(LwPolyline))
                                {
                                    LwPolyline LW = (LwPolyline)EO;
                                    DXFSegment temp = new DXFSegment(LW);
                                    if (Count == 0)
                                    {
                                        Min.X = temp.Min.X;
                                        Min.Y = temp.Min.Y;
                                        Max.X = temp.Max.X;
                                        Max.Y = temp.Max.Y;
                                        Outer = EO;
                                    }
                                    else
                                    {
                                        if (temp.Min.X <= Min.X && temp.Min.Y <= Min.Y && temp.Max.X >= Max.X && temp.Max.Y >= Max.Y)
                                        {
                                            Min.X = temp.Min.X;
                                            Min.Y = temp.Min.Y;
                                            Max.X = temp.Max.X;
                                            Max.Y = temp.Max.Y;
                                            Outer = EO;
                                        }
                                        else if (temp.Min.X > Min.X && temp.Min.Y > Min.Y && temp.Max.X < Max.X && temp.Max.Y < Max.Y)
                                        {
                                            //比 Count 0 小
                                        }
                                        else
                                        {
                                            // 重疊
                                            EntityObject Other = (EntityObject)EO.Clone();
                                            Other.TransformBy(ScaleMatrix);
                                            Other.IsVisible = true;
                                            Other.Layer.IsVisible = true;
                                            ScaledEntities.Entities.Add(Other);
                                        }
                                    }
                                }
                                if (EO.GetType() == typeof(netDxf.Entities.Polyline))
                                {
                                    netDxf.Entities.Polyline PL = (netDxf.Entities.Polyline)EO;
                                    DXFSegment temp = new DXFSegment(PL);
                                    if (Count == 0)
                                    {
                                        Min.X = temp.Min.X;
                                        Min.Y = temp.Min.Y;
                                        Max.X = temp.Max.X;
                                        Max.Y = temp.Max.Y;
                                        Outer = EO;
                                    }
                                    else
                                    {
                                        if (temp.Min.X <= Min.X && temp.Min.Y <= Min.Y && temp.Max.X >= Max.X && temp.Max.Y >= Max.Y)
                                        {

                                            //比 Count 0 大
                                            Min.X = temp.Min.X;
                                            Min.Y = temp.Min.Y;
                                            Max.X = temp.Max.X;
                                            Max.Y = temp.Max.Y;
                                            Outer = EO;
                                        }
                                        else if (temp.Min.X > Min.X && temp.Min.Y > Min.Y && temp.Max.X < Max.X && temp.Max.Y < Max.Y)
                                        {
                                            //比 Count 0 小
                                        }
                                        else
                                        {
                                            // 重疊
                                            EntityObject Other = (EntityObject)EO.Clone();
                                            Other.TransformBy(ScaleMatrix);
                                            Other.IsVisible = true;
                                            Other.Layer.IsVisible = true;
                                            ScaledEntities.Entities.Add(Other);
                                        }
                                    }
                                }
                                Count++;
                            }

                            EntityObject Scaled = (EntityObject)Outer.Clone();
                            Scaled.TransformBy(ScaleMatrix);
                            Scaled.IsVisible = true;
                            Scaled.Layer.IsVisible = true;
                            ScaledEntities.Entities.Add(Scaled);
                            #endregion
                        }
                        else
                        {
                            foreach (EntityObject EO in _DXFItems[_layerName].Entities)
                            {
                                EntityObject Scaled = (EntityObject)EO.Clone();
                                Scaled.TransformBy(ScaleMatrix);
                                Scaled.IsVisible = true;
                                Scaled.Layer.IsVisible = true;
                                ScaledEntities.Entities.Add(Scaled);
                            }
                        }
                        savingDXF.Groups.Add(ScaledEntities);

                        Group ScaledEntitiesClone = (Group)ScaledEntities.Clone();
                        lock (LockMe)
                        {
                            saving5xOriginDXF.Groups.Add(ScaledEntitiesClone);
                        }
                        foreach (EntityObject EO in ScaledEntities.Entities)
                        {
                            EntityObject Mirrored = (EntityObject)EO.Clone();
                            Mirrored.TransformBy(MirrorMatrix);
                            Mirrored.IsVisible = true;
                            Mirrored.Layer.IsVisible = true;
                            MirrorEntities.Entities.Add(Mirrored);
                        }
                        savingmirrorDXF.Groups.Add(MirrorEntities);




                        //string RecipeFolderPath = ModelDXFFolder;
                        string fileName = ModelDXFFolder + "\\" + _layerName + ".dxf";
                        string mirrorfileName = ModelDXFFolder + "\\" + _layerName + "_mi.dxf";
                        if (!Directory.Exists(ModelDXFFolder)) Directory.CreateDirectory(ModelDXFFolder);


                        savingDXF.Save(fileName);
                        //savingmirrorDXF.Save(mirrorfileName);

                    }
                });
                //string RecipeFolderPath2 = CommonFunc.GetRecipeDXFFolderPath(_modelname, ModelTypeString, _size);
                string[] splitData = ModelDXFFolder.Split('\\');
                string fileName2 = ModelDXFFolder + "\\" + splitData[splitData.Length - 2] + ".dxf";

                saving5xOriginDXF.Save(fileName2);
            }
            catch (Exception ex)
            {
                string Exm = ex.Message;
            }
        }
        public void Save(string DXFFilePath)
        {
            try
            {
                List<string> LayerNames = _Items.Keys.ToList();
                DxfDocument savingDXF = new DxfDocument();

                for (int i = 0; i < LayerNames.Count; i++)
                {
                    string _layerName = LayerNames[i];
                    Group ScaledEntities = new Group();

                    foreach (EntityObject EO in _DXFItems[_layerName].Entities)
                    {
                        EntityObject m_Object = (EntityObject)EO.Clone();
                        EO.IsVisible = true;
                        EO.Layer.IsVisible = true;
                        ScaledEntities.Entities.Add(m_Object);
                    }
                    savingDXF.Groups.Add(ScaledEntities);
                }

                savingDXF.Save(DXFFilePath);

            }
            catch (Exception ex)
            {
                string Exm = ex.Message;
            }
        }
        public void Scale(double inScale)
        {
            try
            {
                List<string> LayerNames = _Items.Keys.ToList();
                Matrix4 ScaleMatrix = Matrix4.Scale(inScale);

                for (int i = 0; i < LayerNames.Count; i++)
                {
                    string _layerName = LayerNames[i];

                    foreach (EntityObject EO in _DXFItems[_layerName].Entities)
                    {
                        EO.TransformBy(ScaleMatrix);
                    }
                }
                RenewDXFItem();
            }
            catch (Exception ex)
            {
                string Exm = ex.Message;
            }

        }
        public void Rotate(double inAngle)
        {
            try
            {
                List<string> LayerNames = _Items.Keys.ToList();
                Matrix4 ScaleMatrix = Matrix4.RotationZ(inAngle / 180 * Math.PI);

                for (int i = 0; i < LayerNames.Count; i++)
                {
                    string _layerName = LayerNames[i];

                    foreach (EntityObject EO in _DXFItems[_layerName].Entities)
                    {
                        EO.TransformBy(ScaleMatrix);
                    }
                }
                RenewDXFItem();
            }
            catch (Exception ex)
            {
                string Exm = ex.Message;
            }

        }
        public void Translate(double inX, double inY)
        {
            try
            {
                List<string> LayerNames = _Items.Keys.ToList();
                Matrix4 ScaleMatrix = Matrix4.Translation(inX, inY, 0.0);

                for (int i = 0; i < LayerNames.Count; i++)
                {
                    string _layerName = LayerNames[i];

                    foreach (EntityObject EO in _DXFItems[_layerName].Entities)
                    {
                        EO.TransformBy(ScaleMatrix);
                    }
                }
                RenewDXFItem();
            }
            catch (Exception ex)
            {
                string Exm = ex.Message;
            }

        }
        public void RotateShift(double inAngle, double inX, double inY)
        {
            Rotate(inAngle);
            Translate(inX, inY);
        }
        public void Add(string inItemName, Group inItem)
        {
            if (!_DXFItems.ContainsKey(inItemName))
            {
                _DXFItems.Add(inItemName, inItem);
                RenewDXFItem();
            }
            else
            {
                throw new Exception(string.Format("Layer Name : {0} is duplicated!", inItemName));
            }
        }
        public void Add(string inItemName, DXFReader inItem)
        {
            if (!_DXFItems.ContainsKey(inItemName))
            {
                foreach (KeyValuePair<string, Group> kvp in inItem._DXFItems)
                {
                    string NewItemName = string.Format("{0}_{1}", inItemName, kvp.Key);
                    _DXFItems.Add(NewItemName, kvp.Value);
                }
                RenewDXFItem();
            }
            else
            {
                throw new Exception(string.Format("Layer Name : {0} is duplicated!", inItemName));
            }
        }
        public void Remove(string inItemName)
        {
            if (_DXFItems.ContainsKey(inItemName))
            {
                _DXFItems.Remove(inItemName);
                RenewDXFItem();
            }
        }
        public Exception MirrorDXF(bool IsUpsideDown)
        {
            Matrix4 MirrorMatrix;
            object LockMe = new object();

            if (IsUpsideDown) MirrorMatrix = ReflectY;
            else MirrorMatrix = ReflectX;
            try
            {
                List<string> LayerNames = _Items.Keys.ToList();
                Dictionary<string, Group> MirroredDXF = new Dictionary<string, Group>();
                ParallelLoopResult PResult = Parallel.For(0, LayerNames.Count, (int k, ParallelLoopState P) =>
                {
                    string _layerName = LayerNames[k];
                    Group MirrorGroup = new Group(_layerName);

                    foreach (EntityObject EO in _DXFItems[_layerName].Entities)
                    {
                        EntityObject Mirrored = (EntityObject)EO.Clone();
                        Mirrored.TransformBy(MirrorMatrix);
                        Mirrored.IsVisible = true;
                        Mirrored.Layer.IsVisible = true;
                        MirrorGroup.Entities.Add(Mirrored);
                    }
                    lock (LockMe)
                    {
                        MirroredDXF.Add(_layerName, MirrorGroup);
                    }
                });
                _DXFItems = MirroredDXF;

                RenewDXFItem();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}