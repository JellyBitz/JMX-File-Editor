using JMXFileEditor.Silkroad.Data.Common;

namespace JMXFileEditor.ViewModels.Silkroad.Common
{
	/// <summary>
	/// ViewModel representing a Matrix on 3D space
	/// </summary>

	public class Matrix3DVM : JMXStructure
	{
		#region Constructor
		public Matrix3DVM(string Name, Matrix3D Matrix) : base(Name, true)
		{
			Childs.Add(new JMXAttribute("M11", Matrix.Data.M11));
			Childs.Add(new JMXAttribute("M12", Matrix.Data.M12));
			Childs.Add(new JMXAttribute("M13", Matrix.Data.M13));
			Childs.Add(new JMXAttribute("M14", Matrix.Data.M14));

			Childs.Add(new JMXAttribute("M21", Matrix.Data.M21));
			Childs.Add(new JMXAttribute("M22", Matrix.Data.M22));
			Childs.Add(new JMXAttribute("M23", Matrix.Data.M23));
			Childs.Add(new JMXAttribute("M24", Matrix.Data.M24));

			Childs.Add(new JMXAttribute("M31", Matrix.Data.M31));
			Childs.Add(new JMXAttribute("M32", Matrix.Data.M32));
			Childs.Add(new JMXAttribute("M33", Matrix.Data.M33));
			Childs.Add(new JMXAttribute("M34", Matrix.Data.M34));

			Childs.Add(new JMXAttribute("OffsetX", Matrix.Data.OffsetX));
			Childs.Add(new JMXAttribute("OffsetY", Matrix.Data.OffsetY));
			Childs.Add(new JMXAttribute("OffsetZ", Matrix.Data.OffsetZ));
			Childs.Add(new JMXAttribute("M44", Matrix.Data.M44));
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			Matrix3D matrix = new Matrix3D
			{
				Data = new System.Windows.Media.Media3D.Matrix3D(
						(double)((JMXAttribute)Structure.Childs[0]).Value,
						(double)((JMXAttribute)Structure.Childs[1]).Value,
						(double)((JMXAttribute)Structure.Childs[2]).Value,
						(double)((JMXAttribute)Structure.Childs[3]).Value,

						(double)((JMXAttribute)Structure.Childs[4]).Value,
						(double)((JMXAttribute)Structure.Childs[5]).Value,
						(double)((JMXAttribute)Structure.Childs[6]).Value,
						(double)((JMXAttribute)Structure.Childs[7]).Value,

						(double)((JMXAttribute)Structure.Childs[8]).Value,
						(double)((JMXAttribute)Structure.Childs[9]).Value,
						(double)((JMXAttribute)Structure.Childs[10]).Value,
						(double)((JMXAttribute)Structure.Childs[11]).Value,

						(double)((JMXAttribute)Structure.Childs[12]).Value,
						(double)((JMXAttribute)Structure.Childs[13]).Value,
						(double)((JMXAttribute)Structure.Childs[14]).Value,
						(double)((JMXAttribute)Structure.Childs[15]).Value
				)
			};
			return matrix;
		}
		#endregion
	}
}
