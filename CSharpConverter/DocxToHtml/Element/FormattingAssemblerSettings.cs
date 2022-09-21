// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.FormattingAssemblerSettings
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

namespace CSharpConverter.DocxToHtml.Element
{
  public class FormattingAssemblerSettings
  {
    public bool RemoveStyleNamesFromParagraphAndRunProperties;
    public bool ClearStyles;
    public bool OrderElementsPerStandard;
    public bool CreateHtmlConverterAnnotationAttributes;
    public bool RestrictToSupportedNumberingFormats;
    public bool RestrictToSupportedLanguages;
    public ListItemRetrieverSettings ListItemRetrieverSettings;

    public FormattingAssemblerSettings()
    {
      this.RemoveStyleNamesFromParagraphAndRunProperties = true;
      this.ClearStyles = true;
      this.OrderElementsPerStandard = true;
      this.CreateHtmlConverterAnnotationAttributes = true;
      this.RestrictToSupportedNumberingFormats = false;
      this.RestrictToSupportedLanguages = false;
      this.ListItemRetrieverSettings = new ListItemRetrieverSettings();
    }
  }
}
