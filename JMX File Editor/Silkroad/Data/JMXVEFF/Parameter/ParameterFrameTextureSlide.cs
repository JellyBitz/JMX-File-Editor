using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterFrameTextureSlide : EEConvertParameter<Vector3, List<Vector4>>
    {
        public override string Name => "FrameTextureSlide";

        public override void Deserialize(BSReader reader)
        {
            Left = reader.ReadVector3();

            var count = reader.ReadInt32();
            Right.Capacity = count;
            for (var i = 0; i < count; i++)
                Right.Add(reader.ReadVector4());
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