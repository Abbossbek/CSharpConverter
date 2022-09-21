// Decompiled with JetBrains decompiler
// Type: VetCV.HtmlRendererCore.PdfSharpCore.PdfGenerator
// Assembly: HtmlRendererCore.PdfSharpCore, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA72F8E-2C1A-42B6-AF29-CEB7845EFBE4
// Assembly location: C:\Users\Abbosbek\.nuget\packages\htmlrenderercore.pdfsharpcore\1.0.1\lib\netcoreapp2.0\HtmlRendererCore.PdfSharpCore.dll

using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using TheArtOfDev.HtmlRenderer.Adapters;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.Core.Utils;
using CSharpConverter.Pdf.Adapters;

namespace CSharpConverter.Pdf
{
    public static class PdfGenerator
    {
        public static void AddFontFamilyMapping(string fromFamily, string toFamily)
        {
            ArgChecker.AssertArgNotNullOrEmpty(fromFamily, "fromFamily");
            ArgChecker.AssertArgNotNullOrEmpty(toFamily, "toFamily");
            PdfSharpAdapter.Instance.AddFontFamilyMapping(fromFamily, toFamily);
        }

        public static CssData ParseStyleSheet(string stylesheet, bool combineWithDefault = true)
        {
            return CssData.Parse(PdfSharpAdapter.Instance, stylesheet, combineWithDefault);
        }

        public static PdfDocument GeneratePdf(string html, PageSize pageSize, int margin = 20, CssData cssData = null, EventHandler<HtmlStylesheetLoadEventArgs> stylesheetLoad = null, EventHandler<HtmlImageLoadEventArgs> imageLoad = null)
        {
            PdfGenerateConfig pdfGenerateConfig = new PdfGenerateConfig();
            pdfGenerateConfig.PageSize = pageSize;
            pdfGenerateConfig.SetMargins(margin);
            return GeneratePdf(html, pdfGenerateConfig, cssData, stylesheetLoad, imageLoad);
        }
        public static PdfDocument GeneratePdf(string html, double width, double height, int margin = 20, CssData cssData = null, EventHandler<HtmlStylesheetLoadEventArgs> stylesheetLoad = null, EventHandler<HtmlImageLoadEventArgs> imageLoad = null)
        {
            PdfGenerateConfig pdfGenerateConfig = new PdfGenerateConfig();
            pdfGenerateConfig.ManualPageSize = new XSize(width, height);
            pdfGenerateConfig.SetMargins(margin);
            return GeneratePdf(html, pdfGenerateConfig, cssData, stylesheetLoad, imageLoad);
        }

        public static PdfDocument GeneratePdf(string html, PdfGenerateConfig config, CssData cssData = null, EventHandler<HtmlStylesheetLoadEventArgs> stylesheetLoad = null, EventHandler<HtmlImageLoadEventArgs> imageLoad = null)
        {
            PdfDocument pdfDocument = new PdfDocument();
            AddPdfPages(pdfDocument, html, config, cssData, stylesheetLoad, imageLoad);
            return pdfDocument;
        }

        public static void AddPdfPages(PdfDocument document, string html, PageSize pageSize, int margin = 20, CssData cssData = null, EventHandler<HtmlStylesheetLoadEventArgs> stylesheetLoad = null, EventHandler<HtmlImageLoadEventArgs> imageLoad = null)
        {
            PdfGenerateConfig pdfGenerateConfig = new PdfGenerateConfig();
            pdfGenerateConfig.PageSize = pageSize;
            pdfGenerateConfig.SetMargins(margin);
            AddPdfPages(document, html, pdfGenerateConfig, cssData, stylesheetLoad, imageLoad);
        }

        public static void AddPdfPages(PdfDocument document, string html, PdfGenerateConfig config, CssData cssData = null, EventHandler<HtmlStylesheetLoadEventArgs> stylesheetLoad = null, EventHandler<HtmlImageLoadEventArgs> imageLoad = null)
        {
            XSize orgPageSize = ((config.PageSize == PageSize.Undefined) ? config.ManualPageSize : PageSizeConverter.ToSize(config.PageSize));
            if (config.PageOrientation == PageOrientation.Landscape)
            {
                orgPageSize = new XSize(orgPageSize.Height, orgPageSize.Width);
            }

            XSize xSize = new XSize(orgPageSize.Width - (double)config.MarginLeft - (double)config.MarginRight, orgPageSize.Height - (double)config.MarginTop - (double)config.MarginBottom);
            if (string.IsNullOrEmpty(html))
            {
                return;
            }

            using HtmlContainer htmlContainer = new HtmlContainer();
            if (stylesheetLoad != null)
            {
                htmlContainer.StylesheetLoad += stylesheetLoad;
            }

            if (imageLoad != null)
            {
                htmlContainer.ImageLoad += imageLoad;
            }

            htmlContainer.Location = new XPoint(config.MarginLeft, config.MarginTop);
            htmlContainer.MaxSize = new XSize(xSize.Width, 0.0);
            htmlContainer.SetHtml(html, cssData);
            htmlContainer.PageSize = xSize;
            htmlContainer.MarginBottom = config.MarginBottom;
            htmlContainer.MarginLeft = config.MarginLeft;
            htmlContainer.MarginRight = config.MarginRight;
            htmlContainer.MarginTop = config.MarginTop;
            using (XGraphics g = XGraphics.CreateMeasureContext(xSize, XGraphicsUnit.Point, XPageDirection.Downwards))
            {
                htmlContainer.PerformLayout(g);
            }

            for (double num = 0.0; num > 0.0 - htmlContainer.ActualSize.Height; num -= xSize.Height)
            {
                PdfPage pdfPage = document.AddPage();
                pdfPage.Height = orgPageSize.Height;
                pdfPage.Width = orgPageSize.Width;
                using XGraphics xGraphics = XGraphics.FromPdfPage(pdfPage);
                xGraphics.IntersectClip(new XRect(0.0, 0.0, pdfPage.Width, pdfPage.Height));
                htmlContainer.ScrollOffset = new XPoint(0.0, num);
                htmlContainer.PerformPaint(xGraphics);
            }

            HandleLinks(document, htmlContainer, orgPageSize, xSize);
        }

        private static void HandleLinks(PdfDocument document, HtmlContainer container, XSize orgPageSize, XSize pageSize)
        {
            foreach (LinkElementData<XRect> link in container.GetLinks())
            {
                for (int i = (int)(link.Rectangle.Top / pageSize.Height); i < document.Pages.Count && pageSize.Height * (double)i < link.Rectangle.Bottom; i++)
                {
                    double num = pageSize.Height * (double)i;
                    XRect rect = new XRect(link.Rectangle.Left, orgPageSize.Height - (link.Rectangle.Height + link.Rectangle.Top - num), link.Rectangle.Width, link.Rectangle.Height);
                    if (link.IsAnchor)
                    {
                        XRect? elementRectangle = container.GetElementRectangle(link.AnchorId);
                        if (elementRectangle.HasValue)
                        {
                            int num2 = (int)(elementRectangle.Value.Top / pageSize.Height);
                            if (i != num2)
                            {
                                document.Pages[i].AddDocumentLink(new PdfRectangle(rect), num2);
                            }
                        }
                    }
                    else
                    {
                        document.Pages[i].AddWebLink(new PdfRectangle(rect), link.Href);
                    }
                }
            }
        }
    }
}
