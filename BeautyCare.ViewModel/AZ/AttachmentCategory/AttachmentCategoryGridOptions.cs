using System.Collections.Generic;
using IntraVision.Core.Sorting;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.ViewModel.AZ.User
{
    public class AttachmentCategoryGridOptions : GridOptions
    {
        public AttachmentCategoryGridOptions()
        {
            SortOptions = new List<GridSortOptions>
            {
                new GridSortOptions {Column = "Name", Direction = SortDirection.Ascending}
            };
        }
    }
}
