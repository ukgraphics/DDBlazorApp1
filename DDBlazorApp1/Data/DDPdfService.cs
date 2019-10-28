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
        public string key;
        public string connectionstring;

        public DDPdfService(IOptions<LicenseStrings> licensestrings, IOptions<AzStorageStrings> azstoragestrings)
        {
            key = licensestrings.Value.DioDocsExcel;
            connectionstring = azstoragestrings.Value.Dev;
        }

        public void Create(string platformname)
        {
            // トライアル用
            //GcPdfDocument.SetLicenseKey(key);

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
            AzStorage storage = new AzStorage(connectionstring);
            storage.UploadPdfAsync(ms);
        }
    }
}
