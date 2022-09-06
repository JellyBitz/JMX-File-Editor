using JMXFileEditor.Silkroad.IO;
using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Blends
{
    public abstract class DefaultBlend<TValue>
    {
        public float Time;
        public TValue Value;

        public abstract void Read(BSReader reader);
        public abstract void Write(BSWriter writer);
        public override string ToString() => $"{Time:0.00} - {Value}";
    }
}