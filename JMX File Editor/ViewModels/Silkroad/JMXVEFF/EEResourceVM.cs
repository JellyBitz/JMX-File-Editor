using JMXFileEditor.Silkroad.Data.JMXVEFF;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EEResourceVM : JMXStructure
    {
        #region Constructor
        public EEResourceVM(string Name, EEResource data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("BackFaceType", data.BackFaceType));

            Childs.Add(new JMXOption("SrcBlend", data.SrcBlend, JMXOption.GetValues<object>(typeof(D3DBLEND))));
            Childs.Add(new JMXOption("DstBlend", data.DstBlend, JMXOption.GetValues<object>(typeof(D3DBLEND))));
            Childs.Add(new JMXOption("SrcTextureArg1", data.SrcTextureArg1, JMXOption.GetValues<object>(typeof(D3DTEXTUREARG))));
            Childs.Add(new JMXOption("SrcTextureArg2", data.SrcTextureArg2, JMXOption.GetValues<object>(typeof(D3DTEXTUREARG))));
            Childs.Add(new JMXOption("SrcTextureOP", data.SrcTextureOP, JMXOption.GetValues<object>(typeof(D3DTEXTUREOP))));
            Childs.Add(new JMXOption("DstTextureArg1", data.DstTextureArg1, JMXOption.GetValues<object>(typeof(D3DTEXTUREARG))));
            Childs.Add(new JMXOption("DstTextureArg2", data.DstTextureArg2, JMXOption.GetValues<object>(typeof(D3DTEXTUREARG))));
            Childs.Add(new JMXOption("DstTextureOP", data.DstTextureOP, JMXOption.GetValues<object>(typeof(D3DTEXTUREOP))));

            AddFormatHandler(typeof(EFMesh), (s, e) => {
                e.Childs.Add(new EFMeshVM("[" + e.Childs.Count + "]", e.Obj is EFMesh _obj ? _obj : new EFMesh()));
            });
            AddChildArray("Meshes", data.Meshes.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EEResource()
            {
                BackFaceType = (uint)((JMXAttribute)s.Childs[i++]).Value,

                SrcBlend = (D3DBLEND)((JMXOption)s.Childs[i++]).Value,
                DstBlend = (D3DBLEND)((JMXOption)s.Childs[i++]).Value,
                SrcTextureArg1 = (D3DTEXTUREARG)((JMXOption)s.Childs[i++]).Value,
                SrcTextureArg2 = (D3DTEXTUREARG)((JMXOption)s.Childs[i++]).Value,
                SrcTextureOP = (D3DTEXTUREOP)((JMXOption)s.Childs[i++]).Value,
                DstTextureArg1 = (D3DTEXTUREARG)((JMXOption)s.Childs[i++]).Value,
                DstTextureArg2 = (D3DTEXTUREARG)((JMXOption)s.Childs[i++]).Value,
                DstTextureOP = (D3DTEXTUREOP)((JMXOption)s.Childs[i++]).Value,

                Meshes = ((JMXStructure)s.Childs[i++]).GetChildList<EFMesh>(),
            };
        }
        #endregion
    }
}
