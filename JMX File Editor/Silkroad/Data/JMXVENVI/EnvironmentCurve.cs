using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVENVI
{
    class EnvironmentCurve<TValue, TBlend> : ISerializableBS
        where TBlend : EnvironmentDefaultBlend<TValue>, new()
        where TValue : new()
    {
        #region Public Properties
        public List<TBlend> Blends { get; set; } = new List<TBlend>();
        #endregion

        #region Abstract Implementation
        public void Deserialize(BSReader reader)
        {
            var length = reader.ReadInt32();
            Blends.Capacity = length;
            for (var i = 0; i < length; i++)
            {
                var blend = reader.Deserialize<TBlend>();
                Blends.Add(blend);
            }
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(Blends.Count);
            foreach (var item in Blends)
                writer.Serialize(item);
        }
        #endregion
    }
}
