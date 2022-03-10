using System;
using System.Windows;

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
        }   

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
            BorderSize.IsEnabled = MainWindow.ToggleResizing();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(MainWindow != null)
                MainWindow.BorderSize = e.NewValue;
        }
    }
}
