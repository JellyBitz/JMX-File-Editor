using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterRotVector : EEConvertParameter<Vector3, Matrix4x4>
    {
        public override string Name => "RotVector";

        public override void Convert()
        {
            Right = Matrix4x4.CreateRotationX(Left.X * (float)Math.PI / 180.0f)
                * Matrix4x4.CreateRotationY(Left.Y * (float)Math.PI / 180.0f)
                * Matrix4x4.CreateRotationZ(Left.Z * (float)Math.PI / 180.0f);
        }

        public override void Deserialize(BSReader reader)
        {
            Left = reader.ReadVector3();
            Right = reader.ReadMatrix4x4();
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Write(Left);
            writer.Write(Right);
        }
    }
}