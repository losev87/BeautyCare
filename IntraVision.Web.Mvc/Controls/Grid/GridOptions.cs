using System;
using System.Collections.Generic;
using System.Linq;
using IntraVision.Core.Sorting;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace IntraVision.Web.Mvc.Controls
{
    [Serializable]
    public class GridOptions : IGridOptions
    {
        public IList<GridSortOptions> SortOptions { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PagesOnPage { get; set; }
        public long FilterId { get; set; }
        public string SearchString { get; set; }
        public bool ShowPager { get; set; }
        public IList<string> VisibleColumns { get; set; }
        public IList<FilterConditionValue> DefaultFilterConditions { get; set; }
        public IList<FilterConditionValue> FilterConditions { get; set; }
        public bool ShowSearchString { get; set; }
        public bool ShowGridOptions { get; set; }

        public GridOptions()
        {
            Page = 1;
            PageSize = 25;
            PagesOnPage = 20;
            ShowPager = true;
            ShowSearchString = false;
            ShowGridOptions = false;

            SortOptions = new List<GridSortOptions> { new GridSortOptions { Column = "Id", Direction = SortDirection.Ascending } };
            VisibleColumns = new List<string>();
            FilterConditions = new List<FilterConditionValue>();
            DefaultFilterConditions = new List<FilterConditionValue>();
        }

        public FilterConditionValue GetFilterConditionValue(string column)
        {
            return FilterConditions.Any(c => !string.IsNullOrEmpty(c.Column) && c.Column.ToUpper() == column.ToUpper()) 
                ? FilterConditions.Single(c => !string.IsNullOrEmpty(c.Column) && c.Column.ToUpper() == column.ToUpper())
                : DefaultFilterConditions.SingleOrDefault(c => !string.IsNullOrEmpty(c.Column) && c.Column.ToUpper() == column.ToUpper());
        }

        public static GridOptions Deserialize(string serialized)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(serialized));
            
            try{
                return bf.Deserialize(ms) as GridOptions;
            }
            finally{
                ms.Close();
            }
        }

        public string Serialize()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream memStr = new MemoryStream();

            var copy = this.MemberwiseClone() as GridOptions;
            copy.Page = 1;
            try
            {
                bf.Serialize(memStr, copy);
                memStr.Position = 0;

                return Convert.ToBase64String(memStr.ToArray());
            }
            finally
            {
                memStr.Close();
            }
        }
    }
}
