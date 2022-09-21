// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ContentPartRelTypeIdTuple
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;

namespace CSharpConverter.DocxToHtml.Element
{
  internal class ContentPartRelTypeIdTuple
  {
    public OpenXmlPart ContentPart { get; set; }

    public string RelationshipType { get; set; }

    public string RelationshipId { get; set; }
  }
}
