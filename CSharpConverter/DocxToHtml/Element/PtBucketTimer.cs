// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.PtBucketTimer
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class PtBucketTimer
  {
    private static string LastBucket;
    private static DateTime LastTime;
    private static Dictionary<string, PtBucketTimer.BucketInfo> Buckets;

    public static void Bucket(string bucket)
    {
      DateTime now = DateTime.Now;
      if (PtBucketTimer.LastBucket != null)
      {
        TimeSpan timeSpan = now - PtBucketTimer.LastTime;
        if (PtBucketTimer.Buckets.ContainsKey(PtBucketTimer.LastBucket))
        {
          ++PtBucketTimer.Buckets[PtBucketTimer.LastBucket].Count;
          PtBucketTimer.Buckets[PtBucketTimer.LastBucket].Time += timeSpan;
        }
        else
          PtBucketTimer.Buckets.Add(PtBucketTimer.LastBucket, new PtBucketTimer.BucketInfo()
          {
            Count = 1,
            Time = timeSpan
          });
      }
      PtBucketTimer.LastBucket = bucket;
      PtBucketTimer.LastTime = now;
    }

    public static string DumpBucketsByKey()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, PtBucketTimer.BucketInfo> keyValuePair in (IEnumerable<KeyValuePair<string, PtBucketTimer.BucketInfo>>) PtBucketTimer.Buckets.OrderBy<KeyValuePair<string, PtBucketTimer.BucketInfo>, string>((Func<KeyValuePair<string, PtBucketTimer.BucketInfo>, string>) (b => b.Key)))
      {
        string source = keyValuePair.Value.Time.ToString();
        if (source.Contains<char>('.'))
          source = source.Substring(0, source.Length - 5);
        string str = keyValuePair.Key.PadRight(60, '-') + "  " + string.Format("{0:00000000}", (object) keyValuePair.Value.Count) + "  " + source;
        stringBuilder.Append(str + Environment.NewLine);
      }
      string str1 = PtBucketTimer.Buckets.Aggregate<KeyValuePair<string, PtBucketTimer.BucketInfo>, TimeSpan>(TimeSpan.Zero, (Func<TimeSpan, KeyValuePair<string, PtBucketTimer.BucketInfo>, TimeSpan>) ((t, b) => t + b.Value.Time)).ToString();
      stringBuilder.Append(string.Format("Total: {0}", (object) str1.Substring(0, str1.Length - 5)));
      return stringBuilder.ToString();
    }

    public static string DumpBucketsByTime()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, PtBucketTimer.BucketInfo> keyValuePair in (IEnumerable<KeyValuePair<string, PtBucketTimer.BucketInfo>>) PtBucketTimer.Buckets.OrderBy<KeyValuePair<string, PtBucketTimer.BucketInfo>, TimeSpan>((Func<KeyValuePair<string, PtBucketTimer.BucketInfo>, TimeSpan>) (b => b.Value.Time)))
      {
        string source = keyValuePair.Value.Time.ToString();
        if (source.Contains<char>('.'))
          source = source.Substring(0, source.Length - 5);
        string str = keyValuePair.Key.PadRight(60, '-') + "  " + string.Format("{0:00000000}", (object) keyValuePair.Value.Count) + "  " + source;
        stringBuilder.Append(str + Environment.NewLine);
      }
      string str1 = PtBucketTimer.Buckets.Aggregate<KeyValuePair<string, PtBucketTimer.BucketInfo>, TimeSpan>(TimeSpan.Zero, (Func<TimeSpan, KeyValuePair<string, PtBucketTimer.BucketInfo>, TimeSpan>) ((t, b) => t + b.Value.Time)).ToString();
      stringBuilder.Append(string.Format("Total: {0}", (object) str1.Substring(0, str1.Length - 5)));
      return stringBuilder.ToString();
    }

    public static void Init() => PtBucketTimer.Buckets = new Dictionary<string, PtBucketTimer.BucketInfo>();

    private class BucketInfo
    {
      public int Count;
      public TimeSpan Time;
    }
  }
}
