// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.Plegacy
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class Plegacy
  {
    public static readonly XNamespace plegacy = (XNamespace) "urn:schemas-microsoft-com:office:powerpoint";
    public static readonly XName textdata = Plegacy.plegacy + nameof (textdata);
  }
}
