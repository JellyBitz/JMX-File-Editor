using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMXFileEditor.Utility
{
    /// <summary>
    /// Extensions helper for BinaryReader
    /// </summary>
    public static class BinaryReaderHelpers
    {
        public static float[] ReadSingleArray(this BinaryReader BinaryReader,int Count)
        {
            var result = new float[Count];
            for (int i = 0; i < Count; i++)
                result[i] = BinaryReader.ReadSingle();
            return result;
        }

        public static uint[] ReadUInt32Array(this BinaryReader BinaryReader, int Count)
        {
            var result = new uint[Count];
            for (int i = 0; i < Count; i++)
                result[i] = BinaryReader.ReadUInt32();
            return result;
        }
    }
}
