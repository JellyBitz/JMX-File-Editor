using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public class ModDataParticleConfig : ISerializableBS
    {
        public bool IsEnabled { get; set; }
        public string Path { get; set; } = string.Empty;
        public string BoneRelative { get; set; } = string.Empty;
        public Vector3 Position { get; set; } = new Vector3();
        public uint BirthTime { get; set; }
        public byte UnkByte01 { get; set; }
        public byte UnkByte02 { get; set; }
        public byte UnkByte03 { get; set; }
        public byte UnkByte04 { get; set; }
        public Vector3 UnkVector01 { get; set; } = new Vector3();

        public void Deserialize(BSReader reader)
        {
            this.IsEnabled = reader.ReadInt32() != 0;
            this.Path = reader.ReadString();
            this.BoneRelative = reader.ReadString();
            this.Position = reader.ReadVector3();
            this.BirthTime = reader.ReadUInt32();
            this.UnkByte01 = reader.ReadByte();
            this.UnkByte02 = reader.ReadByte();
            this.UnkByte03 = reader.ReadByte();
            this.UnkByte04 = reader.ReadByte();
            if (this.UnkByte04 == 1)
                this.UnkVector01 = reader.ReadVector3();
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write(this.IsEnabled ? 1u : 0u);
            writer.Write(this.Path);
            writer.Write(this.BoneRelative);
            writer.Write(this.Position);
            writer.Write(this.BirthTime);
            writer.Write(this.UnkByte01);
            writer.Write(this.UnkByte02);
            writer.Write(this.UnkByte03);
            writer.Write(this.UnkByte04);
            if (this.UnkByte04 == 1)
                writer.Write(this.UnkVector01);
        }
    }
}