using System;

namespace JMXFileEditor.Silkroad.Data.NIF
{
    [Flags]
    public enum NIFStyle : uint
    {
        Center = 256,
        Right = 512,

        LineCenter = 65536, //Not really sure about this.
    }
}
