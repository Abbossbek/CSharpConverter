// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.FlatOpc
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class FlatOpc
  {
    private static XElement GetContentsAsXml(PackagePart part)
    {
      XNamespace xnamespace = (XNamespace) "http://schemas.microsoft.com/office/2006/xmlPackage";
      if (part.ContentType.EndsWith("xml"))
      {
        using (Stream stream = part.GetStream())
        {
          using (StreamReader input = new StreamReader(stream))
          {
            using (XmlReader reader = XmlReader.Create((TextReader) input))
              return new XElement(xnamespace + nameof (part), new object[3]
              {
                (object) new XAttribute(xnamespace + "name", (object) part.Uri),
                (object) new XAttribute(xnamespace + "contentType", (object) part.ContentType),
                (object) new XElement(xnamespace + "xmlData", (object) XElement.Load(reader))
              });
          }
        }
      }
      else
      {
        using (Stream stream = part.GetStream())
        {
          using (BinaryReader binaryReader = new BinaryReader(stream))
          {
            int length = (int) binaryReader.BaseStream.Length;
            string content = Convert.ToBase64String(binaryReader.ReadBytes(length)).Select<char, FlatOpc.FlatOpcTupple>((Func<char, int, FlatOpc.FlatOpcTupple>) ((c, i) => new FlatOpc.FlatOpcTupple()
            {
              FoCharacter = c,
              FoChunk = i / 76
            })).GroupBy<FlatOpc.FlatOpcTupple, int>((Func<FlatOpc.FlatOpcTupple, int>) (c => c.FoChunk)).Aggregate<IGrouping<int, FlatOpc.FlatOpcTupple>, StringBuilder, string>(new StringBuilder(), (Func<StringBuilder, IGrouping<int, FlatOpc.FlatOpcTupple>, StringBuilder>) ((s, i) => s.Append(i.Aggregate<FlatOpc.FlatOpcTupple, StringBuilder, string>(new StringBuilder(), (Func<StringBuilder, FlatOpc.FlatOpcTupple, StringBuilder>) ((seed, it) => seed.Append(it.FoCharacter)), (Func<StringBuilder, string>) (sb => sb.ToString()))).Append(Environment.NewLine)), (Func<StringBuilder, string>) (s => s.ToString()));
            return new XElement(xnamespace + nameof (part), new object[4]
            {
              (object) new XAttribute(xnamespace + "name", (object) part.Uri),
              (object) new XAttribute(xnamespace + "contentType", (object) part.ContentType),
              (object) new XAttribute(xnamespace + "compression", (object) "store"),
              (object) new XElement(xnamespace + "binaryData", (object) content)
            });
          }
        }
      }
    }

    private static XProcessingInstruction GetProcessingInstruction(string path)
    {
      FileInfo fileInfo = new FileInfo(path);
      if (Util.IsWordprocessingML(fileInfo.Extension))
        return new XProcessingInstruction("mso-application", "progid=\"Word.Document\"");
      if (Util.IsPresentationML(fileInfo.Extension))
        return new XProcessingInstruction("mso-application", "progid=\"PowerPoint.Show\"");
      return Util.IsSpreadsheetML(fileInfo.Extension) ? new XProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"") : (XProcessingInstruction) null;
    }

    public static XmlDocument OpcToXmlDocument(string fileName)
    {
      using (Package package = Package.Open(fileName))
      {
        XNamespace xnamespace = (XNamespace) "http://schemas.microsoft.com/office/2006/xmlPackage";
        return FlatOpc.GetXmlDocument(new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), new object[2]
        {
          (object) FlatOpc.GetProcessingInstruction(fileName),
          (object) new XElement(xnamespace + "package", new object[2]
          {
            (object) new XAttribute(XNamespace.Xmlns + "pkg", (object) xnamespace.ToString()),
            (object) ((IEnumerable<PackagePart>) package.GetParts()).Select<PackagePart, XElement>((Func<PackagePart, XElement>) (part => FlatOpc.GetContentsAsXml(part)))
          })
        }));
      }
    }

    public static XDocument OpcToXDocument(string fileName)
    {
      using (Package package = Package.Open(fileName))
      {
        XNamespace xnamespace = (XNamespace) "http://schemas.microsoft.com/office/2006/xmlPackage";
        return new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), new object[2]
        {
          (object) FlatOpc.GetProcessingInstruction(fileName),
          (object) new XElement(xnamespace + "package", new object[2]
          {
            (object) new XAttribute(XNamespace.Xmlns + "pkg", (object) xnamespace.ToString()),
            (object) ((IEnumerable<PackagePart>) package.GetParts()).Select<PackagePart, XElement>((Func<PackagePart, XElement>) (part => FlatOpc.GetContentsAsXml(part)))
          })
        });
      }
    }

    public static string[] OpcToText(string fileName)
    {
      using (Package package = Package.Open(fileName))
      {
        XNamespace xnamespace = (XNamespace) "http://schemas.microsoft.com/office/2006/xmlPackage";
        return new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), new object[2]
        {
          (object) FlatOpc.GetProcessingInstruction(fileName),
          (object) new XElement(xnamespace + "package", new object[2]
          {
            (object) new XAttribute(XNamespace.Xmlns + "pkg", (object) xnamespace.ToString()),
            (object) ((IEnumerable<PackagePart>) package.GetParts()).Select<PackagePart, XElement>((Func<PackagePart, XElement>) (part => FlatOpc.GetContentsAsXml(part)))
          })
        }).ToString().Split(new string[1]
        {
          Environment.NewLine
        }, StringSplitOptions.None);
      }
    }

    private static XmlDocument GetXmlDocument(XDocument document)
    {
      using (XmlReader reader = document.CreateReader())
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(reader);
        if (document.Declaration != null)
        {
          XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration(document.Declaration.Version, document.Declaration.Encoding, document.Declaration.Standalone);
          xmlDocument.InsertBefore((XmlNode) xmlDeclaration, xmlDocument.FirstChild);
        }
        return xmlDocument;
      }
    }

    private static XDocument GetXDocument(this XmlDocument document)
    {
      XDocument xdocument = new XDocument();
      using (XmlWriter writer = xdocument.CreateWriter())
        document.WriteTo(writer);
      XmlDeclaration xmlDeclaration = document.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault<XmlDeclaration>();
      if (xmlDeclaration != null)
        xdocument.Declaration = new XDeclaration(xmlDeclaration.Version, xmlDeclaration.Encoding, xmlDeclaration.Standalone);
      return xdocument;
    }

    public static void FlatToOpc(XmlDocument doc, string outputPath) => FlatOpc.FlatToOpc(FlatOpc.GetXDocument(doc), outputPath);

    public static void FlatToOpc(string xmlText, string outputPath) => FlatOpc.FlatToOpc(XDocument.Parse(xmlText), outputPath);

    public static void FlatToOpc(XDocument doc, string outputPath)
    {
      XNamespace pkg = (XNamespace) "http://schemas.microsoft.com/office/2006/xmlPackage";
      XNamespace xnamespace = (XNamespace) "http://schemas.openxmlformats.org/package/2006/relationships";
      using (Package package = Package.Open(outputPath, FileMode.Create))
      {
        foreach (XElement xelement in doc.Root.Elements().Where<XElement>((Func<XElement, bool>) (p => (string) p.Attribute(pkg + "contentType") != "application/vnd.openxmlformats-package.relationships+xml")))
        {
          string uriString = (string) xelement.Attribute(pkg + "name");
          string str = (string) xelement.Attribute(pkg + "contentType");
          if (str.EndsWith("xml"))
          {
            Uri uri = new Uri(uriString, UriKind.Relative);
            using (Stream stream = package.CreatePart(uri, str, (CompressionOption) 3).GetStream(FileMode.Create))
            {
              using (XmlWriter writer = XmlWriter.Create(stream))
                xelement.Element(pkg + "xmlData").Elements().First<XElement>().WriteTo(writer);
            }
          }
          else
          {
            Uri uri = new Uri(uriString, UriKind.Relative);
            using (Stream stream = package.CreatePart(uri, str, (CompressionOption) 3).GetStream(FileMode.Create))
            {
              using (BinaryWriter binaryWriter = new BinaryWriter(stream))
              {
                char[] array = ((string) xelement.Element(pkg + "binaryData")).Where<char>((Func<char, bool>) (c => c != '\r' && c != '\n')).ToArray<char>();
                byte[] buffer = Convert.FromBase64CharArray(array, 0, array.Length);
                binaryWriter.Write(buffer);
              }
            }
          }
        }
        foreach (XElement element in doc.Root.Elements())
        {
          string str1 = (string) element.Attribute(pkg + "name");
          if ((string) element.Attribute(pkg + "contentType") == "application/vnd.openxmlformats-package.relationships+xml")
          {
            if (str1 == "/_rels/.rels")
            {
              foreach (XElement descendant in element.Descendants(xnamespace + "Relationship"))
              {
                string str2 = (string) descendant.Attribute((XName) "Id");
                string str3 = (string) descendant.Attribute((XName) "Type");
                string uriString = (string) descendant.Attribute((XName) "Target");
                if ((string) descendant.Attribute((XName) "TargetMode") == "External")
                  package.CreateRelationship(new Uri(uriString, UriKind.Absolute), (TargetMode) 1, str3, str2);
                else
                  package.CreateRelationship(new Uri(uriString, UriKind.Relative), (TargetMode) 0, str3, str2);
              }
            }
            else
            {
              string str4 = str1.Substring(0, str1.IndexOf("/_rels"));
              string str5 = str1.Substring(str1.LastIndexOf('/'));
              string str6 = str5.Substring(0, str5.IndexOf(".rels"));
              PackagePart part = package.GetPart(new Uri(str4 + str6, UriKind.Relative));
              foreach (XElement descendant in element.Descendants(xnamespace + "Relationship"))
              {
                string str7 = (string) descendant.Attribute((XName) "Id");
                string str8 = (string) descendant.Attribute((XName) "Type");
                string uriString = (string) descendant.Attribute((XName) "Target");
                if ((string) descendant.Attribute((XName) "TargetMode") == "External")
                  part.CreateRelationship(new Uri(uriString, UriKind.Absolute), (TargetMode) 1, str8, str7);
                else
                  part.CreateRelationship(new Uri(uriString, UriKind.Relative), (TargetMode) 0, str8, str7);
              }
            }
          }
        }
      }
    }

    private class FlatOpcTupple
    {
      public char FoCharacter;
      public int FoChunk;
    }
  }
}
