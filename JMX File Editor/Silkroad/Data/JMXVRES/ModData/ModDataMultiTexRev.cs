using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public class ModDataMultiTexRev : IModData
    {
        public override ModDataType Type => ModDataType.ModDataMultiTexRev;

        public uint UnkUInt06 { get; set; }
        public string Path { get; set; } = string.Empty;
        public uint UnkUInt07 { get; set; }

        public override void Deserialize(BSReader reader)
        {
            base.Deserialize(reader);
            this.UnkUInt06 = reader.ReadUInt32();
            this.Path = reader.ReadString();
            this.UnkUInt07 = reader.ReadUInt32();
        }

        public override void Serialize(BSWriter writer)
        {
            base.Serialize(writer);
            writer.Write(this.UnkUInt06);
            writer.Write(this.Path);
            writer.Write(this.UnkUInt07);
        }
    }
}