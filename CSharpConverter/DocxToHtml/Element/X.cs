// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.X
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class X
  {
    public static readonly XNamespace x = (XNamespace) "urn:schemas-microsoft-com:office:excel";
    public static readonly XName Anchor = X.x + nameof (Anchor);
    public static readonly XName AutoFill = X.x + nameof (AutoFill);
    public static readonly XName ClientData = X.x + nameof (ClientData);
    public static readonly XName Column = X.x + nameof (Column);
    public static readonly XName MoveWithCells = X.x + nameof (MoveWithCells);
    public static readonly XName Row = X.x + nameof (Row);
    public static readonly XName SizeWithCells = X.x + nameof (SizeWithCells);
  }
}
