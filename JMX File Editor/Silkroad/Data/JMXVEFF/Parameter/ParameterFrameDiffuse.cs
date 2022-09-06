using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterFrameDiffuse : EEParameter<List<Color32>>
    {
        public override string Name => "FrameDiffuse";

        public ParameterFrameDiffuse()
        {
            this.Value = new List<Color32>();
        }

        public override void Read(BSReader reader)
        {
            var count = reader.ReadInt32();

            this.Value.Capacity = count;
            for (int i = 0; i < count; i++)
                this.Value.Add(reader.ReadColor32());
        }

        public override void Write(BSWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}