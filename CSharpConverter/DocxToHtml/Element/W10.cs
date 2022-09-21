// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.W10
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class W10
  {
    public static readonly XNamespace w10 = (XNamespace) "urn:schemas-microsoft-com:office:word";
    public static readonly XName anchorlock = W10.w10 + nameof (anchorlock);
    public static readonly XName borderbottom = W10.w10 + nameof (borderbottom);
    public static readonly XName borderleft = W10.w10 + nameof (borderleft);
    public static readonly XName borderright = W10.w10 + nameof (borderright);
    public static readonly XName bordertop = W10.w10 + nameof (bordertop);
    public static readonly XName wrap = W10.w10 + nameof (wrap);
  }
}
