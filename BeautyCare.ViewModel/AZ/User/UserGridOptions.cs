using System.Collections.Generic;
using IntraVision.Core.Sorting;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.ViewModel.AZ.User
{
    public class UserGridOptions : GridOptions
    {
        public UserGridOptions()
        {
            SortOptions = new List<GridSortOptions>
            {
                new GridSortOptions {Column = "UserName", Direction = SortDirection.Ascending}
            };
        }
    }
}
