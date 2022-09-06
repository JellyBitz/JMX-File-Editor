using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    [Flags]
    public enum SourceNodeType : byte
    {
        Bit0 = 1 << 0, // Relative to parent.
        Bit1 = 1 << 1, // Includes own transform.
        Bit2 = 1 << 2, // Includes parent transform.
        Bit3 = 1 << 3,
        Bit4 = 1 << 4,
        Bit5 = 1 << 5,
        Bit6 = 1 << 6,
        Bit7 = 1 << 7,
    }
}