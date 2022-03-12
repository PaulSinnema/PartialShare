using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PartialShare
{
    /// <summary>
    /// Interaction logic for ToolWindow.xaml
    /// </summary>
    public partial class ToolWindow : Window
    {
        public ToolWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            MainWindow = mainWindow;

            BorderSize.Value = MainWindow.BorderSize;

            Controls.Add(BorderSize);
        }

        private List<Control> Controls = new List<Control>();

        private MainWindow MainWindow { get; set; }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var resizing = MainWindow.ToggleResizing();

            BorderSize.IsEnabled = resizing;
            DimControls(resizing);
        }

        private void DimControls(bool resizing)
        {
            Controls.ForEach(c => c.Opacity = resizing ? 1 : 0.5);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(MainWindow != null)
                MainWindow.BorderSize = e.NewValue;
        }
    }
}
