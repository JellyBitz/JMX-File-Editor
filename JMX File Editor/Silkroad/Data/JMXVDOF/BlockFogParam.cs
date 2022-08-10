using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVDOF
{
    public class BlockFogParam : ISerializableBS
    {
        #region Public Properties
        public uint Color { get; set; }
        public float NearPlane { get; set; }
        public float FarPlane { get; set; }
        public float Intensity { get; set; }
        public byte HasHeightFog { get; set; }
        public float UnkFloat01 { get; set; }
        public float UnkFloat02 { get; set; }
        public float UnkFloat03 { get; set; }
        public float UnkFloat04 { get; set; }
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            Color = reader.ReadUInt32();
            NearPlane = reader.ReadFloat();
            FarPlane = reader.ReadFloat();
            Intensity = reader.ReadFloat();

            HasHeightFog = reader.ReadByte();
            if (HasHeightFog == 1)
            {
                UnkFloat01 = reader.ReadFloat();
                UnkFloat02 = reader.ReadFloat();
                UnkFloat03 = reader.ReadFloat();
                UnkFloat04 = reader.ReadFloat();
            }
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(Color);
            writer.Write(NearPlane);
            writer.Write(FarPlane);
            writer.Write(Intensity);

            writer.Write(HasHeightFog);
            if (HasHeightFog == 1)
            {
                writer.Write(UnkFloat01);
                writer.Write(UnkFloat02);
                writer.Write(UnkFloat03);
                writer.Write(UnkFloat04);
            }
        }
        #endregion
    }
}