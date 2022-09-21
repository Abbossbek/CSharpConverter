// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ListItemTextGetter_Default
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

namespace CSharpConverter.DocxToHtml.Element
{
  internal class ListItemTextGetter_Default
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
      "one",
      "two",
      "three",
      "four",
      "five",
      "six",
      "seven",
      "eight",
      "nine",
      "ten",
      "eleven",
      "twelve",
      "thirteen",
      "fourteen",
      "fifteen",
      "sixteen",
      "seventeen",
      "eighteen",
      "nineteen"
    };
    private static string[] Tens = new string[9]
    {
      "ten",
      "twenty",
      "thirty",
      "forty",
      "fifty",
      "sixty",
      "seventy",
      "eighty",
      "ninety"
    };
    private static string[] OrdinalOneThroughNineteen = new string[19]
    {
      "first",
      "second",
      "third",
      "fourth",
      "fifth",
      "sixth",
      "seventh",
      "eighth",
      "ninth",
      "tenth",
      "eleventh",
      "twelfth",
      "thirteenth",
      "fourteenth",
      "fifteenth",
      "sixteenth",
      "seventeenth",
      "eighteenth",
      "nineteenth"
    };
    private static string[] OrdinalTenths = new string[9]
    {
      "tenth",
      "twentieth",
      "thirtieth",
      "fortieth",
      "fiftieth",
      "sixtieth",
      "seventieth",
      "eightieth",
      "ninetieth"
    };

    public static string GetListItemText(
      string languageCultureName,
      int levelNumber,
      string numFmt)
    {
      if (numFmt == "none")
        return "";
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
        return ListItemTextGetter_Default.RomanThousands[index4] + ListItemTextGetter_Default.RomanHundreds[index3] + ListItemTextGetter_Default.RomanTens[index2] + ListItemTextGetter_Default.RomanOnes[index1];
      }
      if (numFmt == "lowerRoman")
      {
        int index5 = levelNumber % 10;
        int index6 = levelNumber % 100 / 10;
        int index7 = levelNumber % 1000 / 100;
        int index8 = levelNumber / 1000;
        return (ListItemTextGetter_Default.RomanThousands[index8] + ListItemTextGetter_Default.RomanHundreds[index7] + ListItemTextGetter_Default.RomanTens[index6] + ListItemTextGetter_Default.RomanOnes[index5]).ToLower();
      }
      if (numFmt == "upperLetter")
      {
        int num = levelNumber % 780;
        if (num == 0)
          num = 780;
        return "".PadRight((num - 1) / 26 + 1, "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[(num - 1) % 26]);
      }
      if (numFmt == "lowerLetter")
      {
        int num = levelNumber % 780;
        if (num == 0)
          num = 780;
        return "".PadRight((num - 1) / 26 + 1, "abcdefghijklmnopqrstuvwxyz"[(num - 1) % 26]);
      }
      if (numFmt == "ordinal")
      {
        string str = levelNumber % 100 == 11 || levelNumber % 100 == 12 || levelNumber % 100 == 13 ? "th" : (levelNumber % 10 != 1 ? (levelNumber % 10 != 2 ? (levelNumber % 10 != 3 ? "th" : "rd") : "nd") : "st");
        return levelNumber.ToString() + str;
      }
      if (numFmt == "cardinalText")
      {
        string str1 = "";
        int num1 = levelNumber / 1000;
        int num2 = levelNumber % 1000;
        if (num1 >= 1)
          str1 = str1 + ListItemTextGetter_Default.OneThroughNineteen[num1 - 1] + " thousand";
        if (num1 >= 1 && num2 == 0)
          return str1.Substring(0, 1).ToUpper() + str1.Substring(1);
        if (num1 >= 1)
          str1 += " ";
        int num3 = levelNumber % 1000 / 100;
        int num4 = levelNumber % 100;
        if (num3 >= 1)
          str1 = str1 + ListItemTextGetter_Default.OneThroughNineteen[num3 - 1] + " hundred";
        if (num3 >= 1 && num4 == 0)
          return str1.Substring(0, 1).ToUpper() + str1.Substring(1);
        if (num3 >= 1)
          str1 += " ";
        int num5 = levelNumber % 100;
        string str2;
        if (num5 <= 19)
        {
          str2 = str1 + ListItemTextGetter_Default.OneThroughNineteen[num5 - 1];
        }
        else
        {
          int num6 = num5 / 10;
          int num7 = num5 % 10;
          str2 = str1 + ListItemTextGetter_Default.Tens[num6 - 1];
          if (num7 >= 1)
            str2 = str2 + "-" + ListItemTextGetter_Default.OneThroughNineteen[num7 - 1];
        }
        return str2.Substring(0, 1).ToUpper() + str2.Substring(1);
      }
      if (numFmt == "ordinalText")
      {
        string str3 = "";
        int num8 = levelNumber / 1000;
        int num9 = levelNumber % 1000;
        if (num8 >= 1 && num9 != 0)
          str3 = str3 + ListItemTextGetter_Default.OneThroughNineteen[num8 - 1] + " thousand";
        if (num8 >= 1 && num9 == 0)
        {
          string str4 = str3 + ListItemTextGetter_Default.OneThroughNineteen[num8 - 1] + " thousandth";
          return str4.Substring(0, 1).ToUpper() + str4.Substring(1);
        }
        if (num8 >= 1)
          str3 += " ";
        int num10 = levelNumber % 1000 / 100;
        int num11 = levelNumber % 100;
        if (num10 >= 1 && num11 != 0)
          str3 = str3 + ListItemTextGetter_Default.OneThroughNineteen[num10 - 1] + " hundred";
        if (num10 >= 1 && num11 == 0)
        {
          string str5 = str3 + ListItemTextGetter_Default.OneThroughNineteen[num10 - 1] + " hundredth";
          return str5.Substring(0, 1).ToUpper() + str5.Substring(1);
        }
        if (num10 >= 1)
          str3 += " ";
        int num12 = levelNumber % 100;
        string str6;
        if (num12 <= 19)
        {
          str6 = str3 + ListItemTextGetter_Default.OrdinalOneThroughNineteen[num12 - 1];
        }
        else
        {
          int num13 = num12 / 10;
          int num14 = num12 % 10;
          str6 = num14 != 0 ? str3 + ListItemTextGetter_Default.Tens[num13 - 1] : str3 + ListItemTextGetter_Default.OrdinalTenths[num13 - 1];
          if (num14 >= 1)
            str6 = str6 + "-" + ListItemTextGetter_Default.OrdinalOneThroughNineteen[num14 - 1];
        }
        return str6.Substring(0, 1).ToUpper() + str6.Substring(1);
      }
      if (numFmt == "01, 02, 03, ...")
        return string.Format("{0:00}", (object) levelNumber);
      if (numFmt == "001, 002, 003, ...")
        return string.Format("{0:000}", (object) levelNumber);
      if (numFmt == "0001, 0002, 0003, ...")
        return string.Format("{0:0000}", (object) levelNumber);
      if (numFmt == "00001, 00002, 00003, ...")
        return string.Format("{0:00000}", (object) levelNumber);
      if (numFmt == "bullet")
        return "";
      if (!(numFmt == "decimalEnclosedCircle") || levelNumber < 1 || levelNumber > 20)
        return levelNumber.ToString();
      return new string(new char[1]
      {
        (char) (9311 + levelNumber)
      });
    }
  }
}
