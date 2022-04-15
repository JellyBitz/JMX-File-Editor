using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
	public class CModDataSet
	{
		public uint Type { get; set; }
		public CPrimAnimationType AnimationType { get; set; }
		public string Name { get; set; } = string.Empty;
		public List<CModData> ModsData { get; set; } = new List<CModData>();
	}
}
