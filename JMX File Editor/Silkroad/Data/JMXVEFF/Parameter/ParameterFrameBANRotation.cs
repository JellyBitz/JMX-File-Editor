using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterFrameBANRotation : EEConvertParameter<float, List<Matrix4x4>>
    {
        public override string Name => "FrameBANRotation";

        public ParameterFrameBANRotation()
        {
            Right = new List<Matrix4x4>();
        }

        public override void Deserialize(BSReader reader)
        {
            Left = reader.ReadSingle();

            var count = reader.ReadInt32();
            Right.Capacity = count;
            for (var i = 0; i < count; i++)
                Right.Add(reader.ReadMatrix4x4());
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Write(Left);

            writer.Write(Right.Count);
            foreach (var right in Right)
                writer.Write(right);
        }

        public override void Convert()
        {
            throw new System.NotImplementedException();
        }
    }
}