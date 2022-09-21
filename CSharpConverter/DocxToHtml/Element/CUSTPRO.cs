// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.CUSTPRO
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class CUSTPRO
  {
    public static readonly XNamespace custpro = (XNamespace) "http://schemas.openxmlformats.org/officeDocument/2006/custom-properties";
    public static readonly XName Properties = CUSTPRO.custpro + nameof (Properties);
    public static readonly XName property = CUSTPRO.custpro + nameof (property);
  }
}
