using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad
{
    public interface ISerializableWithParamBS<in TParam>
    {
        void Deserialize(BSReader reader, TParam param);

        void Serialize(BSWriter writer, TParam param);
    }
}