using System;
using System.Windows;
using System.Windows.Interop;

namespace PartialShare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ToolWindow tw = new ToolWindow();
            tw.Show();

            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Make entire window and everything in it transparent to the Mouse

            // Make the button "visible" to the Mouse
            // var buttonHwndSource = (HwndSource)HwndSource.FromVisual(btn);
            // var buttonHwnd = buttonHwndSource.Handle;
            // WindowsServices.SetWindowExNotTransparent(buttonHwnd);
        }
    }
}
