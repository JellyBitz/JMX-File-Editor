using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System;
using System.Linq;

namespace JMXFileEditor.Silkroad.Data.JMXVENVI
{
    class EnvironmentColorBlend : EnvironmentDefaultBlend<Color3>
    {
        public override void Deserialize(BSReader reader)
        {
            Value = reader.ReadColor3();
            Time = reader.ReadFloat();
        }
        public override void Serialize(BSWriter writer)
        {
            writer.Write(Value);
            writer.Write(Time);
        }
    }
}
