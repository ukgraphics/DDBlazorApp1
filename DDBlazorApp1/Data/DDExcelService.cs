using GrapeCity.Documents.Excel;
using Microsoft.Extensions.Options;
using System.IO;

namespace DDBlazorApp1.Data
{
    public class DDExcelService
    {
        private OptionsAccessor OptionsAccessor { get; }

        public DDExcelService(IOptions<OptionsAccessor> optionsAccessor)
        {
            OptionsAccessor = optionsAccessor.Value;
        }

        public void Create(string platformname)
        {
            // ライセンスキー設定
            //Workbook.SetLicenseKey(optionsAccessor.licenseStrings.DioDocsExcel);

            // ワークブックの作成
            Workbook workbook = new Workbook();

            // ワークシートの取得
            IWorksheet worksheet = workbook.Worksheets[0];

            // セル範囲を指定して文字列を設定
            worksheet.Range["B2"].Value = "Hello DioDocs!";
            worksheet.Range["B3"].Value = "from " + platformname;

            // メモリストリームに保存
            MemoryStream ms = new MemoryStream();
            workbook.Save(ms, SaveFileFormat.Xlsx);
            ms.Seek(0, SeekOrigin.Begin);

            // BLOBストレージにアップロード
            AzStorage storage = new AzStorage(OptionsAccessor.AzStorageStrings.Dev);
            storage.UploadExcelAsync(ms);
        }
    }
}
