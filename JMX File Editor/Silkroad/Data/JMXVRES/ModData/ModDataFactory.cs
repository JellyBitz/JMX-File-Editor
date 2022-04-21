namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public static class ModDataFactory
    {
        public static IModData Create(ModDataType type)
        {
            switch (type)
            {
                case ModDataType.ModDataMtrl: return new ModDataMtrl();
                case ModDataType.ModDataTexAni: return new ModDataTexAni();
                case ModDataType.ModDataMultiTex: return new ModDataMultiTex();
                case ModDataType.ModDataMultiTexRev: return new ModDataMultiTexRev();
                case ModDataType.ModDataParticle: return new ModDataParticle();
                case ModDataType.ModDataEnvMap: return new ModDataEnvMap();
                case ModDataType.ModDataBumpEnv: return new ModDataBumpEnv();
                case ModDataType.ModDataSound: return new ModDataSound();
                case ModDataType.ModDataDyVertex: return new ModDataDyVertex();
                case ModDataType.ModDataDyJoint: return new ModDataDyJoint();
                case ModDataType.ModDataDyLattice: return new ModDataDyLattice();
                case ModDataType.ModDataProgEquipPow: return new ModDataDyLattice();
                default: throw new System.NotImplementedException();
            }
        }
    }
}