// Decompiled with JetBrains decompiler
// Type: VetCV.HtmlRendererCore.PdfSharpCore.Adapters.FontAdapter
// Assembly: HtmlRendererCore.PdfSharpCore, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA72F8E-2C1A-42B6-AF29-CEB7845EFBE4
// Assembly location: C:\Users\Abbosbek\.nuget\packages\htmlrenderercore.pdfsharpcore\1.0.1\lib\netcoreapp2.0\HtmlRendererCore.PdfSharpCore.dll

using PdfSharpCore.Drawing;

using TheArtOfDev.HtmlRenderer.Adapters;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;

namespace CSharpConverter.Pdf.Adapters
{
    internal sealed class FontAdapter : IRFont
    {
        private readonly XFont _font;
        private double _underlineOffset = -1.0;
        private double _height = -1.0;
        private double _whitespaceWidth = -1.0;

        public FontAdapter(XFont font) => this._font = font;

        public XFont Font => this._font;

        public double UnderlineOffset => this._underlineOffset;


        public double FontHeight => this._height;

        public double FontSize => _font.Size;

        public double FontLeftPadding => this._height / 6.0;

        public double GetWhitespaceWidth(RGraphics graphics)
        {
            if (this._whitespaceWidth < 0.0)
            {
                RSize rsize = graphics.MeasureString(" ", (IRFont)this);
                this._whitespaceWidth = ((RSize)rsize).Width;
            }
            return this._whitespaceWidth;
        }

        internal void SetMetrics(int height, int underlineOffset)
        {
            this._height = (double)height;
            this._underlineOffset = (double)underlineOffset;
        }
    }
}
