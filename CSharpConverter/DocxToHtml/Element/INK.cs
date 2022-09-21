// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.INK
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class INK
  {
    public static readonly XNamespace ink = (XNamespace) "http://schemas.microsoft.com/ink/2010/main";
    public static readonly XName context = INK.ink + nameof (context);
    public static readonly XName sourceLink = INK.ink + nameof (sourceLink);
  }
}
