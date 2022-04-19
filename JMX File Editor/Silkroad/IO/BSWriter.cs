using JMXFileEditor.Silkroad.Mathematics;

using System;
using System.IO;
using System.Text;

namespace JMXFileEditor.Silkroad.IO
{
    public class BSWriter : BinaryWriter
    {
        public BSWriter(Stream output) : base(output, Encoding.GetEncoding("EUC-KR"))
        {
        }

        public override void Write(string value)
        {
            this.Write(value.Length);
            this.Write(value, value.Length);
        }

        public void Write(string value, int length)
        {
            if (unchecked((uint)length) > 8192)
                throw new ArgumentOutOfRangeException(nameof(length));

            var buffer = value.ToCharArray();

            // Make sure the buffer is cut of or expanded depending on length.
            // Array.Resize is definitly not the best solution but it's the easiest and works both ways.
            Array.Resize(ref buffer, length);

            this.Write(buffer);
        }

        public void Serialize<T>(T value)
        where T : ISerializableBS
        {
            value.Serialize(this);
        }

        public void Write(Vector2 value)
        {
            this.Write(value.X);
            this.Write(value.Y);
        }

        public void Write(Vector3 value)
        {
            this.Write(value.X);
            this.Write(value.Y);
            this.Write(value.Z);
        }

        public void Write(Vector4 value)
        {
            this.Write(value.X);
            this.Write(value.Y);
            this.Write(value.Z);
            this.Write(value.W);
        }

        public void Write(Quaternion value)
        {
            this.Write(value.X);
            this.Write(value.Y);
            this.Write(value.Z);
            this.Write(value.W);
        }

        public void Write(Matrix4x4 value)
        {
            this.Write(value.M11);
            this.Write(value.M12);
            this.Write(value.M13);
            this.Write(value.M14);

            this.Write(value.M21);
            this.Write(value.M22);
            this.Write(value.M23);
            this.Write(value.M24);

            this.Write(value.M31);
            this.Write(value.M32);
            this.Write(value.M33);
            this.Write(value.M34);

            this.Write(value.M41);
            this.Write(value.M42);
            this.Write(value.M43);
            this.Write(value.M44);
        }

        public void Write(RectangleF value)
        {
            this.Write(value.Min);
            this.Write(value.Max);
        }

        public void Write(BoundingBoxF value)
        {
            this.Write(value.Min);
            this.Write(value.Max);
        }

        public void Write(Color32 value)
        {
            // Format32bppArgb
            this.Write(value.Blue);
            this.Write(value.Green);
            this.Write(value.Red);
            this.Write(value.Alpha);
        }

        public void Write(Color3 value)
        {
            this.Write(value.Red);
            this.Write(value.Green);
            this.Write(value.Blue);
        }

        public void Write(Color4 value)
        {
            this.Write(value.Red);
            this.Write(value.Green);
            this.Write(value.Blue);
            this.Write(value.Alpha);
        }
    }
}