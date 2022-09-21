// Decompiled with JetBrains decompiler
// Type: VetCV.HtmlRendererCore.PdfSharpCore.Adapters.GraphicsPathAdapter
// Assembly: HtmlRendererCore.PdfSharpCore, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA72F8E-2C1A-42B6-AF29-CEB7845EFBE4
// Assembly location: C:\Users\Abbosbek\.nuget\packages\htmlrenderercore.pdfsharpcore\1.0.1\lib\netcoreapp2.0\HtmlRendererCore.PdfSharpCore.dll

using PdfSharpCore.Drawing;
using System;
using TheArtOfDev.HtmlRenderer.Adapters;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;

namespace CSharpConverter.Pdf.Adapters
{
  internal sealed class GraphicsPathAdapter : IRGraphicsPath
  {
    private readonly XGraphicsPath _graphicsPath = new XGraphicsPath();
    private RPoint _lastPoint;

    public XGraphicsPath GraphicsPath => this._graphicsPath;

    public void StartPath(double x, double y) => this._lastPoint = new RPoint(x, y);

    public void LineTo(double x, double y)
    {
      this._graphicsPath.AddLine(((RPoint) this._lastPoint).X, ((RPoint) this._lastPoint).Y, x, y);
      this._lastPoint = new RPoint(x, y);
    }

    public void ArcTo(double x, double y, double size, IRGraphicsPath.Corner corner)
    {
      this._graphicsPath.AddArc(Math.Min(x, ((RPoint) this._lastPoint).X) - ((int)corner == 1 || (int)corner == 3 ? size : 0.0), Math.Min(y, ((RPoint) this._lastPoint).Y) - ((int)corner == 2 || (int)corner == 3 ? size : 0.0), size * 2.0, size * 2.0, (double) GraphicsPathAdapter.GetStartAngle(corner), 90.0);
      this._lastPoint = new RPoint(x, y);
    }

    public void Dispose()
    {
    }

    private static int GetStartAngle(IRGraphicsPath.Corner corner)
    {
      switch ((int) corner)
      {
        case 0:
          return 180;
        case 1:
          return 270;
        case 2:
          return 90;
        case 3:
          return 0;
        default:
          throw new ArgumentOutOfRangeException(nameof (corner));
      }
    }
  }
}
