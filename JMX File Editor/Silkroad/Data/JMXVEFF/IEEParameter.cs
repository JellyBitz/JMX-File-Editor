using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public interface IEEParameter
    {
        string Name { get; }

        void Read(BSReader reader);

        void Write(BSWriter writer);
    }
}