using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using netDxf.Entities;
using System.Drawing;
using RsLib.PointCloud;
using RsLib.Common;
using netDxf;
namespace RsLib.DXF
{

    [Serializable]
    public class DXFPoint
    {
        public double X = 0;
        public double Y = 0;
        public bool IsStartPoint = true;
        public double Radius = 0;

        public DXFPoint(double PointX, double PointY)
        {
            X = PointX;
            Y = PointY;
            Radius = 0;
        }
        public DXFPoint(double PointX, double PointY, double PointR)
        {
            X = PointX;
            Y = PointY;
            Radius = PointR;
        }
    }

    public enum SegmentType : int
    {
        Empty = 0,
        PolyLine,
        Circle,
        Line,
    }
    public interface LayerInfo
    {

    }
    public class DXFItem
    {
        public string FullName = "";
        public Point2D Max = new Point2D();
        public Point2D Min = new Point2D();
        public List<DXFSegment> _Segment = new List<DXFSegment>();
        public LayerInfo layerInfos;
        public Point2D Avg
        {
            get
            {
                double AvgX = Math.Round((Max.X + Min.X) / 2, 2);
                double AvgY = Math.Round((Max.Y + Min.Y) / 2, 2);

                return new Point2D(AvgX, AvgY);
            }
        }

        public DXFItem()
        {

        }


        public DXFItem(string InputName)
        {
            FullName = InputName;
        }

        public void Add(DXFSegment Input)
        {
            _Segment.Add(Input);
        }
        public List<RsLib.PointCloud.Polyline> Get3DPolylines()
        {
            List<RsLib.PointCloud.Polyline> Output = new List<RsLib.PointCloud.Polyline>();
            for (int i = 0; i < _Segment.Count; i++)
            {
                Output.Add(_Segment[i].Get3DPolyline());
            }
            return Output;
        }

    }
    public class DXFSegment
    {
        public SegmentType _Type = SegmentType.Empty;
        public string _Name = "";
        public bool _IsClose = true;
        public Point2D Max = new Point2D();
        public Point2D Min = new Point2D();
        public List<DXFPoint> _Points = new List<DXFPoint>();


