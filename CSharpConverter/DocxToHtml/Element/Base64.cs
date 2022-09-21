﻿// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.Base64
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharpConverter.DocxToHtml.Element
{
  public class Base64
    {
        public static string ConvertToBase64(string fileName)
        {
            byte[] ba = System.IO.File.ReadAllBytes(fileName);
            string base64String = (System.Convert.ToBase64String(ba))
                .Select
                (
                    (c, i) => new
                    {
                        Chunk = i / 76,
                        Character = c
                    }
                )
                .GroupBy(c => c.Chunk)
                .Aggregate(
                    new StringBuilder(),
                    (s, i) =>
                        s.Append(
                            i.Aggregate(
                                new StringBuilder(),
                                (seed, it) => seed.Append(it.Character),
                                sb => sb.ToString()
                            )
                        )
                        .Append(Environment.NewLine),
                    s =>
                    {
                        s.Length -= Environment.NewLine.Length;
                        return s.ToString();
                    }
                );

            return base64String;
        }

        public static byte[] ConvertFromBase64(string fileName, string b64)
        {
            string b64b = b64.Replace("\r\n", "");
            byte[] ba = System.Convert.FromBase64String(b64b);
            return ba;
        }
    }
}
