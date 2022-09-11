using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVENVI
{
    class EnvironmentProfile : ISerializableParameterizedBS
    {
        #region Public Properties
        public short Id { get; set; }
        public string Name { get; set; }
        public string String0 { get; set; }
        public string String1 { get; set; }
        public EnvironmentCurve<Color3, EnvironmentColorBlend> SunColor { get; set; } = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
        public EnvironmentCurve<Color3, EnvironmentColorBlend> SkyTopColor { get; set; } = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
        public EnvironmentCurve<Color3, EnvironmentColorBlend> DiffuseColor { get; set; } = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
        public EnvironmentCurve<Color3, EnvironmentColorBlend> ObjectAmbientColor { get; set; } = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
        public EnvironmentCurve<Color3, EnvironmentColorBlend> Curve4 { get; set; } = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
        public EnvironmentCurve<Color3, EnvironmentColorBlend> TerrainAmbientColor { get; set; } = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
        public EnvironmentCurve<Color3, EnvironmentColorBlend> TerrainShadowColor { get; set; } = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
        public EnvironmentCurve<float, EnvironmentFloatBlend> FogNearPlane { get; set; } = new EnvironmentCurve<float, EnvironmentFloatBlend>();
        public EnvironmentCurve<float, EnvironmentFloatBlend> FogFarPlane { get; set; } = new EnvironmentCurve<float, EnvironmentFloatBlend>();
        public EnvironmentCurve<Color3, EnvironmentColorBlend> FogColor { get; set; } = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
        public EnvironmentCurve<float, EnvironmentFloatBlend> Curve10 { get; set; } = new EnvironmentCurve<float, EnvironmentFloatBlend>();
        public EnvironmentCurve<float, EnvironmentFloatBlend> Curve11 { get; set; } = new EnvironmentCurve<float, EnvironmentFloatBlend>();
        public EnvironmentCurve<float, EnvironmentFloatBlend> Curve12 { get; set; } = new EnvironmentCurve<float, EnvironmentFloatBlend>();
        public EnvironmentCurve<Color3, EnvironmentColorBlend> SkyBottomColor { get; set; } = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
        public EnvironmentCurve<Color3, EnvironmentColorBlend> WaterColor { get; set; } = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
        public EnvironmentCurve<float, EnvironmentFloatBlend> Curve15 { get; set; } = new EnvironmentCurve<float, EnvironmentFloatBlend>();
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader, params object[] parameters)
        {
            var version = (int)parameters[0];

            Id = reader.ReadInt16();
            Name = reader.ReadString();

            String0 = reader.ReadString();
            String1 = reader.ReadString();

            SunColor.Deserialize(reader);
            SkyTopColor.Deserialize(reader);
            DiffuseColor.Deserialize(reader);
            ObjectAmbientColor.Deserialize(reader);
            Curve4.Deserialize(reader);
            TerrainAmbientColor.Deserialize(reader);

            if (version >= 1003)
            {
                TerrainShadowColor.Deserialize(reader);
            }
            else
            {
                TerrainShadowColor.Blends.Add(new EnvironmentColorBlend { Time = 0.0f, Value = Color3.Zero });
                TerrainShadowColor.Blends.Add(new EnvironmentColorBlend { Time = 1.0f, Value = Color3.Zero });
            }

            FogNearPlane.Deserialize(reader);
            FogFarPlane.Deserialize(reader);
            FogColor.Deserialize(reader);
            Curve10.Deserialize(reader);
            Curve11.Deserialize(reader);

            if (version >= 1001)
            {
                Curve12.Deserialize(reader);
            }
            else
            {
                Curve12.Blends.Add(new EnvironmentFloatBlend { Time = 0.0f, Value = 1.0f });
                Curve12.Blends.Add(new EnvironmentFloatBlend { Time = 1.0f, Value = 1.0f });
            }

            if (version >= 1002)
            {
                SkyBottomColor.Deserialize(reader);
                WaterColor.Deserialize(reader);
                Curve15.Deserialize(reader);
            }
            else
            {
                SkyBottomColor.Blends.Add(new EnvironmentColorBlend { Time = 0.0f, Value = Color3.One });
                SkyBottomColor.Blends.Add(new EnvironmentColorBlend { Time = 1.0f, Value = Color3.One });

                WaterColor.Blends.Add(new EnvironmentColorBlend { Time = 0.0f, Value = Color3.One });
                WaterColor.Blends.Add(new EnvironmentColorBlend { Time = 1.0f, Value = Color3.One });

                Curve15.Blends.Add(new EnvironmentFloatBlend { Time = 0.0f, Value = 1.0f });
                Curve15.Blends.Add(new EnvironmentFloatBlend { Time = 1.0f, Value = 1.0f });
            }
        }
        public void Serialize(BSWriter writer, params object[] parameters)
        {
            writer.Write(Id);
            writer.Write(Name);

            writer.Write(String0);
            writer.Write(String1);

            writer.Serialize(SunColor);
            writer.Serialize(SkyTopColor);
            writer.Serialize(DiffuseColor);
            writer.Serialize(ObjectAmbientColor);
            writer.Serialize(Curve4);
            writer.Serialize(TerrainAmbientColor);
            writer.Serialize(TerrainShadowColor);
            writer.Serialize(FogNearPlane);
            writer.Serialize(FogFarPlane);
            writer.Serialize(FogColor);
            writer.Serialize(Curve10);
            writer.Serialize(Curve11);
            writer.Serialize(Curve12);
            writer.Serialize(SkyBottomColor);
            writer.Serialize(WaterColor);
            writer.Serialize(Curve15);
        }
        #endregion
    }
}
