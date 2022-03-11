using System.Windows;
using System.Windows.Input;

namespace PartialShare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            Resizing = true;

            DimBorders(Resizing);

            ToolWindow toolWindow = new ToolWindow(this);

            toolWindow.Show();
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
                PartialShare.Opacity = 1;
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
