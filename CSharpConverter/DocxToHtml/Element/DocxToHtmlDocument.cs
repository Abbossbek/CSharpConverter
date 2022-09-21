// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.DocxToHtmlDocument
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

namespace CSharpConverter.DocxToHtml.Element
{
  public class DocxToHtmlDocument
  {
    public string FileName { get; set; }

    public byte[] DocumentByteArray { get; set; }

    public static DocxToHtmlDocument FromFileName(string fileName)
    {
      byte[] numArray = File.ReadAllBytes(fileName);
      Type documentType;
      try
      {
        documentType = DocxToHtmlDocument.GetDocumentType(numArray);
      }
      catch (FileFormatException ex)
      {
        throw new PowerToolsDocumentException("Not an Open XML document.");
      }
      if (documentType == typeof (WordprocessingDocument))
        return (DocxToHtmlDocument) new WmlDocument(fileName, numArray);
      if (documentType == typeof (SpreadsheetDocument))
        return (DocxToHtmlDocument) new SmlDocument(fileName, numArray);
      if (documentType == typeof (PresentationDocument))
        return (DocxToHtmlDocument) new PmlDocument(fileName, numArray);
      if (!(documentType == typeof (Package)))
        throw new PowerToolsDocumentException("Not an Open XML document.");
      return new DocxToHtmlDocument(numArray)
      {
        FileName = fileName
      };
    }

    public static DocxToHtmlDocument FromDocument(DocxToHtmlDocument doc)
    {
      Type documentType = doc.GetDocumentType();
      if (documentType == typeof (WordprocessingDocument))
        return (DocxToHtmlDocument) new WmlDocument(doc);
      if (documentType == typeof (SpreadsheetDocument))
        return (DocxToHtmlDocument) new SmlDocument(doc);
      return documentType == typeof (PresentationDocument) ? (DocxToHtmlDocument) new PmlDocument(doc) : (DocxToHtmlDocument) null;
    }

    public DocxToHtmlDocument(DocxToHtmlDocument original)
    {
      this.DocumentByteArray = new byte[original.DocumentByteArray.Length];
      Array.Copy((Array) original.DocumentByteArray, (Array) this.DocumentByteArray, original.DocumentByteArray.Length);
      this.FileName = original.FileName;
    }

    public DocxToHtmlDocument(DocxToHtmlDocument original, bool convertToTransitional)
    {
      if (convertToTransitional)
      {
        this.ConvertToTransitional(original.FileName, original.DocumentByteArray);
      }
      else
      {
        this.DocumentByteArray = new byte[original.DocumentByteArray.Length];
        Array.Copy((Array) original.DocumentByteArray, (Array) this.DocumentByteArray, original.DocumentByteArray.Length);
        this.FileName = original.FileName;
      }
    }

    public DocxToHtmlDocument(string fileName)
    {
      this.FileName = fileName;
      this.DocumentByteArray = File.ReadAllBytes(fileName);
    }

    public DocxToHtmlDocument(string fileName, bool convertToTransitional)
    {
      this.FileName = fileName;
      if (convertToTransitional)
      {
        byte[] tempByteArray = File.ReadAllBytes(fileName);
        this.ConvertToTransitional(fileName, tempByteArray);
      }
      else
      {
        this.FileName = fileName;
        this.DocumentByteArray = File.ReadAllBytes(fileName);
      }
    }

