using System.Collections.Generic;
namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
	public class CResAttachable
	{
		public uint UnkUInt01 { get; set; }
		public uint UnkUInt02 { get; set; }
		public uint AttachMethod { get; set; }
		public List<Slot> Slots { get; set; } = new List<Slot>();
		public uint nComboNum { get; set; }
		public class Slot
		{
			public uint Index;
			public uint MeshSetIndex;
		}
	}
}
