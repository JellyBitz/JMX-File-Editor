namespace JMXFileEditor.Silkroad.Data.Common
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
		#endregion

		#region Constructors
		public Color4() { }
		public Color4(float Red, float Green, float Blue, float Alpha)
		{
			this.Red = Red;
			this.Green = Green;
			this.Blue = Blue;
			this.Alpha = Alpha;
		}
		#endregion
	}
}
