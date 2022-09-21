// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.PresentationMLUtil
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class PresentationMLUtil
  {
    public static void FixUpPresentationDocument(PresentationDocument pDoc)
    {
      foreach (OpenXmlPart allPart in pDoc.GetAllParts())
      {
        if (allPart.ContentType == "application/vnd.openxmlformats-officedocument.presentationml.slide+xml" || allPart.ContentType == "application/vnd.openxmlformats-officedocument.presentationml.slideMaster+xml" || allPart.ContentType == "application/vnd.openxmlformats-officedocument.presentationml.slideLayout+xml" || allPart.ContentType == "application/vnd.openxmlformats-officedocument.presentationml.notesMaster+xml" || allPart.ContentType == "application/vnd.openxmlformats-officedocument.presentationml.notesSlide+xml" || allPart.ContentType == "application/vnd.openxmlformats-officedocument.presentationml.handoutMaster+xml" || allPart.ContentType == "application/vnd.openxmlformats-officedocument.theme+xml" || allPart.ContentType == "application/vnd.openxmlformats-officedocument.drawingml.chart+xml" || allPart.ContentType == "application/vnd.openxmlformats-officedocument.drawingml.diagramData+xml" || allPart.ContentType == "application/vnd.openxmlformats-officedocument.drawingml.chartshapes+xml" || allPart.ContentType == "application/vnd.ms-office.drawingml.diagramDrawing+xml")
        {
          XDocument xdocument = allPart.GetXDocument();
          xdocument.Descendants().Attributes((XName) "smtClean").Remove();
          xdocument.Descendants().Attributes((XName) "smtId").Remove();
          allPart.PutXDocument();
        }
        if (allPart.ContentType == "application/vnd.openxmlformats-officedocument.vmlDrawing")
        {
          string str1 = (string) null;
          using (Stream stream = allPart.GetStream(FileMode.Open, FileAccess.ReadWrite))
          {
            using (StreamReader streamReader = new StreamReader(stream))
            {
              str1 = Regex.Replace(streamReader.ReadToEnd(), "<!\\[(?<test>.*)\\]>", (MatchEvaluator) (m =>
              {
                string str2 = m.Groups[1].Value;
                return str2.StartsWith("CDATA[") ? "<![" + str2 + "]>" : "<![CDATA[" + str2 + "]]>";
              }), RegexOptions.Multiline);
              string pattern = "o:relid=[\"'](?<id1>.*)[\"'] o:relid=[\"'](?<id2>.*)[\"']";
              str1 = Regex.Replace(str1, pattern, (MatchEvaluator) (m => "o:relid=\"" + m.Groups[1].Value + "\""), RegexOptions.Multiline);
              str1 = str1.Replace("</xml>ml>", "</xml>");
              stream.SetLength((long) str1.Length);
            }
          }
          using (MemoryStream sourceStream = new MemoryStream(Encoding.UTF8.GetBytes(str1)))
            allPart.FeedData((Stream) sourceStream);
        }
      }
    }
  }
}
