// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.MP
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class MP
  {
    public static readonly XNamespace mp = (XNamespace) "http://schemas.microsoft.com/office/mac/powerpoint/2008/main";
    public static readonly XName cube = MP.mp + nameof (cube);
    public static readonly XName flip = MP.mp + nameof (flip);
    public static readonly XName transition = MP.mp + nameof (transition);
  }
}
