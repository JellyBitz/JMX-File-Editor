using JMXFileEditor.Silkroad.Data.JMXVRES;
namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
	public class CPrimMeshGroupVM : JMXStructure
	{
		#region Constructor
		public CPrimMeshGroupVM(string Name, PrimMeshGroup MeshGroup) : base(Name, true)
		{
			Childs.Add(new JMXAttribute("Name", MeshGroup.Name));
			AddChildArray("MeshSet.Indexes", MeshGroup.MeshIndices.ToArray(), true, true);
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			PrimMeshGroup obj = new PrimMeshGroup()
			{
				Name = (string)((JMXAttribute)Structure.Childs[0]).Value,
				MeshIndices = ((JMXStructure)Structure.Childs[1]).GetChildList<int>()
			};
			return obj;
		}
		#endregion
	}
}
