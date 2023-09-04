using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Annotations;

namespace sNpViewer
{
    public partial class MagnitudeGraphic : Form
    {
        private readonly OxyPlot.WindowsForms.PlotView _magnitudePlotView;

        public MagnitudeGraphic(string data)
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
            var saveToImageMagnitude = new Button
            {
                Text = @"Export magnitude image",
                Dock = DockStyle.Fill
            };
            var calculateIndB = new Button
            {
                Text = @"Calculate in dB",
                Dock = DockStyle.Fill
            };
            var calculateInLinear = new Button
            {
                Text = @"Calculate in Linear",
                Dock = DockStyle.Fill
            };

            saveToImageMagnitude.Click += (sender, e) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = @"SVG Image (*.svg)|*.svg";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    using (var stream = File.Create(filePath))
                    {
                        if (_magnitudePlotView != null)
                        {
                            var exporter = new SvgExporter
                            {
                                Width = _magnitudePlotView.Width,
                                Height = _magnitudePlotView.Height
                            };

                            exporter.Export(_magnitudePlotView.Model, stream);
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
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            _magnitudePlotView = new OxyPlot.WindowsForms.PlotView
            {
                Dock = DockStyle.Fill
            };
            tableLayoutPanel.Controls.Add(new Panel(), 0, 0);
            tableLayoutPanel.Controls.Add(_magnitudePlotView, 0, 1);
            tableLayoutPanel.Controls.Add(calculateIndB, 0, 2);
            tableLayoutPanel.Controls.Add(reset, 0, 5);
            tableLayoutPanel.Controls.Add(saveToImageMagnitude, 0, 4);
            tableLayoutPanel.Controls.Add(calculateInLinear, 0, 3);
            tableLayoutPanel.Dock = DockStyle.Fill;
            Controls.Add(tableLayoutPanel);
            double[] frequencies;
            double[] s11Mag;
            double[] s21Mag;
            double[] s12Mag;
            double[] s22Mag;
            bool match;
            bool db;
            var magnitudeModel = new PlotModel();
            var model = magnitudeModel;
            _magnitudePlotView.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Space)
                {

                    var point = _magnitudePlotView.PointToClient(Cursor.Position);

                    var annotation = new PointAnnotation
                    {
                        X = _magnitudePlotView.Model.Axes[0].InverseTransform(point.X),
                        Y = _magnitudePlotView.Model.Axes[1].InverseTransform(point.Y),
                        Shape = MarkerType.Circle,
                        Fill = OxyColors.Red,
                        StrokeThickness = 1,
                        Stroke = OxyColors.Black,
                        Text =
                            $"Frequency: {_magnitudePlotView.Model.Axes[0].InverseTransform(point.X):0.00}, Magnitude: {_magnitudePlotView.Model.Axes[1].InverseTransform(point.Y):0.00}"
                    };

                    model.Annotations.Add(annotation);
                    model.InvalidatePlot(true);
                }
            };
            if (lines == 8)
            {
                s2pFile.LoadS2PData(data, out frequencies, out s11Mag, out s21Mag, out s12Mag, out s22Mag, out _,
                    out _, out _, out _, out match, out db);
                
                magnitudeModel =
                    s2pFile.CreateMagnitudePlotModel(frequencies, s11Mag, s21Mag, s12Mag, s22Mag, match, db);
                _magnitudePlotView.Model = magnitudeModel;
                calculateIndB.Click += (sender, e) =>
                {
                    if (lines == 8)
                    {
                        tableLayoutPanel.Controls.Remove(_magnitudePlotView);
                        magnitudeModel =
                            s2pFile.CreateMagnitudePlotModelIndB(frequencies, s11Mag, s21Mag, s12Mag, s22Mag, match,
                                db);
                        if (magnitudeModel != null) _magnitudePlotView.Model = magnitudeModel;
                        tableLayoutPanel.Controls.Add(_magnitudePlotView, 0, 1);
                    }
                };
                calculateInLinear.Click += (sender, e) =>
                {
                    if (lines == 8)
                    {
                        tableLayoutPanel.Controls.Remove(_magnitudePlotView);
                        magnitudeModel =
                            s2pFile.CreateMagnitudePlotModel(frequencies, s11Mag, s21Mag, s12Mag, s22Mag, match, db);
                        _magnitudePlotView.Model = magnitudeModel;
                        tableLayoutPanel.Controls.Add(_magnitudePlotView, 0, 1);
                    }
                };

            }

