// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.Pic
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class Pic
  {
    public static readonly XNamespace pic = (XNamespace) "http://schemas.openxmlformats.org/drawingml/2006/picture";
    public static readonly XName blipFill = Pic.pic + nameof (blipFill);
    public static readonly XName cNvPicPr = Pic.pic + nameof (cNvPicPr);
    public static readonly XName cNvPr = Pic.pic + nameof (cNvPr);
    public static readonly XName nvPicPr = Pic.pic + nameof (nvPicPr);
    public static readonly XName _pic = Pic.pic + nameof (pic);
    public static readonly XName spPr = Pic.pic + nameof (spPr);
  }
}
