namespace JMXFileEditor.Silkroad.Mathematics
{
    /// <summary>
    /// Vector as 3D space representation
    /// </summary>
    public class Vector3
    {
        #region Public Properties
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        #endregion Public Properties

        #region Constructors
        public Vector3()
        { }
        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        #endregion Constructors
    }
}