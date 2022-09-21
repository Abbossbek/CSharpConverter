// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.MediaData
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using DocumentFormat.OpenXml.Packaging;
using System.Collections.Generic;
using System.IO;

namespace CSharpConverter.DocxToHtml.Element
{
  internal class MediaData
  {
    public List<ContentPartRelTypeIdTuple> ContentPartRelTypeIdList = new List<ContentPartRelTypeIdTuple>();

    private string ContentType { get; set; }

    private byte[] Media { get; set; }

    public DataPart DataPart { get; set; }

    public MediaData(DataPart part)
    {
      this.ContentType = part.ContentType;
      using (Stream stream = part.GetStream(FileMode.Open, FileAccess.Read))
      {
        this.Media = new byte[stream.Length];
        stream.Read(this.Media, 0, (int) stream.Length);
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

    public void WriteMedia(DataPart part)
    {
      using (Stream stream = part.GetStream(FileMode.Create, FileAccess.ReadWrite))
        stream.Write(this.Media, 0, this.Media.GetUpperBound(0) + 1);
    }

    public bool Compare(MediaData arg)
    {
      if (this.ContentType != arg.ContentType || this.Media.GetLongLength(0) != arg.Media.GetLongLength(0))
        return false;
      long longLength = this.Media.GetLongLength(0);
      byte[] media1 = this.Media;
      byte[] media2 = arg.Media;
      for (long index = 0; index < longLength; ++index)
      {
        if ((int) media1[index] != (int) media2[index])
          return false;
      }
      return true;
    }
  }
}
