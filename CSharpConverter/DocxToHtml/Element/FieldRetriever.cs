// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.FieldRetriever
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public class FieldRetriever
  {
    public static string InstrText(XElement root, int id)
    {
      XNamespace w = (XNamespace) "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
      Dictionary<int, List<XElement>> dictionary = root.Annotation<Dictionary<int, List<XElement>>>();
      if (dictionary == null)
        throw new DocxToHtmlException("Internal error");
      return !dictionary.ContainsKey(id) ? "" : "{" + dictionary[id].GroupAdjacent<XElement, bool>((Func<XElement, bool>) (e =>
      {
        Stack<FieldRetriever.FieldElementTypeInfo> source = e.Annotation<Stack<FieldRetriever.FieldElementTypeInfo>>();
        FieldRetriever.FieldElementTypeInfo stackElement = source.FirstOrDefault<FieldRetriever.FieldElementTypeInfo>((Func<FieldRetriever.FieldElementTypeInfo, bool>) (z => z.Id == id));
        return source.TakeWhile<FieldRetriever.FieldElementTypeInfo>((Func<FieldRetriever.FieldElementTypeInfo, bool>) (z => z != stackElement)).Any<FieldRetriever.FieldElementTypeInfo>();
      })).ToList<IGrouping<bool, XElement>>().Select<IGrouping<bool, XElement>, string>((Func<IGrouping<bool, XElement>, string>) (g =>
      {
        if (!g.Key)
          return g.Select<XElement, string>((Func<XElement, string>) (e => e.Annotation<Stack<FieldRetriever.FieldElementTypeInfo>>().FirstOrDefault<FieldRetriever.FieldElementTypeInfo>((Func<FieldRetriever.FieldElementTypeInfo, bool>) (z => z.Id == id)).FieldElementType == FieldRetriever.FieldElementTypeEnum.InstrText && e.Name == w + "instrText" ? e.Value : "")).StringConcatenate();
        Stack<FieldRetriever.FieldElementTypeInfo> source = g.First<XElement>().Annotation<Stack<FieldRetriever.FieldElementTypeInfo>>();
        FieldRetriever.FieldElementTypeInfo stackElement = source.FirstOrDefault<FieldRetriever.FieldElementTypeInfo>((Func<FieldRetriever.FieldElementTypeInfo, bool>) (z => z.Id == id));
        return FieldRetriever.InstrText(root, source.TakeWhile<FieldRetriever.FieldElementTypeInfo>((Func<FieldRetriever.FieldElementTypeInfo, bool>) (z => z != stackElement)).Last<FieldRetriever.FieldElementTypeInfo>().Id);
      })).StringConcatenate() + "}";
    }

    public static void AnnotateWithFieldInfo(OpenXmlPart part)
    {
      XNamespace w = (XNamespace) "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
      XElement root = part.GetXDocument().Root;
      IEnumerable<XElement> source1 = root.DescendantsAndSelf();
      FieldRetriever.FieldElementTypeStack seed = new FieldRetriever.FieldElementTypeStack();
      seed.Id = 0;
      seed.FiStack = (Stack<FieldRetriever.FieldElementTypeInfo>) null;
      Func<XElement, FieldRetriever.FieldElementTypeStack, FieldRetriever.FieldElementTypeStack> projection = (Func<XElement, FieldRetriever.FieldElementTypeStack, FieldRetriever.FieldElementTypeStack>) ((e, s) =>
      {
        if (e.Name == w + "fldChar")
        {
          if (e.Attribute(w + "fldCharType").Value == "begin")
          {
            Stack<FieldRetriever.FieldElementTypeInfo> fieldElementTypeInfoStack = s.FiStack != null ? new Stack<FieldRetriever.FieldElementTypeInfo>(s.FiStack.Reverse<FieldRetriever.FieldElementTypeInfo>()) : new Stack<FieldRetriever.FieldElementTypeInfo>();
            fieldElementTypeInfoStack.Push(new FieldRetriever.FieldElementTypeInfo()
            {
              Id = s.Id + 1,
              FieldElementType = FieldRetriever.FieldElementTypeEnum.Begin
            });
            return new FieldRetriever.FieldElementTypeStack()
            {
              Id = s.Id + 1,
              FiStack = fieldElementTypeInfoStack
            };
          }
          if (e.Attribute(w + "fldCharType").Value == "separate")
          {
            Stack<FieldRetriever.FieldElementTypeInfo> fieldElementTypeInfoStack = new Stack<FieldRetriever.FieldElementTypeInfo>(s.FiStack.Reverse<FieldRetriever.FieldElementTypeInfo>());
            FieldRetriever.FieldElementTypeInfo fieldElementTypeInfo = fieldElementTypeInfoStack.Pop();
            fieldElementTypeInfoStack.Push(new FieldRetriever.FieldElementTypeInfo()
            {
              Id = fieldElementTypeInfo.Id,
              FieldElementType = FieldRetriever.FieldElementTypeEnum.Separate
            });
            return new FieldRetriever.FieldElementTypeStack()
            {
              Id = s.Id,
              FiStack = fieldElementTypeInfoStack
            };
          }
          if (e.Attribute(w + "fldCharType").Value == "end")
          {
            Stack<FieldRetriever.FieldElementTypeInfo> fieldElementTypeInfoStack = new Stack<FieldRetriever.FieldElementTypeInfo>(s.FiStack.Reverse<FieldRetriever.FieldElementTypeInfo>());
            fieldElementTypeInfoStack.Pop();
            return new FieldRetriever.FieldElementTypeStack()
            {
              Id = s.Id,
              FiStack = fieldElementTypeInfoStack
            };
          }
        }
        if (s.FiStack == null || s.FiStack.Count<FieldRetriever.FieldElementTypeInfo>() == 0)
          return s;
        FieldRetriever.FieldElementTypeInfo fieldElementTypeInfo1 = s.FiStack.Peek();
        if (fieldElementTypeInfo1.FieldElementType == FieldRetriever.FieldElementTypeEnum.Begin)
        {
          Stack<FieldRetriever.FieldElementTypeInfo> fieldElementTypeInfoStack = new Stack<FieldRetriever.FieldElementTypeInfo>(s.FiStack.Reverse<FieldRetriever.FieldElementTypeInfo>());
          FieldRetriever.FieldElementTypeInfo fieldElementTypeInfo2 = fieldElementTypeInfoStack.Pop();
          fieldElementTypeInfoStack.Push(new FieldRetriever.FieldElementTypeInfo()
          {
            Id = fieldElementTypeInfo2.Id,
            FieldElementType = FieldRetriever.FieldElementTypeEnum.InstrText
          });
          return new FieldRetriever.FieldElementTypeStack()
          {
            Id = s.Id,
            FiStack = fieldElementTypeInfoStack
          };
        }
        if (fieldElementTypeInfo1.FieldElementType == FieldRetriever.FieldElementTypeEnum.Separate)
        {
          Stack<FieldRetriever.FieldElementTypeInfo> fieldElementTypeInfoStack = new Stack<FieldRetriever.FieldElementTypeInfo>(s.FiStack.Reverse<FieldRetriever.FieldElementTypeInfo>());
          FieldRetriever.FieldElementTypeInfo fieldElementTypeInfo3 = fieldElementTypeInfoStack.Pop();
          fieldElementTypeInfoStack.Push(new FieldRetriever.FieldElementTypeInfo()
          {
            Id = fieldElementTypeInfo3.Id,
            FieldElementType = FieldRetriever.FieldElementTypeEnum.Result
          });
          return new FieldRetriever.FieldElementTypeStack()
          {
            Id = s.Id,
            FiStack = fieldElementTypeInfoStack
          };
        }
        if (fieldElementTypeInfo1.FieldElementType != FieldRetriever.FieldElementTypeEnum.End)
          return s;
        Stack<FieldRetriever.FieldElementTypeInfo> source2 = new Stack<FieldRetriever.FieldElementTypeInfo>(s.FiStack.Reverse<FieldRetriever.FieldElementTypeInfo>());
        source2.Pop();
        if (!source2.Any<FieldRetriever.FieldElementTypeInfo>())
          source2 = (Stack<FieldRetriever.FieldElementTypeInfo>) null;
        return new FieldRetriever.FieldElementTypeStack()
        {
          Id = s.Id,
          FiStack = source2
        };
      });
      IEnumerable<FieldRetriever.FieldElementTypeStack> second = source1.Rollup<XElement, FieldRetriever.FieldElementTypeStack>(seed, projection);
      foreach (var data in root.DescendantsAndSelf().PtZip(second, (t1, t2) => new
      {
        Element = t1,
        Id = t2.Id,
        WmlFieldInfoStack = t2.FiStack
      }))
      {
        if (data.WmlFieldInfoStack != null)
          data.Element.AddAnnotation((object) data.WmlFieldInfoStack);
      }
      Dictionary<int, List<XElement>> annotation = new Dictionary<int, List<XElement>>();
      foreach (XElement xelement in root.DescendantsTrimmed((Func<XElement, bool>) (d => d.Name == W.rPr || d.Name == W.pPr)))
      {
        Stack<FieldRetriever.FieldElementTypeInfo> fieldElementTypeInfoStack = xelement.Annotation<Stack<FieldRetriever.FieldElementTypeInfo>>();
        if (fieldElementTypeInfoStack != null)
        {
          foreach (FieldRetriever.FieldElementTypeInfo fieldElementTypeInfo in fieldElementTypeInfoStack)
          {
            if (fieldElementTypeInfo.FieldElementType == FieldRetriever.FieldElementTypeEnum.InstrText)
            {
              if (annotation.ContainsKey(fieldElementTypeInfo.Id))
                annotation[fieldElementTypeInfo.Id].Add(xelement);
              else
                annotation.Add(fieldElementTypeInfo.Id, new List<XElement>()
                {
                  xelement
                });
            }
          }
        }
      }
      root.AddAnnotation((object) annotation);
    }

    private static string[] GetTokens(string field)
    {
      FieldRetriever.State state = FieldRetriever.State.InWhiteSpace;
      int startIndex = 0;
      char minValue = char.MinValue;
      List<string> stringList = new List<string>();
      for (int index = 0; index < field.Length; ++index)
      {
        if (char.IsWhiteSpace(field[index]))
        {
          if (state == FieldRetriever.State.InToken)
          {
            stringList.Add(field.Substring(startIndex, index - startIndex));
            state = FieldRetriever.State.InWhiteSpace;
          }
          else
          {
            if (state == FieldRetriever.State.OnOpeningQuote)
            {
              startIndex = index;
              state = FieldRetriever.State.InQuotedToken;
            }
            if (state == FieldRetriever.State.OnClosingQuote)
              state = FieldRetriever.State.InWhiteSpace;
          }
        }
        else if (field[index] == '\\' && state == FieldRetriever.State.InQuotedToken)
          state = FieldRetriever.State.OnBackslash;
        else if (state == FieldRetriever.State.OnBackslash)
          state = FieldRetriever.State.InQuotedToken;
        else if (field[index] == '"' || field[index] == '\'' || field[index] == '”')
        {
          switch (state)
          {
            case FieldRetriever.State.InWhiteSpace:
              minValue = field[index];
              state = FieldRetriever.State.OnOpeningQuote;
              continue;
            case FieldRetriever.State.InQuotedToken:
              if ((int) field[index] == (int) minValue)
              {
                stringList.Add(field.Substring(startIndex, index - startIndex));
                state = FieldRetriever.State.OnClosingQuote;
                continue;
              }
              continue;
            case FieldRetriever.State.OnOpeningQuote:
              if ((int) field[index] == (int) minValue)
              {
                state = FieldRetriever.State.OnClosingQuote;
                continue;
              }
              startIndex = index;
              state = FieldRetriever.State.InQuotedToken;
              continue;
            default:
              continue;
          }
        }
        else
        {
          switch (state)
          {
            case FieldRetriever.State.InWhiteSpace:
              startIndex = index;
              state = FieldRetriever.State.InToken;
              continue;
            case FieldRetriever.State.OnOpeningQuote:
              startIndex = index;
              state = FieldRetriever.State.InQuotedToken;
              continue;
            case FieldRetriever.State.OnClosingQuote:
              startIndex = index;
              state = FieldRetriever.State.InToken;
              continue;
            default:
              continue;
          }
        }
      }
      if (state == FieldRetriever.State.InToken)
        stringList.Add(field.Substring(startIndex, field.Length - startIndex));
      return stringList.ToArray();
    }

    public static FieldRetriever.FieldInfo ParseField(string field)
    {
      FieldRetriever.FieldInfo field1 = new FieldRetriever.FieldInfo()
      {
        FieldType = "",
        Arguments = new string[0],
        Switches = new string[0]
      };
      if (field.Length == 0)
        return field1;
      string str = ((IEnumerable<string>) field.TrimStart().Split(' ')).FirstOrDefault<string>();
      if (str == null || str.ToUpper() != "HYPERLINK" && str.ToUpper() != "SEQ" && str.ToUpper() != "STYLEREF")
        return field1;
      string[] tokens = FieldRetriever.GetTokens(field);
      if (tokens.Length == 0)
        return field1;
      return new FieldRetriever.FieldInfo()
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

    public class FieldInfo
    {
      public string FieldType;
      public string[] Switches;
      public string[] Arguments;
    }

    public enum FieldElementTypeEnum
    {
      Begin,
      InstrText,
      Separate,
      Result,
      End,
    }

    public class FieldElementTypeInfo
    {
      public int Id;
      public FieldRetriever.FieldElementTypeEnum FieldElementType;
    }

    public class FieldElementTypeStack
    {
      public int Id;
      public Stack<FieldRetriever.FieldElementTypeInfo> FiStack;
    }
  }
}
