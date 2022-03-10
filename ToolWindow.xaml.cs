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
            MainWindow = mainWindow;

            InitializeComponent();
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
            MainWindow.ToggleResizing();
        }
    }
}
