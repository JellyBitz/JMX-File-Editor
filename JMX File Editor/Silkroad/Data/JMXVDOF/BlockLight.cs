using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVDOF
{
    public class BlockLight : ISerializableBS
    {
        #region Public Properties
        public string Name { get; set; }
        public Vector3 Position { get; set; } = new Vector3();
        public Color3 Color01 { get; set; } = new Color3();
        public Color3 Color02 { get; set; } = new Color3();
        public Color3 Color03 { get; set; } = new Color3();
        public float UnkFloat01 { get; set; }
        public float UnkFloat02 { get; set; }
        public float UnkFloat03 { get; set; }
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            Name = reader.ReadString();

            Position = reader.ReadVector3();
            Color01 = reader.ReadColor3();
            Color02 = reader.ReadColor3();
            Color03 = reader.ReadColor3();

            UnkFloat01 = reader.ReadFloat();
            UnkFloat02 = reader.ReadFloat();
            UnkFloat03 = reader.ReadFloat();
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(Name);

            writer.Write(Position);
            writer.Write(Color01);
            writer.Write(Color02);
            writer.Write(Color03);

            writer.Write(UnkFloat01);
            writer.Write(UnkFloat02);
            writer.Write(UnkFloat03);
        }

        #endregion
    }
}