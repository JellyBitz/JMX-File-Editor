using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterFrameTextureSlide : EEConvertParameter<Vector3, List<Vector4>>
    {
        public override string Name => "FrameTextureSlide";

        public ParameterFrameTextureSlide()
        {
            this.Right = new List<Vector4>();
        }

        public override void Read(BSReader reader)
        {
            this.Left = reader.ReadVector3();

            var count = reader.ReadInt32();
            this.Right.Capacity = count;
            for (int i = 0; i < count; i++)
                this.Right.Add(reader.ReadVector4());
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