            if (lines == 2)
            {
                s1pFile.LoadS1PData(data, out frequencies, out s11Mag, out _, out match, out db);
                magnitudeModel = s1pFile.CreateMagnitudePlotModel(frequencies, s11Mag, match, db);
                calculateIndB.Click += (sender, e) =>
                {
                    if (lines == 2)
                    {
                        tableLayoutPanel.Controls.Remove(_magnitudePlotView);
                        magnitudeModel = s1pFile.CreateMagnitudePlotModelIndB(frequencies, s11Mag, match, db);
                        _magnitudePlotView.Model = magnitudeModel;
                        tableLayoutPanel.Controls.Add(_magnitudePlotView, 0, 1);
                    }
                };
                calculateInLinear.Click += (sender, e) =>
                {
                    if (lines == 2)
                    {
                        tableLayoutPanel.Controls.Remove(_magnitudePlotView);
                        magnitudeModel = s1pFile.CreateMagnitudePlotModel(frequencies, s11Mag, match, db);
                        _magnitudePlotView.Model = magnitudeModel;
                        tableLayoutPanel.Controls.Add(_magnitudePlotView, 0, 1);
                    }
                };
                _magnitudePlotView.Model = magnitudeModel;
            }

            if (lines == 18)
            {
                s3pFile.LoadS3PData(data, out frequencies, out s11Mag, out s21Mag, out s12Mag, out s22Mag, out var s13Mag,
                    out var s23Mag, out var s31Mag, out var s32Mag, out var s33Mag,
                    out _, out _, out _, out _, out _, out _, out _, out _,
                    out _, out match, out db);
                
                magnitudeModel = s3pFile.CreateMagnitudePlotModel(frequencies, s11Mag, s21Mag, s12Mag, s22Mag, s13Mag,
                    s23Mag, s31Mag, s32Mag, s33Mag, match, db);
                calculateIndB.Click += (sender, e) =>
                {
                    if (lines == 18)
                    {
                        tableLayoutPanel.Controls.Remove(_magnitudePlotView);
                        magnitudeModel = s3pFile.CreateMagnitudePlotModelIndB(frequencies, s11Mag, s21Mag, s12Mag,
                            s22Mag, s13Mag, s23Mag, s31Mag, s32Mag, s33Mag, match, db);
                        _magnitudePlotView.Model = magnitudeModel;
                        tableLayoutPanel.Controls.Add(_magnitudePlotView, 0, 1);
                    }
                };
                calculateInLinear.Click += (sender, e) =>
                {
                    if (lines == 18)
                    {
                        tableLayoutPanel.Controls.Remove(_magnitudePlotView);
                        magnitudeModel = s3pFile.CreateMagnitudePlotModel(frequencies, s11Mag, s21Mag, s12Mag, s22Mag,
                            s13Mag, s23Mag, s31Mag, s32Mag, s33Mag, match, db);
                        _magnitudePlotView.Model = magnitudeModel;
                        tableLayoutPanel.Controls.Add(_magnitudePlotView, 0, 1);
                    }
                };

                _magnitudePlotView.Model = magnitudeModel;
            }

            reset.Click += (sender, args) =>
            {
                tableLayoutPanel.Controls.Remove(_magnitudePlotView);
                _magnitudePlotView.Model.ResetAllAxes();
                tableLayoutPanel.Controls.Add(_magnitudePlotView, 0, 1);
            };
        }
    }
}