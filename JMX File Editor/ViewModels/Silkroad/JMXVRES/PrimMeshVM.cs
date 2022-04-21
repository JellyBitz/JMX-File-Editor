using JMXFileEditor.Silkroad.Data.JMXVRES;
namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
	public class PrimMeshVM : JMXStructure
	{
		#region Constructor
		public PrimMeshVM(string Name, PrimMesh Mesh) : base(Name, true)
		{
			Childs.Add(new JMXAttribute("Path", Mesh.Path));
			Childs.Add(new JMXAttribute("UnkUInt01", Mesh.Int0));
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			PrimMesh obj = new PrimMesh()
			{
				Path = (string)((JMXAttribute)Structure.Childs[0]).Value,
				Int0 = (int)((JMXAttribute)Structure.Childs[1]).Value
			};
			return obj;
		}
		#endregion
	}
}
