using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterFrameScale : EERefreshParameter<List<Vector3>>
    {
        public override string Name => "FrameScale";

        public override void Deserialize(BSReader reader)
        {
            var count = reader.ReadInt32();

            Value.Capacity = count;
            for (var i = 0; i < count; i++)
                Value.Add(reader.ReadVector3());
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Write(Value.Count);
            foreach (var item in Value)
                writer.Write(item);
        }
    }
}