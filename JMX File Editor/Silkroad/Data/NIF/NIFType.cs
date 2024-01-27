using System;
using System.Linq;

namespace JMXFileEditor.Silkroad.Data.NIF
{
    /// <summary>
    /// 00489D5B  |. 83F8 14        CMP EAX,14                               ;  Switch (cases 0..14)
    /// </summary>
    public enum NIFType : int
    {
        NIFMainFrame = 0,
        NIFrame = 1,
        NIFNormaltile = 2,
        NIFStretch = 3,
        NIFButton = 4,
        NIFStatic = 5,
        NIFEdit = 6,
        NIFTextBox = 7,
        NIFSlot = 8,
        NIFLattice = 9,
        NIFGauge = 10,
        NIFCheckBox = 11,
        NIFComboBox = 12,
        NIFVirticalScroll = 13,
        NIFPageManager = 14,
        NIFBarWnd = 15,
        NIFTabButton = 16,
        NIFBothSidesGauge = 17,
        NIFWnd = 18,
        NIFSlideCtrl = 19,
        NIFSpinButtonCtrl = 20,
    }
}
