// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.Xsi
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class Xsi
  {
    public static XNamespace xsi = (XNamespace) "http://www.w3.org/2001/XMLSchema-instance";
    public static XName schemaLocation = Xsi.xsi + nameof (schemaLocation);
    public static XName noNamespaceSchemaLocation = Xsi.xsi + nameof (noNamespaceSchemaLocation);
  }
}
