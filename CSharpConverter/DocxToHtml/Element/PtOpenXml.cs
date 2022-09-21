// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.PtOpenXml
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class PtOpenXml
  {
    public static XNamespace ptOpenXml = (XNamespace) "http://powertools.codeplex.com/documentbuilder/2011/insert";
    public static XName Insert = PtOpenXml.ptOpenXml + nameof (Insert);
    public static XName Id = (XName) nameof (Id);
    public static XNamespace pt = (XNamespace) "http://powertools.codeplex.com/2011";
    public static XName Uri = PtOpenXml.pt + nameof (Uri);
    public static XName Unid = PtOpenXml.pt + nameof (Unid);
    public static XName SHA1Hash = PtOpenXml.pt + nameof (SHA1Hash);
    public static XName CorrelatedSHA1Hash = PtOpenXml.pt + nameof (CorrelatedSHA1Hash);
    public static XName StructureSHA1Hash = PtOpenXml.pt + nameof (StructureSHA1Hash);
    public static XName CorrelationSet = PtOpenXml.pt + nameof (CorrelationSet);
    public static XName Status = PtOpenXml.pt + nameof (Status);
    public static XName Level = PtOpenXml.pt + nameof (Level);
    public static XName IndentLevel = PtOpenXml.pt + nameof (IndentLevel);
    public static XName ContentType = PtOpenXml.pt + nameof (ContentType);
    public static XName trPr = PtOpenXml.pt + nameof (trPr);
    public static XName tcPr = PtOpenXml.pt + nameof (tcPr);
    public static XName rPr = PtOpenXml.pt + nameof (rPr);
    public static XName pPr = PtOpenXml.pt + nameof (pPr);
    public static XName tblPr = PtOpenXml.pt + nameof (tblPr);
    public static XName style = PtOpenXml.pt + nameof (style);
    public static XName FontName = PtOpenXml.pt + nameof (FontName);
    public static XName LanguageType = PtOpenXml.pt + nameof (LanguageType);
    public static XName AbstractNumId = PtOpenXml.pt + nameof (AbstractNumId);
    public static XName StyleName = PtOpenXml.pt + nameof (StyleName);
    public static XName TabWidth = PtOpenXml.pt + nameof (TabWidth);
    public static XName Leader = PtOpenXml.pt + nameof (Leader);
    public static XName ListItemRun = PtOpenXml.pt + nameof (ListItemRun);
    public static XName HtmlToWmlCssWidth = PtOpenXml.pt + nameof (HtmlToWmlCssWidth);
  }
}
