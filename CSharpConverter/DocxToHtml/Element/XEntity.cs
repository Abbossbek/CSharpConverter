// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.XEntity
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public class XEntity : XText
  {
    public override void WriteTo(XmlWriter writer)
    {
      if (this.Value.Substring(0, 1) == "#")
      {
        string data = string.Format("&{0};", (object) this.Value);
        writer.WriteRaw(data);
      }
      else
        writer.WriteEntityRef(this.Value);
    }

    public XEntity(string value)
      : base(value)
    {
    }
  }
}
