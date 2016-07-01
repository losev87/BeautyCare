using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using ClosedXML.Excel;

namespace IntraVision.Web.Mvc
{
    public static class HttpPostedFileBaseExtensions
    {
        public static IEnumerable<IEnumerable<object>> XlsxToArray(this HttpPostedFileBase fileBase)
        {
            var workbook = new XLWorkbook(fileBase.InputStream);
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet != null)
                return worksheet.RowsUsed().Select(r =>worksheet.ColumnsUsed().Select(column =>
                {
                    var cell = r.Cells().FirstOrDefault(c => c.Address.ColumnNumber == column.ColumnNumber());
                    return cell != null ? cell.Value : null;
                }));
            return null;
        }

        public static IEnumerable<IEnumerable<object>> XlsxToArray2(this HttpPostedFileBase fileBase)
        {
            var workbook = new XLWorkbook(fileBase.InputStream);
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet != null)
            {
                var adr = worksheet.ColumnsUsed().Select(column => column.ColumnNumber());
                return worksheet.RowsUsed().Select(r => adr.Select(column =>
                {
                    var cell = r.Cells().FirstOrDefault(c => c.Address.ColumnNumber == column);
                    return cell != null ? cell.Value : null;
                }).ToList()).ToList();
            }
            return null;
        }

        public static IEnumerable<IEnumerable<string>> CvsToArray(this HttpPostedFileBase fileBase, bool readFirstRow = false)
        {
            using (var sr = new StreamReader(fileBase.InputStream, Encoding.GetEncoding(1251)))
            {
                var result = new List<List<string>>();

                String row;

                if (!readFirstRow)
                    sr.ReadLine();

                while ((row = sr.ReadLine()) != null)
                {
                    if (String.IsNullOrEmpty(row))
                        continue;

                    result.Add(row.Split(';').ToList());
                }
                return result;
            }
        }
    }
}
