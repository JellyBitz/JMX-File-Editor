using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public abstract class EEObject : ISerializableBS
    {
        public virtual void Deserialize(BSReader reader)
        {
        }

        public virtual void Serialize(BSWriter writer)
        {

        }
    }
}