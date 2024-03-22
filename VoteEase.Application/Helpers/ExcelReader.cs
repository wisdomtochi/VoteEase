using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System.Data;
using VoteEase.DTO.ReadDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Application.Helpers
{
    public class ExcelReader
    {
        private readonly IFormFile file;
        private IExcelDataReader excelDataReader;
        private DataSet dataSet;

        private readonly string[] supportedFiles = { "xls", "xlsx" };
        public ExcelReader(IFormFile File)
        {
            file = File;
        }

        /// <summary>
        /// this reads the content of new products from excel document, and translates it into a c# object class
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ModelResult<MemberExcelSheet> ReadMembersFromExcel()
        {
            string extension = Path.GetExtension(file.FileName);

            if (extension == null) return Map.GetModelResult<MemberExcelSheet>(null, null, false, "File cannot be null.");

            if (file.FileName.Split(".")[0] != LookupKey.MembersExcelWorkbookFileName) return Map.GetModelResult<MemberExcelSheet>(null, null, false, "File not recognised.");

            if (!supportedFiles.Any(x => x.ToLower() == extension.ToLower())) return Map.GetModelResult<MemberExcelSheet>(null, null, false, "File format not supported.");

            try
            {
                if (extension == "xls") excelDataReader = ExcelReaderFactory.CreateBinaryReader(file.OpenReadStream());

                if (extension == "xlsx") excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(file.OpenReadStream());

                dataSet = excelDataReader.AsDataSet();

                if (dataSet == null && dataSet.Tables.Count <= 0) return Map.GetModelResult<MemberExcelSheet>(null, null, false, "The selected table is empty.");

                DataTable dataTable = dataSet.Tables[0];

                List<MemberExcelSheet> members = new();

                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    if (i == 0) continue;

                    var member = new MemberExcelSheet()
                    {
                        Name = dataTable.Rows[i][0].ToString(),
                        PhoneNumber = dataTable.Rows[i][1].ToString(),
                        GroupName = dataTable.Rows[i][2].ToString()
                    };

                    members.Add(member);
                }

                return Map.GetModelResult<MemberExcelSheet>(null, members, true, "Succeeded.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }

    public class MemberExcelSheet
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string GroupName { get; set; }
    }
}
