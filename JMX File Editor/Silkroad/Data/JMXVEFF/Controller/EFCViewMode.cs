using JMXFileEditor.Silkroad.IO;
using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    public class EFCViewMode : EFController
    {
        public override string Name => "ViewMode";

        public ViewMode ViewMode;

        public override void Read(BSReader reader)
        {
            string name = reader.ReadString();
            switch (name)
            {
                case "ViewNone":
                    this.ViewMode = ViewMode.None;
                    break;
                case "ViewBillboard":
                    this.ViewMode = ViewMode.Billboard;
                    break;
                case "ViewVBillboard":
                    this.ViewMode = ViewMode.VBillboard;
                    break;
                case "ViewYBillboard":
                    this.ViewMode = ViewMode.YBillboard;
                    break;
                default:
                    throw new Exception($"Undefined {nameof(ViewMode)}: {name}");
            }
        }
    }
}