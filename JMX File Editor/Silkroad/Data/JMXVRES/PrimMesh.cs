using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public class PrimMesh : ISerializableWithParamBS<int>
    {
        public string Path { get; set; } = string.Empty;
        public int Int0 { get; set; }

        public void Deserialize(BSReader reader, int param)
        {
            this.Path = reader.ReadString();
            if ((param & 1) != 0)
                this.Int0 = reader.ReadInt32();
        }

        public void Serialize(BSWriter writer, int param)
        {
            writer.Write(this.Path);
            if ((param & 1) != 0)
                writer.Write(this.Int0);
        }
    }
}