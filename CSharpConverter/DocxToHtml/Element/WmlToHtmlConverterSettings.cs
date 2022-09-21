// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.WmlToHtmlConverterSettings
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public class WmlToHtmlConverterSettings
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

    public WmlToHtmlConverterSettings()
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

    public WmlToHtmlConverterSettings(HtmlConverterSettings htmlConverterSettings)
    {
      this.PageTitle = htmlConverterSettings.PageTitle;
      this.CssClassPrefix = htmlConverterSettings.CssClassPrefix;
      this.FabricateCssClasses = htmlConverterSettings.FabricateCssClasses;
      this.GeneralCss = htmlConverterSettings.GeneralCss;
      this.AdditionalCss = htmlConverterSettings.AdditionalCss;
      this.RestrictToSupportedLanguages = htmlConverterSettings.RestrictToSupportedLanguages;
      this.RestrictToSupportedNumberingFormats = htmlConverterSettings.RestrictToSupportedNumberingFormats;
      this.ListItemImplementations = htmlConverterSettings.ListItemImplementations;
      this.ImageHandler = htmlConverterSettings.ImageHandler;
    }
  }
}
