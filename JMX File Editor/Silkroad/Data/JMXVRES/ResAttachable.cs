using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Silkroad.IO;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public class ResAttachable : ISerializableParameterizedBS
    {
        #region Public Properties
        public uint UnkUInt01 { get; set; }
        public uint UnkUInt02 { get; set; }
        public uint AttachMethod { get; set; }
        public List<Slot> Slots { get; set; } = new List<Slot>();
        public uint nComboNum { get; set; }
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader, params object[] param)
        {
            var objectInfoType = (ObjectGeneralType)param[0];

            // This could change if other file than JMXVRES use this class
            if (objectInfoType == ObjectGeneralType.Character || objectInfoType == ObjectGeneralType.Item)
            {
                UnkUInt01 = reader.ReadUInt32();
                UnkUInt02 = reader.ReadUInt32();
                AttachMethod = reader.ReadUInt32();

                var count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    Slots.Add(new Slot()
                    {
                        Type = (SlotType)reader.ReadUInt32(),
                        MeshSetIndex = reader.ReadUInt32(),
                    });
                }
                if (objectInfoType == ObjectGeneralType.Character)
                {
                    nComboNum = reader.ReadUInt32();
                }
            }
        }
        public void Serialize(BSWriter writer, params object[] param)
        {
            var objectInfoType = (ObjectGeneralType)param[0];

            // This could change if other file than JMXVRES use this class
            if (objectInfoType == ObjectGeneralType.Character || objectInfoType == ObjectGeneralType.Item)
            {
                writer.Write(UnkUInt01);
                writer.Write(UnkUInt02);
                writer.Write(AttachMethod);
                writer.Write(Slots.Count);
                for (int i = 0; i < Slots.Count; i++)
                {
                    writer.Write((uint)Slots[i].Type);
                    writer.Write(Slots[i].MeshSetIndex);
                }
                if (objectInfoType == ObjectGeneralType.Character)
                    writer.Write(nComboNum);
            }
        }
        #endregion

        #region Internal classes
        public class Slot
        {
            public SlotType Type { get; set; }
            public uint MeshSetIndex { get; set; }
        }
        public enum SlotType : uint
        {
            Hair = 0,
            Face = 1,
            TorsoUpper = 2,
            TorsoLower = 3,
            Unk04 = 4, // override in avatars but never set?
            ArmUpper = 5,
            ArmLower = 6,
            LeftHand = 7,
            Unk08 = 8,
            RightHand = 9,
            Spear = 10,
            Pelvis = 11,
            Thigh = 12,
            Calf = 13,
            AttachCape = 14 // Cloack?
        }
        #endregion
    }
}