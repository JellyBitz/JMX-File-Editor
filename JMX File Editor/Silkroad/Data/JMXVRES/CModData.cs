using JMXFileEditor.Silkroad.Data.Common;
using System.Collections.Generic;
namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
	public abstract class CModData
	{
		public float UnkFloat01 { get; set; }
		public uint UnkUInt01 { get; set; }
		public uint UnkUInt02 { get; set; }
		public uint UnkUInt03 { get; set; }
		public uint UnkUInt04 { get; set; }
		public uint UnkUInt05 { get; set; }
		public byte UnkByte01 { get; set; }
		public byte UnkByte02 { get; set; }
		public byte UnkByte03 { get; set; }
		public byte UnkByte04 { get; set; }
	}

	public class CModDataMtrl : CModData
	{
		public uint UnkUInt06 { get; set; }
		public uint UnkUInt07 { get; set; }
		public uint UnkUInt08 { get; set; }
		public List<GradientKey> GradientKeys { get; set; } = new List<GradientKey>();
		public List<CurveKey> CurveKeys { get; set; } = new List<CurveKey>();
		public uint UnkUInt09 { get; set; }
		public uint UnkUInt10 { get; set; }
		public uint UnkUInt11 { get; set; }
		public uint UnkUInt12 { get; set; }
		public uint UnkUInt13 { get; set; }
		public uint UnkUInt14 { get; set; }
		public uint UnkUInt15 { get; set; }
		public uint UnkUInt16 { get; set; }
		public uint UnkUInt17 { get; set; }

		public class GradientKey
		{
			public uint Time { get; set; }
			public Color4 Value { get; set; } = new Color4();
		}
		public class CurveKey
		{
			public uint Time { get; set; }
			public float Value { get; set; }
		}
	}
	public class CModDataTexAni : CModData
	{
		public uint UnkUInt06 { get; set; }
		public uint UnkUInt07 { get; set; }
		public uint UnkUInt08 { get; set; }
		public uint UnkUInt09 { get; set; }
		public uint UnkUInt10 { get; set; }
		public Matrix3D Matrix { get; set; } = new Matrix3D();
	}
	public class CModDataMultiTex : CModData
	{
		public uint UnkUInt06 { get; set; }
		public string Path { get; set; } = string.Empty;
		public uint UnkUInt07 { get; set; }
	}
	public class CModDataMultiTexRev : CModData
	{
		public uint UnkUInt06 { get; set; }
		public string Path { get; set; } = string.Empty;
		public uint UnkUInt07 { get; set; }
	}
	public class CModDataParticle : CModData
	{
		public List<Particle> Particles { get; set; } = new List<Particle>();
		public class Particle
		{
			public bool IsEnabled { get; set; }
			public string Path { get; set; } = string.Empty;
			public string BoneRelative { get; set; } = string.Empty;
			public Vector3 Position { get; set; } = new Vector3();
			public uint BirthTime { get; set; }
			public byte UnkByte01 { get; set; }
			public byte UnkByte02 { get; set; }
			public byte UnkByte03 { get; set; }
			public byte UnkByte04 { get; set; }
			public Vector3 UnkVector01 { get; set; } = new Vector3();
		}
	}
	public class CModDataEnvMap : CModData
	{
		public uint UnkUInt06 { get; set; }
		public uint UnkUInt07 { get; set; }
		public uint UnkUInt08 { get; set; }
		public uint UnkUInt09 { get; set; }
	}
	public class CModDataBumpEnv : CModData
	{
		public float UnkFloat02 { get; set; }
		public float UnkFloat03 { get; set; }
		public float UnkFloat04 { get; set; }
		public float UnkFloat05 { get; set; }
		public float UnkFloat06 { get; set; }
		public float UnkFloat07 { get; set; }
		public List<string> Textures { get; set; } = new List<string>();
	}
	public class CModDataSound : CModData
	{
		public uint UnkUInt06 { get; set; }
		public uint UnkUInt07 { get; set; }
		public uint UnkUInt08 { get; set; }
		public float UnkFloat02 { get; set; }
		public float UnkFloat03 { get; set; }
		public uint UnkUInt09 { get; set; }
		public uint UnkUInt10 { get; set; }
		public uint UnkUInt11 { get; set; }
		public uint UnkUInt12 { get; set; }
		public uint UnkUInt13 { get; set; }
		public uint UnkUInt14 { get; set; }
		public List<SndSet> SoundSet { get; set; } = new List<SndSet>();
		public class SndSet
		{
			public string Name { get; set; } = string.Empty;
			public List<Track> Tracks { get; set; } = new List<Track>();
			public class Track
			{
				public string Path { get; set; }
				public uint Time { get; set; }
				public string Event { get; set; }
			}
		}
	}
	public class CModDataDyVertex : CModData
	{
	}
	public class CModDataDyJoint : CModData
	{
	}
	public class CModDataDyLattice : CModData
	{
	}
	public class CModDataProgEquipPow : CModData
	{
	}
}
