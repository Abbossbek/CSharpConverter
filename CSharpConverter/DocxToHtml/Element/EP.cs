// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.EP
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class EP
  {
    public static readonly XNamespace ep = (XNamespace) "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties";
    public static readonly XName Application = EP.ep + nameof (Application);
    public static readonly XName AppVersion = EP.ep + nameof (AppVersion);
    public static readonly XName Characters = EP.ep + nameof (Characters);
    public static readonly XName CharactersWithSpaces = EP.ep + nameof (CharactersWithSpaces);
    public static readonly XName Company = EP.ep + nameof (Company);
    public static readonly XName DocSecurity = EP.ep + nameof (DocSecurity);
    public static readonly XName HeadingPairs = EP.ep + nameof (HeadingPairs);
    public static readonly XName HiddenSlides = EP.ep + nameof (HiddenSlides);
    public static readonly XName HLinks = EP.ep + nameof (HLinks);
    public static readonly XName HyperlinkBase = EP.ep + nameof (HyperlinkBase);
    public static readonly XName HyperlinksChanged = EP.ep + nameof (HyperlinksChanged);
    public static readonly XName Lines = EP.ep + nameof (Lines);
    public static readonly XName LinksUpToDate = EP.ep + nameof (LinksUpToDate);
    public static readonly XName Manager = EP.ep + nameof (Manager);
    public static readonly XName MMClips = EP.ep + nameof (MMClips);
    public static readonly XName Notes = EP.ep + nameof (Notes);
    public static readonly XName Pages = EP.ep + nameof (Pages);
    public static readonly XName Paragraphs = EP.ep + nameof (Paragraphs);
    public static readonly XName PresentationFormat = EP.ep + nameof (PresentationFormat);
    public static readonly XName Properties = EP.ep + nameof (Properties);
    public static readonly XName ScaleCrop = EP.ep + nameof (ScaleCrop);
    public static readonly XName SharedDoc = EP.ep + nameof (SharedDoc);
    public static readonly XName Slides = EP.ep + nameof (Slides);
    public static readonly XName Template = EP.ep + nameof (Template);
    public static readonly XName TitlesOfParts = EP.ep + nameof (TitlesOfParts);
    public static readonly XName TotalTime = EP.ep + nameof (TotalTime);
    public static readonly XName Words = EP.ep + nameof (Words);
  }
}
