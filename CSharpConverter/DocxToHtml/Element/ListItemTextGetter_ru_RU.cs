// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ListItemTextGetter_ru_RU
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

namespace CSharpConverter.DocxToHtml.Element
{
  public class ListItemTextGetter_ru_RU
  {
    private static string[] OneThroughNineteen = new string[19]
    {
      "один",
      "два",
      "три",
      "четыре",
      "пять",
      "шесть",
      "семь",
      "восемь",
      "девять",
      "десять",
      "одиннадцать",
      "двенадцать",
      "тринадцать",
      "четырнадцать",
      "пятнадцать",
      "шестнадцать",
      "семнадцать",
      "восемнадцать",
      "девятнадцать"
    };
    private static string[] Tens = new string[9]
    {
      "десять",
      "двадцать",
      "тридцать",
      "сорок",
      "пятьдесят",
      "шестьдесят",
      "семьдесят",
      "восемьдесят",
      "девяносто"
    };
    private static string[] OrdinalOneThroughNineteen = new string[19]
    {
      "первый",
      "второй",
      "третий",
      "четвертый",
      "пятый",
      "шестой",
      "седьмой",
      "восьмой",
      "девятый",
      "десятый",
      "одиннадцатый",
      "двенадцатый",
      "тринадцатый",
      "четырнадцатый",
      "пятнадцатый",
      "шестнадцатый",
      "семнадцатый",
      "восемнадцатый",
      "девятнадцатый"
    };
    private static string[] OrdinalTenths = new string[9]
    {
      "десятый",
      "двадцатый",
      "тридцатый",
      "сороковой",
      "пятидесятый",
      "шестидесятый",
      "семидесятый",
      "восьмидесятый",
      "девяностый"
    };

    public static string GetListItemText(
      string languageCultureName,
      int levelNumber,
      string numFmt)
    {
      if (numFmt == "cardinalText")
      {
        string str1 = "";
        int num1 = levelNumber / 1000;
        int num2 = levelNumber % 1000;
        if (num1 >= 1)
          str1 = str1 + ListItemTextGetter_ru_RU.OneThroughNineteen[num1 - 1] + " thousand";
        if (num1 >= 1 && num2 == 0)
          return str1.Substring(0, 1).ToUpper() + str1.Substring(1);
        if (num1 >= 1)
          str1 += " ";
        int num3 = levelNumber % 1000 / 100;
        int num4 = levelNumber % 100;
        if (num3 >= 1)
          str1 = str1 + ListItemTextGetter_ru_RU.OneThroughNineteen[num3 - 1] + " hundred";
        if (num3 >= 1 && num4 == 0)
          return str1.Substring(0, 1).ToUpper() + str1.Substring(1);
        if (num3 >= 1)
          str1 += " ";
        int num5 = levelNumber % 100;
        string str2;
        if (num5 <= 19)
        {
          str2 = str1 + ListItemTextGetter_ru_RU.OneThroughNineteen[num5 - 1];
        }
        else
        {
          int num6 = num5 / 10;
          int num7 = num5 % 10;
          str2 = str1 + ListItemTextGetter_ru_RU.Tens[num6 - 1];
          if (num7 >= 1)
            str2 = str2 + "-" + ListItemTextGetter_ru_RU.OneThroughNineteen[num7 - 1];
        }
        return str2.Substring(0, 1).ToUpper() + str2.Substring(1);
      }
      if (!(numFmt == "ordinalText"))
        return (string) null;
      string str3 = "";
      int num8 = levelNumber / 1000;
      int num9 = levelNumber % 1000;
      if (num8 >= 1 && num9 != 0)
        str3 = str3 + ListItemTextGetter_ru_RU.OneThroughNineteen[num8 - 1] + " thousand";
      if (num8 >= 1 && num9 == 0)
      {
        string str4 = str3 + ListItemTextGetter_ru_RU.OneThroughNineteen[num8 - 1] + " thousandth";
        return str4.Substring(0, 1).ToUpper() + str4.Substring(1);
      }
      if (num8 >= 1)
        str3 += " ";
      int num10 = levelNumber % 1000 / 100;
      int num11 = levelNumber % 100;
      if (num10 >= 1 && num11 != 0)
        str3 = str3 + ListItemTextGetter_ru_RU.OneThroughNineteen[num10 - 1] + " hundred";
      if (num10 >= 1 && num11 == 0)
      {
        string str5 = str3 + ListItemTextGetter_ru_RU.OneThroughNineteen[num10 - 1] + " hundredth";
        return str5.Substring(0, 1).ToUpper() + str5.Substring(1);
      }
      if (num10 >= 1)
        str3 += " ";
      int num12 = levelNumber % 100;
      string str6;
      if (num12 <= 19)
      {
        str6 = str3 + ListItemTextGetter_ru_RU.OrdinalOneThroughNineteen[num12 - 1];
      }
      else
      {
        int num13 = num12 / 10;
        int num14 = num12 % 10;
        str6 = num14 != 0 ? str3 + ListItemTextGetter_ru_RU.Tens[num13 - 1] : str3 + ListItemTextGetter_ru_RU.OrdinalTenths[num13 - 1];
        if (num14 >= 1)
          str6 = str6 + " " + ListItemTextGetter_ru_RU.OrdinalOneThroughNineteen[num14 - 1];
      }
      return str6.Substring(0, 1).ToUpper() + str6.Substring(1);
    }
  }
}
