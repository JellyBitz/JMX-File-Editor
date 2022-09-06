using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterBSAnimation : EEParameter<List<string>>
    {
        public override string Name => "BSAnimation";

        public ParameterBSAnimation()
        {
            this.Value = new List<string>();
        }

        public override void Read(BSReader reader)
        {
            int count = reader.ReadInt32();
            this.Value.Capacity = count;

            for (int i = 0; i < count; i++)
                this.Value.Add(reader.ReadString());
        }

        public override void Write(BSWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}