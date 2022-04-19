using JMXFileEditor.Silkroad.Mathematics;

using System;
using System.IO;
using System.Text;

namespace JMXFileEditor.Silkroad.IO
{
    public class BSReader : BinaryReader
    {
        public BSReader(Stream input) : base(input, Encoding.GetEncoding("EUC-KR"))
        {
        }

        public override string ReadString()
        {
            var length = this.ReadInt32();
            return this.ReadString(length);
        }

        public string ReadString(int length)
        {
            var buffer = base.ReadChars(length);
            if ((uint)length > 8192) // negative or over 8K is probably a parsing error
                throw new ArgumentOutOfRangeException(nameof(length));

            // Sometimes fixed strings contain trailing data.
            var terminatorOffset = length;
            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == '\0')
                {
                    terminatorOffset = i;
                    break;
                }
            }
            return new string(buffer, 0, terminatorOffset);
        }

        public T Deserialize<T>()
            where T : ISerializableBS, new()
        {
            var serialized = new T();
            serialized.Deserialize(this);
            return serialized;
        }

        public float ReadFloat() => this.ReadSingle();

        public Vector2 ReadVector2() => new Vector2(this.ReadFloat(), this.ReadFloat());

        public Vector3 ReadVector3() => new Vector3(this.ReadFloat(), this.ReadFloat(), this.ReadFloat());

        public Vector4 ReadVector4() => new Vector4(this.ReadFloat(), this.ReadFloat(), this.ReadFloat(), this.ReadFloat());

        public Quaternion ReadQuaternion() => new Quaternion(this.ReadFloat(), this.ReadFloat(), this.ReadFloat(), this.ReadFloat());

        public Matrix4x4 ReadMatrix4x4() => new Matrix4x4
            (
                this.ReadFloat(), this.ReadFloat(), this.ReadFloat(), this.ReadFloat(),
                this.ReadFloat(), this.ReadFloat(), this.ReadFloat(), this.ReadFloat(),
                this.ReadFloat(), this.ReadFloat(), this.ReadFloat(), this.ReadFloat(),
                this.ReadFloat(), this.ReadFloat(), this.ReadFloat(), this.ReadFloat()
            );

        public RectangleF ReadRectangleF() => new RectangleF(this.ReadVector2(), this.ReadVector2());

        public BoundingBoxF ReadBoundingBoxF() => new BoundingBoxF(this.ReadVector3(), this.ReadVector3());

        public Color32 ReadColor32()
        {
            // Format32bppArgb
            var b = this.ReadByte();
            var g = this.ReadByte();
            var r = this.ReadByte();
            var a = this.ReadByte();

            return new Color32(r, g, b, a);
        }

        public Color3 ReadColor3() => new Color3(this.ReadFloat(), this.ReadFloat(), this.ReadFloat());

        public Color4 ReadColor4() => new Color4(this.ReadFloat(), this.ReadFloat(), this.ReadFloat(), this.ReadFloat());
    }
}