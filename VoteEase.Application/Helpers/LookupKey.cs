namespace VoteEase.Application.Helpers
{
    public struct LookupKey
    {
        public static string DotNetEnvironmentKey { get { return "ASPNETCORE_ENVIRONMENT"; } }

        public static string MembersExcelWorkbookFileName { get { return "VoteEase_Members_Workbook"; } }
        public static string MembersListingExcelWorksheetName { get { return "VoteEase Members Listing"; } }
    }
}
