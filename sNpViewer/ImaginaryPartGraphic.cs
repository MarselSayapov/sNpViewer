using System.IO;
using System.Windows.Forms;
using System;
using System.Drawing;
using OxyPlot;
using OxyPlot.Annotations;
using static sNpViewer.s2pFile;

namespace sNpViewer
{
    public partial class ImaginaryPartGraphic : Form
    {
        private OxyPlot.WindowsForms.PlotView _imaginaryPartPlotViev;
        public ImaginaryPartGraphic(string data)
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
                Text = @"Export imaginary part image",
                Dock = DockStyle.Fill
            };
            saveToImageRealPart.Click += (sender, e) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = @"SVG Image (*.svg)|*.svg";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    using (var stream = File.Create(filePath))
                    {
                        if (_imaginaryPartPlotViev != null)
                        {
                            var exporter = new SvgExporter
                            {
                                Width = _imaginaryPartPlotViev.Width,
                                Height = _imaginaryPartPlotViev.Height
                            };

                            exporter.Export(_imaginaryPartPlotViev.Model, stream);
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

            _imaginaryPartPlotViev = new OxyPlot.WindowsForms.PlotView
            {
                Dock = DockStyle.Fill
            };
            tableLayoutPanel.Controls.Add(new Panel(), 0, 0);
            tableLayoutPanel.Controls.Add(_imaginaryPartPlotViev, 0, 1);
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
            _imaginaryPartPlotViev.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Space)
                {
                    var point = _imaginaryPartPlotViev.PointToClient(Cursor.Position);
                    
                    var annotation = new PointAnnotation
                    {
                        X = _imaginaryPartPlotViev.Model.Axes[0].InverseTransform(point.X),
                        Y = _imaginaryPartPlotViev.Model.Axes[1].InverseTransform(point.Y),
                        Shape = MarkerType.Circle,
                        Fill = OxyColors.Red,
                        StrokeThickness = 1,
                        Stroke = OxyColors.Black,
                        Text = $"Frequency: {_imaginaryPartPlotViev.Model.Axes[0].InverseTransform(point.X):0.00}, Imaginary Part: {_imaginaryPartPlotViev.Model.Axes[1].InverseTransform(point.Y):0.00}",
                        TextColor = OxyColors.Black,
                        FontWeight = FontWeights.Bold
                    };
                    model.Annotations.Add(annotation);
                    
                    model.InvalidatePlot(true);
                }
            };
            if (lines == 8)
            {
                LoadS2PData(data, out frequencies, out s11Mag, out s21Mag, out s12Mag, out s22Mag, out s11Pha, out s21Pha, out s12Pha, out s22Pha, out match, out _);

                phaseModel = CreateImaginaryPartPlotModel(frequencies, s11Mag, s21Mag, s12Mag, s22Mag, s11Pha, s21Pha, s12Pha, s22Pha, match);

                _imaginaryPartPlotViev.Model = phaseModel;
            }
            if (lines == 2)
            {

                s1pFile.LoadS1PData(data, out frequencies, out s11Mag, out s11Pha, out match, out _);

                phaseModel = s1pFile.CreateImaginaryPartPlotModel(frequencies, s11Mag, s11Pha, match);

                _imaginaryPartPlotViev.Model = phaseModel;
            }
            if (lines == 18)
            {

                s3pFile.LoadS3PData(data, out frequencies, out s11Mag, out s21Mag, out s12Mag, out s22Mag, out var s13Mag, out var s23Mag, out var s31Mag, out var s32Mag, out var s33Mag,
                    out s11Pha, out s21Pha, out s12Pha, out s22Pha, out var s13Pha, out var s23Pha, out var s31Pha, out var s32Pha, out var s33Pha, out match, out _);


                phaseModel = s3pFile.CreateImaginaryPartPlotModel(frequencies, s11Mag, s21Mag, s12Mag, s22Mag, s13Mag, s23Mag, s33Mag, s31Mag, s32Mag, s11Pha, s12Pha, s21Pha, s22Pha,
                    s13Pha, s23Pha, s33Pha, s31Pha, s32Pha, match);

                _imaginaryPartPlotViev.Model = phaseModel;
            }

            void OnResetOnClick()
            {
                tableLayoutPanel.Controls.Remove(_imaginaryPartPlotViev);
                _imaginaryPartPlotViev.Model.ResetAllAxes();
                tableLayoutPanel.Controls.Add(_imaginaryPartPlotViev, 0, 1);
            }

            reset.Click += (o, args) => OnResetOnClick();
        }
    }
}