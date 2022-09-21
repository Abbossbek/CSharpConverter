// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.DCTERMS
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class DCTERMS
  {
    public static readonly XNamespace dcterms = (XNamespace) "http://purl.org/dc/terms/";
    public static readonly XName created = DCTERMS.dcterms + nameof (created);
    public static readonly XName modified = DCTERMS.dcterms + nameof (modified);
  }
}
