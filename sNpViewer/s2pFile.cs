using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;

namespace sNpViewer
{
    internal class s2pFile
    {
        private const double HzToGHz = 1e-9;
        public static PlotModel CreateMagnitudePlotModel(double[] frequencies, double[] s11Mag, double[] s21Mag, double[] s12Mag, double[] s22Mag, bool match, bool db)
        {
            var model = new PlotModel
            {
                Title = "Frequency S-Parameter"
            };
            var legendForMagnitude = new Legend
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorderThickness = 0
            };
            model.Legends.Add(legendForMagnitude);
            var frequencyAxis = new LinearAxis { Title = "Frequency (GHz)", Position = AxisPosition.Bottom };
            var magnitudeAxis = new LinearAxis { Title = "Magnitude of S-Parameter", Position = AxisPosition.Left };
            frequencyAxis.MajorGridlineStyle = LineStyle.Solid;
            frequencyAxis.MinorGridlineStyle = LineStyle.Dot;
            magnitudeAxis.MajorGridlineStyle = LineStyle.Solid;
            magnitudeAxis.MinorGridlineStyle = LineStyle.Dot;
            model.Axes.Add(frequencyAxis);
            model.Axes.Add(magnitudeAxis);

            var s11MagSeries = new LineSeries { Title = "|S11|" };
            var s21MagSeries = new LineSeries { Title = "|S21|" };
            var s12MagSeries = new LineSeries { Title = "|S12|" };
            var s22MagSeries = new LineSeries { Title = "|S22|" };
            if (!match)
            {
                if (db)
                {
                    var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        s11MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(Math.Pow(10, s11Mag[i] / 20))));
                        s21MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(Math.Pow(10, s21Mag[i] / 20))));
                        s12MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(Math.Pow(10, s12Mag[i] / 20))));
                        s22MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(Math.Pow(10, s22Mag[i] / 20))));
                    }
                }
             
                else
                {
                    var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        s11MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(s11Mag[i])));
                        s21MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(s21Mag[i])));
                        s12MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(s12Mag[i])));
                        s22MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(s22Mag[i])));
                    }
                }
            }
            else
            {
                if (db)
                {
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        s11MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(Math.Pow(10, s11Mag[i] / 20))));
                        s21MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(Math.Pow(10, s21Mag[i] / 20))));
                        s12MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(Math.Pow(10, s12Mag[i] / 20))));
                        s22MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(Math.Pow(10, s22Mag[i] / 20))));
                    }
                }
                else
                {
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        s11MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(s11Mag[i])));
                        s21MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(s21Mag[i])));
                        s12MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(s12Mag[i])));
                        s22MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(s22Mag[i])));
                    }
                }

            }
            model.Series.Add(s11MagSeries);
            model.Series.Add(s21MagSeries);
            model.Series.Add(s12MagSeries);
            model.Series.Add(s22MagSeries);

            return model;
        }

        public static PlotModel CreateMagnitudePlotModelIndB(double[] frequencies, double[] s11Mag, double[] s21Mag, double[] s12Mag, double[] s22Mag, bool match, bool db)
        {
            var model = new PlotModel
            {
                Title = "Magnitude of S-Parameter"
            };
            var legendForMagnitude = new Legend
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorderThickness = 0
            };
            model.Legends.Add(legendForMagnitude);
            var frequencyAxis = new LinearAxis { Title = "Frequency (GHz)", Position = AxisPosition.Bottom };
            var magnitudeAxis = new LinearAxis { Title = "dB", Position = AxisPosition.Left };
            frequencyAxis.MajorGridlineStyle = LineStyle.Solid;
            frequencyAxis.MinorGridlineStyle = LineStyle.Dot;
            magnitudeAxis.MajorGridlineStyle = LineStyle.Solid;
            magnitudeAxis.MinorGridlineStyle = LineStyle.Dot;
            model.Axes.Add(frequencyAxis);
            model.Axes.Add(magnitudeAxis);

            var s11MagSeries = new LineSeries { Title = "S11" };
            var s21MagSeries = new LineSeries { Title = "S21" };
            var s12MagSeries = new LineSeries { Title = "S12" };
            var s22MagSeries = new LineSeries { Title = "S22" };
            if (!match)
            {
                if (db)
                {
                    var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        s11MagSeries.Points.Add(new DataPoint(gHzfreq[i], s11Mag[i]));
                        s21MagSeries.Points.Add(new DataPoint(gHzfreq[i], s21Mag[i]));
                        s12MagSeries.Points.Add(new DataPoint(gHzfreq[i], s12Mag[i]));
                        s22MagSeries.Points.Add(new DataPoint(gHzfreq[i], s22Mag[i]));
                    }
                }
                else
                {
                    var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        s11MagSeries.Points.Add(new DataPoint(gHzfreq[i], 20 * Math.Log10(s11Mag[i])));
                        s21MagSeries.Points.Add(new DataPoint(gHzfreq[i], 20 * Math.Log10(s21Mag[i])));
                        s12MagSeries.Points.Add(new DataPoint(gHzfreq[i], 20 * Math.Log10(s12Mag[i])));
                        s22MagSeries.Points.Add(new DataPoint(gHzfreq[i], 20 * Math.Log10(s22Mag[i])));
                    }
                }
            }
            else
            {
                if (db)
                {
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        s11MagSeries.Points.Add(new DataPoint(frequencies[i], s11Mag[i]));
                        s21MagSeries.Points.Add(new DataPoint(frequencies[i], s21Mag[i]));
                        s12MagSeries.Points.Add(new DataPoint(frequencies[i], s12Mag[i]));
                        s22MagSeries.Points.Add(new DataPoint(frequencies[i], s22Mag[i]));
                    }
                }
                else
                {
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        s11MagSeries.Points.Add(new DataPoint(frequencies[i], 20 * Math.Log10(s11Mag[i])));
                        s21MagSeries.Points.Add(new DataPoint(frequencies[i], 20 * Math.Log10(s21Mag[i])));
                        s12MagSeries.Points.Add(new DataPoint(frequencies[i], 20 * Math.Log10(s12Mag[i])));
                        s22MagSeries.Points.Add(new DataPoint(frequencies[i], 20 * Math.Log10(s22Mag[i])));
                    }
                }
            }
            model.Series.Add(s11MagSeries);
            model.Series.Add(s21MagSeries);
            model.Series.Add(s12MagSeries);
            model.Series.Add(s22MagSeries);
            
            model.PlotType = PlotType.XY;
            return model;
        }
        public static PlotModel CreatePhasePlotModel(double[] frequencies, double[] s11Pha, double[] s21Pha, double[] s12Pha, double[] s22Pha, bool match)
        {
            var legendForPhase = new Legend
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorderThickness = 0
            };
            var model = new PlotModel
            {
                Title = "Phase of S-Parameters"
            };
            model.Legends.Add(legendForPhase);
            var frequencyAxis = new LinearAxis { Title = "Frequency (GHz)", Position = AxisPosition.Bottom };
            var phaseAxis = new LinearAxis { Title = "Phase S-Parameter", Position = AxisPosition.Left };
            frequencyAxis.MajorGridlineStyle = LineStyle.Solid;
            frequencyAxis.MinorGridlineStyle = LineStyle.Dot;
            phaseAxis.MajorGridlineStyle = LineStyle.Solid;
            phaseAxis.MinorGridlineStyle = LineStyle.Dot;
            model.Axes.Add(frequencyAxis);
            model.Axes.Add(phaseAxis);

            var s11PhaSeries = new LineSeries { Title = "S11" };
            var s21PhaSeries = new LineSeries { Title = "S21" };
            var s12PhaSeries = new LineSeries { Title = "S12" };
            var s22PhaSeries = new LineSeries { Title = "S22" };
            if (!match)
            {
                var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s11Pha[i]));
                    s21PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s21Pha[i]));
                    s12PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s12Pha[i]));
                    s22PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s22Pha[i]));
                }
            }
            else
            {
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11PhaSeries.Points.Add(new DataPoint(frequencies[i], s11Pha[i]));
                    s21PhaSeries.Points.Add(new DataPoint(frequencies[i], s21Pha[i]));
                    s12PhaSeries.Points.Add(new DataPoint(frequencies[i], s12Pha[i]));
                    s22PhaSeries.Points.Add(new DataPoint(frequencies[i], s22Pha[i]));
                }
            }

            model.Series.Add(s11PhaSeries);
            model.Series.Add(s21PhaSeries);
            model.Series.Add(s12PhaSeries);
            model.Series.Add(s22PhaSeries);

            return model;
        }
        public static PlotModel CreateRealPartPlotModel(double[] frequencies, double[] s11Mag, double[] s21Mag, double[] s12Mag, double[] s22Mag, double[] s11Pha, double[] s21Pha,
            double[] s12Pha, double[] s22Pha, bool match)
        {

            double[] s11Real = new double[s11Mag.Length];
            double[] s21Real = new double[s21Mag.Length];
            double[] s12Real = new double[s12Mag.Length];
            double[] s22Real = new double[s22Mag.Length];
            for (int i = 0; i < s11Real.Length; i++)
            {
                s11Real[i] = new Complex(s11Mag[i] * Math.Cos(s11Pha[i] * Math.PI / 180), s11Mag[i] * Math.Sin(s11Pha[i] * Math.PI / 180)).Real;
                s21Real[i] = new Complex(s21Mag[i] * Math.Cos(s21Pha[i] * Math.PI / 180), s21Mag[i] * Math.Sin(s21Pha[i] * Math.PI / 180)).Real;
                s12Real[i] = new Complex(s12Mag[i] * Math.Cos(s12Pha[i] * Math.PI / 180), s12Mag[i] * Math.Sin(s12Pha[i] * Math.PI / 180)).Real;
                s22Real[i] = new Complex(s22Mag[i] * Math.Cos(s22Pha[i] * Math.PI / 180), s22Mag[i] * Math.Sin(s22Pha[i] * Math.PI / 180)).Real;
            }
            var model = new PlotModel
            {
                Title = "Real Part of S-Parameters"
            };
            var legendForRealPart = new Legend
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorderThickness = 0
            };
            model.Legends.Add(legendForRealPart);
            var frequencyAxis = new LinearAxis { Title = "Frequency (GHz)", Position = AxisPosition.Bottom };
            var realPartAxis = new LinearAxis { Title = "Real Part", Position = AxisPosition.Left };
            frequencyAxis.MajorGridlineStyle = LineStyle.Solid;
            frequencyAxis.MinorGridlineStyle = LineStyle.Dot;
            realPartAxis.MajorGridlineStyle = LineStyle.Solid;
            realPartAxis.MinorGridlineStyle = LineStyle.Dot;
            model.Axes.Add(frequencyAxis);
            model.Axes.Add(realPartAxis);

            var s11RealSeries = new LineSeries { Title = "Real Part of S11" };
            var s21RealSeries = new LineSeries { Title = "Real Part of S21" };
            var s12RealSeries = new LineSeries { Title = "Real Part of S12" };
            var s22RealSeries = new LineSeries { Title = "Real Part of S22" };
            if (!match)
            {
                var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11RealSeries.Points.Add(new DataPoint(gHzfreq[i], s11Real[i]));
                    s21RealSeries.Points.Add(new DataPoint(gHzfreq[i], s21Real[i]));
                    s12RealSeries.Points.Add(new DataPoint(gHzfreq[i], s12Real[i]));
                    s22RealSeries.Points.Add(new DataPoint(gHzfreq[i], s22Real[i]));
                }
            }
            else
            {
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11RealSeries.Points.Add(new DataPoint(frequencies[i], s11Real[i]));
                    s21RealSeries.Points.Add(new DataPoint(frequencies[i], s21Real[i]));
                    s12RealSeries.Points.Add(new DataPoint(frequencies[i], s12Real[i]));
                    s22RealSeries.Points.Add(new DataPoint(frequencies[i], s22Real[i]));
                }
            }
            model.Series.Add(s11RealSeries);
            model.Series.Add(s12RealSeries);
            model.Series.Add(s21RealSeries);
            model.Series.Add(s22RealSeries);
            return model;
        }
        public static PlotModel CreateImaginaryPartPlotModel(double[] frequencies, double[] s11Mag, double[] s21Mag, double[] s12Mag, double[] s22Mag, double[] s11Pha, double[] s21Pha,
           double[] s12Pha, double[] s22Pha, bool match)
        {

            double[] s11Real = new double[s11Mag.Length];
            double[] s21Real = new double[s21Mag.Length];
            double[] s12Real = new double[s12Mag.Length];
            double[] s22Real = new double[s22Mag.Length];
            for (int i = 0; i < s11Real.Length; i++)
            {
                s11Real[i] = new Complex(s11Mag[i] * Math.Cos(s11Pha[i] * Math.PI / 180), s11Mag[i] * Math.Sin(s11Pha[i] * Math.PI / 180)).Imaginary;
                s21Real[i] = new Complex(s21Mag[i] * Math.Cos(s21Pha[i] * Math.PI / 180), s21Mag[i] * Math.Sin(s21Pha[i] * Math.PI / 180)).Imaginary;
                s12Real[i] = new Complex(s12Mag[i] * Math.Cos(s12Pha[i] * Math.PI / 180), s12Mag[i] * Math.Sin(s12Pha[i] * Math.PI / 180)).Imaginary;
                s22Real[i] = new Complex(s22Mag[i] * Math.Cos(s22Pha[i] * Math.PI / 180), s22Mag[i] * Math.Sin(s22Pha[i] * Math.PI / 180)).Imaginary;
            }
            var model = new PlotModel
            {
                Title = "Real Part of S-Parameters"
            };
            var legendForRealPart = new Legend
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorderThickness = 0
            };
            model.Legends.Add(legendForRealPart);
            var frequencyAxis = new LinearAxis { Title = "Frequency (GHz)", Position = AxisPosition.Bottom };
            var realPartAxis = new LinearAxis { Title = "Imaginary Part", Position = AxisPosition.Left };
            frequencyAxis.MajorGridlineStyle = LineStyle.Solid;
            frequencyAxis.MinorGridlineStyle = LineStyle.Dot;
            realPartAxis.MajorGridlineStyle = LineStyle.Solid;
            realPartAxis.MinorGridlineStyle = LineStyle.Dot;
            model.Axes.Add(frequencyAxis);
            model.Axes.Add(realPartAxis);

            var s11RealSeries = new LineSeries { Title = "Imaginary Part of S11" };
            var s21RealSeries = new LineSeries { Title = "Imaginary Part of S21" };
            var s12RealSeries = new LineSeries { Title = "Imaginary Part of S12" };
            var s22RealSeries = new LineSeries { Title = "Imaginary Part of S22" };
            if (!match)
            {
                var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11RealSeries.Points.Add(new DataPoint(gHzfreq[i], s11Real[i]));
                    s21RealSeries.Points.Add(new DataPoint(gHzfreq[i], s21Real[i]));
                    s12RealSeries.Points.Add(new DataPoint(gHzfreq[i], s12Real[i]));
                    s22RealSeries.Points.Add(new DataPoint(gHzfreq[i], s22Real[i]));
                }
            }
            else
            {
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11RealSeries.Points.Add(new DataPoint(frequencies[i], s11Real[i]));
                    s21RealSeries.Points.Add(new DataPoint(frequencies[i], s21Real[i]));
                    s12RealSeries.Points.Add(new DataPoint(frequencies[i], s12Real[i]));
                    s22RealSeries.Points.Add(new DataPoint(frequencies[i], s22Real[i]));
                }
            }
            model.Series.Add(s11RealSeries);
            model.Series.Add(s12RealSeries);
            model.Series.Add(s21RealSeries);
            model.Series.Add(s22RealSeries);
            return model;
        }
        public static void LoadS2PData(string filePath, out double[] frequencies, out double[] s11Mag, out double[] s21Mag, out double[] s12Mag, out double[] s22Mag, out double[] s11Pha, 
            out double[] s21Pha, out double[] s12Pha, out double[] s22Pha, out bool match, out bool db)
        {
            var lines = File.ReadAllLines(filePath);
            string all = "";
            var search = lines.Where(line => line.StartsWith("#")).ToArray();
            for (int i = 0; i < search.Length; i++)
            {
                all += search[i];
            }
            db = all.IndexOf("db", StringComparison.CurrentCultureIgnoreCase) >= 0;
            match = all.IndexOf("ghz", StringComparison.CurrentCultureIgnoreCase) >= 0;
            var dataLines = lines.Where(line => !line.TrimStart().StartsWith("!") && !line.TrimStart().StartsWith("#")).ToArray();
            frequencies = new double[dataLines.Length - 1];
            s11Mag = new double[dataLines.Length - 1];
            s21Mag = new double[dataLines.Length - 1];
            s12Mag = new double[dataLines.Length - 1];
            s22Mag = new double[dataLines.Length - 1];
            s11Pha = new double[dataLines.Length - 1];
            s21Pha = new double[dataLines.Length - 1];
            s12Pha = new double[dataLines.Length - 1];
            s22Pha = new double[dataLines.Length - 1];

            for (int i = 1; i < dataLines.Length; i++)
            {
                var parts = dataLines[i].Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                frequencies[i - 1] = double.Parse(parts[0], CultureInfo.InvariantCulture);
                s11Mag[i - 1] = double.Parse(parts[1], CultureInfo.InvariantCulture);
                s11Pha[i - 1] = double.Parse(parts[2], CultureInfo.InvariantCulture);
                s21Mag[i - 1] = double.Parse(parts[3], CultureInfo.InvariantCulture);
                s21Pha[i - 1] = double.Parse(parts[4], CultureInfo.InvariantCulture);
                s12Mag[i - 1] = double.Parse(parts[5], CultureInfo.InvariantCulture);
                s12Pha[i - 1] = double.Parse(parts[6], CultureInfo.InvariantCulture);
                s22Mag[i - 1] = double.Parse(parts[7], CultureInfo.InvariantCulture);
                s22Pha[i - 1] = double.Parse(parts[8], CultureInfo.InvariantCulture);
            }
        }
    }

}