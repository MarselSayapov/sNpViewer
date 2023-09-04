using System.IO;
using System.Windows.Forms;
using System;
using System.Drawing;
using OxyPlot;
using OxyPlot.Annotations;
using static sNpViewer.s1pFile;
using static sNpViewer.s2pFile;
using static sNpViewer.s3pFile;

namespace sNpViewer
{
    public partial class PhaseGraphic : Form
    {
        private readonly OxyPlot.WindowsForms.PlotView _phasePlotView;
        public PhaseGraphic(string data)
        {
            this.Icon = new Icon(Path.Combine(Directory.GetCurrentDirectory(), "sNpViewerICON.ico"));
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
            var saveToImagePhase = new Button
            {
                Text = @"Export Phase Image",
                Dock = DockStyle.Fill
            };
            saveToImagePhase.Click += (sender, e) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = @"SVG Image (*.svg)|*.svg";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    using (var stream = File.Create(filePath))
                    {
                        if (_phasePlotView != null)
                        {
                            var exporter = new SvgExporter
                            {
                                Width = _phasePlotView.Width,
                                Height = _phasePlotView.Height
                            };

                            exporter.Export(_phasePlotView.Model, stream);
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

            _phasePlotView = new OxyPlot.WindowsForms.PlotView
            {
                Dock = DockStyle.Fill
            };
            tableLayoutPanel.Controls.Add(new Panel(), 0, 0);
            tableLayoutPanel.Controls.Add(_phasePlotView, 0, 1);
            tableLayoutPanel.Controls.Add(reset, 0, 2);
            tableLayoutPanel.Controls.Add(saveToImagePhase, 0, 3);
            tableLayoutPanel.Dock = DockStyle.Fill;
            Controls.Add(tableLayoutPanel);
            double[] frequencies;
            double[] s11Pha;
            double[] s21Pha;
            double[] s12Pha;
            double[] s22Pha;
            bool match;
            var phaseModel = new PlotModel();
            var model = phaseModel;
            _phasePlotView.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Space)
                {
                    var point = _phasePlotView.PointToClient(Cursor.Position);
                    
                    var annotation = new PointAnnotation
                    {
                        X = _phasePlotView.Model.Axes[0].InverseTransform(point.X),
                        Y = _phasePlotView.Model.Axes[1].InverseTransform(point.Y),
                        Shape = MarkerType.Circle,
                        Fill = OxyColors.Red,
                        StrokeThickness = 1,
                        Stroke = OxyColors.Black,
                        Text = $"Frequency: {_phasePlotView.Model.Axes[0].InverseTransform(point.X):0.00}, Phase: {_phasePlotView.Model.Axes[1].InverseTransform(point.Y):0.00}",
                        TextColor = OxyColors.Black,
                        FontWeight = FontWeights.Bold
                    };
                    
                    model.Annotations.Add(annotation);
                    
                    model.InvalidatePlot(true);
                }
            };
            if (lines == 8)
            {
                LoadS2PData(data, out frequencies, out _, out _, out _, out _, out s11Pha, out s21Pha, out s12Pha, out s22Pha, out match, out _);

                phaseModel = CreatePhasePlotModel(frequencies, s11Pha, s21Pha, s12Pha, s22Pha, match);
                _phasePlotView.Model = phaseModel;
            }
            if (lines == 2)
            {
                LoadS1PData(data, out frequencies, out _, out s11Pha, out match, out _);
                phaseModel = CreatePhasePlotModel(frequencies, s11Pha, match);
                _phasePlotView.Model = phaseModel;
            }
            if (lines == 18)
            {
                LoadS3PData(data, out frequencies, out _, out _, out _, out _, out _, out _, out _, out _, out _,
                    out s11Pha, out s21Pha, out s12Pha, out s22Pha, out var s13Pha, out var s23Pha, out var s31Pha, out var s32Pha, out var s33Pha, out match, out _);
                phaseModel = CreatePhasePlotModel(frequencies, s11Pha, s21Pha, s12Pha, s22Pha, s13Pha, s23Pha, s31Pha, s32Pha, s33Pha, match);
                _phasePlotView.Model = phaseModel;
            }
            reset.Click += (sender, args) =>
            {
                tableLayoutPanel.Controls.Remove(_phasePlotView);
                _phasePlotView.Model.ResetAllAxes();
                tableLayoutPanel.Controls.Add(_phasePlotView, 0, 1);
            };
        }

    }
}