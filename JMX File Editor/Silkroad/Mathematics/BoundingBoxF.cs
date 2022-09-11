namespace JMXFileEditor.Silkroad.Mathematics
{
    public class BoundingBoxF
    {
        #region Public Properties
        public Vector3 Min { get; set; } = new Vector3();
        public Vector3 Max { get; set; } = new Vector3();
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