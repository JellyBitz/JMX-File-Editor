using System;

using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Parameter;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

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
                AddChildNodes(new ParameterEFStaticEmitVM("Value", (ParameterEFStaticEmit)e.Obj));
            });
            AddFormatHandler(typeof(ParameterFloat), (s, e) =>
            {
                e.Childs.Add(new JMXAttribute("Value", ((ParameterFloat)e.Obj).Value));
            });
            AddFormatHandler(typeof(ParameterVector3), (s, e) =>
            {
                AddChildNodes(new Vector3VM("Value", ((ParameterVector3)e.Obj).Value));
            });
            AddFormatHandler(typeof(ParameterMatrix), (s, e) =>
            {
                AddChildNodes(new Matrix4x4VM("Value", ((ParameterMatrix)e.Obj).Value));
            });
            AddFormatHandler(typeof(ParameterRotVector), (s, e) =>
            {
                AddChildNodes(new ParameterRotVectorVM("Value", (ParameterRotVector)e.Obj));
            });
            AddFormatHandler(typeof(ParameterAxisVector4), (s, e) =>
            {
                AddChildNodes(new ParameterAxisVector4VM("Value", (ParameterAxisVector4)e.Obj));
            });
            AddFormatHandler(typeof(ParameterAngleVector1), (s, e) =>
            {
                AddChildNodes(new ParameterAngleVector1VM("Value", (ParameterAngleVector1)e.Obj));
            });
            AddFormatHandler(typeof(ParameterFrameScale), (s, e) =>
            {
                AddChildNodes(new ParameterFrameScaleVM("Value", (ParameterFrameScale)e.Obj));
            });
            AddFormatHandler(typeof(ParameterBlendScaleGraphPointer), (s, e) =>
            {
                e.Childs.Add(new JMXAttribute("Value", ((ParameterBlendScaleGraphPointer)e.Obj).Value));
            });
            AddFormatHandler(typeof(ParameterFrameDiffuse), (s, e) =>
            {
                AddChildNodes(new ParameterFrameDiffuseVM("Value", (ParameterFrameDiffuse)e.Obj));
            });
            AddFormatHandler(typeof(ParameterFrameBANRotation), (s, e) =>
            {
                AddChildNodes(new ParameterFrameBANRotationVM("Value", (ParameterFrameBANRotation)e.Obj));
            });
            AddFormatHandler(typeof(ParameterFrameBANPosition), (s, e) =>
            {
                AddChildNodes(new ParameterFrameBANPositionVM("Value", (ParameterFrameBANPosition)e.Obj));
            });
            AddFormatHandler(typeof(ParameterFrameTextureSlide), (s, e) =>
            {
                AddChildNodes(new ParameterFrameTextureSlideVM("Value", (ParameterFrameTextureSlide)e.Obj));
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
                data = (ParameterEFStaticEmit)new ParameterEFStaticEmitVM(string.Empty, new ParameterEFStaticEmit()).GetClassFrom(this, i++);
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
                    Value = (Vector3)new Vector3VM(string.Empty, new Vector3()).GetClassFrom(this, i++)
                };
            }
            else if (CurrentType == typeof(ParameterMatrix))
            {
                data = new ParameterMatrix()
                {
                    Value = (Matrix4x4)new Matrix4x4VM(string.Empty, new Matrix4x4()).GetClassFrom(this, i++)
                };
            }
            else if (CurrentType == typeof(ParameterRotVector))
            {
                data = (ParameterRotVector)new ParameterRotVectorVM(string.Empty, new ParameterRotVector()).GetClassFrom(this, i++);
            }
            else if (CurrentType == typeof(ParameterAxisVector4))
            {
                data = (ParameterAxisVector4)new ParameterAxisVector4VM(string.Empty, new ParameterAxisVector4()).GetClassFrom(this, i++);
            }
            else if (CurrentType == typeof(ParameterAngleVector1))
            {
                data = (ParameterAngleVector1)new ParameterAngleVector1VM(string.Empty, new ParameterAngleVector1()).GetClassFrom(this, i++);
            }
            else if (CurrentType == typeof(ParameterFrameScale))
            {
                data = (ParameterFrameScale)new ParameterFrameScaleVM(string.Empty, new ParameterFrameScale()).GetClassFrom(this, i++);
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
                data = (ParameterFrameDiffuse)new ParameterFrameDiffuseVM(string.Empty, new ParameterFrameDiffuse()).GetClassFrom(this, i++);
            }
            else if (CurrentType == typeof(ParameterFrameBANRotation))
            {
                data = (ParameterFrameBANRotation)new ParameterFrameBANRotationVM(string.Empty, new ParameterFrameBANRotation()).GetClassFrom(this, i++);
            }
            else if (CurrentType == typeof(ParameterFrameBANPosition))
            {
                data = (ParameterFrameBANPosition)new ParameterFrameBANPositionVM(string.Empty, new ParameterFrameBANPosition()).GetClassFrom(this, i++);
            }
            else if (CurrentType == typeof(ParameterFrameTextureSlide))
            {
                data = (ParameterFrameTextureSlide)new ParameterFrameTextureSlideVM(string.Empty, new ParameterFrameTextureSlide()).GetClassFrom(this, i++);
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