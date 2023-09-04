using System.IO;
using System.Windows.Forms;
using System;
using System.Drawing;
using OxyPlot;
using OxyPlot.Annotations;

namespace sNpViewer
{
    public partial class RealPartGraphic : Form
    {
        private OxyPlot.WindowsForms.PlotView _realPartPlotViev;
        public RealPartGraphic(string data)
        {
            var lines = 0;
            using (StreamReader reader = new StreamReader(data))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {

                    if (line.TrimStart().StartsWith("!") || line.TrimStart().StartsWith("#"))
                    {
                        continue;
                    }
                    var str = line.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    lines = str.Length - 1;
                }
            }
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                Location = new System.Drawing.Point(12, 12),
                Size = new System.Drawing.Size(800, 400),
                ColumnCount = 2,
                RowCount = 1
            };
            var saveToImageRealPart = new Button
            {
                Text = "Export Real Part Image",
                Dock = DockStyle.Fill
            };
            saveToImageRealPart.Click += (sender, e) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "SVG Image (*.svg)|*.svg";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    using (var stream = File.Create(filePath))
                    {
                        if (_realPartPlotViev != null)
                        {
                            var exporter = new SvgExporter
                            {
                                Width = _realPartPlotViev.Width,
                                Height = _realPartPlotViev.Height
                            };

                            exporter.Export(_realPartPlotViev.Model, stream);
                        }
                    }
                }

            };
            var reset = new Button()
            {
                Text = @"Default Zoom",
                Dock = DockStyle.Fill
            };
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 1));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            _realPartPlotViev = new OxyPlot.WindowsForms.PlotView
            {
                Dock = DockStyle.Fill
            };
            tableLayoutPanel.Controls.Add(new Panel(), 0, 0);
            tableLayoutPanel.Controls.Add(_realPartPlotViev, 0, 1);
            tableLayoutPanel.Controls.Add(reset, 0, 2);
            tableLayoutPanel.Controls.Add(saveToImageRealPart, 0, 3);
            tableLayoutPanel.Dock = DockStyle.Fill;
            Controls.Add(tableLayoutPanel);
            double[] frequencies;
            double[] s11Mag;
            double[] s21Mag;
            double[] s12Mag;
            double[] s22Mag;
            double[] s11Pha;
            double[] s21Pha;
            double[] s12Pha;
            double[] s22Pha;
            bool match;
            var phaseModel = new PlotModel();
            var model = phaseModel;
            _realPartPlotViev.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Space)
                {
                    var point = _realPartPlotViev.PointToClient(Cursor.Position);

                    var annotation = new PointAnnotation
                    {
                        X = _realPartPlotViev.Model.Axes[0].InverseTransform(point.X),
                        Y = _realPartPlotViev.Model.Axes[1].InverseTransform(point.Y),
                        Shape = MarkerType.Circle,
                        Fill = OxyColors.Red,
                        StrokeThickness = 1,
                        Stroke = OxyColors.Black,
                        Text = $"Frequency: {_realPartPlotViev.Model.Axes[0].InverseTransform(point.X):0.00}, Real Part: {_realPartPlotViev.Model.Axes[1].InverseTransform(point.Y):0.00}",
                        TextColor = OxyColors.Black,
                        FontWeight = FontWeights.Bold
                    };

                    model.Annotations.Add(annotation);

                    model.InvalidatePlot(true);
                }
            };

            if (lines == 8)
            {
                s2pFile.LoadS2PData(data, out frequencies, out s11Mag, out s21Mag, out s12Mag, out s22Mag, out s11Pha, out s21Pha, out s12Pha, out s22Pha, out match, out _);

                phaseModel = s2pFile.CreateRealPartPlotModel(frequencies, s11Mag, s21Mag, s12Mag, s22Mag, s11Pha, s21Pha, s12Pha, s22Pha, match);
                _realPartPlotViev.Model = phaseModel;
            }
            if (lines == 2)
            {
                s1pFile.LoadS1PData(data, out frequencies, out s11Mag, out s11Pha, out match, out _);
                phaseModel = s1pFile.CreateRealPartPlotModel(frequencies, s11Mag, s11Pha, match);
                _realPartPlotViev.Model = phaseModel;
            }
            if (lines == 18)
            {
                s3pFile.LoadS3PData(data, out frequencies, out s11Mag, out s21Mag, out s12Mag, out s22Mag, out var s13Mag, out var s23Mag, out var s31Mag, out var s32Mag, out var s33Mag,
                    out s11Pha, out s21Pha, out s12Pha, out s22Pha, out var s13Pha, out var s23Pha, out var s31Pha, out var s32Pha, out var s33Pha, out match, out _);
                phaseModel = s3pFile.CreateRealPartPlotModel(frequencies, s11Mag, s21Mag, s12Mag, s22Mag, s13Mag, s23Mag, s33Mag,
                    s31Mag, s32Mag, s11Pha, s12Pha, s21Pha, s22Pha, s13Pha, s23Pha, s33Pha, s31Pha, s32Pha, match);

                _realPartPlotViev.Model = phaseModel;
            }
            reset.Click += (sender, args) =>
            {
                tableLayoutPanel.Controls.Remove(_realPartPlotViev);
                _realPartPlotViev.Model.ResetAllAxes();
                tableLayoutPanel.Controls.Add(_realPartPlotViev, 0, 1);
            };
        }

    }
}