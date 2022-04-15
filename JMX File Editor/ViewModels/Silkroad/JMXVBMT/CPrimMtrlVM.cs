using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Silkroad.Data.JMXVBMT;
using JMXFileEditor.ViewModels.Silkroad.Common;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVBMT
{
	public class CPrimMtrlVM : JMXStructure
	{
		#region Constructor
		public CPrimMtrlVM(string Name, CPrimMtrl Mtrl) : base(Name, true)
		{
			// add formats
			AddFormatHandler(typeof(Color4), (s, e) => {
				e.Childs.Add(new Color4VM("[" + e.Childs.Count + "]", e.Obj is Color4 _obj ? _obj : new Color4()));
			});
			// create nodes
			Childs.Add(new JMXAttribute("Name", Mtrl.Name));
			Childs.Add(new Color4VM("Diffuse", Mtrl.Diffuse));
			Childs.Add(new Color4VM("Ambient", Mtrl.Ambient));
			Childs.Add(new Color4VM("Specular", Mtrl.Specular));
			Childs.Add(new Color4VM("Emissive", Mtrl.Emissive));
			Childs.Add(new JMXAttribute("UnkFloat01", Mtrl.UnkFloat01));
			Childs.Add(new JMXAttribute("Flags", Mtrl.Flags));
			Childs.Add(new JMXAttribute("DiffuseMapPath", Mtrl.DiffuseMapPath));
			Childs.Add(new JMXAttribute("UnkFloat02", Mtrl.UnkFloat02));
			Childs.Add(new JMXAttribute("UnkByte01", Mtrl.UnkByte01));
			Childs.Add(new JMXAttribute("UnkByte02", Mtrl.UnkByte02));
			Childs.Add(new JMXAttribute("IsAbsolutePath", Mtrl.IsAbsolutePath));
			Childs.Add(new JMXAttribute("NormalMapPath", Mtrl.NormalMapPath));
			Childs.Add(new JMXAttribute("UnkUInt01", Mtrl.UnkUInt01));
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			return new CPrimMtrl()
			{
				Name = (string)((JMXAttribute)Structure.Childs[0]).Value,
				Diffuse = (Color4)((Color4VM)Structure.Childs[1]).GetClass(),
				Ambient = (Color4)((Color4VM)Structure.Childs[2]).GetClass(),
				Specular = (Color4)((Color4VM)Structure.Childs[3]).GetClass(),
				Emissive = (Color4)((Color4VM)Structure.Childs[4]).GetClass(),
				UnkFloat01 = (float)((JMXAttribute)Structure.Childs[5]).Value,
				Flags = (uint)((JMXAttribute)Structure.Childs[6]).Value,
				DiffuseMapPath = (string)((JMXAttribute)Structure.Childs[7]).Value,
				UnkFloat02 = (float)((JMXAttribute)Structure.Childs[8]).Value,
				UnkByte01 = (byte)((JMXAttribute)Structure.Childs[9]).Value,
				UnkByte02 = (byte)((JMXAttribute)Structure.Childs[10]).Value,
				IsAbsolutePath = (bool)((JMXAttribute)Structure.Childs[11]).Value,
				NormalMapPath = (string)((JMXAttribute)Structure.Childs[12]).Value,
				UnkUInt01 = (uint)((JMXAttribute)Structure.Childs[13]).Value
			};
		}
		#endregion
	}
}
