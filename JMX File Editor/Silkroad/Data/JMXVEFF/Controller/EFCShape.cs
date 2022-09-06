using JMXFileEditor.Silkroad.IO;

using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    //Shape
    public class EFCShape : EFController
    {
        public override string Name => "Shape";

        public RenderShape Shape { get; set; }
        public EEResource Resource { get; } = new EEResource();

        public override void Read(BSReader reader)
        {
            var name = reader.ReadString();

            switch (name)
            {
                case "ViewNone":
                    this.Shape = RenderShape.None;
                    break;
                case "RenderPlate":
                    this.Shape = RenderShape.Plate;
                    break;
                case "RenderMesh":
                    this.Shape = RenderShape.Mesh;
                    break;
                case "RenderLinkDPipe":
                    this.Shape = RenderShape.LinkDPipe;
                    break;
                case "RenderLinkPipe":
                    this.Shape = RenderShape.LinkPipe;
                    break;
                case "RenderLinkObj":
                    this.Shape = RenderShape.LinkObj;
                    break;

                default:
                    throw new Exception($"Undefined {nameof(RenderShape)}: {name}");
            }

            this.Resource.Read(reader);
        }
    }
}