        public DXFSegment()
        {

        }
        public RsLib.PointCloud.Polyline Get3DPolyline()
        {
            RsLib.PointCloud.Polyline Output = new RsLib.PointCloud.Polyline();
            if (_Type == SegmentType.PolyLine)
            {
                for (int i = 0; i < _Points.Count; i++)
                {
                    Point3D tempP = new Point3D(_Points[i].X, _Points[i].Y, 0.0);
                    Output.Add(tempP);
                }
                return Output;
            }
            else if (_Type == SegmentType.Line)
            {
                for (int i = 0; i < _Points.Count; i++)
                {
                    Point3D tempP = new Point3D(_Points[i].X, _Points[i].Y, 0.0);
                    Output.Add(tempP);
                }
                return Output;
            }
            else return null;
        }
        public void Draw(Pen pen, Graphics g)
        {
            switch (_Type)
            {
                case SegmentType.Circle:
                    g.DrawEllipse(pen,
                        (float)(_Points[0].X - _Points[0].Radius), (float)(_Points[0].Y - _Points[0].Radius),
                        (float)(_Points[0].Radius * 2),
                        (float)(_Points[0].Radius * 2));
                    break;
                case SegmentType.PolyLine:
                    for (int i = 0; i < _Points.Count - 1; i++)
                    {
                        int i0 = i;
                        int i1 = i + 1;

                        g.DrawLine(pen,
                            (float)_Points[i].X,
                            (float)_Points[i].Y,
                            (float)_Points[i + 1].X,
                            (float)_Points[i + 1].Y);

                    }

                    break;
                case SegmentType.Line:
                    g.DrawLine(pen,
                        (float)_Points[0].X,
                        (float)_Points[0].Y,
                        (float)_Points[1].X,
                        (float)_Points[1].Y);

                    break;
            }
        }
        public DXFSegment(Circle Input)
        {
            _Type = SegmentType.Circle;
            _Name = Input.Layer.Name;
            //_Color = Input.Color.ToColor();

            DXFPoint tempP = new DXFPoint(Input.Center.X, Input.Center.Y, Input.Radius);
            tempP.IsStartPoint = true;
            _IsClose = true;


            Max = new Point2D(Input.Center.X + Input.Radius, Input.Center.Y + Input.Radius);
            Min = new Point2D(Input.Center.X - Input.Radius, Input.Center.Y - Input.Radius);

            _Points.Add(tempP);
        }
        public DXFSegment(netDxf.Entities.Polyline Input)
        {
            _Type = SegmentType.PolyLine;
            _Name = Input.Layer.Name;
            //_Color = Input.Color.ToColor();
            double MinXValue = double.MaxValue;
            double MinYValue = double.MaxValue;
            double MaxXValue = double.MinValue;
            double MaxYValue = double.MinValue;

            for (int i = 0; i < Input.Vertexes.Count; i++)
            {
                double VertexX = Input.Vertexes[i].Position.X;
                double VertexY = Input.Vertexes[i].Position.Y;
                DXFPoint tempP = new DXFPoint(VertexX, VertexY);

                if (VertexX <= MinXValue) MinXValue = VertexX;
                if (VertexY <= MinYValue) MinYValue = VertexY;
                if (VertexX >= MaxXValue) MaxXValue = VertexX;
                if (VertexY >= MaxYValue) MaxYValue = VertexY;

                if (i == 0) tempP.IsStartPoint = true;
                else tempP.IsStartPoint = false;
                if (i == Input.Vertexes.Count - 1)
                {
                    if (Input.IsClosed)
                    {
                        if (Input.Vertexes[Input.Vertexes.Count - 1].Position.X != Input.Vertexes[0].Position.X &&
                            Input.Vertexes[Input.Vertexes.Count - 1].Position.Y != Input.Vertexes[0].Position.Y)
                        {
                            tempP = new DXFPoint(_Points[0].X, _Points[0].Y);
                            tempP.IsStartPoint = false;

                            _IsClose = true;
                        }
                        else _IsClose = false;
                    }
                }
                _Points.Add(tempP);
            }
            Max = new Point2D(MaxXValue, MaxYValue);
            Min = new Point2D(MinXValue, MinYValue);
        }
        public DXFSegment(LwPolyline Input)
        {
            _Type = SegmentType.PolyLine;
            _Name = Input.Layer.Name;
            //_Color = Input.Color.ToColor();

            double MinXValue = double.MaxValue;
            double MinYValue = double.MaxValue;
            double MaxXValue = double.MinValue;
            double MaxYValue = double.MinValue;

            for (int i = 0; i < Input.Vertexes.Count; i++)
            {
                double VertexX = Input.Vertexes[i].Position.X;
                double VertexY = Input.Vertexes[i].Position.Y;
                DXFPoint tempP = new DXFPoint(VertexX, VertexY);

                if (VertexX <= MinXValue) MinXValue = VertexX;
                if (VertexY <= MinYValue) MinYValue = VertexY;
                if (VertexX >= MaxXValue) MaxXValue = VertexX;
                if (VertexY >= MaxYValue) MaxYValue = VertexY;

                if (i == 0) tempP.IsStartPoint = true;
                else tempP.IsStartPoint = false;
                if (i == Input.Vertexes.Count - 1)
                {
                    if (Input.IsClosed)
                    {
                        if (Input.Vertexes[Input.Vertexes.Count - 1].Position.X != Input.Vertexes[0].Position.X &&
                            Input.Vertexes[Input.Vertexes.Count - 1].Position.Y != Input.Vertexes[0].Position.Y)
                        {
                            tempP = new DXFPoint(_Points[0].X, _Points[0].Y);
                            tempP.IsStartPoint = false;
                            tempP.Radius = 0.0;
                        }
                    }
                }
                _Points.Add(tempP);
            }
            Max = new Point2D(MaxXValue, MaxYValue);
            Min = new Point2D(MinXValue, MinYValue);
        }
    }
}
