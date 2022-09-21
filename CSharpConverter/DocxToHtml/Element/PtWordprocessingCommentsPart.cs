// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.PtWordprocessingCommentsPart
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public class PtWordprocessingCommentsPart : XElement
  {
    private WmlDocument ParentWmlDocument;

    public PtWordprocessingCommentsPart(
      WmlDocument wmlDocument,
      Uri uri,
      XName name,
      params object[] values)
      : base(name, values)
    {
      this.ParentWmlDocument = wmlDocument;
      this.Add((object) new XAttribute(PtOpenXml.Uri, (object) uri), (object) new XAttribute(XNamespace.Xmlns + "pt", (object) PtOpenXml.pt));
    }
  }
}
