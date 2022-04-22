using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad
{
    public interface ISerializableParameterizedBS
    {
        void Deserialize(BSReader reader, params object[] param);
        void Serialize(BSWriter writer, params object[] param);
    }
}