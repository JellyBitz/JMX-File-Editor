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
            this.Right = Matrix4x4.CreateRotationX(this.Left.X * (float)Math.PI / 180.0f)
                         * Matrix4x4.CreateRotationY(this.Left.Y * (float)Math.PI / 180.0f)
                         * Matrix4x4.CreateRotationZ(this.Left.Z * (float)Math.PI / 180.0f);

            // TODO: Figure out if this works too:
            //       this.Right = Matrix4x4.CreateFromYawPitchRoll(this.Left.Y, this.Left.X, this.Left.Z);
        }

        public override void Read(BSReader reader)
        {
            Left = reader.ReadVector3();
            Right = reader.ReadMatrix4x4();
        }

        public override void Write(BSWriter writer)
        {
            writer.Write(Left);
            writer.Write(Right);
        }
    }
}