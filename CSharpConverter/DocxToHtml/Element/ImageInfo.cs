// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ImageInfo
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Drawing;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public class ImageInfo
  {
    public Bitmap Bitmap;
    public XAttribute ImgStyleAttribute;
    public string ContentType;
    public XElement DrawingElement;
    public string AltText;
    public const int EmusPerInch = 914400;
    public const int EmusPerCm = 360000;
  }
}
