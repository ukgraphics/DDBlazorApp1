namespace DDBlazorApp1.Data
{
    public class OptionsAccessor
    {
        public LicenseStrings LicenseStrings { get; set; }
        public AzStorageStrings AzStorageStrings { get; set; }
    }

    public class LicenseStrings
    {
        public string DioDocsExcel { get; set; }
        public string DioDocsPdf { get; set; }
    }

    public class AzStorageStrings
    {
        public string Dev { get; set; }
        public string Product { get; set; }
    }
}
