using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public abstract class EERefreshParameter<T> : IEEParameter
    {
        public T Value { get; set; }

        public abstract void Read(BSReader reader);

        public abstract void Write(BSWriter writer);

        public abstract string Name { get; }
    }
}