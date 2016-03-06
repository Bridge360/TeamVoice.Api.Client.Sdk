using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using TeamVoice.Api.Interfaces;

namespace TeamVoice.Api.Client.Sdk.Tests.Classes
{
    public class Helpers
    {
        internal const string ACCOUNTKEY = "CF837E912CD94393B77CDCFBE85A1496";
        internal const string APPKEY = "ifkFKmC85ucjHeIchrCMmT5sVqRK/5tXkO87U1YdfQY=";

        internal static Credentials Credentials
        {
            get
            {
                return new Credentials(ACCOUNTKEY, APPKEY);
            }
        }

        internal enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }

        [DllImport("shell32.dll")]
        internal static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation,
            string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);

        internal static ExcelPackage CreateSpreadsheet()
        {
            string filename = Path.Combine(new string[] {
                    AppDomain.CurrentDomain.BaseDirectory, 
                    "..", "..", "Data",
                });
            if (!Directory.Exists(filename)) Directory.CreateDirectory(filename);
            filename = Path.Combine(filename, "FillSpreadsheet.xlsx");
            FileInfo newFile = new FileInfo(filename);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(filename);
            }
            Debug.WriteLine("Creating " + filename);
            var package = new ExcelPackage(newFile);
            package.Workbook.Properties.Title = "TeamVoice Sample Data";
            package.Workbook.Properties.Author = "Bridge³";
            package.Workbook.Properties.Company = "Bridge³";
            package.Workbook.Properties.Application = "TeamVoice API";
            return package;
        }

        internal static void CreateWorksheet(ExcelPackage Package, string ModelName)
        {
            Debug.WriteLine("Getting " + ModelName + " data...");
            var controller = new Controller(Credentials, ModelName);
            if (!controller.HasList) return;
            var list = controller.GetListAsync().Result;
            Debug.WriteLine("Filling " + ModelName + " worksheet...");
            var worksheet = Package.Workbook.Worksheets.Add(ModelName);
            int col = 0;
            int row = 1;
            var fields = Services.GetFields(ModelName);
            foreach (var field in fields)
            {
                col++;
                worksheet.Cells[row, col].Value = field.Caption;
            }
            worksheet.Row(row).Style.Font.Bold = true;
            worksheet.View.FreezePanes(row + 1, 1);
            foreach (var listitem in list.Values)
            {
                    row++;
                    col = 0;
                    foreach (var field in fields)
                    {
                        col++;
                        object val = listitem.GetValue(field.Name);
                        if (val == null || val == DBNull.Value)
                            continue;
                        if (field.Type == typeof(Decimal))
                            worksheet.Cells[row, col].Style.Numberformat.Format = "0.00#####";
                        else if (field.Type == typeof(DateTime))
                            worksheet.Cells[row, col].Style.Numberformat.Format = "yyyy-MM-dd hh:mm:ss";
                        worksheet.Cells[row, col].Value = val;
                    }
            }
            worksheet.Cells.AutoFitColumns();
            if (row > 1) worksheet.Select("A2");
            else worksheet.Select("A1");
        }
    }
}
