using JMXFileEditor.Silkroad.IO;
using System;
using System.Linq;

namespace JMXFileEditor.Silkroad.Data.JMXVENVI
{
    class EnvironmentFloatBlend : EnvironmentDefaultBlend<float>
    {
        public override void Deserialize(BSReader reader)
        {
            Value = reader.ReadFloat();
            Time = reader.ReadFloat();
        }
        public override void Serialize(BSWriter writer)
        {
            writer.Write(Value);
            writer.Write(Time);
        }
    }
}
