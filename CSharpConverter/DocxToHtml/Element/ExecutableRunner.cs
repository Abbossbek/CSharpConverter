// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.ExecutableRunner
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CSharpConverter.DocxToHtml.Element
{
  public class ExecutableRunner
  {
    public static ExecutableRunner.RunResults RunExecutable(
      string executablePath,
      string arguments,
      string workingDirectory)
    {
      ExecutableRunner.RunResults runResults = new ExecutableRunner.RunResults()
      {
        Output = new StringBuilder(),
        Error = new StringBuilder(),
        RunException = (Exception) null
      };
      try
      {
        if (!File.Exists(executablePath))
          throw new ArgumentException("Invalid executable path.", nameof (executablePath));
        using (Process process = new Process())
        {
          process.StartInfo.FileName = executablePath;
          process.StartInfo.Arguments = arguments;
          process.StartInfo.WorkingDirectory = workingDirectory;
          process.StartInfo.UseShellExecute = false;
          process.StartInfo.RedirectStandardOutput = true;
          process.StartInfo.RedirectStandardError = true;
          process.OutputDataReceived += (DataReceivedEventHandler) ((o, e) => runResults.Output.Append(e.Data).Append(Environment.NewLine));
          process.ErrorDataReceived += (DataReceivedEventHandler) ((o, e) => runResults.Error.Append(e.Data).Append(Environment.NewLine));
          process.Start();
          process.BeginOutputReadLine();
          process.BeginErrorReadLine();
          process.WaitForExit();
          runResults.ExitCode = process.ExitCode;
        }
      }
      catch (Exception ex)
      {
        runResults.RunException = ex;
      }
      return runResults;
    }

    public class RunResults
    {
      public int ExitCode;
      public Exception RunException;
      public StringBuilder Output;
      public StringBuilder Error;
    }
  }
}
