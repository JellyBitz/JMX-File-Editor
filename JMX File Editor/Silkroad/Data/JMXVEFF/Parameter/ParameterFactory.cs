using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public static class ParameterFactory
    {
        public static IEEParameter CreateParameterByName(string parameterName)
        {
            switch (parameterName)
            {
                case "float":
                    return new ParameterFloat();
                case "Vector":
                    return new ParameterVector3();
                case "Matrix":
                    return new ParameterMatrix();
                case "SEFStaticEmit":
                    return new ParameterEFStaticEmit();
                case "AxisVector4":
                    return new ParameterAxisVector4();
                case "RotVector":
                    return new ParameterRotVector();
                case "AngleVector1":
                    return new ParameterAngleVector1();
                case "FrameScale":
                    return new ParameterFrameScale();
                case "BlendScaleGraph":
                    return new ParameterBlendScaleGraph();
                case "BlendScaleGraphPointer":
                    return new ParameterBlendScaleGraphPointer();
                case "FrameDiffuse":
                    return new ParameterFrameDiffuse();
                case "BlendDiffuseGraph":
                    return new ParameterBlendDiffuseGraph();
                case "FrameBANRotation":
                    return new ParameterFrameBANRotation();
                case "FrameBANPosition":
                    return new ParameterFrameBANPosition();
                case "BSAnimation":
                    return new ParameterBSAnimation();
                case "FrameTextureSlide":
                    return new ParameterFrameTextureSlide();
                default:
                    throw new Exception($"Unknown ParameterName: {parameterName}");
            };
        }

        public static IEEParameter CreateParameterByCommandName(string commandName)
        {
            switch (commandName)
            {
                // Emitters
                case "StaticEmit":
                    return new ParameterEFStaticEmit();

                // LifeTime has no data
                case "NeverExtinct":
                case "NormalTimeExtinct":
                case "NormalTimeLoop":
                case "NormalTimeLife":
                    return null;

                // ViewMode has no data
                case "ViewNone":
                case "ViewBillboard":
                case "ViewVBillboard":
                case "ViewYBillboard":
                    return null;

                // RenderMode has no data
                case "RenderNone":
                case "RenderMesh":
                case "RenderPlate":
                case "RenderLinkObj":
                case "RenderLinkPipe":
                case "RenderLinkDPipe":
                    return null;

                // Program
                case "ProgramUpdate":
                    return null;

                // Float
                case "Attraction":
                    return new ParameterFloat();

                // Vector3
                case "SetPosition":
                case "SetSpherePos":
                case "SetVelocity":
                case "Force":
                    return new ParameterVector3();

                // Matrix
                case "SetRotationMat":
                case "SetRVelocityMat":
                    return new ParameterMatrix();

                //RotVector
                case "SetRotation":
                case "SetRVelocity":
                    return new ParameterRotVector();

                //AxisVector4
                case "SetRotationAxis":
                case "SetRVelocityAxis":
                case "SetShapeRot":
                case "SetShapeRotVel":
                    return new ParameterAxisVector4();

                //AngleVector1
                case "SetConePos":
                case "SetConeVel":
                case "ConeForce":
                    return new ParameterAngleVector1();

                //FrameScale
                case "SetGraphScale":
                    return new ParameterFrameScale();

                //BlendScaleGraphPointer
                case "SetGraphRandomScale":
                    return new ParameterBlendScaleGraphPointer();

                //FrameDiffuse
                case "SetGraphDiffuse":
                    return new ParameterFrameDiffuse();

                //FrameBANRotation
                case "SetBANRot":
                    return new ParameterFrameBANRotation();

                //FrameBANPosition
                case "SetBANPos":
                    return new ParameterFrameBANPosition();

                //FrameTextureSlide
                case "TextureSlide":
                    return new ParameterFrameTextureSlide();

                default:
                    throw new Exception($"Unknown CommandName: {commandName}");
            };
        }
    }
}