namespace JMXFileEditor.Silkroad.Mathematics
{
    /// <summary>
    /// Represents a 2D rectangle
    /// </summary>
    public class RectangleF
    {
        #region Public Properties
        public Vector2 Min { get; set; }
        public Vector2 Max { get; set; }
        #endregion

        #region Constructor
        public RectangleF()
        {
        }
        public RectangleF(Vector2 min, Vector2 max)
        {
            this.Min = min;
            this.Max = max;
        }
        #endregion
    }
}