    private void ConvertToTransitional(string fileName, byte[] tempByteArray)
    {
      Type documentType;
      try
      {
        documentType = DocxToHtmlDocument.GetDocumentType(tempByteArray);
      }
      catch (FileFormatException ex)
      {
        throw new PowerToolsDocumentException("Not an Open XML document.");
      }
      using (MemoryStream memoryStream = new MemoryStream())
      {
        memoryStream.Write(tempByteArray, 0, tempByteArray.Length);
        if (documentType == typeof (WordprocessingDocument))
        {
          using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open((Stream) memoryStream, true))
          {
            foreach (IdPartPair part in wordprocessingDocument.Parts)
            {
              try
              {
                OpenXmlPartRootElement rootElement = part.OpenXmlPart.RootElement;
              }
              catch (Exception ex)
              {
              }
            }
          }
        }
        else if (documentType == typeof (SpreadsheetDocument))
        {
          using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open((Stream) memoryStream, true))
          {
            foreach (IdPartPair part in spreadsheetDocument.Parts)
            {
              try
              {
                OpenXmlPartRootElement rootElement = part.OpenXmlPart.RootElement;
              }
              catch (Exception ex)
              {
              }
            }
          }
        }
        else if (documentType == typeof (PresentationDocument))
        {
          using (PresentationDocument presentationDocument = PresentationDocument.Open((Stream) memoryStream, true))
          {
            foreach (IdPartPair part in presentationDocument.Parts)
            {
              try
              {
                OpenXmlPartRootElement rootElement = part.OpenXmlPart.RootElement;
              }
              catch (Exception ex)
              {
              }
            }
          }
        }
        this.FileName = fileName;
        this.DocumentByteArray = memoryStream.ToArray();
      }
    }

    public DocxToHtmlDocument(byte[] byteArray)
    {
      this.DocumentByteArray = new byte[byteArray.Length];
      Array.Copy((Array) byteArray, (Array) this.DocumentByteArray, byteArray.Length);
      this.FileName = (string) null;
    }

    public DocxToHtmlDocument(byte[] byteArray, bool convertToTransitional)
    {
      if (convertToTransitional)
      {
        this.ConvertToTransitional((string) null, byteArray);
      }
      else
      {
        this.DocumentByteArray = new byte[byteArray.Length];
        Array.Copy((Array) byteArray, (Array) this.DocumentByteArray, byteArray.Length);
        this.FileName = (string) null;
      }
    }

    public DocxToHtmlDocument(string fileName, MemoryStream memStream)
    {
      this.FileName = fileName;
      this.DocumentByteArray = new byte[memStream.Length];
      Array.Copy((Array) memStream.GetBuffer(), (Array) this.DocumentByteArray, memStream.Length);
    }

    public DocxToHtmlDocument(string fileName, MemoryStream memStream, bool convertToTransitional)
    {
      if (convertToTransitional)
      {
        this.ConvertToTransitional(fileName, memStream.ToArray());
      }
      else
      {
        this.FileName = fileName;
        this.DocumentByteArray = new byte[memStream.Length];
        Array.Copy((Array) memStream.GetBuffer(), (Array) this.DocumentByteArray, memStream.Length);
      }
    }

    public string GetName() => this.FileName == null ? "Unnamed Document" : new FileInfo(this.FileName).Name;

    public void SaveAs(string fileName) => File.WriteAllBytes(fileName, this.DocumentByteArray);

    public void Save()
    {
      if (this.FileName == null)
        throw new InvalidOperationException("Attempting to Save a document that has no file name.  Use SaveAs instead.");
      File.WriteAllBytes(this.FileName, this.DocumentByteArray);
    }

    public void WriteByteArray(Stream stream) => stream.Write(this.DocumentByteArray, 0, this.DocumentByteArray.Length);

    public Type GetDocumentType() => DocxToHtmlDocument.GetDocumentType(this.DocumentByteArray);

    private static Type GetDocumentType(byte[] bytes)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        memoryStream.Write(bytes, 0, bytes.Length);
        using (Package package = Package.Open((Stream) memoryStream, FileMode.Open))
        {
          PackageRelationship packageRelationship = ((IEnumerable<PackageRelationship>) package.GetRelationshipsByType("http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument")).FirstOrDefault<PackageRelationship>() ?? ((IEnumerable<PackageRelationship>) package.GetRelationshipsByType("http://purl.oclc.org/ooxml/officeDocument/relationships/officeDocument")).FirstOrDefault<PackageRelationship>();
          if (packageRelationship == null)
            return (Type) null;
          switch (package.GetPart(PackUriHelper.ResolvePartUri(packageRelationship.SourceUri, packageRelationship.TargetUri)).ContentType)
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
              return typeof (Package);
          }
        }
      }
    }

    public static void SavePartAs(OpenXmlPart part, string filePath)
    {
      Stream stream = part.GetStream(FileMode.Open, FileAccess.Read);
      byte[] numArray = new byte[stream.Length];
      stream.Read(numArray, 0, (int) stream.Length);
      File.WriteAllBytes(filePath, numArray);
    }
  }
}
