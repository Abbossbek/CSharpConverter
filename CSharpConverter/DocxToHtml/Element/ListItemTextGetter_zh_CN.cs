// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ListItemTextGetter_zh_CN
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

namespace CSharpConverter.DocxToHtml.Element
{
  public class ListItemTextGetter_zh_CN
  {
    public static string GetListItemText(
      string languageCultureName,
      int levelNumber,
      string numFmt)
    {
      string[] strArray1 = new string[10]
      {
        "",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六",
        "七",
        "八",
        "九"
      };
      string str1 = "十";
      string str2 = "百";
      string str3 = "千";
      string str4 = "〇";
      string[] strArray2 = new string[10]
      {
        "○",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六",
        "七",
        "八",
        "九"
      };
      int levelNumber1 = levelNumber % 1000;
      int num = levelNumber % 100;
      int index1 = levelNumber / 1000;
      int index2 = levelNumber % 1000 / 100;
      int index3 = levelNumber % 100 / 10;
      int index4 = levelNumber % 10;
      if (numFmt == "chineseCounting")
      {
        if (levelNumber >= 1 && levelNumber <= 9)
          return strArray2[levelNumber];
        if (levelNumber >= 10 && levelNumber <= 19)
          return levelNumber == 10 ? str1 : str1 + strArray2[index4];
        if (levelNumber >= 11 && levelNumber <= 99)
          return index4 == 0 ? strArray2[index3] + str1 : strArray2[index3] + str1 + strArray2[index4];
        if (levelNumber >= 100 && levelNumber <= 999)
          return strArray2[index2] + strArray2[index3] + strArray2[index4];
        return levelNumber >= 1000 && levelNumber <= 9999 ? strArray2[index1] + strArray2[index2] + strArray2[index3] + strArray2[index4] : levelNumber.ToString();
      }
      if (numFmt == "chineseCountingThousand")
      {
        if (levelNumber >= 1 && levelNumber <= 9)
          return strArray1[levelNumber];
        if (levelNumber >= 10 && levelNumber <= 19)
          return str1 + strArray1[index4];
        if (levelNumber >= 20 && levelNumber <= 99)
          return strArray1[index3] + str1 + strArray1[index4];
        if (levelNumber >= 100 && levelNumber <= 999)
        {
          switch (num)
          {
            case 0:
              return strArray1[index2] + str2;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
              return strArray1[index2] + str2 + str4 + strArray1[levelNumber % 10];
            default:
              if (index4 == 0)
                return strArray1[index2] + str2 + strArray1[index3] + str1;
              return strArray1[index2] + str2 + strArray1[index3] + str1 + strArray1[index4];
          }
        }
        else
        {
          if (levelNumber < 1000 || levelNumber > 9999)
            return levelNumber.ToString();
          if (levelNumber1 == 0)
            return strArray1[index1] + str3;
          if (levelNumber1 >= 1 && levelNumber1 <= 9)
            return strArray1[index1] + str3 + str4 + ListItemTextGetter_zh_CN.GetListItemText("zh_CN", levelNumber1, numFmt);
          if (levelNumber1 >= 10 && levelNumber1 <= 99)
            return strArray1[index1] + str3 + str4 + strArray1[index3] + str1 + strArray1[index4];
          switch (num)
          {
            case 0:
              return strArray1[index1] + str3 + strArray1[index2] + str2;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
              return strArray1[index1] + str3 + strArray1[index2] + str2 + str4 + strArray1[index4];
            default:
              return strArray1[index1] + str3 + strArray1[index2] + str2 + strArray1[index3] + str1 + strArray1[index4];
          }
        }
      }
      else
      {
        if (!(numFmt == "ideographTraditional"))
          return (string) null;
        string[] strArray3 = new string[11]
        {
          " ",
          "甲",
          "乙",
          "丙",
          "丁",
          "戊",
          "己",
          "庚",
          "辛",
          "壬",
          "癸"
        };
        return levelNumber >= 1 && levelNumber <= 10 ? strArray3[levelNumber] : levelNumber.ToString();
      }
    }
  }
}
