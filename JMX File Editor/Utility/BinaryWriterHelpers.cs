using System.IO;
namespace JMXFileEditor.Utility
{
    /// <summary>
    /// Extensions helper for BinaryWriter
    /// </summary>
    public static class BinaryWriterHelpers
    {
        /// <summary>
        /// Writes a float array into the stream
        /// </summary>
        public static void Write(this BinaryWriter BinaryWriter,float[] value)
        {
            for (int i = 0; i < value.Length; i++)
                BinaryWriter.Write(value[i]);
        }
        /// <summary>
        /// Writes a uint array into the stream
        /// </summary>
        public static void Write(this BinaryWriter BinaryWriter, uint[] value)
        {
            for (int i = 0; i < value.Length; i++)
                BinaryWriter.Write(value[i]);
        }
        /// <summary>
        /// Writes a string format with the length as preffix using 32 bits
        /// </summary>
        public static void WriteString32(this BinaryWriter BinaryWriter,string value)
        {
            BinaryWriter.Write(value.Length);
            BinaryWriter.Write(value.ToCharArray());
        }
    }
}
