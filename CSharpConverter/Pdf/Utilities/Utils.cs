// Decompiled with JetBrains decompiler
// Type: VetCV.HtmlRendererCore.PdfSharpCore.Utilities.Utils
// Assembly: HtmlRendererCore.PdfSharpCore, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA72F8E-2C1A-42B6-AF29-CEB7845EFBE4
// Assembly location: C:\Users\Abbosbek\.nuget\packages\htmlrenderercore.pdfsharpcore\1.0.1\lib\netcoreapp2.0\HtmlRendererCore.PdfSharpCore.dll

using PdfSharpCore.Drawing;
using System.Drawing;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;

namespace CSharpConverter.Pdf.Utilities
{
  internal static class Utils
  {
    public static RPoint Convert(XPoint p) => new RPoint(((XPoint) p).X, ((XPoint) p).Y);

    public static XPoint[] Convert(RPoint[] points)
    {
      XPoint[] xpointArray = new XPoint[points.Length];
      for (int index = 0; index < points.Length; ++index)
        xpointArray[index] = Utils.Convert(points[index]);
      return xpointArray;
    }

    public static XPoint Convert(RPoint p) => new XPoint(((RPoint)  p).X, ((RPoint)  p).Y);

    public static RSize Convert(XSize s) => new RSize(((XSize)  s).Width, ((XSize)  s).Height);

    public static XSize Convert(RSize s) => new XSize(((RSize)  s).Width, ((RSize)  s).Height);

    public static RRect Convert(XRect r) => new RRect(((XRect)  r).X, ((XRect)  r).Y, ((XRect)  r).Width, ((XRect)  r).Height);

    public static XRect Convert(RRect r) => new XRect(((RRect)  r).X, ((RRect)  r).Y, ((RRect)  r).Width, ((RRect)  r).Height);

    public static XColor Convert(RColor c) => XColor.FromArgb((int) ((RColor)  c).A, (int) ((RColor)  c).R, (int) ((RColor)  c).G, (int) ((RColor)  c).B);

    public static RColor Convert(Color c) => RColor.FromArgb((int) c.A, (int) c.R, (int) c.G, (int) c.B);
  }
}
