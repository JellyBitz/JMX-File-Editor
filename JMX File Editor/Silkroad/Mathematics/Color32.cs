namespace JMXFileEditor.Silkroad.Mathematics
{
    public class Color32
    {
        #region Public Properties
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Alpha { get; set; } = 255;
        #endregion

        #region Constructor
        public Color32()
        {
        }
        public Color32(byte red, byte green, byte blue, byte alpha)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
            this.Alpha = alpha;
        }
        #endregion
    }
}