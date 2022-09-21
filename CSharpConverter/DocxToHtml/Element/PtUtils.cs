// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.PtUtils
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Linq;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class PtUtils
  {
    public static string NormalizeDirName(string dirName)
    {
      string str = dirName.Replace('\\', '/');
      return str[dirName.Length - 1] != '/' && str[dirName.Length - 1] != '\\' ? str + "/" : str;
    }

    public static string MakeValidXml(string p) => !p.Any<char>((Func<char, bool>) (c => c < ' ')) ? p : p.Select<char, string>((Func<char, string>) (c => c >= ' ' ? c.ToString() : string.Format("_{0:X}_", (object) (int) c))).StringConcatenate();

    public static void AddElementIfMissing(
      XDocument partXDoc,
      XElement existing,
      string newElement)
    {
      if (existing != null)
        return;
      XElement content = XElement.Parse(newElement);
      content.Attributes().Where<XAttribute>((Func<XAttribute, bool>) (a => a.IsNamespaceDeclaration)).Remove();
      if (partXDoc.Root == null)
        return;
      partXDoc.Root.Add((object) content);
    }
  }
}
