using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.IO;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public class EEBlend<TValue, TBlend> : IEnumerable<TBlend>, ISerializableBS
        where TBlend : DefaultBlend<TValue>, new()
    {
        public float Begin { get; set; }
        public float End { get; set; }
        private List<TBlend> _points;

        public int Count => _points.Count;
        public TBlend this[int index] => _points[index];

        public void Deserialize(BSReader reader)
        {
            Begin = reader.ReadSingle();
            End = reader.ReadSingle();

            var pointCount = reader.ReadInt32();
            _points = new List<TBlend>(pointCount);

            for (var i = 0; i < pointCount; i++)
            {
                var point = new TBlend();
                point.Read(reader);
                _points.Add(point);
            }
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(Begin);
            writer.Write(End);

            writer.Write(_points.Count);
            foreach (var point in _points)
                point.Write(writer);
        }

        public override string ToString() => $"{nameof(Begin)}:{Begin}; {nameof(End)}:{End}; {Count} points";

        public IEnumerator<TBlend> GetEnumerator() => ((IEnumerable<TBlend>)_points).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_points).GetEnumerator();
    }
}