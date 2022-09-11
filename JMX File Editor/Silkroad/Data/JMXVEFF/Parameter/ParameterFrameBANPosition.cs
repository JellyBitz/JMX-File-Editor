using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterFrameBANPosition : EEConvertParameter<float, List<Vector3>>
    {
        public override string Name => "FrameBANPosition";

        public ParameterFrameBANPosition() { }
        public ParameterFrameBANPosition(float left, List<Vector3> right)
        {
            Left = left;
            Right = right;
        }

        public override void Deserialize(BSReader reader)
        {
            Left = reader.ReadSingle();

            var count = reader.ReadInt32();
            Right.Capacity = count;
            for (var i = 0; i < count; i++)
                Right.Add(reader.ReadVector3());
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