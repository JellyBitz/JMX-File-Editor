using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public abstract class EEConvertParameter<T1, T2> : IEEParameter
    {
        public abstract string Name { get; }

        public T1 Left { get; protected set; }
        public T2 Right { get; protected set; }

        public abstract void Read(BSReader reader);

        public abstract void Write(BSWriter writer);

        public abstract void Convert();
    }
}