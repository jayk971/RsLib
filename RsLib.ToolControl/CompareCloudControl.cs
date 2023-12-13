using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Legends;
using OxyPlot.WindowsForms;
using OxyPlot.Utilities;
using RsLib.PointCloudLib;
namespace RsLib.ToolControl
{
    public partial class CompareCloudControl : UserControl
    {
        PlotView plotView = new PlotView();

        public CompareCloudControl()
        {
            InitializeComponent();
            plotView.Dock = DockStyle.Fill;
            pnl_ComparePlot.Controls.Add(plotView);

        }

        public void SetCompareResult(CompareCloudOption option)
        {
            if (InvokeRequired)
            {
                Action<CompareCloudOption> action = new Action<CompareCloudOption>(SetCompareResult);
                Invoke(action,option);
            }
            else
            {
                plotView.Model = CreateBarSeries(option);
                plotView.Controller = GetPlotController();
                lbl_Similarity.Text = option.Similarity.ToString();
                groupBox1.Text = $"Score ({option.AcceptLimitMin:F2}~{option.AcceptLimitMax:F2})";
            }
        }
        PlotController GetPlotController()
        {
            var controller = new PlotController();

            // add a tracker command to the mouse enter event
            controller.BindMouseEnter(PlotCommands.HoverPointsOnlyTrack);
            return controller;

        }
        BarSeries createBarSeries(string title, Color c, bool isStacked, string stackedGroup)
        {
            OxyColor color = OxyColor.FromArgb(c.A, c.R, c.G, c.B);
            if (isStacked)
            {
                return new BarSeries
                {
                    LabelFormatString = "{0}",
                    IsStacked = true,
                    StackGroup = stackedGroup,
                    LabelPlacement = LabelPlacement.Outside,
                    TrackerFormatString = "{1}\n{0} : {2} ",
                    Title = title,
                    FillColor = color,
                };
            }
            else
            {
                return new BarSeries
                {
                    LabelFormatString = "{0}",
                    LabelPlacement = LabelPlacement.Outside,
                    TrackerFormatString = "{2} ",

                    Title = title,
                    FillColor = color
                };
            }
        }
        public PlotModel CreateBarSeries(CompareCloudOption option)
        {
            var model = new PlotModel
            {
                Background = OxyColor.FromRgb(255, 255, 255),
            };
            //var l = new Legend
            //{
            //    LegendPlacement = LegendPlacement.Outside,
            //    LegendPosition = LegendPosition.BottomCenter,
            //    LegendOrientation = LegendOrientation.Horizontal,
            //    LegendBorderThickness = 0
            //};
            //model.Legends.Add(l);

            BarSeries _0_20 = createBarSeries(option.Base20.ToString(), Color.Blue, false, "20");
            BarSeries _20_40 = createBarSeries(option.Base40.ToString(), Color.Cyan, false, "40");
            BarSeries _40_60 = createBarSeries(option.Base60.ToString(), Color.LightGreen, false, "60");
            BarSeries _60_80 = createBarSeries(option.Base80.ToString(), Color.Gold, false, "80");
            BarSeries _80_100 = createBarSeries(option.Base100.ToString(), Color.Red, false, "100");

            _0_20.Items.Add(new BarItem() { Value = option.Ratio_20, CategoryIndex = 0 });
            _20_40.Items.Add(new BarItem() { Value = option.Ratio_40, CategoryIndex = 1 });
            _40_60.Items.Add(new BarItem() { Value = option.Ratio_60, CategoryIndex = 2 });
            _60_80.Items.Add(new BarItem() { Value = option.Ratio_80, CategoryIndex = 3 });
            _80_100.Items.Add(new BarItem() { Value = option.Ratio_100, CategoryIndex = 4 });
            var categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Title = "Deviation (mm)",
            };

            categoryAxis.Labels.Add($"<{option.Base20:F1}");
            categoryAxis.Labels.Add($"{option.Base20:F1}-{option.Base40:F1}");
            categoryAxis.Labels.Add($"{option.Base40:F1}-{option.Base60:F1}");
            categoryAxis.Labels.Add($"{option.Base60:F1}-{option.Base80:F1}");
            categoryAxis.Labels.Add($">{option.Base80:F1}");

            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = 100,
                MinimumPadding = 0,
                MaximumPadding = 0.1,
                ExtraGridlines = new[] { 0d },
                //Title = "Ratio"
            };

            model.Series.Add(_0_20);
            model.Series.Add(_20_40);
            model.Series.Add(_40_60);
            model.Series.Add(_60_80);
            model.Series.Add(_80_100);
            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);
            //model.Transpose();
            return model;
        }

    }
}
