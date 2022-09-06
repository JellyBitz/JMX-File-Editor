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
            this.Right = new List<Matrix4x4>();
        }

        public override void Read(BSReader reader)
        {
            this.Left = reader.ReadSingle();

            var count = reader.ReadInt32();
            this.Right.Capacity = count;
            for (int i = 0; i < count; i++)
                this.Right.Add(reader.ReadMatrix4x4());
        }

        public override void Write(BSWriter writer)
        {
            throw new System.NotImplementedException();
        }

        public override void Convert()
        {
            throw new System.NotImplementedException();
        }
    }
}