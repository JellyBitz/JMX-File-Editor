using System.Collections.Generic;
namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
	public class CPrimMeshGroup
	{
		public string Name { get; set; } = string.Empty;
		public List<uint> MeshSetIndexes { get; set; } = new List<uint>();
	}
}
