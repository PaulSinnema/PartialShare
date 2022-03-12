namespace PartialShare
{
    internal class Remember
    {
        public static bool operator == (Remember? a, Remember? b)
        {
            return Compare(a, b);
        }

        public static bool operator !=(Remember? a, Remember? b)
        {
            return !Compare(a, b);
        }

        public override int GetHashCode()
        {
            return LastWidth.GetHashCode()
                + LastHeight.GetHashCode()
                + LastX.GetHashCode()
                + LastY.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        private static bool Compare(Remember? a, Remember? b)
        {
            if (a is null && b is null) return true;

            if (a is null) return false;
            if (b is null) return false;

            return (a.LastWidth == b.LastWidth
                    && a.LastHeight == b.LastHeight
                    && a.LastX == b.LastX
                    && a.LastY == b.LastY);
        }

        public double LastX { get; set; }
        public double LastY { get; set; }  
        public double LastWidth { get; set; }
        public double LastHeight { get; set; }
        public bool IsResizing { get; set; }
        public double BorderSize { get; set; }
    }
}
