using JMXFileEditor.Silkroad.Data.Common;
using System.Collections.Generic;
namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
	public class CPrimAniGroup
	{
		public string Name { get; set; } = string.Empty;
		public List<Entry> Entries { get; set; } = new List<Entry>();
		public class Entry
		{
			public CPrimAnimationType Type { get; set; }
			public uint AnimationSetIndex { get; set; }
			public List<Event> Events { get; set; } = new List<Event>();
			public List<Vector2> WalkGraph { get; set; } = new List<Vector2>();
			public float WalkLength { get; set; }

			public class Event
			{
				public uint Time { get; set; }
				public uint Type { get; set; }
				public uint UnkUInt01 { get; set; }
				public uint UnkUInt02 { get; set; }
			}
		}
	}
}
