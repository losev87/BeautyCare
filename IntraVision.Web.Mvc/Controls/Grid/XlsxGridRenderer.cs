using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace IntraVision.Web.Mvc.Controls
{
	public class XlsxGridRenderer<T> : IXlsxGridRenderer<T> where T : class
	{
        protected IGridModel<T> GridModel { get; private set; }
        protected IEnumerable<T> DataSource { get; private set; }

        protected List<AutoFilter> AutoFilterList { get; set; }

        public MemoryStream Render(IGridModel<T> gridModel, IEnumerable<T> dataSource)
        {
            GridModel = gridModel;
            DataSource = dataSource;
            return GetStreamReport();
        }

        protected virtual MemoryStream GetStreamReport()
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Report");

            var dataTable = GetTable();
            //ws.Cell(1, 1).Value = DateTime.Now;

            //ws.Range(1, 1, 1, dataTable.Columns.Count).Merge().AddToNamed("Titles");
            var xlTable = ws.Cell(1, 1).InsertTable(dataTable.AsEnumerable());

            ws.Columns().AdjustToContents();

            var ms = new MemoryStream();

            wb.SaveAs(ms);

            return ms;
        }

        protected IEnumerable<GridColumn<T>> VisibleColumns()
        {
            return GridModel.Columns.Where(x => x.Visible);
                }

        private DataTable GetTable()
                {
            var table = new DataTable();

            foreach (var column in VisibleColumns().OrderBy(c => c.Order))
                table.Columns.Add(column.DisplayName, column.ColumnType ?? typeof(string));

            foreach (var item in DataSource)
                table.Rows.Add(VisibleColumns().OrderBy(c => c.Order).Select(column => column.GetValue(item)).ToArray());

            if (VisibleColumns().OrderBy(c => c.Order).Any(c => c.Total))
            {
                var total = VisibleColumns().OrderBy(c => c.Order).Select<GridColumn<T>, object>(column =>
                    {
                        if (column.Total)
                        {
                            var values = DataSource.Select(column.GetValue);
                            return values.Cast<int>().Sum();
                        }
                        else if (column.TotalName)
                        {
                            return column.TotalText;
                        }
                        else
                        {
                            return null;
                        }
                    });

                table.Rows.Add(total.ToArray());
            }

            return table;
        }
    }
}