using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVDOF
{
    public class BlockObject : ISerializableBS
    {
        #region Public Properties
        public string Path { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Vector3 Position { get; set; } = new Vector3();
        public Vector3 Rotation { get; set; } = new Vector3();
        public Vector3 Scale { get; set; } = new Vector3();
        public uint Flag { get; set; }
        public uint UnkUInt01 { get; set; }
        public float RadiusSqrt { get; set; }
        public uint WaterColor { get; set; }
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            Path = reader.ReadString();
            Name = reader.ReadString();

            Position = reader.ReadVector3();
            Rotation = reader.ReadVector3();
            Scale = reader.ReadVector3();

            Flag = reader.ReadUInt32();
            UnkUInt01 = reader.ReadUInt32();
            RadiusSqrt = reader.ReadFloat();
            if ((Flag & 4) != 0)
                WaterColor = reader.ReadUInt32();
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(Path);
            writer.Write(Name);

            writer.Write(Position);
            writer.Write(Rotation);
            writer.Write(Scale);

            writer.Write(Flag);
            writer.Write(UnkUInt01);
            writer.Write(RadiusSqrt);
            if ((Flag & 4) != 0)
                writer.Write(WaterColor);
        }
        #endregion
    }
}