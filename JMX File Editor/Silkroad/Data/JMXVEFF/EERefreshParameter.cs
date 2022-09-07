using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public abstract class EERefreshParameter<T> : IEEParameter
        where T : new()
    {
        public T Value { get; set; } = new T();

        public abstract void Deserialize(BSReader reader);

        public abstract void Serialize(BSWriter writer);

        public abstract string Name { get; }
    }
}