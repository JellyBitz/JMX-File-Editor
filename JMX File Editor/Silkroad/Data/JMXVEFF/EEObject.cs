using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public abstract class EEObject
    {
        public virtual void Read(BSReader reader)
        {
        }

        public virtual void Write(BSWriter writer)
        {

        }
    }
}