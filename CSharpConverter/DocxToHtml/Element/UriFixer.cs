// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.UriFixer
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class UriFixer
  {
    public static void FixInvalidUri(Stream fs, Func<string, Uri> invalidUriHandler)
    {
      XNamespace xnamespace = (XNamespace) "http://schemas.openxmlformats.org/package/2006/relationships";
      using (ZipArchive zipArchive = new ZipArchive(fs, ZipArchiveMode.Update))
      {
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries.ToList<ZipArchiveEntry>())
        {
          if (zipArchiveEntry.Name.EndsWith(".rels"))
          {
            bool flag = false;
            XDocument xdocument = (XDocument) null;
            using (Stream stream = zipArchiveEntry.Open())
            {
              try
              {
                xdocument = XDocument.Load(stream);
                if (xdocument.Root != null)
                {
                  if (xdocument.Root.Name.Namespace == xnamespace)
                  {
                    foreach (XElement xelement in xdocument.Descendants(xnamespace + "Relationship").Where<XElement>((Func<XElement, bool>) (r => r.Attribute((XName) "TargetMode") != null && (string) r.Attribute((XName) "TargetMode") == "External")))
                    {
                      string uriString = (string) xelement.Attribute((XName) "Target");
                      if (uriString != null)
                      {
                        try
                        {
                          Uri uri = new Uri(uriString);
                        }
                        catch (UriFormatException ex)
                        {
                          Uri uri = invalidUriHandler(uriString);
                          xelement.Attribute((XName) "Target").Value = uri.ToString();
                          flag = true;
                        }
                      }
                    }
                  }
                }
              }
              catch (XmlException ex)
              {
                continue;
              }
            }
            if (flag)
            {
              string fullName = zipArchiveEntry.FullName;
              zipArchiveEntry.Delete();
              using (StreamWriter output = new StreamWriter(zipArchive.CreateEntry(fullName).Open()))
              {
                using (XmlWriter writer = XmlWriter.Create((TextWriter) output))
                  xdocument.WriteTo(writer);
              }
            }
          }
        }
      }
    }
  }
}
