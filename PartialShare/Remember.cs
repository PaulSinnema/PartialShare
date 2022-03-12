using System.Windows;

namespace PartialShare
{
    internal class Remember
    {
        public double LastX { get; set; }
        public double LastY { get; set; }  
        public double LastWidth { get; set; }
        public double LastHeight { get; set; }
        public bool IsResizing { get; set; }
        public double BorderSize { get; set; }
        public WindowState ToolWindowState { get; set; }
    }
}
