using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterFrameBANPosition : EEConvertParameter<float, List<Vector3>>
    {
        public override string Name => "FrameBANPosition";

        public ParameterFrameBANPosition()
        {
            this.Right = new List<Vector3>();
        }

        public override void Read(BSReader reader)
        {
            this.Left = reader.ReadSingle();

            var count = reader.ReadInt32();
            this.Right.Capacity = count;
            for (int i = 0; i < count; i++)
                this.Right.Add(reader.ReadVector3());
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