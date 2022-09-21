// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ListItemTextGetter_fr_FR
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

namespace CSharpConverter.DocxToHtml.Element
{
  public class ListItemTextGetter_fr_FR
  {
    private static string[] OneThroughNineteen = new string[20]
    {
      "",
      "un",
      "deux",
      "trois",
      "quatre",
      "cinq",
      "six",
      "sept",
      "huit",
      "neuf",
      "dix",
      "onze",
      "douze",
      "treize",
      "quatorze",
      "quinze",
      "seize",
      "dix-sept",
      "dix-huit",
      "dix-neuf"
    };
    private static string[] Tens = new string[10]
    {
      "",
      "dix",
      "vingt",
      "trente",
      "quarante",
      "cinquante",
      "soixante",
      "soixante-dix",
      "quatre-vingt",
      "quatre-vingt-dix"
    };
    private static string[] OrdinalOneThroughNineteen = new string[20]
    {
      "",
      "unième",
      "deuxième",
      "troisième",
      "quatrième",
      "cinquième",
      "sixième",
      "septième",
      "huitième",
      "neuvième",
      "dixième",
      "onzième",
      "douzième",
      "treizième",
      "quatorzième",
      "quinzième",
      "seizième",
      "dix-septième",
      "dix-huitième",
      "dix-neuvième"
    };
    private static string[] OrdinalTenths = new string[10]
    {
      "",
      "dixième",
      "vingtième",
      "trentième",
      "quarantième",
      "cinquantième",
      "soixantième",
      "soixante-dixième",
      "quatre-vingtième",
      "quatre-vingt-dixième"
    };
    private static string[] OrdinalTenthsPlus = new string[10]
    {
      "",
      "",
      "vingt",
      "trente",
      "quarante",
      "cinquante",
      "soixante",
      "",
      "quatre-vingt",
      ""
    };

