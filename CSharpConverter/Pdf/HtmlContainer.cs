// Decompiled with JetBrains decompiler
// Type: VetCV.HtmlRendererCore.PdfSharpCore.HtmlContainer
// Assembly: HtmlRendererCore.PdfSharpCore, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA72F8E-2C1A-42B6-AF29-CEB7845EFBE4
// Assembly location: C:\Users\Abbosbek\.nuget\packages\htmlrenderercore.pdfsharpcore\1.0.1\lib\netcoreapp2.0\HtmlRendererCore.PdfSharpCore.dll

using PdfSharpCore.Drawing;
using System;
using System.Collections.Generic;
using TheArtOfDev.HtmlRenderer.Adapters;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.Core.Utils;
using CSharpConverter.Pdf.Adapters;

namespace CSharpConverter.Pdf
{
  public sealed class HtmlContainer : IDisposable
  {
    private readonly HtmlContainerInt _htmlContainerInt;

    public HtmlContainer() => this._htmlContainerInt = new HtmlContainerInt((RAdapter) PdfSharpAdapter.Instance)
    {
      AvoidAsyncImagesLoading = true,
      AvoidImagesLateLoading = true
    };

    public event EventHandler LoadComplete
    {
      add => this._htmlContainerInt.LoadComplete += value;
      remove => this._htmlContainerInt.LoadComplete -= value;
    }

    public event EventHandler<HtmlRenderErrorEventArgs> RenderError
    {
      add => this._htmlContainerInt.RenderError += value;
      remove => this._htmlContainerInt.RenderError -= value;
    }

    public event EventHandler<HtmlStylesheetLoadEventArgs> StylesheetLoad
    {
      add => this._htmlContainerInt.StylesheetLoad += value;
      remove => this._htmlContainerInt.StylesheetLoad -= value;
    }

    public event EventHandler<HtmlImageLoadEventArgs> ImageLoad
    {
      add => this._htmlContainerInt.ImageLoad += value;
      remove => this._htmlContainerInt.ImageLoad -= value;
    }

    internal HtmlContainerInt HtmlContainerInt => this._htmlContainerInt;

    public CssData CssData => this._htmlContainerInt.CssData;

    public bool AvoidGeometryAntialias
    {
      get => this._htmlContainerInt.AvoidGeometryAntialias;
      set => this._htmlContainerInt.AvoidGeometryAntialias = value;
    }

    public XPoint ScrollOffset
    {
      get => Utilities.Utils.Convert(this._htmlContainerInt.ScrollOffset);
      set => this._htmlContainerInt.ScrollOffset = Utilities.Utils.Convert(value);
    }

    public XPoint Location
    {
      get => Utilities.Utils.Convert(this._htmlContainerInt.Location);
      set => this._htmlContainerInt.Location = Utilities.Utils.Convert(value);
    }

    public XSize MaxSize
    {
      get => Utilities.Utils.Convert(this._htmlContainerInt.MaxSize);
      set => this._htmlContainerInt.MaxSize = Utilities.Utils.Convert(value);
    }

    public XSize ActualSize
    {
      get => Utilities.Utils.Convert(this._htmlContainerInt.ActualSize);
      internal set => this._htmlContainerInt.ActualSize = Utilities.Utils.Convert(value);
    }

    public XSize PageSize
    {
      get
      {
        RSize pageSize = this._htmlContainerInt.PageSize;
        double width = ((RSize) pageSize).Width;
        pageSize = this._htmlContainerInt.PageSize;
        double height = ((RSize) pageSize).Height;
        return new XSize(width, height);
      }
      set => this._htmlContainerInt.PageSize = new RSize(((XSize) value).Width, ((XSize) value).Height);
    }

    public int MarginTop
    {
      get => this._htmlContainerInt.MarginTop;
      set
      {
        if (value <= -1)
          return;
        this._htmlContainerInt.MarginTop = value;
      }
    }

    public int MarginBottom
    {
      get => this._htmlContainerInt.MarginBottom;
      set
      {
        if (value <= -1)
          return;
        this._htmlContainerInt.MarginBottom = value;
      }
    }

    public int MarginLeft
    {
      get => this._htmlContainerInt.MarginLeft;
      set
      {
        if (value <= -1)
          return;
        this._htmlContainerInt.MarginLeft = value;
      }
    }

    public int MarginRight
    {
      get => this._htmlContainerInt.MarginRight;
      set
      {
        if (value <= -1)
          return;
        this._htmlContainerInt.MarginRight = value;
      }
    }

    public void SetMargins(int value)
    {
      if (value <= -1)
        return;
      this._htmlContainerInt.SetMargins(value);
    }

    public string SelectedText => this._htmlContainerInt.SelectedText;

    public string SelectedHtml => this._htmlContainerInt.SelectedHtml;

    public void SetHtml(string htmlSource, CssData baseCssData = null) => this._htmlContainerInt.SetHtml(htmlSource, baseCssData);

    public string GetHtml(HtmlGenerationStyle styleGen = HtmlGenerationStyle.Inline) => this._htmlContainerInt.GetHtml(styleGen);

    public string GetAttributeAt(XPoint location, string attribute) => this._htmlContainerInt.GetAttributeAt(Utilities.Utils.Convert(location), attribute);

    public List<LinkElementData<XRect>> GetLinks()
    {
      List<LinkElementData<XRect>> links = new List<LinkElementData<XRect>>();
      foreach (LinkElementData<RRect> link in this.HtmlContainerInt.GetLinks())
        links.Add(new LinkElementData<XRect>(link.Id, link.Href, Utilities.Utils.Convert(link.Rectangle)));
      return links;
    }

    public string GetLinkAt(XPoint location) => this._htmlContainerInt.GetLinkAt(Utilities.Utils.Convert(location));

    public XRect? GetElementRectangle(string elementId)
    {
      RRect? elementRectangle = this._htmlContainerInt.GetElementRectangle(elementId);
      return !elementRectangle.HasValue ? new XRect?() : new XRect?(Utilities.Utils.Convert(elementRectangle.Value));
    }

    public void PerformLayout(XGraphics g)
    {
      ArgChecker.AssertArgNotNull((object) g, nameof (g));
      using (GraphicsAdapter graphicsAdapter = new GraphicsAdapter(g))
        this._htmlContainerInt.PerformLayout((RGraphics) graphicsAdapter);
    }

    public void PerformPaint(XGraphics g)
    {
      ArgChecker.AssertArgNotNull((object) g, nameof (g));
      using (GraphicsAdapter graphicsAdapter = new GraphicsAdapter(g))
        this._htmlContainerInt.PerformPaint((RGraphics) graphicsAdapter);
    }

    public void Dispose() => this._htmlContainerInt.Dispose();
  }
}
