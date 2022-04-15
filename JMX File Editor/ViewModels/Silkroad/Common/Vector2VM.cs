using JMXFileEditor.Silkroad.Data.Common;
namespace JMXFileEditor.ViewModels.Silkroad.Common
{
	/// <summary>
	/// ViewModel representing Vector3
	/// </summary>
	public class Vector2VM : JMXStructure
	{
		#region Constructor
		public Vector2VM(string Name, Vector2 Vector) : base(Name, true)
		{
			Childs.Add(new JMXAttribute("X", Vector.X));
			Childs.Add(new JMXAttribute("Y", Vector.Y));
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			Vector2 vector = new Vector2
			{
				X = (float)((JMXAttribute)Structure.Childs[0]).Value,
				Y = (float)((JMXAttribute)Structure.Childs[1]).Value
			};
			return vector;
		}
		#endregion
	}
}
