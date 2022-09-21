// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.GroupOfAdjacent`2
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public class GroupOfAdjacent<TSource, TKey> : 
    IGrouping<TKey, TSource>,
    IEnumerable<TSource>,
    IEnumerable
  {
    public GroupOfAdjacent(List<TSource> source, TKey key)
    {
      this.GroupList = source;
      this.Key = key;
    }

    public TKey Key { get; set; }

    private List<TSource> GroupList { get; set; }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) ((IEnumerable<TSource>) this).GetEnumerator();

    IEnumerator<TSource> IEnumerable<TSource>.GetEnumerator() => ((IEnumerable<TSource>) this.GroupList).GetEnumerator();
  }
}
