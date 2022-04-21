namespace JMXFileEditor.Silkroad.Mathematics
{
    /// <summary>
    /// Vector as 2D space representation
    /// </summary>
    public class Vector2
    {
        #region Public Properties
        public float X { get; set; }
        public float Y { get; set; }
        #endregion

        #region Constructors
        public Vector2() { }
        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        #endregion
    }
}
