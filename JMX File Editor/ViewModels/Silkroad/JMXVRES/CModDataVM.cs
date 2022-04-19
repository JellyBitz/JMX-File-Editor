using JMXFileEditor.Silkroad.Data.JMXVRES.ModData;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Common;

using System;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
    public class CModDataVM : JMXAbstract
    {
        #region Constructor

        public CModDataVM(string Name, Type[] AvailableTypes, Type CurrentType = null, object CurrentObject = null)
            : base(Name, AvailableTypes, CurrentType, CurrentObject)
        {
            // Add abstract formats
            AddFormatHandler(typeof(ModDataMtrl), (s, e) =>
            {
                var vm = new CModDataMtrlVM(string.Empty, e.Obj is ModDataMtrl _obj ? _obj : new ModDataMtrl());
                foreach (var c in vm.Childs)
                    e.Childs.Add(c);
            });
            AddFormatHandler(typeof(ModDataTexAni), (s, e) =>
            {
                var vm = new CModDataTexAniVM(string.Empty, e.Obj is ModDataTexAni _obj ? _obj : new ModDataTexAni());
                foreach (var c in vm.Childs)
                    e.Childs.Add(c);
            });
            AddFormatHandler(typeof(ModDataMultiTex), (s, e) =>
            {
                var vm = new CModDataMultiTexVM(string.Empty, e.Obj is ModDataMultiTex _obj ? _obj : new ModDataMultiTex());
                foreach (var c in vm.Childs)
                    e.Childs.Add(c);
            });
            AddFormatHandler(typeof(ModDataMultiTexRev), (s, e) =>
            {
                var vm = new CModDataMultiTexRevVM(string.Empty, e.Obj is ModDataMultiTexRev _obj ? _obj : new ModDataMultiTexRev());
                foreach (var c in vm.Childs)
                    e.Childs.Add(c);
            });
            AddFormatHandler(typeof(ModDataParticle), (s, e) =>
            {
                var vm = new CModDataParticleVM(string.Empty, e.Obj is ModDataParticle _obj ? _obj : new ModDataParticle());
                foreach (var c in vm.Childs)
                    e.Childs.Add(c);
            });
            AddFormatHandler(typeof(ModDataEnvMap), (s, e) =>
            {
                var vm = new CModDataEnvMapVM(string.Empty, e.Obj is ModDataEnvMap _obj ? _obj : new ModDataEnvMap());
                foreach (var c in vm.Childs)
                    e.Childs.Add(c);
            });
            AddFormatHandler(typeof(ModDataBumpEnv), (s, e) =>
            {
                var vm = new CModDataBumpEnvVM(string.Empty, e.Obj is ModDataBumpEnv _obj ? _obj : new ModDataBumpEnv());
                foreach (var c in vm.Childs)
                    e.Childs.Add(c);
            });
            AddFormatHandler(typeof(ModDataSound), (s, e) =>
            {
                var vm = new CModDataSoundVM(string.Empty, e.Obj is ModDataSound _obj ? _obj : new ModDataSound());
                foreach (var c in vm.Childs)
                    e.Childs.Add(c);
            });
            AddFormatHandler(typeof(ModDataDyVertex), (s, e) =>
            {
                // Empty
            });
            AddFormatHandler(typeof(ModDataDyJoint), (s, e) =>
            {
                // Empty
            });
            AddFormatHandler(typeof(ModDataDyLattice), (s, e) =>
            {
                // Empty
            });
            AddFormatHandler(typeof(ModDataProgEquipPow), (s, e) =>
            {
                // Empty
            });

            // Update values with new formats
            SetCurrentType(CurrentType, CurrentObject);
        }

        #endregion Constructor

        #region Public Methods

        public override object GetClassFrom(JMXStructure Structure)
        {
            if (CurrentType != null)
            {
                // Check abstract formats
                if (CurrentType == typeof(ModDataMtrl))
                    return new CModDataMtrlVM(string.Empty, new ModDataMtrl()).GetClassFrom(this);
                else if (CurrentType == typeof(ModDataTexAni))
                    return new CModDataTexAniVM(string.Empty, new ModDataTexAni()).GetClassFrom(this);
                else if (CurrentType == typeof(ModDataMultiTex))
                    return new CModDataMultiTexVM(string.Empty, new ModDataMultiTex()).GetClassFrom(this);
                else if (CurrentType == typeof(ModDataMultiTexRev))
                    return new CModDataMultiTexRevVM(string.Empty, new ModDataMultiTexRev()).GetClassFrom(this);
                else if (CurrentType == typeof(ModDataParticle))
                    return new CModDataParticleVM(string.Empty, new ModDataParticle()).GetClassFrom(this);
                else if (CurrentType == typeof(ModDataEnvMap))
                    return new CModDataEnvMapVM(string.Empty, new ModDataEnvMap()).GetClassFrom(this);
                else if (CurrentType == typeof(ModDataBumpEnv))
                    return new CModDataBumpEnvVM(string.Empty, new ModDataBumpEnv()).GetClassFrom(this);
                else if (CurrentType == typeof(ModDataSound))
                    return new CModDataSoundVM(string.Empty, new ModDataSound()).GetClassFrom(this);
                else if (CurrentType == typeof(ModDataDyVertex))
                    return new ModDataDyVertex();
                else if (CurrentType == typeof(ModDataDyJoint))
                    return new ModDataDyJoint();
                else if (CurrentType == typeof(ModDataDyLattice))
                    return new ModDataDyLattice();
                else if (CurrentType == typeof(ModDataProgEquipPow))
                    return new ModDataProgEquipPow();
            }
            return null;
        }

        #endregion Public Methods

        #region Internal Classes

        public class CModDataMtrlVM : JMXStructure
        {
            #region Constructor

            public CModDataMtrlVM(string Name, ModDataMtrl Mtrl) : base(Name, true)
            {
                // add formats
                AddFormatHandler(typeof(ModDataMtrl.GradientKey), (s, e) =>
                {
                    e.Childs.Add(new GradientKey("[" + e.Childs.Count + "]", e.Obj is ModDataMtrl.GradientKey _obj ? _obj : new ModDataMtrl.GradientKey()));
                });
                AddFormatHandler(typeof(ModDataMtrl.CurveKey), (s, e) =>
                {
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

            #endregion Constructor

            #region Public Methods

            public override object GetClassFrom(JMXStructure Structure)
            {
                ModDataMtrl obj = new ModDataMtrl
                {
                    UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
                    UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[1]).Value,
                    UnkUInt08 = (uint)((JMXAttribute)Structure.Childs[2]).Value,
                    GradientKeys = ((JMXStructure)Structure.Childs[3]).GetChildList<ModDataMtrl.GradientKey>(),
                    CurveKeys = ((JMXStructure)Structure.Childs[4]).GetChildList<ModDataMtrl.CurveKey>(),
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

            #endregion Public Methods

            #region Internal Classes

            public class GradientKey : JMXStructure
            {
                public GradientKey(string Name, ModDataMtrl.GradientKey Key) : base(Name, true)
                {
                    // Add format
                    AddFormatHandler(typeof(Color4), (s, e) =>
                    {
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

            #endregion Internal Classes
        }

        public class CModDataTexAniVM : JMXStructure
        {
            #region Constructor

            public CModDataTexAniVM(string Name, ModDataTexAni TexAni) : base(Name, true)
            {
                // create nodes
                Childs.Add(new JMXAttribute("UnkUInt06", TexAni.UnkUInt06));
                Childs.Add(new JMXAttribute("UnkUInt07", TexAni.UnkUInt07));
                Childs.Add(new JMXAttribute("UnkUInt08", TexAni.UnkUInt08));
                Childs.Add(new JMXAttribute("UnkUInt09", TexAni.UnkUInt09));
                Childs.Add(new JMXAttribute("UnkUInt10", TexAni.UnkUInt10));
                Childs.Add(new Matrix3DVM("Matrix", TexAni.Matrix));
            }

            #endregion Constructor

            #region Public Methods

            public override object GetClassFrom(JMXStructure Structure)
            {
                ModDataTexAni obj = new ModDataTexAni()
                {
                    UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
                    UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[1]).Value,
                    UnkUInt08 = (uint)((JMXAttribute)Structure.Childs[2]).Value,
                    UnkUInt09 = (uint)((JMXAttribute)Structure.Childs[3]).Value,
                    UnkUInt10 = (uint)((JMXAttribute)Structure.Childs[4]).Value,
                    Matrix = (Matrix4x4)((Matrix3DVM)Structure.Childs[5]).GetClass()
                };
                return obj;
            }

            #endregion Public Methods
        }

        public class CModDataMultiTexVM : JMXStructure
        {
            #region Constructor

            public CModDataMultiTexVM(string Name, ModDataMultiTex MultiTex) : base(Name, true)
            {
                // create nodes
                Childs.Add(new JMXAttribute("UnkUInt06", MultiTex.UnkUInt06));
                Childs.Add(new JMXAttribute("Path", MultiTex.Path));
                Childs.Add(new JMXAttribute("UnkUInt07", MultiTex.UnkUInt07));
            }

            #endregion Constructor

            #region Public Methods

            public override object GetClassFrom(JMXStructure Structure)
            {
                ModDataMultiTex obj = new ModDataMultiTex()
                {
                    UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
                    Path = (string)((JMXAttribute)Structure.Childs[1]).Value,
                    UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[2]).Value
                };
                return obj;
            }

            #endregion Public Methods
        }

        public class CModDataMultiTexRevVM : JMXStructure
        {
            #region Constructor

            public CModDataMultiTexRevVM(string Name, ModDataMultiTexRev MultiTexRev) : base(Name, true)
            {
                // create nodes
                Childs.Add(new JMXAttribute("UnkUInt06", MultiTexRev.UnkUInt06));
                Childs.Add(new JMXAttribute("Path", MultiTexRev.Path));
                Childs.Add(new JMXAttribute("UnkUInt07", MultiTexRev.UnkUInt07));
            }

            #endregion Constructor

            #region Public Methods

            public override object GetClassFrom(JMXStructure Structure)
            {
                ModDataMultiTex obj = new ModDataMultiTex()
                {
                    UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
                    Path = (string)((JMXAttribute)Structure.Childs[1]).Value,
                    UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[2]).Value
                };
                return obj;
            }

            #endregion Public Methods
        }

        public class CModDataParticleVM : JMXStructure
        {
            #region Constructor

            public CModDataParticleVM(string Name, ModDataParticle Particle) : base(Name, true)
            {
                // add formats
                AddFormatHandler(typeof(ModDataParticleConfig), (s, e) =>
                {
                    e.Childs.Add(new Particle("[" + e.Childs.Count + "]", e.Obj is ModDataParticleConfig _obj ? _obj : new ModDataParticleConfig()));
                });
                // create nodes
                AddChildArray("Particles", Particle.Particles.ToArray(), true, true);
            }

            #endregion Constructor

            #region Public Methods

            public override object GetClassFrom(JMXStructure Structure)
            {
                ModDataParticle obj = new ModDataParticle()
                {
                    Particles = ((JMXStructure)Structure.Childs[0]).GetChildList<ModDataParticleConfig>()
                };
                return obj;
            }

            #endregion Public Methods

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

            #endregion Internal Classes
        }

        public class CModDataEnvMapVM : JMXStructure
        {
            #region Constructor

            public CModDataEnvMapVM(string Name, ModDataEnvMap EnvMap) : base(Name, true)
            {
                // create nodes
                Childs.Add(new JMXAttribute("UnkUInt06", EnvMap.UnkUInt06));
                Childs.Add(new JMXAttribute("UnkUInt07", EnvMap.UnkUInt07));
                Childs.Add(new JMXAttribute("UnkUInt08", EnvMap.UnkUInt08));
                Childs.Add(new JMXAttribute("UnkUInt09", EnvMap.UnkUInt09));
            }

            #endregion Constructor

            #region Public Methods

            public override object GetClassFrom(JMXStructure Structure)
            {
                ModDataEnvMap obj = new ModDataEnvMap()
                {
                    UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
                    UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[1]).Value,
                    UnkUInt08 = (uint)((JMXAttribute)Structure.Childs[2]).Value,
                    UnkUInt09 = (uint)((JMXAttribute)Structure.Childs[3]).Value,
                };
                return obj;
            }

            #endregion Public Methods
        }

        public class CModDataBumpEnvVM : JMXStructure
        {
            #region Constructor

            public CModDataBumpEnvVM(string Name, ModDataBumpEnv BumpEnv) : base(Name, true)
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

            #endregion Constructor

            #region Public Methods

            public override object GetClassFrom(JMXStructure Structure)
            {
                ModDataBumpEnv obj = new ModDataBumpEnv()
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

            #endregion Public Methods
        }

        public class CModDataSoundVM : JMXStructure
        {
            #region Constructor

            public CModDataSoundVM(string Name, ModDataSound Sound) : base(Name, true)
            {
                // add formats
                AddFormatHandler(typeof(ModDataSoundSet), (s, e) =>
                {
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

            #endregion Constructor

            #region Public Methods

            public override object GetClassFrom(JMXStructure Structure)
            {
                ModDataSound obj = new ModDataSound()
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
                    SoundSet = ((JMXStructure)Structure.Childs[11]).GetChildList<ModDataSoundSet>()
                };
                return obj;
            }

            #endregion Public Methods

            #region Internal Classes

            public class SndSet : JMXStructure
            {
                #region Constructor

                public SndSet(string Name, ModDataSoundSet Set) : base(Name, true)
                {
                    // add formats
                    AddFormatHandler(typeof(ModDataSoundTrack), (s, e) =>
                    {
                        e.Childs.Add(new Track("[" + e.Childs.Count + "]", e.Obj is ModDataSoundTrack _obj ? _obj : new ModDataSoundTrack()));
                    });
                    // create nodes
                    Childs.Add(new JMXAttribute("Name", Set.Name));
                    AddChildArray("Tracks", Set.Tracks.ToArray(), true, true);
                }

                #endregion Constructor

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

                #endregion Public Methods

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

                #endregion Internal Classes
            }

            #endregion Internal Classes
        }

        #endregion Internal Classes
    }
}