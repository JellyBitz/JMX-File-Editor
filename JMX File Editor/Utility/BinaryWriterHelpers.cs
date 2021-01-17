using System.IO;
namespace JMXFileEditor.Utility
{
    /// <summary>
    /// Extensions helper for BinaryWriter
    /// </summary>
    public static class BinaryWriterHelpers
    {
        public static void Write(this BinaryWriter BinaryWriter,float[] value)
        {
            for (int i = 0; i < value.Length; i++)
                BinaryWriter.Write(value[i]);
        }

        public static void Write(this BinaryWriter BinaryWriter, uint[] value)
        {
            for (int i = 0; i < value.Length; i++)
                BinaryWriter.Write(value[i]);
        }
    }
}
