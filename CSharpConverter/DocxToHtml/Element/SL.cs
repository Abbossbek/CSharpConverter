// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.SL
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class SL
  {
    public static readonly XNamespace sl = (XNamespace) "http://schemas.openxmlformats.org/schemaLibrary/2006/main";
    public static readonly XName manifestLocation = SL.sl + nameof (manifestLocation);
    public static readonly XName schema = SL.sl + nameof (schema);
    public static readonly XName schemaLibrary = SL.sl + nameof (schemaLibrary);
    public static readonly XName uri = SL.sl + nameof (uri);
  }
}
