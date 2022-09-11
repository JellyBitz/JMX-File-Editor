using JMXFileEditor.Silkroad.IO;

using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Blends
{
    public abstract class DefaultBlend<TValue>
    {
        public float Time;
        public TValue Value;

        public virtual void Read(BSReader reader)
        {
            this.Time = reader.ReadFloat();
        }

        public virtual void Write(BSWriter writer)
        {
            writer.Write(this.Time);
        }
        public override string ToString() => $"{Time:0.00} - {Value}";
    }
}