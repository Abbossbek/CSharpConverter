// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ListItemTextGetter_tr_TR
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

namespace CSharpConverter.DocxToHtml.Element
{
  public class ListItemTextGetter_tr_TR
  {
    private static string[] RomanOnes = new string[10]
    {
      "",
      "I",
      "II",
      "III",
      "IV",
      "V",
      "VI",
      "VII",
      "VIII",
      "IX"
    };
    private static string[] RomanTens = new string[10]
    {
      "",
      "X",
      "XX",
      "XXX",
      "XL",
      "L",
      "LX",
      "LXX",
      "LXXX",
      "XC"
    };
    private static string[] RomanHundreds = new string[11]
    {
      "",
      "C",
      "CC",
      "CCC",
      "CD",
      "D",
      "DC",
      "DCC",
      "DCCC",
      "CM",
      "M"
    };
    private static string[] RomanThousands = new string[11]
    {
      "",
      "M",
      "MM",
      "MMM",
      "MMMM",
      "MMMMM",
      "MMMMMM",
      "MMMMMMM",
      "MMMMMMMM",
      "MMMMMMMMM",
      "MMMMMMMMMM"
    };
    private static string[] OneThroughNineteen = new string[19]
    {
      "bir",
      "iki",
      "üç",
      "dört",
      "beş",
      "altı",
      "yedi",
      "sekiz",
      "dokuz",
      "on",
      "onbir",
      "oniki",
      "onüç",
      "ondört",
      "onbeş",
      "onaltı",
      "onyedi",
      "onsekiz",
      "ondokuz"
    };
    private static string[] Tens = new string[9]
    {
      "on",
      "yirmi",
      "otuz",
      "kırk",
      "elli",
      "altmış",
      "yetmiş",
      "seksen",
      "doksan"
    };
    private static string[] OrdinalOneThroughNineteen = new string[19]
    {
      "birinci",
      "ikinci",
      "üçüncü",
      "dördüncü",
      "beşinci",
      "altıncı",
      "yedinci",
      "sekizinci",
      "dokuzuncu",
      "onuncu",
      "onbirinci",
      "onikinci",
      "onüçüncü",
      "ondördüncü",
      "onbeşinci",
      "onaltıncı",
      "onyedinci",
      "onsekizinci",
      "ondokuzuncu"
    };
    private static string[] TwoThroughNineteen = new string[19]
    {
      "",
      "iki",
      "üç",
      "dört",
      "beş",
      "altı",
      "yedi",
      "sekiz",
      "dokuz",
      "on",
      "onbir",
      "oniki",
      "onüç",
      "ondört",
      "onbeş",
      "onaltı",
      "onyedi",
      "onsekiz",
      "ondokuz"
    };
    private static string[] OrdinalTenths = new string[9]
    {
      "onuncu",
      "yirminci",
      "otuzuncu",
      "kırkıncı",
      "ellinci",
      "altmışıncı",
      "yetmişinci",
      "sekseninci",
      "doksanıncı"
    };

