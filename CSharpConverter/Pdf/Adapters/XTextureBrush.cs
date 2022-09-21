// Decompiled with JetBrains decompiler
// Type: VetCV.HtmlRendererCore.PdfSharpCore.Adapters.XTextureBrush
// Assembly: HtmlRendererCore.PdfSharpCore, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA72F8E-2C1A-42B6-AF29-CEB7845EFBE4
// Assembly location: C:\Users\Abbosbek\.nuget\packages\htmlrenderercore.pdfsharpcore\1.0.1\lib\netcoreapp2.0\HtmlRendererCore.PdfSharpCore.dll

using PdfSharpCore.Drawing;

namespace CSharpConverter.Pdf.Adapters
{
  internal sealed class XTextureBrush
  {
    private readonly XImage _image;
    private readonly XRect _dstRect;
    private readonly XPoint _translateTransformLocation;

    public XTextureBrush(XImage image, XRect dstRect, XPoint translateTransformLocation)
    {
      this._image = image;
      this._dstRect = dstRect;
      this._translateTransformLocation = translateTransformLocation;
    }

    public void DrawRectangle(XGraphics g, double x, double y, double width, double height)
    {
      XGraphicsState xgraphicsState = g.Save();
      g.IntersectClip(new XRect(x, y, width, height));
      XPoint transformLocation1 = this._translateTransformLocation;
      double x1 = ((XPoint) transformLocation1).X;
      double pixelWidth = (double) this._image.PixelWidth;
      double pixelHeight = (double) this._image.PixelHeight;
      for (; x1 < x + width; x1 += pixelWidth)
      {
        XPoint transformLocation2 = this._translateTransformLocation;
        for (double y1 = ((XPoint) transformLocation2).Y; y1 < y + height; y1 += pixelHeight)
          g.DrawImage(this._image, x1, y1, pixelWidth, pixelHeight);
      }
      g.Restore(xgraphicsState);
    }
  }
}
