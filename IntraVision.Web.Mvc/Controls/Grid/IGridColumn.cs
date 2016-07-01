using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IntraVision.Web.Mvc.Controls
{
	/// <summary>
	/// Grid Column fluent interface
	/// </summary>
	public interface IGridColumn<T>
	{
		/// <summary>
		/// Specified an explicit name for the column.
		/// </summary>
		/// <param name="name">Name of column</param>
		/// <returns></returns>
		IGridColumn<T> Named(string name);
		/// <summary>
		/// If the property name is PascalCased, it should not be split part.
		/// </summary>
		/// <returns></returns>
		IGridColumn<T> DoNotSplit();
		/// <summary>
		/// A custom format to use when building the cell's value
		/// </summary>
		/// <param name="format">Format to use</param>
		/// <returns></returns>
		IGridColumn<T> Format(string format);
		/// <summary>
		/// Delegate used to hide the contents of the cells in a column.
		/// </summary>
		IGridColumn<T> CellCondition(Func<T, bool> func);

		/// <summary>
		/// Determines whether the column should be displayed
		/// </summary>
		/// <param name="isVisible"></param>
		/// <returns></returns>
		IGridColumn<T> Visible(bool isVisible);

        IGridColumn<T> Total();
        IGridColumn<T> TotalName();
        IGridColumn<T> TotalText(string totalText);
        IGridColumn<T> TotalColspan(int totalColspan);

		/// <summary>
		/// Do not HTML Encode the output
		/// </summary>
		/// <returns></returns>
		IGridColumn<T> DoNotEncode();

		/// <summary>
		/// Defines additional attributes for the column heading.
		/// </summary>
		/// <param name="attributes"></param>
		/// <returns></returns>
		IGridColumn<T> HeaderAttributes(IDictionary<string, object> attributes);

        IGridColumn<T> HeaderXlsxAttributes(IDictionary<string, object> attributes);

		/// <summary>
		/// Defines additional attributes for the cell. 
		/// </summary>
		/// <param name="attributes">Lambda expression that should return a dictionary containing the attributes for the cell</param>
		/// <returns></returns>
		IGridColumn<T> Attributes(Func<GridRowViewData<T>, IDictionary<string, object>> attributes);

        IGridColumn<T> XlsxAttributes(Func<GridRowViewData<T>, IDictionary<string, object>> attributes);

        /// <summary>
        /// Specifies whether or not this column should be sortable. 
        /// The default is true. 
        /// </summary>
        /// <param name="isColumnSortable"></param>
        /// <returns></returns>
        IGridColumn<T> Sortable(bool isColumnSortable);

        IGridColumn<T> AutoFilter();

        /// <summary>
        /// Specifies the sort name for this column.
        /// The default is member name. 
        /// </summary>
        /// <param name="isColumnSortable"></param>
        /// <returns></returns>
        IGridColumn<T> Sortable(string sortName);

	    IGridColumn<T> Filtrable(string filterName);

        /// <summary>
        /// Specifies whether or not this column should be searchable
        /// The default is member name. 
        /// </summary>
        /// <param name="isColumnSortable"></param>
        /// <returns></returns>
        IGridColumn<T> Searchable(bool isColumnSearchable);

        /// <summary>
        /// Specifies the search name for this column.
        /// The default is member name. 
        /// </summary>
        /// <param name="isColumnSortable"></param>
        /// <returns></returns>
        IGridColumn<T> Searchable(string searchName);

        /// <summary>
        /// Specifies whether or not this column should be searchable
        /// The default is member name. 
        /// </summary>
        /// <param name="isColumnSortable"></param>
        /// <returns></returns>
        IGridColumn<T> NumericSearchable(bool isColumnSearchable);

        /// <summary>
        /// Specifies the search name for this column.
        /// The default is member name. 
        /// </summary>
        /// <param name="isColumnSortable"></param>
        /// <returns></returns>
        IGridColumn<T> NumericSearchable(string searchName);

		/// <summary>
		/// Custom header renderer
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)] //hide from intellisense in fluent interface
		Action<RenderingContext> CustomHeaderRenderer { get; set; }

		/// <summary>
		/// Custom item renderer
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)] //hide from intellisense in fluent interface
		Action<RenderingContext, T> CustomItemRenderer { get; set; }
	}
}