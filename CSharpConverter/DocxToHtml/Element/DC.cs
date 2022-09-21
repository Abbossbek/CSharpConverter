// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.DC
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class DC
  {
    public static readonly XNamespace dc = (XNamespace) "http://purl.org/dc/elements/1.1/";
    public static readonly XName creator = DC.dc + nameof (creator);
    public static readonly XName description = DC.dc + nameof (description);
    public static readonly XName subject = DC.dc + nameof (subject);
    public static readonly XName title = DC.dc + nameof (title);
  }
}
