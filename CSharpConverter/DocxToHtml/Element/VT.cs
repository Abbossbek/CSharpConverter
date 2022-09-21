// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.VT
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class VT
  {
    public static readonly XNamespace vt = (XNamespace) "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes";
    public static readonly XName _bool = VT.vt + "bool";
    public static readonly XName filetime = VT.vt + nameof (filetime);
    public static readonly XName i4 = VT.vt + nameof (i4);
    public static readonly XName lpstr = VT.vt + nameof (lpstr);
    public static readonly XName lpwstr = VT.vt + nameof (lpwstr);
    public static readonly XName r8 = VT.vt + nameof (r8);
    public static readonly XName variant = VT.vt + nameof (variant);
    public static readonly XName vector = VT.vt + nameof (vector);
  }
}
