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

        public override void Read(BSReader reader)
        {
            var sourceCount = reader.ReadInt32();
            for (int i = 0; i < sourceCount; i++)
            {
                var data = new EESourceNode();
                data.Read(reader);

                this.SourceData.Add(data);
            }
        }

        public IEnumerator<EESourceNode> GetEnumerator() => ((IEnumerable<EESourceNode>)SourceData).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)SourceData).GetEnumerator();
    }
}