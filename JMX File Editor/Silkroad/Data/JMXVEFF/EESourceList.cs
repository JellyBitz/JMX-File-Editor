using JMXFileEditor.Silkroad.IO;

using System;
using System.Collections;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    [Serializable]
    public class EESourceList : EEProgramBase, IEnumerable<EESourceNode>
    {
        public List<EESourceNode> SourceData { get; set; } = new List<EESourceNode>();

        public override void Deserialize(BSReader reader)
        {
            var sourceCount = reader.ReadInt32();
            SourceData.Capacity = sourceCount;
            for (var i = 0; i < sourceCount; i++)
                SourceData.Add(reader.Deserialize<EESourceNode>());
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Write(SourceData.Count);
            foreach (var item in SourceData)
                writer.Serialize(item);
        }

        public IEnumerator<EESourceNode> GetEnumerator() => ((IEnumerable<EESourceNode>)SourceData).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)SourceData).GetEnumerator();
    }
}