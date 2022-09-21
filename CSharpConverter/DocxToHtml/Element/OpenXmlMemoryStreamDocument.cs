// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.OpenXmlMemoryStreamDocument
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public class OpenXmlMemoryStreamDocument : IDisposable
  {
    private DocxToHtmlDocument Document;
    private MemoryStream DocMemoryStream;
    private Package DocPackage;

    public OpenXmlMemoryStreamDocument(DocxToHtmlDocument doc)
    {
      this.Document = doc;
      this.DocMemoryStream = new MemoryStream();
      this.DocMemoryStream.Write(doc.DocumentByteArray, 0, doc.DocumentByteArray.Length);
      try
      {
        this.DocPackage = Package.Open((Stream) this.DocMemoryStream, FileMode.Open);
      }
      catch (Exception ex)
      {
        throw new PowerToolsDocumentException(ex.Message);
      }
    }

    internal OpenXmlMemoryStreamDocument(MemoryStream stream)
    {
      this.DocMemoryStream = stream;
      try
      {
        this.DocPackage = Package.Open((Stream) this.DocMemoryStream, FileMode.Open);
      }
      catch (Exception ex)
      {
        throw new PowerToolsDocumentException(ex.Message);
      }
    }

    public static OpenXmlMemoryStreamDocument CreateWordprocessingDocument()
    {
      MemoryStream stream = new MemoryStream();
      using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Create((Stream) stream, WordprocessingDocumentType.Document))
      {
        wordprocessingDocument.AddMainDocumentPart();
        wordprocessingDocument.MainDocumentPart.PutXDocument(new XDocument(new object[1]
        {
          (object) new XElement(W.document, new object[3]
          {
            (object) new XAttribute(XNamespace.Xmlns + "w", (object) W.w),
            (object) new XAttribute(XNamespace.Xmlns + "r", (object) R.r),
            (object) new XElement(W.body)
          })
        }));
        wordprocessingDocument.Close();
        return new OpenXmlMemoryStreamDocument(stream);
      }
    }

    public static OpenXmlMemoryStreamDocument CreateSpreadsheetDocument()
    {
      MemoryStream stream = new MemoryStream();
      using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create((Stream) stream, SpreadsheetDocumentType.Workbook))
      {
        spreadsheetDocument.AddWorkbookPart();
        XNamespace xnamespace1 = (XNamespace) "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        XNamespace xnamespace2 = (XNamespace) "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        spreadsheetDocument.WorkbookPart.PutXDocument(new XDocument(new object[1]
        {
          (object) new XElement(xnamespace1 + "workbook", new object[3]
          {
            (object) new XAttribute((XName) "xmlns", (object) xnamespace1),
            (object) new XAttribute(XNamespace.Xmlns + "r", (object) xnamespace2),
            (object) new XElement(xnamespace1 + "sheets")
          })
        }));
        spreadsheetDocument.Close();
        return new OpenXmlMemoryStreamDocument(stream);
      }
    }

    public static OpenXmlMemoryStreamDocument CreatePresentationDocument()
    {
      MemoryStream stream = new MemoryStream();
      using (PresentationDocument presentationDocument = PresentationDocument.Create((Stream) stream, PresentationDocumentType.Presentation))
      {
        presentationDocument.AddPresentationPart();
        XNamespace xnamespace1 = (XNamespace) "http://schemas.openxmlformats.org/presentationml/2006/main";
        XNamespace xnamespace2 = (XNamespace) "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        XNamespace xnamespace3 = (XNamespace) "http://schemas.openxmlformats.org/drawingml/2006/main";
        presentationDocument.PresentationPart.PutXDocument(new XDocument(new object[1]
        {
          (object) new XElement(xnamespace1 + "presentation", new object[6]
          {
            (object) new XAttribute(XNamespace.Xmlns + "a", (object) xnamespace3),
            (object) new XAttribute(XNamespace.Xmlns + "r", (object) xnamespace2),
            (object) new XAttribute(XNamespace.Xmlns + "p", (object) xnamespace1),
            (object) new XElement(xnamespace1 + "sldMasterIdLst"),
            (object) new XElement(xnamespace1 + "sldIdLst"),
            (object) new XElement(xnamespace1 + "notesSz", new object[2]
            {
              (object) new XAttribute((XName) "cx", (object) "6858000"),
              (object) new XAttribute((XName) "cy", (object) "9144000")
            })
          })
        }));
        presentationDocument.Close();
        return new OpenXmlMemoryStreamDocument(stream);
      }
    }

    public static OpenXmlMemoryStreamDocument CreatePackage()
    {
      MemoryStream stream = new MemoryStream();
      Package.Open((Stream) stream, FileMode.Create).Close();
      return new OpenXmlMemoryStreamDocument(stream);
    }

    public Package GetPackage() => this.DocPackage;

    public WordprocessingDocument GetWordprocessingDocument()
    {
      try
      {
        if (this.GetDocumentType() != typeof (WordprocessingDocument))
          throw new PowerToolsDocumentException("Not a Wordprocessing document.");
        return WordprocessingDocument.Open(this.DocPackage);
      }
      catch (Exception ex)
      {
        throw new PowerToolsDocumentException(ex.Message);
      }
    }

    public SpreadsheetDocument GetSpreadsheetDocument()
    {
      try
      {
        if (this.GetDocumentType() != typeof (SpreadsheetDocument))
          throw new PowerToolsDocumentException("Not a Spreadsheet document.");
        return SpreadsheetDocument.Open(this.DocPackage);
      }
      catch (Exception ex)
      {
        throw new PowerToolsDocumentException(ex.Message);
      }
    }

    public PresentationDocument GetPresentationDocument()
    {
      try
      {
        if (this.GetDocumentType() != typeof (PresentationDocument))
          throw new PowerToolsDocumentException("Not a Presentation document.");
        return PresentationDocument.Open(this.DocPackage);
      }
      catch (Exception ex)
      {
        throw new PowerToolsDocumentException(ex.Message);
      }
    }

    public Type GetDocumentType()
    {
      PackageRelationship packageRelationship = ((IEnumerable<PackageRelationship>) this.DocPackage.GetRelationshipsByType("http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument")).FirstOrDefault<PackageRelationship>() ?? ((IEnumerable<PackageRelationship>) this.DocPackage.GetRelationshipsByType("http://purl.oclc.org/ooxml/officeDocument/relationships/officeDocument")).FirstOrDefault<PackageRelationship>();
      if (packageRelationship == null)
        throw new PowerToolsDocumentException("Not an Open XML Document.");
      switch (this.DocPackage.GetPart(PackUriHelper.ResolvePartUri(packageRelationship.SourceUri, packageRelationship.TargetUri)).ContentType)
      {
        case "application/vnd.ms-excel.sheet.macroEnabled.main+xml":
        case "application/vnd.ms-excel.template.macroEnabled.main+xml":
        case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml":
        case "application/vnd.openxmlformats-officedocument.spreadsheetml.template.main+xml":
          return typeof (SpreadsheetDocument);
        case "application/vnd.ms-powerpoint.addin.macroEnabled.main+xml":
        case "application/vnd.ms-powerpoint.presentation.macroEnabled.main+xml":
        case "application/vnd.ms-powerpoint.template.macroEnabled.main+xml":
        case "application/vnd.openxmlformats-officedocument.presentationml.presentation.main+xml":
        case "application/vnd.openxmlformats-officedocument.presentationml.slideshow.main+xml":
        case "application/vnd.openxmlformats-officedocument.presentationml.template.main+xml":
          return typeof (PresentationDocument);
        case "application/vnd.ms-word.document.macroEnabled.main+xml":
        case "application/vnd.ms-word.template.macroEnabledTemplate.main+xml":
        case "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml":
        case "application/vnd.openxmlformats-officedocument.wordprocessingml.template.main+xml":
          return typeof (WordprocessingDocument);
        default:
          return (Type) null;
      }
    }

    public DocxToHtmlDocument GetModifiedDocument()
    {
      this.DocPackage.Close();
      this.DocPackage = (Package) null;
      return new DocxToHtmlDocument(this.Document == null ? (string) null : this.Document.FileName, this.DocMemoryStream);
    }

    public WmlDocument GetModifiedWmlDocument()
    {
      this.DocPackage.Close();
      this.DocPackage = (Package) null;
      return new WmlDocument(this.Document == null ? (string) null : this.Document.FileName, this.DocMemoryStream);
    }

    public SmlDocument GetModifiedSmlDocument()
    {
      this.DocPackage.Close();
      this.DocPackage = (Package) null;
      return new SmlDocument(this.Document == null ? (string) null : this.Document.FileName, this.DocMemoryStream);
    }

    public PmlDocument GetModifiedPmlDocument()
    {
      this.DocPackage.Close();
      this.DocPackage = (Package) null;
      return new PmlDocument(this.Document == null ? (string) null : this.Document.FileName, this.DocMemoryStream);
    }

    public void Close() => this.Dispose(true);

    public void Dispose() => this.Dispose(true);

    ~OpenXmlMemoryStreamDocument() => this.Dispose(false);

    private void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.DocPackage != null)
          this.DocPackage.Close();
        if (this.DocMemoryStream != null)
          this.DocMemoryStream.Dispose();
      }
      if (this.DocPackage == null && this.DocMemoryStream == null)
        return;
      this.DocPackage = (Package) null;
      this.DocMemoryStream = (MemoryStream) null;
      GC.SuppressFinalize((object) this);
    }
  }
}
