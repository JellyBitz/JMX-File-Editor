using JMXFileEditor.Silkroad.Mathematics;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public class PrimAniGroup
    {
        public string Name { get; set; } = string.Empty;
        public List<PrimAniTypeData> Entries { get; set; } = new List<PrimAniTypeData>();

        public class PrimAniTypeData
        {
            public PrimAnimationType Type { get; set; }
            public int PrimAnimationIndex { get; set; }
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