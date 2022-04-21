using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public class PrimAnimation : ISerializableBS
    {
        public string Path { get; set; } = string.Empty;

        public void Deserialize(BSReader reader)
        {
            this.Path = reader.ReadString();
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write(this.Path);
        }
    }
}