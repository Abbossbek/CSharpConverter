// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ColumnReferenceOutOfRange
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;

namespace CSharpConverter.DocxToHtml.Element
{
  public class ColumnReferenceOutOfRange : Exception
  {
    public ColumnReferenceOutOfRange(string columnReference)
      : base(string.Format("Column reference ({0}) is out of range.", (object) columnReference))
    {
    }
  }
}
