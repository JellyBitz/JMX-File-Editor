using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public class EFMesh
    {
        public string Path { get; set; }
        public List<string> Textures { get; } = new List<string>();

        public void Read(BSReader reader)
        {
            Path = reader.ReadString();
            var textureCount = reader.ReadInt32();
            for (int i = 0; i < textureCount; i++)
            {
                var texturePath = reader.ReadString();
                Textures.Add(texturePath);
            }
        }
    }
}