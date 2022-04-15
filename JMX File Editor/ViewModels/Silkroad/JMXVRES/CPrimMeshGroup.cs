using JMXFileEditor.Silkroad.Data.JMXVRES;
namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
	public class CPrimMeshGroupVM : JMXStructure
	{
		#region Constructor
		public CPrimMeshGroupVM(string Name, CPrimMeshGroup MeshGroup) : base(Name, true)
		{
			Childs.Add(new JMXAttribute("Name", MeshGroup.Name));
			AddChildArray("MeshSet.Indexes", MeshGroup.MeshSetIndexes.ToArray(), true, true);
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			CPrimMeshGroup obj = new CPrimMeshGroup()
			{
				Name = (string)((JMXAttribute)Structure.Childs[0]).Value,
				MeshSetIndexes = ((JMXStructure)Structure.Childs[1]).GetChildList<uint>()
			};
			return obj;
		}
		#endregion
	}
}
