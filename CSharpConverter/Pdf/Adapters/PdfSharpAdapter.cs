// Decompiled with JetBrains decompiler
// Type: VetCV.HtmlRendererCore.PdfSharpCore.Adapters.PdfSharpAdapter
// Assembly: HtmlRendererCore.PdfSharpCore, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA72F8E-2C1A-42B6-AF29-CEB7845EFBE4
// Assembly location: C:\Users\Abbosbek\.nuget\packages\htmlrenderercore.pdfsharpcore\1.0.1\lib\netcoreapp2.0\HtmlRendererCore.PdfSharpCore.dll

using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Drawing;
using System.IO;
using TheArtOfDev.HtmlRenderer.Adapters;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;
using CSharpConverter.Pdf.Utilities;

namespace CSharpConverter.Pdf.Adapters
{
  internal sealed class PdfSharpAdapter : RAdapter
  {
    private static readonly PdfSharpAdapter _instance = new PdfSharpAdapter();

    private PdfSharpAdapter() => this.AddFontFamily((IRFontFamily) new FontFamilyAdapter(new XFontFamily("monospace")));

    public static PdfSharpAdapter Instance => PdfSharpAdapter._instance;

    protected override RColor GetColorInt(string colorName)
    {
      try
      {
        return Utils.Convert(Color.FromName(colorName));
      }
      catch
      {
        return RColor.Empty;
      }
    }

    protected override IRPen CreatePen(RColor color) => (IRPen) new PenAdapter(new XPen(Utils.Convert(color)));

    protected override IRBrush CreateSolidBrush(RColor color) => (IRBrush) new BrushAdapter(!RColor.Equals(color, RColor.White) ? (!RColor.Equals(color, RColor.Black) ? (((RColor) color).A >= (byte) 1 ? (object) (XBrush) new XSolidBrush(Utils.Convert(color)) : (object) (XBrush) XBrushes.Transparent) : (object) (XBrush) XBrushes.Black) : (object) (XBrush) XBrushes.White);

    protected override IRBrush CreateLinearGradientBrush(
      RRect rect,
      RColor color1,
      RColor color2,
      double angle)
    {
      XLinearGradientMode xlinearGradientMode = angle >= 45.0 ? (angle >= 90.0 ? (angle >= 135.0 ? (XLinearGradientMode) 0 : (XLinearGradientMode) 3) : (XLinearGradientMode) 1) : (XLinearGradientMode) 2;
      return (IRBrush) new BrushAdapter((object) new XLinearGradientBrush(Utils.Convert(rect), Utils.Convert(color1), Utils.Convert(color2), xlinearGradientMode));
    }

    protected override IRImage ConvertImageInt(object image) => image == null ? (IRImage) null : (IRImage) new ImageAdapter((XImage) image);

    protected override IRImage ImageFromStreamInt(Stream memoryStream) => (IRImage) new ImageAdapter(XImage.FromStream((Func<Stream>) (() => memoryStream)));

    protected override IRFont CreateFontInt(string family, double size, RFontStyle style)
    {
      XFontStyle xfontStyle = (XFontStyle) style;
      return (IRFont) new FontAdapter(new XFont(family, size, xfontStyle, new XPdfFontOptions((PdfFontEncoding) 1)));
    }

    protected override IRFont CreateFontInt(IRFontFamily family, double size, RFontStyle style)
    {
      XFontStyle xfontStyle = (XFontStyle) style;
      return (IRFont) new FontAdapter(new XFont(((FontFamilyAdapter) family).FontFamily.Name, size, xfontStyle, new XPdfFontOptions((PdfFontEncoding) 1)));
    }
  }
}
