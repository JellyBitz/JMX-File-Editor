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
            Childs.Add(new ChartXYEditorVM("FogNearPlane", data.FogNearPlane.Blends));
            Childs.Add(new ChartXYEditorVM("FogFarPlane", data.FogFarPlane.Blends));
            Childs.Add(new GradientColorPickerVM("FogColor", 0, 1, data.FogColor));
            Childs.Add(new ChartXYEditorVM("Curve10", data.Curve10.Blends));
            Childs.Add(new ChartXYEditorVM("Curve11", data.Curve11.Blends));
            Childs.Add(new ChartXYEditorVM("Curve12", data.Curve12.Blends));
            Childs.Add(new GradientColorPickerVM("SkyBottomColor", 0, 1, data.SkyBottomColor));
            Childs.Add(new GradientColorPickerVM("WaterColor", 0, 1, data.WaterColor));
            Childs.Add(new ChartXYEditorVM("Curve15", data.Curve15.Blends));
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
            data.SunColor = ((GradientColorPickerVM)s.Childs[i++]).GetEnvironmentColorBlend();
            data.SkyTopColor = ((GradientColorPickerVM)s.Childs[i++]).GetEnvironmentColorBlend();
            data.DiffuseColor = ((GradientColorPickerVM)s.Childs[i++]).GetEnvironmentColorBlend();
            data.ObjectAmbientColor = ((GradientColorPickerVM)s.Childs[i++]).GetEnvironmentColorBlend();
            data.Curve4 = ((GradientColorPickerVM)s.Childs[i++]).GetEnvironmentColorBlend();
            data.TerrainAmbientColor = ((GradientColorPickerVM)s.Childs[i++]).GetEnvironmentColorBlend();
            data.TerrainShadowColor = ((GradientColorPickerVM)s.Childs[i++]).GetEnvironmentColorBlend();
            data.FogNearPlane.Blends = ((ChartXYEditorVM)s.Childs[i++]).GetEnvironmentFloatBlends();
            data.FogFarPlane.Blends = ((ChartXYEditorVM)s.Childs[i++]).GetEnvironmentFloatBlends();
            data.FogColor = ((GradientColorPickerVM)s.Childs[i++]).GetEnvironmentColorBlend();
            data.Curve10.Blends = ((ChartXYEditorVM)s.Childs[i++]).GetEnvironmentFloatBlends();
            data.Curve11.Blends = ((ChartXYEditorVM)s.Childs[i++]).GetEnvironmentFloatBlends();
            data.Curve12.Blends = ((ChartXYEditorVM)s.Childs[i++]).GetEnvironmentFloatBlends();
            data.SkyBottomColor = ((GradientColorPickerVM)s.Childs[i++]).GetEnvironmentColorBlend();
            data.WaterColor = ((GradientColorPickerVM)s.Childs[i++]).GetEnvironmentColorBlend();
            data.Curve15.Blends = ((ChartXYEditorVM)s.Childs[i++]).GetEnvironmentFloatBlends();
            return data;
        }
    }
}
