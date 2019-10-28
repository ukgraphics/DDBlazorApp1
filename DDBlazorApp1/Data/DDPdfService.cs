using GrapeCity.Documents.Pdf;
using GrapeCity.Documents.Text;
using Microsoft.Extensions.Options;
using System;
using System.Drawing;
using System.IO;

namespace DDBlazorApp1.Data
{
    public class DDPdfService
    {
        private OptionsAccessor OptionsAccessor { get; }

        public DDPdfService(IOptions<OptionsAccessor> optionsAccessor)
        {
            OptionsAccessor = optionsAccessor.Value;
        }

        public void Create(string platformname)
        {
            // ライセンスキー設定
            //GcPdfDocument.SetLicenseKey(optionsAccessor.licenseStrings.DioDocsPdf);

            // PDFドキュメントを作成します。
            GcPdfDocument doc = new GcPdfDocument();

            // ページを追加し、そのグラフィックスを取得します。
            GcPdfGraphics g = doc.NewPage().Graphics;

            // ページに文字列を描画します。
            g.DrawString("Hello, DioDocs!" + Environment.NewLine + "from " + platformname,
                new TextFormat() { Font = StandardFonts.Helvetica, FontSize = 12 },
                new PointF(72, 72));

            // メモリストリームに保存
            MemoryStream ms = new MemoryStream();
            doc.Save(ms, false);
            ms.Seek(0, SeekOrigin.Begin);

            // BLOBストレージにアップロード
            AzStorage storage = new AzStorage(OptionsAccessor.AzStorageStrings.Dev);
            storage.UploadPdfAsync(ms);
        }
    }
}
