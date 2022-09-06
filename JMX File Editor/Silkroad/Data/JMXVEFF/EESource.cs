using JMXFileEditor.Silkroad.IO;

using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    [Serializable]
    public class EESource : EEProgramBase
    {
        public EESourceNode Data { get; } = new EESourceNode();

        public override void Read(BSReader reader)
        {
            this.Data.Read(reader);
        }
    }
}