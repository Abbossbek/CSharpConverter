// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.RevisionAccepter
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;

namespace CSharpConverter.DocxToHtml.Element
{
  public class RevisionAccepter
  {
    public static WmlDocument AcceptRevisions(WmlDocument document)
    {
      using (OpenXmlMemoryStreamDocument memoryStreamDocument = new OpenXmlMemoryStreamDocument((DocxToHtmlDocument) document))
      {
        using (WordprocessingDocument wordprocessingDocument = memoryStreamDocument.GetWordprocessingDocument())
          RevisionAccepter.AcceptRevisions(wordprocessingDocument);
        return memoryStreamDocument.GetModifiedWmlDocument();
      }
    }

    public static void AcceptRevisions(WordprocessingDocument doc) => RevisionProcessor.AcceptRevisions(doc);

    public static bool PartHasTrackedRevisions(OpenXmlPart part) => RevisionProcessor.PartHasTrackedRevisions(part);

    public static bool HasTrackedRevisions(WmlDocument document) => RevisionProcessor.HasTrackedRevisions(document);

    public static bool HasTrackedRevisions(WordprocessingDocument doc) => RevisionProcessor.HasTrackedRevisions(doc);
  }
}
