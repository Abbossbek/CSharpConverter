// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.MV
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class MV
  {
    public static readonly XNamespace mv = (XNamespace) "urn:schemas-microsoft-com:mac:vml";
    public static readonly XName blur = MV.mv + nameof (blur);
    public static readonly XName complextextbox = MV.mv + nameof (complextextbox);
  }
}
