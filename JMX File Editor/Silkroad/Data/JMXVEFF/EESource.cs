using JMXFileEditor.Silkroad.IO;

using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    [Serializable]
    public class EESource : EEProgramBase
    {
        public EESourceNode Data { get; set; } = new EESourceNode();

        public override void Deserialize(BSReader reader) => Data.Deserialize(reader);

        public override void Serialize(BSWriter writer) => writer.Serialize(Data);
    }
}