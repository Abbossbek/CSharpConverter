// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.PAV
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class PAV
  {
    public static readonly XNamespace pav = (XNamespace) "http://schemas.microsoft.com/office/2007/6/19/audiovideo";
    public static readonly XName media = PAV.pav + nameof (media);
    public static readonly XName srcMedia = PAV.pav + nameof (srcMedia);
    public static readonly XName bmkLst = PAV.pav + nameof (bmkLst);
  }
}
