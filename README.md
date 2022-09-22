# CSharpConverter

Simple document converter with 100% C# code.

You can convert docx to pdf like this:
```csharp
            byte[] bytes = CSharpConverter.Converter.DocxToPdf("<docx path>");
            File.WriteAllBytes($"<pdf path>", bytes);
```
or like this:
```csharp
            using (Stream stream = File.Open("<docx path>", FileMode.Open))
            {
                byte[] bytes = CSharpConverter.Converter.DocxToPdf(stream);
                File.WriteAllBytes($"<pdf path>", bytes);
            }
```