    public static string GetListItemText(
      string languageCultureName,
      int levelNumber,
      string numFmt)
    {
      if (numFmt == "cardinalText")
      {
        string str1 = "";
        string str2 = (levelNumber + 10000).ToString();
        int index1 = int.Parse(str2.Substring(1, 1));
        int index2 = int.Parse(str2.Substring(2, 1));
        int index3 = int.Parse(str2.Substring(3, 1));
        int index4 = int.Parse(str2.Substring(4, 1));
        if (levelNumber == 1000)
          return "Mille";
        if (levelNumber > 1000 && index2 == 0 && index3 == 0 && index4 == 0)
        {
          string str3 = ListItemTextGetter_fr_FR.OneThroughNineteen[index1] + " mille";
          return str3.Substring(0, 1).ToUpper() + str3.Substring(1);
        }
        if (levelNumber > 1000 && levelNumber < 2000)
          str1 = "mille ";
        else if (levelNumber > 2000 && levelNumber < 10000)
          str1 = ListItemTextGetter_fr_FR.OneThroughNineteen[index1] + " mille ";
        if (index2 > 0 && index3 == 0 && index4 == 0)
        {
          string str4 = index2 != 1 ? str1 + ListItemTextGetter_fr_FR.OneThroughNineteen[index2] + " cents" : str1 + "cent";
          return str4.Substring(0, 1).ToUpper() + str4.Substring(1);
        }
        if (index2 > 0)
          str1 = index2 != 1 ? str1 + ListItemTextGetter_fr_FR.OneThroughNineteen[index2] + " cent " : str1 + "cent ";
        if (index3 > 0 && index4 == 0)
        {
          string str5 = index3 != 8 ? str1 + ListItemTextGetter_fr_FR.Tens[index3] : str1 + "quatre-vingts";
          return str5.Substring(0, 1).ToUpper() + str5.Substring(1);
        }
        if (index3 == 7)
        {
          string str6 = index4 != 1 ? str1 + ListItemTextGetter_fr_FR.Tens[6] + "-" + ListItemTextGetter_fr_FR.OneThroughNineteen[index4 + 10] : str1 + ListItemTextGetter_fr_FR.Tens[6] + " et " + ListItemTextGetter_fr_FR.OneThroughNineteen[index4 + 10];
          return str6.Substring(0, 1).ToUpper() + str6.Substring(1);
        }
        if (index3 == 9)
        {
          string str7 = str1 + ListItemTextGetter_fr_FR.Tens[8] + "-" + ListItemTextGetter_fr_FR.OneThroughNineteen[index4 + 10];
          return str7.Substring(0, 1).ToUpper() + str7.Substring(1);
        }
        if (index3 >= 2)
        {
          string str8 = index4 != 1 || index3 == 8 || index3 == 9 ? str1 + ListItemTextGetter_fr_FR.Tens[index3] + "-" + ListItemTextGetter_fr_FR.OneThroughNineteen[index4] : str1 + ListItemTextGetter_fr_FR.Tens[index3] + " et un";
          return str8.Substring(0, 1).ToUpper() + str8.Substring(1);
        }
        string str9 = str1 + ListItemTextGetter_fr_FR.OneThroughNineteen[index3 * 10 + index4];
        return str9.Substring(0, 1).ToUpper() + str9.Substring(1);
      }
      if (numFmt == "ordinal")
      {
        string str = levelNumber != 1 ? "e" : "er";
        return levelNumber.ToString() + str;
      }
      if (!(numFmt == "ordinalText"))
        return (string) null;
      string str10 = "";
      if (levelNumber == 1)
        return "Premier";
      string str11 = (levelNumber + 10000).ToString();
      int index5 = int.Parse(str11.Substring(1, 1));
      int index6 = int.Parse(str11.Substring(2, 1));
      int index7 = int.Parse(str11.Substring(3, 1));
      int index8 = int.Parse(str11.Substring(4, 1));
      if (levelNumber == 1000)
        return "Millième";
      if (levelNumber > 1000 && index6 == 0 && index7 == 0 && index8 == 0)
      {
        string str12 = ListItemTextGetter_fr_FR.OneThroughNineteen[index5] + " millième";
        return str12.Substring(0, 1).ToUpper() + str12.Substring(1);
      }
      if (levelNumber > 1000 && levelNumber < 2000)
        str10 = "mille ";
      else if (levelNumber > 2000 && levelNumber < 10000)
        str10 = ListItemTextGetter_fr_FR.OneThroughNineteen[index5] + " mille ";
      if (index6 > 0 && index7 == 0 && index8 == 0)
      {
        string str13 = index6 != 1 ? str10 + ListItemTextGetter_fr_FR.OneThroughNineteen[index6] + " centième" : str10 + "centième";
        return str13.Substring(0, 1).ToUpper() + str13.Substring(1);
      }
      if (index6 > 0)
        str10 = index6 != 1 ? str10 + ListItemTextGetter_fr_FR.OneThroughNineteen[index6] + " cent " : str10 + "cent ";
      if (index7 > 0 && index8 == 0)
      {
        string str14 = str10 + ListItemTextGetter_fr_FR.OrdinalTenths[index7];
        return str14.Substring(0, 1).ToUpper() + str14.Substring(1);
      }
      if (index7 == 7)
      {
        string str15 = index8 != 1 ? str10 + ListItemTextGetter_fr_FR.OrdinalTenthsPlus[6] + "-" + ListItemTextGetter_fr_FR.OrdinalOneThroughNineteen[index8 + 10] : str10 + ListItemTextGetter_fr_FR.OrdinalTenthsPlus[6] + " et " + ListItemTextGetter_fr_FR.OrdinalOneThroughNineteen[index8 + 10];
        return str15.Substring(0, 1).ToUpper() + str15.Substring(1);
      }
      if (index7 == 9)
      {
        string str16 = str10 + ListItemTextGetter_fr_FR.OrdinalTenthsPlus[8] + "-" + ListItemTextGetter_fr_FR.OrdinalOneThroughNineteen[index8 + 10];
        return str16.Substring(0, 1).ToUpper() + str16.Substring(1);
      }
      if (index7 >= 2)
      {
        string str17 = index8 != 1 || index7 == 8 || index7 == 9 ? str10 + ListItemTextGetter_fr_FR.OrdinalTenthsPlus[index7] + "-" + ListItemTextGetter_fr_FR.OrdinalOneThroughNineteen[index8] : str10 + ListItemTextGetter_fr_FR.OrdinalTenthsPlus[index7] + " et unième";
        return str17.Substring(0, 1).ToUpper() + str17.Substring(1);
      }
      string str18 = str10 + ListItemTextGetter_fr_FR.OrdinalOneThroughNineteen[index7 * 10 + index8];
      return str18.Substring(0, 1).ToUpper() + str18.Substring(1);
    }
  }
}
