// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.PmlDocument
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System.IO;

namespace CSharpConverter.DocxToHtml.Element
{
  public class PmlDocument : DocxToHtmlDocument
  {
    public PmlDocument(DocxToHtmlDocument original)
      : base(original)
    {
      if (this.GetDocumentType() != typeof (PresentationDocument))
        throw new PowerToolsDocumentException("Not a Presentation document.");
    }

    public PmlDocument(DocxToHtmlDocument original, bool convertToTransitional)
      : base(original, convertToTransitional)
    {
      if (this.GetDocumentType() != typeof (PresentationDocument))
        throw new PowerToolsDocumentException("Not a Presentation document.");
    }

    public PmlDocument(string fileName)
      : base(fileName)
    {
      if (this.GetDocumentType() != typeof (PresentationDocument))
        throw new PowerToolsDocumentException("Not a Presentation document.");
    }

    public PmlDocument(string fileName, bool convertToTransitional)
      : base(fileName, convertToTransitional)
    {
      if (this.GetDocumentType() != typeof (PresentationDocument))
        throw new PowerToolsDocumentException("Not a Presentation document.");
    }

    public PmlDocument(string fileName, byte[] byteArray)
      : base(byteArray)
    {
      this.FileName = fileName;
      if (this.GetDocumentType() != typeof (PresentationDocument))
        throw new PowerToolsDocumentException("Not a Presentation document.");
    }

    public PmlDocument(string fileName, byte[] byteArray, bool convertToTransitional)
      : base(byteArray, convertToTransitional)
    {
      this.FileName = fileName;
      if (this.GetDocumentType() != typeof (PresentationDocument))
        throw new PowerToolsDocumentException("Not a Presentation document.");
    }

    public PmlDocument(string fileName, MemoryStream memStream)
      : base(fileName, memStream)
    {
    }

    public PmlDocument(string fileName, MemoryStream memStream, bool convertToTransitional)
      : base(fileName, memStream, convertToTransitional)
    {
    }
  }
}
