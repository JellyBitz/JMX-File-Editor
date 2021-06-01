using System.IO;

namespace JMXFileEditor.Utility
{
    /// <summary>
    /// Extensions helper for BinaryReader
    /// </summary>
    public static class BinaryReaderHelpers
    {
        /// <summary>
        /// Reads a float array from stream
        /// </summary>
        public static float[] ReadSingleArray(this BinaryReader BinaryReader,int Count)
        {
            var result = new float[Count];
            for (int i = 0; i < Count; i++)
                result[i] = BinaryReader.ReadSingle();
            return result;
        }
        /// <summary>
        /// Reads a uint array from stream
        /// </summary>
        public static uint[] ReadUInt32Array(this BinaryReader BinaryReader, int Count)
        {
            var result = new uint[Count];
            for (int i = 0; i < Count; i++)
                result[i] = BinaryReader.ReadUInt32();
            return result;
        }
        /// <summary>
        /// Reads a string format with the length as preffix using 32 bits
        /// </summary>
        public static string ReadString32(this BinaryReader BinaryReader)
        {
            return new string(BinaryReader.ReadChars(BinaryReader.ReadInt32()));
        }
    }
}
