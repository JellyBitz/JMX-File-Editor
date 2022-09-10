using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;
using System;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFControllerVM : JMXAbstract
    {
        #region Constructor
        public EFControllerVM(string Name, Type[] AvailableTypes, Type CurrentType, object CurrentObject)
           : base(Name, AvailableTypes, CurrentType, CurrentObject)
        {
            // Add child nodes to current abstract node
            AddFormatHandler(typeof(EFCNormalTimeLife), (s, e) =>
            {
                AddChildNodes(new EFCNormalTimeLifeVM(string.Empty, e.Obj is EFCNormalTimeLife _obj ? _obj : new EFCNormalTimeLife()));
            });
            AddFormatHandler(typeof(EFCNormalTimeLoopLife), (s, e) =>
            {
                AddChildNodes(new EFCNormalTimeLoopLifeVM(string.Empty, e.Obj is EFCNormalTimeLoopLife _obj ? _obj : new EFCNormalTimeLoopLife()));
            });
            AddFormatHandler(typeof(EFCStaticEmit), (s, e) =>
            {
                AddChildNodes(new EFCStaticEmitVM(string.Empty, e.Obj is EFCStaticEmit _obj ? _obj : new EFCStaticEmit()));
            });
            AddFormatHandler(typeof(EFCProgram), (s, e) =>
            {
                AddChildNodes(new EFCProgramVM(string.Empty, e.Obj is EFCProgram _obj ? _obj : new EFCProgram()));
            });
            AddFormatHandler(typeof(EFCLinkMode), (s, e) =>
            {
                AddChildNodes(new EFCLinkModeVM(string.Empty, e.Obj is EFCLinkMode _obj ? _obj : new EFCLinkMode()));
            });
            AddFormatHandler(typeof(EFCBAN), (s, e) =>
            {
                AddChildNodes(new EFCBANVM(string.Empty, e.Obj is EFCBAN _obj ? _obj : new EFCBAN()));
            });
            AddFormatHandler(typeof(EFCViewMode), (s, e) =>
            {
                AddChildNodes(new EFCViewModeVM(string.Empty, e.Obj is EFCViewMode _obj ? _obj : new EFCViewMode()));
            });
            AddFormatHandler(typeof(EFCShape), (s, e) =>
            {
                AddChildNodes(new EFCShapeVM(string.Empty, e.Obj is EFCShape _obj ? _obj : new EFCShape()));
            });
            AddFormatHandler(typeof(EFCScaleGraph), (s, e) =>
            {
                AddChildNodes(new EFCScaleGraphVM(string.Empty, e.Obj is EFCScaleGraph _obj ? _obj : new EFCScaleGraph()));
            });
            AddFormatHandler(typeof(EFCDiffuseGraph), (s, e) =>
            {
                AddChildNodes(new EFCDiffuseGraphVM(string.Empty, e.Obj is EFCDiffuseGraph _obj ? _obj : new EFCDiffuseGraph()));
            });

            // Update values with new formats
            SetCurrentType(CurrentType, CurrentObject);
        }
        #endregion

        #region Abstract Implementation
        protected override void AddBaseNodes(object Obj)
        {
            // Set default case
            if (Obj == null)
            {
                Obj = new EFCNormalTimeLife();
                SetCurrentType(Obj.GetType(), Obj);
                return;
            }
        }
        public override object GetClassFrom(JMXStructure s, int i)
        {
            EFController data = null;
            // Check abstract format
            if (CurrentType == typeof(EFCNormalTimeLife))
            {
                data = (EFCNormalTimeLife)new EFCNormalTimeLifeVM(string.Empty, new EFCNormalTimeLife()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(EFCNormalTimeLoopLife))
            {
                data = (EFCNormalTimeLoopLife)new EFCNormalTimeLoopLifeVM(string.Empty, new EFCNormalTimeLoopLife()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(EFCStaticEmit))
            {
                data = (EFCStaticEmit)new EFCStaticEmitVM(string.Empty, new EFCStaticEmit()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(EFCProgram))
            {
                data = (EFCProgram)new EFCProgramVM(string.Empty, new EFCProgram()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(EFCLinkMode))
            {
                data = (EFCLinkMode)new EFCLinkModeVM(string.Empty, new EFCLinkMode()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(EFCViewMode))
            {
                data = (EFCViewMode)new EFCViewModeVM(string.Empty, new EFCViewMode()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(EFCShape))
            {
                data = (EFCShape)new EFCShapeVM(string.Empty, new EFCShape()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(EFCScaleGraph))
            {
                data = (EFCScaleGraph)new EFCScaleGraphVM(string.Empty, new EFCScaleGraph()).GetClassFrom(s, i++);
            }
            else if (CurrentType == typeof(EFCDiffuseGraph))
            {
                data = (EFCDiffuseGraph)new EFCDiffuseGraphVM(string.Empty, new EFCDiffuseGraph()).GetClassFrom(s, i++);
            }

            // return abstract class
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