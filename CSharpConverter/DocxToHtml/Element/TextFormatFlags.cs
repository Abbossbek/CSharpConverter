using System;

namespace CSharpConverter.DocxToHtml.Element
{

    [Flags]
    public enum TextFormatFlags
    {
        Default = 0x0,
        Left = 0x0,
        Top = 0x0,
        GlyphOverhangPadding = 0x0,
        HorizontalCenter = 0x1,
        Right = 0x2,
        VerticalCenter = 0x4,
        Bottom = 0x8,
        WordBreak = 0x10,
        SingleLine = 0x20,
        ExpandTabs = 0x40,
        NoClipping = 0x100,
        ExternalLeading = 0x200,
        NoPrefix = 0x800,
        Internal = 0x1000,
        TextBoxControl = 0x2000,
        PathEllipsis = 0x4000,
        EndEllipsis = 0x8000,
        ModifyString = 0x10000,
        RightToLeft = 0x20000,
        WordEllipsis = 0x40000,
        NoFullWidthCharacterBreak = 0x80000,
        HidePrefix = 0x100000,
        PrefixOnly = 0x200000,
        PreserveGraphicsClipping = 0x1000000,
        PreserveGraphicsTranslateTransform = 0x2000000,
        NoPadding = 0x10000000,
        LeftAndRightPadding = 0x20000000
    }
}
