namespace JMXFileEditor.Silkroad.Data.Common
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
		public Vector2(float X, float Y)
		{
			this.X = X;
			this.Y = Y;
		}
		#endregion
	}
}
