// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.WP
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class WP
  {
    public static readonly XNamespace wp = (XNamespace) "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing";
    public static readonly XName align = WP.wp + nameof (align);
    public static readonly XName anchor = WP.wp + nameof (anchor);
    public static readonly XName cNvGraphicFramePr = WP.wp + nameof (cNvGraphicFramePr);
    public static readonly XName docPr = WP.wp + nameof (docPr);
    public static readonly XName effectExtent = WP.wp + nameof (effectExtent);
    public static readonly XName extent = WP.wp + nameof (extent);
    public static readonly XName inline = WP.wp + nameof (inline);
    public static readonly XName lineTo = WP.wp + nameof (lineTo);
    public static readonly XName positionH = WP.wp + nameof (positionH);
    public static readonly XName positionV = WP.wp + nameof (positionV);
    public static readonly XName posOffset = WP.wp + nameof (posOffset);
    public static readonly XName simplePos = WP.wp + nameof (simplePos);
    public static readonly XName start = WP.wp + nameof (start);
    public static readonly XName wrapNone = WP.wp + nameof (wrapNone);
    public static readonly XName wrapPolygon = WP.wp + nameof (wrapPolygon);
    public static readonly XName wrapSquare = WP.wp + nameof (wrapSquare);
    public static readonly XName wrapThrough = WP.wp + nameof (wrapThrough);
    public static readonly XName wrapTight = WP.wp + nameof (wrapTight);
    public static readonly XName wrapTopAndBottom = WP.wp + nameof (wrapTopAndBottom);
  }
}
