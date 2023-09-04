using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace sNpViewer
{
    public partial class S2Pgraphic : Form
    {
        public S2Pgraphic(string data)
        {
            this.Icon = new Icon(Path.Combine(Directory.GetCurrentDirectory(), "sNpViewerICON.ico"));
            var label = new Label
            {
                Text = @"Select the desired type of graph to build:",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(Top, Top),
                Size = new Size(ClientSize.Width, 2 * ClientSize.Height / 10)
            };
            var magnitudeButton = new Button
            {
                Text = @"Magnitude",
                Location = new Point(30, label.Bottom),
                Size = new Size(ClientSize.Width / 2, ClientSize.Height / 2)
            };
            magnitudeButton.Click += (sender, args) =>
            {
                MagnitudeGraphic magnitudeGraphic = new MagnitudeGraphic(data);
                magnitudeGraphic.Show();
                magnitudeGraphic.Size = new Size(900, 900);
            };
            var phaseButton = new Button
            {
                Text = @"Phase",
                Size = magnitudeButton.Size,
                Location = new Point(30 + magnitudeButton.Right, label.Bottom)
            };
            phaseButton.Click += (sender, args) =>
            {
                PhaseGraphic phaseGraphic = new PhaseGraphic(data);
                phaseGraphic.Show();
                phaseGraphic.Size = new Size(900, 900);
            };
            var realPart = new Button
            {
                Text = @"Real Part",
                Size = magnitudeButton.Size,
                Location = new Point(30, 30 + magnitudeButton.Bottom)
            };
            realPart.Click += (sender, args) =>
            {
                RealPartGraphic realPartGraphic = new RealPartGraphic(data);
                realPartGraphic.Show();
                realPartGraphic.Size = new Size(900, 900);
            };
            var imaginaryPart = new Button
            {
                Text = @"Imaginary Part",
                Size = magnitudeButton.Size,
                Location = new Point(30 + realPart.Right, 30 + phaseButton.Bottom)
            };
            imaginaryPart.Click += (sender, args) =>
            {
                ImaginaryPartGraphic imaginaryPartGraphic = new ImaginaryPartGraphic(data);
                imaginaryPartGraphic.Show();
                imaginaryPartGraphic.Size = new Size(900, 900);
            };
            Controls.Add(label);
            Controls.Add(magnitudeButton);
            Controls.Add(phaseButton);
            Controls.Add(imaginaryPart);
            Controls.Add(realPart);
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

        }
        
    }
}