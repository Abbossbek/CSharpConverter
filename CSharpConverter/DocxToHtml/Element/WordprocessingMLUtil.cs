// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.WordprocessingMLUtil
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class WordprocessingMLUtil
  {
    private static HashSet<string> UnknownFonts = new HashSet<string>();
    private static HashSet<string> KnownFamilies = (HashSet<string>) null;
    private static readonly List<XName> AdditionalRunContainerNames = new List<XName>()
    {
      W.w + "bdo",
      W.customXml,
      W.dir,
      W.fldSimple,
      W.hyperlink,
      W.moveFrom,
      W.moveTo,
      W.sdtContent
    };
    private static Dictionary<XName, int> Order_pPr = new Dictionary<XName, int>()
    {
      {
        W.pStyle,
        10
      },
      {
        W.keepNext,
        20
      },
      {
        W.keepLines,
        30
      },
      {
        W.pageBreakBefore,
        40
      },
      {
        W.framePr,
        50
      },
      {
        W.widowControl,
        60
      },
      {
        W.numPr,
        70
      },
      {
        W.suppressLineNumbers,
        80
      },
      {
        W.pBdr,
        90
      },
      {
        W.shd,
        100
      },
      {
        W.tabs,
        120
      },
      {
        W.suppressAutoHyphens,
        130
      },
      {
        W.kinsoku,
        140
      },
      {
        W.wordWrap,
        150
      },
      {
        W.overflowPunct,
        160
      },
      {
        W.topLinePunct,
        170
      },
      {
        W.autoSpaceDE,
        180
      },
      {
        W.autoSpaceDN,
        190
      },
      {
        W.bidi,
        200
      },
      {
        W.adjustRightInd,
        210
      },
      {
        W.snapToGrid,
        220
      },
      {
        W.spacing,
        230
      },
      {
        W.ind,
        240
      },
      {
        W.contextualSpacing,
        250
      },
      {
        W.mirrorIndents,
        260
      },
      {
        W.suppressOverlap,
        270
      },
      {
        W.jc,
        280
      },
      {
        W.textDirection,
        290
      },
      {
        W.textAlignment,
        300
      },
      {
        W.textboxTightWrap,
        310
      },
      {
        W.outlineLvl,
        320
      },
      {
        W.divId,
        330
      },
      {
        W.cnfStyle,
        340
      },
      {
        W.rPr,
        350
      },
      {
        W.sectPr,
        360
      },
      {
        W.pPrChange,
        370
      }
    };
    private static Dictionary<XName, int> Order_rPr = new Dictionary<XName, int>()
    {
      {
        W.ins,
        10
      },
      {
        W.del,
        20
      },
      {
        W.rStyle,
        30
      },
      {
        W.rFonts,
        40
      },
      {
        W.b,
        50
      },
      {
        W.bCs,
        60
      },
      {
        W.i,
        70
      },
      {
        W.iCs,
        80
      },
      {
        W.caps,
        90
      },
      {
        W.smallCaps,
        100
      },
      {
        W.strike,
        110
      },
      {
        W.dstrike,
        120
      },
      {
        W.outline,
        130
      },
      {
        W.shadow,
        140
      },
      {
        W.emboss,
        150
      },
      {
        W.imprint,
        160
      },
      {
        W.noProof,
        170
      },
      {
        W.snapToGrid,
        180
      },
      {
        W.vanish,
        190
      },
      {
        W.webHidden,
        200
      },
      {
        W.color,
        210
      },
      {
        W.spacing,
        220
      },
      {
        W._w,
        230
      },
      {
        W.kern,
        240
      },
      {
        W.position,
        250
      },
      {
        W.sz,
        260
      },
      {
        W14.wShadow,
        270
      },
      {
        W14.wTextOutline,
        280
      },
      {
        W14.wTextFill,
        290
      },
      {
        W14.wScene3d,
        300
      },
      {
        W14.wProps3d,
        310
      },
      {
        W.szCs,
        320
      },
      {
        W.highlight,
        330
      },
      {
        W.u,
        340
      },
      {
        W.effect,
        350
      },
      {
        W.bdr,
        360
      },
      {
        W.shd,
        370
      },
      {
        W.fitText,
        380
      },
      {
        W.vertAlign,
        390
      },
      {
        W.rtl,
        400
      },
      {
        W.cs,
        410
      },
      {
        W.em,
        420
      },
      {
        W.lang,
        430
      },
      {
        W.eastAsianLayout,
        440
      },
      {
        W.specVanish,
        450
      },
      {
        W.oMath,
        460
      }
    };
    private static Dictionary<XName, int> Order_tblPr = new Dictionary<XName, int>()
    {
      {
        W.tblStyle,
        10
      },
      {
        W.tblpPr,
        20
      },
      {
        W.tblOverlap,
        30
      },
      {
        W.bidiVisual,
        40
      },
      {
        W.tblStyleRowBandSize,
        50
      },
      {
        W.tblStyleColBandSize,
        60
      },
      {
        W.tblW,
        70
      },
      {
        W.jc,
        80
      },
      {
        W.tblCellSpacing,
        90
      },
      {
        W.tblInd,
        100
      },
      {
        W.tblBorders,
        110
      },
      {
        W.shd,
        120
      },
      {
        W.tblLayout,
        130
      },
      {
        W.tblCellMar,
        140
      },
      {
        W.tblLook,
        150
      },
      {
        W.tblCaption,
        160
      },
      {
        W.tblDescription,
        170
      }
    };
    private static Dictionary<XName, int> Order_tblBorders = new Dictionary<XName, int>()
    {
      {
        W.top,
        10
      },
      {
        W.left,
        20
      },
      {
        W.start,
        30
      },
      {
        W.bottom,
        40
      },
      {
        W.right,
        50
      },
      {
        W.end,
        60
      },
      {
        W.insideH,
        70
      },
      {
        W.insideV,
        80
      }
    };
    private static Dictionary<XName, int> Order_tcPr = new Dictionary<XName, int>()
    {
      {
        W.cnfStyle,
        10
      },
      {
        W.tcW,
        20
      },
      {
        W.gridSpan,
        30
      },
      {
        W.hMerge,
        40
      },
      {
        W.vMerge,
        50
      },
      {
        W.tcBorders,
        60
      },
      {
        W.shd,
        70
      },
      {
        W.noWrap,
        80
      },
      {
        W.tcMar,
        90
      },
      {
        W.textDirection,
        100
      },
      {
        W.tcFitText,
        110
      },
      {
        W.vAlign,
        120
      },
      {
        W.hideMark,
        130
      },
      {
        W.headers,
        140
      }
    };
    private static Dictionary<XName, int> Order_tcBorders = new Dictionary<XName, int>()
    {
      {
        W.top,
        10
      },
      {
        W.start,
        20
      },
      {
        W.left,
        30
      },
      {
        W.bottom,
        40
      },
      {
        W.right,
        50
      },
      {
        W.end,
        60
      },
      {
        W.insideH,
        70
      },
      {
        W.insideV,
        80
      },
      {
        W.tl2br,
        90
      },
      {
        W.tr2bl,
        100
      }
    };
    private static Dictionary<XName, int> Order_pBdr = new Dictionary<XName, int>()
    {
      {
        W.top,
        10
      },
      {
        W.left,
        20
      },
      {
        W.bottom,
        30
      },
      {
        W.right,
        40
      },
      {
        W.between,
        50
      },
      {
        W.bar,
        60
      }
    };

    public static Size MeasureText(
      string text,
      Font font,
      Size proposedSize,
      TextFormatFlags flags)
    {
      if (string.IsNullOrEmpty(text))
        return Size.Empty;
      Bitmap bitmap = new Bitmap(500, 200);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      SizeF sizeF = graphics.MeasureString(text, font);
      ((Image) bitmap).Dispose();
      graphics.Dispose();
      return new Size((int) Math.Floor((double) sizeF.Width), (int) Math.Floor((double) sizeF.Height));
    }

    public static int CalcWidthOfRunInTwips(XElement r)
    {
      if (WordprocessingMLUtil.KnownFamilies == null)
      {
        WordprocessingMLUtil.KnownFamilies = new HashSet<string>();
        foreach (FontFamily family in FontFamily.Families)
          WordprocessingMLUtil.KnownFamilies.Add(family.Name);
      }
      string str1 = (string) r.Attribute(PtOpenXml.pt + "FontName") ?? (string) r.Ancestors(W.p).First<XElement>().Attribute(PtOpenXml.pt + "FontName");
      if (str1 == null)
        throw new DocxToHtmlException("Internal Error, should have FontName attribute");
      if (WordprocessingMLUtil.UnknownFonts.Contains(str1))
        return 0;
      XElement runProps = r.Element(W.rPr);
      if (runProps == null)
        throw new DocxToHtmlException("Internal Error, should have run properties");
      string str2 = (string) r.Attribute(PtOpenXml.LanguageType);
      Decimal? nullable1 = new Decimal?();
      Decimal? nullable2 = !(str2 == "bidi") ? (Decimal?) runProps.Elements(W.sz).Attributes(W.val).FirstOrDefault<XAttribute>() : (Decimal?) runProps.Elements(W.szCs).Attributes(W.val).FirstOrDefault<XAttribute>();
      if (!nullable2.HasValue)
        nullable2 = new Decimal?(22M);
      if (!WordprocessingMLUtil.KnownFamilies.Contains(str1))
        return 0;
      FontFamily fontFamily;
      try
      {
        fontFamily = new FontFamily(str1);
      }
      catch (ArgumentException ex)
      {
        WordprocessingMLUtil.UnknownFonts.Add(str1);
        return 0;
      }
      FontStyle fontStyle1 = (FontStyle) 0;
      bool flag1 = WordprocessingMLUtil.GetBoolProp(runProps, W.b) || WordprocessingMLUtil.GetBoolProp(runProps, W.bCs);
      bool flag2 = WordprocessingMLUtil.GetBoolProp(runProps, W.i) || WordprocessingMLUtil.GetBoolProp(runProps, W.iCs);
      if (flag1 && !flag2)
        fontStyle1 = (FontStyle) 1;
      if (flag2 && !flag1)
        fontStyle1 = (FontStyle) 2;
      if (flag1 & flag2)
        fontStyle1 = (FontStyle) 3;
      string text = r.DescendantsTrimmed(W.txbxContent).Where<XElement>((Func<XElement, bool>) (e => e.Name == W.t)).Select<XElement, string>((Func<XElement, string>) (t => (string) t)).StringConcatenate();
      Decimal num1 = r.DescendantsTrimmed(W.txbxContent).Where<XElement>((Func<XElement, bool>) (e => e.Name == W.tab)).Select<XElement, Decimal>((Func<XElement, Decimal>) (t => (Decimal) t.Attribute(PtOpenXml.TabWidth))).Sum();
      if (text.Length == 0 && num1 == 0M)
        return 0;
      int num2 = 1;
      if (text.Length <= 2)
        num2 = 100;
      else if (text.Length <= 4)
        num2 = 50;
      else if (text.Length <= 8)
        num2 = 25;
      else if (text.Length <= 16)
        num2 = 12;
      else if (text.Length <= 32)
        num2 = 6;
      if (num2 != 1)
      {
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < num2; ++index)
          stringBuilder.Append(text);
        text = stringBuilder.ToString();
      }
      try
      {
        using (Font font = new Font(fontFamily, (float) nullable2.Value / 2f, fontStyle1))
        {
          TextFormatFlags flags = TextFormatFlags.NoPadding;
          Size proposedSize = new Size(int.MaxValue, int.MaxValue);
          return (int) ((Decimal) WordprocessingMLUtil.MeasureText(text, font, proposedSize, flags).Width / 96M * 1440M / (Decimal) num2 + num1 * 1440M);
        }
      }
      catch (ArgumentException ex1)
      {
        try
        {
          FontStyle fontStyle2 = (FontStyle) 0;
          using (Font font = new Font(fontFamily, (float) nullable2.Value / 2f, fontStyle2))
          {
            TextFormatFlags flags = TextFormatFlags.NoPadding;
            Size proposedSize = new Size(int.MaxValue, int.MaxValue);
            return (int) ((Decimal) WordprocessingMLUtil.MeasureText(text, font, proposedSize, flags).Width / 96M * 1440M / (Decimal) num2 + num1 * 1440M);
          }
        }
        catch (ArgumentException ex2)
        {
          FontStyle fontStyle3 = (FontStyle) 1;
          try
          {
            using (Font font = new Font(fontFamily, (float) nullable2.Value / 2f, fontStyle3))
            {
              TextFormatFlags flags = TextFormatFlags.NoPadding;
              Size proposedSize = new Size(int.MaxValue, int.MaxValue);
              return (int) ((Decimal) WordprocessingMLUtil.MeasureText(text, font, proposedSize, flags).Width / 96M * 1440M / (Decimal) num2 + num1 * 1440M);
            }
          }
          catch (ArgumentException ex3)
          {
            using (Font font = new Font(new FontFamily("Times New Roman"), (float) nullable2.Value / 2f, fontStyle1))
            {
              TextFormatFlags flags = TextFormatFlags.NoPadding;
              Size proposedSize = new Size(int.MaxValue, int.MaxValue);
              return (int) ((Decimal) WordprocessingMLUtil.MeasureText(text, font, proposedSize, flags).Width / 96M * 1440M / (Decimal) num2 + num1 * 1440M);
            }
          }
        }
      }
    }

    public static bool GetBoolProp(XElement runProps, XName xName)
    {
      XElement xelement = runProps.Element(xName);
      if (xelement == null)
        return false;
      XAttribute xattribute = xelement.Attribute(W.val);
      if (xattribute == null)
        return true;
      string lower = xattribute.Value.ToLower();
      return !(lower == "0") && !(lower == "false") && (lower == "1" || lower == "true");
    }

    public static XElement CoalesceAdjacentRunsWithIdenticalFormatting(XElement runContainer)
    {
      IEnumerable<IGrouping<string, XElement>> source = runContainer.Elements().GroupAdjacent<XElement, string>((Func<XElement, string>) (ce =>
      {
        if (ce.Name == W.r)
        {
          if (ce.Elements().Count<XElement>((Func<XElement, bool>) (e => e.Name != W.rPr)) != 1)
            return "DontConsolidate";
          XElement xelement = ce.Element(W.rPr);
          string str = xelement != null ? xelement.ToString(SaveOptions.None) : string.Empty;
          if (ce.Element(W.t) != null)
            return "Wt" + str;
          return ce.Element(W.instrText) != null ? "WinstrText" + str : "DontConsolidate";
        }
        if (ce.Name == W.ins)
        {
          if (ce.Elements(W.del).Any<XElement>() || ce.Elements().Elements<XElement>().Count<XElement>((Func<XElement, bool>) (e => e.Name != W.rPr)) != 1 || !ce.Elements().Elements<XElement>(W.t).Any<XElement>())
            return "DontConsolidate";
          XAttribute xattribute = ce.Attribute(W.date);
          return "Wins2" + ((string) ce.Attribute(W.author) ?? string.Empty) + (xattribute != null ? ((DateTime) xattribute).ToString("s") : string.Empty) + (string) ce.Attribute(W.id) + ce.Elements().Elements<XElement>(W.rPr).Select<XElement, string>((Func<XElement, string>) (rPr => rPr.ToString(SaveOptions.None))).StringConcatenate();
        }
        if (!(ce.Name == W.del) || ce.Elements(W.r).Elements<XElement>().Count<XElement>((Func<XElement, bool>) (e => e.Name != W.rPr)) != 1 || !ce.Elements().Elements<XElement>(W.delText).Any<XElement>())
          return "DontConsolidate";
        XAttribute xattribute1 = ce.Attribute(W.date);
        return "Wdel" + ((string) ce.Attribute(W.author) ?? string.Empty) + (xattribute1 != null ? ((DateTime) xattribute1).ToString("s") : string.Empty) + ce.Elements(W.r).Elements<XElement>(W.rPr).Select<XElement, string>((Func<XElement, string>) (rPr => rPr.ToString(SaveOptions.None))).StringConcatenate();
      }));
      XElement xelement1 = new XElement(runContainer.Name, new object[2]
      {
        (object) runContainer.Attributes(),
        (object) source.Select<IGrouping<string, XElement>, object>((Func<IGrouping<string, XElement>, object>) (g =>
        {
          if (g.Key == "DontConsolidate")
            return (object) g;
          string str = g.Select<XElement, string>((Func<XElement, string>) (r => r.Descendants().Where<XElement>((Func<XElement, bool>) (d => d.Name == W.t || d.Name == W.delText || d.Name == W.instrText)).Select<XElement, string>((Func<XElement, string>) (d => d.Value)).StringConcatenate())).StringConcatenate();
          XAttribute xmlSpaceAttribute = XmlUtil.GetXmlSpaceAttribute(str);
          if (g.First<XElement>().Name == W.r)
          {
            if (g.First<XElement>().Element(W.t) != null)
            {
              IEnumerable<IEnumerable<XAttribute>> xattributes = g.Select<XElement, IEnumerable<XAttribute>>((Func<XElement, IEnumerable<XAttribute>>) (r => r.Descendants(W.t).Take<XElement>(1).Attributes(PtOpenXml.Status)));
              return (object) new XElement(W.r, new object[2]
              {
                (object) g.First<XElement>().Elements(W.rPr),
                (object) new XElement(W.t, new object[3]
                {
                  (object) xattributes,
                  (object) xmlSpaceAttribute,
                  (object) str
                })
              });
            }
            if (g.First<XElement>().Element(W.instrText) != null)
              return (object) new XElement(W.r, new object[2]
              {
                (object) g.First<XElement>().Elements(W.rPr),
                (object) new XElement(W.instrText, new object[2]
                {
                  (object) xmlSpaceAttribute,
                  (object) str
                })
              });
          }
          if (g.First<XElement>().Name == W.ins)
            return (object) new XElement(W.ins, new object[2]
            {
              (object) g.First<XElement>().Attributes(),
              (object) new XElement(W.r, new object[2]
              {
                (object) g.First<XElement>().Elements(W.r).Elements<XElement>(W.rPr),
                (object) new XElement(W.t, new object[2]
                {
                  (object) xmlSpaceAttribute,
                  (object) str
                })
              })
            });
          if (!(g.First<XElement>().Name == W.del))
            return (object) g;
          return (object) new XElement(W.del, new object[2]
          {
            (object) g.First<XElement>().Attributes(),
            (object) new XElement(W.r, new object[2]
            {
              (object) g.First<XElement>().Elements(W.r).Elements<XElement>(W.rPr),
              (object) new XElement(W.delText, new object[2]
              {
                (object) xmlSpaceAttribute,
                (object) str
              })
            })
          });
        }))
      });
      foreach (XElement descendant in xelement1.Descendants(W.txbxContent))
      {
        foreach (XElement runContainer1 in descendant.DescendantsTrimmed(W.txbxContent).Where<XElement>((Func<XElement, bool>) (d => d.Name == W.p)))
          runContainer1.ReplaceWith((object) WordprocessingMLUtil.CoalesceAdjacentRunsWithIdenticalFormatting(runContainer1));
      }
      foreach (XElement runContainer2 in xelement1.Descendants().Where<XElement>((Func<XElement, bool>) (d => WordprocessingMLUtil.AdditionalRunContainerNames.Contains(d.Name))).ToList<XElement>())
        runContainer2.ReplaceWith((object) WordprocessingMLUtil.CoalesceAdjacentRunsWithIdenticalFormatting(runContainer2));
      return xelement1;
    }

    public static object WmlOrderElementsPerStandard(XNode node)
    {
      if (!(node is XElement xelement))
        return (object) node;
      return xelement.Name == W.pPr ? (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Elements().Select<XElement, XElement>((Func<XElement, XElement>) (e => (XElement) WordprocessingMLUtil.WmlOrderElementsPerStandard((XNode) e))).OrderBy<XElement, int>((Func<XElement, int>) (e => WordprocessingMLUtil.Order_pPr.ContainsKey(e.Name) ? WordprocessingMLUtil.Order_pPr[e.Name] : 999))
      }) : (xelement.Name == W.rPr ? (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Elements().Select<XElement, XElement>((Func<XElement, XElement>) (e => (XElement) WordprocessingMLUtil.WmlOrderElementsPerStandard((XNode) e))).OrderBy<XElement, int>((Func<XElement, int>) (e => WordprocessingMLUtil.Order_rPr.ContainsKey(e.Name) ? WordprocessingMLUtil.Order_rPr[e.Name] : 999))
      }) : (xelement.Name == W.tblPr ? (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Elements().Select<XElement, XElement>((Func<XElement, XElement>) (e => (XElement) WordprocessingMLUtil.WmlOrderElementsPerStandard((XNode) e))).OrderBy<XElement, int>((Func<XElement, int>) (e => WordprocessingMLUtil.Order_tblPr.ContainsKey(e.Name) ? WordprocessingMLUtil.Order_tblPr[e.Name] : 999))
      }) : (xelement.Name == W.tcPr ? (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Elements().Select<XElement, XElement>((Func<XElement, XElement>) (e => (XElement) WordprocessingMLUtil.WmlOrderElementsPerStandard((XNode) e))).OrderBy<XElement, int>((Func<XElement, int>) (e => WordprocessingMLUtil.Order_tcPr.ContainsKey(e.Name) ? WordprocessingMLUtil.Order_tcPr[e.Name] : 999))
      }) : (xelement.Name == W.tcBorders ? (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Elements().Select<XElement, XElement>((Func<XElement, XElement>) (e => (XElement) WordprocessingMLUtil.WmlOrderElementsPerStandard((XNode) e))).OrderBy<XElement, int>((Func<XElement, int>) (e => WordprocessingMLUtil.Order_tcBorders.ContainsKey(e.Name) ? WordprocessingMLUtil.Order_tcBorders[e.Name] : 999))
      }) : (xelement.Name == W.tblBorders ? (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Elements().Select<XElement, XElement>((Func<XElement, XElement>) (e => (XElement) WordprocessingMLUtil.WmlOrderElementsPerStandard((XNode) e))).OrderBy<XElement, int>((Func<XElement, int>) (e => WordprocessingMLUtil.Order_tblBorders.ContainsKey(e.Name) ? WordprocessingMLUtil.Order_tblBorders[e.Name] : 999))
      }) : (xelement.Name == W.pBdr ? (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Elements().Select<XElement, XElement>((Func<XElement, XElement>) (e => (XElement) WordprocessingMLUtil.WmlOrderElementsPerStandard((XNode) e))).OrderBy<XElement, int>((Func<XElement, int>) (e => WordprocessingMLUtil.Order_pBdr.ContainsKey(e.Name) ? WordprocessingMLUtil.Order_pBdr[e.Name] : 999))
      }) : (xelement.Name == W.p ? (object) new XElement(xelement.Name, new object[3]
      {
        (object) xelement.Attributes(),
        (object) xelement.Elements(W.pPr).Select<XElement, XElement>((Func<XElement, XElement>) (e => (XElement) WordprocessingMLUtil.WmlOrderElementsPerStandard((XNode) e))),
        (object) xelement.Elements().Where<XElement>((Func<XElement, bool>) (e => e.Name != W.pPr)).Select<XElement, XElement>((Func<XElement, XElement>) (e => (XElement) WordprocessingMLUtil.WmlOrderElementsPerStandard((XNode) e)))
      }) : (xelement.Name == W.r ? (object) new XElement(xelement.Name, new object[3]
      {
        (object) xelement.Attributes(),
        (object) xelement.Elements(W.rPr).Select<XElement, XElement>((Func<XElement, XElement>) (e => (XElement) WordprocessingMLUtil.WmlOrderElementsPerStandard((XNode) e))),
        (object) xelement.Elements().Where<XElement>((Func<XElement, bool>) (e => e.Name != W.rPr)).Select<XElement, XElement>((Func<XElement, XElement>) (e => (XElement) WordprocessingMLUtil.WmlOrderElementsPerStandard((XNode) e)))
      }) : (object) new XElement(xelement.Name, new object[2]
      {
        (object) xelement.Attributes(),
        (object) xelement.Nodes().Select<XNode, object>((Func<XNode, object>) (n => WordprocessingMLUtil.WmlOrderElementsPerStandard(n)))
      })))))))));
    }

    public static WmlDocument BreakLinkToTemplate(WmlDocument source)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        memoryStream.Write(source.DocumentByteArray, 0, source.DocumentByteArray.Length);
        using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open((Stream) memoryStream, true))
        {
          ExtendedFilePropertiesPart filePropertiesPart = wordprocessingDocument.ExtendedFilePropertiesPart;
          if (filePropertiesPart != null)
          {
            XElement xelement = filePropertiesPart.GetXDocument().Descendants(EP.Template).FirstOrDefault<XElement>();
            if (xelement != null)
              xelement.Value = "";
            filePropertiesPart.PutXDocument();
          }
        }
        return new WmlDocument(source.FileName, memoryStream.ToArray());
      }
    }
  }
}
