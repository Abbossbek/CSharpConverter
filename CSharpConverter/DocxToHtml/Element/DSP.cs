// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.DSP
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class DSP
  {
    public static readonly XNamespace dsp = (XNamespace) "http://schemas.microsoft.com/office/drawing/2008/diagram";
    public static readonly XName dataModelExt = DSP.dsp + nameof (dataModelExt);
  }
}
