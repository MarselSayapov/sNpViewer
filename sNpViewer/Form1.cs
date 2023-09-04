using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using sNpViewer.Properties;

namespace sNpViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            this.Icon = new Icon(Path.Combine(Directory.GetCurrentDirectory(), "sNpViewerICON.ico"));
            this.Text = "sNpViewer";
            this.Size = new Size(800, 800);
            this.MaximizeBox = false;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            var label = new Label
            {
                Text = @"Welcome!",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(Top, Top)
            };
            var name = new Label
            {
                Text = @"sNpViewer",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                Location = new Point(Top, Top)
            };
            LinkLabel documentationLabel = new LinkLabel()
            {
                Text = @"Documentation",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(Top, Top)
            };
            documentationLabel.Click += (sender, e) =>
            {
                var direc = AppDomain.CurrentDomain.BaseDirectory;
                var files = Directory.GetFiles(direc);
                var filePath = files.Where(x => x.Contains("Help.chm")).Select(x => x).FirstOrDefault();
                if (File.Exists(filePath))
                {
                    if (filePath != null) Process.Start(filePath);
                }
                else
                {
                    MessageBox.Show(@"File is not found.");
                }
            };
            var button = new Button()
            {
                Text = @"Upload your file",
                Dock = DockStyle.Fill,
            };
            var table = new TableLayoutPanel();
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            table.Controls.Add(new Panel(), 0, 0);
            table.Controls.Add(name, 0, 1);
            table.Controls.Add(label, 0, 2);
            table.Controls.Add(button, 0, 3);
            table.Controls.Add(documentationLabel, 0, 5);
            table.Controls.Add(new Panel(), 0, 6);
            button.Click += (sender, e) =>
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = @"Text files | *.txt; *.s2p; *.csv; *.s1p; *.s3p"; // file types, that will be allowed to upload
                    dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
                    if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
                    {
                        String path = dialog.FileName; // get name of file
                       
                        var s2Pgraph = new S2Pgraphic(path);
                        s2Pgraph.Text = @"sNpViewer";
                        s2Pgraph.Size = new Size(400, 400);
                        s2Pgraph.MaximizeBox = false;
                        s2Pgraph.Show();
                    }
                };
            table.Dock = DockStyle.Fill;
            Controls.Add(table);
        }

        [Localizable(false)]
        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
    }
}
