// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ListItemRetriever
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public class ListItemRetriever
  {
    private static ListItemRetriever.ListItemInfo NotAListItem = new ListItemRetriever.ListItemInfo(false, true);
    private static ListItemRetriever.ListItemInfo ZeroNumId = new ListItemRetriever.ListItemInfo(false, false);

    public static void SetParagraphLevel(XElement paragraph, int ilvl)
    {
      if (paragraph.Annotation<ListItemRetriever.ParagraphInfo>() != null)
        throw new DocxToHtmlException("Internal error - should never set ilvl more than once.");
      ListItemRetriever.ParagraphInfo annotation = new ListItemRetriever.ParagraphInfo()
      {
        Ilvl = ilvl
      };
      paragraph.AddAnnotation((object) annotation);
    }

    public static int GetParagraphLevel(XElement paragraph) => (paragraph.Annotation<ListItemRetriever.ParagraphInfo>() ?? throw new DocxToHtmlException("Internal error - should never ask for ilvl without it first being set.")).Ilvl;

    public static ListItemRetriever.ListItemInfo GetListItemInfo(
      XDocument numXDoc,
      XDocument stylesXDoc,
      XElement paragraph)
    {
      return paragraph.Annotation<ListItemRetriever.ListItemInfo>() ?? throw new DocxToHtmlException("Attempting to retrieve ListItemInfo before initialization");
    }

    public static void InitListItemInfo(
      XDocument numXDoc,
      XDocument stylesXDoc,
      XElement paragraph)
    {
      if (ListItemRetriever.FirstRunIsEmptySectionBreak(paragraph))
      {
        paragraph.AddAnnotation((object) ListItemRetriever.NotAListItem);
      }
      else
      {
        int? numId = new int?();
        XElement paragraphNumberingProperties = paragraph.Elements(W.pPr).Elements<XElement>(W.numPr).FirstOrDefault<XElement>();
        if (paragraphNumberingProperties != null)
        {
          numId = (int?) paragraphNumberingProperties.Elements(W.numId).Attributes(W.val).FirstOrDefault<XAttribute>();
          if (numId.HasValue)
          {
            int? nullable = numId;
            int num = 0;
            if ((nullable.GetValueOrDefault() == num ? (nullable.HasValue ? 1 : 0) : 0) == 0)
              goto label_6;
          }
          paragraph.AddAnnotation((object) ListItemRetriever.NotAListItem);
          return;
        }
label_6:
        string paragraphStyleName = ListItemRetriever.GetParagraphStyleName(stylesXDoc, paragraph);
        ListItemRetriever.ListItemInfo itemInfoFromCache = ListItemRetriever.GetListItemInfoFromCache(numXDoc, paragraphStyleName, numId);
        if (itemInfoFromCache != null)
        {
          paragraph.AddAnnotation((object) itemInfoFromCache);
          if (itemInfoFromCache.FromParagraph != null)
          {
            int? nullable = (int?) paragraphNumberingProperties.Elements(W.ilvl).Attributes(W.val).FirstOrDefault<XAttribute>();
            if (!nullable.HasValue)
              nullable = new int?(0);
            if ((string) itemInfoFromCache.FromParagraph.Main.AbstractNum.Elements(W.multiLevelType).Attributes(W.val).FirstOrDefault<XAttribute>() == "singleLevel")
              nullable = new int?(0);
            ListItemRetriever.SetParagraphLevel(paragraph, nullable.Value);
          }
          else
          {
            if (itemInfoFromCache.FromStyle == null)
              return;
            int ilvl = itemInfoFromCache.FromStyle.Style_ilvl;
            if ((string) itemInfoFromCache.FromStyle.Main.AbstractNum.Elements(W.multiLevelType).Attributes(W.val).FirstOrDefault<XAttribute>() == "singleLevel")
              ilvl = 0;
            ListItemRetriever.SetParagraphLevel(paragraph, ilvl);
          }
        }
        else
        {
          ListItemRetriever.ListItemInfo listItemInfo = new ListItemRetriever.ListItemInfo();
          int? ilvl1 = new int?();
          bool? zeroNumId1 = new bool?();
          if (paragraphStyleName != null)
            listItemInfo.FromStyle = ListItemRetriever.InitializeStyleListItemSource(numXDoc, stylesXDoc, paragraph, paragraphStyleName, out ilvl1, out zeroNumId1);
          int? ilvl2 = new int?();
          bool? zeroNumId2 = new bool?();
          if (paragraphNumberingProperties != null && paragraphNumberingProperties.Element(W.numId) != null)
            listItemInfo.FromParagraph = ListItemRetriever.InitializeParagraphListItemSource(numXDoc, stylesXDoc, paragraph, paragraphNumberingProperties, out ilvl2, out zeroNumId2);
          bool? nullable = zeroNumId1;
          bool flag1 = true;
          if ((nullable.GetValueOrDefault() == flag1 ? (nullable.HasValue ? 1 : 0) : 0) == 0 || zeroNumId2.HasValue)
          {
            nullable = zeroNumId2;
            bool flag2 = true;
            if ((nullable.GetValueOrDefault() == flag2 ? (nullable.HasValue ? 1 : 0) : 0) == 0)
            {
              int ilvl3 = 0;
              if (ilvl2.HasValue)
                ilvl3 = ilvl2.Value;
              else if (ilvl1.HasValue)
                ilvl3 = ilvl1.Value;
              if (listItemInfo.FromParagraph != null)
              {
                if ((string) listItemInfo.FromParagraph.Main.AbstractNum.Elements(W.multiLevelType).Attributes(W.val).FirstOrDefault<XAttribute>() == "singleLevel")
                  ilvl3 = 0;
              }
              else if (listItemInfo.FromStyle != null && (string) listItemInfo.FromStyle.Main.AbstractNum.Elements(W.multiLevelType).Attributes(W.val).FirstOrDefault<XAttribute>() == "singleLevel")
                ilvl3 = 0;
              ListItemRetriever.SetParagraphLevel(paragraph, ilvl3);
              listItemInfo.IsListItem = listItemInfo.FromStyle != null || listItemInfo.FromParagraph != null;
              paragraph.AddAnnotation((object) listItemInfo);
              ListItemRetriever.AddListItemInfoIntoCache(numXDoc, paragraphStyleName, numId, listItemInfo);
              return;
            }
          }
          paragraph.AddAnnotation((object) ListItemRetriever.NotAListItem);
          ListItemRetriever.AddListItemInfoIntoCache(numXDoc, paragraphStyleName, numId, ListItemRetriever.NotAListItem);
        }
      }
    }

    private static string GetParagraphStyleName(XDocument stylesXDoc, XElement paragraph) => (string) paragraph.Elements(W.pPr).Elements<XElement>(W.pStyle).Attributes(W.val).FirstOrDefault<XAttribute>() ?? ListItemRetriever.GetDefaultParagraphStyleName(stylesXDoc);

    private static bool FirstRunIsEmptySectionBreak(XElement paragraph)
    {
      XElement xelement = paragraph.DescendantsTrimmed(W.txbxContent).Where<XElement>((Func<XElement, bool>) (d => d.Name == W.r)).FirstOrDefault<XElement>();
      bool flag = paragraph.DescendantsTrimmed(W.txbxContent).Where<XElement>((Func<XElement, bool>) (d => d.Name == W.r)).Elements<XElement>(W.t).Any<XElement>();
      return (xelement == null || !flag) && paragraph.Elements(W.pPr).Elements<XElement>(W.sectPr).Any<XElement>();
    }

    private static ListItemRetriever.ListItemSource InitializeParagraphListItemSource(
      XDocument numXDoc,
      XDocument stylesXDoc,
      XElement paragraph,
      XElement paragraphNumberingProperties,
      out int? ilvl,
      out bool? zeroNumId)
    {
      zeroNumId = new bool?();
      int? numId = (int?) paragraphNumberingProperties.Elements(W.numId).Attributes(W.val).FirstOrDefault<XAttribute>();
      ilvl = (int?) paragraphNumberingProperties.Elements(W.ilvl).Attributes(W.val).FirstOrDefault<XAttribute>();
      if (!numId.HasValue)
      {
        zeroNumId = new bool?(true);
        return (ListItemRetriever.ListItemSource) null;
      }
      if (numXDoc.Root.Elements(W.num).FirstOrDefault<XElement>((Func<XElement, bool>) (n =>
      {
        int num = (int) n.Attribute(W.numId);
        int? nullable = numId;
        int valueOrDefault = nullable.GetValueOrDefault();
        return num == valueOrDefault && nullable.HasValue;
      })) == null)
      {
        zeroNumId = new bool?(true);
        return (ListItemRetriever.ListItemSource) null;
      }
      zeroNumId = new bool?(false);
      if (!ilvl.HasValue)
        ilvl = new int?(0);
      return new ListItemRetriever.ListItemSource(numXDoc, stylesXDoc, numId.Value);
    }

    private static ListItemRetriever.ListItemSource InitializeStyleListItemSource(
      XDocument numXDoc,
      XDocument stylesXDoc,
      XElement paragraph,
      string paragraphStyleName,
      out int? ilvl,
      out bool? zeroNumId)
    {
      zeroNumId = new bool?();
      XElement xelement1 = FormattingAssembler.ParagraphStyleRollup(paragraph, stylesXDoc, ListItemRetriever.GetDefaultParagraphStyleName(stylesXDoc));
      if (xelement1 != null)
      {
        XElement xelement2 = xelement1.Elements(W.numPr).FirstOrDefault<XElement>();
        if (xelement2 != null && xelement2.Element(W.numId) != null)
        {
          int numId = (int) xelement2.Elements(W.numId).Attributes(W.val).FirstOrDefault<XAttribute>();
          ilvl = (int?) xelement2.Elements(W.ilvl).Attributes(W.val).FirstOrDefault<XAttribute>();
          if (!ilvl.HasValue)
            ilvl = new int?(0);
          if (numId == 0)
          {
            zeroNumId = new bool?(true);
            return (ListItemRetriever.ListItemSource) null;
          }
          if (numXDoc.Root.Elements(W.num).Where<XElement>((Func<XElement, bool>) (e => (int) e.Attribute(W.numId) == numId)).FirstOrDefault<XElement>() == null)
          {
            zeroNumId = new bool?(true);
            return (ListItemRetriever.ListItemSource) null;
          }
          ListItemRetriever.ListItemSource listItemSource = new ListItemRetriever.ListItemSource(numXDoc, stylesXDoc, numId);
          listItemSource.Style_ilvl = ilvl.Value;
          zeroNumId = new bool?(false);
          return listItemSource;
        }
      }
      ilvl = new int?();
      return (ListItemRetriever.ListItemSource) null;
    }

    private static string GetDefaultParagraphStyleName(XDocument stylesXDoc)
    {
      ListItemRetriever.StylesInfo stylesInfo = stylesXDoc.Annotation<ListItemRetriever.StylesInfo>();
      string paragraphStyleName1;
      if (stylesInfo != null)
      {
        paragraphStyleName1 = stylesInfo.DefaultParagraphStyleName;
      }
      else
      {
        XElement xelement = stylesXDoc.Root.Elements(W.style).FirstOrDefault<XElement>((Func<XElement, bool>) (s =>
        {
          if ((string) s.Attribute(W.type) != "paragraph")
            return false;
          XAttribute xattribute = s.Attribute(W._default);
          bool paragraphStyleName2 = false;
          if (xattribute != null && s.Attribute(W._default).ToBoolean().Value)
            paragraphStyleName2 = true;
          return paragraphStyleName2;
        }));
        paragraphStyleName1 = (string) null;
        if (xelement != null)
          paragraphStyleName1 = (string) xelement.Attribute(W.styleId);
        ListItemRetriever.StylesInfo annotation = new ListItemRetriever.StylesInfo()
        {
          DefaultParagraphStyleName = paragraphStyleName1
        };
        stylesXDoc.AddAnnotation((object) annotation);
      }
      return paragraphStyleName1;
    }

    private static ListItemRetriever.ListItemInfo GetListItemInfoFromCache(
      XDocument numXDoc,
      string styleName,
      int? numId)
    {
      string key = (styleName == null ? "" : styleName) + "|" + (!numId.HasValue ? "" : numId.ToString());
      XElement root = numXDoc.Root;
      Dictionary<string, ListItemRetriever.ListItemInfo> annotation = root.Annotation<Dictionary<string, ListItemRetriever.ListItemInfo>>();
      if (annotation == null)
      {
        annotation = new Dictionary<string, ListItemRetriever.ListItemInfo>();
        root.AddAnnotation((object) annotation);
      }
      return annotation.ContainsKey(key) ? annotation[key] : (ListItemRetriever.ListItemInfo) null;
    }

    private static void AddListItemInfoIntoCache(
      XDocument numXDoc,
      string styleName,
      int? numId,
      ListItemRetriever.ListItemInfo listItemInfo)
    {
      string key = (styleName == null ? "" : styleName) + "|" + (!numId.HasValue ? "" : numId.ToString());
      XElement root = numXDoc.Root;
      Dictionary<string, ListItemRetriever.ListItemInfo> annotation = root.Annotation<Dictionary<string, ListItemRetriever.ListItemInfo>>();
      if (annotation == null)
      {
        annotation = new Dictionary<string, ListItemRetriever.ListItemInfo>();
        root.AddAnnotation((object) annotation);
      }
      if (annotation.ContainsKey(key))
        return;
      annotation.Add(key, listItemInfo);
    }

    public static string RetrieveListItem(WordprocessingDocument wordDoc, XElement paragraph) => ListItemRetriever.RetrieveListItem(wordDoc, paragraph, (ListItemRetrieverSettings) null);

    public static string RetrieveListItem(
      WordprocessingDocument wordDoc,
      XElement paragraph,
      ListItemRetrieverSettings settings)
    {
      if (wordDoc.MainDocumentPart.NumberingDefinitionsPart == null)
        return (string) null;
      if (paragraph.Annotation<ListItemRetriever.ListItemInfo>() == null)
        ListItemRetriever.InitializeListItemRetriever(wordDoc, settings);
      ListItemRetriever.ListItemInfo lii = paragraph.Annotation<ListItemRetriever.ListItemInfo>();
      if (!lii.IsListItem)
        return (string) null;
      NumberingDefinitionsPart numberingDefinitionsPart = wordDoc.MainDocumentPart.NumberingDefinitionsPart;
      if (numberingDefinitionsPart == null)
        return (string) null;
      StyleDefinitionsPart styleDefinitionsPart = wordDoc.MainDocumentPart.StyleDefinitionsPart;
      if (styleDefinitionsPart == null)
        return (string) null;
      numberingDefinitionsPart.GetXDocument();
      XDocument xdocument = styleDefinitionsPart.GetXDocument();
      string lvlText = (string) lii.Lvl(ListItemRetriever.GetParagraphLevel(paragraph)).Elements(W.lvlText).Attributes(W.val).FirstOrDefault<XAttribute>();
      if (lvlText == null)
        return (string) null;
      int[] levelNumbersArray = (paragraph.Annotation<ListItemRetriever.LevelNumbers>() ?? throw new DocxToHtmlException("Internal error")).LevelNumbersArray;
      string languageIdentifier = ListItemRetriever.GetLanguageIdentifier(paragraph, xdocument);
      return ListItemRetriever.FormatListItem(lii, levelNumbersArray, ListItemRetriever.GetParagraphLevel(paragraph), lvlText, xdocument, languageIdentifier, settings);
    }

    private static string GetLanguageIdentifier(XElement paragraph, XDocument stylesXDoc)
    {
      string str = (string) paragraph.DescendantsTrimmed(W.txbxContent).Where<XElement>((Func<XElement, bool>) (d => d.Name == W.r)).Attributes(PtOpenXml.LanguageType).FirstOrDefault<XAttribute>();
      string languageIdentifier = (string) null;
      if (str == null || str == "western")
        languageIdentifier = (string) paragraph.Elements(W.r).Elements<XElement>(W.rPr).Elements<XElement>(W.lang).Attributes(W.val).FirstOrDefault<XAttribute>() ?? (string) stylesXDoc.Root.Elements(W.docDefaults).Elements<XElement>(W.rPrDefault).Elements<XElement>(W.rPr).Elements<XElement>(W.lang).Attributes(W.val).FirstOrDefault<XAttribute>();
      else if (str == "eastAsia")
        languageIdentifier = (string) paragraph.Elements(W.r).Elements<XElement>(W.rPr).Elements<XElement>(W.lang).Attributes(W.eastAsia).FirstOrDefault<XAttribute>() ?? (string) stylesXDoc.Root.Elements(W.docDefaults).Elements<XElement>(W.rPrDefault).Elements<XElement>(W.rPr).Elements<XElement>(W.lang).Attributes(W.eastAsia).FirstOrDefault<XAttribute>();
      else if (str == "bidi")
        languageIdentifier = (string) paragraph.Elements(W.r).Elements<XElement>(W.rPr).Elements<XElement>(W.lang).Attributes(W.bidi).FirstOrDefault<XAttribute>() ?? (string) stylesXDoc.Root.Elements(W.docDefaults).Elements<XElement>(W.rPrDefault).Elements<XElement>(W.rPr).Elements<XElement>(W.lang).Attributes(W.bidi).FirstOrDefault<XAttribute>();
      if (languageIdentifier == null)
        languageIdentifier = "en-US";
      return languageIdentifier;
    }

    private static void InitializeListItemRetriever(
      WordprocessingDocument wordDoc,
      ListItemRetrieverSettings settings)
    {
      foreach (OpenXmlPart contentPart in wordDoc.ContentParts())
        ListItemRetriever.InitializeListItemRetrieverForPart(wordDoc, contentPart, settings);
    }

    private static void InitializeListItemRetrieverForPart(
      WordprocessingDocument wordDoc,
      OpenXmlPart part,
      ListItemRetrieverSettings settings)
    {
      XDocument xdocument1 = part.GetXDocument();
      NumberingDefinitionsPart numberingDefinitionsPart = wordDoc.MainDocumentPart.NumberingDefinitionsPart;
      if (numberingDefinitionsPart == null)
        return;
      XDocument xdocument2 = numberingDefinitionsPart.GetXDocument();
      StyleDefinitionsPart styleDefinitionsPart = wordDoc.MainDocumentPart.StyleDefinitionsPart;
      if (styleDefinitionsPart == null)
        return;
      XDocument xdocument3 = styleDefinitionsPart.GetXDocument();
      XElement root = xdocument1.Root;
      ListItemRetriever.InitializeListItemRetrieverForStory(xdocument2, xdocument3, root);
      foreach (XElement descendant in xdocument1.Root.Descendants(W.txbxContent))
        ListItemRetriever.InitializeListItemRetrieverForStory(xdocument2, xdocument3, descendant);
    }

    private static void InitializeListItemRetrieverForStory(
      XDocument numXDoc,
      XDocument stylesXDoc,
      XElement rootNode)
    {
      IEnumerable<XElement> source1 = rootNode.DescendantsTrimmed(W.txbxContent).Where<XElement>((Func<XElement, bool>) (p => p.Name == W.p));
      foreach (XElement paragraph in source1)
        ListItemRetriever.InitListItemInfo(numXDoc, stylesXDoc, paragraph);
      foreach (int? nullable1 in source1.Select<XElement, int?>((Func<XElement, int?>) (paragraph =>
      {
        ListItemRetriever.ListItemInfo listItemInfo = paragraph.Annotation<ListItemRetriever.ListItemInfo>();
        return !listItemInfo.IsListItem ? new int?() : listItemInfo.AbstractNumId;
      })).Where<int?>((Func<int?, bool>) (a => a.HasValue)).Distinct<int?>().ToList<int?>())
      {
        int? abstractNumId = nullable1;
        List<XElement> list = source1.Where<XElement>((Func<XElement, bool>) (paragraph =>
        {
          ListItemRetriever.ListItemInfo listItemInfo = paragraph.Annotation<ListItemRetriever.ListItemInfo>();
          if (!listItemInfo.IsListItem)
            return false;
          int? abstractNumId1 = listItemInfo.AbstractNumId;
          int? nullable2 = abstractNumId;
          return abstractNumId1.GetValueOrDefault() == nullable2.GetValueOrDefault() && abstractNumId1.HasValue == nullable2.HasValue;
        })).ToList<XElement>();
        XElement xelement1 = (XElement) null;
        foreach (XElement xelement2 in list)
        {
          xelement2.AddAnnotation((object) new ListItemRetriever.ReverseAxis()
          {
            PreviousParagraph = xelement1
          });
          xelement1 = xelement2;
        }
        List<int> intList1 = new List<int>();
        List<int> source2 = (List<int>) null;
        ListItemRetriever.ListItemInfo[] listItemInfoArray = new ListItemRetriever.ListItemInfo[10];
        foreach (XElement paragraph in list)
        {
          ListItemRetriever.ListItemInfo listItemInfo1 = paragraph.Annotation<ListItemRetriever.ListItemInfo>();
          int ilvl = ListItemRetriever.GetParagraphLevel(paragraph);
          listItemInfoArray[ilvl] = listItemInfo1;
          ListItemRetriever.ListItemInfo listItemInfo2 = (ListItemRetriever.ListItemInfo) null;
          if (ilvl > 0)
            listItemInfo2 = listItemInfoArray[ilvl - 1];
          List<int> intList2 = new List<int>();
          for (int index = 0; index <= ilvl; ++index)
          {
            int numId = listItemInfo1.NumId;
            int? nullable3 = listItemInfo1.StartOverride(index);
            int? nullable4 = new int?();
            if (listItemInfo2 != null)
              nullable4 = listItemInfo2.StartOverride(index);
            if (index == ilvl)
            {
              int? nullable5 = (int?) listItemInfo1.Lvl(ilvl).Elements(W.lvlRestart).Attributes(W.val).FirstOrDefault<XAttribute>();
              if (nullable5.HasValue)
              {
                XElement xelement3 = ListItemRetriever.PreviousParagraphsForLvlRestart(paragraph, nullable5.Value).FirstOrDefault<XElement>((Func<XElement, bool>) (p => ListItemRetriever.GetParagraphLevel(p) == ilvl));
                if (xelement3 != null)
                  source2 = ((IEnumerable<int>) xelement3.Annotation<ListItemRetriever.LevelNumbers>().LevelNumbersArray).ToList<int>();
              }
            }
            if (source2 == null || index >= source2.Count<int>() || index == ilvl && nullable3.HasValue && !intList1.Contains(numId))
            {
              if (source2 == null || index >= source2.Count<int>())
              {
                int num1 = listItemInfo1.Start(index);
                if (index == ilvl)
                {
                  if (nullable3.HasValue && !intList1.Contains(numId))
                  {
                    intList1.Add(numId);
                    num1 = nullable3.Value;
                  }
                  else
                  {
                    if (nullable3.HasValue)
                      num1 = nullable3.Value;
                    if (nullable4.HasValue)
                    {
                      int? nullable6 = nullable4;
                      int num2 = num1;
                      if ((nullable6.GetValueOrDefault() > num2 ? (nullable6.HasValue ? 1 : 0) : 0) != 0)
                        num1 = nullable4.Value;
                    }
                  }
                }
                intList2.Add(num1);
              }
              else
              {
                int num = listItemInfo1.Start(index);
                if (index == ilvl && nullable3.HasValue && !intList1.Contains(numId))
                {
                  intList1.Add(numId);
                  num = nullable3.Value;
                }
                intList2.Add(num);
              }
            }
            else
            {
              int? nullable7 = new int?();
              if (index == ilvl)
              {
                if (nullable3.HasValue)
                {
                  if (!intList1.Contains(numId))
                  {
                    intList1.Add(numId);
                    nullable7 = new int?(nullable3.Value);
                  }
                  nullable7 = new int?(source2.ElementAt<int>(index) + 1);
                }
                else
                  nullable7 = new int?(source2.ElementAt<int>(index) + 1);
              }
              else
                nullable7 = new int?(source2.ElementAt<int>(index));
              intList2.Add(nullable7.Value);
            }
          }
          ListItemRetriever.LevelNumbers annotation = new ListItemRetriever.LevelNumbers()
          {
            LevelNumbersArray = intList2.ToArray()
          };
          paragraph.AddAnnotation((object) annotation);
          source2 = intList2;
        }
      }
    }

    private static IEnumerable<XElement> PreviousParagraphsForLvlRestart(
      XElement paragraph,
      int ilvl)
    {
      XElement xelement = paragraph;
      while (true)
      {
        ListItemRetriever.ReverseAxis ra = xelement.Annotation<ListItemRetriever.ReverseAxis>();
        if (ra != null && ra.PreviousParagraph != null && ListItemRetriever.GetParagraphLevel(ra.PreviousParagraph) >= ilvl)
        {
          yield return ra.PreviousParagraph;
          xelement = ra.PreviousParagraph;
          ra = (ListItemRetriever.ReverseAxis) null;
        }
        else
          break;
      }
    }

    private static string FormatListItem(
      ListItemRetriever.ListItemInfo lii,
      int[] levelNumbers,
      int ilvl,
      string lvlText,
      XDocument styles,
      string languageCultureName,
      ListItemRetrieverSettings settings)
    {
      string[] array = ListItemRetriever.GetFormatTokens(lvlText).ToArray<string>();
      bool isLgl = lii.Lvl(ilvl).Elements(W.isLgl).Any<XElement>();
      Func<string, int, string> selector = (Func<string, int, string>) ((t, l) =>
      {
        int result;
        if (t.Substring(0, 1) != "%" || !int.TryParse(t.Substring(1), out result))
          return t;
        --result;
        if (result >= levelNumbers.Length)
          result = levelNumbers.Length - 1;
        int levelNumber = levelNumbers[result];
        string str = (string) null;
        XElement xelement1 = lii.Lvl(result);
        string numFmt = (string) xelement1.Elements(W.numFmt).Attributes(W.val).FirstOrDefault<XAttribute>();
        if (numFmt == null)
        {
          XElement xelement2 = xelement1.Elements(MC.AlternateContent).Elements<XElement>(MC.Choice).Elements<XElement>(W.numFmt).FirstOrDefault<XElement>();
          if (xelement2 != null && (string) xelement2.Attribute(W.val) == "custom")
            numFmt = (string) xelement2.Attribute(W.format);
        }
        if (numFmt != "none" && isLgl && numFmt != "decimalZero")
          numFmt = "decimal";
        if (languageCultureName != null && settings != null && settings.ListItemTextImplementations.ContainsKey(languageCultureName))
          str = settings.ListItemTextImplementations[languageCultureName](languageCultureName, levelNumber, numFmt);
        if (str == null)
          str = ListItemTextGetter_Default.GetListItemText(languageCultureName, levelNumber, numFmt);
        return str;
      });
      return ((IEnumerable<string>) array).Select<string, string>(selector).StringConcatenate();
    }

    private static IEnumerable<string> GetFormatTokens(string lvlText)
    {
      int i = 0;
      while (i < lvlText.Length)
      {
        if (lvlText[i] == '%' && i <= lvlText.Length - 2)
        {
          yield return lvlText.Substring(i, 2);
          i += 2;
        }
        else
        {
          int percentIndex = lvlText.IndexOf('%', i);
          if (percentIndex == -1 || percentIndex > lvlText.Length - 2)
          {
            yield return lvlText.Substring(i);
            break;
          }
          yield return lvlText.Substring(i, percentIndex - i);
          yield return lvlText.Substring(percentIndex, 2);
          i = percentIndex + 2;
        }
      }
    }

    public class ListItemSourceSet
    {
      public int NumId;
      public XElement Num;
      public int AbstractNumId;
      public XElement AbstractNum;

      public ListItemSourceSet(XDocument numXDoc, XDocument styleXDoc, int numId)
      {
        this.NumId = numId;
        this.Num = numXDoc.Root.Elements(W.num).FirstOrDefault<XElement>((Func<XElement, bool>) (n => (int) n.Attribute(W.numId) == numId));
        this.AbstractNumId = (int) this.Num.Elements(W.abstractNumId).Attributes(W.val).FirstOrDefault<XAttribute>();
        this.AbstractNum = numXDoc.Root.Elements(W.abstractNum).Where<XElement>((Func<XElement, bool>) (e => (int) e.Attribute(W.abstractNumId) == this.AbstractNumId)).FirstOrDefault<XElement>();
      }

      public int? StartOverride(int ilvl)
      {
        XElement xelement = this.Num.Elements(W.lvlOverride).FirstOrDefault<XElement>((Func<XElement, bool>) (nlo => (int) nlo.Attribute(W.ilvl) == ilvl));
        return xelement != null ? (int?) xelement.Elements(W.startOverride).Attributes(W.val).FirstOrDefault<XAttribute>() : new int?();
      }

      public XElement OverrideLvl(int ilvl) => this.Num.Elements(W.lvlOverride).FirstOrDefault<XElement>((Func<XElement, bool>) (nlo => (int) nlo.Attribute(W.ilvl) == ilvl))?.Element(W.lvl);

      public XElement AbstractLvl(int ilvl) => this.AbstractNum.Elements(W.lvl).FirstOrDefault<XElement>((Func<XElement, bool>) (al => (int) al.Attribute(W.ilvl) == ilvl));

      public XElement Lvl(int ilvl) => this.OverrideLvl(ilvl) ?? this.AbstractLvl(ilvl);
    }

    public class ListItemSource
    {
      public ListItemRetriever.ListItemSourceSet Main;
      public string NumStyleLinkName;
      public ListItemRetriever.ListItemSourceSet NumStyleLink;
      public int Style_ilvl;

      public ListItemSource(XDocument numXDoc, XDocument stylesXDoc, int numId)
      {
        this.Main = new ListItemRetriever.ListItemSourceSet(numXDoc, stylesXDoc, numId);
        this.NumStyleLinkName = (string) this.Main.AbstractNum.Elements(W.numStyleLink).Attributes(W.val).FirstOrDefault<XAttribute>();
        if (this.NumStyleLinkName == null)
          return;
        int? nullable = (int?) stylesXDoc.Root.Elements(W.style).Where<XElement>((Func<XElement, bool>) (s => (string) s.Attribute(W.styleId) == this.NumStyleLinkName)).Elements<XElement>(W.pPr).Elements<XElement>(W.numPr).Elements<XElement>(W.numId).Attributes(W.val).FirstOrDefault<XAttribute>();
        if (!nullable.HasValue)
          return;
        this.NumStyleLink = new ListItemRetriever.ListItemSourceSet(numXDoc, stylesXDoc, nullable.Value);
      }

      public XElement Lvl(int ilvl)
      {
        if (this.NumStyleLink != null)
        {
          XElement xelement = this.NumStyleLink.Lvl(ilvl);
          if (xelement == null)
          {
            for (int ilvl1 = ilvl - 1; ilvl1 >= 0; --ilvl1)
            {
              xelement = this.NumStyleLink.Lvl(ilvl1);
              if (xelement != null)
                break;
            }
          }
          return xelement;
        }
        XElement xelement1 = this.Main.Lvl(ilvl);
        if (xelement1 == null)
        {
          for (int ilvl2 = ilvl - 1; ilvl2 >= 0; --ilvl2)
          {
            xelement1 = this.Main.Lvl(ilvl2);
            if (xelement1 != null)
              break;
          }
        }
        return xelement1;
      }

      public int? StartOverride(int ilvl)
      {
        if (this.NumStyleLink != null)
        {
          int? nullable = this.NumStyleLink.StartOverride(ilvl);
          if (nullable.HasValue)
            return nullable;
        }
        return this.Main.StartOverride(ilvl);
      }

      public int Start(int ilvl)
      {
        int? nullable = (int?) this.Lvl(ilvl).Elements(W.start).Attributes(W.val).FirstOrDefault<XAttribute>();
        return nullable.HasValue ? nullable.Value : 0;
      }

      public int AbstractNumId => this.Main.AbstractNumId;
    }

    public class ListItemInfo
    {
      public bool IsListItem;
      public bool IsZeroNumId;
      public ListItemRetriever.ListItemSource FromStyle;
      public ListItemRetriever.ListItemSource FromParagraph;
      private int? mAbstractNumId;
      private int? mNumId;

      public int? AbstractNumId
      {
        get
        {
          if (this.mAbstractNumId.HasValue)
            return this.mAbstractNumId;
          if (this.FromParagraph != null)
            this.mAbstractNumId = new int?(this.FromParagraph.AbstractNumId);
          else if (this.FromStyle != null)
            this.mAbstractNumId = new int?(this.FromStyle.AbstractNumId);
          return this.mAbstractNumId;
        }
      }

      public XElement Lvl(int ilvl)
      {
        if (this.FromParagraph != null)
        {
          XElement xelement = this.FromParagraph.Lvl(ilvl);
          if (xelement == null)
          {
            for (int ilvl1 = ilvl - 1; ilvl1 >= 0; --ilvl1)
            {
              xelement = this.FromParagraph.Lvl(ilvl1);
              if (xelement != null)
                break;
            }
          }
          return xelement;
        }
        XElement xelement1 = this.FromStyle.Lvl(ilvl);
        if (xelement1 == null)
        {
          for (int ilvl2 = ilvl - 1; ilvl2 >= 0; --ilvl2)
          {
            xelement1 = this.FromParagraph.Lvl(ilvl2);
            if (xelement1 != null)
              break;
          }
        }
        return xelement1;
      }

      public int Start(int ilvl) => this.FromParagraph != null ? this.FromParagraph.Start(ilvl) : this.FromStyle.Start(ilvl);

      public int Start(int ilvl, bool takeOverride, out bool isOverride)
      {
        if (this.FromParagraph != null)
        {
          if (takeOverride)
          {
            int? nullable = this.FromParagraph.StartOverride(ilvl);
            if (nullable.HasValue)
            {
              isOverride = true;
              return nullable.Value;
            }
          }
          isOverride = false;
          return this.FromParagraph.Start(ilvl);
        }
        if (this.FromStyle != null)
        {
          if (takeOverride)
          {
            int? nullable = this.FromStyle.StartOverride(ilvl);
            if (nullable.HasValue)
            {
              isOverride = true;
              return nullable.Value;
            }
          }
          isOverride = false;
          return this.FromStyle.Start(ilvl);
        }
        isOverride = false;
        return 0;
      }

      public int? StartOverride(int ilvl)
      {
        if (this.FromParagraph != null)
        {
          int? nullable = this.FromParagraph.StartOverride(ilvl);
          return nullable.HasValue ? new int?(nullable.Value) : new int?();
        }
        if (this.FromStyle == null)
          return new int?();
        int? nullable1 = this.FromStyle.StartOverride(ilvl);
        return nullable1.HasValue ? new int?(nullable1.Value) : new int?();
      }

      public int NumId
      {
        get
        {
          if (this.mNumId.HasValue)
            return this.mNumId.Value;
          if (this.FromParagraph != null)
            this.mNumId = new int?(this.FromParagraph.Main.NumId);
          else if (this.FromStyle != null)
            this.mNumId = new int?(this.FromStyle.Main.NumId);
          return this.mNumId.Value;
        }
      }

      public ListItemInfo()
      {
      }

      public ListItemInfo(bool isListItem, bool isZeroNumId)
      {
        this.IsListItem = isListItem;
        this.IsZeroNumId = isZeroNumId;
      }
    }

    public class LevelNumbers
    {
      public int[] LevelNumbersArray;
    }

    private class StylesInfo
    {
      public string DefaultParagraphStyleName;
    }

    private class ParagraphInfo
    {
      public int Ilvl;
    }

    private class ReverseAxis
    {
      public XElement PreviousParagraph;
    }
  }
}
