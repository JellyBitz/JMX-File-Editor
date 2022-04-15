using System;
namespace JMXFileEditor.Silkroad.Data.JMXVBMT
{
	[Flags]
	public enum PrimMtrlFlag : uint
	{
		//None = 0,
		Bit1 = 0x1,

		Bit2 = 0x2,
		Bit3 = 0x4,
		Bit4 = 0x8,
		Bit5 = 0x10,
		Bit6 = 0x20,

		/// <summary>
		/// ColorTint?
		/// </summary>
		Bit7 = 0x40,

		Bit8 = 0x80,

		/// <summary>
		/// DiffuseMap?
		/// </summary>
		Bit9 = 0x100,

		/// <summary>
		/// Alpha channel, also for metalic sheen
		/// </summary>
		Bit10 = 0x200,

		Bit11 = 0x400,
		Bit12 = 0x800,
		Bit13 = 0x1000,

		/// <summary>
		/// BumpMap
		/// </summary>
		Bit14 = 0x2000,

		Bit15 = 0x4000,
		Bit16 = 0x8000,
		Bit17 = 0x10000,
		Bit18 = 0x20000,
		Bit19 = 0x40000,
		Bit20 = 0x80000,
		Bit21 = 0x100000,
		Bit22 = 0x200000,
		Bit23 = 0x400000,
		Bit24 = 0x800000,
		Bit25 = 0x1000000,
		Bit26 = 0x2000000,
		Bit27 = 0x4000000,
		Bit28 = 0x8000000,
		Bit29 = 0x10000000,
		Bit30 = 0x20000000,
		Bit31 = 0x40000000,
		//Bit32 = 0x80000000,
	}
}
