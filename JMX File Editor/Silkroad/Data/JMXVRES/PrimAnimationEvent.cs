using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public class PrimAnimationEvent : ISerializableBS
    {
        #region Public Properties
        public int Time { get; set; }
        public int Type { get; set; }
        public int Param01 { get; set; }
        public int Param02 { get; set; }
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            this.Time = reader.ReadInt32();
            this.Type = reader.ReadInt32();
            this.Param01 = reader.ReadInt32();
            this.Param02 = reader.ReadInt32();
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(this.Time);
            writer.Write(this.Type);
            writer.Write(this.Param01);
            writer.Write(this.Param02);
        }
        #endregion
    }
}