using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Silkroad.Data.JMXVRES;
using JMXFileEditor.ViewModels.Silkroad.Common;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
	/// <summary>
	/// ViewModel representing the JMXVRES file format
	/// </summary>
	public class JMXVRESVM : JMXStructure
	{
		#region Constructor
		public JMXVRESVM(JMXVRES_0109 JMXFile) : base(JMXFile.Format, true)
		{
			AddChildFormats();
			CreateNodes(JMXFile);
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			JMXVRES_0109 file = new JMXVRES_0109();

			// Signature
			file.Header = (string)((JMXAttribute)Structure.Childs[0]).Value;

			// Unknown flags
			file.FlagUInt01 = (uint)((JMXAttribute)Structure.Childs[9]).Value;
			file.FlagUInt02 = (uint)((JMXAttribute)Structure.Childs[10]).Value;
			file.FlagUInt03 = (uint)((JMXAttribute)Structure.Childs[11]).Value;
			file.FlagUInt04 = (uint)((JMXAttribute)Structure.Childs[12]).Value;
			file.FlagUInt05 = (uint)((JMXAttribute)Structure.Childs[13]).Value;

			// Object info
			file.Type = (ResourceType)((JMXOption)Structure.Childs[14]).Value;
			file.Name = (string)((JMXAttribute)Structure.Childs[15]).Value;
			file.UnkUInt01 = (uint)((JMXAttribute)Structure.Childs[16]).Value;
			file.UnkUInt02 = (uint)((JMXAttribute)Structure.Childs[17]).Value;
			// Reserved
			file.UnkBuffer = ((JMXStructure)Structure.Childs[18]).GetChildList<byte>().ToArray();

			// FileOffset.Collision
			file.CollisionMesh = (string)((JMXAttribute)Structure.Childs[19]).Value;
			file.CollisionBox01 = (BoundingBox)((BoundingBoxVM)Structure.Childs[20]).GetClass();
			file.CollisionBox02 = (BoundingBox)((BoundingBoxVM)Structure.Childs[21]).GetClass();
			file.UseCollisionMatrix = (bool)((JMXAttribute)Structure.Childs[22]).Value;
			file.CollisionMatrix = (Matrix3D)((Matrix3DVM)Structure.Childs[23]).GetClass();

			// FileOffset.Material
			file.MaterialSet = ((JMXStructure)Structure.Childs[24]).GetChildList<CPrimMtrlSet>();

			// FileOffset.Mesh
			file.MeshSet = ((JMXStructure)Structure.Childs[25]).GetChildList<CPrimMesh>();

			// FileOffset.Animation
			file.AnimationTypeVersion = (uint)((JMXAttribute)Structure.Childs[26]).Value;
			file.AnimationTypeUserDefine = (uint)((JMXAttribute)Structure.Childs[27]).Value;
			file.AnimationSet = ((JMXStructure)Structure.Childs[28]).GetChildList<CPrimAni>();

			// FileOffset.Skeleton
			file.UseSkeleton = (bool)((JMXAttribute)Structure.Childs[29]).Value;
			file.SkeletonPath = (string)((JMXAttribute)Structure.Childs[30]).Value;
			file.AttachmentBone = (string)((JMXAttribute)Structure.Childs[31]).Value;

			// FileOffset.MeshGroups
			file.MeshGroups = ((JMXStructure)Structure.Childs[32]).GetChildList<CPrimMeshGroup>();

			// FileOffset.AnimationGroup
			file.AnimationGroups = ((JMXStructure)Structure.Childs[33]).GetChildList<CPrimAniGroup>();

			// FileOffset.ModPalette
			file.SystemModSets = ((JMXStructure)Structure.Childs[34]).GetChildList<CModDataSet>();
			file.AniModSets = ((JMXStructure)Structure.Childs[35]).GetChildList<CModDataSet>();

			// Extra
			file.ResourceAttachable = (CResAttachable)((CResAttachableVM)Structure.Childs[36]).GetClass();

			// Update and return
			file.UpdateFileOffsets();
			return file;
		}
		#endregion

		#region Private Helpers
		/// <summary>
		/// Add new node formats
		/// </summary>
		private void AddChildFormats()
		{
			AddFormatHandler(typeof(CPrimMtrlSet), (s, e) => {
				e.Childs.Add(new CPrimMtrlSetVM("[" + e.Childs.Count + "]", e.Obj is CPrimMtrlSet _obj ? _obj : new CPrimMtrlSet()));
			});
			AddFormatHandler(typeof(CPrimMesh), (s, e) => {
				e.Childs.Add(new CPrimMeshVM("[" + e.Childs.Count + "]", e.Obj is CPrimMesh _obj ? _obj : new CPrimMesh()));
			});
			AddFormatHandler(typeof(CPrimAni), (s, e) => {
				e.Childs.Add(new CPrimAniVM("[" + e.Childs.Count + "]", e.Obj is CPrimAni _obj ? _obj : new CPrimAni()));
			});
			AddFormatHandler(typeof(CPrimMeshGroup), (s, e) => {
				e.Childs.Add(new CPrimMeshGroupVM("[" + e.Childs.Count + "]", e.Obj is CPrimMeshGroup _obj ? _obj : new CPrimMeshGroup()));
			});
			AddFormatHandler(typeof(CPrimAniGroup), (s, e) => {
				e.Childs.Add(new CPrimAniGroupVM("[" + e.Childs.Count + "]", e.Obj is CPrimAniGroup _obj ? _obj : new CPrimAniGroup()));
			});
			AddFormatHandler(typeof(CModDataSet), (s, e) => {
				e.Childs.Add(new CModDataSetVM("[" + e.Childs.Count + "]", e.Obj is CModDataSet _obj ? _obj : new CModDataSet()));
			});
			AddFormatHandler(typeof(CResAttachable), (s, e) => {
				e.Childs.Add(new CResAttachableVM("[" + e.Childs.Count + "]", e.Obj is CResAttachable _obj ? _obj : new CResAttachable()));
			});
		}
		/// <summary>
		/// Create UI format
		/// </summary>
		private void CreateNodes(JMXVRES_0109 JMXFile)
		{
			// Signature
			Childs.Add(new JMXAttribute("Header", JMXFile.Header, false));

			// File offsets
			Childs.Add(new JMXAttribute("FileOffset.Material", JMXFile.MaterialFileOffset, false));
			Childs.Add(new JMXAttribute("FileOffset.Mesh", JMXFile.MeshFileOffset, false));
			Childs.Add(new JMXAttribute("FileOffset.Skeleton", JMXFile.SkeletonFileOffset, false));
			Childs.Add(new JMXAttribute("FileOffset.Animation", JMXFile.AnimationFileOffset, false));
			Childs.Add(new JMXAttribute("FileOffset.MeshGroup", JMXFile.MeshGroupFileOffset, false));
			Childs.Add(new JMXAttribute("FileOffset.AnimationGroup", JMXFile.AnimationGroupFileOffset, false));
			Childs.Add(new JMXAttribute("FileOffset.ModPalette", JMXFile.ModPaletteFileOffset, false));
			Childs.Add(new JMXAttribute("FileOffset.Collision", JMXFile.CollisionFileOffset, false));

			// Unknown flags
			Childs.Add(new JMXAttribute("Flag.UInt01", JMXFile.FlagUInt01));
			Childs.Add(new JMXAttribute("Flag.UInt02", JMXFile.FlagUInt02));
			Childs.Add(new JMXAttribute("Flag.UInt03", JMXFile.FlagUInt03));
			Childs.Add(new JMXAttribute("Flag.UInt04", JMXFile.FlagUInt04));
			Childs.Add(new JMXAttribute("Flag.UInt05", JMXFile.FlagUInt05));

			// Object info
			Childs.Add(new JMXOption("Resource.Type", JMXFile.Type, JMXOption.GetValues<object>(typeof(ResourceType))));
			Childs.Add(new JMXAttribute("Resource.Name", JMXFile.Name));
			Childs.Add(new JMXAttribute("Resource.UnkUInt01", JMXFile.UnkUInt01));
			Childs.Add(new JMXAttribute("Resource.UnkUInt02", JMXFile.UnkUInt02));
			// Reserved
			AddChildArray("UnkBuffer", JMXFile.UnkBuffer, true, false);

			// FileOffset.Collision
			Childs.Add(new JMXAttribute("CollisionMesh", JMXFile.CollisionMesh));
			Childs.Add(new BoundingBoxVM("CollisionBox01", JMXFile.CollisionBox01));
			Childs.Add(new BoundingBoxVM("CollisionBox02", JMXFile.CollisionBox02));
			Childs.Add(new JMXAttribute("UseCollisionMatrix", JMXFile.UseCollisionMatrix));
			Childs.Add(new Matrix3DVM("CollisionMatrix", JMXFile.CollisionMatrix));

			// FileOffset.Material
			AddChildArray("MaterialSet", JMXFile.MaterialSet.ToArray(), true, true);

			// FileOffset.Mesh
			AddChildArray("MeshSet", JMXFile.MeshSet.ToArray(), true, true);

			// FileOffset.Animation
			Childs.Add(new JMXAttribute("Animation.TypeVersion", JMXFile.AnimationTypeVersion));
			Childs.Add(new JMXAttribute("Animation.TypeUserDefine", JMXFile.AnimationTypeUserDefine));
			AddChildArray("AnimationSet", JMXFile.AnimationSet.ToArray(), true, true);

			// FileOffset.Skeleton
			Childs.Add(new JMXAttribute("UseSkeleton", JMXFile.UseSkeleton));
			Childs.Add(new JMXAttribute("Skeleton.Path", JMXFile.SkeletonPath));
			Childs.Add(new JMXAttribute("AttachmentBone", JMXFile.AttachmentBone));

			// FileOffset.MeshGroups
			AddChildArray("MeshGroups", JMXFile.MeshGroups.ToArray(), true, true);

			// FileOffset.AnimationGroup
			AddChildArray("AnimationGroups", JMXFile.AnimationGroups.ToArray(), true, true);

			// FileOffset.ModPalette
			AddChildArray("SystemModSets", JMXFile.SystemModSets.ToArray(), true, true);
			AddChildArray("AnimationModSets", JMXFile.AniModSets.ToArray(), true, true);

			// Extra
			Childs.Add(new CResAttachableVM("ResourceAttachable", JMXFile.ResourceAttachable));
		}
		#endregion
	}
}
