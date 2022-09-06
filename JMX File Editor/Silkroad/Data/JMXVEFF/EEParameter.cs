using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public abstract class EEParameter<T> : IEEParameter
    {
        public abstract string Name { get; }

        public T Value { get; protected set; }

        public abstract void Read(BSReader reader);

        public abstract void Write(BSWriter writer);
    }
}