using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public abstract class EEConvertParameter<TLeft, TRight> : IEEParameter
        where TLeft : new()
        where TRight : new()
    {
        public abstract string Name { get; }

        public TLeft Left { get; protected set; } = new TLeft();
        public TRight Right { get; protected set; } = new TRight();

        public abstract void Deserialize(BSReader reader);

        public abstract void Serialize(BSWriter writer);

        public abstract void Convert();
    }
}