namespace JMXFileEditor.Silkroad.Data.Common
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
		#endregion

		#region Constructors
		public Vector3() { }
		public Vector3(float X, float Y, float Z)
		{
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}
		#endregion
	}
}
