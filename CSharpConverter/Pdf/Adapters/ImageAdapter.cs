// Decompiled with JetBrains decompiler
// Type: VetCV.HtmlRendererCore.PdfSharpCore.Adapters.ImageAdapter
// Assembly: HtmlRendererCore.PdfSharpCore, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA72F8E-2C1A-42B6-AF29-CEB7845EFBE4
// Assembly location: C:\Users\Abbosbek\.nuget\packages\htmlrenderercore.pdfsharpcore\1.0.1\lib\netcoreapp2.0\HtmlRendererCore.PdfSharpCore.dll

using PdfSharpCore.Drawing;
using TheArtOfDev.HtmlRenderer.Adapters;

namespace CSharpConverter.Pdf.Adapters
{
  internal sealed class ImageAdapter : IRImage
  {
    private readonly XImage _image;

    public ImageAdapter(XImage image) => this._image = image;

    public XImage Image => this._image;

    public double ImageWidth => (double) this._image.PixelWidth;

    public double ImageHeight => (double) this._image.PixelHeight;

    public void Dispose() => this._image.Dispose();
  }
}
