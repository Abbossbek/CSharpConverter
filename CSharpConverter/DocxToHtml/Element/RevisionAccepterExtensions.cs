// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.RevisionAccepterExtensions
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class RevisionAccepterExtensions
  {
    private static void InitializeParagraphInfo(XElement contentContext)
    {
      if (!((IEnumerable<XName>) W.BlockLevelContentContainers).Contains<XName>(contentContext.Name))
        throw new ArgumentException("GetParagraphInfo called for element that is not child of content container");
      XElement xelement1 = (XElement) null;
      foreach (XElement element in contentContext.Elements())
      {
        XElement xelement2 = element.DescendantsAndSelf().Where<XElement>((Func<XElement, bool>) (e => e.Name == W.p || e.Name == W.tc || e.Name == W.txbxContent)).FirstOrDefault<XElement>();
        if (xelement2 != null && (xelement2.Name == W.tc || xelement2.Name == W.txbxContent))
          xelement2 = (XElement) null;
        element.AddAnnotation((object) new BlockContentInfo()
        {
          PreviousBlockContentElement = xelement1,
          ThisBlockContentElement = xelement2
        });
        xelement1 = element;
      }
    }

    public static BlockContentInfo GetParagraphInfo(this XElement contentElement)
    {
      BlockContentInfo paragraphInfo = contentElement.Annotation<BlockContentInfo>();
      if (paragraphInfo != null)
        return paragraphInfo;
      RevisionAccepterExtensions.InitializeParagraphInfo(contentElement.Parent);
      return contentElement.Annotation<BlockContentInfo>();
    }

    public static IEnumerable<XElement> ContentElementsBeforeSelf(
      this XElement element)
    {
      XElement contentElement = element;
      while (true)
      {
        BlockContentInfo pi = contentElement.GetParagraphInfo();
        if (pi.PreviousBlockContentElement != null)
        {
          yield return pi.PreviousBlockContentElement;
          contentElement = pi.PreviousBlockContentElement;
          pi = (BlockContentInfo) null;
        }
        else
          break;
      }
    }
  }
}
