using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.Common
{
    /// <summary>
    /// ViewModel representing Vector3
    /// </summary>
    public class Vector3VM : JMXStructure
	{
		#region Constructor
		public Vector3VM(string Name, Vector3 Vector) : base(Name, true)
		{
			Childs.Add(new JMXAttribute("X", Vector.X));
			Childs.Add(new JMXAttribute("Y", Vector.Y));
			Childs.Add(new JMXAttribute("Z", Vector.Z));
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			Vector3 vector = new Vector3
			{
				X = (float)((JMXAttribute)Structure.Childs[0]).Value,
				Y = (float)((JMXAttribute)Structure.Childs[1]).Value,
				Z = (float)((JMXAttribute)Structure.Childs[2]).Value
			};
			return vector;
		}
		#endregion
	}
}
