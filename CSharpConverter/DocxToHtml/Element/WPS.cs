// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.WPS
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class WPS
  {
    public static readonly XNamespace wps = (XNamespace) "http://schemas.microsoft.com/office/word/2010/wordprocessingShape";
    public static readonly XName altTxbx = WPS.wps + nameof (altTxbx);
    public static readonly XName bodyPr = WPS.wps + nameof (bodyPr);
    public static readonly XName cNvSpPr = WPS.wps + nameof (cNvSpPr);
    public static readonly XName spPr = WPS.wps + nameof (spPr);
    public static readonly XName style = WPS.wps + nameof (style);
    public static readonly XName textbox = WPS.wps + nameof (textbox);
    public static readonly XName txbx = WPS.wps + nameof (txbx);
    public static readonly XName wsp = WPS.wps + nameof (wsp);
  }
}
