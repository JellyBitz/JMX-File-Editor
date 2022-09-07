using JMXFileEditor.Silkroad.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JMXFileEditor.Silkroad.Data.JMXVENVI
{
    class EnvironmentNode : ISerializableBS
    {
        public string Name { get; set; }
        public short ProfileId { get; set; }
        public short Short0 { get; private set; }
        public int Int0 { get; set; }
        public int Int1 { get; set; }
        public List<EnvironmentNode> Children { get; set; } = new List<EnvironmentNode>();

        public void Deserialize(BSReader reader)
        {
            var childCount = reader.ReadInt32();

            Name = reader.ReadString();
            ProfileId = reader.ReadInt16();
            Short0 = reader.ReadInt16();
            Int0 = reader.ReadInt32();
            Int1 = reader.ReadInt32();

            for (var i = 0; i < childCount; i++)
                Children.Add(reader.Deserialize<EnvironmentNode>());
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write(Children.Count);

            writer.Write(Name);
            writer.Write(ProfileId);
            writer.Write(Short0);
            writer.Write(Int0);
            writer.Write(Int1);

            foreach (var item in Children)
                writer.Serialize(item);
        }
    }
}
