// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.Normalizer
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;

namespace CSharpConverter.DocxToHtml.Element
{
  public class Normalizer
  {
    public static XDocument Normalize(XDocument source, XmlSchemaSet schema)
    {
      bool havePSVI = false;
      if (schema != null)
      {
        source.Validate(schema, (ValidationEventHandler) null, true);
        havePSVI = true;
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
              return (XNode) Normalizer.NormalizeElement(element2, havePSVI);
            default:
              return n;
          }
        }))
      });
    }

    public static bool DeepEqualsWithNormalization(
      XDocument doc1,
      XDocument doc2,
      XmlSchemaSet schemaSet)
    {
      return XNode.DeepEquals((XNode) Normalizer.Normalize(doc1, schemaSet), (XNode) Normalizer.Normalize(doc2, schemaSet));
    }

    private static IEnumerable<XAttribute> NormalizeAttributes(
      XElement element,
      bool havePSVI)
    {
      return element.Attributes().Where<XAttribute>((Func<XAttribute, bool>) (a => !a.IsNamespaceDeclaration && a.Name != Xsi.schemaLocation && a.Name != Xsi.noNamespaceSchemaLocation)).OrderBy<XAttribute, string>((Func<XAttribute, string>) (a => a.Name.NamespaceName)).ThenBy<XAttribute, string>((Func<XAttribute, string>) (a => a.Name.LocalName)).Select<XAttribute, XAttribute>((Func<XAttribute, XAttribute>) (a =>
      {
        if (havePSVI)
        {
          switch (a.GetSchemaInfo().SchemaType.TypeCode)
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
        return a;
      }));
    }

    private static XNode NormalizeNode(XNode node, bool havePSVI)
    {
      switch (node)
      {
        case XComment _:
        case XProcessingInstruction _:
          return (XNode) null;
        case XElement element:
          return (XNode) Normalizer.NormalizeElement(element, havePSVI);
        default:
          return node;
      }
    }

    private static XElement NormalizeElement(XElement element, bool havePSVI)
    {
      if (havePSVI)
      {
        switch (element.GetSchemaInfo().SchemaType.TypeCode)
        {
          case XmlTypeCode.Boolean:
            return new XElement(element.Name, new object[2]
            {
              (object) Normalizer.NormalizeAttributes(element, havePSVI),
              (object) (bool) element
            });
          case XmlTypeCode.Decimal:
            return new XElement(element.Name, new object[2]
            {
              (object) Normalizer.NormalizeAttributes(element, havePSVI),
              (object) (Decimal) element
            });
          case XmlTypeCode.Float:
            return new XElement(element.Name, new object[2]
            {
              (object) Normalizer.NormalizeAttributes(element, havePSVI),
              (object) (float) element
            });
          case XmlTypeCode.Double:
            return new XElement(element.Name, new object[2]
            {
              (object) Normalizer.NormalizeAttributes(element, havePSVI),
              (object) (double) element
            });
          case XmlTypeCode.DateTime:
            return new XElement(element.Name, new object[2]
            {
              (object) Normalizer.NormalizeAttributes(element, havePSVI),
              (object) (DateTime) element
            });
          case XmlTypeCode.HexBinary:
          case XmlTypeCode.Language:
            return new XElement(element.Name, new object[2]
            {
              (object) Normalizer.NormalizeAttributes(element, havePSVI),
              (object) ((string) element).ToLower()
            });
          default:
            return new XElement(element.Name, new object[2]
            {
              (object) Normalizer.NormalizeAttributes(element, havePSVI),
              (object) element.Nodes().Select<XNode, XNode>((Func<XNode, XNode>) (n => Normalizer.NormalizeNode(n, havePSVI)))
            });
        }
      }
      else
        return new XElement(element.Name, new object[2]
        {
          (object) Normalizer.NormalizeAttributes(element, havePSVI),
          (object) element.Nodes().Select<XNode, XNode>((Func<XNode, XNode>) (n => Normalizer.NormalizeNode(n, havePSVI)))
        });
    }
  }
}
