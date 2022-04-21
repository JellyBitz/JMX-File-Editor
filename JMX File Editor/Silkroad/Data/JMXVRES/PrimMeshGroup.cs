using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public class PrimMeshGroup : ISerializableBS
    {
        public string Name { get; set; } = string.Empty;
        public List<int> MeshIndices { get; set; } = new List<int>();

        public void Deserialize(BSReader reader)
        {
            this.Name = reader.ReadString();
            var meshCount = reader.ReadInt32();
            for (int i = 0; i < meshCount; i++)
                this.MeshIndices.Add(reader.ReadInt32());
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write(this.Name);
            writer.Write(this.MeshIndices.Count);
            foreach (var item in this.MeshIndices)
                writer.Write(item);
        }
    }
}