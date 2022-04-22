namespace JMXFileEditor.Silkroad.Mathematics
{
    /// <summary>
    /// Vector as 4D space representation
    /// </summary>
    public class Vector4
    {
        #region Public Properties
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }
        #endregion Public Properties

        #region Constructor
        public Vector4()
        { }
        public Vector4(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }
        #endregion
    }
}