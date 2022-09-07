using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public class EFMesh : ISerializableBS
    {
        public string Path { get; set; }
        public List<string> Textures { get; } = new List<string>();

        public void Deserialize(BSReader reader)
        {
            Path = reader.ReadString();
            var textureCount = reader.ReadInt32();
            for (var i = 0; i < textureCount; i++)
                Textures.Add(reader.ReadString());
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write(Path);
            writer.Write(Textures.Count);
            foreach (var texture in Textures)
                writer.Write(texture);
        }
    }
}