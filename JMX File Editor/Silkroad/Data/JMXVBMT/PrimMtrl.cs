using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVBMT
{
    public class PrimMtrl : ISerializableBS
    {
        public string Name { get; set; } = string.Empty;
        public Color4 Diffuse { get; set; } = new Color4();
        public Color4 Ambient { get; set; } = new Color4();
        public Color4 Specular { get; set; } = new Color4();
        public Color4 Emissive { get; set; } = new Color4();
        public float UnkFloat01 { get; set; }
        public uint Flags { get; set; }
        public string DiffuseMapPath { get; set; } = string.Empty;
        public float UnkFloat02 { get; set; }
        public byte UnkByte01 { get; set; }
        public byte UnkByte02 { get; set; }
        public bool IsAbsolutePath { get; set; }
        public string NormalMapPath { get; set; } = string.Empty;
        public uint UnkUInt01 { get; set; }

        public void Deserialize(BSReader reader)
        {
            this.Name = reader.ReadString();
            this.Diffuse = reader.ReadColor4();
            this.Ambient = reader.ReadColor4();
            this.Specular = reader.ReadColor4();
            this.Emissive = reader.ReadColor4();
            this.UnkFloat01 = reader.ReadSingle();
            this.Flags = reader.ReadUInt32(); // MaterialEntryFlags (64 is default often used with 256 and/or 512 only a few exceptions have 1 2 4 8...)
            this.DiffuseMapPath = reader.ReadString();
            this.UnkFloat02 = reader.ReadSingle();
            this.UnkByte01 = reader.ReadByte();
            this.UnkByte02 = reader.ReadByte();
            this.IsAbsolutePath = reader.ReadBoolean();
            if ((this.Flags & (uint)PrimMtrlFlag.Bit14) != 0)
            {
                this.NormalMapPath = reader.ReadString();
                this.UnkUInt01 = reader.ReadUInt32();
            }
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write(this.Name);
            writer.Write(this.Diffuse);
            writer.Write(this.Ambient);
            writer.Write(this.Specular);
            writer.Write(this.Emissive);
            writer.Write(this.UnkFloat01);
            writer.Write(this.Flags);
            writer.Write(this.DiffuseMapPath);
            writer.Write(this.UnkFloat02);
            writer.Write(this.UnkByte01);
            writer.Write(this.UnkByte02);
            writer.Write(this.IsAbsolutePath);

            if ((this.Flags & (uint)PrimMtrlFlag.Bit14) != 0)
            {
                writer.Write(this.NormalMapPath);
                writer.Write(this.UnkUInt01);
            }
        }
    }
}