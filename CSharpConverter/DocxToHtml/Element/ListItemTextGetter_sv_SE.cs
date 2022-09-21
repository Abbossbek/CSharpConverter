// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ListItemTextGetter_sv_SE
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;

namespace CSharpConverter.DocxToHtml.Element
{
  public class ListItemTextGetter_sv_SE
  {
    private static string[] OneThroughNineteen = new string[20]
    {
      "",
      "ett",
      "två",
      "tre",
      "fyra",
      "fem",
      "sex",
      "sju",
      "åtta",
      "nio",
      "tio",
      "elva",
      "tolv",
      "tretton",
      "fjorton",
      "femton",
      "sexton",
      "sjutton",
      "arton",
      "nitton"
    };
    private static string[] Tens = new string[11]
    {
      "",
      "tio",
      "tjugo",
      "trettio",
      "fyrtio",
      "femtio",
      "sextio",
      "sjuttio",
      "åttio",
      "nittio",
      "etthundra"
    };
    private static string[] OrdinalOneThroughNineteen = new string[20]
    {
      "",
      "första",
      "andra",
      "tredje",
      "fjärde",
      "femte",
      "sjätte",
      "sjunde",
      "åttonde",
      "nionde",
      "tionde",
      "elfte",
      "tolfte",
      "trettonde",
      "fjortonde",
      "femtonde",
      "sextonde",
      "sjuttonde",
      "artonde",
      "nittonde"
    };

    public static string GetListItemText(
      string languageCultureName,
      int levelNumber,
      string numFmt)
    {
      if (numFmt == "cardinalText")
        return ListItemTextGetter_sv_SE.NumberAsCardinalText(languageCultureName, levelNumber, numFmt);
      if (numFmt == "ordinalText")
        return ListItemTextGetter_sv_SE.NumberAsOrdinalText(languageCultureName, levelNumber, numFmt);
      return numFmt == "ordinal" ? ListItemTextGetter_sv_SE.NumberAsOrdinal(languageCultureName, levelNumber, numFmt) : (string) null;
    }

    private static string NumberAsCardinalText(
      string languageCultureName,
      int levelNumber,
      string numFmt)
    {
      string str1 = "";
      string str2 = (levelNumber + 10000).ToString();
      int index1 = int.Parse(str2.Substring(1, 1));
      int index2 = int.Parse(str2.Substring(2, 1));
      int index3 = int.Parse(str2.Substring(3, 1));
      int index4 = int.Parse(str2.Substring(4, 1));
      if (index1 > 19)
        throw new ArgumentOutOfRangeException(nameof (levelNumber), "Convering a levelNumber to ordinal text that is greater then 19 999 is not supported");
      if (levelNumber == 0)
        return "Noll";
      if (levelNumber < 0)
        throw new ArgumentOutOfRangeException(nameof (levelNumber), "Converting a negative levelNumber to ordinal text is not supported");
      if (levelNumber == 1000)
        return "Ettusen";
      if (levelNumber > 1000 && index2 == 0 && index3 == 0 && index4 == 0)
      {
        string str3 = ListItemTextGetter_sv_SE.OneThroughNineteen[index1] + "tusen";
        return str3.Substring(0, 1).ToUpper() + str3.Substring(1);
      }
      if (levelNumber > 1000 && levelNumber < 2000)
        str1 = "ettusen";
      else if (levelNumber > 2000 && levelNumber < 10000)
        str1 = ListItemTextGetter_sv_SE.OneThroughNineteen[index1] + "tusen";
      if (index2 > 0 && index3 == 0 && index4 == 0)
      {
        string str4 = index2 != 1 ? str1 + ListItemTextGetter_sv_SE.OneThroughNineteen[index2] + "hundra" : str1 + "etthundra";
        return str4.Substring(0, 1).ToUpper() + str4.Substring(1);
      }
      if (index2 > 0)
        str1 = index2 != 1 ? str1 + ListItemTextGetter_sv_SE.OneThroughNineteen[index2] + "hundra" : str1 + "etthundra";
      if (index3 > 0 && index4 == 0)
      {
        string str5 = str1 + ListItemTextGetter_sv_SE.Tens[index3];
        return str5.Substring(0, 1).ToUpper() + str5.Substring(1);
      }
      if (index3 == 1)
      {
        string str6 = str1 + ListItemTextGetter_sv_SE.OneThroughNineteen[index3 * 10 + index4];
        return str6.Substring(0, 1).ToUpper() + str6.Substring(1);
      }
      if (index3 > 1)
      {
        string str7 = str1 + ListItemTextGetter_sv_SE.Tens[index3] + ListItemTextGetter_sv_SE.OneThroughNineteen[index4];
        return str7.Substring(0, 1).ToUpper() + str7.Substring(1);
      }
      string str8 = str1 + ListItemTextGetter_sv_SE.OneThroughNineteen[index4];
      return str8.Substring(0, 1).ToUpper() + str8.Substring(1);
    }

