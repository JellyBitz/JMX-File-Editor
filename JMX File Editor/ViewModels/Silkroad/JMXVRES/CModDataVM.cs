using System;
using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Silkroad.Data.JMXVRES;
using JMXFileEditor.Silkroad.Data.JMXVRES.ModData;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Common;
namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
	public class ModDataVM : JMXAbstract
	{
		#region Constructor
		public ModDataVM(string Name, Type[] AvailableTypes, Type CurrentType, object CurrentObject)
			: base(Name, AvailableTypes, CurrentType, CurrentObject)
		{
			// Add abstract formats
			AddFormatHandler(typeof(ModDataMtrl), (s, e) => {
				var vm = new ModDataMtrlVM(string.Empty, e.Obj is ModDataMtrl _obj ? _obj : new ModDataMtrl());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(ModDataTexAni), (s, e) => {
				var vm = new ModDataTexAniVM(string.Empty, e.Obj is ModDataTexAni _obj ? _obj : new ModDataTexAni());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(ModDataMultiTex), (s, e) => {
				var vm = new ModDataMultiTexVM(string.Empty, e.Obj is ModDataMultiTex _obj ? _obj : new ModDataMultiTex());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(ModDataMultiTexRev), (s, e) => {
				var vm = new ModDataMultiTexRevVM(string.Empty, e.Obj is ModDataMultiTexRev _obj ? _obj : new ModDataMultiTexRev());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(ModDataParticle), (s, e) => {
				var vm = new ModDataParticleVM(string.Empty, e.Obj is ModDataParticle _obj ? _obj : new ModDataParticle());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(ModDataEnvMap), (s, e) => {
				var vm = new ModDataEnvMapVM(string.Empty, e.Obj is ModDataEnvMap _obj ? _obj : new ModDataEnvMap());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(ModDataBumpEnv), (s, e) => {
				var vm = new ModDataBumpEnvVM(string.Empty, e.Obj is ModDataBumpEnv _obj ? _obj : new ModDataBumpEnv());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(ModDataSound), (s, e) => {
				var vm = new ModDataSoundVM(string.Empty, e.Obj is ModDataSound _obj ? _obj : new ModDataSound());
				foreach (var c in vm.Childs)
					e.Childs.Add(c);
			});
			AddFormatHandler(typeof(ModDataDyVertex), (s, e) => {
				// Empty
			});
			AddFormatHandler(typeof(ModDataDyJoint), (s, e) => {
				// Empty
			});
			AddFormatHandler(typeof(ModDataDyLattice), (s, e) => {
				// Empty
			});
			AddFormatHandler(typeof(ModDataProgEquipPow), (s, e) => {
				// Empty
			});

			// Update values with new formats
			SetCurrentType(CurrentType, CurrentObject);
		}
		#endregion

		#region Abstract Implementation
		protected override void AddBaseNodes(object Obj)
		{
			IModData obj = (IModData)Obj;
			Childs.Add(new JMXAttribute("UnkFloat01", obj.UnkFloat01));
			Childs.Add(new JMXAttribute("UnkUInt01", obj.UnkUInt01));
			Childs.Add(new JMXAttribute("UnkUInt02", obj.UnkUInt02));
			Childs.Add(new JMXAttribute("UnkUInt03", obj.UnkUInt03));
			Childs.Add(new JMXAttribute("UnkUInt04", obj.UnkUInt04));
			Childs.Add(new JMXAttribute("UnkUInt05", obj.UnkUInt05));
			Childs.Add(new JMXAttribute("UnkByte01", obj.UnkByte01));
			Childs.Add(new JMXAttribute("UnkByte02", obj.UnkByte02));
			Childs.Add(new JMXAttribute("UnkByte03", obj.UnkByte03));
			Childs.Add(new JMXAttribute("UnkByte04", obj.UnkByte04));
		}
		public override object GetClassFrom(JMXStructure Structure)
		{
			IModData data = null;
			// Check abstract formats
			if (CurrentType == typeof(ModDataMtrl))
				data = (ModDataMtrl)(new ModDataMtrlVM(string.Empty, new ModDataMtrl()).GetClassFrom(this,10));
			else if (CurrentType == typeof(ModDataTexAni))
				data = (ModDataTexAni)(new ModDataTexAniVM(string.Empty, new ModDataTexAni()).GetClassFrom(this,10));
			else if (CurrentType == typeof(ModDataMultiTex))
				data = (ModDataMultiTex)(new ModDataMultiTexVM(string.Empty, new ModDataMultiTex()).GetClassFrom(this,10));
			else if (CurrentType == typeof(ModDataMultiTexRev))
				data = (ModDataMultiTexRev)(new ModDataMultiTexRevVM(string.Empty, new ModDataMultiTexRev()).GetClassFrom(this,10));
			else if (CurrentType == typeof(ModDataParticle))
				data = (ModDataParticle)(new ModDataParticleVM(string.Empty, new ModDataParticle()).GetClassFrom(this,10));
			else if (CurrentType == typeof(ModDataEnvMap))
				data = (ModDataEnvMap)(new ModDataEnvMapVM(string.Empty, new ModDataEnvMap()).GetClassFrom(this,10));
			else if (CurrentType == typeof(ModDataBumpEnv))
				data = (ModDataBumpEnv)(new ModDataBumpEnvVM(string.Empty, new ModDataBumpEnv()).GetClassFrom(this,10));
			else if (CurrentType == typeof(ModDataSound))
				data = (ModDataSound)(new ModDataSoundVM(string.Empty, new ModDataSound()).GetClassFrom(this,10));
			else if (CurrentType == typeof(ModDataDyVertex))
				data = new ModDataDyVertex();
			else if (CurrentType == typeof(ModDataDyJoint))
				data = new ModDataDyJoint();
			else if (CurrentType == typeof(ModDataDyLattice))
				data = new ModDataDyLattice();
			else if (CurrentType == typeof(ModDataProgEquipPow))
				data = new ModDataProgEquipPow();
			// Set base nodes
			data.UnkFloat01 = (float)((JMXAttribute)Structure.Childs[0]).Value;
			data.UnkUInt01 = (uint)((JMXAttribute)Structure.Childs[1]).Value;
			data.UnkUInt02 = (uint)((JMXAttribute)Structure.Childs[2]).Value;
			data.UnkUInt03 = (uint)((JMXAttribute)Structure.Childs[3]).Value;
			data.UnkUInt04 = (uint)((JMXAttribute)Structure.Childs[4]).Value;
			data.UnkUInt05 = (uint)((JMXAttribute)Structure.Childs[5]).Value;
			data.UnkByte01 = (byte)((JMXAttribute)Structure.Childs[6]).Value;
			data.UnkByte02 = (byte)((JMXAttribute)Structure.Childs[7]).Value;
			data.UnkByte03 = (byte)((JMXAttribute)Structure.Childs[8]).Value;
			data.UnkByte04 = (byte)((JMXAttribute)Structure.Childs[9]).Value;
			// return abstract class
			return data;
		}
		#endregion

		#region Internal Classes
		public class ModDataMtrlVM : JMXStructure
		{
			#region Constructor
			public ModDataMtrlVM(string Name, ModDataMtrl Mtrl) : base(Name, true)
			{
				// add formats
				AddFormatHandler(typeof(ModDataMtrl.GradientKey), (s, e) => {
					e.Childs.Add(new GradientKey("[" + e.Childs.Count + "]", e.Obj is ModDataMtrl.GradientKey _obj ? _obj : new ModDataMtrl.GradientKey()));
				});
				AddFormatHandler(typeof(ModDataMtrl.CurveKey), (s, e) => {
					e.Childs.Add(new CurveKey("[" + e.Childs.Count + "]", e.Obj is ModDataMtrl.CurveKey _obj ? _obj : new ModDataMtrl.CurveKey()));
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
			public override object GetClassFrom(JMXStructure Structure, int i)
			{
				ModDataMtrl obj = new ModDataMtrl
				{
					UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt08 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					GradientKeys = ((JMXStructure)Structure.Childs[i++]).GetChildList<ModDataMtrl.GradientKey>(),
					CurveKeys = ((JMXStructure)Structure.Childs[i++]).GetChildList<ModDataMtrl.CurveKey>(),
					UnkUInt09 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt10 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt11 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt12 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt13 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt14 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt15 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt16 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt17 = (uint)((JMXAttribute)Structure.Childs[i++]).Value
				};
				return obj;
			}
			#endregion

			#region Internal Classes
			public class GradientKey : JMXStructure
			{
				public GradientKey(string Name, ModDataMtrl.GradientKey Key) : base(Name, true)
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
					ModDataMtrl.GradientKey obj = new ModDataMtrl.GradientKey()
					{
						Time = (int)((JMXAttribute)Structure.Childs[0]).Value,
						Value = (Color4)((Color4VM)Structure.Childs[1]).GetClass()
					};
					return obj;
				}
			}
			public class CurveKey : JMXStructure
			{
				public CurveKey(string Name, ModDataMtrl.CurveKey Key) : base(Name, true)
				{
					Childs.Add(new JMXAttribute("Time", Key.Time));
					Childs.Add(new JMXAttribute("Value", Key.Value));
				}
				public override object GetClassFrom(JMXStructure Structure)
				{
					ModDataMtrl.CurveKey obj = new ModDataMtrl.CurveKey()
					{
						Time = (int)((JMXAttribute)Structure.Childs[0]).Value,
						Value = (float)((JMXAttribute)Structure.Childs[0]).Value
					};
					return obj;
				}
			}
			#endregion
		}
		public class ModDataTexAniVM : JMXStructure
		{
			#region Constructor
			public ModDataTexAniVM(string Name, ModDataTexAni TexAni) : base(Name, true)
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
			public override object GetClassFrom(JMXStructure Structure, int i)
			{
				ModDataTexAni obj = new ModDataTexAni()
				{
					UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt08 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt09 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt10 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					Matrix = (Matrix4x4)((Matrix3DVM)Structure.Childs[i++]).GetClass()
				};
				return obj;
			}
			#endregion
		}
		public class ModDataMultiTexVM : JMXStructure
		{
			#region Constructor
			public ModDataMultiTexVM(string Name, ModDataMultiTex MultiTex) : base(Name, true)
			{
				// create nodes
				Childs.Add(new JMXAttribute("UnkUInt06", MultiTex.UnkUInt06));
				Childs.Add(new JMXAttribute("Path", MultiTex.Path));
				Childs.Add(new JMXAttribute("UnkUInt07", MultiTex.UnkUInt07));
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure, int i)
			{
				ModDataMultiTex obj = new ModDataMultiTex()
				{
					UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					Path = (string)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[i++]).Value
				};
				return obj;
			}
			#endregion
		}
		public class ModDataMultiTexRevVM : JMXStructure
		{
			#region Constructor
			public ModDataMultiTexRevVM(string Name, ModDataMultiTexRev MultiTexRev) : base(Name, true)
			{
				// create nodes
				Childs.Add(new JMXAttribute("UnkUInt06", MultiTexRev.UnkUInt06));
				Childs.Add(new JMXAttribute("Path", MultiTexRev.Path));
				Childs.Add(new JMXAttribute("UnkUInt07", MultiTexRev.UnkUInt07));
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure, int i)
			{
				ModDataMultiTex obj = new ModDataMultiTex()
				{
					UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					Path = (string)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[i++]).Value
				};
				return obj;
			}
			#endregion
		}
		public class ModDataParticleVM : JMXStructure
		{
			#region Constructor
			public ModDataParticleVM(string Name, ModDataParticle Particle) : base(Name, true)
			{
				// add formats
				AddFormatHandler(typeof(ModDataParticleConfig), (s, e) => {
					e.Childs.Add(new Particle("[" + e.Childs.Count + "]", e.Obj is ModDataParticleConfig _obj ? _obj : new ModDataParticleConfig()));
				});
				// create nodes
				AddChildArray("Particles", Particle.Particles.ToArray(), true, true);
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure, int i)
			{
				ModDataParticle obj = new ModDataParticle()
				{
					Particles = ((JMXStructure)Structure.Childs[i++]).GetChildList<ModDataParticleConfig>()
				};
				return obj;
			}
			#endregion

			#region Internal Classes
			public class Particle : JMXStructure
			{
				public Particle(string Name, ModDataParticleConfig Particle) : base(Name, true)
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
					ModDataParticleConfig obj = new ModDataParticleConfig()
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
		public class ModDataEnvMapVM : JMXStructure
		{
			#region Constructor
			public ModDataEnvMapVM(string Name, ModDataEnvMap EnvMap) : base(Name, true)
			{
				// create nodes
				Childs.Add(new JMXAttribute("UnkUInt06", EnvMap.UnkUInt06));
				Childs.Add(new JMXAttribute("UnkUInt07", EnvMap.UnkUInt07));
				Childs.Add(new JMXAttribute("UnkUInt08", EnvMap.UnkUInt08));
				Childs.Add(new JMXAttribute("UnkUInt09", EnvMap.UnkUInt09));
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure, int i)
			{
				ModDataEnvMap obj = new ModDataEnvMap()
				{
					UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt08 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt09 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
				};
				return obj;
			}
			#endregion
		}
		public class ModDataBumpEnvVM : JMXStructure
		{
			#region Constructor
			public ModDataBumpEnvVM(string Name, ModDataBumpEnv BumpEnv) : base(Name, true)
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
			public override object GetClassFrom(JMXStructure Structure, int i)
			{
				ModDataBumpEnv obj = new ModDataBumpEnv()
				{
					UnkFloat02 = (float)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkFloat03 = (float)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkFloat04 = (float)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkFloat05 = (float)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkFloat06 = (float)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkFloat07 = (float)((JMXAttribute)Structure.Childs[i++]).Value,
					Textures = ((JMXStructure)Structure.Childs[i++]).GetChildList<string>()
				};
				return obj;
			}
			#endregion
		}
		public class ModDataSoundVM : JMXStructure
		{
			#region Constructor
			public ModDataSoundVM(string Name, ModDataSound Sound) : base(Name, true)
			{
				// add formats
				AddFormatHandler(typeof(ModDataSoundSet), (s, e) => {
					e.Childs.Add(new SndSet("[" + e.Childs.Count + "]", e.Obj is ModDataSoundSet _obj ? _obj : new ModDataSoundSet()));
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
			public override object GetClassFrom(JMXStructure Structure, int i)
			{
				ModDataSound obj = new ModDataSound()
				{
					UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt08 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkFloat02 = (float)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkFloat03 = (float)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt09 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt10 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt11 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt12 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt13 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					UnkUInt14 = (uint)((JMXAttribute)Structure.Childs[i++]).Value,
					SoundSet = ((JMXStructure)Structure.Childs[i++]).GetChildList<ModDataSoundSet>()
				};
				return obj;
			}
			#endregion

			#region Internal Classes
			public class SndSet : JMXStructure
			{
				#region Constructor
				public SndSet(string Name, ModDataSoundSet Set) : base(Name, true)
				{
					// add formats
					AddFormatHandler(typeof(ModDataSoundTrack), (s, e) => {
						e.Childs.Add(new Track("[" + e.Childs.Count + "]", e.Obj is ModDataSoundTrack _obj ? _obj : new ModDataSoundTrack()));
					});
					// create nodes
					Childs.Add(new JMXAttribute("Name", Set.Name));
					AddChildArray("Tracks", Set.Tracks.ToArray(), true, true);
				}
				#endregion

				#region Public Methods
				public override object GetClassFrom(JMXStructure Structure)
				{
					ModDataSoundSet obj = new ModDataSoundSet()
					{
						Name = (string)((JMXAttribute)Structure.Childs[0]).Value,
						Tracks = ((JMXStructure)Structure.Childs[1]).GetChildList<ModDataSoundTrack>()
					};
					return obj;
				}
				#endregion

				#region Internal Classes
				public class Track : JMXStructure
				{
					public Track(string Name, ModDataSoundTrack Track) : base(Name, true)
					{
						Childs.Add(new JMXAttribute("Path", Track.Path));
						Childs.Add(new JMXAttribute("Time", Track.Time));
						Childs.Add(new JMXAttribute("Event", Track.Event));
					}
					public override object GetClassFrom(JMXStructure Structure)
					{
						ModDataSoundTrack obj = new ModDataSoundTrack()
						{
							Path = (string)((JMXAttribute)Structure.Childs[0]).Value,
							Time = (int)((JMXAttribute)Structure.Childs[1]).Value,
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
