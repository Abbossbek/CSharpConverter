// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.MDSSI
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class MDSSI
  {
    public static readonly XNamespace mdssi = (XNamespace) "http://schemas.openxmlformats.org/package/2006/digital-signature";
    public static readonly XName Format = MDSSI.mdssi + nameof (Format);
    public static readonly XName RelationshipReference = MDSSI.mdssi + nameof (RelationshipReference);
    public static readonly XName SignatureTime = MDSSI.mdssi + nameof (SignatureTime);
    public static readonly XName Value = MDSSI.mdssi + nameof (Value);
  }
}
