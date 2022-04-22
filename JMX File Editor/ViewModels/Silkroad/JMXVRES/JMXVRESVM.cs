using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Silkroad.Data.JMXVRES;
using JMXFileEditor.Silkroad.Data.JMXVRES.ModData;
using JMXFileEditor.Silkroad.Mathematics;
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
        #endregion Constructor

        #region Public Methods
        public override object GetClassFrom(JMXStructure Structure)
        {
            JMXVRES_0109 file = new JMXVRES_0109();

            // Unknown flags
            file.Flag01 = (int)((JMXAttribute)Structure.Childs[0]).Value;
            file.Flag02 = (int)((JMXAttribute)Structure.Childs[1]).Value;
            file.Flag03 = (int)((JMXAttribute)Structure.Childs[2]).Value;
            file.Flag04 = (int)((JMXAttribute)Structure.Childs[3]).Value;
            file.Flag05 = (int)((JMXAttribute)Structure.Childs[4]).Value;

            // Object info
            file.ObjectInfo.Type = (ObjectGeneralType)((JMXAttribute)Structure.Childs[5]).Value;
            file.ObjectInfo.Name = (string)((JMXAttribute)Structure.Childs[6]).Value;
            file.ObjectInfo.Int01 = (int)((JMXAttribute)Structure.Childs[7]).Value;
            file.ObjectInfo.Int02 = (int)((JMXAttribute)Structure.Childs[8]).Value;

            // Reserved
            file.UnkBuffer = ((JMXStructure)Structure.Childs[9]).GetChildList<byte>().ToArray();

            // FileOffset.Collision
            file.CollisionMesh = (string)((JMXAttribute)Structure.Childs[10]).Value;
            file.CollisionBox01 = (BoundingBoxF)((BoundingBoxVM)Structure.Childs[11]).GetClass();
            file.CollisionBox02 = (BoundingBoxF)((BoundingBoxVM)Structure.Childs[12]).GetClass();
            file.UseCollisionMatrix = (bool)((JMXAttribute)Structure.Childs[13]).Value;
            file.CollisionMatrix = (Matrix4x4)((Matrix4x4VM)Structure.Childs[14]).GetClass();

            // FileOffset.Material
            file.MaterialSet = ((JMXStructure)Structure.Childs[15]).GetChildList<PrimMtrlSet>();

            // FileOffset.Mesh
            file.MeshSet = ((JMXStructure)Structure.Childs[16]).GetChildList<PrimMesh>();

            // FileOffset.Animation
            file.AnimationTypeVersion = (uint)((JMXAttribute)Structure.Childs[17]).Value;
            file.AnimationTypeUserDefine = (uint)((JMXAttribute)Structure.Childs[18]).Value;
            file.AnimationSet = ((JMXStructure)Structure.Childs[19]).GetChildList<PrimAnimation>();

            // FileOffset.Skeleton
            file.HasSkeleton = (bool)((JMXAttribute)Structure.Childs[20]).Value;
            file.SkeletonPath = (string)((JMXAttribute)Structure.Childs[21]).Value;
            file.AttachmentBone = (string)((JMXAttribute)Structure.Childs[22]).Value;

            // FileOffset.MeshGroups
            file.MeshGroups = ((JMXStructure)Structure.Childs[23]).GetChildList<PrimMeshGroup>();

            // FileOffset.AnimationGroup
            file.AnimationGroups = ((JMXStructure)Structure.Childs[24]).GetChildList<PrimAniGroup>();

            // FileOffset.ModPalette
            file.SystemModSets = ((JMXStructure)Structure.Childs[25]).GetChildList<ModDataSet>();
            file.AniModSets = ((JMXStructure)Structure.Childs[26]).GetChildList<ModDataSet>();

            // Extra
            file.ResourceAttachable = (ResAttachable)((ResAttachableVM)Structure.Childs[27]).GetClass();

            return file;
        }

        #endregion Public Methods

        #region Private Helpers

        /// <summary>
        /// Add new node formats
        /// </summary>
        private void AddChildFormats()
        {
            AddFormatHandler(typeof(PrimMtrlSet), (s, e) =>
            {
                e.Childs.Add(new PrimMtrlSetVM("[" + e.Childs.Count + "]", e.Obj is PrimMtrlSet _obj ? _obj : new PrimMtrlSet()));
            });
            AddFormatHandler(typeof(PrimMesh), (s, e) =>
            {
                e.Childs.Add(new PrimMeshVM("[" + e.Childs.Count + "]", e.Obj is PrimMesh _obj ? _obj : new PrimMesh()));
            });
            AddFormatHandler(typeof(PrimAnimation), (s, e) =>
            {
                e.Childs.Add(new PrimAniVM("[" + e.Childs.Count + "]", e.Obj is PrimAnimation _obj ? _obj : new PrimAnimation()));
            });
            AddFormatHandler(typeof(PrimMeshGroup), (s, e) =>
            {
                e.Childs.Add(new CPrimMeshGroupVM("[" + e.Childs.Count + "]", e.Obj is PrimMeshGroup _obj ? _obj : new PrimMeshGroup()));
            });
            AddFormatHandler(typeof(PrimAniGroup), (s, e) =>
            {
                e.Childs.Add(new PrimAniGroupVM("[" + e.Childs.Count + "]", e.Obj is PrimAniGroup _obj ? _obj : new PrimAniGroup()));
            });
            AddFormatHandler(typeof(ModDataSet), (s, e) =>
            {
                e.Childs.Add(new ModDataSetVM("[" + e.Childs.Count + "]", e.Obj is ModDataSet _obj ? _obj : new ModDataSet()));
            });
            AddFormatHandler(typeof(ResAttachable), (s, e) =>
            {
                e.Childs.Add(new ResAttachableVM("[" + e.Childs.Count + "]", e.Obj is ResAttachable _obj ? _obj : new ResAttachable()));
            });
        }
        /// <summary>
        /// Create UI format
        /// </summary>
        private void CreateNodes(JMXVRES_0109 JMXFile)
        {
            // Unknown flags
            Childs.Add(new JMXAttribute("Flag01", JMXFile.Flag01));
            Childs.Add(new JMXAttribute("Flag02", JMXFile.Flag02));
            Childs.Add(new JMXAttribute("Flag03", JMXFile.Flag03));
            Childs.Add(new JMXAttribute("Flag04", JMXFile.Flag04));
            Childs.Add(new JMXAttribute("Flag05", JMXFile.Flag05));

            // Object info
            // TODO: New VM class
            Childs.Add(new JMXOption("ObjInfo.Type", JMXFile.ObjectInfo.Type, JMXOption.GetValues<object>(typeof(ObjectGeneralType))));
            Childs.Add(new JMXAttribute("ObjInfo.Name", JMXFile.ObjectInfo.Name));
            Childs.Add(new JMXAttribute("ObjInfo.Int01", JMXFile.ObjectInfo.Int01));
            Childs.Add(new JMXAttribute("ObjInfo.Int02", JMXFile.ObjectInfo.Int02));

            // Reserved
            AddChildArray("UnkBuffer", JMXFile.UnkBuffer, true, false);

            // FileOffset.Collision
            Childs.Add(new JMXAttribute("CollisionMesh", JMXFile.CollisionMesh));
            Childs.Add(new BoundingBoxVM("CollisionBox01", JMXFile.CollisionBox01));
            Childs.Add(new BoundingBoxVM("CollisionBox02", JMXFile.CollisionBox02));
            Childs.Add(new JMXAttribute("UseCollisionMatrix", JMXFile.UseCollisionMatrix));
            Childs.Add(new Matrix4x4VM("CollisionMatrix", JMXFile.CollisionMatrix));

            // FileOffset.Material
            AddChildArray("MaterialSet", JMXFile.MaterialSet.ToArray(), true, true);

            // FileOffset.Mesh
            AddChildArray("MeshSet", JMXFile.MeshSet.ToArray(), true, true);

            // FileOffset.Animation
            Childs.Add(new JMXAttribute("Animation.TypeVersion", JMXFile.AnimationTypeVersion));
            Childs.Add(new JMXAttribute("Animation.TypeUserDefine", JMXFile.AnimationTypeUserDefine));
            AddChildArray("AnimationSet", JMXFile.AnimationSet.ToArray(), true, true);

            // FileOffset.Skeleton
            Childs.Add(new JMXAttribute("UseSkeleton", JMXFile.HasSkeleton));
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
            Childs.Add(new ResAttachableVM("ResourceAttachable", JMXFile.ResourceAttachable));
        }
        #endregion Private Helpers
    }
}