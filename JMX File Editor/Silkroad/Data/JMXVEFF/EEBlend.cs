using System.Collections;
using System.Collections.Generic;

using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public class EEBlend<TValue, TBlend> : IEnumerable<TBlend>, ISerializableBS
        where TBlend : DefaultBlend<TValue>, new()
    {
        public List<TBlend> Points { get; private set; } = new List<TBlend>();

        public float Begin { get; set; }
        public float End { get; set; }
        public TBlend this[int index] => Points[index];

        public EEBlend() { }
        public EEBlend(List<TBlend> points)
        {
            Points = points;
        }

        public void Deserialize(BSReader reader)
        {
            Begin = reader.ReadSingle();
            End = reader.ReadSingle();

            var pointCount = reader.ReadInt32();
            Points = new List<TBlend>(pointCount);

            for (var i = 0; i < pointCount; i++)
            {
                var point = new TBlend();
                point.Read(reader);
                Points.Add(point);
            }
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(Begin);
            writer.Write(End);

            writer.Write(Points.Count);
            foreach (var point in Points)
                point.Write(writer);
        }

        public override string ToString() => $"{nameof(Begin)}:{Begin}; {nameof(End)}:{End}; {Points.Count} points";

        public IEnumerator<TBlend> GetEnumerator() => ((IEnumerable<TBlend>)Points).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Points).GetEnumerator();
    }
}