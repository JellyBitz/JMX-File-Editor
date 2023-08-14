using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVENVI
{
    public abstract class EnvironmentDefaultBlend<TValue> : ISerializableBS
        where TValue : new()
    {
        #region Public Properties
        public TValue Value { get; set; } = new TValue();
        public float Time { get; set; }
        #endregion

        #region Public Methods
        public abstract void Deserialize(BSReader reader);
        public abstract void Serialize(BSWriter writer);
        #endregion
    }
}
