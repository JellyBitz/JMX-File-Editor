using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public class PrimAnimationEvent : ISerializableBS
    {
        public int Time { get; set; }
        public int Type { get; set; }
        public int Param0 { get; set; }
        public int Param1 { get; set; }

        public void Deserialize(BSReader reader)
        {
            this.Time = reader.ReadInt32();
            this.Type = reader.ReadInt32();
            this.Param0 = reader.ReadInt32();
            this.Param1 = reader.ReadInt32();
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write(this.Time);
            writer.Write(this.Type);
            writer.Write(this.Param0);
            writer.Write(this.Param1);
        }
    }
}