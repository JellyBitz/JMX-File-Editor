using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.IO;

using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    [Serializable]
    public class EESourceNode : ISerializableBS
    {
        public bool HasData { get; set; }
        public string Name { get; set; }
        public SourceNodeType Type { get; set; }
        public byte Byte1 { get; set; }
        public float Start { get; set; }
        public float End { get; set; }
        public float Float2 { get; set; } // Length?
        public IEEParameter Parameter { get; set; }

        public void Deserialize(BSReader reader)
        {
            HasData = reader.ReadBoolean();
            if (!HasData)
                return;

            Name = reader.ReadString();
            Type = (SourceNodeType)reader.ReadByte();
            Byte1 = reader.ReadByte();

            Start = reader.ReadSingle();
            End = reader.ReadSingle();
            Float2 = reader.ReadSingle();

            Parameter = ParameterFactory.CreateParameterByCommandName(Name);
            Parameter?.Deserialize(reader);
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write(HasData);
            if (!HasData)
                return;

            writer.Write(Name);
            writer.Write((byte)Type);
            writer.Write(Byte1);

            writer.Write(Start);
            writer.Write(End);
            writer.Write(Float2);

            if (Parameter == null)
                return;

            writer.Serialize(Parameter);
        }
    }
}