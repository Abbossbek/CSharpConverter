// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.MC
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class MC
  {
    public static readonly XNamespace mc = (XNamespace) "http://schemas.openxmlformats.org/markup-compatibility/2006";
    public static readonly XName AlternateContent = MC.mc + nameof (AlternateContent);
    public static readonly XName Choice = MC.mc + nameof (Choice);
    public static readonly XName Fallback = MC.mc + nameof (Fallback);
    public static readonly XName Ignorable = MC.mc + nameof (Ignorable);
    public static readonly XName PreserveAttributes = MC.mc + nameof (PreserveAttributes);
  }
}
