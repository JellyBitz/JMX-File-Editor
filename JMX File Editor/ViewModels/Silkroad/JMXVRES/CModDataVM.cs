using System;
using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Silkroad.Data.JMXVRES;
using JMXFileEditor.ViewModels.Silkroad.Common;
namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
	public class CModDataVM : JMXAbstract
	{
		#region Constructor
		public CModDataVM(string Name, Type[] AvailableTypes, Type CurrentType = null, object CurrentObject = null)
			: base(Name, AvailableTypes, CurrentType, CurrentObject)
		{
			// Add abstract formats
			AddFormatHandler(typeof(CModDataMtrl), (s, e) => {
				var vm = new CModDataMtrlVM(string.Empty, e.Obj is CModDataMtrl _obj ? _obj : new CModDataMtrl());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(CModDataTexAni), (s, e) => {
				var vm = new CModDataTexAniVM(string.Empty, e.Obj is CModDataTexAni _obj ? _obj : new CModDataTexAni());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(CModDataMultiTex), (s, e) => {
				var vm = new CModDataMultiTexVM(string.Empty, e.Obj is CModDataMultiTex _obj ? _obj : new CModDataMultiTex());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(CModDataMultiTexRev), (s, e) => {
				var vm = new CModDataMultiTexRevVM(string.Empty, e.Obj is CModDataMultiTexRev _obj ? _obj : new CModDataMultiTexRev());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(CModDataParticle), (s, e) => {
				var vm = new CModDataParticleVM(string.Empty, e.Obj is CModDataParticle _obj ? _obj : new CModDataParticle());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(CModDataEnvMap), (s, e) => {
				var vm = new CModDataEnvMapVM(string.Empty, e.Obj is CModDataEnvMap _obj ? _obj : new CModDataEnvMap());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(CModDataBumpEnv), (s, e) => {
				var vm = new CModDataBumpEnvVM(string.Empty, e.Obj is CModDataBumpEnv _obj ? _obj : new CModDataBumpEnv());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(CModDataSound), (s, e) => {
				var vm = new CModDataSoundVM(string.Empty, e.Obj is CModDataSound _obj ? _obj : new CModDataSound());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(CModDataDyVertex), (s, e) => {
				// Empty
			});
			AddFormatHandler(typeof(CModDataDyJoint), (s, e) => {
				// Empty
			});
			AddFormatHandler(typeof(CModDataDyLattice), (s, e) => {
				// Empty
			});
			AddFormatHandler(typeof(CModDataProgEquipPow), (s, e) => {
				// Empty
			});

			// Update values with new formats
			SetCurrentType(CurrentType, CurrentObject);
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			if (CurrentType != null)
			{
				// Check abstract formats
				if (CurrentType == typeof(CModDataMtrl))
					return new CModDataMtrlVM(string.Empty, new CModDataMtrl()).GetClassFrom(this);
				else if (CurrentType == typeof(CModDataTexAni))
					return new CModDataTexAniVM(string.Empty, new CModDataTexAni()).GetClassFrom(this);
				else if (CurrentType == typeof(CModDataMultiTex))
					return new CModDataMultiTexVM(string.Empty, new CModDataMultiTex()).GetClassFrom(this);
				else if (CurrentType == typeof(CModDataMultiTexRev))
					return new CModDataMultiTexRevVM(string.Empty, new CModDataMultiTexRev()).GetClassFrom(this);
				else if (CurrentType == typeof(CModDataParticle))
					return new CModDataParticleVM(string.Empty, new CModDataParticle()).GetClassFrom(this);
				else if (CurrentType == typeof(CModDataEnvMap))
					return new CModDataEnvMapVM(string.Empty, new CModDataEnvMap()).GetClassFrom(this);
				else if (CurrentType == typeof(CModDataBumpEnv))
					return new CModDataBumpEnvVM(string.Empty, new CModDataBumpEnv()).GetClassFrom(this);
				else if (CurrentType == typeof(CModDataSound))
					return new CModDataSoundVM(string.Empty, new CModDataSound()).GetClassFrom(this);
				else if (CurrentType == typeof(CModDataDyVertex))
					return new CModDataDyVertex();
				else if (CurrentType == typeof(CModDataDyJoint))
					return new CModDataDyJoint();
				else if (CurrentType == typeof(CModDataDyLattice))
					return new CModDataDyLattice();
				else if (CurrentType == typeof(CModDataProgEquipPow))
					return new CModDataProgEquipPow();
			}
			return null;
		}
		#endregion

		#region Internal Classes
		public class CModDataMtrlVM : JMXStructure
		{
			#region Constructor
			public CModDataMtrlVM(string Name, CModDataMtrl Mtrl) : base(Name, true)
			{
				// add formats
				AddFormatHandler(typeof(CModDataMtrl.GradientKey), (s, e) => {
					e.Childs.Add(new GradientKey("[" + e.Childs.Count + "]", e.Obj is CModDataMtrl.GradientKey _obj ? _obj : new CModDataMtrl.GradientKey()));
				});
				AddFormatHandler(typeof(CModDataMtrl.CurveKey), (s, e) => {
					e.Childs.Add(new CurveKey("[" + e.Childs.Count + "]", e.Obj is CModDataMtrl.CurveKey _obj ? _obj : new CModDataMtrl.CurveKey()));
				});
				// create nodes
				Childs.Add(new JMXAttribute("UnkUInt06", Mtrl.UnkUInt06));
				Childs.Add(new JMXAttribute("UnkUInt07", Mtrl.UnkUInt07));
				Childs.Add(new JMXAttribute("UnkUInt08", Mtrl.UnkUInt08));
				AddChildArray("GradientKeys", Mtrl.GradientKeys.ToArray(), true, true);
				AddChildArray("CurveKeys", Mtrl.CurveKeys.ToArray(), true, true);
				Childs.Add(new JMXAttribute("UnkUInt09", Mtrl.UnkUInt09));
				Childs.Add(new JMXAttribute("UnkUInt10", Mtrl.UnkUInt10));
				Childs.Add(new JMXAttribute("UnkUInt11", Mtrl.UnkUInt11));
				Childs.Add(new JMXAttribute("UnkUInt12", Mtrl.UnkUInt12));
				Childs.Add(new JMXAttribute("UnkUInt13", Mtrl.UnkUInt13));
				Childs.Add(new JMXAttribute("UnkUInt14", Mtrl.UnkUInt14));
				Childs.Add(new JMXAttribute("UnkUInt15", Mtrl.UnkUInt15));
				Childs.Add(new JMXAttribute("UnkUInt16", Mtrl.UnkUInt16));
				Childs.Add(new JMXAttribute("UnkUInt17", Mtrl.UnkUInt17));
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure)
			{
				CModDataMtrl obj = new CModDataMtrl
				{
					UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
					UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[1]).Value,
					UnkUInt08 = (uint)((JMXAttribute)Structure.Childs[2]).Value,
					GradientKeys = ((JMXStructure)Structure.Childs[3]).GetChildList<CModDataMtrl.GradientKey>(),
					CurveKeys = ((JMXStructure)Structure.Childs[4]).GetChildList<CModDataMtrl.CurveKey>(),
					UnkUInt09 = (uint)((JMXAttribute)Structure.Childs[5]).Value,
					UnkUInt10 = (uint)((JMXAttribute)Structure.Childs[6]).Value,
					UnkUInt11 = (uint)((JMXAttribute)Structure.Childs[7]).Value,
					UnkUInt12 = (uint)((JMXAttribute)Structure.Childs[8]).Value,
					UnkUInt13 = (uint)((JMXAttribute)Structure.Childs[9]).Value,
					UnkUInt14 = (uint)((JMXAttribute)Structure.Childs[10]).Value,
					UnkUInt15 = (uint)((JMXAttribute)Structure.Childs[11]).Value,
					UnkUInt16 = (uint)((JMXAttribute)Structure.Childs[12]).Value,
					UnkUInt17 = (uint)((JMXAttribute)Structure.Childs[13]).Value
				};
				return obj;
			}
			#endregion

			#region Internal Classes
			public class GradientKey : JMXStructure
			{
				public GradientKey(string Name, CModDataMtrl.GradientKey Key) : base(Name, true)
				{
					// Add format
					AddFormatHandler(typeof(Color4), (s, e) => {
						e.Childs.Add(new Color4VM("[" + e.Childs.Count + "]", e.Obj is Color4 _obj ? _obj : new Color4()));
					});
					// Create VM
					Childs.Add(new JMXAttribute("Time", Key.Time));
					Childs.Add(new Color4VM("Value", Key.Value));
				}
				public override object GetClassFrom(JMXStructure Structure)
				{
					CModDataMtrl.GradientKey obj = new CModDataMtrl.GradientKey()
					{
						Time = (uint)((JMXAttribute)Structure.Childs[0]).Value,
						Value = (Color4)((Color4VM)Structure.Childs[1]).GetClass()
					};
					return obj;
				}
			}
			public class CurveKey : JMXStructure
			{
				public CurveKey(string Name, CModDataMtrl.CurveKey Key) : base(Name, true)
				{
					Childs.Add(new JMXAttribute("Time", Key.Time));
					Childs.Add(new JMXAttribute("Value", Key.Value));
				}
				public override object GetClassFrom(JMXStructure Structure)
				{
					CModDataMtrl.CurveKey obj = new CModDataMtrl.CurveKey()
					{
						Time = (uint)((JMXAttribute)Structure.Childs[0]).Value,
						Value = (float)((JMXAttribute)Structure.Childs[0]).Value
					};
					return obj;
				}
			}
			#endregion
		}
		public class CModDataTexAniVM : JMXStructure
		{
			#region Constructor
			public CModDataTexAniVM(string Name, CModDataTexAni TexAni) : base(Name, true)
			{
				// create nodes
				Childs.Add(new JMXAttribute("UnkUInt06", TexAni.UnkUInt06));
				Childs.Add(new JMXAttribute("UnkUInt07", TexAni.UnkUInt07));
				Childs.Add(new JMXAttribute("UnkUInt08", TexAni.UnkUInt08));
				Childs.Add(new JMXAttribute("UnkUInt09", TexAni.UnkUInt09));
				Childs.Add(new JMXAttribute("UnkUInt10", TexAni.UnkUInt10));
				Childs.Add(new Matrix3DVM("Matrix", TexAni.Matrix));
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure)
			{
				CModDataTexAni obj = new CModDataTexAni()
				{
					UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
					UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[1]).Value,
					UnkUInt08 = (uint)((JMXAttribute)Structure.Childs[2]).Value,
					UnkUInt09 = (uint)((JMXAttribute)Structure.Childs[3]).Value,
					UnkUInt10 = (uint)((JMXAttribute)Structure.Childs[4]).Value,
					Matrix = (Matrix3D)((Matrix3DVM)Structure.Childs[5]).GetClass()
				};
				return obj;
			}
			#endregion
		}
		public class CModDataMultiTexVM : JMXStructure
		{
			#region Constructor
			public CModDataMultiTexVM(string Name, CModDataMultiTex MultiTex) : base(Name, true)
			{
				// create nodes
				Childs.Add(new JMXAttribute("UnkUInt06", MultiTex.UnkUInt06));
				Childs.Add(new JMXAttribute("Path", MultiTex.Path));
				Childs.Add(new JMXAttribute("UnkUInt07", MultiTex.UnkUInt07));
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure)
			{
				CModDataMultiTex obj = new CModDataMultiTex()
				{
					UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
					Path = (string)((JMXAttribute)Structure.Childs[1]).Value,
					UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[2]).Value
				};
				return obj;
			}
			#endregion
		}
		public class CModDataMultiTexRevVM : JMXStructure
		{
			#region Constructor
			public CModDataMultiTexRevVM(string Name, CModDataMultiTexRev MultiTexRev) : base(Name, true)
			{
				// create nodes
				Childs.Add(new JMXAttribute("UnkUInt06", MultiTexRev.UnkUInt06));
				Childs.Add(new JMXAttribute("Path", MultiTexRev.Path));
				Childs.Add(new JMXAttribute("UnkUInt07", MultiTexRev.UnkUInt07));
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure)
			{
				CModDataMultiTex obj = new CModDataMultiTex()
				{
					UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
					Path = (string)((JMXAttribute)Structure.Childs[1]).Value,
					UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[2]).Value
				};
				return obj;
			}
			#endregion
		}
		public class CModDataParticleVM : JMXStructure
		{
			#region Constructor
			public CModDataParticleVM(string Name, CModDataParticle Particle) : base(Name, true)
			{
				// add formats
				AddFormatHandler(typeof(CModDataParticle.Particle), (s, e) => {
					e.Childs.Add(new Particle("[" + e.Childs.Count + "]", e.Obj is CModDataParticle.Particle _obj ? _obj : new CModDataParticle.Particle()));
				});
				// create nodes
				AddChildArray("Particles", Particle.Particles.ToArray(), true, true);
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure)
			{
				CModDataParticle obj = new CModDataParticle()
				{
					Particles = ((JMXStructure)Structure.Childs[0]).GetChildList<CModDataParticle.Particle>()
				};
				return obj;
			}
			#endregion

			#region Internal Classes
			public class Particle : JMXStructure
			{
				public Particle(string Name, CModDataParticle.Particle Particle) : base(Name, true)
				{
					Childs.Add(new JMXAttribute("IsEnabled", Particle.IsEnabled));
					Childs.Add(new JMXAttribute("Path", Particle.Path));
					Childs.Add(new JMXAttribute("BoneRelative", Particle.BoneRelative));
					Childs.Add(new Vector3VM("Position", Particle.Position));
					Childs.Add(new JMXAttribute("BirthTime", Particle.BirthTime));
					Childs.Add(new JMXAttribute("UnkByte01", Particle.UnkByte01));
					Childs.Add(new JMXAttribute("UnkByte02", Particle.UnkByte02));
					Childs.Add(new JMXAttribute("UnkByte03", Particle.UnkByte03));
					Childs.Add(new JMXAttribute("UnkByte04", Particle.UnkByte04));
					Childs.Add(new Vector3VM("UnkVector01", Particle.UnkVector01));
				}
				public override object GetClassFrom(JMXStructure Structure)
				{
					CModDataParticle.Particle obj = new CModDataParticle.Particle()
					{
						IsEnabled = (bool)((JMXAttribute)Structure.Childs[0]).Value,
						Path = (string)((JMXAttribute)Structure.Childs[1]).Value,
						BoneRelative = (string)((JMXAttribute)Structure.Childs[2]).Value,
						Position = (Vector3)((Vector3VM)Structure.Childs[3]).GetClass(),
						BirthTime = (uint)((JMXAttribute)Structure.Childs[4]).Value,
						UnkByte01 = (byte)((JMXAttribute)Structure.Childs[5]).Value,
						UnkByte02 = (byte)((JMXAttribute)Structure.Childs[6]).Value,
						UnkByte03 = (byte)((JMXAttribute)Structure.Childs[7]).Value,
						UnkByte04 = (byte)((JMXAttribute)Structure.Childs[8]).Value,
						UnkVector01 = (Vector3)((Vector3VM)Structure.Childs[9]).GetClass()
					};
					return obj;
				}
			}
			#endregion
		}
		public class CModDataEnvMapVM : JMXStructure
		{
			#region Constructor
			public CModDataEnvMapVM(string Name, CModDataEnvMap EnvMap) : base(Name, true)
			{
				// create nodes
				Childs.Add(new JMXAttribute("UnkUInt06", EnvMap.UnkUInt06));
				Childs.Add(new JMXAttribute("UnkUInt07", EnvMap.UnkUInt07));
				Childs.Add(new JMXAttribute("UnkUInt08", EnvMap.UnkUInt08));
				Childs.Add(new JMXAttribute("UnkUInt09", EnvMap.UnkUInt09));
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure)
			{
				CModDataEnvMap obj = new CModDataEnvMap()
				{
					UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
					UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[1]).Value,
					UnkUInt08 = (uint)((JMXAttribute)Structure.Childs[2]).Value,
					UnkUInt09 = (uint)((JMXAttribute)Structure.Childs[3]).Value,
				};
				return obj;
			}
			#endregion
		}
		public class CModDataBumpEnvVM : JMXStructure
		{
			#region Constructor
			public CModDataBumpEnvVM(string Name, CModDataBumpEnv BumpEnv) : base(Name, true)
			{
				// create nodes
				Childs.Add(new JMXAttribute("UnkFloat02", BumpEnv.UnkFloat02));
				Childs.Add(new JMXAttribute("UnkFloat03", BumpEnv.UnkFloat03));
				Childs.Add(new JMXAttribute("UnkFloat04", BumpEnv.UnkFloat04));
				Childs.Add(new JMXAttribute("UnkFloat05", BumpEnv.UnkFloat05));
				Childs.Add(new JMXAttribute("UnkFloat06", BumpEnv.UnkFloat06));
				Childs.Add(new JMXAttribute("UnkFloat07", BumpEnv.UnkFloat07));
				AddChildArray("Textures", BumpEnv.Textures.ToArray(), true, true);
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure)
			{
				CModDataBumpEnv obj = new CModDataBumpEnv()
				{
					UnkFloat02 = (float)((JMXAttribute)Structure.Childs[0]).Value,
					UnkFloat03 = (float)((JMXAttribute)Structure.Childs[1]).Value,
					UnkFloat04 = (float)((JMXAttribute)Structure.Childs[2]).Value,
					UnkFloat05 = (float)((JMXAttribute)Structure.Childs[3]).Value,
					UnkFloat06 = (float)((JMXAttribute)Structure.Childs[4]).Value,
					UnkFloat07 = (float)((JMXAttribute)Structure.Childs[5]).Value,
					Textures = ((JMXStructure)Structure.Childs[6]).GetChildList<string>()
				};
				return obj;
			}
			#endregion
		}
		public class CModDataSoundVM : JMXStructure
		{
			#region Constructor
			public CModDataSoundVM(string Name, CModDataSound Sound) : base(Name, true)
			{
				// add formats
				AddFormatHandler(typeof(CModDataSound.SndSet), (s, e) => {
					e.Childs.Add(new SndSet("[" + e.Childs.Count + "]", e.Obj is CModDataSound.SndSet _obj ? _obj : new CModDataSound.SndSet()));
				});
				// create nodes
				Childs.Add(new JMXAttribute("UnkUInt06", Sound.UnkUInt06));
				Childs.Add(new JMXAttribute("UnkUInt07", Sound.UnkUInt07));
				Childs.Add(new JMXAttribute("UnkUInt08", Sound.UnkUInt08));
				Childs.Add(new JMXAttribute("UnkFloat02", Sound.UnkFloat02));
				Childs.Add(new JMXAttribute("UnkFloat03", Sound.UnkFloat03));
				Childs.Add(new JMXAttribute("UnkUInt09", Sound.UnkUInt09));
				Childs.Add(new JMXAttribute("UnkUInt10", Sound.UnkUInt10));
				Childs.Add(new JMXAttribute("UnkUInt11", Sound.UnkUInt11));
				Childs.Add(new JMXAttribute("UnkUInt12", Sound.UnkUInt12));
				Childs.Add(new JMXAttribute("UnkUInt13", Sound.UnkUInt13));
				Childs.Add(new JMXAttribute("UnkUInt14", Sound.UnkUInt14));
				AddChildArray("SoundSet", Sound.SoundSet.ToArray(), true, true);
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure)
			{
				CModDataSound obj = new CModDataSound()
				{
					UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
					UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[1]).Value,
					UnkUInt08 = (uint)((JMXAttribute)Structure.Childs[2]).Value,
					UnkFloat02 = (float)((JMXAttribute)Structure.Childs[3]).Value,
					UnkFloat03 = (float)((JMXAttribute)Structure.Childs[4]).Value,
					UnkUInt09 = (uint)((JMXAttribute)Structure.Childs[5]).Value,
					UnkUInt10 = (uint)((JMXAttribute)Structure.Childs[6]).Value,
					UnkUInt11 = (uint)((JMXAttribute)Structure.Childs[7]).Value,
					UnkUInt12 = (uint)((JMXAttribute)Structure.Childs[8]).Value,
					UnkUInt13 = (uint)((JMXAttribute)Structure.Childs[9]).Value,
					UnkUInt14 = (uint)((JMXAttribute)Structure.Childs[10]).Value,
					SoundSet = ((JMXStructure)Structure.Childs[11]).GetChildList<CModDataSound.SndSet>()
				};
				return obj;
			}
			#endregion

			#region Internal Classes
			public class SndSet : JMXStructure
			{
				#region Constructor
				public SndSet(string Name, CModDataSound.SndSet Set) : base(Name, true)
				{
					// add formats
					AddFormatHandler(typeof(CModDataSound.SndSet.Track), (s, e) => {
						e.Childs.Add(new Track("[" + e.Childs.Count + "]", e.Obj is CModDataSound.SndSet.Track _obj ? _obj : new CModDataSound.SndSet.Track()));
					});
					// create nodes
					Childs.Add(new JMXAttribute("Name", Set.Name));
					AddChildArray("Tracks", Set.Tracks.ToArray(), true, true);
				}
				#endregion

				#region Public Methods
				public override object GetClassFrom(JMXStructure Structure)
				{
					CModDataSound.SndSet obj = new CModDataSound.SndSet()
					{
						Name = (string)((JMXAttribute)Structure.Childs[0]).Value,
						Tracks = ((JMXStructure)Structure.Childs[1]).GetChildList<CModDataSound.SndSet.Track>()
					};
					return obj;
				}
				#endregion

				#region Internal Classes
				public class Track : JMXStructure
				{
					public Track(string Name, CModDataSound.SndSet.Track Track) : base(Name, true)
					{
						Childs.Add(new JMXAttribute("Path", Track.Path));
						Childs.Add(new JMXAttribute("Time", Track.Time));
						Childs.Add(new JMXAttribute("Event", Track.Event));
					}
					public override object GetClassFrom(JMXStructure Structure)
					{
						CModDataSound.SndSet.Track obj = new CModDataSound.SndSet.Track()
						{
							Path = (string)((JMXAttribute)Structure.Childs[0]).Value,
							Time = (uint)((JMXAttribute)Structure.Childs[1]).Value,
							Event = (string)((JMXAttribute)Structure.Childs[2]).Value,
						};
						return obj;
					}
				}
				#endregion
			}
			#endregion
		}
		#endregion
	}
}
