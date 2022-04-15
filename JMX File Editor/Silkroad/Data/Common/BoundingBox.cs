namespace JMXFileEditor.Silkroad.Data.Common
{
	public class BoundingBox
	{
		#region Public Properties
		public Vector3 Min { get; set; }
		public Vector3 Max { get; set; }
		#endregion

		#region Constructors
		public BoundingBox() { }
		public BoundingBox(Vector3 Min, Vector3 Max)
		{
			this.Min = Min;
			this.Max = Max;
		}
		#endregion
	}
}
