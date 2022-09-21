// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.XM
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class XM
  {
    public static readonly XNamespace xm = (XNamespace) "http://schemas.microsoft.com/office/excel/2006/main";
    public static readonly XName f = XM.xm + nameof (f);
    public static readonly XName _ref = XM.xm + "ref";
    public static readonly XName sqref = XM.xm + nameof (sqref);
  }
}
