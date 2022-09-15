using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Blends;
using JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Parameter;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

using System;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF
{
    /// <summary>
    /// Viewmodel representing <see cref="IEEParameter"/>
    /// </summary>
    public class EEParameterVM : JMXAbstract
    {
        #region Constructor
        public EEParameterVM(string Name, Type[] AvailableTypes, Type CurrentType, object CurrentObject, bool IsEditable = true)
           : base(Name, AvailableTypes, CurrentType, CurrentObject, IsEditable)
        {
            // Add child nodes to current interface node
            AddFormatHandler(typeof(ParameterEFStaticEmit), (s, e) =>
            {
                AddChildNodes(new ParameterEFStaticEmitVM("Value", e.Obj is ParameterEFStaticEmit _obj ? _obj : new ParameterEFStaticEmit()));
            });
            AddFormatHandler(typeof(ParameterFloat), (s, e) =>
            {
                e.Childs.Add(new JMXAttribute("Value", e.Obj is ParameterFloat _obj ? _obj.Value : default));
            });
            AddFormatHandler(typeof(ParameterVector3), (s, e) =>
            {
                AddChildNodes(new Vector3VM("Value", e.Obj is ParameterVector3 _obj ? _obj.Value : new Vector3()));
            });
            AddFormatHandler(typeof(ParameterMatrix), (s, e) =>
            {
                AddChildNodes(new Matrix4x4VM("Value", e.Obj is ParameterMatrix _obj ? _obj.Value : new Matrix4x4()));
            });
            AddFormatHandler(typeof(ParameterRotVector), (s, e) =>
            {
                AddChildNodes(new ParameterRotVectorVM("Value", e.Obj is ParameterRotVector _obj ? _obj : new ParameterRotVector()));
            });
            AddFormatHandler(typeof(ParameterAxisVector4), (s, e) =>
            {
                AddChildNodes(new ParameterAxisVector4VM("Value", e.Obj is ParameterAxisVector4 _obj ? _obj : new ParameterAxisVector4()));
            });
            AddFormatHandler(typeof(ParameterAngleVector1), (s, e) =>
            {
                AddChildNodes(new ParameterAngleVector1VM("Value", e.Obj is ParameterAngleVector1 _obj ? _obj : new ParameterAngleVector1()));
            });
            AddFormatHandler(typeof(ParameterFrameScale), (s, e) =>
            {
                AddChildNodes(new ParameterFrameScaleVM("Value", e.Obj is ParameterFrameScale _obj ? _obj : new ParameterFrameScale()));
            });
            AddFormatHandler(typeof(ParameterBlendScaleGraphPointer), (s, e) =>
            {
                e.Childs.Add(new JMXAttribute("Value", e.Obj is ParameterBlendScaleGraphPointer _obj ? _obj.Value : default));
            });
            AddFormatHandler(typeof(ParameterFrameDiffuse), (s, e) =>
            {
                AddChildNodes(new ParameterFrameDiffuseVM("Value", e.Obj is ParameterFrameDiffuse _obj ? _obj : new ParameterFrameDiffuse()));
            });
            AddFormatHandler(typeof(ParameterFrameBANRotation), (s, e) =>
            {
                AddChildNodes(new ParameterFrameBANRotationVM("Value", e.Obj is ParameterFrameBANRotation _obj ? _obj : new ParameterFrameBANRotation()));
            });
            AddFormatHandler(typeof(ParameterFrameBANPosition), (s, e) =>
            {
                AddChildNodes(new ParameterFrameBANPositionVM("Value", e.Obj is ParameterFrameBANPosition _obj ? _obj : new ParameterFrameBANPosition()));
            });
            AddFormatHandler(typeof(ParameterFrameTextureSlide), (s, e) =>
            {
                AddChildNodes(new ParameterFrameTextureSlideVM("Value", e.Obj is ParameterFrameTextureSlide _obj ? _obj : new ParameterFrameTextureSlide()));
            });
            AddFormatHandler(typeof(ParameterBlendScaleGraph), (s, e) =>
            {
                AddChildNodes(new EEVectorBlendVM("Value", e.Obj is ParameterBlendScaleGraph _obj ? _obj.Value : new EEBlend<Vector3, VectorBlend>()));
            });
            AddFormatHandler(typeof(ParameterBlendDiffuseGraph), (s, e) =>
            {
                var obj = e.Obj is ParameterBlendDiffuseGraph _obj ? _obj.Value : new EEBlend<Color32, DiffuseBlend>();
                Childs.Add(new GradientColorPickerVM("DiffuseBlend", obj.Begin, obj.End, obj));
            });
            AddFormatHandler(typeof(ParameterBSAnimation), (s, e) =>
            {
                AddChildArray("Values", e.Obj is ParameterBSAnimation _obj ? _obj.Value.ToArray() : new ParameterBSAnimation().Value.ToArray(), true, true);
            });


            // Update values with new formats
            SetCurrentType(CurrentType, CurrentObject);
        }
        #endregion

        #region Abstract Implementation
        protected override void AddBaseNodes(object Obj)
        {
            // empty
        }
        public override object GetClassFrom(JMXStructure s, int i)
        {
            IEEParameter data = null;
            // Check interface format
            if (CurrentType == typeof(ParameterEFStaticEmit))
            {
                data = (ParameterEFStaticEmit)new ParameterEFStaticEmitVM(string.Empty, new ParameterEFStaticEmit()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(ParameterFloat))
            {
                data = new ParameterFloat()
                {
                    Value = (float)((JMXAttribute)s.Childs[i++]).Value
                };
            }
            else if (CurrentType == typeof(ParameterVector3))
            {
                data = new ParameterVector3()
                {
                    Value = (Vector3)new Vector3VM(string.Empty, new Vector3()).GetClassFrom(s, i++),
                };
            }
            else if (CurrentType == typeof(ParameterMatrix))
            {
                data = new ParameterMatrix()
                {
                    Value = (Matrix4x4)new Matrix4x4VM(string.Empty, new Matrix4x4()).GetClassFrom(s, i++),
                };
            }
            else if (CurrentType == typeof(ParameterRotVector))
            {
                data = (ParameterRotVector)new ParameterRotVectorVM(string.Empty, new ParameterRotVector()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(ParameterAxisVector4))
            {
                data = (ParameterAxisVector4)new ParameterAxisVector4VM(string.Empty, new ParameterAxisVector4()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(ParameterAngleVector1))
            {
                data = (ParameterAngleVector1)new ParameterAngleVector1VM(string.Empty, new ParameterAngleVector1()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(ParameterFrameScale))
            {
                data = (ParameterFrameScale)new ParameterFrameScaleVM(string.Empty, new ParameterFrameScale()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(ParameterBlendScaleGraphPointer))
            {
                data = new ParameterBlendScaleGraphPointer()
                {
                    Value = (float)((JMXAttribute)s.Childs[i++]).Value
                };
            }
            else if (CurrentType == typeof(ParameterFrameDiffuse))
            {
                data = (ParameterFrameDiffuse)new ParameterFrameDiffuseVM(string.Empty, new ParameterFrameDiffuse()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(ParameterFrameBANRotation))
            {
                data = (ParameterFrameBANRotation)new ParameterFrameBANRotationVM(string.Empty, new ParameterFrameBANRotation()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(ParameterFrameBANPosition))
            {
                data = (ParameterFrameBANPosition)new ParameterFrameBANPositionVM(string.Empty, new ParameterFrameBANPosition()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(ParameterFrameTextureSlide))
            {
                data = (ParameterFrameTextureSlide)new ParameterFrameTextureSlideVM(string.Empty, new ParameterFrameTextureSlide()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(ParameterBlendScaleGraph))
            {
                data = new ParameterBlendScaleGraph()
                {
                    Value = (EEBlend<Vector3, VectorBlend>)new EEVectorBlendVM(string.Empty, new EEBlend<Vector3, VectorBlend>()).GetClassFrom(s, i++)
                };
            }
            else if (CurrentType == typeof(ParameterBlendDiffuseGraph))
            {
                var gradientColor = (GradientColorPickerVM)s.Childs[i++];
                // Copy data
                var diffuseBlend = new EEBlend<Color32, DiffuseBlend>();
                diffuseBlend.Begin = gradientColor.Begin;
                diffuseBlend.End = gradientColor.End;
                foreach (var v in gradientColor.GradientValues)
                    diffuseBlend.Points.Add(new DiffuseBlend() { Value = new Color32(v.Color.R, v.Color.G, v.Color.B, v.Color.A), Time = (float)v.Offset });
                // build data
                data = new ParameterBlendDiffuseGraph()
                {
                    Value = diffuseBlend
                };
            }
            else if (CurrentType == typeof(ParameterBSAnimation))
            {
                data = new ParameterBSAnimation()
                {
                    Value = ((JMXStructure)s.Childs[i++]).GetChildList<string>(),
                };
            }
            // return interface
            return data;
        }
        #endregion

        #region Private Helpers
        /// <summary>
        /// Add nodes from structure to the current node
        /// </summary>
        private void AddChildNodes(JMXStructure vm)
        {
            foreach (var c in vm.Childs)
                Childs.Add(c);
        }
        #endregion
    }
}