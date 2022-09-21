// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.PtOpenXmlExtensions
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class PtOpenXmlExtensions
  {
    public static XDocument GetXDocument(this OpenXmlPart part)
    {
      XDocument annotation = part != null ? part.Annotation<XDocument>() : throw new ArgumentNullException(nameof (part));
      if (annotation != null)
        return annotation;
      using (Stream stream = part.GetStream())
      {
        if (stream.Length == 0L)
        {
          annotation = new XDocument();
          annotation.Declaration = new XDeclaration("1.0", "UTF-8", "yes");
        }
        else
        {
          using (XmlReader reader = XmlReader.Create(stream))
            annotation = XDocument.Load(reader);
        }
      }
      part.AddAnnotation((object) annotation);
      return annotation;
    }

    public static XDocument GetXDocument(
      this OpenXmlPart part,
      out XmlNamespaceManager namespaceManager)
    {
      namespaceManager = part != null ? part.Annotation<XmlNamespaceManager>() : throw new ArgumentNullException(nameof (part));
      XDocument xDocument = part.Annotation<XDocument>();
      if (xDocument != null)
      {
        if (namespaceManager != null)
          return xDocument;
        namespaceManager = PtOpenXmlExtensions.GetManagerFromXDocument(xDocument);
        part.AddAnnotation((object) namespaceManager);
        return xDocument;
      }
      using (Stream stream = part.GetStream())
      {
        if (stream.Length == 0L)
        {
          XDocument annotation = new XDocument();
          annotation.Declaration = new XDeclaration("1.0", "UTF-8", "yes");
          part.AddAnnotation((object) annotation);
          return annotation;
        }
        using (XmlReader reader = XmlReader.Create(stream))
        {
          XDocument annotation = XDocument.Load(reader);
          namespaceManager = new XmlNamespaceManager(reader.NameTable);
          part.AddAnnotation((object) annotation);
          part.AddAnnotation((object) namespaceManager);
          return annotation;
        }
      }
    }

    public static void PutXDocument(this OpenXmlPart part)
    {
      XDocument xdocument = part != null ? part.GetXDocument() : throw new ArgumentNullException(nameof (part));
      if (xdocument == null)
        return;
      using (Stream stream = part.GetStream(FileMode.Create, FileAccess.Write))
      {
        using (XmlWriter writer = XmlWriter.Create(stream))
          xdocument.Save(writer);
      }
    }

    public static void PutXDocumentWithFormatting(this OpenXmlPart part)
    {
      XDocument xdocument = part != null ? part.GetXDocument() : throw new ArgumentNullException(nameof (part));
      if (xdocument == null)
        return;
      using (Stream stream = part.GetStream(FileMode.Create, FileAccess.Write))
      {
        using (XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings()
        {
          Indent = true,
          OmitXmlDeclaration = true,
          NewLineOnAttributes = true
        }))
          xdocument.Save(writer);
      }
    }

    public static void PutXDocument(this OpenXmlPart part, XDocument document)
    {
      if (part == null)
        throw new ArgumentNullException(nameof (part));
      if (document == null)
        throw new ArgumentNullException(nameof (document));
      using (Stream stream = part.GetStream(FileMode.Create, FileAccess.Write))
      {
        using (XmlWriter writer = XmlWriter.Create(stream))
          document.Save(writer);
      }
      part.RemoveAnnotations<XDocument>();
      part.AddAnnotation((object) document);
    }

    private static XmlNamespaceManager GetManagerFromXDocument(
      XDocument xDocument)
    {
      XmlReader reader = xDocument.CreateReader();
      XDocument xdocument = XDocument.Load(reader);
      xDocument.Elements().FirstOrDefault<XElement>().ReplaceWith((object) xdocument.Root);
      return new XmlNamespaceManager(reader.NameTable);
    }

    public static IEnumerable<XElement> LogicalChildrenContent(
      this XElement element)
    {
      if (element.Name == W.document)
        return element.Descendants(W.body).Take<XElement>(1);
      if (element.Name == W.body || element.Name == W.tc || element.Name == W.txbxContent)
        return element.DescendantsTrimmed((Func<XElement, bool>) (e => e.Name == W.tbl || e.Name == W.p)).Where<XElement>((Func<XElement, bool>) (e => e.Name == W.p || e.Name == W.tbl));
      if (element.Name == W.tbl)
        return element.DescendantsTrimmed(W.tr).Where<XElement>((Func<XElement, bool>) (e => e.Name == W.tr));
      if (element.Name == W.tr)
        return element.DescendantsTrimmed(W.tc).Where<XElement>((Func<XElement, bool>) (e => e.Name == W.tc));
      if (element.Name == W.p)
        return element.DescendantsTrimmed((Func<XElement, bool>) (e => e.Name == W.r || e.Name == W.pict || e.Name == W.drawing)).Where<XElement>((Func<XElement, bool>) (e => e.Name == W.r || e.Name == W.pict || e.Name == W.drawing));
      if (element.Name == W.r)
        return element.DescendantsTrimmed((Func<XElement, bool>) (e => ((IEnumerable<XName>) W.SubRunLevelContent).Contains<XName>(e.Name))).Where<XElement>((Func<XElement, bool>) (e => ((IEnumerable<XName>) W.SubRunLevelContent).Contains<XName>(e.Name)));
      if (element.Name == MC.AlternateContent)
        return element.DescendantsTrimmed((Func<XElement, bool>) (e => e.Name == W.pict || e.Name == W.drawing || e.Name == MC.Fallback)).Where<XElement>((Func<XElement, bool>) (e => e.Name == W.pict || e.Name == W.drawing));
      return element.Name == W.pict || element.Name == W.drawing ? element.DescendantsTrimmed(W.txbxContent).Where<XElement>((Func<XElement, bool>) (e => e.Name == W.txbxContent)) : XElement.EmptySequence;
    }

    public static IEnumerable<XElement> LogicalChildrenContent(
      this IEnumerable<XElement> source)
    {
      foreach (XElement element in source)
      {
        foreach (XElement xelement in element.LogicalChildrenContent())
          yield return xelement;
      }
    }

    public static IEnumerable<XElement> LogicalChildrenContent(
      this XElement element,
      XName name)
    {
      return element.LogicalChildrenContent().Where<XElement>((Func<XElement, bool>) (e => e.Name == name));
    }

    public static IEnumerable<XElement> LogicalChildrenContent(
      this IEnumerable<XElement> source,
      XName name)
    {
      foreach (XElement element in source)
      {
        foreach (XElement xelement in element.LogicalChildrenContent(name))
          yield return xelement;
      }
    }

    public static IEnumerable<OpenXmlPart> ContentParts(
      this WordprocessingDocument doc)
    {
      yield return (OpenXmlPart) doc.MainDocumentPart;
      foreach (OpenXmlPart headerPart in doc.MainDocumentPart.HeaderParts)
        yield return headerPart;
      foreach (OpenXmlPart footerPart in doc.MainDocumentPart.FooterParts)
        yield return footerPart;
      if (doc.MainDocumentPart.FootnotesPart != null)
        yield return (OpenXmlPart) doc.MainDocumentPart.FootnotesPart;
      if (doc.MainDocumentPart.EndnotesPart != null)
        yield return (OpenXmlPart) doc.MainDocumentPart.EndnotesPart;
    }

    /// <summary>
    /// Creates a complete list of all parts contained in the <see cref="T:DocumentFormat.OpenXml.Packaging.OpenXmlPartContainer" />.
    /// </summary>
    /// <param name="container">
    /// A <see cref="T:DocumentFormat.OpenXml.Packaging.WordprocessingDocument" />, <see cref="T:DocumentFormat.OpenXml.Packaging.SpreadsheetDocument" />, or
    /// <see cref="T:DocumentFormat.OpenXml.Packaging.PresentationDocument" />.
    /// </param>
    /// <returns>list of <see cref="T:DocumentFormat.OpenXml.Packaging.OpenXmlPart" />s contained in the <see cref="T:DocumentFormat.OpenXml.Packaging.OpenXmlPartContainer" />.</returns>
    public static List<OpenXmlPart> GetAllParts(this OpenXmlPartContainer container)
    {
      HashSet<OpenXmlPart> openXmlPartSet = new HashSet<OpenXmlPart>();
      foreach (IdPartPair part in container.Parts)
        PtOpenXmlExtensions.AddPart(openXmlPartSet, part.OpenXmlPart);
      return openXmlPartSet.OrderBy<OpenXmlPart, string>((Func<OpenXmlPart, string>) (p => p.ContentType)).ThenBy<OpenXmlPart, string>((Func<OpenXmlPart, string>) (p => p.Uri.ToString())).ToList<OpenXmlPart>();
    }

    private static void AddPart(HashSet<OpenXmlPart> partList, OpenXmlPart part)
    {
      if (partList.Contains(part))
        return;
      partList.Add(part);
      foreach (IdPartPair part1 in part.Parts)
        PtOpenXmlExtensions.AddPart(partList, part1.OpenXmlPart);
    }
  }
}
