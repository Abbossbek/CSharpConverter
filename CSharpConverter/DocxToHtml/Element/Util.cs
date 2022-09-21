// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.Util
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public class Util
  {
    public static string[] WordprocessingExtensions = new string[4]
    {
      ".docx",
      ".docm",
      ".dotx",
      ".dotm"
    };
    public static string[] SpreadsheetExtensions = new string[5]
    {
      ".xlsx",
      ".xlsm",
      ".xltx",
      ".xltm",
      ".xlam"
    };
    public static string[] PresentationExtensions = new string[7]
    {
      ".pptx",
      ".potx",
      ".ppsx",
      ".pptm",
      ".potm",
      ".ppsm",
      ".ppam"
    };

    public static bool IsWordprocessingML(string ext) => ((IEnumerable<string>) Util.WordprocessingExtensions).Contains<string>(ext.ToLower());

    public static bool IsSpreadsheetML(string ext) => ((IEnumerable<string>) Util.SpreadsheetExtensions).Contains<string>(ext.ToLower());

    public static bool IsPresentationML(string ext) => ((IEnumerable<string>) Util.PresentationExtensions).Contains<string>(ext.ToLower());

    public static bool? GetBoolProp(XElement rPr, XName propertyName)
    {
      XElement xelement = rPr.Element(propertyName);
      if (xelement == null)
        return new bool?();
      XAttribute xattribute = xelement.Attribute(W.val);
      if (xattribute == null)
        return new bool?(true);
      string lower = ((string) xattribute).ToLower();
      if (lower == "1")
        return new bool?(true);
      if (lower == "0")
        return new bool?(false);
      if (lower == "true")
        return new bool?(true);
      if (lower == "false")
        return new bool?(false);
      if (lower == "on")
        return new bool?(true);
      return lower == "off" ? new bool?(false) : new bool?((bool) xelement.Attribute(W.val));
    }
  }
}
