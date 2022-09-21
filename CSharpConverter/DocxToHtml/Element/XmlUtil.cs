// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.XmlUtil
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class XmlUtil
  {
    public static XAttribute GetXmlSpaceAttribute(string value) => value.Length <= 0 || value[0] != ' ' && value[value.Length - 1] != ' ' ? (XAttribute) null : new XAttribute(XNamespace.Xml + "space", (object) "preserve");

    public static XAttribute GetXmlSpaceAttribute(char value) => value != ' ' ? (XAttribute) null : new XAttribute(XNamespace.Xml + "space", (object) "preserve");
  }
}
