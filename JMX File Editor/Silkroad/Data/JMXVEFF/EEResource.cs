using JMXFileEditor.Silkroad.IO;

using System;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public enum D3DBLEND : int
    {
        D3DBLEND_ZERO = 1,
        D3DBLEND_ONE = 2,
        D3DBLEND_SRCCOLOR = 3,
        D3DBLEND_INVSRCCOLOR = 4,
        D3DBLEND_SRCALPHA = 5,
        D3DBLEND_INVSRCALPHA = 6,
        D3DBLEND_DESTALPHA = 7,
        D3DBLEND_INVDESTALPHA = 8,
        D3DBLEND_DESTCOLOR = 9,
        D3DBLEND_INVDESTCOLOR = 10,
        D3DBLEND_SRCALPHASAT = 11,
        D3DBLEND_BOTHSRCALPHA = 12,
        D3DBLEND_BOTHINVSRCALPHA = 13,
        D3DBLEND_BLENDFACTOR = 14,
        D3DBLEND_INVBLENDFACTOR = 15,
        D3DBLEND_SRCCOLOR2 = 16,
        D3DBLEND_INVSRCCOLOR2 = 17,
        D3DBLEND_FORCE_DWORD = 0x7fffffff
    }

    public enum D3DTEXTUREOP : int
    {
        D3DTOP_DISABLE = 1,
        D3DTOP_SELECTARG1 = 2,
        D3DTOP_SELECTARG2 = 3,
        D3DTOP_MODULATE = 4,
        D3DTOP_MODULATE2X = 5,
        D3DTOP_MODULATE4X = 6,
        D3DTOP_ADD = 7,
        D3DTOP_ADDSIGNED = 8,
        D3DTOP_ADDSIGNED2X = 9,
        D3DTOP_SUBTRACT = 10,
        D3DTOP_ADDSMOOTH = 11,
        D3DTOP_BLENDDIFFUSEALPHA = 12,
        D3DTOP_BLENDTEXTUREALPHA = 13,
        D3DTOP_BLENDFACTORALPHA = 14,
        D3DTOP_BLENDTEXTUREALPHAPM = 15,
        D3DTOP_BLENDCURRENTALPHA = 16,
        D3DTOP_PREMODULATE = 17,
        D3DTOP_MODULATEALPHA_ADDCOLOR = 18,
        D3DTOP_MODULATECOLOR_ADDALPHA = 19,
        D3DTOP_MODULATEINVALPHA_ADDCOLOR = 20,
        D3DTOP_MODULATEINVCOLOR_ADDALPHA = 21,
        D3DTOP_BUMPENVMAP = 22,
        D3DTOP_BUMPENVMAPLUMINANCE = 23,
        D3DTOP_DOTPRODUCT3 = 24,
        D3DTOP_MULTIPLYADD = 25,
        D3DTOP_LERP = 26,
        D3DTOP_FORCE_DWORD = 0x7fffffff
    }

    public enum D3DTEXTUREARG
    {
        D3DTA_DIFFUSE = 0x00000000,
        D3DTA_CURRENT = 0x00000001,
        D3DTA_TEXTURE = 0x00000002,
        D3DTA_TFACTOR = 0x00000003,
        D3DTA_SPECULAR = 0x00000004,
        D3DTA_TEMP = 0x00000005,
        D3DTA_CONSTANT = 0x00000006,
        D3DTA_COMPLEMENT = 0x00000010,
    }

    [Serializable]
    public class EEResource
    {
        public bool Backface { get; set; }
        public D3DBLEND SrcBlend { get; set; }
        public D3DBLEND DstBlend { get; set; }
        public D3DTEXTUREARG SrcTextureArg1 { get; set; }
        public D3DTEXTUREARG SrcTextureArg2 { get; set; }
        public D3DTEXTUREOP SrcTextureOP { get; set; }
        public D3DTEXTUREARG DstTextureArg1 { get; set; }
        public D3DTEXTUREARG DstTextureArg2 { get; set; }
        public D3DTEXTUREOP DstTextureOP { get; set; }

        public List<EFMesh> Meshes { get; } = new List<EFMesh>();

        public void Read(BSReader reader)
        {
            Backface = reader.ReadInt32() != 0;
            SrcBlend = (D3DBLEND)reader.ReadInt32();
            DstBlend = (D3DBLEND)reader.ReadInt32();
            SrcTextureArg1 = (D3DTEXTUREARG)reader.ReadInt32();
            SrcTextureArg2 = (D3DTEXTUREARG)reader.ReadInt32();
            SrcTextureOP = (D3DTEXTUREOP)reader.ReadInt32();
            DstTextureArg1 = (D3DTEXTUREARG)reader.ReadInt32();
            DstTextureArg2 = (D3DTEXTUREARG)reader.ReadInt32();
            DstTextureOP = (D3DTEXTUREOP)reader.ReadInt32();

            var meshCount = reader.ReadInt32();
            for (int iMesh = 0; iMesh < meshCount; iMesh++)
            {
                var mesh = new EFMesh();
                mesh.Read(reader);

                Meshes.Add(mesh);
            }
        }
    }
}