// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.CP
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class CP
  {
    public static readonly XNamespace cp = (XNamespace) "http://schemas.openxmlformats.org/package/2006/metadata/core-properties";
    public static readonly XName category = CP.cp + nameof (category);
    public static readonly XName contentStatus = CP.cp + nameof (contentStatus);
    public static readonly XName contentType = CP.cp + nameof (contentType);
    public static readonly XName coreProperties = CP.cp + nameof (coreProperties);
    public static readonly XName keywords = CP.cp + nameof (keywords);
    public static readonly XName lastModifiedBy = CP.cp + nameof (lastModifiedBy);
    public static readonly XName lastPrinted = CP.cp + nameof (lastPrinted);
    public static readonly XName revision = CP.cp + nameof (revision);
  }
}
