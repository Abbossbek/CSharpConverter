// Decompiled with JetBrains decompiler
// Type: VetCV.HtmlRendererCore.PdfSharpCore.Adapters.FontFamilyAdapter
// Assembly: HtmlRendererCore.PdfSharpCore, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA72F8E-2C1A-42B6-AF29-CEB7845EFBE4
// Assembly location: C:\Users\Abbosbek\.nuget\packages\htmlrenderercore.pdfsharpcore\1.0.1\lib\netcoreapp2.0\HtmlRendererCore.PdfSharpCore.dll

using PdfSharpCore.Drawing;
using TheArtOfDev.HtmlRenderer.Adapters;

namespace CSharpConverter.Pdf.Adapters
{
  internal sealed class FontFamilyAdapter : IRFontFamily
  {
    private readonly XFontFamily _fontFamily;

    public FontFamilyAdapter(XFontFamily fontFamily) => this._fontFamily = fontFamily;

    public XFontFamily FontFamily => this._fontFamily;
        public  string FontName => this._fontFamily.Name;
    }
}
