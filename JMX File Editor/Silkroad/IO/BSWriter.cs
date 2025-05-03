using JMXFileEditor.Silkroad.Mathematics;
using System;
using System.IO;
using System.Text;
using Rectangle = System.Drawing.Rectangle;

namespace JMXFileEditor.Silkroad.IO
{
    /// <summary>
    /// Binary stream writer
    /// </summary>
    public class BSWriter : BinaryWriter
    {
        #region Public Members
        /// <summary>
        /// Encoding used to write strings
        /// </summary>
        public Encoding Encoding { get; }
        #endregion

        #region Constructor
        public BSWriter(Stream input, Encoding encoding) : base(input, encoding)
        {
            Encoding = encoding;
        }
        public BSWriter(Stream input) : base(input)
        {
            Encoding = Encoding.Default;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Writes a string on the current stream. The string is prefixed with the length, encoded as an integer 32 bits at a time.
        /// </summary>
        public override void Write(string value)
        {
            // Apply encoding and write it
            var chars = value.ToCharArray();
            var bytes = Encoding.GetBytes(chars, 0, chars.Length);
            Write(bytes.Length);
            Write(bytes);
        }

        public void Write(string value, int length)
        {
            // Apply encoding and write it
            var chars = value.ToCharArray();
            var bytes = Encoding.GetBytes(chars, 0, length);
            Array.Resize(ref bytes, length);
            Write(bytes);
        }

        public void Write(string[] values)
        {
            foreach (string value in values)
                Write(value);
        }
        public void Write(Vector2 value)
        {
            Write(value.X);
            Write(value.Y);
        }
        public void Write(Vector3 value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
        }
        public void Write(Vector4 value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
            Write(value.W);
        }
        public void Write(Quaternion value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
            Write(value.W);
        }
        public void Write(Matrix4x4 value)
        {
            Write(value.M11);
            Write(value.M12);
            Write(value.M13);
            Write(value.M14);

            Write(value.M21);
            Write(value.M22);
            Write(value.M23);
            Write(value.M24);

            Write(value.M31);
            Write(value.M32);
            Write(value.M33);
            Write(value.M34);

            Write(value.M41);
            Write(value.M42);
            Write(value.M43);
            Write(value.M44);
        }
        public void Write(RectangleF value)
        {
            Write(value.Min);
            Write(value.Max);
        }
        public void Write(BoundingBoxF value)
        {
            Write(value.Min);
            Write(value.Max);
        }
        /// <summary>
        /// Format32bppArgb
        /// </summary>
        public void Write(Color32 value)
        {
            Write(value.Blue);
            Write(value.Green);
            Write(value.Red);
            Write(value.Alpha);
        }
        public void Write(Color3 value)
        {
            Write(value.Red);
            Write(value.Green);
            Write(value.Blue);
        }
        public void Write(Color4 value)
        {
            Write(value.Red);
            Write(value.Green);
            Write(value.Blue);
            Write(value.Alpha);
        }
        public void Write(uint[] values)
        {
            foreach (uint value in values)
                Write(value);
        }
        public void Serialize<T>(T value)
        where T : ISerializableBS
        {
            value.Serialize(this);
        }
        public void SerializeParameterized<T>(T value, params object[] parameters)
        where T : ISerializableParameterizedBS
        {
            value.Serialize(this, parameters);
        }

        public void Write(Rectangle rectangle)
        {
            Write(rectangle.X);
            Write(rectangle.Y);
            Write(rectangle.Width);
            Write(rectangle.Height);
        }
        #endregion
    }
}