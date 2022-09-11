
using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    public static class EFCFactory
    {
        public static EFController CreateController(string name)
        {
            switch (name)
            {
                case "NormalTimeLife":
                    return new EFCNormalTimeLife();
                case "NormalTimeLoopLife":
                    return new EFCNormalTimeLoopLife();
                case "StaticEmit":
                    return new EFCStaticEmit();
                case "LinkMode":
                    return new EFCLinkMode();
                case "BAN":
                    return new EFCBAN();
                case "ViewMode":
                    return new EFCViewMode();
                case "Shape":
                    return new EFCShape();
                case "ScaleGraph":
                    return new EFCScaleGraph();
                case "DiffuseGraph":
                    return new EFCDiffuseGraph();
                case "Program":
                    return new EFCProgram();
                default:
                    throw new Exception($"Unknown ControllerName: {name}");
            }
        }
    }
}