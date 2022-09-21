// Decompiled with JetBrains decompiler
// Type: VetCV.HtmlRendererCore.PdfSharpCore.Adapters.PenAdapter
// Assembly: HtmlRendererCore.PdfSharpCore, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA72F8E-2C1A-42B6-AF29-CEB7845EFBE4
// Assembly location: C:\Users\Abbosbek\.nuget\packages\htmlrenderercore.pdfsharpcore\1.0.1\lib\netcoreapp2.0\HtmlRendererCore.PdfSharpCore.dll

using PdfSharpCore.Drawing;
using TheArtOfDev.HtmlRenderer.Adapters;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;

namespace CSharpConverter.Pdf.Adapters
{
  internal sealed class PenAdapter : IRPen
  {
    private readonly XPen _pen;

    public PenAdapter(XPen pen) => this._pen = pen;

    public XPen Pen => this._pen;

    public double PenWidth
    {
      get => this._pen.Width;
      set => this._pen.Width = value;
    }

    public RDashStyle DashStyle
    {
      set
      {
        switch ((int) value)
        {
          case 0:
            this._pen.DashStyle = (XDashStyle) 0;
            break;
          case 1:
            this._pen.DashStyle = (XDashStyle) 1;
            if (PenWidth >= 2.0)
              break;
            this._pen.DashPattern = new double[2]
            {
              4.0,
              4.0
            };
            break;
          case 2:
            this._pen.DashStyle = (XDashStyle) 2;
            break;
          case 3:
            this._pen.DashStyle = (XDashStyle) 3;
            break;
          case 4:
            this._pen.DashStyle = (XDashStyle) 4;
            break;
          case 5:
            this._pen.DashStyle = (XDashStyle) 5;
            break;
          default:
            this._pen.DashStyle = (XDashStyle) 0;
            break;
        }
      }
    }
  }
}
