using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterAxisVector4 : EEConvertParameter<Vector4, Matrix4x4>
    {
        public override string Name => "AxisVector4";

        public ParameterAxisVector4() { }
        public ParameterAxisVector4(Vector4 left, Matrix4x4 right)
        {
            Left = left;
            Right = right;
        }

        public override void Convert()
        {
            var axis = new Vector3(this.Left.X, this.Left.Y, this.Left.Z);
            var angle = this.Left.W * (float)Math.PI / 180.0f;

            this.Right = Matrix4x4.CreateFromAxisAngle(axis, angle);
        }

        public override void Deserialize(BSReader reader)
        {
            Left = reader.ReadVector4();
            Right = reader.ReadMatrix4x4();
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Write(Left);
            writer.Write(Right);
        }
    }
}