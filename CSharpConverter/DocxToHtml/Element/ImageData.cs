// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ImageData
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System.Collections.Generic;
using System.IO;

namespace CSharpConverter.DocxToHtml.Element
{
  internal class ImageData
  {
    public List<ContentPartRelTypeIdTuple> ContentPartRelTypeIdList = new List<ContentPartRelTypeIdTuple>();

    private string ContentType { get; set; }

    private byte[] Image { get; set; }

    public OpenXmlPart ImagePart { get; set; }

    public ImageData(DocumentFormat.OpenXml.Packaging.ImagePart part)
    {
      this.ContentType = part.ContentType;
      using (Stream stream = part.GetStream(FileMode.Open, FileAccess.Read))
      {
        this.Image = new byte[stream.Length];
        stream.Read(this.Image, 0, (int) stream.Length);
      }
    }

    public void AddContentPartRelTypeResourceIdTupple(
      OpenXmlPart contentPart,
      string relationshipType,
      string relationshipId)
    {
      this.ContentPartRelTypeIdList.Add(new ContentPartRelTypeIdTuple()
      {
        ContentPart = contentPart,
        RelationshipType = relationshipType,
        RelationshipId = relationshipId
      });
    }

    public void WriteImage(DocumentFormat.OpenXml.Packaging.ImagePart part)
    {
      using (Stream stream = part.GetStream(FileMode.Create, FileAccess.ReadWrite))
        stream.Write(this.Image, 0, this.Image.GetUpperBound(0) + 1);
    }

    public bool Compare(ImageData arg)
    {
      if (this.ContentType != arg.ContentType || this.Image.GetLongLength(0) != arg.Image.GetLongLength(0))
        return false;
      long longLength = this.Image.GetLongLength(0);
      byte[] image1 = this.Image;
      byte[] image2 = arg.Image;
      for (long index = 0; index < longLength; ++index)
      {
        if ((int) image1[index] != (int) image2[index])
          return false;
      }
      return true;
    }
  }
}
