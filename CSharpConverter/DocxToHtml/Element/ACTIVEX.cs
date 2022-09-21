// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ACTIVEX
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class ACTIVEX
  {
    public static readonly XNamespace activex = (XNamespace) "http://schemas.microsoft.com/office/2006/activeX";
    public static readonly XName classid = ACTIVEX.activex + nameof (classid);
    public static readonly XName font = ACTIVEX.activex + nameof (font);
    public static readonly XName license = ACTIVEX.activex + nameof (license);
    public static readonly XName name = ACTIVEX.activex + nameof (name);
    public static readonly XName ocx = ACTIVEX.activex + nameof (ocx);
    public static readonly XName ocxPr = ACTIVEX.activex + nameof (ocxPr);
    public static readonly XName persistence = ACTIVEX.activex + nameof (persistence);
    public static readonly XName value = ACTIVEX.activex + nameof (value);
  }
}
