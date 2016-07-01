using System.Collections.Generic;

namespace IntraVision.Web.Mvc.Controls
{
	/// <summary>
	/// Defines a grid model
	/// </summary>
	public interface IGridModel<T> where T: class 
	{
		IGridRenderer<T> Renderer { get; set; }
        IXlsxGridRenderer<T> XlsxRenderer { get; set; }
		ICollection<GridColumn<T>> Columns { get; }
		IGridSections<T> Sections { get; }
		string EmptyText { get; set; }
		IDictionary<string, object> Attributes { get; set; }
        IEnumerable<GridSortOptions> SortOptions { get; set; }
	}
}