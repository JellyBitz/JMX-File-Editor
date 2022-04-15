using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Utility;
using System.Collections.Generic;
using System.IO;
namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
	/// <summary>
	/// Joymax Resource File
	/// <para>https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/JMXVRES </para>
	/// </summary>
	public class JMXVRES_0109 : IJMXFile
	{
		#region Public Properties
		/// <summary>
		/// Original header used by Joymax
		/// </summary>
		public const string FileHeader = "JMXVRES 0109";
		public string Header { get; set; }
		public uint MaterialFileOffset { get; private set; }
		public uint MeshFileOffset { get; private set; }
		public uint SkeletonFileOffset { get; private set; }
		public uint AnimationFileOffset { get; private set; }
		public uint MeshGroupFileOffset { get; private set; }
		public uint AnimationGroupFileOffset { get; private set; }
		public uint ModPaletteFileOffset { get; private set; }
		public uint CollisionFileOffset { get; private set; }
		public uint FlagUInt01 { get; set; }
		public uint FlagUInt02 { get; set; }
		public uint FlagUInt03 { get; set; }
		public uint FlagUInt04 { get; set; }
		public uint FlagUInt05 { get; set; }
		public ResourceType Type { get; set; }
		public string Name { get; set; } = string.Empty;
		public uint UnkUInt01 { get; set; }
		public uint UnkUInt02 { get; set; }
		public byte[] UnkBuffer { get; set; } = new byte[40];
		public string CollisionMesh { get; set; } = string.Empty;
		public BoundingBox CollisionBox01 { get; set; }
		public BoundingBox CollisionBox02 { get; set; }
		public bool UseCollisionMatrix { get; set; }
		public Matrix3D CollisionMatrix { get; set; } = new Matrix3D();
		public List<CPrimMtrlSet> MaterialSet { get; set; }
		public List<CPrimMesh> MeshSet { get; set; }
		public uint AnimationTypeVersion { get; set; }
		public uint AnimationTypeUserDefine { get; set; }
		public List<CPrimAni> AnimationSet { get; set; }
		public bool UseSkeleton { get; set; }
		public string SkeletonPath { get; set; } = string.Empty;
		public string AttachmentBone { get; set; } = string.Empty;
		public List<CPrimMeshGroup> MeshGroups { get; set; }
		public List<CPrimAniGroup> AnimationGroups { get; set; }
		public List<CModDataSet> SystemModSets { get; set; } = new List<CModDataSet>();
		public List<CModDataSet> AniModSets { get; set; } = new List<CModDataSet>();
		public CResAttachable ResourceAttachable { get; set; } = new CResAttachable();
		public byte[] NonDecodedBytes { get; set; }
		#endregion

		#region Public Methods
		/// <summary>
		/// Calculate and update the values from file offsets
		/// </summary>
		public void UpdateFileOffsets()
		{
			CollisionFileOffset = (uint)(Header.Length + (8 * 4) + (5 * 4) + (4) + (4 + Name.Length) + 8 + UnkBuffer.Length);
			MaterialFileOffset = CollisionFileOffset + (uint)((4 + CollisionMesh.Length) + (24 + 24) + (4 + (UseCollisionMatrix ? 64 : 0)));
			MeshFileOffset = MaterialFileOffset + 4;
			for (int i = 0; i < MaterialSet.Count; i++)
			{
				MeshFileOffset += (uint)(4 + (4 + MaterialSet[i].Path.Length));
			}
			AnimationFileOffset = MeshFileOffset + 4;
			for (int i = 0; i < MeshSet.Count; i++)
			{
				AnimationFileOffset += (uint)((4 + MeshSet[i].Path.Length) + (FlagUInt01 == 1 ? 4 : 0));
			}
			SkeletonFileOffset = AnimationFileOffset + 8 + 4;
			for (int i = 0; i < AnimationSet.Count; i++)
			{
				SkeletonFileOffset += (uint)(4 + AnimationSet[i].Path.Length);
			}
			MeshGroupFileOffset = SkeletonFileOffset + 4;
			if (UseSkeleton)
			{
				MeshGroupFileOffset += (uint)((4 + SkeletonPath.Length) + (4 + AttachmentBone.Length));
			}
			AnimationGroupFileOffset = MeshGroupFileOffset + 4;
			for (int i = 0; i < MeshGroups.Count; i++)
			{
				AnimationGroupFileOffset += (uint)((4 + MeshGroups[i].Name.Length) + (4 + MeshGroups[i].MeshSetIndexes.Count * 4));
			}
			ModPaletteFileOffset = AnimationGroupFileOffset + 4;
			for (int i = 0; i < AnimationGroups.Count; i++)
			{
				ModPaletteFileOffset += (uint)((4 + AnimationGroups[i].Name.Length) + 4);
				for (int j = 0; j < AnimationGroups[i].Entries.Count; j++)
				{
					ModPaletteFileOffset += (uint)(4 + 4 + (4 + AnimationGroups[i].Entries[j].Events.Count * 16) + 4 + (4 + AnimationGroups[i].Entries[j].WalkGraph.Count * 8));
				}
			}
		}
		#endregion

		#region Interface Implementation
		public string Format { get { return FileHeader; } }
		public string Extension { get; } = "bsr";

		public void Load(FileStream FileStream)
		{
			// Read file structure
			using (var br = new BinaryReader(FileStream, System.Text.Encoding.ASCII))
			{
				// Signature
				Header = new string(br.ReadChars(12));

				// File Offsets
				MaterialFileOffset = br.ReadUInt32();
				MeshFileOffset = br.ReadUInt32();
				SkeletonFileOffset = br.ReadUInt32();
				AnimationFileOffset = br.ReadUInt32();
				MeshGroupFileOffset = br.ReadUInt32();
				AnimationGroupFileOffset = br.ReadUInt32();
				ModPaletteFileOffset = br.ReadUInt32();
				CollisionFileOffset = br.ReadUInt32();

				// Unknown flags
				FlagUInt01 = br.ReadUInt32();
				FlagUInt02 = br.ReadUInt32();
				FlagUInt03 = br.ReadUInt32();
				FlagUInt04 = br.ReadUInt32();
				FlagUInt05 = br.ReadUInt32();

				// Object info
				Type = (ResourceType)br.ReadUInt32();
				Name = br.ReadString32();
				UnkUInt01 = br.ReadUInt32();
				UnkUInt02 = br.ReadUInt32();
				// Reserved
				UnkBuffer = br.ReadBytes(40);

				// FileOffset.Collision
				CollisionMesh = br.ReadString32();
				CollisionBox01 = new BoundingBox(new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle()), new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle()));
				CollisionBox02 = new BoundingBox(new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle()), new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle()));
				UseCollisionMatrix = br.ReadUInt32() != 0;
				if (UseCollisionMatrix)
				{
					var matrix = br.ReadSingleArray(16);
					CollisionMatrix.Data = new System.Windows.Media.Media3D.Matrix3D(
						matrix[0], matrix[1], matrix[2], matrix[3],
						matrix[4], matrix[5], matrix[6], matrix[7],
						matrix[8], matrix[9], matrix[10], matrix[11],
						matrix[12], matrix[13], matrix[14], matrix[15]);
				}

				// FileOffset.Material
				var count = br.ReadInt32();
				MaterialSet = new List<CPrimMtrlSet>();
				for (int i = 0; i < count; i++)
				{
					// create
					MaterialSet.Add(new CPrimMtrlSet()
					{
						Index = br.ReadUInt32(),
						Path = br.ReadString32()
					});
				}

				// FileOffset.Mesh
				count = br.ReadInt32();
				MeshSet = new List<CPrimMesh>();
				for (int i = 0; i < count; i++)
				{
					// create
					MeshSet.Add(new CPrimMesh()
					{
						Path = br.ReadString32(),
						UnkUInt01 = FlagUInt01 == 1 ? br.ReadUInt32() : 0
					});
				}

				// FileOffset.Animation
				AnimationTypeVersion = br.ReadUInt32();
				AnimationTypeUserDefine = br.ReadUInt32();
				count = br.ReadInt32();
				AnimationSet = new List<CPrimAni>();
				for (int i = 0; i < count; i++)
				{
					// create
					AnimationSet.Add(new CPrimAni()
					{
						Path = br.ReadString32(),
					});
				}

				// FileOffset.Skeleton
				UseSkeleton = br.ReadUInt32() != 0;
				if (UseSkeleton)
				{
					SkeletonPath = br.ReadString32();
					AttachmentBone = br.ReadString32();
				}

				// FileOffset.MeshGroup
				count = br.ReadInt32();
				MeshGroups = new List<CPrimMeshGroup>();
				for (int i = 0; i < count; i++)
				{
					// create
					MeshGroups.Add(new CPrimMeshGroup()
					{
						Name = br.ReadString32(),
						MeshSetIndexes = new List<uint>(br.ReadUInt32Array(br.ReadInt32()))
					});
				}

				// FileOffset.AnimationGroup
				count = br.ReadInt32();
				AnimationGroups = new List<CPrimAniGroup>();
				for (int i = 0; i < count; i++)
				{
					// create
					var animationGroup = new CPrimAniGroup();
					AnimationGroups.Add(animationGroup);
					// read
					animationGroup.Name = br.ReadString32();
					var animationEntryCount = br.ReadInt32();
					animationGroup.Entries = new List<CPrimAniGroup.Entry>(animationEntryCount);
					for (int j = 0; j < animationEntryCount; j++)
					{
						// create
						var entry = new CPrimAniGroup.Entry();
						animationGroup.Entries.Add(entry);
						// read
						entry.Type = (CPrimAnimationType)br.ReadUInt32();
						entry.AnimationSetIndex = br.ReadUInt32();
						var eventCount = br.ReadInt32();
						entry.Events = new List<CPrimAniGroup.Entry.Event>(eventCount);
						for (int k = 0; k < eventCount; k++)
						{
							// create
							entry.Events.Add(new CPrimAniGroup.Entry.Event()
							{
								Time = br.ReadUInt32(),
								Type = br.ReadUInt32(),
								UnkUInt01 = br.ReadUInt32(),
								UnkUInt02 = br.ReadUInt32()
							});
						}
						var WalkGraphPointCount = br.ReadInt32();
						entry.WalkLength = br.ReadSingle();
						entry.WalkGraph = new List<Vector2>(WalkGraphPointCount);
						for (int k = 0; k < WalkGraphPointCount; k++)
						{
							// create
							entry.WalkGraph.Add(new Vector2()
							{
								X = br.ReadSingle(),
								Y = br.ReadSingle()
							});
						}
					}
				}

				// FileOffset.ModPalette
				for (int x = 0; x < 2; x++)
				{
					// Quick variable to resume code
					var modSet = x == 0 ? SystemModSets : AniModSets;

					count = br.ReadInt32();
					for (int i = 0; i < count; i++)
					{
						// create
						var modDataSet = new CModDataSet();
						modSet.Add(modDataSet);
						// read
						modDataSet.Type = br.ReadUInt32();
						modDataSet.AnimationType = (CPrimAnimationType)br.ReadUInt32();
						modDataSet.Name = br.ReadString32();

						var modsDataCount = br.ReadInt32();
						modDataSet.ModsData = new List<CModData>();
						for (int j = 0; j < modsDataCount; j++)
						{
							// create
							CModDataType modDataType = (CModDataType)br.ReadUInt32();
							CModData modData = null;
							switch (modDataType)
							{
								case CModDataType.ModDataMtrl: modData = new CModDataMtrl(); break;
								case CModDataType.ModDataTexAni: modData = new CModDataTexAni(); break;
								case CModDataType.ModDataMultiTex: modData = new CModDataMultiTex(); break;
								case CModDataType.ModDataMultiTexRev: modData = new CModDataMultiTexRev(); break;
								case CModDataType.ModDataParticle: modData = new CModDataParticle(); break;
								case CModDataType.ModDataEnvMap: modData = new CModDataEnvMap(); break;
								case CModDataType.ModDataBumpEnv: modData = new CModDataBumpEnv(); break;
								case CModDataType.ModDataSound: modData = new CModDataSound(); break;
								case CModDataType.ModDataDyVertex: modData = new CModDataDyVertex(); break;
								case CModDataType.ModDataDyJoint: modData = new CModDataDyJoint(); break;
								case CModDataType.ModDataDyLattice: modData = new CModDataDyLattice(); break;
								case CModDataType.ModDataProgEquipPow: modData = new CModDataProgEquipPow(); break;
								default: throw new System.NotImplementedException();
							}
							modDataSet.ModsData.Add(modData);
							// read base
							modData.UnkFloat01 = br.ReadSingle(); // 0.5
							modData.UnkUInt01 = br.ReadUInt32(); // 2 (ModDataMtrl)
							modData.UnkUInt02 = br.ReadUInt32(); // 272 (ModDataMtrl)
							modData.UnkUInt03 = br.ReadUInt32(); // 7 (ModDataMtrl)
							modData.UnkUInt04 = br.ReadUInt32();
							modData.UnkUInt05 = br.ReadUInt32();
							modData.UnkByte01 = br.ReadByte();
							modData.UnkByte02 = br.ReadByte();
							modData.UnkByte03 = br.ReadByte();
							modData.UnkByte04 = br.ReadByte();
							// read polimorfism
							switch (modDataType)
							{
								case CModDataType.ModDataMtrl:
									{
										CModDataMtrl data = (CModDataMtrl)modData;
										data.UnkUInt06 = br.ReadUInt32(); // 1000
										data.UnkUInt07 = br.ReadUInt32(); // 2
										data.UnkUInt08 = br.ReadUInt32();
										var gradientKeyCount = br.ReadInt32();
										for (int k = 0; k < gradientKeyCount; k++)
										{
											data.GradientKeys.Add(new CModDataMtrl.GradientKey()
											{
												Time = br.ReadUInt32(),
												Value = new Color4(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle())
											}); ;
										}
										if ((data.UnkUInt07 & 4) != 0)
										{
											var curveKeyCount = br.ReadInt32();
											for (int k = 0; k < curveKeyCount; k++)
											{
												data.CurveKeys.Add(new CModDataMtrl.CurveKey()
												{
													Time = br.ReadUInt32(),
													Value = br.ReadSingle()
												}); ;
											}
										}
										data.UnkUInt09 = br.ReadUInt32();
										data.UnkUInt10 = br.ReadUInt32(); // 1
										data.UnkUInt11 = br.ReadUInt32(); // 1
										data.UnkUInt12 = br.ReadUInt32();
										data.UnkUInt13 = br.ReadUInt32(); // 262661 ?
										data.UnkUInt14 = br.ReadUInt32(); // 33686530 ?
										data.UnkUInt15 = br.ReadUInt32(); // 3361998720 ?
										data.UnkUInt16 = br.ReadUInt32(); // 1065353216 ?
										data.UnkUInt17 = br.ReadUInt32(); // 1
									}
									break;
								case CModDataType.ModDataTexAni:
									{
										CModDataTexAni data = (CModDataTexAni)modData;
										data.UnkUInt06 = br.ReadUInt32();
										data.UnkUInt07 = br.ReadUInt32();
										data.UnkUInt08 = br.ReadUInt32();
										data.UnkUInt09 = br.ReadUInt32();
										data.UnkUInt10 = br.ReadUInt32();
										var matrix = br.ReadSingleArray(16);
										data.Matrix.Data = new System.Windows.Media.Media3D.Matrix3D(
											matrix[0], matrix[1], matrix[2], matrix[3],
											matrix[4], matrix[5], matrix[6], matrix[7],
											matrix[8], matrix[9], matrix[10], matrix[11],
											matrix[12], matrix[13], matrix[14], matrix[15]);
									}
									break;
								case CModDataType.ModDataMultiTex:
									{
										CModDataMultiTex data = (CModDataMultiTex)modData;
										data.UnkUInt06 = br.ReadUInt32();
										data.Path = br.ReadString32();
										data.UnkUInt07 = br.ReadUInt32();
									}
									break;
								case CModDataType.ModDataMultiTexRev:
									{
										CModDataMultiTexRev data = (CModDataMultiTexRev)modData;
										data.UnkUInt06 = br.ReadUInt32();
										data.Path = br.ReadString32();
										data.UnkUInt07 = br.ReadUInt32();
									}
									break;
								case CModDataType.ModDataParticle:
									{
										CModDataParticle data = (CModDataParticle)modData;
										var particleCount = br.ReadInt32();
										for (int k = 0; k < particleCount; k++)
										{
											var particle = new CModDataParticle.Particle()
											{
												IsEnabled = br.ReadUInt32() != 0,
												Path = br.ReadString32(),
												BoneRelative = br.ReadString32(),
												Position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle()),
												BirthTime = br.ReadUInt32(),
												UnkByte01 = br.ReadByte(),
												UnkByte02 = br.ReadByte(),
												UnkByte03 = br.ReadByte(),
												UnkByte04 = br.ReadByte()
											};
											if (particle.UnkByte04 == 1)
											{
												particle.UnkVector01 = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
											}
											data.Particles.Add(particle);
										}
									}
									break;
								case CModDataType.ModDataEnvMap:
									{
										CModDataEnvMap data = (CModDataEnvMap)modData;
										data.UnkUInt06 = br.ReadUInt32();
										data.UnkUInt07 = br.ReadUInt32();
										data.UnkUInt08 = br.ReadUInt32();
										data.UnkUInt09 = br.ReadUInt32();
									}
									break;
								case CModDataType.ModDataBumpEnv:
									{
										CModDataBumpEnv data = (CModDataBumpEnv)modData;
										data.UnkFloat02 = br.ReadSingle();
										data.UnkFloat03 = br.ReadSingle();
										data.UnkFloat04 = br.ReadSingle();
										data.UnkFloat05 = br.ReadSingle();
										data.UnkFloat06 = br.ReadSingle();
										data.UnkFloat07 = br.ReadSingle();
										var textureCount = br.ReadInt32();
										for (int k = 0; k < textureCount; k++)
										{
											// Check if entry is not enabled
											if (br.ReadByte() == 0)
												continue;
											data.Textures.Add(br.ReadString32());
										}
									}
									break;
								case CModDataType.ModDataSound:
									{
										// Check working set
										var soundSetCount = br.ReadInt32();
										if (soundSetCount <= 0)
											break;
										// Continue reading
										CModDataSound data = (CModDataSound)modData;
										data.UnkUInt06 = br.ReadUInt32();
										data.UnkUInt07 = br.ReadUInt32();
										data.UnkUInt08 = br.ReadUInt32();
										data.UnkFloat02 = br.ReadSingle(); // 10
										data.UnkFloat03 = br.ReadSingle(); // 100
										data.UnkUInt09 = br.ReadUInt32();
										data.UnkUInt10 = br.ReadUInt32();
										data.UnkUInt11 = br.ReadUInt32();
										data.UnkUInt12 = br.ReadUInt32();
										data.UnkUInt13 = br.ReadUInt32();
										data.UnkUInt14 = br.ReadUInt32();
										for (int k = 0; k < soundSetCount; k++)
										{
											CModDataSound.SndSet sndSet = new CModDataSound.SndSet()
											{
												Name = br.ReadString32() // default
											};
											var trackCount = br.ReadInt32();
											for (int l = 0; l < trackCount; l++)
											{
												// Check if entry is not enabled
												if (br.ReadUInt32() == 0)
													continue;
												sndSet.Tracks.Add(new CModDataSound.SndSet.Track()
												{
													Path = br.ReadString32(),
													Time = br.ReadUInt32(),
													Event = br.ReadString32()
												});
											}
											data.SoundSet.Add(sndSet);
										}
									}
									break;
								case CModDataType.ModDataDyVertex:
								case CModDataType.ModDataDyJoint:
								case CModDataType.ModDataDyLattice:
								case CModDataType.ModDataProgEquipPow:
									// No additional data
									break;
							}
						}
					}
				}
				if (Type == ResourceType.Character || Type == ResourceType.Item)
				{
					ResourceAttachable.UnkUInt01 = br.ReadUInt32();
					ResourceAttachable.UnkUInt02 = br.ReadUInt32();
					ResourceAttachable.AttachMethod = br.ReadUInt32();
					count = br.ReadInt32();
					for (int i = 0; i < count; i++)
					{
						ResourceAttachable.Slots.Add(new CResAttachable.Slot()
						{
							Index = br.ReadUInt32(),
							MeshSetIndex = br.ReadUInt32(),
						});
					}
					if (Type == ResourceType.Character)
					{
						ResourceAttachable.nComboNum = br.ReadUInt32();
					}
				}
			}
		}
		public void Save(string Path)
		{
			// Override file structure
			using (BinaryWriter bw = new BinaryWriter(new FileStream(Path, FileMode.Create, FileAccess.Write), System.Text.Encoding.ASCII))
			{
				// Signature
				bw.Write(Header.ToCharArray());

				// Recalculate file offsets for safety
				UpdateFileOffsets();

				// File Offsets
				bw.Write(MaterialFileOffset);
				bw.Write(MeshFileOffset);
				bw.Write(SkeletonFileOffset);
				bw.Write(AnimationFileOffset);
				bw.Write(MeshGroupFileOffset);
				bw.Write(AnimationGroupFileOffset);
				bw.Write(ModPaletteFileOffset);
				bw.Write(CollisionFileOffset);

				// Unknown Flags
				bw.Write(FlagUInt01);
				bw.Write(FlagUInt02);
				bw.Write(FlagUInt03);
				bw.Write(FlagUInt04);
				bw.Write(FlagUInt05);

				// Object Info
				bw.Write((uint)Type);
				bw.WriteString32(Name);
				bw.Write(UnkUInt01);
				bw.Write(UnkUInt02);
				bw.Write(UnkBuffer);

				// FileOffset.Collision
				bw.WriteString32(CollisionMesh);
				bw.Write(CollisionBox01.Min.X); bw.Write(CollisionBox01.Min.Y); bw.Write(CollisionBox01.Min.Z); bw.Write(CollisionBox01.Max.X); bw.Write(CollisionBox01.Max.Y); bw.Write(CollisionBox01.Max.Z);
				bw.Write(CollisionBox02.Min.X); bw.Write(CollisionBox02.Min.Y); bw.Write(CollisionBox02.Min.Z); bw.Write(CollisionBox02.Max.X); bw.Write(CollisionBox02.Max.Y); bw.Write(CollisionBox02.Max.Z);
				bw.Write(UseCollisionMatrix ? 1u : 0u);
				if (UseCollisionMatrix)
				{
					// Matrix
					bw.Write((float)CollisionMatrix.Data.M11); bw.Write((float)CollisionMatrix.Data.M12); bw.Write((float)CollisionMatrix.Data.M13); bw.Write((float)CollisionMatrix.Data.M14);
					bw.Write((float)CollisionMatrix.Data.M21); bw.Write((float)CollisionMatrix.Data.M22); bw.Write((float)CollisionMatrix.Data.M23); bw.Write((float)CollisionMatrix.Data.M24);
					bw.Write((float)CollisionMatrix.Data.M31); bw.Write((float)CollisionMatrix.Data.M32); bw.Write((float)CollisionMatrix.Data.M33); bw.Write((float)CollisionMatrix.Data.M34);
					bw.Write((float)CollisionMatrix.Data.OffsetX); bw.Write((float)CollisionMatrix.Data.OffsetY); bw.Write((float)CollisionMatrix.Data.OffsetZ); bw.Write((float)CollisionMatrix.Data.M44);
				}

				// FileOffset.Material
				bw.Write(MaterialSet.Count);
				for (int i = 0; i < MaterialSet.Count; i++)
				{
					bw.Write(MaterialSet[i].Index);
					bw.WriteString32(MaterialSet[i].Path);
				}

				// FileOffset.Mesh
				bw.Write(MeshSet.Count);
				for (int i = 0; i < MeshSet.Count; i++)
				{
					bw.WriteString32(MeshSet[i].Path);
					if (FlagUInt01 != 0)
						bw.Write(MeshSet[i].UnkUInt01);
				}

				// FileOffset.Animation
				bw.Write(AnimationTypeVersion);
				bw.Write(AnimationTypeUserDefine);
				bw.Write(AnimationSet.Count);
				for (int i = 0; i < AnimationSet.Count; i++)
				{
					bw.WriteString32(AnimationSet[i].Path);
				}

				// FileOffset.Skeleton
				bw.Write(UseSkeleton ? 1u : 0u);
				if (UseSkeleton)
				{
					bw.WriteString32(SkeletonPath);
					bw.WriteString32(AttachmentBone);
				}

				// FileOffset.MeshGroup
				bw.Write(MeshGroups.Count);
				for (int i = 0; i < MeshGroups.Count; i++)
				{
					bw.WriteString32(MeshGroups[i].Name);
					bw.Write(MeshGroups[i].MeshSetIndexes.Count);
					bw.Write(MeshGroups[i].MeshSetIndexes.ToArray());
				}

				// FileOffset.AnimationGroup
				bw.Write(AnimationGroups.Count);
				for (int i = 0; i < AnimationGroups.Count; i++)
				{
					bw.WriteString32(AnimationGroups[i].Name);
					bw.Write(AnimationGroups[i].Entries.Count);
					for (int j = 0; j < AnimationGroups[i].Entries.Count; j++)
					{
						bw.Write((uint)AnimationGroups[i].Entries[j].Type);
						bw.Write(AnimationGroups[i].Entries[j].AnimationSetIndex);
						bw.Write(AnimationGroups[i].Entries[j].Events.Count);
						for (int k = 0; k < AnimationGroups[i].Entries[j].Events.Count; k++)
						{
							bw.Write(AnimationGroups[i].Entries[j].Events[k].Time);
							bw.Write(AnimationGroups[i].Entries[j].Events[k].Type);
							bw.Write(AnimationGroups[i].Entries[j].Events[k].UnkUInt01);
							bw.Write(AnimationGroups[i].Entries[j].Events[k].UnkUInt02);
						}
						bw.Write(AnimationGroups[i].Entries[j].WalkGraph.Count);
						bw.Write(AnimationGroups[i].Entries[j].WalkLength);
						for (int k = 0; k < AnimationGroups[i].Entries[j].WalkGraph.Count; k++)
						{
							bw.Write(AnimationGroups[i].Entries[j].WalkGraph[k].X);
							bw.Write(AnimationGroups[i].Entries[j].WalkGraph[k].Y);
						}
					}
				}

				// FileOffset.ModPalette
				for (int x = 0; x < 2; x++)
				{
					// Quick variable to resume code
					var modSet = x == 0 ? SystemModSets : AniModSets;

					bw.Write(modSet.Count);
					for (int i = 0; i < modSet.Count; i++)
					{
						bw.Write(modSet[i].Type);
						bw.Write((uint)modSet[i].AnimationType);
						bw.WriteString32(modSet[i].Name);
						bw.Write(modSet[i].ModsData.Count);
						for (int j = 0; j < modSet[i].ModsData.Count; j++)
						{
							var type  = modSet[i].ModsData[j].GetType();
							if (type == typeof(CModDataMtrl))
								bw.Write((uint)CModDataType.ModDataMtrl);
							else if (type == typeof(CModDataTexAni))
								bw.Write((uint)CModDataType.ModDataTexAni);
							else if (type == typeof(CModDataMultiTex))
								bw.Write((uint)CModDataType.ModDataMultiTex);
							else if (type == typeof(CModDataMultiTexRev))
								bw.Write((uint)CModDataType.ModDataMultiTexRev);
							else if (type == typeof(CModDataParticle))
								bw.Write((uint)CModDataType.ModDataParticle);
							else if (type == typeof(CModDataEnvMap))
								bw.Write((uint)CModDataType.ModDataEnvMap);
							else if (type == typeof(CModDataBumpEnv))
								bw.Write((uint)CModDataType.ModDataBumpEnv);
							else if (type == typeof(CModDataSound))
								bw.Write((uint)CModDataType.ModDataSound);
							else if (type == typeof(CModDataDyVertex))
								bw.Write((uint)CModDataType.ModDataDyVertex);
							else if (type == typeof(CModDataDyJoint))
								bw.Write((uint)CModDataType.ModDataDyJoint);
							else if (type == typeof(CModDataDyLattice))
								bw.Write((uint)CModDataType.ModDataDyLattice);
							else if (type == typeof(CModDataProgEquipPow))
								bw.Write((uint)CModDataType.ModDataProgEquipPow);
							// write base
							bw.Write(modSet[i].ModsData[j].UnkFloat01);
							bw.Write(modSet[i].ModsData[j].UnkUInt01);
							bw.Write(modSet[i].ModsData[j].UnkUInt02);
							bw.Write(modSet[i].ModsData[j].UnkUInt03);
							bw.Write(modSet[i].ModsData[j].UnkUInt04);
							bw.Write(modSet[i].ModsData[j].UnkUInt05);
							bw.Write(modSet[i].ModsData[j].UnkByte01);
							bw.Write(modSet[i].ModsData[j].UnkByte02);
							bw.Write(modSet[i].ModsData[j].UnkByte03);
							bw.Write(modSet[i].ModsData[j].UnkByte04);
							// write polimorfism
							if (modSet[i].ModsData[j] is CModDataMtrl mtrl)
							{
								bw.Write(mtrl.UnkUInt06);
								bw.Write(mtrl.UnkUInt07);
								bw.Write(mtrl.UnkUInt08);
								bw.Write(mtrl.GradientKeys.Count);
								for (int k = 0; k < mtrl.GradientKeys.Count; k++)
								{
									bw.Write(mtrl.GradientKeys[k].Time);
									bw.Write(mtrl.GradientKeys[k].Value.Red); bw.Write(mtrl.GradientKeys[k].Value.Green); bw.Write(mtrl.GradientKeys[k].Value.Blue); bw.Write(mtrl.GradientKeys[k].Value.Alpha);
								}
								if ((mtrl.UnkUInt07 & 4) != 0)
								{
									bw.Write(mtrl.CurveKeys.Count);
									for (int k = 0; k < mtrl.CurveKeys.Count; k++)
									{
										bw.Write(mtrl.CurveKeys[k].Time);
										bw.Write(mtrl.CurveKeys[k].Value);
									}
								}
								bw.Write(mtrl.UnkUInt09);
								bw.Write(mtrl.UnkUInt10);
								bw.Write(mtrl.UnkUInt11);
								bw.Write(mtrl.UnkUInt12);
								bw.Write(mtrl.UnkUInt13);
								bw.Write(mtrl.UnkUInt14);
								bw.Write(mtrl.UnkUInt15);
								bw.Write(mtrl.UnkUInt16);
								bw.Write(mtrl.UnkUInt17);
							}
							else if (modSet[i].ModsData[j] is CModDataTexAni texAni)
							{
								bw.Write(texAni.UnkUInt06);
								bw.Write(texAni.UnkUInt07);
								bw.Write(texAni.UnkUInt08);
								bw.Write(texAni.UnkUInt09);
								bw.Write(texAni.UnkUInt10);
								// matrix
								bw.Write((float)texAni.Matrix.Data.M11); bw.Write((float)texAni.Matrix.Data.M12); bw.Write((float)texAni.Matrix.Data.M13); bw.Write((float)texAni.Matrix.Data.M14);
								bw.Write((float)texAni.Matrix.Data.M21); bw.Write((float)texAni.Matrix.Data.M22); bw.Write((float)texAni.Matrix.Data.M23); bw.Write((float)texAni.Matrix.Data.M24);
								bw.Write((float)texAni.Matrix.Data.M31); bw.Write((float)texAni.Matrix.Data.M32); bw.Write((float)texAni.Matrix.Data.M33); bw.Write((float)texAni.Matrix.Data.M34);
								bw.Write((float)texAni.Matrix.Data.OffsetX); bw.Write((float)texAni.Matrix.Data.OffsetY); bw.Write((float)texAni.Matrix.Data.OffsetZ); bw.Write((float)texAni.Matrix.Data.M44);
							}
							else if (modSet[i].ModsData[j] is CModDataMultiTex multiTex)
							{
								bw.Write(multiTex.UnkUInt06);
								bw.WriteString32(multiTex.Path);
								bw.Write(multiTex.UnkUInt07);
							}
							else if (modSet[i].ModsData[j] is CModDataMultiTexRev multiTexRev)
							{
								bw.Write(multiTexRev.UnkUInt06);
								bw.WriteString32(multiTexRev.Path);
								bw.Write(multiTexRev.UnkUInt07);
							}
							else if (modSet[i].ModsData[j] is CModDataParticle particle)
							{
								bw.Write(particle.Particles.Count);
								for (int k = 0; k < particle.Particles.Count; k++)
								{
									bw.Write(particle.Particles[k].IsEnabled ? 1u : 0u);
									bw.WriteString32(particle.Particles[k].Path);
									bw.WriteString32(particle.Particles[k].BoneRelative);
									bw.Write(particle.Particles[k].Position.X); bw.Write(particle.Particles[k].Position.Y); bw.Write(particle.Particles[k].Position.Z);
									bw.Write(particle.Particles[k].BirthTime);
									bw.Write(particle.Particles[k].UnkByte01);
									bw.Write(particle.Particles[k].UnkByte02);
									bw.Write(particle.Particles[k].UnkByte03);
									bw.Write(particle.Particles[k].UnkByte04);
									if (particle.Particles[k].UnkByte04 == 1)
									{
										bw.Write(particle.Particles[k].UnkVector01.X); bw.Write(particle.Particles[k].UnkVector01.Y); bw.Write(particle.Particles[k].UnkVector01.Z);
									}
								}
							}
							else if (modSet[i].ModsData[j] is CModDataEnvMap envMap)
							{
								bw.Write(envMap.UnkUInt06);
								bw.Write(envMap.UnkUInt07);
								bw.Write(envMap.UnkUInt08);
								bw.Write(envMap.UnkUInt09);
							}
							else if (modSet[i].ModsData[j] is CModDataBumpEnv bumpEnv)
							{
								bw.Write(bumpEnv.UnkFloat02);
								bw.Write(bumpEnv.UnkFloat03);
								bw.Write(bumpEnv.UnkFloat04);
								bw.Write(bumpEnv.UnkFloat05);
								bw.Write(bumpEnv.UnkFloat06);
								bw.Write(bumpEnv.UnkFloat07);
								bw.Write(bumpEnv.Textures.Count);
								for (int k = 0; k < bumpEnv.Textures.Count; k++)
								{
									if (bumpEnv.Textures[k].Length == 0)
									{
										bw.Write((byte)0);
										continue;
									}
									bw.Write((byte)1);
									bw.WriteString32(bumpEnv.Textures[k]);
								}
							}
							else if (modSet[i].ModsData[j] is CModDataSound sound)
							{
								bw.Write(sound.SoundSet.Count);
								if (sound.SoundSet.Count == 0)
									continue;
								bw.Write(sound.UnkUInt06);
								bw.Write(sound.UnkUInt07);
								bw.Write(sound.UnkUInt08);
								bw.Write(sound.UnkFloat02);
								bw.Write(sound.UnkFloat03);
								bw.Write(sound.UnkUInt09);
								bw.Write(sound.UnkUInt10);
								bw.Write(sound.UnkUInt11);
								bw.Write(sound.UnkUInt12);
								bw.Write(sound.UnkUInt13);
								bw.Write(sound.UnkUInt14);
								for (int k = 0; k < sound.SoundSet.Count; k++)
								{
									bw.WriteString32(sound.SoundSet[k].Name);
									bw.Write(sound.SoundSet[k].Tracks.Count);
									for (int l = 0; l < sound.SoundSet[k].Tracks.Count; l++)
									{
										bw.Write(1u);
										bw.WriteString32(sound.SoundSet[k].Tracks[l].Path);
										bw.Write(sound.SoundSet[k].Tracks[l].Time);
										bw.WriteString32(sound.SoundSet[k].Tracks[l].Event);
									}
								}
							}
						}
					}
				}

				// Extra
				if (Type == ResourceType.Character || Type == ResourceType.Item)
				{
					bw.Write(ResourceAttachable.UnkUInt01);
					bw.Write(ResourceAttachable.UnkUInt02);
					bw.Write(ResourceAttachable.AttachMethod);
					bw.Write(ResourceAttachable.Slots.Count);
					for (int i = 0; i < ResourceAttachable.Slots.Count; i++)
					{
						bw.Write(ResourceAttachable.Slots[i].Index);
						bw.Write(ResourceAttachable.Slots[i].MeshSetIndex);
					}
					if (Type == ResourceType.Character)
					{
						bw.Write(ResourceAttachable.nComboNum);
					}
				}
			}
		}
		#endregion
	}
}