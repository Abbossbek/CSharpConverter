// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.PtMainDocumentPart
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public class PtMainDocumentPart : XElement
  {
    private WmlDocument ParentWmlDocument;

    public PtWordprocessingCommentsPart WordprocessingCommentsPart
    {
      get
      {
        using (MemoryStream memoryStream = new MemoryStream(this.ParentWmlDocument.DocumentByteArray))
        {
          using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open((Stream) memoryStream, false))
          {
            DocumentFormat.OpenXml.Packaging.WordprocessingCommentsPart wordprocessingCommentsPart = wordprocessingDocument.MainDocumentPart.WordprocessingCommentsPart;
            if (wordprocessingCommentsPart == null)
              return (PtWordprocessingCommentsPart) null;
            XElement root = wordprocessingCommentsPart.GetXDocument().Root;
            List<XNode> list = root.Nodes().ToList<XNode>();
            foreach (XNode xnode in list)
              xnode.Remove();
            return new PtWordprocessingCommentsPart(this.ParentWmlDocument, wordprocessingCommentsPart.Uri, root.Name, new object[2]
            {
              (object) root.Attributes(),
              (object) list
            });
          }
        }
      }
    }

    public PtMainDocumentPart(
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
