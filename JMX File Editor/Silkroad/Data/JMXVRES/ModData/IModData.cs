using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public abstract class IModData : ISerializableBS
    {

        public abstract ModDataType Type { get; }

        public float UnkFloat01 { get; set; }
        public uint UnkUInt01 { get; set; }
        public uint UnkUInt02 { get; set; }
        public uint UnkUInt03 { get; set; }
        public uint UnkUInt04 { get; set; }
        public uint UnkUInt05 { get; set; }
        public byte UnkByte01 { get; set; }
        public byte UnkByte02 { get; set; }
        public byte UnkByte03 { get; set; }
        public byte UnkByte04 { get; set; }

        public virtual void Deserialize(BSReader reader)
        {
            this.UnkFloat01 = reader.ReadSingle(); // 0.5
            this.UnkUInt01 = reader.ReadUInt32(); // 2 (ModDataMtrl)
            this.UnkUInt02 = reader.ReadUInt32(); // 272 (ModDataMtrl)
            this.UnkUInt03 = reader.ReadUInt32(); // 7 (ModDataMtrl)
            this.UnkUInt04 = reader.ReadUInt32();
            this.UnkUInt05 = reader.ReadUInt32();
            this.UnkByte01 = reader.ReadByte();
            this.UnkByte02 = reader.ReadByte();
            this.UnkByte03 = reader.ReadByte();
            this.UnkByte04 = reader.ReadByte();
        }

        public virtual void Serialize(BSWriter writer)
        {
            writer.Write(this.UnkFloat01);
            writer.Write(this.UnkUInt01);
            writer.Write(this.UnkUInt02);
            writer.Write(this.UnkUInt03);
            writer.Write(this.UnkUInt04);
            writer.Write(this.UnkUInt05);
            writer.Write(this.UnkByte01);
            writer.Write(this.UnkByte02);
            writer.Write(this.UnkByte03);
            writer.Write(this.UnkByte04);
        }
    }
}