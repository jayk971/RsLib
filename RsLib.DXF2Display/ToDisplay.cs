
using RsLib.Display;
using RsLib.DXF;
namespace RsLib.DXF2Display
{
    public class ToDisplay
    {
        public static FtCompositeShape Convert(DXFItem dXF)
        {
            FtCompositeShape fcp = new FtCompositeShape();
            for (int i = 0; i < dXF._Segment.Count; i++)
            {
                if (dXF._Segment[i]._Type == SegmentType.Circle)
                {
                    FtCircle fc = new FtCircle(dXF._Segment[i]._Points[0].X, dXF._Segment[i]._Points[0].Y, dXF._Segment[i]._Points[0].Radius);
                    fcp.Add(fc);
                }
                else if (dXF._Segment[i]._Type == SegmentType.Line)
                {
                    FtPolyline fp = new FtPolyline();
                    fp.Add(dXF._Segment[i]._Points[0].X, dXF._Segment[i]._Points[0].Y);
                    fp.Add(dXF._Segment[i]._Points[1].X, dXF._Segment[i]._Points[1].Y);
                    fcp.Add(fp);
                }
                else if (dXF._Segment[i]._Type == SegmentType.PolyLine)
                {
                    FtPolygon fp = new FtPolygon();
                    //FtPolyline fp = new FtPolyline();
                    for (int j = 0; j < dXF._Segment[i]._Points.Count; j++)
                    {
                        fp.Add(dXF._Segment[i]._Points[j].X, dXF._Segment[i]._Points[j].Y);
                    }
                    fcp.Add(fp);

                }
            }
            return fcp;
        }
        public static FtCompositeShape Convert(DXFItem dXF, double ShiftX, double ShiftY)
        {
            FtCompositeShape fcp = new FtCompositeShape();
            for (int i = 0; i < dXF._Segment.Count; i++)
            {
                if (dXF._Segment[i]._Type == SegmentType.Circle)
                {
                    FtCircle fc = new FtCircle(dXF._Segment[i]._Points[0].X + ShiftX, dXF._Segment[i]._Points[0].Y + ShiftY, dXF._Segment[i]._Points[0].Radius);
                    fcp.Add(fc);
                }
                else if (dXF._Segment[i]._Type == SegmentType.Line)
                {
                    FtPolyline fp = new FtPolyline();
                    fp.Add(dXF._Segment[i]._Points[0].X + ShiftX, dXF._Segment[i]._Points[0].Y + ShiftY);
                    fp.Add(dXF._Segment[i]._Points[1].X + ShiftX, dXF._Segment[i]._Points[1].Y + ShiftY);
                    fcp.Add(fp);
                }
                else if (dXF._Segment[i]._Type == SegmentType.PolyLine)
                {
                    FtPolygon fp = new FtPolygon();
                    //FtPolyline fp = new FtPolyline();
                    for (int j = 0; j < dXF._Segment[i]._Points.Count; j++)
                    {
                        fp.Add(dXF._Segment[i]._Points[j].X + ShiftX, dXF._Segment[i]._Points[j].Y + ShiftY);
                    }
                    fcp.Add(fp);

                }
            }
            return fcp;
        }

    }
}
