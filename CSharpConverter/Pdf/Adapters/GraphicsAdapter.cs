// Decompiled with JetBrains decompiler
// Type: VetCV.HtmlRendererCore.PdfSharpCore.Adapters.GraphicsAdapter
// Assembly: HtmlRendererCore.PdfSharpCore, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA72F8E-2C1A-42B6-AF29-CEB7845EFBE4
// Assembly location: C:\Users\Abbosbek\.nuget\packages\htmlrenderercore.pdfsharpcore\1.0.1\lib\netcoreapp2.0\HtmlRendererCore.PdfSharpCore.dll

using PdfSharpCore.Drawing;
using System;
using TheArtOfDev.HtmlRenderer.Adapters;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;
using TheArtOfDev.HtmlRenderer.Core.Utils;

namespace CSharpConverter.Pdf.Adapters
{
  internal sealed class GraphicsAdapter : RGraphics
  {
    private readonly XGraphics _g;
    private readonly bool _releaseGraphics;
    private static readonly XStringFormat _stringFormat = new XStringFormat();

    static GraphicsAdapter()
    {
      GraphicsAdapter._stringFormat.Alignment = (XStringAlignment) 0;
      GraphicsAdapter._stringFormat.LineAlignment = (XLineAlignment) 0;
    }

    public GraphicsAdapter(XGraphics g, bool releaseGraphics = false)
      : base((RAdapter) PdfSharpAdapter.Instance, new RRect(0.0, 0.0, double.MaxValue, double.MaxValue))
    {
      ArgChecker.AssertArgNotNull((object) g, nameof (g));
      this._g = g;
      this._releaseGraphics = releaseGraphics;
    }

    public override void PopClip()
    {
      this._clipStack.Pop();
      this._g.Restore();
    }

    public override void PushClip(RRect rect)
    {
      this._clipStack.Push(rect);
      this._g.Save();
      this._g.IntersectClip(Utilities.Utils.Convert(rect));
    }

    public override void PushClipExclude(RRect rect)
    {
    }

    public override object SetAntiAliasSmoothingMode()
    {
      XSmoothingMode smoothingMode = this._g.SmoothingMode;
      this._g.SmoothingMode = (XSmoothingMode) 4;
      return (object) smoothingMode;
    }

    public override void ReturnPreviousSmoothingMode(object prevMode)
    {
      if (prevMode == null)
        return;
      this._g.SmoothingMode = (XSmoothingMode) prevMode;
    }

    public override RSize MeasureString(string str, IRFont font)
    {
      FontAdapter fontAdapter = (FontAdapter) font;
      XFont font1 = fontAdapter.Font;
      XSize s = this._g.MeasureString(str, font1, GraphicsAdapter._stringFormat);
      if (font.FontHeight < 0.0)
      {
        int height = font1.Height;
        double num = font1.Size * (double) font1.FontFamily.GetCellDescent(font1.Style) / (double) font1.FontFamily.GetEmHeight(font1.Style);
        fontAdapter.SetMetrics(height, (int) Math.Round((double) height - num + 1.0));
      }
      return Utilities.Utils.Convert(s);
    }

    public override void MeasureString(
      string str,
      IRFont font,
      double maxWidth,
      out int charFit,
      out double charFitWidth)
    {
      throw new NotSupportedException();
    }

    public override void DrawString(
      string str,
      IRFont font,
      RColor color,
      RPoint point,
      RSize size,
      bool rtl)
    {
      object brush = ((BrushAdapter) this._adapter.GetSolidBrush(color)).Brush;
      this._g.DrawString(str, ((FontAdapter) font).Font, (XBrush) brush, ((RPoint) point).X, ((RPoint) point).Y, GraphicsAdapter._stringFormat);
    }

    public override IRBrush GetTextureBrush(
      IRImage image,
      RRect dstRect,
      RPoint translateTransformLocation)
    {
      return (IRBrush) new BrushAdapter((object) new XTextureBrush(((ImageAdapter) image).Image, Utilities.Utils.Convert(dstRect), Utilities.Utils.Convert(translateTransformLocation)));
    }

    public override IRGraphicsPath GetGraphicsPath() => (IRGraphicsPath) new GraphicsPathAdapter();

    public override void Dispose()
    {
      if (!this._releaseGraphics)
        return;
      this._g.Dispose();
    }

    public override void DrawLine(IRPen pen, double x1, double y1, double x2, double y2) => this._g.DrawLine(((PenAdapter) pen).Pen, x1, y1, x2, y2);

    public override void DrawRectangle(IRPen pen, double x, double y, double width, double height) => this._g.DrawRectangle(((PenAdapter) pen).Pen, x, y, width, height);

    public override void DrawRectangle(
      IRBrush brush,
      double x,
      double y,
      double width,
      double height)
    {
      object brush1 = ((BrushAdapter) brush).Brush;
      if (brush1 is XTextureBrush xtextureBrush)
      {
        xtextureBrush.DrawRectangle(this._g, x, y, width, height);
      }
      else
      {
        this._g.DrawRectangle((XBrush) brush1, x, y, width, height);
        if (!(brush1 is XLinearGradientBrush))
          return;
        this._g.DrawRectangle((XBrush) XBrushes.White, 0.0, 0.0, 0.1, 0.1);
      }
    }

    public override void DrawImage(IRImage image, RRect destRect, RRect srcRect) => this._g.DrawImage(((ImageAdapter) image).Image, Utilities.Utils.Convert(destRect), Utilities.Utils.Convert(srcRect), (XGraphicsUnit) 0);

    public override void DrawImage(IRImage image, RRect destRect) => this._g.DrawImage(((ImageAdapter) image).Image, Utilities.Utils.Convert(destRect));

    public override void DrawPath(IRPen pen, IRGraphicsPath path) => this._g.DrawPath(((PenAdapter) pen).Pen, ((GraphicsPathAdapter) path).GraphicsPath);

    public override void DrawPath(IRBrush brush, IRGraphicsPath path) => this._g.DrawPath((XBrush) ((BrushAdapter) brush).Brush, ((GraphicsPathAdapter) path).GraphicsPath);

    public override void DrawPolygon(IRBrush brush, RPoint[] points)
    {
      if (points == null || points.Length == 0)
        return;
      this._g.DrawPolygon((XBrush) ((BrushAdapter) brush).Brush, Utilities.Utils.Convert(points), (XFillMode) 1);
    }
  }
}
