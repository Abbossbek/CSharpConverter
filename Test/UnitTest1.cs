
namespace Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            using (Stream stream = File.Open($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\response.docx", FileMode.Open))
            {
                byte[] bytes = CSharpConverter.Converter.DocxToPdf(stream);
                File.WriteAllBytes($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\response.pdf", bytes);
            }
        }
    }
}