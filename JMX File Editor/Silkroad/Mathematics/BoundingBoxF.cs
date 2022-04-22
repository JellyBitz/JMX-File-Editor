namespace JMXFileEditor.Silkroad.Mathematics
{
    public class BoundingBoxF
    {
        #region Public Properties
        public Vector3 Min { get; set; }
        public Vector3 Max { get; set; }
        #endregion Public Properties

        #region Constructors
        public BoundingBoxF()
        {
        }
        public BoundingBoxF(Vector3 min, Vector3 max)
        {
            this.Min = min;
            this.Max = max;
        }
        #endregion Constructors
    }
}