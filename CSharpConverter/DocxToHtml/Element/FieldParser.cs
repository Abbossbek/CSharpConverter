// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.FieldParser
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class FieldParser
  {
    private static string[] GetTokens(string field)
    {
      FieldParser.State state = FieldParser.State.InWhiteSpace;
      int startIndex = 0;
      char minValue = char.MinValue;
      List<string> stringList = new List<string>();
      for (int index = 0; index < field.Length; ++index)
      {
        if (char.IsWhiteSpace(field[index]))
        {
          if (state == FieldParser.State.InToken)
          {
            stringList.Add(field.Substring(startIndex, index - startIndex));
            state = FieldParser.State.InWhiteSpace;
          }
          else
          {
            if (state == FieldParser.State.OnOpeningQuote)
            {
              startIndex = index;
              state = FieldParser.State.InQuotedToken;
            }
            if (state == FieldParser.State.OnClosingQuote)
              state = FieldParser.State.InWhiteSpace;
          }
        }
        else if (field[index] == '\\' && state == FieldParser.State.InQuotedToken)
          state = FieldParser.State.OnBackslash;
        else if (state == FieldParser.State.OnBackslash)
          state = FieldParser.State.InQuotedToken;
        else if (field[index] == '"' || field[index] == '\'' || field[index] == '”')
        {
          switch (state)
          {
            case FieldParser.State.InWhiteSpace:
              minValue = field[index];
              state = FieldParser.State.OnOpeningQuote;
              continue;
            case FieldParser.State.InQuotedToken:
              if ((int) field[index] == (int) minValue)
              {
                stringList.Add(field.Substring(startIndex, index - startIndex));
                state = FieldParser.State.OnClosingQuote;
                continue;
              }
              continue;
            case FieldParser.State.OnOpeningQuote:
              if ((int) field[index] == (int) minValue)
              {
                state = FieldParser.State.OnClosingQuote;
                continue;
              }
              startIndex = index;
              state = FieldParser.State.InQuotedToken;
              continue;
            default:
              continue;
          }
        }
        else
        {
          switch (state)
          {
            case FieldParser.State.InWhiteSpace:
              startIndex = index;
              state = FieldParser.State.InToken;
              continue;
            case FieldParser.State.OnOpeningQuote:
              startIndex = index;
              state = FieldParser.State.InQuotedToken;
              continue;
            case FieldParser.State.OnClosingQuote:
              startIndex = index;
              state = FieldParser.State.InToken;
              continue;
            default:
              continue;
          }
        }
      }
      if (state == FieldParser.State.InToken)
        stringList.Add(field.Substring(startIndex, field.Length - startIndex));
      return stringList.ToArray();
    }

    public static FieldInfo ParseField(string field)
    {
      FieldInfo field1 = new FieldInfo()
      {
        FieldType = "",
        Arguments = new string[0],
        Switches = new string[0]
      };
      if (field.Length == 0)
        return field1;
      string str = ((IEnumerable<string>) field.TrimStart().Split(' ')).FirstOrDefault<string>();
      if (str == null || str.ToUpper() != "HYPERLINK")
        return field1;
      string[] tokens = FieldParser.GetTokens(field);
      if (tokens.Length == 0)
        return field1;
      return new FieldInfo()
      {
        FieldType = tokens[0],
        Switches = ((IEnumerable<string>) tokens).Where<string>((Func<string, bool>) (t => t[0] == '\\')).ToArray<string>(),
        Arguments = ((IEnumerable<string>) tokens).Skip<string>(1).Where<string>((Func<string, bool>) (t => t[0] != '\\')).ToArray<string>()
      };
    }

    private enum State
    {
      InToken,
      InWhiteSpace,
      InQuotedToken,
      OnOpeningQuote,
      OnClosingQuote,
      OnBackslash,
    }
  }
}
