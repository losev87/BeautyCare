using System.Collections.Generic;

namespace IntraVision.Web.Mvc.Controls
{
    public interface IGridOptions
    {
        IList<FilterConditionValue> FilterConditions { get; set; }
        FilterConditionValue GetFilterConditionValue(string column);

        int Page { get; set; }
        int PageSize { get; set; }
        int PagesOnPage { get; set; }
        long FilterId { get; set; }
        string SearchString { get; set; }
        bool ShowPager { get; set; }
        IList<GridSortOptions> SortOptions { get; set; }
        IList<string> VisibleColumns { get; set; }
        bool ShowSearchString { get; set; }
        bool ShowGridOptions { get; set; }

        string Serialize();
    }
}
