using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;

namespace PartialShare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string RememberFile = "PartialShare.json";
        private const double DefaultBorderSize = 10;

        /// <summary>
        /// The ToolWindow reference.
        /// </summary>
        private ToolWindow ToolWindow { get; set; } = null;

        private double _BorderSize = DefaultBorderSize;

        /// <summary>
        /// The thickness of the border.
        /// </summary>
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
        }

        /// <summary>
        /// Set values for the MainWindow using a Remember object.
        /// </summary>
        private void SetRemember(Remember? remember)
        {
            if (remember != null)
            {
                Top = remember.LastY;
                Left = remember.LastX;
                Width = remember.LastWidth;
                Height = remember.LastHeight;
                IsResizing = remember.IsResizing;
                BorderSize = remember.BorderSize == 0 ? DefaultBorderSize : remember.BorderSize;
                ToolWindow.WindowState = remember.ToolWindowState;
            }

            SetMainWindowState();
        }

        /// <summary>
        /// Get a Remember object with values of the MainWindow.
        /// </summary>
        private Remember GetRemember()
        {
            var remember = new Remember()
            {
                LastX = Left,
                LastY = Top,
                LastWidth = Width,
                LastHeight = Height,
                IsResizing = IsResizing,
                BorderSize = BorderSize,
                ToolWindowState = ToolWindow.WindowState
            };

            return remember;
        }

        /// <summary>
        /// Read the Remember json from disk and restore the values to the MainWindow.
        /// </summary>
        private void RestoreRemember()
        {
            if (File.Exists(RememberFile))
            {
                Remember? remember = GetFileContent(RememberFile);

                SetRemember(remember);
            }
        }

        /// <summary>
        /// Read the Remember json file from disk and return its Contents as a Remember object.
        /// </summary>
        private static Remember? GetFileContent(string path)
        {
            var file = File.OpenRead(path);

            var buffer = new byte[file.Length];

            file.Read(buffer, 0, (int)file.Length);

            file.Close();
            file.Dispose();

            var content = UTF8Encoding.ASCII.GetString(buffer);

            var remember = Newtonsoft.Json.JsonConvert.DeserializeObject<Remember>(content);

            return remember;
        }

        /// <summary>
        /// Initialization of MainWindow is complete.
        /// </summary>
        protected override void OnInitialized(EventArgs e)
        {
            InitializeWindows();

            // this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            base.OnInitialized(e);
        }

        /// <summary>
        /// Create the ToolWindow and restore and initialize parameters.
        /// </summary>
        private void InitializeWindows()
        {
            ToolWindow = new ToolWindow(this);

            RestoreRemember();

            ToolWindow.Initialize();

            ToolWindow.Show();

            SetMainWindowState();
        }

        /// <summary>
        /// Application is closing (initiated from ToolWindow). Save parameters.
        /// </summary>
        protected override void OnClosing(CancelEventArgs e)
        {
            SaveRemember(GetRemember());

            base.OnClosing(e);
        }

        /// <summary>
        /// Save a Remember object as a json to disk.
        /// </summary>
        private static void SaveRemember(Remember remember)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(remember);
            var bytes = UTF8Encoding.ASCII.GetBytes(json);

            var file = File.Open(RememberFile, FileMode.Create);

            file.Write(bytes, 0, bytes.Length);

            file.Close();
            file.Dispose();
        }

        /// <summary>
        /// Toggle the resizing option and set borders accordingly.
        /// </summary>
        public bool ToggleResizing()
        {
            IsResizing = !IsResizing;

            SetMainWindowState();

            return IsResizing;
        }

        /// <summary>
        /// Public boolean that tells wether we are Resizing or not.
        /// </summary>
        public bool IsResizing { get; private set; }

        /// <summary>
        /// Set the opacity and thinckness of the border of the MainWindow depending
        /// on wether we are resizing or not.
        /// </summary>
        private void SetMainWindowState()
        {
            if (IsResizing)
            {
                ResizeMode = ResizeMode.CanResize;
                PartialShare.Opacity = 1;
                BorderThickness = new Thickness(BorderSize);
            }
            else
            {
                ResizeMode = ResizeMode.NoResize;
                PartialShare.Opacity = 0.5;
                PartialShare.BorderThickness = new Thickness(1);
            }
        }
    }
}
