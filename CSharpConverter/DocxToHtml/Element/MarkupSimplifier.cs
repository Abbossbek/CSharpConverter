// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.MarkupSimplifier
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class MarkupSimplifier
  {
    public static WmlDocument SimplifyMarkup(
      WmlDocument doc,
      SimplifyMarkupSettings settings)
    {
      using (OpenXmlMemoryStreamDocument memoryStreamDocument = new OpenXmlMemoryStreamDocument((DocxToHtmlDocument) doc))
      {
        using (WordprocessingDocument wordprocessingDocument = memoryStreamDocument.GetWordprocessingDocument())
          MarkupSimplifier.SimplifyMarkup(wordprocessingDocument, settings);
        return memoryStreamDocument.GetModifiedWmlDocument();
      }
    }

    public static void SimplifyMarkup(WordprocessingDocument doc, SimplifyMarkupSettings settings)
    {
      if (settings.RemoveMarkupForDocumentComparison)
      {
        settings.RemoveRsidInfo = true;
        MarkupSimplifier.RemoveElementsForDocumentComparison(doc);
      }
      if (settings.RemoveRsidInfo)
        MarkupSimplifier.RemoveRsidInfoInSettings(doc);
      if (settings.AcceptRevisions)
        RevisionAccepter.AcceptRevisions(doc);
      foreach (OpenXmlPart contentPart in doc.ContentParts())
        MarkupSimplifier.SimplifyMarkupForPart(contentPart, settings);
      if (doc.MainDocumentPart.StyleDefinitionsPart != null)
        MarkupSimplifier.SimplifyMarkupForPart((OpenXmlPart) doc.MainDocumentPart.StyleDefinitionsPart, settings);
      if (doc.MainDocumentPart.StylesWithEffectsPart != null)
        MarkupSimplifier.SimplifyMarkupForPart((OpenXmlPart) doc.MainDocumentPart.StylesWithEffectsPart, settings);
      if (!settings.RemoveComments)
        return;
      WordprocessingCommentsPart wordprocessingCommentsPart = doc.MainDocumentPart.WordprocessingCommentsPart;
      if (wordprocessingCommentsPart != null)
        doc.MainDocumentPart.DeletePart((OpenXmlPart) wordprocessingCommentsPart);
      WordprocessingCommentsExPart wordprocessingCommentsExPart = doc.MainDocumentPart.WordprocessingCommentsExPart;
      if (wordprocessingCommentsExPart == null)
        return;
      doc.MainDocumentPart.DeletePart((OpenXmlPart) wordprocessingCommentsExPart);
    }

    private static void RemoveRsidInfoInSettings(WordprocessingDocument doc)
    {
      DocumentSettingsPart documentSettingsPart = doc.MainDocumentPart.DocumentSettingsPart;
      if (documentSettingsPart == null)
        return;
      documentSettingsPart.GetXDocument().Descendants(W.rsids).Remove<XElement>();
      documentSettingsPart.PutXDocument();
    }

    private static void RemoveElementsForDocumentComparison(WordprocessingDocument doc)
    {
      OpenXmlPart filePropertiesPart1 = (OpenXmlPart) doc.ExtendedFilePropertiesPart;
      if (filePropertiesPart1 != null)
      {
        filePropertiesPart1.GetXDocument().Descendants(EP.TotalTime).Remove<XElement>();
        filePropertiesPart1.PutXDocument();
      }
      OpenXmlPart filePropertiesPart2 = (OpenXmlPart) doc.CoreFilePropertiesPart;
      if (filePropertiesPart2 != null)
      {
        XDocument xdocument = filePropertiesPart2.GetXDocument();
        xdocument.Descendants(CP.revision).Remove<XElement>();
        xdocument.Descendants(DCTERMS.created).Remove<XElement>();
        xdocument.Descendants(DCTERMS.modified).Remove<XElement>();
        filePropertiesPart2.PutXDocument();
      }
      XDocument xdocument1 = doc.MainDocumentPart.GetXDocument();
      List<XElement> list = xdocument1.Descendants(W.bookmarkStart).Where<XElement>((Func<XElement, bool>) (b => (string) b.Attribute(W.name) == "_GoBack")).ToList<XElement>();
      foreach (XElement xelement in list)
      {
        XElement item = xelement;
        xdocument1.Descendants(W.bookmarkEnd).Where<XElement>((Func<XElement, bool>) (be => (int) be.Attribute(W.id) == (int) item.Attribute(W.id))).Remove<XElement>();
      }
      list.Remove<XElement>();
      doc.MainDocumentPart.PutXDocument();
    }

    public static XElement MergeAdjacentSuperfluousRuns(XElement element) => (XElement) MarkupSimplifier.MergeAdjacentRunsTransform((XNode) element);

    public static XElement TransformElementToSingleCharacterRuns(XElement element) => (XElement) MarkupSimplifier.SingleCharacterRunTransform((XNode) element);

    public static void TransformPartToSingleCharacterRuns(OpenXmlPart part)
    {
      XDocument xdocument = part.GetXDocument();
      XElement content = (XElement) MarkupSimplifier.SingleCharacterRunTransform((XNode) MarkupSimplifier.RemoveRsidTransform((XNode) xdocument.Root));
      xdocument.Elements().First<XElement>().ReplaceWith((object) content);
      part.PutXDocument();
    }

    public static void TransformToSingleCharacterRuns(WordprocessingDocument doc)
    {
      if (RevisionAccepter.HasTrackedRevisions(doc))
        throw new DocxToHtmlException("Transforming a document to single character runs is not supported for a document with tracked revisions.");
      foreach (OpenXmlPart contentPart in doc.ContentParts())
        MarkupSimplifier.TransformPartToSingleCharacterRuns(contentPart);
    }

    private static object RemoveCustomXmlAndContentControlsTransform(
      XNode node,
      SimplifyMarkupSettings simplifyMarkupSettings)
    {
      if (!(node is XElement xelement))
        return (object) node;
      if (simplifyMarkupSettings.RemoveSmartTags && xelement.Name == W.smartTag)
        return (object) xelement.Elements().Select<XElement, object>((Func<XElement, object>) (e => MarkupSimplifier.RemoveCustomXmlAndContentControlsTransform((XNode) e, simplifyMarkupSettings)));
      if (simplifyMarkupSettings.RemoveContentControls && xelement.Name == W.sdt)
        return (object) xelement.Elements(W.sdtContent).Elements<XElement>().Select<XElement, object>((Func<XElement, object>) (e => MarkupSimplifier.RemoveCustomXmlAndContentControlsTransform((XNode) e, simplifyMarkupSettings)));
      return (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Nodes().Select<XNode, object>((Func<XNode, object>) (n => MarkupSimplifier.RemoveCustomXmlAndContentControlsTransform(n, simplifyMarkupSettings)))
      });
    }

    private static object RemoveRsidTransform(XNode node)
    {
      if (!(node is XElement xelement))
        return (object) node;
      if (xelement.Name == W.rsid)
        return (object) null;
      return (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes().Where<XAttribute>((Func<XAttribute, bool>) (a => a.Name != W.rsid && a.Name != W.rsidDel && a.Name != W.rsidP && a.Name != W.rsidR && a.Name != W.rsidRDefault && a.Name != W.rsidRPr && a.Name != W.rsidSect && a.Name != W.rsidTr)),
        (object) xelement.Nodes().Select<XNode, object>((Func<XNode, object>) (n => MarkupSimplifier.RemoveRsidTransform(n)))
      });
    }

    private static object MergeAdjacentRunsTransform(XNode node)
    {
      if (!(node is XElement runContainer))
        return (object) node;
      if (runContainer.Name == W.p)
        return (object) WordprocessingMLUtil.CoalesceAdjacentRunsWithIdenticalFormatting(runContainer);
      return (object) new XElement(runContainer.Name, new object[2]
      {
        (object) runContainer.Attributes(),
        (object) runContainer.Nodes().Select<XNode, object>((Func<XNode, object>) (n => MarkupSimplifier.MergeAdjacentRunsTransform(n)))
      });
    }

    private static object RemoveEmptyRunsAndRunPropertiesTransform(XNode node)
    {
      if (!(node is XElement xelement))
        return (object) node;
      if ((xelement.Name == W.r || xelement.Name == W.rPr || xelement.Name == W.pPr) && !xelement.Elements().Any<XElement>())
        return (object) null;
      return (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Nodes().Select<XNode, object>((Func<XNode, object>) (n => MarkupSimplifier.RemoveEmptyRunsAndRunPropertiesTransform(n)))
      });
    }

    private static object MergeAdjacentInstrText(XNode node)
    {
      if (!(node is XElement xelement))
        return (object) node;
      if (xelement.Name == W.r && xelement.Elements(W.instrText).Any<XElement>())
      {
        IEnumerable<IGrouping<bool, XElement>> source = xelement.Elements().GroupAdjacent<XElement, bool>((Func<XElement, bool>) (e => e.Name == W.instrText));
        return (object) new XElement(W.r, (object) source.Select<IGrouping<bool, XElement>, object>((Func<IGrouping<bool, XElement>, object>) (g =>
        {
          if (!g.Key)
            return (object) g;
          string str = g.Select<XElement, string>((Func<XElement, string>) (i => (string) i)).StringConcatenate();
          if (string.IsNullOrEmpty(str))
            return (object) new XElement(W.instrText);
          return (object) new XElement(W.instrText, new object[2]
          {
            str[0] == ' ' || str[str.Length - 1] == ' ' ? (object) new XAttribute(XNamespace.Xml + "space", (object) "preserve") : (object) (XAttribute) null,
            (object) str
          });
        })));
      }
      return (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Nodes().Select<XNode, object>((Func<XNode, object>) (n => MarkupSimplifier.MergeAdjacentInstrText(n)))
      });
    }

    private static object SimplifyMarkupTransform(
      XNode node,
      SimplifyMarkupSettings settings,
      MarkupSimplifier.SimplifyMarkupParameters parameters)
    {
      if (!(node is XElement xelement))
        return (object) node;
      if (settings.RemovePermissions && (xelement.Name == W.permEnd || xelement.Name == W.permStart))
        return (object) null;
      if (settings.RemoveProof && (xelement.Name == W.proofErr || xelement.Name == W.noProof))
        return (object) null;
      if (settings.RemoveSoftHyphens && xelement.Name == W.softHyphen)
        return (object) null;
      if (settings.RemoveLastRenderedPageBreak && xelement.Name == W.lastRenderedPageBreak)
        return (object) null;
      if (settings.RemoveBookmarks && (xelement.Name == W.bookmarkStart || xelement.Name == W.bookmarkEnd))
        return (object) null;
      if (settings.RemoveGoBackBookmark)
      {
        if (xelement.Name == W.bookmarkStart)
        {
          int num = (int) xelement.Attribute(W.id);
          int? goBackId = parameters.GoBackId;
          int valueOrDefault = goBackId.GetValueOrDefault();
          if ((num == valueOrDefault ? (goBackId.HasValue ? 1 : 0) : 0) != 0)
            goto label_17;
        }
        if (xelement.Name == W.bookmarkEnd)
        {
          int num = (int) xelement.Attribute(W.id);
          int? goBackId = parameters.GoBackId;
          int valueOrDefault = goBackId.GetValueOrDefault();
          if ((num == valueOrDefault ? (goBackId.HasValue ? 1 : 0) : 0) == 0)
            goto label_18;
        }
        else
          goto label_18;
label_17:
        return (object) null;
      }
label_18:
      if (settings.RemoveWebHidden && xelement.Name == W.webHidden)
        return (object) null;
      if (settings.ReplaceTabsWithSpaces && xelement.Name == W.tab && xelement.Parent != null && xelement.Parent.Name == W.r)
        return (object) new XElement(W.t, new object[2]
        {
          (object) new XAttribute(XNamespace.Xml + "space", (object) "preserve"),
          (object) " "
        });
      if (settings.RemoveComments && (xelement.Name == W.commentRangeStart || xelement.Name == W.commentRangeEnd || xelement.Name == W.commentReference || xelement.Name == W.annotationRef))
        return (object) null;
      if (settings.RemoveComments && xelement.Name == W.rStyle && xelement.Attribute(W.val).Value == "CommentReference")
        return (object) null;
      if (settings.RemoveEndAndFootNotes && (xelement.Name == W.endnoteReference || xelement.Name == W.footnoteReference))
        return (object) null;
      if (settings.RemoveFieldCodes)
      {
        if (xelement.Name == W.fldSimple)
          return (object) xelement.Elements().Select<XElement, object>((Func<XElement, object>) (e => MarkupSimplifier.SimplifyMarkupTransform((XNode) e, settings, parameters)));
        if (xelement.Name == W.fldData || xelement.Name == W.fldChar || xelement.Name == W.instrText)
          return (object) null;
      }
      if (settings.RemoveHyperlinks && xelement.Name == W.hyperlink)
        return (object) xelement.Elements();
      return (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Nodes().Select<XNode, object>((Func<XNode, object>) (n => MarkupSimplifier.SimplifyMarkupTransform(n, settings, parameters)))
      });
    }

    private static XDocument Normalize(XDocument source, XmlSchemaSet schema)
    {
      bool havePsvi = false;
      if (schema != null)
      {
        source.Validate(schema, (ValidationEventHandler) null, true);
        havePsvi = true;
      }
      return new XDocument(source.Declaration, new object[1]
      {
        (object) source.Nodes().Select<XNode, XNode>((Func<XNode, XNode>) (n =>
        {
          switch (n)
          {
            case XComment _:
            case XProcessingInstruction _:
            case XText _:
              return (XNode) null;
            case XElement element2:
              return (XNode) MarkupSimplifier.NormalizeElement(element2, havePsvi);
            default:
              return n;
          }
        }))
      });
    }

    private static IEnumerable<XAttribute> NormalizeAttributes(
      XElement element,
      bool havePsvi)
    {
      return element.Attributes().Where<XAttribute>((Func<XAttribute, bool>) (a => !a.IsNamespaceDeclaration && a.Name != MarkupSimplifier.Xsi.schemaLocation && a.Name != MarkupSimplifier.Xsi.noNamespaceSchemaLocation)).OrderBy<XAttribute, string>((Func<XAttribute, string>) (a => a.Name.NamespaceName)).ThenBy<XAttribute, string>((Func<XAttribute, string>) (a => a.Name.LocalName)).Select<XAttribute, XAttribute>((Func<XAttribute, XAttribute>) (a =>
      {
        if (havePsvi)
        {
          XmlTypeCode? typeCode = a.GetSchemaInfo()?.SchemaType?.TypeCode;
          if (typeCode.HasValue)
          {
            switch (typeCode.GetValueOrDefault())
            {
              case XmlTypeCode.Boolean:
                return new XAttribute(a.Name, (object) (bool) a);
              case XmlTypeCode.Decimal:
                return new XAttribute(a.Name, (object) (Decimal) a);
              case XmlTypeCode.Float:
                return new XAttribute(a.Name, (object) (float) a);
              case XmlTypeCode.Double:
                return new XAttribute(a.Name, (object) (double) a);
              case XmlTypeCode.DateTime:
                return new XAttribute(a.Name, (object) (DateTime) a);
              case XmlTypeCode.HexBinary:
              case XmlTypeCode.Language:
                return new XAttribute(a.Name, (object) ((string) a).ToLower());
            }
          }
        }
        return a;
      }));
    }

    private static XNode NormalizeNode(XNode node, bool havePsvi)
    {
      switch (node)
      {
        case XComment _:
        case XProcessingInstruction _:
          return (XNode) null;
        case XElement element:
          return (XNode) MarkupSimplifier.NormalizeElement(element, havePsvi);
        default:
          return node;
      }
    }

    private static XElement NormalizeElement(XElement element, bool havePsvi)
    {
      if (havePsvi)
      {
        XmlTypeCode? typeCode = element.GetSchemaInfo()?.SchemaType?.TypeCode;
        if (typeCode.HasValue)
        {
          switch (typeCode.GetValueOrDefault())
          {
            case XmlTypeCode.Boolean:
              return new XElement(element.Name, new object[2]
              {
                (object) MarkupSimplifier.NormalizeAttributes(element, true),
                (object) (bool) element
              });
            case XmlTypeCode.Decimal:
              return new XElement(element.Name, new object[2]
              {
                (object) MarkupSimplifier.NormalizeAttributes(element, true),
                (object) (Decimal) element
              });
            case XmlTypeCode.Float:
              return new XElement(element.Name, new object[2]
              {
                (object) MarkupSimplifier.NormalizeAttributes(element, true),
                (object) (float) element
              });
            case XmlTypeCode.Double:
              return new XElement(element.Name, new object[2]
              {
                (object) MarkupSimplifier.NormalizeAttributes(element, true),
                (object) (double) element
              });
            case XmlTypeCode.DateTime:
              return new XElement(element.Name, new object[2]
              {
                (object) MarkupSimplifier.NormalizeAttributes(element, true),
                (object) (DateTime) element
              });
            case XmlTypeCode.HexBinary:
            case XmlTypeCode.Language:
              return new XElement(element.Name, new object[2]
              {
                (object) MarkupSimplifier.NormalizeAttributes(element, true),
                (object) ((string) element).ToLower()
              });
          }
        }
        return new XElement(element.Name, new object[2]
        {
          (object) MarkupSimplifier.NormalizeAttributes(element, true),
          (object) element.Nodes().Select<XNode, XNode>((Func<XNode, XNode>) (n => MarkupSimplifier.NormalizeNode(n, true)))
        });
      }
      return new XElement(element.Name, new object[2]
      {
        (object) MarkupSimplifier.NormalizeAttributes(element, false),
        (object) element.Nodes().Select<XNode, XNode>((Func<XNode, XNode>) (n => MarkupSimplifier.NormalizeNode(n, false)))
      });
    }

    private static void SimplifyMarkupForPart(OpenXmlPart part, SimplifyMarkupSettings settings)
    {
      MarkupSimplifier.SimplifyMarkupParameters parameters = new MarkupSimplifier.SimplifyMarkupParameters();
      if (part.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml")
      {
        WordprocessingDocument openXmlPackage = (WordprocessingDocument) part.OpenXmlPackage;
        if (settings.RemoveGoBackBookmark)
        {
          XElement xelement = openXmlPackage.MainDocumentPart.GetXDocument().Descendants(W.bookmarkStart).FirstOrDefault<XElement>((Func<XElement, bool>) (bm => (string) bm.Attribute(W.name) == "_GoBack"));
          if (xelement != null)
            parameters.GoBackId = new int?((int) xelement.Attribute(W.id));
        }
      }
      XElement xelement1 = part.GetXDocument().Root;
      if (settings.RemoveContentControls || settings.RemoveSmartTags)
        xelement1 = (XElement) MarkupSimplifier.RemoveCustomXmlAndContentControlsTransform((XNode) xelement1, settings);
      if (settings.RemoveRsidInfo)
        xelement1 = (XElement) MarkupSimplifier.RemoveRsidTransform((XNode) xelement1);
      XDocument xdocument = new XDocument(new object[1]
      {
        (object) xelement1
      });
      while (true)
      {
        if (settings.RemoveComments || settings.RemoveEndAndFootNotes || settings.ReplaceTabsWithSpaces || settings.RemoveFieldCodes || settings.RemovePermissions || settings.RemoveProof || settings.RemoveBookmarks || settings.RemoveWebHidden || settings.RemoveGoBackBookmark || settings.RemoveHyperlinks)
          xelement1 = (XElement) MarkupSimplifier.SimplifyMarkupTransform((XNode) xelement1, settings, parameters);
        xelement1 = (XElement) MarkupSimplifier.SeparateRunChildrenIntoSeparateRuns((XNode) MarkupSimplifier.MergeAdjacentInstrText((XNode) MarkupSimplifier.MergeAdjacentRunsTransform((XNode) MarkupSimplifier.RemoveEmptyRunsAndRunPropertiesTransform((XNode) xelement1))));
        if (!XNode.DeepEquals((XNode) xdocument.Root, (XNode) xelement1))
          xdocument = new XDocument(new object[1]
          {
            (object) xelement1
          });
        else
          break;
      }
      if (settings.NormalizeXml)
      {
        XAttribute[] xattributeArray = new XAttribute[18]
        {
          new XAttribute(XNamespace.Xmlns + "wpc", (object) WPC.wpc),
          new XAttribute(XNamespace.Xmlns + "mc", (object) MC.mc),
          new XAttribute(XNamespace.Xmlns + "o", (object) O.o),
          new XAttribute(XNamespace.Xmlns + "r", (object) R.r),
          new XAttribute(XNamespace.Xmlns + "m", (object) M.m),
          new XAttribute(XNamespace.Xmlns + "v", (object) VML.vml),
          new XAttribute(XNamespace.Xmlns + "wp14", (object) WP14.wp14),
          new XAttribute(XNamespace.Xmlns + "wp", (object) WP.wp),
          new XAttribute(XNamespace.Xmlns + "w10", (object) W10.w10),
          new XAttribute(XNamespace.Xmlns + "w", (object) W.w),
          new XAttribute(XNamespace.Xmlns + "w14", (object) W14.w14),
          new XAttribute(XNamespace.Xmlns + "w15", (object) W15.w15),
          new XAttribute(XNamespace.Xmlns + "w16se", (object) W16SE.w16se),
          new XAttribute(XNamespace.Xmlns + "wpg", (object) WPG.wpg),
          new XAttribute(XNamespace.Xmlns + "wpi", (object) WPI.wpi),
          new XAttribute(XNamespace.Xmlns + "wne", (object) WNE.wne),
          new XAttribute(XNamespace.Xmlns + "wps", (object) WPS.wps),
          new XAttribute(MC.Ignorable, (object) "w14 wp14 w15 w16se")
        };
        XDocument document = MarkupSimplifier.Normalize(new XDocument(new object[1]
        {
          (object) xelement1
        }), (XmlSchemaSet) null);
        XElement root = document.Root;
        if (root != null)
        {
          foreach (XAttribute content in xattributeArray)
          {
            if (root.Attribute(content.Name) == null)
              root.Add((object) content);
          }
        }
        part.PutXDocument(document);
      }
      else
        part.PutXDocument(new XDocument(new object[1]
        {
          (object) xelement1
        }));
    }

    private static object SeparateRunChildrenIntoSeparateRuns(XNode node)
    {
      if (!(node is XElement xelement))
        return (object) node;
      if (xelement.Name == W.r)
      {
        IEnumerable<XElement> source = xelement.Elements().Where<XElement>((Func<XElement, bool>) (e => e.Name != W.rPr));
        XElement rPr = xelement.Element(W.rPr);
        Func<XElement, XElement> selector = (Func<XElement, XElement>) (rc => new XElement(W.r, new object[2]
        {
          (object) rPr,
          (object) rc
        }));
        return (object) source.Select<XElement, XElement>(selector);
      }
      return (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Nodes().Select<XNode, object>((Func<XNode, object>) (n => MarkupSimplifier.SeparateRunChildrenIntoSeparateRuns(n)))
      });
    }

    private static object SingleCharacterRunTransform(XNode node)
    {
      XElement element = node as XElement;
      if (element == null)
        return (object) node;
      if (element.Name == W.r)
        return (object) element.Elements().Where<XElement>((Func<XElement, bool>) (e => e.Name != W.rPr)).GroupAdjacent<XElement, bool>((Func<XElement, bool>) (sr => sr.Name == W.t)).Select<IGrouping<bool, XElement>, IEnumerable<XElement>>((Func<IGrouping<bool, XElement>, IEnumerable<XElement>>) (g => g.Key ? g.Select<XElement, string>((Func<XElement, string>) (t => (string) t)).StringConcatenate().Select<char, XElement>((Func<char, XElement>) (c => new XElement(W.r, new object[2]
        {
          (object) element.Elements(W.rPr),
          (object) new XElement(W.t, new object[2]
          {
            c == ' ' ? (object) new XAttribute(XNamespace.Xml + "space", (object) "preserve") : (object) (XAttribute) null,
            (object) c
          })
        }))) : g.Select<XElement, XElement>((Func<XElement, XElement>) (sr => new XElement(W.r, new object[2]
        {
          (object) element.Elements(W.rPr),
          (object) new XElement(sr.Name, new object[2]
          {
            (object) sr.Attributes(),
            (object) sr.Nodes().Select<XNode, object>((Func<XNode, object>) (n => MarkupSimplifier.SingleCharacterRunTransform(n)))
          })
        })))));
      return (object) new XElement(element.Name, new object[2]
      {
        (object) element.Attributes(),
        (object) element.Nodes().Select<XNode, object>((Func<XNode, object>) (n => MarkupSimplifier.SingleCharacterRunTransform(n)))
      });
    }

    private static class Xsi
    {
      private static readonly XNamespace xsi = (XNamespace) "http://www.w3.org/2001/XMLSchema-instance";
      public static readonly XName schemaLocation = MarkupSimplifier.Xsi.xsi + nameof (schemaLocation);
      public static readonly XName noNamespaceSchemaLocation = MarkupSimplifier.Xsi.xsi + nameof (noNamespaceSchemaLocation);
    }

    public class InternalException : Exception
    {
      public InternalException(string message)
        : base(message)
      {
      }
    }

    public class InvalidSettingsException : Exception
    {
      public InvalidSettingsException(string message)
        : base(message)
      {
      }
    }

    private class SimplifyMarkupParameters
    {
      public int? GoBackId { get; set; }
    }
  }
}
