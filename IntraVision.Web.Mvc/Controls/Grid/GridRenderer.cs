using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc.Controls
{
	/// <summary>
	/// Base class for Grid Renderers. 
	/// </summary>
	public abstract class GridRenderer<T> : IGridRenderer<T> where T : class 
	{
		protected IGridModel<T> GridModel { get; private set; }
        protected IFilter<T> Filter { get; private set; }
		protected IEnumerable<T> DataSource { get; private set; }
		protected ViewContext Context { get; private set; }
		private TextWriter _writer;
		private readonly ViewEngineCollection _engines;
        
        protected  TextWriter Writer
		{
			get { return _writer; }
		}

		protected GridRenderer() : this(ViewEngines.Engines) {}

		protected GridRenderer(ViewEngineCollection engines)
		{
			_engines = engines;
		}

		public void Render(IGridModel<T> gridModel, IFilter<T> filter, IEnumerable<T> dataSource, TextWriter output, ViewContext context)
		{
			_writer = output;
			GridModel = gridModel;
		    Filter = filter;
			DataSource = dataSource;
			Context = context;

			RenderGridStart();
			bool hasItems = RenderHeader();

            if (filter != null)
            {
                //RenderQuickFilter();
            }

		    if(hasItems)
			{
				RenderItems();
			}
			else
			{
				RenderEmpty();
			}

			RenderGridEnd(!hasItems);
		}

		protected void RenderText(string text)
		{
			Writer.Write(text);
		}

		protected virtual void RenderItems()
		{
			RenderBodyStart();

			bool isAlternate = false;
			foreach(var item in DataSource)
			{
				RenderItem(new GridRowViewData<T>(item, isAlternate));
				isAlternate = !isAlternate;
			}

		    RenderTotal();
		    RenderBodyEnd();
		}

        protected virtual void RenderTotal()
        {
            if (VisibleColumns().OrderBy(c => c.Order).Any(c => c.Total))
            {
                RenderText("<tr>");
                foreach (var column in VisibleColumns().OrderBy(c => c.Order))
                {
                    if (column.Total)
                    {
                        var values = DataSource.Select(s => column.GetValue(s));
                        RenderText(string.Format("<td>{0}</td>", values.Cast<int>().Sum()));
                    }
                    else if(column.TotalName)
                    {
                        RenderText(string.Format("<td>{0}</td>", column.TotalText));
                    }
                    else
                    {
                        RenderText("<td></td>");
                    }
                }
                RenderText("</tr>");
            }
        }

		protected virtual void RenderItem(GridRowViewData<T> rowData)
		{
			BaseRenderRowStart(rowData);

			foreach(var column in VisibleColumns().OrderBy(c=>c.Order))
			{
				//A custom item section has been specified - render it and continue to the next iteration.
				if (column.CustomItemRenderer != null)
				{
					column.CustomItemRenderer(new RenderingContext(Writer, Context, _engines), rowData.Item);
					continue;
				}

				RenderStartCell(column, rowData);
				RenderCellValue(column, rowData);
				RenderEndCell();
			}

			BaseRenderRowEnd(rowData);
		}

		protected virtual void RenderCellValue(GridColumn<T> column, GridRowViewData<T> rowData)
		{
			var cellValue = column.GetValue(rowData.Item);

			if(cellValue != null)
			{
				RenderText(cellValue.ToString());
			}
		}

        protected virtual bool RenderHeader()
        {
            //No items - do not render a header.
            if (!ShouldRenderHeader()) return false;

            RenderHeadStart();

            foreach (var column in VisibleColumns().OrderBy(c => c.Order))
            {
                //Allow for custom header overrides.
                if (column.CustomHeaderRenderer != null)
                {
                    column.CustomHeaderRenderer(new RenderingContext(Writer, Context, _engines));
                }
                else
                {
                    RenderHeaderCellStart(column);
                    RenderHeaderText(column);
                    RenderHeaderCellEnd();
                }
            }

            RenderHeadEnd();

            return true;
        }

        protected virtual void RenderQuickFilter()
		{
			if (IsDataSourceEmpty()) return;

            RenderQuickFilterStart();

			foreach(var column in VisibleColumns().OrderBy(c=>c.Order))
			{
				//Allow for custom header overrides.
                if (column.CustomQuickFilterRenderer != null)
				{
                    column.CustomQuickFilterRenderer(new RenderingContext(Writer, Context, _engines));
				}
				else
				{
                    RenderQuickFilterCellStart(column);
                    RenderQuickFilterHtml(column);
                    RenderQuickFilterCellEnd();
				}
			}

            RenderQuickFilterEnd();
		}

        protected virtual void RenderHeaderText(GridColumn<T> column)
        {
            RenderText(column.DisplayName);
        }

		protected virtual bool ShouldRenderHeader()
		{
			return !IsDataSourceEmpty();
		}

		protected bool IsDataSourceEmpty()
		{
			return DataSource == null || !DataSource.Any();
		}

		protected IEnumerable<GridColumn<T>> VisibleColumns()
		{
			return GridModel.Columns.Where(x => x.Visible);
		}

		protected void BaseRenderRowStart(GridRowViewData<T> rowData)
		{
			bool rendered = GridModel.Sections.Row.StartSectionRenderer(rowData, new RenderingContext(Writer, Context, _engines));

			if(! rendered)
			{
				RenderRowStart(rowData);
			}
		}

		protected void BaseRenderRowEnd(GridRowViewData<T> rowData)
		{
			bool rendered = GridModel.Sections.Row.EndSectionRenderer(rowData, new RenderingContext(Writer, Context, _engines));

			if(! rendered)
			{
				RenderRowEnd();
			}
		}

		protected bool IsSortingEnabled
		{
			get { return GridModel.SortOptions != null; }
		}

		protected abstract void RenderHeaderCellEnd();
		protected abstract void RenderHeaderCellStart(GridColumn<T> column);
        protected abstract void RenderQuickFilterCellEnd();
        protected abstract void RenderQuickFilterCellStart(GridColumn<T> column);
		protected abstract void RenderRowStart(GridRowViewData<T> rowData);
		protected abstract void RenderRowEnd();
		protected abstract void RenderEndCell();
		protected abstract void RenderStartCell(GridColumn<T> column, GridRowViewData<T> rowViewData);
		protected abstract void RenderHeadStart();
		protected abstract void RenderHeadEnd();
        protected abstract void RenderQuickFilterStart();
        protected abstract void RenderQuickFilterEnd();
		protected abstract void RenderGridStart();
		protected abstract void RenderGridEnd(bool isEmpty);
		protected abstract void RenderEmpty();
		protected abstract void RenderBodyStart();
		protected abstract void RenderBodyEnd();
	    protected abstract void RenderQuickFilterHtml(GridColumn<T> column);
	}
}
