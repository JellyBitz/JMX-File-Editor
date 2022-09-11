using JMXFileEditor.Silkroad.IO;
using System;
using System.Linq;

namespace JMXFileEditor.Silkroad.Data.JMXVENVI
{
    abstract class EnvironmentDefaultBlend<TValue> : ISerializableBS
        where TValue : new()
    {
        public TValue Value { get; set; } = new TValue();
        public float Time { get; set; }

        public abstract void Deserialize(BSReader reader);
        public abstract void Serialize(BSWriter writer);
    }
}