    public static string GetListItemText(
      string languageCultureName,
      int levelNumber,
      string numFmt)
    {
      if (numFmt == "decimal")
        return levelNumber.ToString();
      if (numFmt == "decimalZero")
        return levelNumber <= 9 ? "0" + levelNumber.ToString() : levelNumber.ToString();
      if (numFmt == "upperRoman")
      {
        int index1 = levelNumber % 10;
        int index2 = levelNumber % 100 / 10;
        int index3 = levelNumber % 1000 / 100;
        int index4 = levelNumber / 1000;
        return ListItemTextGetter_tr_TR.RomanThousands[index4] + ListItemTextGetter_tr_TR.RomanHundreds[index3] + ListItemTextGetter_tr_TR.RomanTens[index2] + ListItemTextGetter_tr_TR.RomanOnes[index1];
      }
      if (numFmt == "lowerRoman")
      {
        int index5 = levelNumber % 10;
        int index6 = levelNumber % 100 / 10;
        int index7 = levelNumber % 1000 / 100;
        int index8 = levelNumber / 1000;
        return (ListItemTextGetter_tr_TR.RomanThousands[index8] + ListItemTextGetter_tr_TR.RomanHundreds[index7] + ListItemTextGetter_tr_TR.RomanTens[index6] + ListItemTextGetter_tr_TR.RomanOnes[index5]).ToLower();
      }
      if (numFmt == "upperLetter")
        return "".PadRight((levelNumber - 1) / 29 + 1, "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ"[(levelNumber - 1) % 29]);
      if (numFmt == "lowerLetter")
        return "".PadRight((levelNumber - 1) / 29 + 1, "abcçdefgğhıijklmnoöprsştuüvyz"[(levelNumber - 1) % 29]);
      if (numFmt == "ordinal")
      {
        string str = ".";
        return levelNumber.ToString() + str;
      }
      if (numFmt == "cardinalText")
      {
        string str1 = "";
        int num1 = levelNumber / 1000;
        int num2 = levelNumber % 1000;
        if (num1 >= 1)
          str1 = str1 + ListItemTextGetter_tr_TR.OneThroughNineteen[num1 - 1] + " yüz";
        if (num1 >= 1 && num2 == 0)
          return str1.Substring(0, 1).ToUpper() + str1.Substring(1);
        if (num1 >= 1)
          str1 += " ";
        int num3 = levelNumber % 1000 / 100;
        int num4 = levelNumber % 100;
        if (num3 >= 1)
          str1 = str1 + ListItemTextGetter_tr_TR.OneThroughNineteen[num3 - 1] + " bin";
        if (num3 >= 1 && num4 == 0)
          return str1.Substring(0, 1).ToUpper() + str1.Substring(1);
        if (num3 >= 1)
          str1 += " ";
        int num5 = levelNumber % 100;
        string str2;
        if (num5 <= 19)
        {
          str2 = str1 + ListItemTextGetter_tr_TR.OneThroughNineteen[num5 - 1];
        }
        else
        {
          int num6 = num5 / 10;
          int num7 = num5 % 10;
          str2 = str1 + ListItemTextGetter_tr_TR.Tens[num6 - 1];
          if (num7 >= 1)
            str2 += ListItemTextGetter_tr_TR.OneThroughNineteen[num7 - 1];
        }
        return str2.Substring(0, 1).ToUpper() + str2.Substring(1);
      }
      if (numFmt == "ordinalText")
      {
        string str3 = "";
        int num8 = levelNumber / 1000;
        int num9 = levelNumber % 1000;
        if (num8 >= 1 && num9 != 0)
          str3 = str3 + ListItemTextGetter_tr_TR.TwoThroughNineteen[num8 - 1] + "bin";
        if (num8 >= 1 && num9 == 0)
        {
          string str4 = str3 + ListItemTextGetter_tr_TR.TwoThroughNineteen[num8 - 1] + "bininci";
          return str4.Substring(0, 1).ToUpper() + str4.Substring(1);
        }
        int num10 = levelNumber % 1000 / 100;
        int num11 = levelNumber % 100;
        if (num10 >= 1 && num11 != 0)
          str3 = str3 + ListItemTextGetter_tr_TR.TwoThroughNineteen[num10 - 1] + "yüz";
        if (num10 >= 1 && num11 == 0)
        {
          string str5 = str3 + ListItemTextGetter_tr_TR.TwoThroughNineteen[num10 - 1] + "yüzüncü";
          return str5.Substring(0, 1).ToUpper() + str5.Substring(1);
        }
        int num12 = levelNumber % 100;
        string str6;
        if (num12 <= 19)
        {
          str6 = str3 + ListItemTextGetter_tr_TR.OrdinalOneThroughNineteen[num12 - 1];
        }
        else
        {
          int num13 = num12 / 10;
          int num14 = num12 % 10;
          str6 = num14 != 0 ? str3 + ListItemTextGetter_tr_TR.Tens[num13 - 1] : str3 + ListItemTextGetter_tr_TR.OrdinalTenths[num13 - 1];
          if (num14 >= 1)
            str6 += ListItemTextGetter_tr_TR.OrdinalOneThroughNineteen[num14 - 1];
        }
        return str6.Substring(0, 1).ToUpper() + str6.Substring(1);
      }
      if (numFmt == "0001, 0002, 0003, ...")
        return string.Format("{0:0000}", (object) levelNumber);
      return numFmt == "bullet" ? "" : levelNumber.ToString();
    }
  }
}
