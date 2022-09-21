// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.WmlToHtmlConverter
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
    public static class WmlToHtmlConverter
    {
        private enum BorderType
        {
            Paragraph,
            Cell
        }

        private class SectionAnnotation
        {
            public XElement SectionElement;
        }

        private class BorderMappingInfo
        {
            public string CssName;

            public decimal CssSize;
        }

        private static readonly Dictionary<string, int> BorderTypePriority = new Dictionary<string, int>
        {
            { "single", 1 },
            { "thick", 2 },
            { "double", 3 },
            { "dotted", 4 }
        };

        private static readonly Dictionary<string, int> BorderNumber = new Dictionary<string, int>
        {
            { "single", 1 },
            { "thick", 2 },
            { "double", 3 },
            { "dotted", 4 },
            { "dashed", 5 },
            { "dotDash", 5 },
            { "dotDotDash", 5 },
            { "triple", 6 },
            { "thinThickSmallGap", 6 },
            { "thickThinSmallGap", 6 },
            { "thinThickThinSmallGap", 6 },
            { "thinThickMediumGap", 6 },
            { "thickThinMediumGap", 6 },
            { "thinThickThinMediumGap", 6 },
            { "thinThickLargeGap", 6 },
            { "thickThinLargeGap", 6 },
            { "thinThickThinLargeGap", 6 },
            { "wave", 7 },
            { "doubleWave", 7 },
            { "dashSmallGap", 5 },
            { "dashDotStroked", 5 },
            { "threeDEmboss", 7 },
            { "threeDEngrave", 7 },
            { "outset", 7 },
            { "inset", 7 }
        };

        private static readonly HashSet<string> UnknownFonts = new HashSet<string>();

        private static HashSet<string> _knownFamilies;

        private static readonly Dictionary<string, BorderMappingInfo> BorderStyleMap = new Dictionary<string, BorderMappingInfo>
        {
            {
                "single",
                new BorderMappingInfo
                {
                    CssName = "solid",
                    CssSize = 1.0m
                }
            },
            {
                "dotted",
                new BorderMappingInfo
                {
                    CssName = "dotted",
                    CssSize = 1.0m
                }
            },
            {
                "dashSmallGap",
                new BorderMappingInfo
                {
                    CssName = "dashed",
                    CssSize = 1.0m
                }
            },
            {
                "dashed",
                new BorderMappingInfo
                {
                    CssName = "dashed",
                    CssSize = 1.0m
                }
            },
            {
                "dotDash",
                new BorderMappingInfo
                {
                    CssName = "dashed",
                    CssSize = 1.0m
                }
            },
            {
                "dotDotDash",
                new BorderMappingInfo
                {
                    CssName = "dashed",
                    CssSize = 1.0m
                }
            },
            {
                "double",
                new BorderMappingInfo
                {
                    CssName = "double",
                    CssSize = 2.5m
                }
            },
            {
                "triple",
                new BorderMappingInfo
                {
                    CssName = "double",
                    CssSize = 2.5m
                }
            },
            {
                "thinThickSmallGap",
                new BorderMappingInfo
                {
                    CssName = "double",
                    CssSize = 4.5m
                }
            },
            {
                "thickThinSmallGap",
                new BorderMappingInfo
                {
                    CssName = "double",
                    CssSize = 4.5m
                }
            },
            {
                "thinThickThinSmallGap",
                new BorderMappingInfo
                {
                    CssName = "double",
                    CssSize = 6.0m
                }
            },
            {
                "thickThinMediumGap",
                new BorderMappingInfo
                {
                    CssName = "double",
                    CssSize = 6.0m
                }
            },
            {
                "thinThickMediumGap",
                new BorderMappingInfo
                {
                    CssName = "double",
                    CssSize = 6.0m
                }
            },
            {
                "thinThickThinMediumGap",
                new BorderMappingInfo
                {
                    CssName = "double",
                    CssSize = 9.0m
                }
            },
            {
                "thinThickLargeGap",
                new BorderMappingInfo
                {
                    CssName = "double",
                    CssSize = 5.25m
                }
            },
            {
                "thickThinLargeGap",
                new BorderMappingInfo
                {
                    CssName = "double",
                    CssSize = 5.25m
                }
            },
            {
                "thinThickThinLargeGap",
                new BorderMappingInfo
                {
                    CssName = "double",
                    CssSize = 9.0m
                }
            },
            {
                "wave",
                new BorderMappingInfo
                {
                    CssName = "solid",
                    CssSize = 3.0m
                }
            },
            {
                "doubleWave",
                new BorderMappingInfo
                {
                    CssName = "double",
                    CssSize = 5.25m
                }
            },
            {
                "dashDotStroked",
                new BorderMappingInfo
                {
                    CssName = "solid",
                    CssSize = 3.0m
                }
            },
            {
                "threeDEmboss",
                new BorderMappingInfo
                {
                    CssName = "ridge",
                    CssSize = 6.0m
                }
            },
            {
                "threeDEngrave",
                new BorderMappingInfo
                {
                    CssName = "groove",
                    CssSize = 6.0m
                }
            },
            {
                "outset",
                new BorderMappingInfo
                {
                    CssName = "outset",
                    CssSize = 4.5m
                }
            },
            {
                "inset",
                new BorderMappingInfo
                {
                    CssName = "inset",
                    CssSize = 4.5m
                }
            }
        };

        private static readonly Dictionary<string, Func<string, string, string>> ShadeMapper = new Dictionary<string, Func<string, string, string>>
        {
            {
                "auto",
                (string c, string f) => c
            },
            {
                "clear",
                (string c, string f) => f
            },
            {
                "nil",
                (string c, string f) => f
            },
            {
                "solid",
                (string c, string f) => c
            },
            {
                "diagCross",
                (string c, string f) => ConvertColorFillPct(c, f, 0.75)
            },
            {
                "diagStripe",
                (string c, string f) => ConvertColorFillPct(c, f, 0.75)
            },
            {
                "horzCross",
                (string c, string f) => ConvertColorFillPct(c, f, 0.5)
            },
            {
                "horzStripe",
                (string c, string f) => ConvertColorFillPct(c, f, 0.5)
            },
            {
                "pct10",
                (string c, string f) => ConvertColorFillPct(c, f, 0.1)
            },
            {
                "pct12",
                (string c, string f) => ConvertColorFillPct(c, f, 0.125)
            },
            {
                "pct15",
                (string c, string f) => ConvertColorFillPct(c, f, 0.15)
            },
            {
                "pct20",
                (string c, string f) => ConvertColorFillPct(c, f, 0.2)
            },
            {
                "pct25",
                (string c, string f) => ConvertColorFillPct(c, f, 0.25)
            },
            {
                "pct30",
                (string c, string f) => ConvertColorFillPct(c, f, 0.3)
            },
            {
                "pct35",
                (string c, string f) => ConvertColorFillPct(c, f, 0.35)
            },
            {
                "pct37",
                (string c, string f) => ConvertColorFillPct(c, f, 0.375)
            },
            {
                "pct40",
                (string c, string f) => ConvertColorFillPct(c, f, 0.4)
            },
            {
                "pct45",
                (string c, string f) => ConvertColorFillPct(c, f, 0.45)
            },
            {
                "pct50",
                (string c, string f) => ConvertColorFillPct(c, f, 0.5)
            },
            {
                "pct55",
                (string c, string f) => ConvertColorFillPct(c, f, 0.55)
            },
            {
                "pct60",
                (string c, string f) => ConvertColorFillPct(c, f, 0.6)
            },
            {
                "pct62",
                (string c, string f) => ConvertColorFillPct(c, f, 0.625)
            },
            {
                "pct65",
                (string c, string f) => ConvertColorFillPct(c, f, 0.65)
            },
            {
                "pct70",
                (string c, string f) => ConvertColorFillPct(c, f, 0.7)
            },
            {
                "pct75",
                (string c, string f) => ConvertColorFillPct(c, f, 0.75)
            },
            {
                "pct80",
                (string c, string f) => ConvertColorFillPct(c, f, 0.8)
            },
            {
                "pct85",
                (string c, string f) => ConvertColorFillPct(c, f, 0.85)
            },
            {
                "pct87",
                (string c, string f) => ConvertColorFillPct(c, f, 0.875)
            },
            {
                "pct90",
                (string c, string f) => ConvertColorFillPct(c, f, 0.9)
            },
            {
                "pct95",
                (string c, string f) => ConvertColorFillPct(c, f, 0.95)
            },
            {
                "reverseDiagStripe",
                (string c, string f) => ConvertColorFillPct(c, f, 0.5)
            },
            {
                "thinDiagCross",
                (string c, string f) => ConvertColorFillPct(c, f, 0.5)
            },
            {
                "thinDiagStripe",
                (string c, string f) => ConvertColorFillPct(c, f, 0.25)
            },
            {
                "thinHorzCross",
                (string c, string f) => ConvertColorFillPct(c, f, 0.3)
            },
            {
                "thinHorzStripe",
                (string c, string f) => ConvertColorFillPct(c, f, 0.25)
            },
            {
                "thinReverseDiagStripe",
                (string c, string f) => ConvertColorFillPct(c, f, 0.25)
            },
            {
                "thinVertStripe",
                (string c, string f) => ConvertColorFillPct(c, f, 0.25)
            }
        };

        private static readonly Dictionary<string, string> ShadeCache = new Dictionary<string, string>();

        private static readonly Dictionary<string, string> NamedColors = new Dictionary<string, string>
        {
            { "black", "black" },
            { "blue", "blue" },
            { "cyan", "aqua" },
            { "green", "green" },
            { "magenta", "fuchsia" },
            { "red", "red" },
            { "yellow", "yellow" },
            { "white", "white" },
            { "darkBlue", "#00008B" },
            { "darkCyan", "#008B8B" },
            { "darkGreen", "#006400" },
            { "darkMagenta", "#800080" },
            { "darkRed", "#8B0000" },
            { "darkYellow", "#808000" },
            { "darkGray", "#A9A9A9" },
            { "lightGray", "#D3D3D3" },
            { "none", "" }
        };

        private static readonly Dictionary<string, string> FontFallback = new Dictionary<string, string>
        {
            { "Arial", "'{0}', 'sans-serif'" },
            { "Arial Narrow", "'{0}', 'sans-serif'" },
            { "Arial Rounded MT Bold", "'{0}', 'sans-serif'" },
            { "Arial Unicode MS", "'{0}', 'sans-serif'" },
            { "Baskerville Old Face", "'{0}', 'serif'" },
            { "Berlin Sans FB", "'{0}', 'sans-serif'" },
            { "Berlin Sans FB Demi", "'{0}', 'sans-serif'" },
            { "Calibri Light", "'{0}', 'sans-serif'" },
            { "Gill Sans MT", "'{0}', 'sans-serif'" },
            { "Gill Sans MT Condensed", "'{0}', 'sans-serif'" },
            { "Lucida Sans", "'{0}', 'sans-serif'" },
            { "Lucida Sans Unicode", "'{0}', 'sans-serif'" },
            { "Segoe UI", "'{0}', 'sans-serif'" },
            { "Segoe UI Light", "'{0}', 'sans-serif'" },
            { "Segoe UI Semibold", "'{0}', 'sans-serif'" },
            { "Tahoma", "'{0}', 'sans-serif'" },
            { "Trebuchet MS", "'{0}', 'sans-serif'" },
            { "Verdana", "'{0}', 'sans-serif'" },
            { "Book Antiqua", "'{0}', 'serif'" },
            { "Bookman Old Style", "'{0}', 'serif'" },
            { "Californian FB", "'{0}', 'serif'" },
            { "Cambria", "'{0}', 'serif'" },
            { "Constantia", "'{0}', 'serif'" },
            { "Garamond", "'{0}', 'serif'" },
            { "Lucida Bright", "'{0}', 'serif'" },
            { "Lucida Fax", "'{0}', 'serif'" },
            { "Palatino Linotype", "'{0}', 'serif'" },
            { "Times New Roman", "'{0}', 'serif'" },
            { "Wide Latin", "'{0}', 'serif'" },
            { "Courier New", "'{0}'" },
            { "Lucida Console", "'{0}'" }
        };

        private static readonly List<string> ImageContentTypes = new List<string> { "image/png", "image/gif", "image/tiff", "image/jpeg" };

        private static HashSet<string> KnownFamilies
        {
            get
            {
                if (_knownFamilies == null)
                {
                    _knownFamilies = new HashSet<string>();
                    FontFamily[] families = FontFamily.Families;
                    foreach (FontFamily fontFamily in families)
                    {
                        _knownFamilies.Add(fontFamily.Name);
                    }
                }

                return _knownFamilies;
            }
        }

        public static XElement ConvertToHtml(WmlDocument doc, WmlToHtmlConverterSettings htmlConverterSettings)
        {
            using OpenXmlMemoryStreamDocument openXmlMemoryStreamDocument = new OpenXmlMemoryStreamDocument(doc);
            using WordprocessingDocument wordDoc = openXmlMemoryStreamDocument.GetWordprocessingDocument();
            return ConvertToHtml(wordDoc, htmlConverterSettings);
        }

        public static XElement ConvertToHtml(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings htmlConverterSettings)
        {
            RevisionAccepter.AcceptRevisions(wordDoc);
            SimplifyMarkupSettings settings = new SimplifyMarkupSettings
            {
                RemoveComments = true,
                RemoveContentControls = true,
                RemoveEndAndFootNotes = true,
                RemoveFieldCodes = false,
                RemoveLastRenderedPageBreak = true,
                RemovePermissions = true,
                RemoveProof = true,
                RemoveRsidInfo = true,
                RemoveSmartTags = true,
                RemoveSoftHyphens = true,
                RemoveGoBackBookmark = true,
                ReplaceTabsWithSpaces = false
            };
            MarkupSimplifier.SimplifyMarkup(wordDoc, settings);
            FormattingAssemblerSettings settings2 = new FormattingAssemblerSettings
            {
                RemoveStyleNamesFromParagraphAndRunProperties = false,
                ClearStyles = false,
                RestrictToSupportedLanguages = htmlConverterSettings.RestrictToSupportedLanguages,
                RestrictToSupportedNumberingFormats = htmlConverterSettings.RestrictToSupportedNumberingFormats,
                CreateHtmlConverterAnnotationAttributes = true,
                OrderElementsPerStandard = false,
                ListItemRetrieverSettings = ((htmlConverterSettings.ListItemImplementations == null) ? new ListItemRetrieverSettings
                {
                    ListItemTextImplementations = ListItemRetrieverSettings.DefaultListItemTextImplementations
                } : new ListItemRetrieverSettings
                {
                    ListItemTextImplementations = htmlConverterSettings.ListItemImplementations
                })
            };
            FormattingAssembler.AssembleFormatting(wordDoc, settings2);
            InsertAppropriateNonbreakingSpaces(wordDoc);
            CalculateSpanWidthForTabs(wordDoc);
            ReverseTableBordersForRtlTables(wordDoc);
            AdjustTableBorders(wordDoc);
            XElement root = wordDoc.MainDocumentPart.GetXDocument().Root;
            FieldRetriever.AnnotateWithFieldInfo(wordDoc.MainDocumentPart);
            AnnotateForSections(wordDoc);
            XElement xElement = (XElement)ConvertToHtmlTransform(wordDoc, htmlConverterSettings, root, suppressTrailingWhiteSpace: false, 0m);
            ReifyStylesAndClasses(htmlConverterSettings, xElement);
            return xElement;
        }

        private static void ReverseTableBordersForRtlTables(WordprocessingDocument wordDoc)
        {
            foreach (XElement item in wordDoc.MainDocumentPart.GetXDocument().Descendants(W.tbl))
            {
                if (item.Elements(W.tblPr).Elements(W.bidiVisual).FirstOrDefault() == null)
                {
                    continue;
                }

                XElement xElement = item.Elements(W.tblPr).Elements(W.tblBorders).FirstOrDefault();
                if (xElement != null)
                {
                    XElement xElement2 = xElement.Element(W.left);
                    if (xElement2 != null)
                    {
                        xElement2 = new XElement(W.right, xElement2.Attributes());
                    }

                    XElement xElement3 = xElement.Element(W.right);
                    if (xElement3 != null)
                    {
                        xElement3 = new XElement(W.left, xElement3.Attributes());
                    }

                    XElement content = new XElement(W.tblBorders, xElement.Element(W.top), xElement2, xElement.Element(W.bottom), xElement3);
                    xElement.ReplaceWith(content);
                }

                foreach (XElement item2 in item.Elements(W.tr).Elements(W.tc))
                {
                    XElement xElement4 = item2.Elements(W.tcPr).Elements(W.tcBorders).FirstOrDefault();
                    if (xElement4 != null)
                    {
                        XElement xElement5 = xElement4.Element(W.left);
                        if (xElement5 != null)
                        {
                            xElement5 = new XElement(W.right, xElement5.Attributes());
                        }

                        XElement xElement6 = xElement4.Element(W.right);
                        if (xElement6 != null)
                        {
                            xElement6 = new XElement(W.left, xElement6.Attributes());
                        }

                        XElement content2 = new XElement(W.tcBorders, xElement4.Element(W.top), xElement5, xElement4.Element(W.bottom), xElement6);
                        xElement4.ReplaceWith(content2);
                    }
                }
            }
        }

        private static void ReifyStylesAndClasses(WmlToHtmlConverterSettings htmlConverterSettings, XElement xhtml)
        {
            if (htmlConverterSettings.FabricateCssClasses)
            {
                HashSet<string> hashSet = new HashSet<string>();
                var list = (from d in xhtml.DescendantsAndSelf()
                            select new
                            {
                                Element = d,
                                Styles = d.Annotation<Dictionary<string, string>>()
                            } into z
                            where z.Styles != null
                            select z into p
                            select new
                            {
                                Element = p.Element,
                                Styles = p.Styles,
                                StylesString = p.Element.Name.LocalName + "|" + (from k in p.Styles
                                                                                 orderby k.Key
                                                                                 select k into s
                                                                                 select $"{s.Key}: {s.Value};").StringConcatenate()
                            } into p
                            group p by p.StylesString).ToList();
                int num = 1000000;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(Environment.NewLine);
                foreach (var item in list)
                {
                    var anon = item.First();
                    Dictionary<string, string> styles = anon.Styles;
                    string text;
                    if (styles.ContainsKey("PtStyleName"))
                    {
                        text = htmlConverterSettings.CssClassPrefix + styles["PtStyleName"];
                        if (hashSet.Contains(text))
                        {
                            text = htmlConverterSettings.CssClassPrefix + styles["PtStyleName"] + "-" + num.ToString().Substring(1);
                            num++;
                        }
                    }
                    else
                    {
                        text = htmlConverterSettings.CssClassPrefix + num.ToString().Substring(1);
                        num++;
                    }

                    hashSet.Add(text);
                    stringBuilder.Append(anon.Element.Name.LocalName + "." + text + " {" + Environment.NewLine);
                    foreach (KeyValuePair<string, string> item2 in anon.Styles.Where((KeyValuePair<string, string> s) => s.Key != "PtStyleName"))
                    {
                        string value = "    " + item2.Key + ": " + item2.Value + ";" + Environment.NewLine;
                        stringBuilder.Append(value);
                    }

                    stringBuilder.Append("}" + Environment.NewLine);
                    XAttribute content = new XAttribute((XName?)"class", text);
                    foreach (var item3 in item)
                    {
                        item3.Element.Add(content);
                    }
                }

                string styleValue = string.Concat(htmlConverterSettings.GeneralCss, stringBuilder, htmlConverterSettings.AdditionalCss);
                SetStyleElementValue(xhtml, styleValue);
                return;
            }

            SetStyleElementValue(xhtml, htmlConverterSettings.GeneralCss + htmlConverterSettings.AdditionalCss);
            foreach (XElement item4 in xhtml.DescendantsAndSelf())
            {
                Dictionary<string, string> dictionary = item4.Annotation<Dictionary<string, string>>();
                if (dictionary != null)
                {
                    string text2 = (from p in dictionary
                                    where p.Key != "PtStyleName"
                                    orderby p.Key
                                    select p into e
                                    select $"{e.Key}: {e.Value};").StringConcatenate();
                    XAttribute content2 = new XAttribute((XName?)"style", text2);
                    if (item4.Attribute((XName?)"style") != null)
                    {
                        item4.Attribute((XName?)"style")!.Value += text2;
                    }
                    else
                    {
                        item4.Add(content2);
                    }
                }
            }
        }

        private static void SetStyleElementValue(XElement xhtml, string styleValue)
        {
            XElement xElement = xhtml.Descendants(Xhtml.style).FirstOrDefault();
            if (xElement != null)
            {
                xElement.Value = styleValue;
                return;
            }

            xElement = new XElement(Xhtml.style, styleValue);
            xhtml.Element(Xhtml.head)?.Add(xElement);
        }

        private static object ConvertToHtmlTransform(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, XNode node, bool suppressTrailingWhiteSpace, decimal currentMarginLeft)
        {
            XElement element = node as XElement;
            if (element == null)
            {
                return null;
            }

            if (element.Name == W.document)
            {
                return new XElement(Xhtml.html, new XElement(Xhtml.head, new XElement(Xhtml.meta, new XAttribute((XName?)"charset", "UTF-8")), (settings.PageTitle != null) ? new XElement(Xhtml.title, new XText(settings.PageTitle)) : new XElement(Xhtml.title, new XText(string.Empty)), new XElement(Xhtml.meta, new XAttribute((XName?)"name", "Generator"), new XAttribute((XName?)"content", "PowerTools for Open XML"))), from e in element.Elements()
                                                                                                                                                                                                                                                                                                                                                                                                                                   select ConvertToHtmlTransform(wordDoc, settings, e, suppressTrailingWhiteSpace: false, currentMarginLeft));
            }

            if (element.Name == W.body)
            {
                return new XElement(Xhtml.body, CreateSectionDivs(wordDoc, settings, element));
            }

            if (element.Name == W.p)
            {
                return ProcessParagraph(wordDoc, settings, element, suppressTrailingWhiteSpace, currentMarginLeft);
            }

            if (element.Name == W.hyperlink && element.Attribute(R.id) != null)
            {
                try
                {
                    XElement xElement = new XElement(Xhtml.a, new XAttribute((XName?)"href", wordDoc.MainDocumentPart.HyperlinkRelationships.First((HyperlinkRelationship x) => x.Id == (string?)element.Attribute(R.id)).Uri), from run in element.Elements(W.r)
                                                                                                                                                                                                                                select ConvertRun(wordDoc, settings, run));
                    if (!xElement.Nodes().Any())
                    {
                        xElement.Add(new XText(""));
                    }

                    return xElement;
                }
                catch (UriFormatException)
                {
                    return from e in element.Elements()
                           select ConvertToHtmlTransform(wordDoc, settings, e, suppressTrailingWhiteSpace: false, currentMarginLeft);
                }
            }

            if (element.Name == W.hyperlink && element.Attribute(W.anchor) != null)
            {
                return ProcessHyperlinkToBookmark(wordDoc, settings, element);
            }

            if (element.Name == W.r)
            {
                return ConvertRun(wordDoc, settings, element);
            }

            if (element.Name == W.bookmarkStart)
            {
                return ProcessBookmarkStart(element);
            }

            if (element.Name == W.t)
            {
                return new XText(element.Value);
            }

            if (element.Name == W.sym)
            {
                int num = Convert.ToInt32((string?)element.Attribute(W._char), 16);
                return new XElement(Xhtml.span, new XEntity($"#{num}"));
            }

            if (element.Name == W.tab)
            {
                return ProcessTab(element);
            }

            if (element.Name == W.br || element.Name == W.cr)
            {
                return ProcessBreak(element);
            }

            if (element.Name == W.noBreakHyphen)
            {
                return new XText("-");
            }

            if (element.Name == W.tbl)
            {
                return ProcessTable(wordDoc, settings, element, currentMarginLeft);
            }

            if (element.Name == W.tr)
            {
                return ProcessTableRow(wordDoc, settings, element, currentMarginLeft);
            }

            if (element.Name == W.tc)
            {
                return ProcessTableCell(wordDoc, settings, element);
            }

            if (element.Name == W.drawing || element.Name == W.pict || element.Name == W._object)
            {
                return ProcessImage(wordDoc, element, settings.ImageHandler);
            }

            if (element.Name == W.sdt)
            {
                return ProcessContentControl(wordDoc, settings, element, currentMarginLeft);
            }

            if (element.Name == W.smartTag || element.Name == W.fldSimple)
            {
                return CreateBorderDivs(wordDoc, settings, element.Elements());
            }

            return null;
        }

        private static object ProcessHyperlinkToBookmark(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, XElement element)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            XElement xElement = new XElement(Xhtml.a, new XAttribute((XName?)"href", "#" + (string?)element.Attribute(W.anchor)), from run in element.Elements(W.r)
                                                                                                                                  select ConvertRun(wordDoc, settings, run));
            if (!xElement.Nodes().Any())
            {
                xElement.Add(new XText(""));
            }

            dictionary.Add("text-decoration", "none");
            xElement.AddAnnotation(dictionary);
            return xElement;
        }

        private static object ProcessBookmarkStart(XElement element)
        {
            string text = (string?)element.Attribute(W.name);
            if (text == null)
            {
                return null;
            }

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            XElement xElement = new XElement(Xhtml.a, new XAttribute((XName?)"id", text), new XText(""));
            if (!xElement.Nodes().Any())
            {
                xElement.Add(new XText(""));
            }

            dictionary.Add("text-decoration", "none");
            xElement.AddAnnotation(dictionary);
            return xElement;
        }

        private static object ProcessTab(XElement element)
        {
            XAttribute xAttribute = element.Attribute(PtOpenXml.TabWidth);
            if (xAttribute == null)
            {
                return null;
            }

            string text = (string?)element.Attribute(PtOpenXml.Leader);
            decimal num = (decimal)xAttribute;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            XElement xElement2;
            if (text != null)
            {
                string text2 = ".";
                switch (text)
                {
                    case "hyphen":
                        text2 = "-";
                        break;
                    case "dot":
                        text2 = ".";
                        break;
                    case "underscore":
                        text2 = "_";
                        break;
                }

                XElement xElement = element.Ancestors(W.r).First();
                XAttribute xAttribute2 = xElement.Attribute(PtOpenXml.pt + "FontName") ?? xElement.Ancestors(W.p).First().Attribute(PtOpenXml.pt + "FontName");
                int num2 = CalcWidthOfRunInTwips(new XElement(W.r, xAttribute2, xElement.Elements(W.rPr), new XElement(W.t, text2)));
                bool flag = false;
                if (num2 == 0)
                {
                    num2 = CalcWidthOfRunInTwips(new XElement(W.r, new XAttribute(PtOpenXml.FontName, "Arial"), xElement.Elements(W.rPr), new XElement(W.t, text2)));
                    flag = true;
                }

                if (num2 != 0)
                {
                    int num3 = (int)Math.Floor(num * 1440m / (decimal)num2);
                    if (num3 < 0)
                    {
                        num3 = 0;
                    }

                    xElement2 = new XElement(Xhtml.span, new XAttribute(XNamespace.Xml + "space", "preserve"), " " + "".PadRight(num3, text2[0]) + " ");
                    dictionary.Add("margin", "0 0 0 0");
                    dictionary.Add("padding", "0 0 0 0");
                    dictionary.Add("width", string.Format(NumberFormatInfo.InvariantInfo, "{0:0.00}in", num));
                    dictionary.Add("text-align", "center");
                    if (flag)
                    {
                        dictionary.Add("font-family", "Arial");
                    }
                }
                else
                {
                    xElement2 = new XElement(Xhtml.span, new XAttribute(XNamespace.Xml + "space", "preserve"), " ");
                    dictionary.Add("margin", "0 0 0 0");
                    dictionary.Add("padding", "0 0 0 0");
                    dictionary.Add("width", string.Format(NumberFormatInfo.InvariantInfo, "{0:0.00}in", num));
                    dictionary.Add("text-align", "center");
                    if (text == "underscore")
                    {
                        dictionary.Add("text-decoration", "underline");
                    }
                }
            }
            else
            {
                xElement2 = new XElement(Xhtml.span, new XEntity("#x00a0"));
                dictionary.Add("margin", string.Format(NumberFormatInfo.InvariantInfo, "0 0 0 {0:0.00}in", num));
                dictionary.Add("padding", "0 0 0 0");
            }

            xElement2.AddAnnotation(dictionary);
            return xElement2;
        }

        private static object ProcessBreak(XElement element)
        {
            XElement xElement = null;
            decimal? num = (decimal?)element.Attribute(PtOpenXml.TabWidth);
            if (num.HasValue)
            {
                xElement = new XElement(Xhtml.span);
                xElement.AddAnnotation(new Dictionary<string, string>
                {
                    {
                        "margin",
                        string.Format(NumberFormatInfo.InvariantInfo, "0 0 0 {0:0.00}in", num)
                    },
                    { "padding", "0 0 0 0" }
                });
            }

            XEntity xEntity = ((element.Ancestors(W.p).FirstOrDefault()?.Elements(W.pPr).Elements(W.bidi).Any((XElement b) => b.Attribute(W.val) == null || b.Attribute(W.val).ToBoolean() == true) ?? false) ? new XEntity("#x200f") : new XEntity("#x200e"));
            return new object[3]
            {
                new XElement(Xhtml.br),
                xEntity,
                xElement
            };
        }

        private static object ProcessContentControl(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, XElement element, decimal currentMarginLeft)
        {
            if (element.Ancestors().TakeWhile((XElement a) => a.Name != W.txbxContent).Any((XElement a) => a.Name == W.p))
            {
                return (from e in element.Elements(W.sdtContent).Elements()
                        select ConvertToHtmlTransform(wordDoc, settings, e, suppressTrailingWhiteSpace: false, currentMarginLeft)).ToList();
            }

            return CreateBorderDivs(wordDoc, settings, element.Elements(W.sdtContent).Elements());
        }

        private static object ProcessParagraph(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, XElement element, bool suppressTrailingWhiteSpace, decimal currentMarginLeft)
        {
            if (HasStyleSeparator(element.ElementsBeforeSelf(W.p).LastOrDefault()))
            {
                return null;
            }

            XName paragraphElementName = GetParagraphElementName(element, wordDoc);
            bool isBidi = IsBidi(element);
            XElement xElement = (XElement)ConvertParagraph(wordDoc, settings, element, paragraphElementName, suppressTrailingWhiteSpace, currentMarginLeft, isBidi);
            (from e in xElement.Elements(Xhtml.span)
             where e.IsEmpty
             select e).Remove();
            foreach (XElement item in xElement.Elements(Xhtml.span).ToList())
            {
                string value = item.Value;
                if (value.Length > 0 && (char.IsWhiteSpace(value[0]) || char.IsWhiteSpace(value[value.Length - 1])) && item.Attribute(XNamespace.Xml + "space") == null)
                {
                    item.Add(new XAttribute(XNamespace.Xml + "space", "preserve"));
                }
            }

            while (HasStyleSeparator(element))
            {
                element = element.ElementsAfterSelf(W.p).FirstOrDefault();
                if (element == null)
                {
                    break;
                }

                paragraphElementName = Xhtml.span;
                isBidi = IsBidi(element);
                XElement xElement2 = (XElement)ConvertParagraph(wordDoc, settings, element, paragraphElementName, suppressTrailingWhiteSpace, currentMarginLeft, isBidi);
                string value2 = xElement2.Value;
                if (value2.Length > 0 && (char.IsWhiteSpace(value2[0]) || char.IsWhiteSpace(value2[value2.Length - 1])) && xElement2.Attribute(XNamespace.Xml + "space") == null)
                {
                    xElement2.Add(new XAttribute(XNamespace.Xml + "space", "preserve"));
                }

                xElement.Add(xElement2);
            }

            return xElement;
        }

        private static object ProcessTable(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, XElement element, decimal currentMarginLeft)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.AddIfMissing("border-collapse", "collapse");
            dictionary.AddIfMissing("border", "none");
            XElement xElement = element.Elements(W.tblPr).Elements(W.bidiVisual).FirstOrDefault();
            XElement xElement2 = element.Elements(W.tblPr).Elements(W.tblW).FirstOrDefault();
            if (xElement2 != null)
            {
                string text = (string?)xElement2.Attribute(W.type);
                if (text != null && text == "pct")
                {
                    int num = (int)xElement2.Attribute(W._w);
                    dictionary.AddIfMissing("width", num / 50 + "%");
                }
            }

            XElement xElement3 = element.Elements(W.tblPr).Elements(W.tblInd).FirstOrDefault();
            if (xElement3 != null)
            {
                string text2 = (string?)xElement3.Attribute(W.type);
                if (text2 != null && text2 == "dxa")
                {
                    decimal? num2 = (decimal?)xElement3.Attribute(W._w);
                    if (num2.HasValue)
                    {
                        decimal? num3 = num2;
                        dictionary.AddIfMissing("margin-left", (num3.GetValueOrDefault() > default(decimal) && num3.HasValue) ? string.Format(NumberFormatInfo.InvariantInfo, "{0}pt", num2 / (decimal?)20m) : "0");
                    }
                }
            }

            XAttribute xAttribute = ((xElement != null) ? new XAttribute((XName?)"dir", "rtl") : new XAttribute((XName?)"dir", "ltr"));
            dictionary.AddIfMissing("margin-bottom", ".001pt");
            XElement xElement4 = new XElement(Xhtml.table, xAttribute, from e in element.Elements()
                                                                       select ConvertToHtmlTransform(wordDoc, settings, e, suppressTrailingWhiteSpace: false, currentMarginLeft));
            xElement4.AddAnnotation(dictionary);
            string text3 = ((string?)element.Elements(W.tblPr).Elements(W.jc).Attributes(W.val)
                .FirstOrDefault()) ?? "left";
            XAttribute xAttribute2 = null;
            XAttribute xAttribute3 = null;
            if (xElement != null)
            {
                xAttribute2 = new XAttribute((XName?)"dir", "rtl");
                switch (text3)
                {
                    case "left":
                        xAttribute3 = new XAttribute((XName?)"align", "right");
                        break;
                    case "right":
                        xAttribute3 = new XAttribute((XName?)"align", "left");
                        break;
                    case "center":
                        xAttribute3 = new XAttribute((XName?)"align", "center");
                        break;
                }
            }
            else
            {
                xAttribute3 = new XAttribute((XName?)"align", text3);
            }

            return new XElement(Xhtml.div, xAttribute2, xAttribute3, xElement4);
        }

        private static object ProcessTableCell(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, XElement element)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            XAttribute xAttribute = null;
            XAttribute xAttribute2 = null;
            XElement xElement = element.Element(W.tcPr);
            if (xElement != null)
            {
                if ((string?)xElement.Elements(W.vMerge).Attributes(W.val).FirstOrDefault() == "restart")
                {
                    int num = element.Parent!.ElementsBeforeSelf(W.tr).Count();
                    int count = element.ElementsBeforeSelf(W.tc).Count();
                    XElement parent = element.Parent!.Parent;
                    int num2 = 1;
                    num++;
                    while (true)
                    {
                        XElement xElement2 = parent.Elements(W.tr).Skip(num).FirstOrDefault();
                        if (xElement2 == null)
                        {
                            break;
                        }

                        XElement xElement3 = xElement2.Elements(W.tc).Skip(count).FirstOrDefault();
                        if (xElement3 == null || xElement3.Elements(W.tcPr).Elements(W.vMerge).FirstOrDefault() == null || (string?)xElement3.Elements(W.tcPr).Elements(W.vMerge).Attributes(W.val)
                            .FirstOrDefault() == "restart")
                        {
                            break;
                        }

                        num++;
                        num2++;
                    }

                    xAttribute2 = new XAttribute((XName?)"rowspan", num2);
                }

                if (xElement.Element(W.vMerge) != null && (string?)xElement.Elements(W.vMerge).Attributes(W.val).FirstOrDefault() != "restart")
                {
                    return null;
                }

                if (xElement.Element(W.vAlign) != null)
                {
                    switch ((string?)xElement.Elements(W.vAlign).Attributes(W.val).FirstOrDefault())
                    {
                        case "top":
                            dictionary.AddIfMissing("vertical-align", "top");
                            break;
                        case "center":
                            dictionary.AddIfMissing("vertical-align", "middle");
                            break;
                        case "bottom":
                            dictionary.AddIfMissing("vertical-align", "bottom");
                            break;
                        default:
                            dictionary.AddIfMissing("vertical-align", "middle");
                            break;
                    }
                }

                dictionary.AddIfMissing("vertical-align", "top");
                if ((string?)xElement.Elements(W.tcW).Attributes(W.type).FirstOrDefault() == "dxa")
                {
                    decimal num3 = (int)xElement.Elements(W.tcW).Attributes(W._w).FirstOrDefault();
                    dictionary.AddIfMissing("width", string.Format(NumberFormatInfo.InvariantInfo, "{0}pt", num3 / 20m));
                }

                if ((string?)xElement.Elements(W.tcW).Attributes(W.type).FirstOrDefault() == "pct")
                {
                    decimal num4 = (int)xElement.Elements(W.tcW).Attributes(W._w).FirstOrDefault();
                    dictionary.AddIfMissing("width", string.Format(NumberFormatInfo.InvariantInfo, "{0:0.0}%", num4 / 50m));
                }

                XElement? pBdr = xElement.Element(W.tcBorders);
                GenerateBorderStyle(pBdr, W.top, dictionary, BorderType.Cell);
                GenerateBorderStyle(pBdr, W.right, dictionary, BorderType.Cell);
                GenerateBorderStyle(pBdr, W.bottom, dictionary, BorderType.Cell);
                GenerateBorderStyle(pBdr, W.left, dictionary, BorderType.Cell);
                CreateStyleFromShd(dictionary, xElement.Element(W.shd));
                int? num5 = (from a in xElement.Elements(W.gridSpan).Attributes(W.val)
                             select (int?)a).FirstOrDefault();
                if (num5.HasValue)
                {
                    xAttribute = new XAttribute((XName?)"colspan", num5.Value);
                }
            }

            dictionary.AddIfMissing("padding-top", "0");
            dictionary.AddIfMissing("padding-bottom", "0");
            XElement xElement4 = new XElement(Xhtml.td, xAttribute2, xAttribute, CreateBorderDivs(wordDoc, settings, element.Elements()));
            xElement4.AddAnnotation(dictionary);
            return xElement4;
        }

        private static object ProcessTableRow(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, XElement element, decimal currentMarginLeft)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            int? num = (int?)element.Elements(W.trPr).Elements(W.trHeight).Attributes(W.val)
                .FirstOrDefault();
            if (num.HasValue)
            {
                dictionary.AddIfMissing("height", string.Format(NumberFormatInfo.InvariantInfo, "{0:0.00}in", (decimal)num.Value / 1440m));
            }

            XElement xElement = new XElement(Xhtml.tr, from e in element.Elements()
                                                       select ConvertToHtmlTransform(wordDoc, settings, e, suppressTrailingWhiteSpace: false, currentMarginLeft));
            if (dictionary.Any())
            {
                xElement.AddAnnotation(dictionary);
            }

            return xElement;
        }

        private static bool HasStyleSeparator(XElement element)
        {
            return element?.Elements(W.pPr).Elements(W.rPr).Any((XElement e) => GetBoolProp(e, W.specVanish)) ?? false;
        }

        private static bool IsBidi(XElement element)
        {
            return element.Elements(W.pPr).Elements(W.bidi).Any((XElement b) => b.Attribute(W.val) == null || b.Attribute(W.val).ToBoolean() == true);
        }

        private static XName GetParagraphElementName(XElement element, WordprocessingDocument wordDoc)
        {
            XName result = Xhtml.p;
            string text = (string?)element.Elements(W.pPr).Elements(W.pStyle).Attributes(W.val)
                .FirstOrDefault();
            if (text == null)
            {
                return result;
            }

            XElement style = GetStyle(text, wordDoc);
            if (style == null)
            {
                return result;
            }

            int? num = (int?)style.Elements(W.pPr).Elements(W.outlineLvl).Attributes(W.val)
                .FirstOrDefault();
            if (num.HasValue && num <= 5)
            {
                result = Xhtml.xhtml + $"h{num + 1}";
            }

            return result;
        }

        private static XElement GetStyle(string styleId, WordprocessingDocument wordDoc)
        {
            return wordDoc.MainDocumentPart.StyleDefinitionsPart?.GetXDocument().Root?.Elements(W.style).FirstOrDefault((XElement s) => (string?)s.Attribute(W.styleId) == styleId);
        }

        private static object CreateSectionDivs(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, XElement element)
        {
            return element.Elements().GroupAdjacent(delegate (XElement e)
            {
                SectionAnnotation sectionAnnotation2 = e.Annotation<SectionAnnotation>();
                return (sectionAnnotation2 == null) ? "" : sectionAnnotation2.SectionElement.ToString();
            }).Select(delegate (IGrouping<string, XElement> g)
            {
                SectionAnnotation sectionAnnotation = g.First().Annotation<SectionAnnotation>();
                XElement xElement = null;
                if (sectionAnnotation != null)
                {
                    xElement = sectionAnnotation.SectionElement.Elements(W.bidi).FirstOrDefault((XElement b) => b.Attribute(W.val) == null || b.Attribute(W.val).ToBoolean() == true);
                }

                return (sectionAnnotation == null || xElement == null) ? new XElement(Xhtml.div, CreateBorderDivs(wordDoc, settings, g)) : new XElement(Xhtml.div, new XAttribute((XName?)"dir", "rtl"), CreateBorderDivs(wordDoc, settings, g));
            });
        }

        private static object ConvertParagraph(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, XElement paragraph, XName elementName, bool suppressTrailingWhiteSpace, decimal currentMarginLeft, bool isBidi)
        {
            Dictionary<string, string> annotation = DefineParagraphStyle(paragraph, elementName, suppressTrailingWhiteSpace, currentMarginLeft, isBidi);
            XAttribute xAttribute = (isBidi ? new XAttribute((XName?)"dir", "rtl") : new XAttribute((XName?)"dir", "ltr"));
            XEntity xEntity = (isBidi ? new XEntity("#x200f") : null);
            XElement firstTabRun = paragraph.Elements(W.r).FirstOrDefault((XElement run) => run.Elements(W.tab).Any());
            List<XElement> list = ((firstTabRun != null) ? (from e in paragraph.Elements(W.r).TakeWhile((XElement e) => e != firstTabRun)
                                                            where e.Elements().Any((XElement c) => c.Attributes(PtOpenXml.TabWidth).Any())
                                                            select e).ToList() : Enumerable.Empty<XElement>().ToList());
            if ((from e in list.Elements(W.r).Elements(W.instrText)
                 select e.Value).Any((string value) => value?.TrimStart(Array.Empty<char>()).ToUpper().StartsWith("HYPERLINK") ?? false))
            {
                XElement xElement = new XElement(elementName, xAttribute, xEntity, ConvertContentThatCanContainFields(wordDoc, settings, paragraph.Elements()));
                xElement.AddAnnotation(annotation);
                return xElement;
            }

            List<object> list2 = TransformElementsPrecedingTab(wordDoc, settings, list, firstTabRun);
            IEnumerable<XElement> elements = ((firstTabRun != null) ? paragraph.Elements().SkipWhile((XElement e) => e != firstTabRun).Skip(1) : paragraph.Elements());
            XElement xElement2 = new XElement(elementName, xAttribute, xEntity, list2, ConvertContentThatCanContainFields(wordDoc, settings, elements));
            xElement2.AddAnnotation(annotation);
            return xElement2;
        }

        private static List<object> TransformElementsPrecedingTab(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, List<XElement> elementsPrecedingTab, XElement firstTabRun)
        {
            decimal num = ((firstTabRun == null) ? 0m : (((decimal?)firstTabRun.Elements(W.tab).Attributes(PtOpenXml.TabWidth).FirstOrDefault()) ?? 0m));
            decimal num2 = (from c in elementsPrecedingTab.Elements()
                            where c.Attributes(PtOpenXml.TabWidth).Any()
                            select c into e
                            select (decimal)e.Attribute(PtOpenXml.TabWidth)).Sum() + num;
            List<object> list = elementsPrecedingTab.Select((XElement e) => ConvertToHtmlTransform(wordDoc, settings, e, suppressTrailingWhiteSpace: false, 0m)).ToList();
            if (list.Count > 1)
            {
                XElement xElement = new XElement(Xhtml.span, list);
                Dictionary<string, string> annotation = new Dictionary<string, string>
                {
                    { "display", "inline-block" },
                    { "text-indent", "0" },
                    {
                        "width",
                        string.Format(NumberFormatInfo.InvariantInfo, "{0:0.000}in", num2)
                    }
                };
                xElement.AddAnnotation(annotation);
            }
            else if (list.Count == 1)
            {
                XElement xElement2 = list.First() as XElement;
                if (xElement2 != null)
                {
                    Dictionary<string, string>? style = xElement2.Annotation<Dictionary<string, string>>();
                    style.AddIfMissing("display", "inline-block");
                    style.AddIfMissing("text-indent", "0");
                    style.AddIfMissing("width", string.Format(NumberFormatInfo.InvariantInfo, "{0:0.000}in", num2));
                }
            }

            return list;
        }

        private static Dictionary<string, string> DefineParagraphStyle(XElement paragraph, XName elementName, bool suppressTrailingWhiteSpace, decimal currentMarginLeft, bool isBidi)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string text = (string?)paragraph.Attribute(PtOpenXml.StyleName);
            if (text != null)
            {
                dictionary.Add("PtStyleName", text);
            }

            XElement xElement = paragraph.Element(W.pPr);
            if (xElement == null)
            {
                return dictionary;
            }

            CreateStyleFromSpacing(dictionary, xElement.Element(W.spacing), elementName, suppressTrailingWhiteSpace);
            CreateStyleFromInd(dictionary, xElement.Element(W.ind), elementName, currentMarginLeft, isBidi);
            CreateStyleFromJc(dictionary, xElement.Element(W.jc), isBidi);
            CreateStyleFromShd(dictionary, xElement.Element(W.shd));
            string text2 = (string?)paragraph.Attributes(PtOpenXml.FontName).FirstOrDefault();
            if (text2 != null)
            {
                CreateFontCssProperty(text2, dictionary);
            }

            DefineFontSize(dictionary, paragraph);
            DefineLineHeight(dictionary, paragraph);
            CreateStyleFromTextAlignment(dictionary, xElement.Element(W.textAlignment));
            dictionary.AddIfMissing("margin-top", "0");
            dictionary.AddIfMissing("margin-left", "0");
            dictionary.AddIfMissing("margin-right", "0");
            dictionary.AddIfMissing("margin-bottom", ".001pt");
            return dictionary;
        }

        private static void CreateStyleFromInd(Dictionary<string, string> style, XElement ind, XName elementName, decimal currentMarginLeft, bool isBidi)
        {
            if (ind != null)
            {
                decimal? num = (decimal?)ind.Attribute(W.left);
                if (num.HasValue && elementName != Xhtml.span)
                {
                    decimal num2 = num.Value / 1440m - currentMarginLeft;
                    style.AddIfMissing(isBidi ? "margin-right" : "margin-left", (num2 > 0m) ? string.Format(NumberFormatInfo.InvariantInfo, "{0:0.00}in", num2) : "0");
                }

                decimal? num3 = (decimal?)ind.Attribute(W.right);
                if (num3.HasValue)
                {
                    decimal num4 = num3.Value / 1440m;
                    style.AddIfMissing(isBidi ? "margin-left" : "margin-right", (num4 > 0m) ? string.Format(NumberFormatInfo.InvariantInfo, "{0:0.00}in", num4) : "0");
                }

                decimal? num5 = (decimal?)ind.Attribute(W.firstLine);
                if (num5.HasValue && elementName != Xhtml.span)
                {
                    decimal num6 = num5.Value / 1440m;
                    style.AddIfMissing("text-indent", string.Format(NumberFormatInfo.InvariantInfo, "{0:0.00}in", num6));
                }

                decimal? num7 = (decimal?)ind.Attribute(W.hanging);
                if (num7.HasValue && elementName != Xhtml.span)
                {
                    decimal num8 = (-num7).Value / 1440m;
                    style.AddIfMissing("text-indent", string.Format(NumberFormatInfo.InvariantInfo, "{0:0.00}in", num8));
                }
            }
        }

        private static void CreateStyleFromJc(Dictionary<string, string> style, XElement jc, bool isBidi)
        {
            if (jc != null)
            {
                switch (((string?)jc.Attributes(W.val).FirstOrDefault()) ?? "left")
                {
                    case "left":
                        style.AddIfMissing("text-align", isBidi ? "right" : "left");
                        break;
                    case "right":
                        style.AddIfMissing("text-align", isBidi ? "left" : "right");
                        break;
                    case "center":
                        style.AddIfMissing("text-align", "center");
                        break;
                    case "both":
                        style.AddIfMissing("text-align", "justify");
                        break;
                }
            }
        }

        private static void CreateStyleFromSpacing(Dictionary<string, string> style, XElement spacing, XName elementName, bool suppressTrailingWhiteSpace)
        {
            if (spacing == null)
            {
                return;
            }

            decimal? num = (decimal?)spacing.Attribute(W.before);
            if (num.HasValue && elementName != Xhtml.span)
            {
                decimal? num2 = num;
                style.AddIfMissing("margin-top", (num2.GetValueOrDefault() > default(decimal) && num2.HasValue) ? string.Format(NumberFormatInfo.InvariantInfo, "{0}pt", num / (decimal?)20.0m) : "0");
            }

            string? text = (string?)spacing.Attribute(W.lineRule);
            if (text == "auto")
            {
                decimal num3 = (decimal)spacing.Attribute(W.line);
                if (num3 != 240m)
                {
                    style.Add("line-height", string.Format(arg0: num3 / 240m * 100m, provider: NumberFormatInfo.InvariantInfo, format: "{0:0.0}%"));
                }
            }

            if (text == "exact")
            {
                style.Add("line-height", string.Format(arg0: (decimal)spacing.Attribute(W.line) / 20m, provider: NumberFormatInfo.InvariantInfo, format: "{0:0.0}pt"));
            }

            if (text == "atLeast")
            {
                decimal num4 = (decimal)spacing.Attribute(W.line) / 20m;
                if (num4 >= 14m)
                {
                    style.Add("line-height", string.Format(NumberFormatInfo.InvariantInfo, "{0:0.0}pt", num4));
                }
            }

            decimal? num5 = (suppressTrailingWhiteSpace ? new decimal?(0m) : ((decimal?)spacing.Attribute(W.after)));
            if (num5.HasValue)
            {
                decimal? num2 = num5;
                style.AddIfMissing("margin-bottom", (num2.GetValueOrDefault() > default(decimal) && num2.HasValue) ? string.Format(NumberFormatInfo.InvariantInfo, "{0}pt", num5 / (decimal?)20.0m) : "0");
            }
        }

        private static void CreateStyleFromTextAlignment(Dictionary<string, string> style, XElement textAlignment)
        {
            if (textAlignment != null)
            {
                switch ((string?)textAlignment.Attributes(W.val).FirstOrDefault())
                {
                    case "auto":
                        break;
                    case "top":
                        style.AddIfMissing("vertical-align", "top");
                        break;
                    case "center":
                        style.AddIfMissing("vertical-align", "middle");
                        break;
                    case "baseline":
                        style.AddIfMissing("vertical-align", "baseline");
                        break;
                    case "bottom":
                        style.AddIfMissing("vertical-align", "bottom");
                        break;
                }
            }
        }

        private static void DefineFontSize(Dictionary<string, string> style, XElement paragraph)
        {
            decimal? num = (from e in paragraph.DescendantsTrimmed(W.txbxContent)
                            where e.Name == W.r
                            select e into r
                            select GetFontSize(r)).Max();
            if (num.HasValue)
            {
                style.AddIfMissing("font-size", string.Format(NumberFormatInfo.InvariantInfo, "{0}pt", num / (decimal?)2.0m));
            }
        }

        private static void DefineLineHeight(Dictionary<string, string> style, XElement paragraph)
        {
            if ((from e in paragraph.DescendantsTrimmed(W.txbxContent)
                 where e.Name == W.r
                 select e into run
                 select (string?)run.Attribute(PtOpenXml.LanguageType)).All((string lt) => lt != "bidi"))
            {
                style.AddIfMissing("line-height", "108%");
            }
        }

        private static object ConvertRun(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, XElement run)
        {
            XElement xElement = run.Element(W.rPr);
            if (xElement == null)
            {
                return from e in run.Elements()
                       select ConvertToHtmlTransform(wordDoc, settings, e, suppressTrailingWhiteSpace: false, 0m);
            }

            if (xElement.Element(W.webHidden) != null)
            {
                return null;
            }

            Dictionary<string, string> dictionary = DefineRunStyle(run);
            object obj = from e in run.Elements()
                         select ConvertToHtmlTransform(wordDoc, settings, e, suppressTrailingWhiteSpace: false, 0m);
            if (xElement.Element(W.vertAlign) != null)
            {
                XElement xElement2 = null;
                string text = (string?)xElement.Elements(W.vertAlign).Attributes(W.val).FirstOrDefault();
                if (!(text == "superscript"))
                {
                    if (text == "subscript")
                    {
                        xElement2 = new XElement(Xhtml.sub, obj);
                    }
                }
                else
                {
                    xElement2 = new XElement(Xhtml.sup, obj);
                }

                if (xElement2 != null && xElement2.Nodes().Any())
                {
                    obj = xElement2;
                }
            }

            XAttribute langAttribute = GetLangAttribute(run);
            DetermineRunMarks(run, xElement, dictionary, out var runStartMark, out var runEndMark);
            if (dictionary.Any() || langAttribute != null || runStartMark != null)
            {
                dictionary.AddIfMissing("margin", "0");
                dictionary.AddIfMissing("padding", "0");
                XElement xElement3 = new XElement(Xhtml.span, langAttribute, runStartMark, obj, runEndMark);
                xElement3.AddAnnotation(dictionary);
                obj = xElement3;
            }

            return obj;
        }

        private static Dictionary<string, string> DefineRunStyle(XElement run)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            XElement xElement = run.Elements(W.rPr).First();
            string text = (string?)run.Attribute(PtOpenXml.StyleName);
            if (text != null)
            {
                dictionary.Add("PtStyleName", text);
            }

            if (xElement.Element(W.bdr) != null && (string?)xElement.Elements(W.bdr).Attributes(W.val).FirstOrDefault() != "none")
            {
                dictionary.AddIfMissing("border", "solid windowtext 1.0pt");
                dictionary.AddIfMissing("padding", "0");
            }

            string text2 = (string?)xElement.Elements(W.color).Attributes(W.val).FirstOrDefault();
            if (text2 != null)
            {
                CreateColorProperty("color", text2, dictionary);
            }

            string text3 = (string?)xElement.Elements(W.highlight).Attributes(W.val).FirstOrDefault();
            if (text3 != null)
            {
                CreateColorProperty("background", text3, dictionary);
            }

            string text4 = (string?)xElement.Elements(W.shd).Attributes(W.fill).FirstOrDefault();
            if (text4 != null)
            {
                CreateColorProperty("background", text4, dictionary);
            }

            XElement xElement2 = run.Element(W.sym);
            string text5 = ((xElement2 != null) ? ((string?)xElement2.Attributes(W.font).FirstOrDefault()) : ((string?)run.Attributes(PtOpenXml.FontName).FirstOrDefault()));
            if (text5 != null)
            {
                CreateFontCssProperty(text5, dictionary);
            }

            decimal? fontSize = GetFontSize((string?)run.Attribute(PtOpenXml.LanguageType), xElement);
            if (fontSize.HasValue)
            {
                dictionary.AddIfMissing("font-size", string.Format(NumberFormatInfo.InvariantInfo, "{0}pt", fontSize / (decimal?)2.0m));
            }

            if (GetBoolProp(xElement, W.caps))
            {
                dictionary.AddIfMissing("text-transform", "uppercase");
            }

            if (GetBoolProp(xElement, W.smallCaps))
            {
                dictionary.AddIfMissing("font-variant", "small-caps");
            }

            decimal? num = (decimal?)xElement.Elements(W.spacing).Attributes(W.val).FirstOrDefault();
            if (num.HasValue)
            {
                decimal? num2 = num;
                dictionary.AddIfMissing("letter-spacing", (num2.GetValueOrDefault() > default(decimal) && num2.HasValue) ? string.Format(NumberFormatInfo.InvariantInfo, "{0}pt", num / (decimal?)20) : "0");
            }

            decimal? num3 = (decimal?)xElement.Elements(W.position).Attributes(W.val).FirstOrDefault();
            if (num3.HasValue)
            {
                dictionary.AddIfMissing("position", "relative");
                dictionary.AddIfMissing("top", string.Format(NumberFormatInfo.InvariantInfo, "{0}pt", -(num3 / (decimal?)2)));
            }

            if (GetBoolProp(xElement, W.vanish) && !GetBoolProp(xElement, W.specVanish))
            {
                dictionary.AddIfMissing("display", "none");
            }

            if (xElement.Element(W.u) != null && (string?)xElement.Elements(W.u).Attributes(W.val).FirstOrDefault() != "none")
            {
                dictionary.AddIfMissing("text-decoration", "underline");
            }

            dictionary.AddIfMissing("font-style", GetBoolProp(xElement, W.i) ? "italic" : "normal");
            dictionary.AddIfMissing("font-weight", GetBoolProp(xElement, W.b) ? "bold" : "normal");
            if (GetBoolProp(xElement, W.strike) || GetBoolProp(xElement, W.dstrike))
            {
                dictionary.AddIfMissing("text-decoration", "line-through");
            }

            return dictionary;
        }

        private static decimal? GetFontSize(XElement e)
        {
            string languageType = (string?)e.Attribute(PtOpenXml.LanguageType);
            if (e.Name == W.p)
            {
                return GetFontSize(languageType, e.Elements(W.pPr).Elements(W.rPr).FirstOrDefault());
            }

            if (e.Name == W.r)
            {
                return GetFontSize(languageType, e.Element(W.rPr));
            }

            return null;
        }

        private static decimal? GetFontSize(string languageType, XElement rPr)
        {
            if (rPr == null)
            {
                return null;
            }

            if (!(languageType == "bidi"))
            {
                return (decimal?)rPr.Elements(W.sz).Attributes(W.val).FirstOrDefault();
            }

            return (decimal?)rPr.Elements(W.szCs).Attributes(W.val).FirstOrDefault();
        }

        private static void DetermineRunMarks(XElement run, XElement rPr, Dictionary<string, string> style, out XEntity runStartMark, out XEntity runEndMark)
        {
            runStartMark = null;
            runEndMark = null;
            if (run.Element(W.t) == null)
            {
                return;
            }

            bool flag = true;
            if (style.ContainsKey("font-family") && style["font-family"].ToLower() == "symbol")
            {
                flag = false;
            }

            if (flag)
            {
                if (rPr.Element(W.rtl) != null)
                {
                    runStartMark = new XEntity("#x200f");
                    runEndMark = new XEntity("#x200f");
                }
                else if (run.Ancestors(W.p).First().Elements(W.pPr)
                    .Elements(W.bidi)
                    .Any((XElement b) => b.Attribute(W.val) == null || b.Attribute(W.val).ToBoolean() == true))
                {
                    runStartMark = new XEntity("#x200e");
                    runEndMark = new XEntity("#x200e");
                }
            }
        }

        private static XAttribute GetLangAttribute(XElement run)
        {
            XElement xElement = run.Elements(W.rPr).First();
            string text = (string?)run.Attribute(PtOpenXml.LanguageType);
            string text2 = null;
            switch (text)
            {
                case "western":
                    text2 = (string?)xElement.Elements(W.lang).Attributes(W.val).FirstOrDefault();
                    break;
                case "bidi":
                    text2 = (string?)xElement.Elements(W.lang).Attributes(W.bidi).FirstOrDefault();
                    break;
                case "eastAsia":
                    text2 = (string?)xElement.Elements(W.lang).Attributes(W.eastAsia).FirstOrDefault();
                    break;
            }

            if (text2 == null)
            {
                text2 = "en-US";
            }

            if (!(text2 != "en-US"))
            {
                return null;
            }

            return new XAttribute((XName?)"lang", text2);
        }

        private static void AdjustTableBorders(WordprocessingDocument wordDoc)
        {
            foreach (XElement item in wordDoc.MainDocumentPart.GetXDocument().Descendants(W.tbl))
            {
                AdjustTableBorders(item);
            }

            wordDoc.MainDocumentPart.PutXDocument();
        }

        private static void AdjustTableBorders(XElement tbl)
        {
            XElement[][] array = (from r in tbl.Elements(W.tr)
                                  select r.Elements(W.tc).SelectMany((XElement c) => Enumerable.Repeat(c, ((int?)c.Elements(W.tcPr).Elements(W.gridSpan).Attributes(W.val)
                                      .FirstOrDefault()) ?? 1)).ToArray()).ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    XElement thisCell = array[i][j];
                    FixTopBorder(array, thisCell, j, i);
                    FixLeftBorder(array, thisCell, j, i);
                    FixBottomBorder(array, thisCell, j, i);
                    FixRightBorder(array, thisCell, j, i);
                }
            }
        }

        private static void FixTopBorder(XElement[][] ta, XElement thisCell, int x, int y)
        {
            if (y <= 0)
            {
                return;
            }

            XElement[] array = ta[y - 1];
            if (x < array.Length - 1)
            {
                XElement xElement = ta[y - 1][x];
                if (xElement != null && thisCell.Elements(W.tcPr).Elements(W.tcBorders).FirstOrDefault() != null && xElement.Elements(W.tcPr).Elements(W.tcBorders).FirstOrDefault() != null)
                {
                    ResolveCellBorder(thisCell.Elements(W.tcPr).Elements(W.tcBorders).Elements(W.top)
                        .FirstOrDefault(), xElement.Elements(W.tcPr).Elements(W.tcBorders).Elements(W.bottom)
                        .FirstOrDefault());
                }
            }
        }

        private static void FixLeftBorder(XElement[][] ta, XElement thisCell, int x, int y)
        {
            if (x > 0)
            {
                XElement xElement = ta[y][x - 1];
                if (xElement != null && thisCell.Elements(W.tcPr).Elements(W.tcBorders).FirstOrDefault() != null && xElement.Elements(W.tcPr).Elements(W.tcBorders).FirstOrDefault() != null)
                {
                    ResolveCellBorder(thisCell.Elements(W.tcPr).Elements(W.tcBorders).Elements(W.left)
                        .FirstOrDefault(), xElement.Elements(W.tcPr).Elements(W.tcBorders).Elements(W.right)
                        .FirstOrDefault());
                }
            }
        }

        private static void FixBottomBorder(XElement[][] ta, XElement thisCell, int x, int y)
        {
            if (y >= ta.Length - 1)
            {
                return;
            }

            XElement[] array = ta[y + 1];
            if (x < array.Length - 1)
            {
                XElement xElement = ta[y + 1][x];
                if (xElement != null && thisCell.Elements(W.tcPr).Elements(W.tcBorders).FirstOrDefault() != null && xElement.Elements(W.tcPr).Elements(W.tcBorders).FirstOrDefault() != null)
                {
                    ResolveCellBorder(thisCell.Elements(W.tcPr).Elements(W.tcBorders).Elements(W.bottom)
                        .FirstOrDefault(), xElement.Elements(W.tcPr).Elements(W.tcBorders).Elements(W.top)
                        .FirstOrDefault());
                }
            }
        }

        private static void FixRightBorder(XElement[][] ta, XElement thisCell, int x, int y)
        {
            if (x < ta[y].Length - 1)
            {
                XElement xElement = ta[y][x + 1];
                if (xElement != null && thisCell.Elements(W.tcPr).Elements(W.tcBorders).FirstOrDefault() != null && xElement.Elements(W.tcPr).Elements(W.tcBorders).FirstOrDefault() != null)
                {
                    ResolveCellBorder(thisCell.Elements(W.tcPr).Elements(W.tcBorders).Elements(W.right)
                        .FirstOrDefault(), xElement.Elements(W.tcPr).Elements(W.tcBorders).Elements(W.left)
                        .FirstOrDefault());
                }
            }
        }

        private static void ResolveCellBorder(XElement border1, XElement border2)
        {
            if (border1 == null || border2 == null || (string?)border1.Attribute(W.val) == "nil" || (string?)border2.Attribute(W.val) == "nil" || (string?)border1.Attribute(W.sz) == "nil" || (string?)border2.Attribute(W.sz) == "nil")
            {
                return;
            }

            string key = (string?)border1.Attribute(W.val);
            int num = 1;
            if (BorderNumber.ContainsKey(key))
            {
                num = BorderNumber[key];
            }

            string key2 = (string?)border2.Attribute(W.val);
            int num2 = 1;
            if (BorderNumber.ContainsKey(key2))
            {
                num2 = BorderNumber[key2];
            }

            if (num != num2)
            {
                if (num < num2)
                {
                    BorderOverride(border2, border1);
                }
                else
                {
                    BorderOverride(border1, border2);
                }
            }

            if ((decimal)border1.Attribute(W.sz) > (decimal)border2.Attribute(W.sz))
            {
                BorderOverride(border1, border2);
                return;
            }

            if ((decimal)border1.Attribute(W.sz) < (decimal)border2.Attribute(W.sz))
            {
                BorderOverride(border2, border1);
                return;
            }

            string key3 = (string?)border1.Attribute(W.val);
            string key4 = (string?)border2.Attribute(W.val);
            if (BorderTypePriority.ContainsKey(key3) && BorderTypePriority.ContainsKey(key4))
            {
                int num3 = BorderTypePriority[key3];
                int num4 = BorderTypePriority[key4];
                if (num3 < num4)
                {
                    BorderOverride(border2, border1);
                    return;
                }

                if (num4 < num3)
                {
                    BorderOverride(border1, border2);
                    return;
                }
            }

            string text = (string?)border1.Attribute(W.color);
            if (text == "auto")
            {
                text = "000000";
            }

            string text2 = (string?)border2.Attribute(W.color);
            if (text2 == "auto")
            {
                text2 = "000000";
            }

            if (text == null || text2 == null || !(text != text2))
            {
                return;
            }

            try
            {
                int num5 = Convert.ToInt32(text, 16);
                int num6 = Convert.ToInt32(text2, 16);
                if (num5 < num6)
                {
                    BorderOverride(border1, border2);
                }
                else if (num6 < num5)
                {
                    BorderOverride(border2, border1);
                }
            }
            catch (Exception)
            {
            }
        }

        private static void BorderOverride(XElement fromBorder, XElement toBorder)
        {
            toBorder.Attribute(W.val)!.Value = fromBorder.Attribute(W.val)!.Value;
            if (fromBorder.Attribute(W.color) != null)
            {
                toBorder.SetAttributeValue(W.color, fromBorder.Attribute(W.color)!.Value);
            }

            if (fromBorder.Attribute(W.sz) != null)
            {
                toBorder.SetAttributeValue(W.sz, fromBorder.Attribute(W.sz)!.Value);
            }

            if (fromBorder.Attribute(W.themeColor) != null)
            {
                toBorder.SetAttributeValue(W.themeColor, fromBorder.Attribute(W.themeColor)!.Value);
            }

            if (fromBorder.Attribute(W.themeTint) != null)
            {
                toBorder.SetAttributeValue(W.themeTint, fromBorder.Attribute(W.themeTint)!.Value);
            }
        }

        private static void CalculateSpanWidthForTabs(WordprocessingDocument wordDoc)
        {
            int defaultTabStop = ((int?)wordDoc.MainDocumentPart.DocumentSettingsPart.GetXDocument().Descendants(W.defaultTabStop).Attributes(W.val)
                .FirstOrDefault()) ?? 720;
            XElement root = wordDoc.MainDocumentPart.GetXDocument().Root;
            if (root != null)
            {
                XElement content = (XElement)CalculateSpanWidthTransform(root, defaultTabStop);
                root.ReplaceWith(content);
                wordDoc.MainDocumentPart.PutXDocument();
            }
        }

        private static object CalculateSpanWidthTransform(XNode node, int defaultTabStop)
        {
            XElement xElement = node as XElement;
            if (xElement == null)
            {
                return node;
            }

            if (xElement.Name != W.p || !(from d in xElement.DescendantsTrimmed(W.txbxContent)
                                          where d.Name == W.r
                                          select d).Elements(W.tab).Any())
            {
                return new XElement(xElement.Name, xElement.Attributes(), from n in xElement.Nodes()
                                                                          select CalculateSpanWidthTransform(n, defaultTabStop));
            }

            XElement xElement2 = new XElement(xElement);
            int num = 0;
            int num2 = 0;
            XElement xElement3 = xElement2.Elements(W.pPr).Elements(W.ind).FirstOrDefault();
            if (xElement3 != null)
            {
                int? num3 = (int?)xElement3.Attribute(W.left);
                if (num3.HasValue)
                {
                    num = num3.Value;
                }

                int num4 = 0;
                int? num5 = (int?)xElement3.Attribute(W.firstLine);
                if (num5.HasValue)
                {
                    num4 = num5.Value;
                }

                int? num6 = (int?)xElement3.Attribute(W.hanging);
                if (num6.HasValue)
                {
                    num4 = -num6.Value;
                }

                num2 = num + num4;
            }

            XElement xElement4 = xElement2.Elements(W.pPr).Elements(W.tabs).FirstOrDefault();
            if (xElement4 == null)
            {
                if (num == 0)
                {
                    xElement4 = new XElement(W.tabs, from r in Enumerable.Range(1, 100)
                                                     select new XElement(W.tab, new XAttribute(W.val, "left"), new XAttribute(W.pos, r * defaultTabStop)));
                }
                else
                {
                    xElement4 = new XElement(W.tabs, new XElement(W.tab, new XAttribute(W.val, "left"), new XAttribute(W.pos, num)));
                    xElement4 = AddDefaultTabsAfterLastTab(xElement4, defaultTabStop);
                }
            }
            else
            {
                if (num != 0)
                {
                    xElement4.Add(new XElement(W.tab, new XAttribute(W.val, "left"), new XAttribute(W.pos, num)));
                }

                xElement4 = AddDefaultTabsAfterLastTab(xElement4, defaultTabStop);
            }

            int num7 = num2;
            XElement[] array = xElement.DescendantsTrimmed((XElement z) => z.Name == W.txbxContent || z.Name == W.pPr || z.Name == W.rPr).ToArray();
            int num8 = 0;
            while (true)
            {
                XElement xElement5 = array[num8];
                if (xElement5.Name == W.br)
                {
                    num7 = num;
                    xElement5.Add(new XAttribute(PtOpenXml.TabWidth, string.Format(NumberFormatInfo.InvariantInfo, "{0:0.000}", (decimal)num2 / 1440m)));
                    num8++;
                    if (num8 >= array.Length)
                    {
                        break;
                    }
                }

                XElement parent;
                XAttribute xAttribute;
                XElement xElement6;
                string text;
                if (xElement5.Name == W.tab)
                {
                    parent = xElement5.Parent;
                    xAttribute = parent.Attribute(PtOpenXml.pt + "FontName") ?? parent.Ancestors(W.p).First().Attribute(PtOpenXml.pt + "FontName");
                    int testAmount = num7;
                    xElement6 = xElement4.Elements(W.tab).FirstOrDefault((XElement t) => (int)t.Attribute(W.pos) > testAmount);
                    if (xElement6 == null)
                    {
                        if (xElement5.Attribute(PtOpenXml.TabWidth) == null)
                        {
                            xElement5.Add(new XAttribute(PtOpenXml.TabWidth, 720m));
                        }

                        break;
                    }

                    text = (string?)xElement6.Attribute(W.val);
                    switch (text)
                    {
                        case "right":
                        case "end":
                            break;
                        case "decimal":
                            goto IL_054e;
                        default:
                            goto IL_0834;
                    }

                    List<XElement> source = array.Skip(num8 + 1).TakeWhile((XElement z) => z.Name != W.tab && z.Name != W.br && z.Name != W.cr).ToList();
                    string content = (from z in source
                                      where z.Name == W.t
                                      select z into t
                                      select (string?)t).StringConcatenate();
                    int num9 = CalcWidthOfRunInTwips(new XElement(W.r, xAttribute, parent.Elements(W.rPr), new XElement(W.t, content)));
                    int num10 = (int)xElement6.Attribute(W.pos) - num9 - num7;
                    if (num10 < 0)
                    {
                        num10 = 0;
                    }

                    xElement5.Add(new XAttribute(PtOpenXml.TabWidth, string.Format(NumberFormatInfo.InvariantInfo, "{0:0.000}", (decimal)num10 / 1440m)), GetLeader(xElement6));
                    num7 = Math.Max((int)xElement6.Attribute(W.pos), num7 + num9);
                    XElement xElement7 = source.LastOrDefault();
                    if (xElement7 == null)
                    {
                        break;
                    }

                    num8 = Array.IndexOf(array, xElement7) + 1;
                    if (num8 >= array.Length)
                    {
                        break;
                    }

                    continue;
                }

                goto IL_0a8c;
            IL_0a8c:
                if (xElement5.Name == W.t)
                {
                    xElement5.Add(new XAttribute(PtOpenXml.TabWidth, string.Format(NumberFormatInfo.InvariantInfo, "{0:0.000}", 0m)));
                    num7 = num7;
                    num8++;
                    if (num8 >= array.Length)
                    {
                        break;
                    }
                }
                else
                {
                    num8++;
                    if (num8 >= array.Length)
                    {
                        break;
                    }
                }

                continue;
            IL_054e:
                List<XElement> source2 = array.Skip(num8 + 1).TakeWhile((XElement z) => z.Name != W.tab && z.Name != W.br && z.Name != W.cr).ToList();
                string text2 = (from z in source2
                                where z.Name == W.t
                                select z into t
                                select (string?)t).StringConcatenate();
                if (text2.Contains("."))
                {
                    string content2 = text2.Split(new char[1] { '.' })[0];
                    int num11 = CalcWidthOfRunInTwips(new XElement(W.r, xAttribute, parent.Elements(W.rPr), new XElement(W.t, content2)));
                    int num12 = (int)xElement6.Attribute(W.pos) - num11 - num7;
                    if (num12 < 0)
                    {
                        num12 = 0;
                    }

                    xElement5.Add(new XAttribute(PtOpenXml.TabWidth, string.Format(NumberFormatInfo.InvariantInfo, "{0:0.000}", (decimal)num12 / 1440m)), GetLeader(xElement6));
                    string content3 = text2.Substring(text2.IndexOf('.'));
                    int num13 = CalcWidthOfRunInTwips(new XElement(W.r, xAttribute, parent.Elements(W.rPr), new XElement(W.t, content3)));
                    num7 = Math.Max((int)xElement6.Attribute(W.pos) + num13, num7 + num11 + num13);
                    XElement xElement8 = source2.LastOrDefault();
                    if (xElement8 == null)
                    {
                        break;
                    }

                    num8 = Array.IndexOf(array, xElement8) + 1;
                    if (num8 >= array.Length)
                    {
                        break;
                    }
                }
                else
                {
                    int num14 = CalcWidthOfRunInTwips(new XElement(W.r, xAttribute, parent.Elements(W.rPr), new XElement(W.t, text2)));
                    int num15 = (int)xElement6.Attribute(W.pos) - num14 - num7;
                    if (num15 < 0)
                    {
                        num15 = 0;
                    }

                    xElement5.Add(new XAttribute(PtOpenXml.TabWidth, string.Format(NumberFormatInfo.InvariantInfo, "{0:0.000}", (decimal)num15 / 1440m)), GetLeader(xElement6));
                    num7 = Math.Max((int)xElement6.Attribute(W.pos), num7 + num14);
                    XElement xElement9 = source2.LastOrDefault();
                    if (xElement9 == null)
                    {
                        break;
                    }

                    num8 = Array.IndexOf(array, xElement9) + 1;
                    if (num8 >= array.Length)
                    {
                        break;
                    }
                }

                continue;
            IL_0834:
                if ((string?)xElement6.Attribute(W.val) == "center")
                {
                    List<XElement> source3 = array.Skip(num8 + 1).TakeWhile((XElement z) => z.Name != W.tab && z.Name != W.br && z.Name != W.cr).ToList();
                    string content4 = (from z in source3
                                       where z.Name == W.t
                                       select z into t
                                       select (string?)t).StringConcatenate();
                    int num16 = CalcWidthOfRunInTwips(new XElement(W.r, xAttribute, parent.Elements(W.rPr), new XElement(W.t, content4)));
                    int num17 = (int)xElement6.Attribute(W.pos) - num16 / 2 - num7;
                    if (num17 < 0)
                    {
                        num17 = 0;
                    }

                    xElement5.Add(new XAttribute(PtOpenXml.TabWidth, string.Format(NumberFormatInfo.InvariantInfo, "{0:0.000}", (decimal)num17 / 1440m)), GetLeader(xElement6));
                    num7 = Math.Max((int)xElement6.Attribute(W.pos) + num16 / 2, num7 + num16);
                    XElement xElement10 = source3.LastOrDefault();
                    if (xElement10 == null)
                    {
                        break;
                    }

                    num8 = Array.IndexOf(array, xElement10) + 1;
                    if (num8 >= array.Length)
                    {
                        break;
                    }
                }
                else
                {
                    switch (text)
                    {
                        case "left":
                        case "start":
                        case "num":
                            break;
                        default:
                            goto IL_0a8c;
                    }

                    int num18 = (int)xElement6.Attribute(W.pos) - num7;
                    xElement5.Add(new XAttribute(PtOpenXml.TabWidth, string.Format(NumberFormatInfo.InvariantInfo, "{0:0.000}", (decimal)num18 / 1440m)), GetLeader(xElement6));
                    num7 = (int)xElement6.Attribute(W.pos);
                    num8++;
                    if (num8 >= array.Length)
                    {
                        break;
                    }
                }
            }

            return new XElement(xElement.Name, xElement.Attributes(), from n in xElement.Nodes()
                                                                      select CalculateSpanWidthTransform(n, defaultTabStop));
        }

        private static XAttribute GetLeader(XElement tabAfterText)
        {
            string text = (string?)tabAfterText.Attribute(W.leader);
            if (text == null)
            {
                return null;
            }

            return new XAttribute(PtOpenXml.Leader, text);
        }

        private static XElement AddDefaultTabsAfterLastTab(XElement tabs, int defaultTabStop)
        {
            XElement xElement = (from t in tabs.Elements(W.tab)
                                 where (string?)t.Attribute(W.val) != "clear" && (string?)t.Attribute(W.val) != "bar"
                                 orderby (int)t.Attribute(W.pos)
                                 select t).LastOrDefault();
            if (xElement != null)
            {
                if (defaultTabStop == 0)
                {
                    defaultTabStop = 720;
                }

                int start = (int)xElement.Attribute(W.pos) / defaultTabStop + 1;
                XElement xElement2 = new XElement(W.tabs, from t in tabs.Elements()
                                                          where (string?)t.Attribute(W.val) != "clear" && (string?)t.Attribute(W.val) != "bar"
                                                          select t, from r in Enumerable.Range(start, 100)
                                                                    select new XElement(W.tab, new XAttribute(W.val, "left"), new XAttribute(W.pos, r * defaultTabStop)));
                return new XElement(W.tabs, from t in xElement2.Elements()
                                            orderby (int)t.Attribute(W.pos)
                                            select t);
            }

            tabs = new XElement(W.tabs, from r in Enumerable.Range(1, 100)
                                        select new XElement(W.tab, new XAttribute(W.val, "left"), new XAttribute(W.pos, r * defaultTabStop)));
            return tabs;
        }

        public static Size MeasureText(string text, Font font, Size proposedSize, TextFormatFlags flags)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Size.Empty;
            }

            Bitmap bitmap = null;
            bitmap = new Bitmap(500, 200);
            Graphics graphics = Graphics.FromImage(bitmap);
            SizeF sizeF = graphics.MeasureString(text, font);
            bitmap.Dispose();
            graphics.Dispose();
            return new Size((int)Math.Floor(sizeF.Width), (int)Math.Floor(sizeF.Height));
        }

        private static int CalcWidthOfRunInTwips(XElement r)
        {
            string text = ((string?)r.Attribute(PtOpenXml.pt + "FontName")) ?? ((string?)r.Ancestors(W.p).First().Attribute(PtOpenXml.pt + "FontName"));
            if (text == null)
            {
                throw new DocxToHtmlException("Internal Error, should have FontName attribute");
            }

            if (UnknownFonts.Contains(text))
            {
                return 0;
            }

            XElement xElement = r.Element(W.rPr);
            if (xElement == null)
            {
                throw new DocxToHtmlException("Internal Error, should have run properties");
            }

            decimal num = GetFontSize(r) ?? 22m;
            if (!KnownFamilies.Contains(text))
            {
                return 0;
            }

            FontFamily family;
            try
            {
                family = new FontFamily(text);
            }
            catch (ArgumentException)
            {
                UnknownFonts.Add(text);
                return 0;
            }

            FontStyle fontStyle = FontStyle.Regular;
            if (GetBoolProp(xElement, W.b) || GetBoolProp(xElement, W.bCs))
            {
                fontStyle |= FontStyle.Bold;
            }

            if (GetBoolProp(xElement, W.i) || GetBoolProp(xElement, W.iCs))
            {
                fontStyle |= FontStyle.Italic;
            }

            string text2 = (from e in r.DescendantsTrimmed(W.txbxContent)
                            where e.Name == W.t
                            select e into t
                            select (string?)t).StringConcatenate() + " ";
            decimal num2 = (from e in r.DescendantsTrimmed(W.txbxContent)
                            where e.Name == W.tab
                            select e into t
                            select (decimal)t.Attribute(PtOpenXml.TabWidth)).Sum();
            if (text2.Length == 0 && num2 == 0m)
            {
                return 0;
            }

            int num3 = 1;
            if (text2.Length <= 2)
            {
                num3 = 100;
            }
            else if (text2.Length <= 4)
            {
                num3 = 50;
            }
            else if (text2.Length <= 8)
            {
                num3 = 25;
            }
            else if (text2.Length <= 16)
            {
                num3 = 12;
            }
            else if (text2.Length <= 32)
            {
                num3 = 6;
            }

            if (num3 != 1)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < num3; i++)
                {
                    stringBuilder.Append(text2);
                }

                text2 = stringBuilder.ToString();
            }

            try
            {
                using Font font = new Font(family, (float)num / 2f, fontStyle);
                Size proposedSize = new Size(int.MaxValue, int.MaxValue);
                return (int)((decimal)MeasureText(text2, font, proposedSize, TextFormatFlags.NoPadding).Width / 96m * 1440m / (decimal)num3 + num2 * 1440m);
            }
            catch (ArgumentException)
            {
                try
                {
                    using Font font2 = new Font(family, (float)num / 2f, FontStyle.Regular);
                    Size proposedSize2 = new Size(int.MaxValue, int.MaxValue);
                    return (int)((decimal)MeasureText(text2, font2, proposedSize2, TextFormatFlags.NoPadding).Width / 96m * 1440m / (decimal)num3 + num2 * 1440m);
                }
                catch (ArgumentException)
                {
                    try
                    {
                        using Font font3 = new Font(family, (float)num / 2f, FontStyle.Bold);
                        Size proposedSize3 = new Size(int.MaxValue, int.MaxValue);
                        return (int)((decimal)MeasureText(text2, font3, proposedSize3, TextFormatFlags.NoPadding).Width / 96m * 1440m / (decimal)num3 + num2 * 1440m);
                    }
                    catch (ArgumentException)
                    {
                        using Font font4 = new Font(new FontFamily("Times New Roman"), (float)num / 2f, fontStyle);
                        Size proposedSize4 = new Size(int.MaxValue, int.MaxValue);
                        return (int)((decimal)MeasureText(text2, font4, proposedSize4, TextFormatFlags.NoPadding).Width / 96m * 1440m / (decimal)num3 + num2 * 1440m);
                    }
                }
            }
            catch (OverflowException)
            {
                return 0;
            }
        }

        private static void InsertAppropriateNonbreakingSpaces(WordprocessingDocument wordDoc)
        {
            foreach (OpenXmlPart item in wordDoc.ContentParts())
            {
                XElement root = item.GetXDocument().Root;
                if (root == null)
                {
                    break;
                }

                XElement content = (XElement)InsertAppropriateNonbreakingSpacesTransform(root);
                root.ReplaceWith(content);
                item.PutXDocument();
            }
        }

        private static object InsertAppropriateNonbreakingSpacesTransform(XNode node)
        {
            XElement xElement = node as XElement;
            if (xElement != null)
            {
                if (xElement.Name == W.p && !(from e in xElement.Elements()
                                              where e.Name != W.pPr
                                              select e).DescendantsAndSelf().Any((XElement e) => e.Name == W.dayLong || e.Name == W.dayShort || e.Name == W.drawing || e.Name == W.monthLong || e.Name == W.monthShort || e.Name == W.noBreakHyphen || e.Name == W._object || e.Name == W.pgNum || e.Name == W.ptab || e.Name == W.separator || e.Name == W.softHyphen || e.Name == W.sym || e.Name == W.t || e.Name == W.tab || e.Name == W.yearLong || e.Name == W.yearShort))
                {
                    return new XElement(xElement.Name, xElement.Attributes(), from n in xElement.Nodes()
                                                                              select InsertAppropriateNonbreakingSpacesTransform(n), new XElement(W.r, xElement.Elements(W.pPr).Elements(W.rPr), new XElement(W.t, " ")));
                }

                return new XElement(xElement.Name, xElement.Attributes(), from n in xElement.Nodes()
                                                                          select InsertAppropriateNonbreakingSpacesTransform(n));
            }

            return node;
        }

        private static void AnnotateForSections(WordprocessingDocument wordDoc)
        {
            XDocument xDocument = wordDoc.MainDocumentPart.GetXDocument();
            XElement root = xDocument.Root;
            if (root == null)
            {
                return;
            }

            XElement xElement = root.Element(W.body);
            if (xElement == null)
            {
                return;
            }

            XElement xElement2 = xElement.Elements(W.sectPr).LastOrDefault();
            if (xElement2 != null)
            {
                XElement xElement3 = xElement.DescendantsTrimmed(W.txbxContent).LastOrDefault((XElement p) => p.Name == W.p);
                if (xElement3 != null)
                {
                    XElement xElement4 = xElement3.Element(W.pPr);
                    if (xElement4 != null)
                    {
                        xElement4.Add(xElement2);
                    }
                    else
                    {
                        xElement3.Add(new XElement(W.pPr, xElement2));
                    }

                    xElement2.Remove();
                }
            }

            List<XElement> list = xDocument.Descendants().Reverse().ToList();
            SectionAnnotation annotation = InitializeSectionAnnotation(list);
            foreach (XElement item in list)
            {
                if (item.Name == W.sectPr)
                {
                    if (item.Attribute(XNamespace.Xmlns + "w") == null)
                    {
                        item.Add(new XAttribute(XNamespace.Xmlns + "w", W.w));
                    }

                    annotation = new SectionAnnotation
                    {
                        SectionElement = item
                    };
                }
                else
                {
                    item.AddAnnotation(annotation);
                }
            }
        }

        private static SectionAnnotation InitializeSectionAnnotation(IEnumerable<XElement> reverseDescendants)
        {
            SectionAnnotation sectionAnnotation = new SectionAnnotation
            {
                SectionElement = reverseDescendants.FirstOrDefault((XElement e) => e.Name == W.sectPr)
            };
            if (sectionAnnotation.SectionElement != null && sectionAnnotation.SectionElement.Attribute(XNamespace.Xmlns + "w") == null)
            {
                sectionAnnotation.SectionElement.Add(new XAttribute(XNamespace.Xmlns + "w", W.w));
            }

            if (sectionAnnotation.SectionElement == null)
            {
                SectionAnnotation sectionAnnotation2 = new SectionAnnotation();
                sectionAnnotation2.SectionElement = new XElement(W.sectPr, new XAttribute(XNamespace.Xmlns + "w", W.w), new XElement(W.pgSz, new XAttribute(W._w, 12240), new XAttribute(W.h, 15840)), new XElement(W.pgMar, new XAttribute(W.top, 1440), new XAttribute(W.right, 1440), new XAttribute(W.bottom, 1440), new XAttribute(W.left, 1440), new XAttribute(W.header, 720), new XAttribute(W.footer, 720), new XAttribute(W.gutter, 0)), new XElement(W.cols, new XAttribute(W.space, 720)), new XElement(W.docGrid, new XAttribute(W.linePitch, 360)));
                sectionAnnotation = sectionAnnotation2;
            }

            return sectionAnnotation;
        }

        private static object CreateBorderDivs(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, IEnumerable<XElement> elements)
        {
            return elements.GroupAdjacent(delegate (XElement e)
            {
                XElement xElement4 = e.Elements(W.pPr).Elements(W.pBdr).FirstOrDefault();
                if (xElement4 != null)
                {
                    string text = string.Empty;
                    XElement xElement5 = e.Elements(W.pPr).Elements(W.ind).FirstOrDefault();
                    if (xElement5 != null)
                    {
                        text = xElement5.ToString(SaveOptions.DisableFormatting);
                    }

                    return xElement4.ToString(SaveOptions.DisableFormatting) + text;
                }

                return (!(e.Name == W.tbl)) ? string.Empty : "table";
            }).Select<IGrouping<string, XElement>, object>((Func<IGrouping<string, XElement>, object>)delegate (IGrouping<string, XElement> g)
            {
                if (g.Key == string.Empty)
                {
                    return GroupAndVerticallySpaceNumberedParagraphs(wordDoc, settings, g, 0m);
                }

                if (!(g.Key == "table"))
                {
                    XElement xElement = g.First().Elements(W.pPr).First();
                    XElement? pBdr = xElement.Element(W.pBdr);
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    GenerateBorderStyle(pBdr, W.top, dictionary, BorderType.Paragraph);
                    GenerateBorderStyle(pBdr, W.right, dictionary, BorderType.Paragraph);
                    GenerateBorderStyle(pBdr, W.bottom, dictionary, BorderType.Paragraph);
                    GenerateBorderStyle(pBdr, W.left, dictionary, BorderType.Paragraph);
                    decimal num = default(decimal);
                    XElement xElement2 = xElement.Element(W.ind);
                    if (xElement2 != null)
                    {
                        decimal obj = ((decimal?)xElement2.Attribute(W.left) / (decimal?)1440m) ?? 0m;
                        decimal num2 = (-(decimal?)xElement2.Attribute(W.hanging) / (decimal?)1440m) ?? 0m;
                        num = obj + num2;
                        dictionary.AddIfMissing("margin-left", (num > 0m) ? string.Format(NumberFormatInfo.InvariantInfo, "{0:0.00}in", num) : "0");
                    }

                    XElement xElement3 = new XElement(Xhtml.div, GroupAndVerticallySpaceNumberedParagraphs(wordDoc, settings, g, num));
                    xElement3.AddAnnotation(dictionary);
                    return xElement3;
                }

                return g.Select((XElement gc) => ConvertToHtmlTransform(wordDoc, settings, gc, suppressTrailingWhiteSpace: false, 0m));
            }).ToList();
        }

        private static IEnumerable<object> GroupAndVerticallySpaceNumberedParagraphs(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, IEnumerable<XElement> elements, decimal currentMarginLeft)
        {
            return elements.GroupAdjacent(delegate (XElement e)
            {
                string text = (string?)e.Attribute(PtOpenXml.pt + "AbstractNumId");
                if (text != null)
                {
                    return "num:" + text;
                }

                if (e.Elements(W.pPr).Elements(W.contextualSpacing).FirstOrDefault() != null)
                {
                    string text2 = (string?)e.Elements(W.pPr).Elements(W.pStyle).Attributes(W.val)
                        .FirstOrDefault();
                    if (text2 == null)
                    {
                        return "";
                    }

                    return "sty:" + text2;
                }

                return "";
            }).ToList().Select(delegate (IGrouping<string, XElement> g)
            {
                if (g.Key == "")
                {
                    return g.Select((XElement e) => ConvertToHtmlTransform(wordDoc, settings, e, suppressTrailingWhiteSpace: false, currentMarginLeft));
                }

                int last = g.Count() - 1;
                return g.Select((XElement e, int i) => ConvertToHtmlTransform(wordDoc, settings, e, i != last, currentMarginLeft));
            });
        }

        private static void GenerateBorderStyle(XElement pBdr, XName sideXName, Dictionary<string, string> style, BorderType borderType)
        {
            string text = ((sideXName == W.top) ? "top" : ((sideXName == W.right) ? "right" : ((!(sideXName == W.bottom)) ? "left" : "bottom")));
            if (pBdr == null)
            {
                style.Add("border-" + text, "none");
                if (borderType == BorderType.Cell && (text == "left" || text == "right"))
                {
                    style.Add("padding-" + text, "5.4pt");
                }

                return;
            }

            XElement xElement = pBdr.Element(sideXName);
            if (xElement == null)
            {
                style.Add("border-" + text, "none");
                if (borderType == BorderType.Cell && (text == "left" || text == "right"))
                {
                    style.Add("padding-" + text, "5.4pt");
                }

                return;
            }

            string text2 = (string?)xElement.Attribute(W.val);
            if (text2 == "nil" || text2 == "none")
            {
                style.Add("border-" + text + "-style", "none");
                decimal num = ((decimal?)xElement.Attribute(W.space)) ?? 0m;
                if (borderType == BorderType.Cell && (text == "left" || text == "right") && num < 5.4m)
                {
                    num = 5.4m;
                }

                style.Add("padding-" + text, (num == 0m) ? "0" : string.Format(NumberFormatInfo.InvariantInfo, "{0:0.0}pt", num));
                return;
            }

            int num2 = (int)xElement.Attribute(W.sz);
            decimal num3 = ((decimal?)xElement.Attribute(W.space)) ?? 0m;
            string text3 = (string?)xElement.Attribute(W.color);
            text3 = ((text3 != null && !(text3 == "auto")) ? ConvertColor(text3) : "windowtext");
            decimal num4 = Math.Max(1m, Math.Min(96m, Math.Max(2m, num2)) / 8m);
            string text4 = "solid";
            if (BorderStyleMap.ContainsKey(text2))
            {
                BorderMappingInfo borderMappingInfo = BorderStyleMap[text2];
                text4 = borderMappingInfo.CssName;
                if (text2 == "double")
                {
                    num4 = ((num2 <= 8) ? 2.5m : ((num2 > 18) ? ((decimal)num2 / 3m) : 6.75m));
                }
                else if (text2 == "triple")
                {
                    num4 = ((num2 <= 8) ? 8m : ((num2 > 18) ? 11.25m : 11.25m));
                }
                else if (text2.ToLower().Contains("dash"))
                {
                    num4 = ((num2 <= 4) ? 1m : ((num2 > 12) ? 2m : 1.5m));
                }
                else if (text2 != "single")
                {
                    num4 = borderMappingInfo.CssSize;
                }
            }

            if (text2 == "outset" || text2 == "inset")
            {
                text3 = "";
            }

            string text5 = string.Format(NumberFormatInfo.InvariantInfo, "{0:0.0}pt", num4);
            style.Add("border-" + text, text4 + " " + text3 + " " + text5);
            if (borderType == BorderType.Cell && (text == "left" || text == "right") && num3 < 5.4m)
            {
                num3 = 5.4m;
            }

            style.Add("padding-" + text, (num3 == 0m) ? "0" : string.Format(NumberFormatInfo.InvariantInfo, "{0:0.0}pt", num3));
        }

        private static string ConvertColorFillPct(string color, string fill, double pct)
        {
            if (color == "auto")
            {
                color = "000000";
            }

            if (fill == "auto")
            {
                fill = "ffffff";
            }

            string key = color + fill + pct.ToString(CultureInfo.InvariantCulture);
            if (ShadeCache.ContainsKey(key))
            {
                return ShadeCache[key];
            }

            int num = Convert.ToInt32(fill.Substring(0, 2), 16);
            int num2 = Convert.ToInt32(fill.Substring(2, 2), 16);
            int num3 = Convert.ToInt32(fill.Substring(4, 2), 16);
            int num4 = Convert.ToInt32(color.Substring(0, 2), 16);
            int num5 = Convert.ToInt32(color.Substring(2, 2), 16);
            int num6 = Convert.ToInt32(color.Substring(4, 2), 16);
            int num7 = (int)((double)num - (double)(num - num4) * pct);
            int num8 = (int)((double)num2 - (double)(num2 - num5) * pct);
            int num9 = (int)((double)num3 - (double)(num3 - num6) * pct);
            string text = $"{num7:x2}{num8:x2}{num9:x2}";
            ShadeCache.Add(key, text);
            return text;
        }

        private static void CreateStyleFromShd(Dictionary<string, string> style, XElement shd)
        {
            if (shd == null)
            {
                return;
            }

            string key = (string?)shd.Attribute(W.val);
            string text = (string?)shd.Attribute(W.color);
            string arg = (string?)shd.Attribute(W.fill);
            if (ShadeMapper.ContainsKey(key))
            {
                text = ShadeMapper[key](text, arg);
            }

            if (text != null)
            {
                string value = ConvertColor(text);
                if (!string.IsNullOrEmpty(value))
                {
                    style.AddIfMissing("background", value);
                }
            }
        }

        private static void CreateColorProperty(string propertyName, string color, Dictionary<string, string> style)
        {
            if (color == null)
            {
                return;
            }

            if (color == "auto")
            {
                color = ((propertyName == "color") ? "black" : "white");
            }

            if (NamedColors.ContainsKey(color))
            {
                string text = NamedColors[color];
                if (!(text == ""))
                {
                    style.AddIfMissing(propertyName, text);
                }
            }
            else
            {
                style.AddIfMissing(propertyName, "#" + color);
            }
        }

        private static string ConvertColor(string color)
        {
            if (color == "auto")
            {
                color = "white";
            }

            if (NamedColors.ContainsKey(color))
            {
                string text = NamedColors[color];
                if (text == "")
                {
                    return "black";
                }

                return text;
            }

            return "#" + color;
        }

        private static void CreateFontCssProperty(string font, Dictionary<string, string> style)
        {
            if (FontFallback.ContainsKey(font))
            {
                style.AddIfMissing("font-family", string.Format(FontFallback[font], font));
            }
            else
            {
                style.AddIfMissing("font-family", font);
            }
        }

        private static bool GetBoolProp(XElement runProps, XName xName)
        {
            XElement xElement = runProps.Element(xName);
            if (xElement == null)
            {
                return false;
            }

            XAttribute xAttribute = xElement.Attribute(W.val);
            if (xAttribute == null)
            {
                return true;
            }

            switch (xAttribute.Value.ToLower())
            {
                case "0":
                case "false":
                    return false;
                case "1":
                case "true":
                    return true;
                default:
                    return false;
            }
        }

        private static object ConvertContentThatCanContainFields(WordprocessingDocument wordDoc, WmlToHtmlConverterSettings settings, IEnumerable<XElement> elements)
        {
            return ((IEnumerable<IGrouping<int?, XElement>>)elements.GroupAdjacent(delegate (XElement e)
            {
                Stack<FieldRetriever.FieldElementTypeInfo> stack = e.Annotation<Stack<FieldRetriever.FieldElementTypeInfo>>();
                return (stack != null && stack.Any()) ? new int?(stack.Select((FieldRetriever.FieldElementTypeInfo st) => st.Id).Min()) : null;
            }).ToList()).Select((Func<IGrouping<int?, XElement>, object>)delegate (IGrouping<int?, XElement> g)
            {
                int? key = g.Key;
                if (!key.HasValue)
                {
                    return g.Select((XElement n) => ConvertToHtmlTransform(wordDoc, settings, n, suppressTrailingWhiteSpace: false, 0m));
                }

                FieldRetriever.FieldInfo fieldInfo = FieldRetriever.ParseField(FieldRetriever.InstrText(g.First().Ancestors().Last(), key.Value).TrimStart(new char[1] { '{' }).TrimEnd(new char[1] { '}' }));
                if (fieldInfo.FieldType != "HYPERLINK")
                {
                    return g.Select((XElement n) => ConvertToHtmlTransform(wordDoc, settings, n, suppressTrailingWhiteSpace: false, 0m));
                }

                IEnumerable<object> enumerable = from run in g.DescendantsAndSelf(W.r)
                                                 select ConvertRun(wordDoc, settings, run);
                XElement xElement = ((fieldInfo.Arguments.Length != 0) ? new XElement(Xhtml.a, new XAttribute((XName?)"href", fieldInfo.Arguments[0]), enumerable) : new XElement(Xhtml.a, enumerable));
                XElement xElement2 = xElement;
                if (!xElement2.Nodes().Any())
                {
                    xElement2.Add(new XText(""));
                    return xElement2;
                }

                return xElement;
            }).ToList();
        }

        public static XElement ProcessImage(WordprocessingDocument wordDoc, XElement element, Func<ImageInfo, XElement> imageHandler)
        {
            if (imageHandler == null)
            {
                return null;
            }

            if (element.Name == W.drawing)
            {
                return ProcessDrawing(wordDoc, element, imageHandler);
            }

            if (element.Name == W.pict || element.Name == W._object)
            {
                return ProcessPictureOrObject(wordDoc, element, imageHandler);
            }

            return null;
        }

        private static XElement ProcessDrawing(WordprocessingDocument wordDoc, XElement element, Func<ImageInfo, XElement> imageHandler)
        {
            XElement xElement = element.Elements().FirstOrDefault((XElement e) => e.Name == WP.inline || e.Name == WP.anchor);
            if (xElement == null)
            {
                return null;
            }

            string text = null;
            XElement xElement2 = element.Elements(WP.inline).Elements(WP.docPr).Elements(A.hlinkClick)
                .FirstOrDefault();
            if (xElement2 != null)
            {
                string rId = (string?)xElement2.Attribute(R.id);
                if (rId != null)
                {
                    HyperlinkRelationship hyperlinkRelationship = wordDoc.MainDocumentPart.HyperlinkRelationships.FirstOrDefault((HyperlinkRelationship hlr) => hlr.Id == rId);
                    if (hyperlinkRelationship != null)
                    {
                        text = hyperlinkRelationship.Uri.ToString();
                    }
                }
            }

            int? num = (int?)xElement.Elements(WP.extent).Attributes(NoNamespace.cx).FirstOrDefault();
            int? num2 = (int?)xElement.Elements(WP.extent).Attributes(NoNamespace.cy).FirstOrDefault();
            string altText = ((string?)xElement.Elements(WP.docPr).Attributes(NoNamespace.descr).FirstOrDefault()) ?? ((string?)xElement.Elements(WP.docPr).Attributes(NoNamespace.name).FirstOrDefault()) ?? "";
            XElement xElement3 = xElement.Elements(A.graphic).Elements(A.graphicData).Elements(Pic._pic)
                .Elements(Pic.blipFill)
                .FirstOrDefault();
            if (xElement3 == null)
            {
                return null;
            }

            string imageRid = (string?)xElement3.Elements(A.blip).Attributes(R.embed).FirstOrDefault();
            if (imageRid == null)
            {
                return null;
            }

            IdPartPair idPartPair = wordDoc.MainDocumentPart.Parts.FirstOrDefault((IdPartPair pp) => pp.RelationshipId == imageRid);
            if (idPartPair == null)
            {
                return null;
            }

            ImagePart imagePart = (ImagePart)idPartPair.OpenXmlPart;
            if (imagePart == null)
            {
                return null;
            }

            try
            {
                imagePart = (ImagePart)wordDoc.MainDocumentPart.GetPartById(imageRid);
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }

            string contentType = imagePart.ContentType;
            if (!ImageContentTypes.Contains(contentType))
            {
                return null;
            }

            using Stream stream = imagePart.GetStream();
            using Bitmap bitmap = new Bitmap(stream);
            if (num.HasValue && num2.HasValue)
            {
                ImageInfo arg = new ImageInfo
                {
                    Bitmap = bitmap,
                    ImgStyleAttribute = new XAttribute((XName?)"style", string.Format(NumberFormatInfo.InvariantInfo, "width: {0}in; height: {1}in", (float)num.Value / 914400f, (float)num2.Value / 914400f)),
                    ContentType = contentType,
                    DrawingElement = element,
                    AltText = altText
                };
                XElement xElement4 = imageHandler(arg);
                if (text != null)
                {
                    return new XElement(XhtmlNoNamespace.a, new XAttribute(XhtmlNoNamespace.href, text), xElement4);
                }

                return xElement4;
            }

            ImageInfo arg2 = new ImageInfo
            {
                Bitmap = bitmap,
                ContentType = contentType,
                DrawingElement = element,
                AltText = altText
            };
            XElement xElement5 = imageHandler(arg2);
            if (text != null)
            {
                return new XElement(XhtmlNoNamespace.a, new XAttribute(XhtmlNoNamespace.href, text), xElement5);
            }

            return xElement5;
        }

        private static XElement ProcessPictureOrObject(WordprocessingDocument wordDoc, XElement element, Func<ImageInfo, XElement> imageHandler)
        {
            string imageRid = (string?)element.Elements(VML.shape).Elements(VML.imagedata).Attributes(R.id)
                .FirstOrDefault();
            if (imageRid == null)
            {
                return null;
            }

            try
            {
                IdPartPair idPartPair = wordDoc.MainDocumentPart.Parts.FirstOrDefault((IdPartPair pp2) => pp2.RelationshipId == imageRid);
                if (idPartPair == null)
                {
                    return null;
                }

                ImagePart imagePart = (ImagePart)idPartPair.OpenXmlPart;
                if (imagePart == null)
                {
                    return null;
                }

                string contentType = imagePart.ContentType;
                if (!ImageContentTypes.Contains(contentType))
                {
                    return null;
                }

                using Stream stream = imagePart.GetStream();
                try
                {
                    using Bitmap bitmap = new Bitmap(stream);
                    ImageInfo imageInfo = new ImageInfo
                    {
                        Bitmap = bitmap,
                        ContentType = contentType,
                        DrawingElement = element
                    };
                    string text = (string?)element.Elements(VML.shape).Attributes("style").FirstOrDefault();
                    if (text == null)
                    {
                        return imageHandler(imageInfo);
                    }

                    string[] tokens = text.Split(new char[1] { ';' });
                    float? num = WidthInPoints(tokens);
                    float? num2 = HeightInPoints(tokens);
                    if (num.HasValue && num2.HasValue)
                    {
                        imageInfo.ImgStyleAttribute = new XAttribute((XName?)"style", string.Format(NumberFormatInfo.InvariantInfo, "width: {0}pt; height: {1}pt", num, num2));
                    }

                    return imageHandler(imageInfo);
                }
                catch (OutOfMemoryException)
                {
                    return null;
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        private static float? HeightInPoints(IEnumerable<string> tokens)
        {
            return SizeInPoints(tokens, "height");
        }

        private static float? WidthInPoints(IEnumerable<string> tokens)
        {
            return SizeInPoints(tokens, "width");
        }

        private static float? SizeInPoints(IEnumerable<string> tokens, string name)
        {
            string text = (from t in tokens
                           select new
                           {
                               Name = t.Split(new char[1] { ':' }).First(),
                               Value = t.Split(new char[1] { ':' }).Skip(1).Take(1)
                                   .FirstOrDefault()
                           } into p
                           where p.Name == name
                           select p.Value).FirstOrDefault();
            if (text != null && text.Length > 2 && text.Substring(text.Length - 2) == "pt" && float.TryParse(text.Substring(0, text.Length - 2), out var result))
            {
                return result;
            }

            return null;
        }
    }
}
