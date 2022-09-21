// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.COM
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class COM
  {
    public static readonly XNamespace com = (XNamespace) "http://schemas.openxmlformats.org/drawingml/2006/compatibility";
    public static readonly XName legacyDrawing = COM.com + nameof (legacyDrawing);
  }
}
