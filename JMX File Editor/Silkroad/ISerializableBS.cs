using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad
{
    public interface ISerializableBS
    {
        void Deserialize(BSReader reader);

        void Serialize(BSWriter writer);
    }
}