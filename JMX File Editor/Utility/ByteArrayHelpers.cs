using System;
using System.Text;

namespace JMXFileEditor.Utility
{
    public static class ByteArrayHelpers
    {
        public static string ToHexDump(this byte[] ByteArray)
        {
            return ToHexDump(ByteArray, 0, ByteArray.Length);
        }
        public static string ToHexDump(this byte[] ByteArray, int Offset, int Count)
        {
            const int bytesPerLine = 16;
            StringBuilder output = new StringBuilder();
            StringBuilder ascii_output = new StringBuilder();
            int length = Count;
            if (length % bytesPerLine != 0)
            {
                length += bytesPerLine - length % bytesPerLine;
            }
            for (int x = 0; x <= length; ++x)
            {
                if (x % bytesPerLine == 0)
                {
                    if (x > 0)
                    {
                        output.AppendFormat("  {0}{1}", ascii_output.ToString(), Environment.NewLine);
                        ascii_output.Clear();
                    }
                    if (x != length)
                    {
                        output.AppendFormat("{0:d10}   ", x);
                    }
                }
                if (x < Count)
                {
                    output.AppendFormat("{0:X2} ", ByteArray[Offset + x]);
                    char ch = (char)ByteArray[Offset + x];
                    if (!Char.IsControl(ch))
                    {
                        ascii_output.AppendFormat("{0}", ch);
                    }
                    else
                    {
                        ascii_output.Append(".");
                    }
                }
                else
                {
                    output.Append("   ");
                    ascii_output.Append(".");
                }
            }
            return output.ToString();
        }
    }
}
