using System.Windows;

namespace PartialShare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ToolWindow toolWindow = new ToolWindow();

            toolWindow.Show();

            InitializeComponent();
        }
    }
}
