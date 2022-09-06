using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public abstract class EEProgramBase
    {
        //public EEGlobalData GlobalData { get; set; }

        public abstract void Read(BSReader reader);
    }
}