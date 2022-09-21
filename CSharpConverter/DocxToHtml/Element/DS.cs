// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.DS
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class DS
  {
    public static readonly XNamespace ds = (XNamespace) "http://schemas.openxmlformats.org/officeDocument/2006/customXml";
    public static readonly XName datastoreItem = DS.ds + nameof (datastoreItem);
    public static readonly XName itemID = DS.ds + nameof (itemID);
    public static readonly XName schemaRef = DS.ds + nameof (schemaRef);
    public static readonly XName schemaRefs = DS.ds + nameof (schemaRefs);
    public static readonly XName uri = DS.ds + nameof (uri);
  }
}
