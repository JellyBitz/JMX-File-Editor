namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public enum ModDataType : int
    {
        /// <summary>
        /// Advanced Material
        /// </summary>
        ModDataMtrl = 0x00000000,

        ModDataTexAni = 0x00010000,
        ModDataMultiTex = 0x00010001,
        ModDataMultiTexRev = 0x00010002,

        ModDataParticle = 0x00030000,

        ModDataEnvMap = 0x00040000,
        ModDataBumpEnv = 0x00040001,

        /// <summary>
        /// Sound Config
        /// </summary>
        ModDataSound = 0x00050000,

        /// <summary>
        /// Dynamic Vertex Config
        /// </summary>
        ModDataDyVertex = 0x00060000,

        /// <summary>
        /// Dynamic Joint Config
        /// </summary>
        ModDataDyJoint = 0x00060001,

        /// <summary>
        /// Dynamic Lattice Config
        /// </summary>
        ModDataDyLattice = 0x00060002,

        ModDataProgEquipPow = 0x00070000,
    }
}