using JMXFileEditor.Silkroad.Mathematics;

using System;
using System.IO;
using System.Text;

using Rectangle = System.Drawing.Rectangle;
using RectangleF = JMXFileEditor.Silkroad.Mathematics.RectangleF;

namespace JMXFileEditor.Silkroad.IO
{
    /// <summary>
    /// Binary stream reader
    /// </summary>
    public class BSReader : BinaryReader
    {
        #region Public Members
        /// <summary>
        /// Encoding used to read strings
        /// </summary>
        public Encoding Encoding { get; }
        #endregion

        #region Constructor
        public BSReader(Stream input, Encoding encoding) : base(input, encoding)
        {
            Encoding = encoding;
        }
        public BSReader(Stream input) : base(input)
        {
            Encoding = Encoding.Default;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Skips bytes reading from current position
        /// </summary>
        public void SkipRead(long count)
        {
            base.BaseStream.Seek(count, SeekOrigin.Current);
        }
        /// <summary>
        /// Reads a string from the current stream. The string is prefixed with the length, encoded as an integer 32 bits at a time.
        /// </summary>
        public override string ReadString()
        {
            var length = ReadInt32();
            return ReadString(length);

        }
        public string[] ReadStringArray(int count)
        {
            string[] array = new string[count];
            for (int i = 0; i < count; i++)
                array[i] = ReadString();
            return array;
        }
        /// <summary>
        /// Reads a string from the current stream.
        /// </summary>
        public string ReadString(int length)
        {
            // negative or over 8K is probably a parsing error
            if (unchecked((uint)length) > 8192)
                throw new ArgumentOutOfRangeException(nameof(length));

            // Read it and apply decoding
            var bytes = base.ReadBytes(length);
            return Encoding.GetEncoding(Encoding.CodePage).GetString(bytes);
        }
        public float ReadFloat() => ReadSingle();
        public uint[] ReadUIntArray(int count)
        {
            uint[] array = new uint[count];
            for (int i = 0; i < count; i++)
                array[i] = ReadUInt32();
            return array;
        }
        public Vector2 ReadVector2() => new Vector2(ReadFloat(), ReadFloat());
        public Vector3 ReadVector3() => new Vector3(ReadFloat(), ReadFloat(), ReadFloat());
        public Vector4 ReadVector4() => new Vector4(ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat());
        public Quaternion ReadQuaternion() => new Quaternion(ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat());
        public Matrix4x4 ReadMatrix4x4() => new Matrix4x4
            (
                ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat(),
                ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat(),
                ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat(),
                ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat()
            );

        public RectangleF ReadRectangleF() => new RectangleF(ReadVector2(), ReadVector2());

        public Rectangle ReadRectangle() => new Rectangle(ReadInt32(), ReadInt32(), ReadInt32(), ReadInt32());

        public BoundingBoxF ReadBoundingBoxF() => new BoundingBoxF(ReadVector3(), ReadVector3());
        /// <summary>
        /// Format32bppArgb
        /// </summary>
        public Color32 ReadColor32()
        {
            var b = ReadByte();
            var g = ReadByte();
            var r = ReadByte();
            var a = ReadByte();
            return new Color32(r, g, b, a);
        }
        public Color3 ReadColor3() => new Color3(ReadFloat(), ReadFloat(), ReadFloat());
        public Color4 ReadColor4() => new Color4(ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat());
        public T Deserialize<T>()
            where T : ISerializableBS, new()
        {
            var serialized = new T();
            serialized.Deserialize(this);
            return serialized;
        }
        public T DeserializeParameterized<T>(params object[] parameters)
           where T : ISerializableParameterizedBS, new()
        {
            var serialized = new T();
            serialized.Deserialize(this, parameters);
            return serialized;
        }
        #endregion
    }
}