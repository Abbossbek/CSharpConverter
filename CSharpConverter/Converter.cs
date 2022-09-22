using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;

using CSharpConverter.DocxToHtml.Element;
using CSharpConverter.Pdf;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

using PdfSharpCore.Pdf;

namespace CSharpConverter
{
    public class Converter
    {
        public static byte[] DocxToPdf(Stream stream)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(stream, true))
            {
                return DocxToPdf(wordDoc);
            }
        }
        public static byte[] DocxToPdf(string path)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, true))
            {
                return DocxToPdf(wordDoc);
            }
        }
        public static byte[] DocxToPdf(WordprocessingDocument wordDoc)
        {
            using (MemoryStream pdfStream = new MemoryStream())
            {
                PageSize pageSize = wordDoc.MainDocumentPart?.Document?.Body?.GetFirstChild<SectionProperties>()?.GetFirstChild<PageSize>();
                var html = DocxToHtml(wordDoc);
                PdfDocument pdf = pageSize == null ?
                    PdfGenerator.GeneratePdf(html, PdfSharpCore.PageSize.A4)
                    : PdfGenerator.GeneratePdf(html, pageSize.Width / 20, pageSize.Height / 20);
                pdf.Save(pdfStream);
                return pdfStream.ToArray();
            }
        }

        public static string DocxToHtml(WordprocessingDocument wordDoc)
        {
            string str1 = "";
            int imageCounter = 0;
            WmlToHtmlConverterSettings htmlConverterSettings = new WmlToHtmlConverterSettings()
            {
                AdditionalCss = $"body {{ max-width: 100%; padding: 0; }}",
                PageTitle = str1,
                FabricateCssClasses = true,
                CssClassPrefix = "pt-",
                RestrictToSupportedLanguages = false,
                RestrictToSupportedNumberingFormats = false,
                ImageHandler = (Func<ImageInfo, XElement>)(imageInfo =>
                {
                    ++imageCounter;
                    string lower = imageInfo.ContentType.Split('/')[1].ToLower();
                    ImageFormat imageFormat = (ImageFormat)null;
                    string str2;
                    if (lower == "png")
                        imageFormat = ImageFormat.Png;
                    else if (lower == "gif")
                        imageFormat = ImageFormat.Gif;
                    else if (lower == "bmp")
                        imageFormat = ImageFormat.Bmp;
                    else if (lower == "jpeg")
                        imageFormat = ImageFormat.Jpeg;
                    else if (lower == "tiff")
                    {
                        str2 = "gif";
                        imageFormat = ImageFormat.Gif;
                    }
                    else if (lower == "x-wmf")
                    {
                        str2 = "wmf";
                        imageFormat = ImageFormat.Wmf;
                    }
                    if (imageFormat == null)
                        return (XElement)null;
                    string str3 = (string)null;
                    try
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            ((Image)imageInfo.Bitmap).Save((Stream)memoryStream, imageFormat);
                            str3 = Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                    catch (ExternalException ex)
                    {
                        return (XElement)null;
                    }
                    ImageFormat format = ((Image)imageInfo.Bitmap).RawFormat;
                    string str4 = string.Format("data:{0};base64,{1}", (object)((IEnumerable<ImageCodecInfo>)ImageCodecInfo.GetImageDecoders()).First<ImageCodecInfo>((Func<ImageCodecInfo, bool>)(c => c.FormatID == format.Guid)).MimeType, (object)str3);
                    return new XElement(Xhtml.img, new object[3]
                    {
              (object) new XAttribute(NoNamespace.src, (object) str4),
              (object) imageInfo.ImgStyleAttribute,
              imageInfo.AltText != null ? (object) new XAttribute(NoNamespace.alt, (object) imageInfo.AltText) : (object) (XAttribute) null
                    });
                })
            };
            return new XDocument(new object[2]
            {
          (object) new XDocumentType("html", (string) null, (string) null, (string) null),
          (object) WmlToHtmlConverter.ConvertToHtml(wordDoc, htmlConverterSettings)
            }).ToString(SaveOptions.DisableFormatting);
        }
        public static string DocxToHtml(string inputFilePath)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(inputFilePath, true))
            {
                return DocxToHtml(wordDoc);
            }
        }
        public static string DocxToHtml(Stream inputFileStream)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(inputFileStream, true))
            {
                return DocxToHtml(wordDoc);
            }
        }
    }
}
