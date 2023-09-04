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
    internal class s3pFile
    {
        private const double HzToGHz = 1e-9;
        public static PlotModel CreateMagnitudePlotModel(double[] frequencies, double[] s11Mag, double[] s21Mag, double[] s12Mag, double[] s22Mag, double[] s13Mag, double[] s23Mag,
            double[] s31Mag, double[] s32Mag, double[] s33Mag, bool match, bool db)
        {
            var model = new PlotModel
            {
                Title = "Magnitude of S-Parameters"
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

            model.Axes.Add(frequencyAxis);
            model.Axes.Add(magnitudeAxis);

            var s11MagSeries = new LineSeries { Title = "|S11|" };
            var s21MagSeries = new LineSeries { Title = "|S21|" };
            var s12MagSeries = new LineSeries { Title = "|S12|" };
            var s22MagSeries = new LineSeries { Title = "|S22|" };
            var s13MagSeries = new LineSeries { Title = "|S13|" };
            var s23MagSeries = new LineSeries { Title = "|S23|" };
            var s31MagSeries = new LineSeries { Title = "|S31|" };
            var s32MagSeries = new LineSeries { Title = "|S32|" };
            var s33MagSeries = new LineSeries { Title = "|S33|" };
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
                        s13MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(Math.Pow(10, s13Mag[i] / 20))));
                        s23MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(Math.Pow(10, s23Mag[i] / 20))));
                        s31MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(Math.Pow(10, s31Mag[i] / 20))));
                        s32MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(Math.Pow(10, s32Mag[i] / 20))));
                        s33MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(Math.Pow(10, s33Mag[i] / 20))));
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
                        s13MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(s13Mag[i])));
                        s23MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(s23Mag[i])));
                        s31MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(s31Mag[i])));
                        s32MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(s22Mag[i])));
                        s33MagSeries.Points.Add(new DataPoint(gHzfreq[i], Math.Abs(s33Mag[i])));
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
                        s13MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(Math.Pow(10, s13Mag[i] / 20))));
                        s23MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(Math.Pow(10, s23Mag[i] / 20))));
                        s31MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(Math.Pow(10, s31Mag[i] / 20))));
                        s32MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(Math.Pow(10, s32Mag[i] / 20))));
                        s33MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(Math.Pow(10, s33Mag[i] / 20))));
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
                        s13MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(s13Mag[i])));
                        s23MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(s23Mag[i])));
                        s31MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(s31Mag[i])));
                        s32MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(s32Mag[i])));
                        s33MagSeries.Points.Add(new DataPoint(frequencies[i], Math.Abs(s33Mag[i])));
                    }
                }

            }

            model.Series.Add(s11MagSeries);
            model.Series.Add(s21MagSeries);
            model.Series.Add(s12MagSeries);
            model.Series.Add(s22MagSeries);
            model.Series.Add(s13MagSeries);
            model.Series.Add(s23MagSeries);
            model.Series.Add(s31MagSeries);
            model.Series.Add(s32MagSeries);
            model.Series.Add(s33MagSeries);

            return model;
        }
        public static PlotModel CreateMagnitudePlotModelIndB(double[] frequencies, double[] s11Mag, double[] s21Mag, double[] s12Mag, double[] s22Mag,
            double[] s13Mag, double[] s23Mag, double[] s31Mag, double[] s32Mag, double[] s33Mag, bool match, bool db)
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

            model.Axes.Add(frequencyAxis);
            model.Axes.Add(magnitudeAxis);

            var s11MagSeries = new LineSeries { Title = "S11" };
            var s21MagSeries = new LineSeries { Title = "S21" };
            var s12MagSeries = new LineSeries { Title = "S12" };
            var s22MagSeries = new LineSeries { Title = "S22" };
            var s13MagSeries = new LineSeries { Title = "S13" };
            var s23MagSeries = new LineSeries { Title = "S23" };
            var s31MagSeries = new LineSeries { Title = "S31" };
            var s32MagSeries = new LineSeries { Title = "S32" };
            var s33MagSeries = new LineSeries { Title = "S33" };
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
                        s13MagSeries.Points.Add(new DataPoint(gHzfreq[i], s13Mag[i]));
                        s23MagSeries.Points.Add(new DataPoint(gHzfreq[i], s23Mag[i]));
                        s31MagSeries.Points.Add(new DataPoint(gHzfreq[i], s31Mag[i]));
                        s32MagSeries.Points.Add(new DataPoint(gHzfreq[i], s32Mag[i]));
                        s33MagSeries.Points.Add(new DataPoint(gHzfreq[i], s33Mag[i]));
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
                        s13MagSeries.Points.Add(new DataPoint(gHzfreq[i], 20 * Math.Log10(s13Mag[i])));
                        s23MagSeries.Points.Add(new DataPoint(gHzfreq[i], 20 * Math.Log10(s23Mag[i])));
                        s31MagSeries.Points.Add(new DataPoint(gHzfreq[i], 20 * Math.Log10(s31Mag[i])));
                        s32MagSeries.Points.Add(new DataPoint(gHzfreq[i], 20 * Math.Log10(s32Mag[i])));
                        s33MagSeries.Points.Add(new DataPoint(gHzfreq[i], 20 * Math.Log10(s33Mag[i])));
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
                        s13MagSeries.Points.Add(new DataPoint(frequencies[i], s13Mag[i]));
                        s23MagSeries.Points.Add(new DataPoint(frequencies[i], s23Mag[i]));
                        s31MagSeries.Points.Add(new DataPoint(frequencies[i], s31Mag[i]));
                        s32MagSeries.Points.Add(new DataPoint(frequencies[i], s32Mag[i]));
                        s33MagSeries.Points.Add(new DataPoint(frequencies[i], s33Mag[i]));
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
                        s13MagSeries.Points.Add(new DataPoint(frequencies[i], 20 * Math.Log10(s13Mag[i])));
                        s23MagSeries.Points.Add(new DataPoint(frequencies[i], 20 * Math.Log10(s23Mag[i])));
                        s31MagSeries.Points.Add(new DataPoint(frequencies[i], 20 * Math.Log10(s31Mag[i])));
                        s32MagSeries.Points.Add(new DataPoint(frequencies[i], 20 * Math.Log10(s32Mag[i])));
                        s33MagSeries.Points.Add(new DataPoint(frequencies[i], 20 * Math.Log10(s33Mag[i])));
                    }
                }
            }
            model.Series.Add(s11MagSeries);
            model.Series.Add(s21MagSeries);
            model.Series.Add(s12MagSeries);
            model.Series.Add(s22MagSeries);
            model.Series.Add(s13MagSeries);
            model.Series.Add(s23MagSeries);
            model.Series.Add(s31MagSeries);
            model.Series.Add(s32MagSeries);
            model.Series.Add(s33MagSeries);

            return model;
        }
        public static PlotModel CreatePhasePlotModel(double[] frequencies, double[] s11Pha, double[] s21Pha, double[] s12Pha, double[] s22Pha, double[] s13Pha,
            double[] s23Pha, double[] s31Pha, double[] s32Pha, double[] s33Pha, bool match)
        {
            var model = new PlotModel
            {
                Title = "Phase of S-Parameter"
            };

            var frequencyAxis = new LinearAxis { Title = "Frequency (GHz)", Position = AxisPosition.Bottom };
            var phaseAxis = new LinearAxis { Title = "Phase of S-Parameter", Position = AxisPosition.Left };

            model.Axes.Add(frequencyAxis);
            model.Axes.Add(phaseAxis);
            var legendForPhase = new Legend
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorderThickness = 0
            };
            model.Legends.Add(legendForPhase);
            var s11PhaSeries = new LineSeries { Title = "S11" };
            var s21PhaSeries = new LineSeries { Title = "S21" };
            var s12PhaSeries = new LineSeries { Title = "S12" };
            var s22PhaSeries = new LineSeries { Title = "S22" };
            var s13PhaSeries = new LineSeries { Title = "S13" };
            var s23PhaSeries = new LineSeries { Title = "S23" };
            var s31PhaSeries = new LineSeries { Title = "S31" };
            var s32PhaSeries = new LineSeries { Title = "S32" };
            var s33PhaSeries = new LineSeries { Title = "S33" };
            if (!match)
            {
                var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s11Pha[i]));
                    s21PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s21Pha[i]));
                    s12PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s12Pha[i]));
                    s22PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s22Pha[i]));
                    s13PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s13Pha[i]));
                    s23PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s23Pha[i]));
                    s31PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s31Pha[i]));
                    s32PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s32Pha[i]));
                    s33PhaSeries.Points.Add(new DataPoint(gHzfreq[i], s33Pha[i]));
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
                    s13PhaSeries.Points.Add(new DataPoint(frequencies[i], s13Pha[i]));
                    s23PhaSeries.Points.Add(new DataPoint(frequencies[i], s23Pha[i]));
                    s31PhaSeries.Points.Add(new DataPoint(frequencies[i], s31Pha[i]));
                    s32PhaSeries.Points.Add(new DataPoint(frequencies[i], s32Pha[i]));
                    s33PhaSeries.Points.Add(new DataPoint(frequencies[i], s33Pha[i]));
                }
            }
            model.Series.Add(s11PhaSeries);
            model.Series.Add(s21PhaSeries);
            model.Series.Add(s12PhaSeries);
            model.Series.Add(s22PhaSeries);
            model.Series.Add(s13PhaSeries);
            model.Series.Add(s23PhaSeries);
            model.Series.Add(s31PhaSeries);
            model.Series.Add(s32PhaSeries);
            model.Series.Add(s33PhaSeries);

            return model;
        }
        public static PlotModel CreateRealPartPlotModel(double[] frequencies, double[] s11Mag, double[] s21Mag, double[] s12Mag, double[] s22Mag,
            double[] s13Mag, double[] s23Mag, double[] s33Mag, double[] s31Mag, double[] s32Mag, double[] s11Pha, double[] s12Pha,double[] s21Pha, double[] s22Pha,
            double[] s13Pha, double[] s23Pha, double[] s33Pha, double[] s31Pha, double[] s32Pha, bool match)
        {
            double[] s11Real = new double[s11Mag.Length];
            double[] s21Real = new double[s21Mag.Length];
            double[] s12Real = new double[s12Mag.Length];
            double[] s22Real = new double[s22Mag.Length];
            double[] s13Real = new double[s22Mag.Length];
            double[] s23Real = new double[s22Mag.Length];
            double[] s33Real = new double[s22Mag.Length];
            double[] s31Real = new double[s22Mag.Length];
            double[] s32Real = new double[s22Mag.Length];
            for (int i = 0; i < s11Real.Length; i++)
            {
                s11Real[i] = new Complex(s11Mag[i] * Math.Cos(s11Pha[i] * Math.PI / 180), s11Mag[i] * Math.Sin(s11Pha[i] * Math.PI / 180)).Real;
                s21Real[i] = new Complex(s21Mag[i] * Math.Cos(s21Pha[i] * Math.PI / 180), s21Mag[i] * Math.Sin(s21Pha[i] * Math.PI / 180)).Real;
                s12Real[i] = new Complex(s12Mag[i] * Math.Cos(s12Pha[i] * Math.PI / 180), s12Mag[i] * Math.Sin(s12Pha[i] * Math.PI / 180)).Real;
                s22Real[i] = new Complex(s22Mag[i] * Math.Cos(s22Pha[i] * Math.PI / 180), s22Mag[i] * Math.Sin(s22Pha[i] * Math.PI / 180)).Real;
                s13Real[i] = new Complex(s13Mag[i] * Math.Cos(s13Pha[i] * Math.PI / 180), s13Mag[i] * Math.Sin(s13Pha[i] * Math.PI / 180)).Real;
                s23Real[i] = new Complex(s23Mag[i] * Math.Cos(s23Pha[i] * Math.PI / 180), s23Mag[i] * Math.Sin(s23Pha[i] * Math.PI / 180)).Real;
                s33Real[i] = new Complex(s33Mag[i] * Math.Cos(s33Pha[i] * Math.PI / 180), s33Mag[i] * Math.Sin(s33Pha[i] * Math.PI / 180)).Real;
                s31Real[i] = new Complex(s31Mag[i] * Math.Cos(s31Pha[i] * Math.PI / 180), s31Mag[i] * Math.Sin(s31Pha[i] * Math.PI / 180)).Real;
                s22Real[i] = new Complex(s32Mag[i] * Math.Cos(s32Pha[i] * Math.PI / 180), s32Mag[i] * Math.Sin(s32Pha[i] * Math.PI / 180)).Real;
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
            var realPartAxis = new LinearAxis { Title = "Real Part of S-Parameter", Position = AxisPosition.Left };
            model.Axes.Add(frequencyAxis);
            model.Axes.Add(realPartAxis);

            var s11RealSeries = new LineSeries { Title = "Real Part of S11" };
            var s21RealSeries = new LineSeries { Title = "Real Part of S21" };
            var s12RealSeries = new LineSeries { Title = "Real Part of S12" };
            var s22RealSeries = new LineSeries { Title = "Real Part of S22" };
            var s13RealSeries = new LineSeries { Title = "Real Part of S13" };
            var s23RealSeries = new LineSeries { Title = "Real Part of S23" };
            var s33RealSeries = new LineSeries { Title = "Real Part of S33" };
            var s31RealSeries = new LineSeries { Title = "Real Part of S21" };
            var s32RealSeries = new LineSeries { Title = "Real Part of S32" };
            if (!match)
            {
                var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11RealSeries.Points.Add(new DataPoint(gHzfreq[i], s11Real[i]));
                    s21RealSeries.Points.Add(new DataPoint(gHzfreq[i], s21Real[i]));
                    s12RealSeries.Points.Add(new DataPoint(gHzfreq[i], s12Real[i]));
                    s22RealSeries.Points.Add(new DataPoint(gHzfreq[i], s22Real[i]));
                    s13RealSeries.Points.Add(new DataPoint(gHzfreq[i], s13Real[i]));
                    s23RealSeries.Points.Add(new DataPoint(gHzfreq[i], s23Real[i]));
                    s31RealSeries.Points.Add(new DataPoint(gHzfreq[i], s31Real[i]));
                    s32RealSeries.Points.Add(new DataPoint(gHzfreq[i], s32Real[i]));
                    s33RealSeries.Points.Add(new DataPoint(gHzfreq[i], s33Real[i]));
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
                    s13RealSeries.Points.Add(new DataPoint(frequencies[i], s13Real[i]));
                    s23RealSeries.Points.Add(new DataPoint(frequencies[i], s23Real[i]));
                    s31RealSeries.Points.Add(new DataPoint(frequencies[i], s31Real[i]));
                    s32RealSeries.Points.Add(new DataPoint(frequencies[i], s32Real[i]));
                    s33RealSeries.Points.Add(new DataPoint(frequencies[i], s33Real[i]));
                }
            }
            model.Series.Add(s11RealSeries);
            model.Series.Add(s12RealSeries);
            model.Series.Add(s21RealSeries);
            model.Series.Add(s22RealSeries);
            model.Series.Add(s13RealSeries);
            model.Series.Add(s23RealSeries);
            model.Series.Add(s33RealSeries);
            model.Series.Add(s31RealSeries);
            model.Series.Add(s33RealSeries);
            return model;
        }
        public static PlotModel CreateImaginaryPartPlotModel(double[] frequencies, double[] s11Mag, double[] s21Mag, double[] s12Mag, double[] s22Mag,
           double[] s13Mag, double[] s23Mag, double[] s33Mag, double[] s31Mag, double[] s32Mag, double[] s11Pha, double[] s12Pha, double[] s21Pha, double[] s22Pha,
           double[] s13Pha, double[] s23Pha, double[] s33Pha, double[] s31Pha, double[] s32Pha, bool match)
        {
            double[] s11Imaginary = new double[s11Mag.Length];
            double[] s21Imaginary = new double[s21Mag.Length];
            double[] s12Imaginary = new double[s12Mag.Length];
            double[] s22Imaginary = new double[s22Mag.Length];
            double[] s13Imaginary = new double[s22Mag.Length];
            double[] s23Imaginary = new double[s22Mag.Length];
            double[] s33Imaginary = new double[s22Mag.Length];
            double[] s31Imaginary = new double[s22Mag.Length];
            double[] s32Imaginary = new double[s22Mag.Length];
            for (int i = 0; i < s11Imaginary.Length; i++)
            {
                s11Imaginary[i] = new Complex(s11Mag[i] * Math.Cos(s11Pha[i] * Math.PI / 180), s11Mag[i] * Math.Sin(s11Pha[i] * Math.PI / 180)).Imaginary;
                s21Imaginary[i] = new Complex(s21Mag[i] * Math.Cos(s21Pha[i] * Math.PI / 180), s21Mag[i] * Math.Sin(s21Pha[i] * Math.PI / 180)).Imaginary;
                s12Imaginary[i] = new Complex(s12Mag[i] * Math.Cos(s12Pha[i] * Math.PI / 180), s12Mag[i] * Math.Sin(s12Pha[i] * Math.PI / 180)).Imaginary;
                s22Imaginary[i] = new Complex(s22Mag[i] * Math.Cos(s22Pha[i] * Math.PI / 180), s22Mag[i] * Math.Sin(s22Pha[i] * Math.PI / 180)).Imaginary;
                s13Imaginary[i] = new Complex(s13Mag[i] * Math.Cos(s13Pha[i] * Math.PI / 180), s13Mag[i] * Math.Sin(s13Pha[i] * Math.PI / 180)).Imaginary;
                s23Imaginary[i] = new Complex(s23Mag[i] * Math.Cos(s23Pha[i] * Math.PI / 180), s23Mag[i] * Math.Sin(s23Pha[i] * Math.PI / 180)).Imaginary;
                s33Imaginary[i] = new Complex(s33Mag[i] * Math.Cos(s33Pha[i] * Math.PI / 180), s33Mag[i] * Math.Sin(s33Pha[i] * Math.PI / 180)).Imaginary;
                s31Imaginary[i] = new Complex(s31Mag[i] * Math.Cos(s31Pha[i] * Math.PI / 180), s31Mag[i] * Math.Sin(s31Pha[i] * Math.PI / 180)).Imaginary;
                s22Imaginary[i] = new Complex(s32Mag[i] * Math.Cos(s32Pha[i] * Math.PI / 180), s32Mag[i] * Math.Sin(s32Pha[i] * Math.PI / 180)).Imaginary;
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
            var realPartAxis = new LinearAxis { Title = "Imaginary Part of S-Parameter", Position = AxisPosition.Left };
            model.Axes.Add(frequencyAxis);
            model.Axes.Add(realPartAxis);

            var s11ImaginarySeries = new LineSeries { Title = "Imaginary Part of S11" };
            var s21ImaginarySeries = new LineSeries { Title = "Imaginary Part of S21" };
            var s12ImaginarySeries = new LineSeries { Title = "Imaginary Part of S12" };
            var s22ImaginarySeries = new LineSeries { Title = "Imaginary Part of S22" };
            var s13ImaginarySeries = new LineSeries { Title = "Imaginary Part of S13" };
            var s23ImaginarySeries = new LineSeries { Title = "Imaginary Part of S23" };
            var s33ImaginarySeries = new LineSeries { Title = "Imaginary Part of S33" };
            var s31ImaginarySeries = new LineSeries { Title = "Imaginary Part of S21" };
            var s32ImaginarySeries = new LineSeries { Title = "Imaginary Part of S32" };
            if (!match)
            {
                var gHzfreq = frequencies.Select(f => f * HzToGHz).ToArray();
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11ImaginarySeries.Points.Add(new DataPoint(gHzfreq[i], s11Imaginary[i]));
                    s21ImaginarySeries.Points.Add(new DataPoint(gHzfreq[i], s21Imaginary[i]));
                    s12ImaginarySeries.Points.Add(new DataPoint(gHzfreq[i], s12Imaginary[i]));
                    s22ImaginarySeries.Points.Add(new DataPoint(gHzfreq[i], s22Imaginary[i]));
                    s13ImaginarySeries.Points.Add(new DataPoint(gHzfreq[i], s13Imaginary[i]));
                    s23ImaginarySeries.Points.Add(new DataPoint(gHzfreq[i], s23Imaginary[i]));
                    s31ImaginarySeries.Points.Add(new DataPoint(gHzfreq[i], s31Imaginary[i]));
                    s32ImaginarySeries.Points.Add(new DataPoint(gHzfreq[i], s32Imaginary[i]));
                    s33ImaginarySeries.Points.Add(new DataPoint(gHzfreq[i], s33Imaginary[i]));
                }
            }
            else
            {
                for (int i = 0; i < frequencies.Length; i++)
                {
                    s11ImaginarySeries.Points.Add(new DataPoint(frequencies[i], s11Imaginary[i]));
                    s21ImaginarySeries.Points.Add(new DataPoint(frequencies[i], s21Imaginary[i]));
                    s12ImaginarySeries.Points.Add(new DataPoint(frequencies[i], s12Imaginary[i]));
                    s22ImaginarySeries.Points.Add(new DataPoint(frequencies[i], s22Imaginary[i]));
                    s13ImaginarySeries.Points.Add(new DataPoint(frequencies[i], s13Imaginary[i]));
                    s23ImaginarySeries.Points.Add(new DataPoint(frequencies[i], s23Imaginary[i]));
                    s31ImaginarySeries.Points.Add(new DataPoint(frequencies[i], s31Imaginary[i]));
                    s32ImaginarySeries.Points.Add(new DataPoint(frequencies[i], s32Imaginary[i]));
                    s33ImaginarySeries.Points.Add(new DataPoint(frequencies[i], s33Imaginary[i]));
                }
            }
            model.Series.Add(s11ImaginarySeries);
            model.Series.Add(s12ImaginarySeries);
            model.Series.Add(s21ImaginarySeries);
            model.Series.Add(s22ImaginarySeries);
            model.Series.Add(s13ImaginarySeries);
            model.Series.Add(s23ImaginarySeries);
            model.Series.Add(s33ImaginarySeries);
            model.Series.Add(s31ImaginarySeries);
            model.Series.Add(s33ImaginarySeries);
            return model;
        }
        public static void LoadS3PData(string filePath, out double[] frequencies, out double[] s11Mag, out double[] s21Mag, out double[] s12Mag,
            out double[] s22Mag, out double[] s13Mag, out double[] s23Mag, out double[] s31Mag, out double[] s32Mag, out double[] s33Mag, out double[] s11Pha,
            out double[] s21Pha, out double[] s12Pha, out double[] s22Pha, out double[] s13Pha, out double[] s23Pha, out double[] s31Pha, out double[] s32Pha,
            out double[] s33Pha, out bool match, out bool db)
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
            s13Mag = new double[dataLines.Length - 1];
            s23Mag = new double[dataLines.Length - 1];
            s31Mag = new double[dataLines.Length - 1];
            s32Mag = new double[dataLines.Length - 1];
            s33Mag = new double[dataLines.Length - 1];
            s11Pha = new double[dataLines.Length - 1];
            s21Pha = new double[dataLines.Length - 1];
            s12Pha = new double[dataLines.Length - 1];
            s22Pha = new double[dataLines.Length - 1];
            s13Pha = new double[dataLines.Length - 1];
            s23Pha = new double[dataLines.Length - 1];
            s31Pha = new double[dataLines.Length - 1];
            s32Pha = new double[dataLines.Length - 1];
            s33Pha = new double[dataLines.Length - 1];

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
                s13Mag[i - 1] = double.Parse(parts[9], CultureInfo.InvariantCulture);
                s13Pha[i - 1] = double.Parse(parts[10], CultureInfo.InvariantCulture);
                s23Mag[i - 1] = double.Parse(parts[11], CultureInfo.InvariantCulture);
                s23Pha[i - 1] = double.Parse(parts[12], CultureInfo.InvariantCulture);
                s31Mag[i - 1] = double.Parse(parts[13], CultureInfo.InvariantCulture);
                s31Pha[i - 1] = double.Parse(parts[14], CultureInfo.InvariantCulture);
                s32Mag[i - 1] = double.Parse(parts[15], CultureInfo.InvariantCulture);
                s32Pha[i - 1] = double.Parse(parts[16], CultureInfo.InvariantCulture);
                s33Mag[i - 1] = double.Parse(parts[17], CultureInfo.InvariantCulture);
                s33Pha[i - 1] = double.Parse(parts[18], CultureInfo.InvariantCulture);
            }
        }
    }
}
