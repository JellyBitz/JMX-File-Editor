using JMXFileEditor.Silkroad.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JMXFileEditor.Silkroad.Data.JMXVENVI
{
    class EnvironmentCurve<TValue, TBlend> : ISerializableBS
        where TBlend : EnvironmentDefaultBlend<TValue>, new()
        where TValue : new()
    {
        public List<TBlend> Blends { get; set; } = new List<TBlend>();

        public void Deserialize(BSReader reader)
        {
            var length = reader.ReadInt32();
            Blends.Capacity = length;
            for (var i = 0; i < length; i++)
            {
                var blend = reader.Deserialize<TBlend>();
                Blends.Add(blend);
            }
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(Blends.Count);
            foreach (var item in Blends)
                writer.Serialize(item);
        }
    }
}
