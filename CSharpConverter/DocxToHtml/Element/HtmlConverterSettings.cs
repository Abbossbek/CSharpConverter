// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.HtmlConverterSettings
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public class HtmlConverterSettings
  {
    public string PageTitle;
    public string CssClassPrefix;
    public bool FabricateCssClasses;
    public string GeneralCss;
    public string AdditionalCss;
    public bool RestrictToSupportedLanguages;
    public bool RestrictToSupportedNumberingFormats;
    public Dictionary<string, Func<string, int, string, string>> ListItemImplementations;
    public Func<ImageInfo, XElement> ImageHandler;

    public HtmlConverterSettings()
    {
      this.PageTitle = "";
      this.CssClassPrefix = "pt-";
      this.FabricateCssClasses = true;
      this.GeneralCss = "span { white-space: pre-wrap; }";
      this.AdditionalCss = "";
      this.RestrictToSupportedLanguages = false;
      this.RestrictToSupportedNumberingFormats = false;
      this.ListItemImplementations = ListItemRetrieverSettings.DefaultListItemTextImplementations;
    }
  }
}
