// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.SpreadsheetMLUtil
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class SpreadsheetMLUtil
  {
    public static string GetCellType(string value) => value.Any<char>((Func<char, bool>) (c => !char.IsDigit(c) && c != '.')) ? "str" : (string) null;

    public static string IntToColumnId(int i)
    {
      if (i >= 0 && i <= 25)
        return ((char) (65 + i)).ToString();
      if (i >= 26 && i <= 701)
      {
        int num1 = i - 26;
        int num2 = num1 / 26;
        int num3 = num1 % 26;
        return ((char) (65 + num2)).ToString() + ((char) (65 + num3)).ToString();
      }
      if (i < 702 || i > 18277)
        throw new ColumnReferenceOutOfRange(i.ToString());
      int num4 = i - 702;
      int num5 = num4 / 676;
      int num6 = num4 % 676;
      int num7 = num6 / 26;
      int num8 = num6 % 26;
      return ((char) (65 + num5)).ToString() + ((char) (65 + num7)).ToString() + ((char) (65 + num8)).ToString();
    }

    private static int CharToInt(char c) => (int) c - 65;

    public static int ColumnIdToInt(string cid)
    {
      if (cid.Length == 1)
        return SpreadsheetMLUtil.CharToInt(cid[0]);
      if (cid.Length == 2)
        return SpreadsheetMLUtil.CharToInt(cid[0]) * 26 + SpreadsheetMLUtil.CharToInt(cid[1]) + 26;
      if (cid.Length == 3)
        return SpreadsheetMLUtil.CharToInt(cid[0]) * 676 + SpreadsheetMLUtil.CharToInt(cid[1]) * 26 + SpreadsheetMLUtil.CharToInt(cid[2]) + 702;
      throw new ColumnReferenceOutOfRange(cid);
    }

    public static IEnumerable<string> ColumnIDs()
    {
      for (int c = 65; c <= 90; ++c)
        yield return ((char) c).ToString();
      for (int c1 = 65; c1 <= 90; ++c1)
      {
        for (int c2 = 65; c2 <= 90; ++c2)
          yield return ((char) c1).ToString() + ((char) c2).ToString();
      }
      for (int d1 = 65; d1 <= 90; ++d1)
      {
        for (int d2 = 65; d2 <= 90; ++d2)
        {
          for (int d3 = 65; d3 <= 90; ++d3)
            yield return ((char) d1).ToString() + ((char) d2).ToString() + ((char) d3).ToString();
        }
      }
    }

    public static string ColumnIdOf(string cellReference) => ((IEnumerable<string>) cellReference.Split('0', '1', '2', '3', '4', '5', '6', '7', '8', '9')).First<string>();
  }
}
