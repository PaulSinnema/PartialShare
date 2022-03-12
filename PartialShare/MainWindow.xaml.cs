using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PartialShare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string RememberFile = "PartialShare.json";

        private Remember? Remember { get; set; }

        private double _BorderSize = 20;

        public double BorderSize
        {
            get => _BorderSize;
            set
            {
                _BorderSize = value;

                this.BorderThickness = new Thickness(value);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DimBorders(Resizing);

            ToolWindow toolWindow = new ToolWindow(this);

            toolWindow.Show();
        }

        private void SetRemember(Remember? remember)
        {
            if (remember != null)
            {
                Top = remember.LastY;
                Left = remember.LastX;
                Width = remember.LastWidth;
                Height = remember.LastHeight;
                Resizing = remember.IsResizing;
            }

            DimBorders(Resizing);
        }

        private Remember GetRemember()
        {
            var remember = new Remember()
            {
                LastX = Left,
                LastY = Top,
                LastWidth = Width,
                LastHeight = Height,
                IsResizing = Resizing
            };

            return remember;
        }

        private void RestoreRemember()
        {
            Remember? remember = null;

            if (File.Exists(RememberFile))
            {
                var file = File.OpenRead(RememberFile);

                var buffer = new byte[file.Length];

                file.Read(buffer, 0, (int)file.Length);

                file.Close();
                file.Dispose();

                var content = UTF8Encoding.ASCII.GetString(buffer);

                remember = Newtonsoft.Json.JsonConvert.DeserializeObject<Remember>(content);

                SetRemember(remember);
            }
        }

        private bool IsActivated = false;

        protected override void OnActivated(EventArgs e)
        {
            if (!IsActivated)
            {
                RestoreRemember();

                IsActivated = true;
            }

            base.OnActivated(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var remember = GetRemember();

            SaveRemember(remember);

            base.OnClosing(e);
        }

        private static void SaveRemember(Remember remember)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(remember);
            var bytes = UTF8Encoding.ASCII.GetBytes(json);

            var file = File.Open(RememberFile, FileMode.Create);

            file.Write(bytes, 0, bytes.Length);

            file.Close();
            file.Dispose();
        }

        public bool ToggleResizing()
        {
            Resizing = !Resizing;

            DimBorders(Resizing);

            return Resizing;
        }

        private bool Resizing { get; set; }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            DimBorders(Resizing);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            DimBorders(Resizing);
        }

        private void DimBorders(bool high)
        {
            if (high)
            {
                PartialShare.Opacity = 1;
                BorderThickness = new Thickness(BorderSize);
            }
            else
            {
                PartialShare.Opacity = 0.5;
                BorderThickness = new Thickness(1);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Resizing = !Resizing;

            DimBorders(Resizing);
        }
    }
}
