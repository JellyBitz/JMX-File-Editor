using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public abstract class EEProgramBase : ISerializableBS
    {
        //public EEGlobalData GlobalData { get; set; }

        public abstract void Deserialize(BSReader reader);
        public abstract void Serialize(BSWriter writer);
    }
}