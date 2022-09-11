using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterFrameDiffuse : EEParameter<List<Color32>>
    {
        public override string Name => "FrameDiffuse";

        public override void Deserialize(BSReader reader)
        {
            var count = reader.ReadInt32();

            Value.Capacity = count;
            for (var i = 0; i < count; i++)
                Value.Add(reader.ReadColor32());
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Write(Value.Count);
            foreach (var value in Value)
                writer.Write(value);
        }
    }
}