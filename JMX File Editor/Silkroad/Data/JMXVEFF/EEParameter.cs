using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public abstract class EEParameter<T> : IEEParameter
        where T : new()
    {
        public abstract string Name { get; }

        public T Value { get; set; } = new T();

        public abstract void Deserialize(BSReader reader);

        public abstract void Serialize(BSWriter writer);
    }
}