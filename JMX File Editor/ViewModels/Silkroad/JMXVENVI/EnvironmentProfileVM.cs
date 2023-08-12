using JMXFileEditor.Silkroad.Data.JMXVENVI;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVENVI
{
    public class EnvironmentProfileVM : JMXStructure
    {
        public EnvironmentProfileVM(string Name, EnvironmentProfile data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Id", data.Id));
            Childs.Add(new JMXAttribute("Name", data.Name));

            Childs.Add(new JMXAttribute("String01", data.String0));
            Childs.Add(new JMXAttribute("String02", data.String1));

            Childs.Add(new GradientColorPickerVM("SunColor", 0, 1, data.SunColor));
            Childs.Add(new GradientColorPickerVM("SkyTopColor", 0, 1, data.SkyTopColor));
            Childs.Add(new GradientColorPickerVM("DiffuseColor", 0, 1, data.DiffuseColor));
            Childs.Add(new GradientColorPickerVM("ObjectAmbientColor", 0, 1, data.ObjectAmbientColor));
            Childs.Add(new GradientColorPickerVM("Curve4", 0, 1, data.Curve4));
            Childs.Add(new GradientColorPickerVM("TerrainAmbientColor", 0, 1, data.TerrainAmbientColor));
            Childs.Add(new GradientColorPickerVM("TerrainShadowColor", 0, 1, data.TerrainShadowColor));
            // EnvironmentFloatBlend
            AddFormatHandler(typeof(EnvironmentFloatBlend), (s, e) => {
                e.Childs.Add(new EnvironmentFloatBlendVM("[" + e.Childs.Count + "]", e.Obj is EnvironmentFloatBlend _obj ? _obj : new EnvironmentFloatBlend()));
            });
            AddChildArray("FogNearPlane", data.FogNearPlane.Blends.ToArray(), true, true);
            AddChildArray("FogFarPlane", data.FogFarPlane.Blends.ToArray(), true, true);
            Childs.Add(new GradientColorPickerVM("FogColor", 0, 1, data.FogColor));
            AddChildArray("Curve10", data.Curve10.Blends.ToArray(), true, true);
            AddChildArray("Curve11", data.Curve11.Blends.ToArray(), true, true);
            AddChildArray("Curve12", data.Curve12.Blends.ToArray(), true, true);
            Childs.Add(new GradientColorPickerVM("SkyBottomColor", 0, 1, data.SkyBottomColor));
            Childs.Add(new GradientColorPickerVM("WaterColor", 0, 1, data.WaterColor));
            AddChildArray("Curve15", data.Curve15.Blends.ToArray(), true, true);
        }
        public override object GetClassFrom(JMXStructure s, int i)
        {
            var data = new EnvironmentProfile()
            {
                Id = (short)((JMXAttribute)s.Childs[i++]).Value,
                Name = (string)((JMXAttribute)s.Childs[i++]).Value,

                String0 = (string)((JMXAttribute)s.Childs[i++]).Value,
                String1 = (string)((JMXAttribute)s.Childs[i++]).Value,
            };
            // Sun Color
            data.SunColor = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
            var gradientColor = (GradientColorPickerVM)s.Childs[i++];
            foreach (var v in gradientColor.GradientValues)
                data.SunColor.Blends.Add(new EnvironmentColorBlend() { Value = ColorVM.GetColor3(v.Color), Time = (float)v.Offset });
            // SkyTopColor
            data.SkyTopColor = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
            gradientColor = (GradientColorPickerVM)s.Childs[i++];
            foreach (var v in gradientColor.GradientValues)
                data.SkyTopColor.Blends.Add(new EnvironmentColorBlend() { Value = ColorVM.GetColor3(v.Color), Time = (float)v.Offset });
            // DiffuseColor
            data.DiffuseColor = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
            gradientColor = (GradientColorPickerVM)s.Childs[i++];
            foreach (var v in gradientColor.GradientValues)
                data.DiffuseColor.Blends.Add(new EnvironmentColorBlend() { Value = ColorVM.GetColor3(v.Color), Time = (float)v.Offset });
            // ObjectAmbientColor
            data.ObjectAmbientColor = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
            gradientColor = (GradientColorPickerVM)s.Childs[i++];
            foreach (var v in gradientColor.GradientValues)
                data.ObjectAmbientColor.Blends.Add(new EnvironmentColorBlend() { Value = ColorVM.GetColor3(v.Color), Time = (float)v.Offset });
            // Curve4
            data.Curve4 = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
            gradientColor = (GradientColorPickerVM)s.Childs[i++];
            foreach (var v in gradientColor.GradientValues)
                data.Curve4.Blends.Add(new EnvironmentColorBlend() { Value = ColorVM.GetColor3(v.Color), Time = (float)v.Offset });
            // TerrainAmbientColor
            data.TerrainAmbientColor = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
            gradientColor = (GradientColorPickerVM)s.Childs[i++];
            foreach (var v in gradientColor.GradientValues)
                data.TerrainAmbientColor.Blends.Add(new EnvironmentColorBlend() { Value = ColorVM.GetColor3(v.Color), Time = (float)v.Offset });
            // TerrainShadowColor
            data.TerrainShadowColor = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
            gradientColor = (GradientColorPickerVM)s.Childs[i++];
            foreach (var v in gradientColor.GradientValues)
                data.TerrainShadowColor.Blends.Add(new EnvironmentColorBlend() { Value = ColorVM.GetColor3(v.Color), Time = (float)v.Offset });
            // FogNearPlane
            data.FogNearPlane.Blends = ((JMXStructure)s.Childs[i++]).GetChildList<EnvironmentFloatBlend>();
            // FogFarPlane
            data.FogFarPlane.Blends = ((JMXStructure)s.Childs[i++]).GetChildList<EnvironmentFloatBlend>();
            // FogColor
            data.FogColor = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
            gradientColor = (GradientColorPickerVM)s.Childs[i++];
            foreach (var v in gradientColor.GradientValues)
                data.FogColor.Blends.Add(new EnvironmentColorBlend() { Value = ColorVM.GetColor3(v.Color), Time = (float)v.Offset });
            // Curve10
            data.Curve10.Blends = ((JMXStructure)s.Childs[i++]).GetChildList<EnvironmentFloatBlend>();
            // Curve11
            data.Curve11.Blends = ((JMXStructure)s.Childs[i++]).GetChildList<EnvironmentFloatBlend>();
            // Curve12
            data.Curve12.Blends = ((JMXStructure)s.Childs[i++]).GetChildList<EnvironmentFloatBlend>();
            // SkyBottomColor
            data.SkyBottomColor = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
            gradientColor = (GradientColorPickerVM)s.Childs[i++];
            foreach (var v in gradientColor.GradientValues)
                data.SkyBottomColor.Blends.Add(new EnvironmentColorBlend() { Value = ColorVM.GetColor3(v.Color), Time = (float)v.Offset });
            // WaterColor
            data.WaterColor = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
            gradientColor = (GradientColorPickerVM)s.Childs[i++];
            foreach (var v in gradientColor.GradientValues)
                data.WaterColor.Blends.Add(new EnvironmentColorBlend() { Value = ColorVM.GetColor3(v.Color), Time = (float)v.Offset });
            // Curve15
            data.Curve15.Blends = ((JMXStructure)s.Childs[i++]).GetChildList<EnvironmentFloatBlend>();
            return data;
        }
    }
}
