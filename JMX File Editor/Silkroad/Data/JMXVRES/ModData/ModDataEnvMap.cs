using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public class ModDataEnvMap : IModData
    {
        public override ModDataType Type => ModDataType.ModDataEnvMap;

        public uint UnkUInt06 { get; set; }
        public uint UnkUInt07 { get; set; }
        public uint UnkUInt08 { get; set; }
        public uint UnkUInt09 { get; set; }

        public override void Deserialize(BSReader reader)
        {
            base.Deserialize(reader);
            this.UnkUInt06 = reader.ReadUInt32();
            this.UnkUInt07 = reader.ReadUInt32();
            this.UnkUInt08 = reader.ReadUInt32();
            this.UnkUInt09 = reader.ReadUInt32();
        }

        public override void Serialize(BSWriter writer)
        {
            base.Serialize(writer);
            writer.Write(this.UnkUInt06);
            writer.Write(this.UnkUInt07);
            writer.Write(this.UnkUInt08);
            writer.Write(this.UnkUInt09);
        }
    }
}