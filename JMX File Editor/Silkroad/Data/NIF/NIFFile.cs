using JMXFileEditor.Silkroad.IO;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JMXFileEditor.Silkroad.Data.NIF
{
    internal class NIFFile : IJMXFile
    {
        public string Format => "JMXNIF";
        public string Extension => "2dt";

        public List<NIFNode> Nodes { get; set; } = new List<NIFNode>();

        public void Load(Stream stream)
        {
            using (var reader = new BSReader(stream, Encoding.GetEncoding(949)))
            {
                var nodeCount = reader.ReadInt32();
                for (var i = 0; i < nodeCount; i++)
                    Nodes.Add(reader.Deserialize<NIFNode>());
            }
        }
        public void Save(string path)
        {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = new BSWriter(stream, Encoding.GetEncoding(949)))
            {
                writer.Write(Nodes.Count);
                foreach (var node in Nodes)
                    writer.Serialize(node);
            }
        }
    }
}
