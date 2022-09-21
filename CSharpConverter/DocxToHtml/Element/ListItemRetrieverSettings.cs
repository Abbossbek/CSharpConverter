// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ListItemRetrieverSettings
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Collections.Generic;

namespace CSharpConverter.DocxToHtml.Element
{
  public class ListItemRetrieverSettings
  {
    public static Dictionary<string, Func<string, int, string, string>> DefaultListItemTextImplementations = new Dictionary<string, Func<string, int, string, string>>()
    {
      {
        "fr-FR",
        new Func<string, int, string, string>(ListItemTextGetter_fr_FR.GetListItemText)
      },
      {
        "tr-TR",
        new Func<string, int, string, string>(ListItemTextGetter_tr_TR.GetListItemText)
      },
      {
        "ru-RU",
        new Func<string, int, string, string>(ListItemTextGetter_ru_RU.GetListItemText)
      },
      {
        "sv-SE",
        new Func<string, int, string, string>(ListItemTextGetter_sv_SE.GetListItemText)
      },
      {
        "zh-CN",
        new Func<string, int, string, string>(ListItemTextGetter_zh_CN.GetListItemText)
      }
    };
    public Dictionary<string, Func<string, int, string, string>> ListItemTextImplementations;

    public ListItemRetrieverSettings() => this.ListItemTextImplementations = ListItemRetrieverSettings.DefaultListItemTextImplementations;
  }
}
