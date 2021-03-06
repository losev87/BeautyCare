using System;
using System.Collections.Generic;

namespace IntraVision.Web.Mvc.Controls
{
	/// <summary>
	/// Default model for grid
	/// </summary>
	public class GridModel<T>  : IGridModel<T> where T : class
	{
		private readonly ColumnBuilder<T> _columnBuilder;
		private readonly GridSections<T> _sections = new GridSections<T>();
		private IGridRenderer<T> _renderer = new HtmlTableGridRenderer<T>();
        private IXlsxGridRenderer<T> _xlsxRenderer = new XlsxGridRenderer<T>();
		private string _emptyText = Resources.Grid.EmptyText;
		private IDictionary<string, object> _attributes = new Dictionary<string, object>();
        private IEnumerable<GridSortOptions> _sortOptions;
	    private int _rowNumber = 1;

        public int GetRowNumber()
        {
            return _rowNumber++;
        }

        IEnumerable<GridSortOptions> IGridModel<T>.SortOptions
		{
			get { return _sortOptions; }
			set { _sortOptions = value; }
		}

		ICollection<GridColumn<T>> IGridModel<T>.Columns
		{
			get { return _columnBuilder; }
		}

		IGridRenderer<T> IGridModel<T>.Renderer
		{
			get { return _renderer; }
			set { _renderer = value; }
		}

        IXlsxGridRenderer<T> IGridModel<T>.XlsxRenderer
        {
            get { return _xlsxRenderer; }
            set { _xlsxRenderer = value; }
        }

		string IGridModel<T>.EmptyText
		{
			get { return _emptyText; }
			set { _emptyText = value; }
		}

		IDictionary<string, object> IGridModel<T>.Attributes
		{
			get { return _attributes; }
			set { _attributes = value; }
		}

		/// <summary>
		/// Creates a new instance of the GridModel class
		/// </summary>
		public GridModel()
		{
			_columnBuilder = CreateColumnBuilder();
		}

		/// <summary>
		/// Column builder for this grid model
		/// </summary>
		public ColumnBuilder<T> Column
		{
			get { return _columnBuilder; }
		}

		/// <summary>
		/// Section overrides for this grid model.
		/// </summary>
		IGridSections<T> IGridModel<T>.Sections
		{
			get { return _sections; }
		}

		/// <summary>
		/// Section overrides for this grid model.
		/// </summary>
		public GridSections<T> Sections
		{
			get { return _sections; }
		}

		/// <summary>
		/// Text that will be displayed when the grid has no data.
		/// </summary>
		/// <param name="emptyText">Text to display</param>
		public void Empty(string emptyText)
		{
			_emptyText = emptyText;
		}

		/// <summary>
		/// Defines additional attributes for the grid.
		/// </summary>
		/// <param name="hash"></param>
		public void Attributes(params Func<object, object>[] hash)
		{
			Attributes(new Hash(hash));
		}

		/// <summary>
		/// Defines additional attributes for the grid
		/// </summary>
		/// <param name="attributes"></param>
		public void Attributes(IDictionary<string, object> attributes)
		{
			_attributes = attributes;
		}

		/// <summary>
		/// Specifies the Renderer to use with this grid. If omitted, the HtmlTableGridRenderer will be used. 
		/// </summary>
		/// <param name="renderer">The Renderer to use</param>
		public void RenderUsing(IGridRenderer<T> renderer)
		{
			_renderer = renderer;
		}

		/// <summary>
		/// Secifies that the grid is currently being sorted by the specified column in a particular direction.
		/// </summary>
		public void Sort(IEnumerable<GridSortOptions> sortOptions)
		{
            _sortOptions = sortOptions;
        }

		protected virtual ColumnBuilder<T> CreateColumnBuilder()
		{
			return new ColumnBuilder<T>();
		}
	}
}
