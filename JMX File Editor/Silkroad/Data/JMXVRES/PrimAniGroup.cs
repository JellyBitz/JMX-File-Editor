using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public class PrimAniGroup : ISerializableBS
    {
        #region Public Properties
        public string Name { get; set; } = string.Empty;
        public List<PrimAniTypeData> AniTypeDataList { get; set; } = new List<PrimAniTypeData>();
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            this.Name = reader.ReadString();
            var aniTypeDataCount = reader.ReadInt32();
            for (int i = 0; i < aniTypeDataCount; i++)
                this.AniTypeDataList.Add(reader.Deserialize<PrimAniTypeData>());
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(this.Name);
            writer.Write(this.AniTypeDataList.Count);
            foreach (var item in this.AniTypeDataList)
                writer.Serialize(item);
        }
        #endregion
    }
}