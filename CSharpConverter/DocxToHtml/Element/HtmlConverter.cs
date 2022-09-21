// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.HtmlConverter
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class HtmlConverter
  {
    public static XElement ConvertToHtml(
      WmlDocument wmlDoc,
      HtmlConverterSettings htmlConverterSettings)
    {
      WmlToHtmlConverterSettings htmlConverterSettings1 = new WmlToHtmlConverterSettings(htmlConverterSettings);
      return WmlToHtmlConverter.ConvertToHtml(wmlDoc, htmlConverterSettings1);
    }

    public static XElement ConvertToHtml(
      WordprocessingDocument wDoc,
      HtmlConverterSettings htmlConverterSettings)
    {
      WmlToHtmlConverterSettings htmlConverterSettings1 = new WmlToHtmlConverterSettings(htmlConverterSettings);
      return WmlToHtmlConverter.ConvertToHtml(wDoc, htmlConverterSettings1);
    }
  }
}
