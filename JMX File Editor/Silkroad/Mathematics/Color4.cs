namespace JMXFileEditor.Silkroad.Mathematics
{
    /// <summary>
    /// Color representation
    /// </summary>
    public class Color4
    {
        #region Public Properties
        public float Red { get; set; }
        public float Green { get; set; }
        public float Blue { get; set; }
        public float Alpha { get; set; }
        #endregion Public Properties

        #region Constructors
        public Color4()
        {
        }
        public Color4(float red, float green, float blue, float alpha)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
            this.Alpha = alpha;
        }
        #endregion Constructors
    }
}