using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterAngleVector1 : EEConvertParameter<Vector3, Vector3>
    {
        public override string Name => "AngleVector1";

        public override void Convert() => Right = new Vector3(Left.X, Left.Y, Left.Z * ((float)Math.PI / 180f));

        public ParameterAngleVector1() { }
        public ParameterAngleVector1(Vector3 left, Vector3 right)
        {
            Left = left;
            Right = right;
        }
        public override void Deserialize(BSReader reader)
        {
            Left = reader.ReadVector3();
            Right = reader.ReadVector3();
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Write(Left);
            writer.Write(Right);
        }
    }
}