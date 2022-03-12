using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PartialShare
{
    /// <summary>
    /// This is the toolbox for Partial share.
    /// From this application several settings for the Mainwindow are controlled.
    /// </summary>
    public partial class ToolWindow : Window
    {
        /// <summary>
        /// List of controls. Used for enabling and dimming.
        /// </summary>
        private List<Control> Controls = new List<Control>();

        /// <summary>
        /// MainWindow reference injected from the MainWindow.
        /// </summary>
        private MainWindow MainWindow { get; set; }

        public ToolWindow(MainWindow mainWindow)
        {
            MainWindow = mainWindow;

            InitializeComponent();

            InitControls();
        }

        /// <summary>
        /// Initialize the list of controls.
        /// </summary>
        private void InitControls()
        {
            Controls.Add(BorderSize);
        }

        /// <summary>
        /// Resizing fetched from the MainWindow.
        /// </summary>
        private bool Resizing => MainWindow.IsResizing;

        public void Initialize()
        {
            BorderSize.Value = MainWindow.BorderSize;

            SetControls();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        /// <summary>
        /// The Resizing button was clicked. Toggle it on the MainWindow.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var resizing = MainWindow.ToggleResizing();

            SetControls();
        }

        /// <summary>
        /// Set various properties on all controls in the Controls list.
        /// </summary>
        private void SetControls()
        {
            Controls.ForEach(c =>
            {
                c.Opacity = Resizing ? 1 : 0.5;
                c.IsEnabled = Resizing;
            });
        }

        /// <summary>
        /// The Slider has a new value. Set it on the MainWindow.
        /// </summary>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(MainWindow != null)
                MainWindow.BorderSize = e.NewValue;
        }
    }
}
