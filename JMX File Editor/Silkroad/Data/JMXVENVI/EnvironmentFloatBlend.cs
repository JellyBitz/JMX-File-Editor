using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVENVI
{
    class EnvironmentFloatBlend : EnvironmentDefaultBlend<float>
    {
        #region Abstract Implementation
        public override void Deserialize(BSReader reader)
        {
            Value = reader.ReadFloat();
            Time = reader.ReadFloat();
        }
        public override void Serialize(BSWriter writer)
        {
            writer.Write(Value);
            writer.Write(Time);
        }
        #endregion
    }
}
