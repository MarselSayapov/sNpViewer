using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;

namespace sNpViewer
{
    internal class s1pFile
    {
        private const double HzToGHz = 1e-9;
        public static PlotModel CreateMagnitudePlotModel(double[] frequencies, double[] smag, bool match, bool db)
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
            var magnitudeAxis = new LinearAxis { Title = "Magnitude S-Parameter", Position = AxisPosition.Left };
            frequencyAxis.MajorGridlineStyle = LineStyle.Solid;
            frequencyAxis.MinorGridlineStyle = LineStyle.Dot;
            magnitudeAxis.MajorGridlineStyle = LineStyle.Solid;
            magnitudeAxis.MinorGridlineStyle = LineStyle.Dot;
            model.Axes.Add(frequencyAxis);
            model.Axes.Add(magnitudeAxis);

            var smagSeries = new LineSeries { Title = "|S11|" };
            if (!match)
            {
                if (db)
                {
                    var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        smagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(Math.Pow(10, smag[i] / 20))));
                    }
                }
                else
                {
                    var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        smagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(smag[i])));
                    }
                }
            }
            else
            {
                if (db)
                {
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        smagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(Math.Pow(10, smag[i] / 20))));
                    }
                }
                else
                {
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        smagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(smag[i])));
                    }
                }

            }
            model.Series.Add(smagSeries);
            return model;
        }
        public static PlotModel CreateMagnitudePlotModelIndB(double[] frequencies, double[] smag, bool match, bool db)
        {
            var model = new PlotModel
            {
                Title = "Magnitude S-Parameter"
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

            var smagSeries = new LineSeries { Title = "S11" };
            if (!match)
            {
                if (db)
                {
                    var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        smagSeries.Points.Add(new DataPoint(gHzfreq[i], smag[i]));
                    }
                }
                else
                {
                    var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        smagSeries.Points.Add(new DataPoint(gHzfreq[i], 20 * Math.Log10(smag[i])));
                    }
                }
            }
            else
            {
                if (db)
                {
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        smagSeries.Points.Add(new DataPoint(frequencies[i], smag[i]));
                    }
                }
                else
                {
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        smagSeries.Points.Add(new DataPoint(frequencies[i], 20 * Math.Log10(smag[i])));
                    }
                }
            }
            model.Series.Add(smagSeries);

            return model;
        }
        public static PlotModel CreatePhasePlotModel(double[] frequencies, double[] spha, bool match)
        {
            var model = new PlotModel
            {
                Title = "Phase of S-Parameter"
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
            var phaseAxis = new LinearAxis { Title = "Phase S-Parameter", Position = AxisPosition.Left };
            frequencyAxis.MajorGridlineStyle = LineStyle.Solid;
            frequencyAxis.MinorGridlineStyle = LineStyle.Dot;
            phaseAxis.MajorGridlineStyle = LineStyle.Solid;
            phaseAxis.MinorGridlineStyle = LineStyle.Dot;
            model.Axes.Add(frequencyAxis);
            model.Axes.Add(phaseAxis);

            var sphaSeries = new LineSeries { Title = "S11" };

            if (!match)
            {
                    var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                    for (int i = 0; i < frequencies.Length; i++)
                    {
                        sphaSeries.Points.Add(new DataPoint(gHzfreq[i], spha[i]));
                    }
            }
            else
            {
                for (int i = 0; i < frequencies.Length; i++)
                {
                    sphaSeries.Points.Add(new DataPoint(frequencies[i], spha[i]));
                }
            }

            model.Series.Add(sphaSeries);

            return model;
        }
        public static PlotModel CreateImaginaryPartPlotModel(double[] frequencies, double[] magnitude, double[] phase, bool match)
        {

            double[] s11Imaginary = new double[magnitude.Length];
            for (int i = 0; i < s11Imaginary.Length; i++)
            {
                s11Imaginary[i] = new Complex(magnitude[i] * Math.Cos(phase[i] * Math.PI / 180), magnitude[i] * Math.Sin(phase[i] * Math.PI / 180)).Imaginary;
            }
            var model = new PlotModel
            {
                Title = "Imaginary Part of S-Parameter"
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

            var s11RealSeries = new LineSeries { Title = "Imaginary Part S11" };
            if (!match)
            {
                var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11RealSeries.Points.Add(new DataPoint(gHzfreq[i], s11Imaginary[i]));
                }
            }
            else
            {
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11RealSeries.Points.Add(new DataPoint(frequencies[i], s11Imaginary[i]));
                }
            }
            model.Series.Add(s11RealSeries);
            return model;
        }
        public static PlotModel CreateRealPartPlotModel(double[] frequencies, double[] magnitude, double[] phase, bool match)
        {

            double[] s11Real = new double[magnitude.Length];
            for (int i = 0; i < s11Real.Length; i++)
            {
                s11Real[i] = new Complex(magnitude[i] * Math.Cos(phase[i] * Math.PI / 180), magnitude[i] * Math.Sin(phase[i] * Math.PI / 180)).Real;
            }
            var model = new PlotModel
            {
                Title = "Real Part of S-Parameter"
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

            var s11RealSeries = new LineSeries { Title = "Reap Part of S11" };
            if (!match)
            {
                var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11RealSeries.Points.Add(new DataPoint(gHzfreq[i], s11Real[i]));
                }
            }
            else
            {
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11RealSeries.Points.Add(new DataPoint(frequencies[i], s11Real[i]));
                }
            }
            model.Series.Add(s11RealSeries);
            return model;
        }
        public static void LoadS1PData(string filePath, out double[] frequencies, out double[] smag, out double[] spha, out bool match, out bool db)
        {

            var lines = File.ReadAllLines(filePath);
            string all = "";
            var search = lines.Where(line => line.StartsWith("#")).ToArray();
            foreach (var t in search)
            {
                all += t;
            }
            db = all.IndexOf("db", StringComparison.CurrentCultureIgnoreCase) >= 0;
            match = all.IndexOf("ghz", StringComparison.CurrentCultureIgnoreCase) >= 0;
            var dataLines = lines.Where(line => !line.TrimStart().StartsWith("!") && !line.TrimStart().StartsWith("#")).ToArray();
            frequencies = new double[dataLines.Length - 1];
            smag = new double[dataLines.Length - 1];
            spha = new double[dataLines.Length - 1];

            for (int i = 1; i < dataLines.Length; i++)
            {
                var parts = dataLines[i].Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                frequencies[i - 1] = double.Parse(parts[0], CultureInfo.InvariantCulture);
                smag[i - 1] = double.Parse(parts[1], CultureInfo.InvariantCulture);
                spha[i - 1] = double.Parse(parts[2], CultureInfo.InvariantCulture);
            }
        }
    }
}