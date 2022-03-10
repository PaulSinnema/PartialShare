using System.Windows;
using System.Windows.Input;

namespace PartialShare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ToolWindow toolWindow = new ToolWindow(this);

            toolWindow.Show();

            InitializeComponent();
        }

        public void ToggleResizing()
        {
            Resizing = !Resizing;

            DimBorders(Resizing);
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
                BorderThickness = new Thickness(40);
            }
            else
            {
                PartialShare.Opacity = 0;
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
