// Decompiled with JetBrains decompiler
// Type: VetCV.HtmlRendererCore.PdfSharpCore.PdfGenerateConfig
// Assembly: HtmlRendererCore.PdfSharpCore, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA72F8E-2C1A-42B6-AF29-CEB7845EFBE4
// Assembly location: C:\Users\Abbosbek\.nuget\packages\htmlrenderercore.pdfsharpcore\1.0.1\lib\netcoreapp2.0\HtmlRendererCore.PdfSharpCore.dll

using PdfSharpCore;
using PdfSharpCore.Drawing;

namespace CSharpConverter.Pdf
{
  public sealed class PdfGenerateConfig
  {
    private PageSize _pageSize;
    private XSize _xsize;
    private PageOrientation _pageOrientation;
    private int _marginTop;
    private int _marginBottom;
    private int _marginLeft;
    private int _marginRight;

    public PageSize PageSize
    {
      get => this._pageSize;
      set => this._pageSize = value;
    }

    public XSize ManualPageSize
    {
      get => this._xsize;
      set => this._xsize = value;
    }

    public PageOrientation PageOrientation
    {
      get => this._pageOrientation;
      set => this._pageOrientation = value;
    }

    public int MarginTop
    {
      get => this._marginTop;
      set
      {
        if (value <= -1)
          return;
        this._marginTop = value;
      }
    }

    public int MarginBottom
    {
      get => this._marginBottom;
      set
      {
        if (value <= -1)
          return;
        this._marginBottom = value;
      }
    }

    public int MarginLeft
    {
      get => this._marginLeft;
      set
      {
        if (value <= -1)
          return;
        this._marginLeft = value;
      }
    }

    public int MarginRight
    {
      get => this._marginRight;
      set
      {
        if (value <= -1)
          return;
        this._marginRight = value;
      }
    }

    public void SetMargins(int value)
    {
      if (value <= -1)
        return;
      this._marginBottom = this._marginLeft = this._marginTop = this._marginRight = value;
    }

    public static XSize MilimitersToUnits(double width, double height) => new XSize(width / 25.4 * 72.0, height / 25.4 * 72.0);

    public static XSize InchesToUnits(double width, double height) => new XSize(width * 72.0, height * 72.0);
  }
}
