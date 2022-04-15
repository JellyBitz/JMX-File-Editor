using JMXFileEditor.Silkroad.Data.JMXVRES;
namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
	public class CPrimMeshVM : JMXStructure
	{
		#region Constructor
		public CPrimMeshVM(string Name, CPrimMesh Mesh) : base(Name, true)
		{
			Childs.Add(new JMXAttribute("Path", Mesh.Path));
			Childs.Add(new JMXAttribute("UnkUInt01", Mesh.UnkUInt01));
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			CPrimMesh obj = new CPrimMesh()
			{
				Path = (string)((JMXAttribute)Structure.Childs[0]).Value,
				UnkUInt01 = (uint)((JMXAttribute)Structure.Childs[1]).Value
			};
			return obj;
		}
		#endregion
	}
}
