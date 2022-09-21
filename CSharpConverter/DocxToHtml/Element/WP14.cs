// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.WP14
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class WP14
  {
    public static readonly XNamespace wp14 = (XNamespace) "http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing";
    public static readonly XName anchorId = WP14.wp14 + nameof (anchorId);
    public static readonly XName editId = WP14.wp14 + nameof (editId);
    public static readonly XName pctHeight = WP14.wp14 + nameof (pctHeight);
    public static readonly XName pctPosVOffset = WP14.wp14 + nameof (pctPosVOffset);
    public static readonly XName pctWidth = WP14.wp14 + nameof (pctWidth);
    public static readonly XName sizeRelH = WP14.wp14 + nameof (sizeRelH);
    public static readonly XName sizeRelV = WP14.wp14 + nameof (sizeRelV);
  }
}
