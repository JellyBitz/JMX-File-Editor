using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.IO;

using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    [Serializable]
    public class EESourceNode
    {
        public bool HasData { get; private set; }
        public string Name { get; private set; }
        public SourceNodeType Type { get; private set; }
        public byte Byte1 { get; private set; }
        public float Start { get; private set; }
        public float End { get; private set; }
        public float Float2 { get; private set; }
        public IEEParameter Parameter { get; private set; }

        internal void Read(BSReader reader)
        {
            this.HasData = reader.ReadBoolean();
            if (!this.HasData)
                return;

            this.Name = reader.ReadString();
            this.Type = (SourceNodeType)reader.ReadByte();
            this.Byte1 = reader.ReadByte();

            this.Start = reader.ReadSingle();
            this.End = reader.ReadSingle();
            this.Float2 = reader.ReadSingle();

            this.Parameter = ParameterFactory.CreateParameterByCommandName(this.Name);
            this.Parameter?.Read(reader);
        }
    }
}