// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.WmlDocument
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public class WmlDocument : DocxToHtmlDocument
  {
    public WmlDocument SimplifyMarkup(SimplifyMarkupSettings settings) => MarkupSimplifier.SimplifyMarkup(this, settings);

    public WmlDocument(DocxToHtmlDocument original)
      : base(original)
    {
      if (this.GetDocumentType() != typeof (WordprocessingDocument))
        throw new PowerToolsDocumentException("Not a Wordprocessing document.");
    }

    public WmlDocument(DocxToHtmlDocument original, bool convertToTransitional)
      : base(original, convertToTransitional)
    {
      if (this.GetDocumentType() != typeof (WordprocessingDocument))
        throw new PowerToolsDocumentException("Not a Wordprocessing document.");
    }

    public WmlDocument(string fileName)
      : base(fileName)
    {
      if (this.GetDocumentType() != typeof (WordprocessingDocument))
        throw new PowerToolsDocumentException("Not a Wordprocessing document.");
    }

    public WmlDocument(string fileName, bool convertToTransitional)
      : base(fileName, convertToTransitional)
    {
      if (this.GetDocumentType() != typeof (WordprocessingDocument))
        throw new PowerToolsDocumentException("Not a Wordprocessing document.");
    }

    public WmlDocument(string fileName, byte[] byteArray)
      : base(byteArray)
    {
      this.FileName = fileName;
      if (this.GetDocumentType() != typeof (WordprocessingDocument))
        throw new PowerToolsDocumentException("Not a Wordprocessing document.");
    }

    public WmlDocument(string fileName, byte[] byteArray, bool convertToTransitional)
      : base(byteArray, convertToTransitional)
    {
      this.FileName = fileName;
      if (this.GetDocumentType() != typeof (WordprocessingDocument))
        throw new PowerToolsDocumentException("Not a Wordprocessing document.");
    }

    public WmlDocument(string fileName, MemoryStream memStream)
      : base(fileName, memStream)
    {
    }

    public WmlDocument(string fileName, MemoryStream memStream, bool convertToTransitional)
      : base(fileName, memStream, convertToTransitional)
    {
    }

    public WmlDocument AcceptRevisions(WmlDocument document) => RevisionAccepter.AcceptRevisions(document);

    public bool HasTrackedRevisions(WmlDocument document) => RevisionAccepter.HasTrackedRevisions(document);

    public PtMainDocumentPart MainDocumentPart
    {
      get
      {
        using (MemoryStream memoryStream = new MemoryStream(this.DocumentByteArray))
        {
          using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open((Stream) memoryStream, false))
          {
            XElement root = wordprocessingDocument.MainDocumentPart.GetXDocument().Root;
            List<XNode> list = root.Nodes().ToList<XNode>();
            foreach (XNode xnode in list)
              xnode.Remove();
            return new PtMainDocumentPart(this, wordprocessingDocument.MainDocumentPart.Uri, root.Name, new object[2]
            {
              (object) root.Attributes(),
              (object) list
            });
          }
        }
      }
    }

    public WmlDocument(WmlDocument other, params XElement[] replacementParts)
      : base((DocxToHtmlDocument) other)
    {
      using (OpenXmlMemoryStreamDocument memoryStreamDocument = new OpenXmlMemoryStreamDocument((DocxToHtmlDocument) this))
      {
        using (Package package = memoryStreamDocument.GetPackage())
        {
          foreach (XElement replacementPart in replacementParts)
          {
            string uri = (replacementPart.Attribute(PtOpenXml.Uri) ?? throw new DocxToHtmlException("Replacement part does not contain a Uri as an attribute")).Value;
            using (Stream stream = ((IEnumerable<PackagePart>) package.GetParts()).FirstOrDefault<PackagePart>((Func<PackagePart, bool>) (p => p.Uri.ToString() == uri)).GetStream(FileMode.Create, FileAccess.Write))
            {
              using (XmlWriter writer = XmlWriter.Create(stream))
                replacementPart.Save(writer);
            }
          }
        }
        this.DocumentByteArray = memoryStreamDocument.GetModifiedDocument().DocumentByteArray;
      }
    }

    public XElement ConvertToHtml(WmlToHtmlConverterSettings htmlConverterSettings) => WmlToHtmlConverter.ConvertToHtml(this, htmlConverterSettings);

    public XElement ConvertToHtml(HtmlConverterSettings htmlConverterSettings) => WmlToHtmlConverter.ConvertToHtml(this, new WmlToHtmlConverterSettings(htmlConverterSettings));
  }
}
