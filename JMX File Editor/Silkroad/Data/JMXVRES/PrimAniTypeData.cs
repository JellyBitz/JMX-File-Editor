using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public class PrimAniTypeData : ISerializableBS
    {
        public PrimAnimationType Type { get; set; }
        public int PrimAnimationIndex { get; set; }
        public List<PrimAnimationEvent> Events { get; set; } = new List<PrimAnimationEvent>();
        public List<Vector2> WalkGraph { get; set; } = new List<Vector2>();
        public float WalkLength { get; set; }

        public void Deserialize(BSReader reader)
        {
            this.Type = (PrimAnimationType)reader.ReadInt32();
            this.PrimAnimationIndex = reader.ReadInt32();

            var eventCount = reader.ReadInt32();
            for (int i = 0; i < eventCount; i++)
                this.Events.Add(reader.Deserialize<PrimAnimationEvent>());

            var walkPointCount = reader.ReadInt32();
            this.WalkLength = reader.ReadFloat();
            for (int i = 0; i < walkPointCount; i++)
                this.WalkGraph.Add(reader.ReadVector2());
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write((int)this.Type);
            writer.Write(this.PrimAnimationIndex);
            writer.Write(this.Events.Count);
            foreach (var item in this.Events)
                writer.Serialize(item);

            writer.Write(this.WalkGraph.Count);
            writer.Write(this.WalkLength);
            foreach (var item in this.WalkGraph)
                writer.Write(item);
        }
    }
}