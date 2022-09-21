// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.FileUtils
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace CSharpConverter.DocxToHtml.Element
{
  public class FileUtils
  {
    public static DirectoryInfo GetDateTimeStampedDirectoryInfo(string prefix)
    {
      DateTime now = DateTime.Now;
      return new DirectoryInfo(prefix + string.Format("-{0:00}-{1:00}-{2:00}-{3:00}{4:00}{5:00}", (object) (now.Year - 2000), (object) now.Month, (object) now.Day, (object) now.Hour, (object) now.Minute, (object) now.Second));
    }

    public static FileInfo GetDateTimeStampedFileInfo(string prefix, string suffix)
    {
      DateTime now = DateTime.Now;
      return new FileInfo(prefix + string.Format("-{0:00}-{1:00}-{2:00}-{3:00}{4:00}{5:00}", (object) (now.Year - 2000), (object) now.Month, (object) now.Day, (object) now.Hour, (object) now.Minute, (object) now.Second) + suffix);
    }

    public static void ThreadSafeCreateDirectory(DirectoryInfo dir)
    {
      while (!dir.Exists)
      {
        try
        {
          dir.Create();
          break;
        }
        catch (IOException ex)
        {
          Thread.Sleep(50);
        }
      }
    }

    public static void ThreadSafeCopy(FileInfo sourceFile, FileInfo destFile)
    {
      while (!destFile.Exists)
      {
        try
        {
          File.Copy(sourceFile.FullName, destFile.FullName);
          break;
        }
        catch (IOException ex)
        {
          Thread.Sleep(50);
        }
      }
    }

    public static void ThreadSafeCreateEmptyTextFileIfNotExist(FileInfo file)
    {
      while (!file.Exists)
      {
        try
        {
          File.WriteAllText(file.FullName, "");
          break;
        }
        catch (IOException ex)
        {
          Thread.Sleep(50);
        }
      }
    }

    internal static void ThreadSafeAppendAllLines(FileInfo file, string[] strings)
    {
      while (true)
      {
        try
        {
          File.AppendAllLines(file.FullName, (IEnumerable<string>) strings);
          break;
        }
        catch (IOException ex)
        {
          Thread.Sleep(50);
        }
      }
    }

    public static List<string> GetFilesRecursive(DirectoryInfo dir, string searchPattern)
    {
      List<string> fileList = new List<string>();
      FileUtils.GetFilesRecursiveInternal(dir, searchPattern, fileList);
      return fileList;
    }

    private static void GetFilesRecursiveInternal(
      DirectoryInfo dir,
      string searchPattern,
      List<string> fileList)
    {
      fileList.AddRange(((IEnumerable<FileInfo>) dir.GetFiles(searchPattern)).Select<FileInfo, string>((Func<FileInfo, string>) (file => file.FullName)));
      foreach (DirectoryInfo directory in dir.GetDirectories())
        FileUtils.GetFilesRecursiveInternal(directory, searchPattern, fileList);
    }

    public static List<string> GetFilesRecursive(DirectoryInfo dir)
    {
      List<string> fileList = new List<string>();
      FileUtils.GetFilesRecursiveInternal(dir, fileList);
      return fileList;
    }

    private static void GetFilesRecursiveInternal(DirectoryInfo dir, List<string> fileList)
    {
      fileList.AddRange(((IEnumerable<FileInfo>) dir.GetFiles()).Select<FileInfo, string>((Func<FileInfo, string>) (file => file.FullName)));
      foreach (DirectoryInfo directory in dir.GetDirectories())
        FileUtils.GetFilesRecursiveInternal(directory, fileList);
    }

    public static void CopyStream(Stream source, Stream target)
    {
      byte[] buffer = new byte[16534];
      int count;
      while ((count = source.Read(buffer, 0, 16534)) > 0)
        target.Write(buffer, 0, count);
    }
  }
}