    private static string NumberAsOrdinalText(
      string languageCultureName,
      int levelNumber,
      string numFmt)
    {
      string str1 = "";
      if (levelNumber <= 0)
        throw new ArgumentOutOfRangeException(nameof (levelNumber), "Converting a zero or negative levelNumber to ordinal text is not supported");
      if (levelNumber >= 10000)
        throw new ArgumentOutOfRangeException(nameof (levelNumber), "Convering a levelNumber to ordinal text that is greater then 10000 is not supported");
      if (levelNumber == 1)
        return "Första";
      string str2 = (levelNumber + 10000).ToString();
      int index1 = int.Parse(str2.Substring(1, 1));
      int index2 = int.Parse(str2.Substring(2, 1));
      int index3 = int.Parse(str2.Substring(3, 1));
      int index4 = int.Parse(str2.Substring(4, 1));
      if (levelNumber == 1000)
        return "Ettusende";
      if (levelNumber > 1000 && index2 == 0 && index3 == 0 && index4 == 0)
      {
        string str3 = ListItemTextGetter_sv_SE.OneThroughNineteen[index1] + "tusende";
        return str3.Substring(0, 1).ToUpper() + str3.Substring(1);
      }
      if (levelNumber > 1000 && levelNumber < 2000)
        str1 = "ettusen";
      else if (levelNumber > 2000 && levelNumber < 10000)
        str1 = ListItemTextGetter_sv_SE.OneThroughNineteen[index1] + "tusende";
      if (index2 > 0 && index3 == 0 && index4 == 0)
      {
        string str4 = index2 != 1 ? str1 + ListItemTextGetter_sv_SE.OneThroughNineteen[index2] + "hundrade" : str1 + "etthundrade";
        return str4.Substring(0, 1).ToUpper() + str4.Substring(1);
      }
      if (index2 > 0)
        str1 = str1 + ListItemTextGetter_sv_SE.OneThroughNineteen[index2] + "hundra";
      if (index3 > 0 && index4 == 0)
      {
        string str5 = str1 + ListItemTextGetter_sv_SE.Tens[index3] + "nde";
        return str5.Substring(0, 1).ToUpper() + str5.Substring(1);
      }
      if (index3 == 1)
      {
        string str6 = str1 + ListItemTextGetter_sv_SE.OrdinalOneThroughNineteen[index3 * 10 + index4];
        return str6.Substring(0, 1).ToUpper() + str6.Substring(1);
      }
      if (index3 > 1)
      {
        string str7 = str1 + ListItemTextGetter_sv_SE.Tens[index3] + ListItemTextGetter_sv_SE.OrdinalOneThroughNineteen[index4];
        return str7.Substring(0, 1).ToUpper() + str7.Substring(1);
      }
      string str8 = str1 + ListItemTextGetter_sv_SE.OrdinalOneThroughNineteen[index4];
      return str8.Substring(0, 1).ToUpper() + str8.Substring(1);
    }

    private static string NumberAsOrdinal(
      string languageCultureName,
      int levelNumber,
      string numFmt)
    {
      string str = levelNumber.ToString();
      if (str == null || str.Trim() == "")
        return "";
      return str.EndsWith("1") || str.EndsWith("2") ? str + ":a" : str + ":e";
    }
  }
}
