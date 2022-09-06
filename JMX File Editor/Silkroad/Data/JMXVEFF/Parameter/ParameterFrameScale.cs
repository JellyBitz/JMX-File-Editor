using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterFrameScale : EERefreshParameter<List<Vector3>>
    {
        public override string Name => "FrameScale";

        public ParameterFrameScale()
        {
            this.Value = new List<Vector3>();
        }

        public override void Read(BSReader reader)
        {
            var count = reader.ReadInt32();

            Value.Capacity = count;
            for (int i = 0; i < count; i++)
                Value.Add(reader.ReadVector3());
        }

        public override void Write(BSWriter writer)
        {
            var vectorCount = Value.Count;

            writer.Write(vectorCount);
            for (int i = 0; i < vectorCount; i++)
                writer.Write(Value[i]);
        }
    }
}