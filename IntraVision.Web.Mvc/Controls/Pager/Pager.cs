using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using IntraVision.Core.Pagination;

namespace IntraVision.Web.Mvc.Controls
{
	/// <summary>
	/// Renders a pager component from an IPagination datasource.
	/// </summary>
	public class Pager
	{
		private readonly IPagination _pagination;
        private readonly HtmlHelper _helper;

		private string _paginationFormat = Resources.Grid.PaginationFormat;
        private string _paginationSingleFormat = Resources.Grid.PaginationSingleFormat;
		private string _paginationFirst = Resources.Grid.PaginationFirst;
		private string _paginationPrev = Resources.Grid.PaginationPrev;
		private string _paginationNext = Resources.Grid.PaginationNext;
        private string _paginationLast = Resources.Grid.PaginationLast;
        private string _paginationPrevN = Resources.Grid.PaginationPrevN;
        private string _paginationNextN = Resources.Grid.PaginationNextN;
		private string _pageQueryName = "page";

		/// <summary>
		/// Creates a new instance of the Pager class.
		/// </summary>
		/// <param name="pagination">The IPagination datasource</param>
		/// <param name="request">The current HTTP Request</param>
		public Pager(IPagination pagination, HtmlHelper helper)
		{
			_pagination = pagination;
            _helper = helper;
		}

		/// <summary>
		/// Specifies the query string parameter to use when generating pager links. The default is 'page'
		/// </summary>
		public Pager QueryParam(string queryStringParam)
		{
			_pageQueryName = queryStringParam;
			return this;
		}

		/// <summary>
		/// Specifies the format to use when rendering a pagination containing a single page. 
		/// The default is 'Showing {0} of {1}' (eg 'Showing 1 of 3')
		/// </summary>
		public Pager SingleFormat(string format)
		{
			_paginationSingleFormat = format;
			return this;
		}

		/// <summary>
		/// Specifies the format to use when rendering a pagination containing multiple pages. 
		/// The default is 'Showing {0} - {1} of {2}' (eg 'Showing 1 to 3 of 6')
		/// </summary>
		public Pager Format(string format)
		{
			_paginationFormat = format;
			return this;
		}

		/// <summary>
		/// Text for the 'first' link.
		/// </summary>
		public Pager First(string first)
		{
			_paginationFirst = first;
			return this;
		}

		/// <summary>
		/// Text for the 'prev' link
		/// </summary>
		public Pager Previous(string previous)
		{
			_paginationPrev = previous;
			return this;
		}

		/// <summary>
		/// Text for the 'next' link
		/// </summary>
		public Pager Next(string next)
		{
			_paginationNext = next;
			return this;
		}

		/// <summary>
		/// Text for the 'last' link
		/// </summary>
		public Pager Last(string last)
		{
			_paginationLast = last;
			return this;
		}

		public override string ToString()
		{
			if(_pagination.TotalItems == 0)
			{
				return null;
			}

			var builder = new StringBuilder();

			builder.Append("<div class='pagination'>");
			RenderLeftSideOfPager(builder);

			if(_pagination.TotalPages > 1)
			{
                RenderBeforePages(builder);

                RenderCenterSideOfPager(builder);

				RenderRightSideOfPager(builder);
			}

			builder.Append(@"</div>");

			return builder.ToString();
		}

		protected virtual void RenderLeftSideOfPager(StringBuilder builder)
		{
			builder.Append("<span class='paginationLeft'>");

			//Special case handling where the page only contains 1 item (ie it's a details-view rather than a grid)
			if(_pagination.PageSize == 1)
			{
				RenderNumberOfItemsWhenThereIsOnlyOneItemPerPage(builder);
			}
			else
			{
				RenderNumberOfItemsWhenThereAreMultipleItemsPerPage(builder);
			}
			builder.Append("</span>");
		}

        protected virtual void RenderCenterSideOfPager(StringBuilder builder)
        {
            var pageCount = _pagination.TotalItems / _pagination.PageSize;
            var pageLast = _pagination.TotalItems % _pagination.PageSize;

            if (pageLast > 0)
                pageCount++;

            if (pageCount > 1)
            {
                builder.Append("<span class='paginationCenter'>");

                int startPage = _pagination.PageNumber%_pagination.PagesOnPage != 0
                    ? (_pagination.PageNumber / _pagination.PagesOnPage) * _pagination.PagesOnPage + 1
                    : ((_pagination.PageNumber / _pagination.PagesOnPage) - 1) * _pagination.PagesOnPage + 1;
                int lastPage = ((((_pagination.PageNumber % _pagination.PagesOnPage != 0)
                    ? ((_pagination.PageNumber / _pagination.PagesOnPage + 1) * _pagination.PagesOnPage)
                    : ((_pagination.PageNumber / _pagination.PagesOnPage) * _pagination.PagesOnPage))
                                 <
                                 _pagination.TotalPages)
                    ? ((_pagination.PageNumber % _pagination.PagesOnPage != 0)
                        ? ((_pagination.PageNumber / _pagination.PagesOnPage + 1) * _pagination.PagesOnPage)
                        : ((_pagination.PageNumber / _pagination.PagesOnPage) * _pagination.PagesOnPage))
                    : _pagination.TotalPages);

                for(int i=startPage;i<=lastPage; i++)
                {
                    var link = CreatePageLink(i, i.ToString());
                    
                    if (i == _pagination.PageNumber)
                        builder.Append(" <span class='currentPage'>" + link + "</span> ");
                    else
                        builder.Append(" " + link + " ");

                    if (i != lastPage)
                        builder.Append("|");
                }

                builder.Append("</span>");
            }
        }

	    protected virtual void RenderBeforePages(StringBuilder builder)
	    {
            builder.Append("<span class='paginationRight'>");

            //If we're on page 1 then there's no need to render a link to the first page. 
            if (_pagination.PageNumber == 1)
            {
                builder.Append(_paginationFirst);
            }
            else
            {
                builder.Append(CreatePageLink(1, _paginationFirst));
            }

            builder.Append(" | ");

            //If we're on page N or later, then render a link to the previous page. 
            //If we're on the first page, then there is no need to render a link to the previous page. 
            if (_pagination.HasPreviousN)
            {
                builder.Append(CreatePageLink((_pagination.PageNumber % _pagination.PagesOnPage!=0?_pagination.PagesOnPage * (_pagination.PageNumber / _pagination.PagesOnPage):_pagination.PagesOnPage * (_pagination.PageNumber / _pagination.PagesOnPage - 1)), string.Format(_paginationPrevN, _pagination.PagesOnPage)));
            }
            else
            {
                builder.AppendFormat(_paginationPrevN, _pagination.PagesOnPage);
            }

            builder.Append(" | ");

            //If we're on page 2 or later, then render a link to the previous page. 
            //If we're on the first page, then there is no need to render a link to the previous page. 
            if (_pagination.HasPreviousPage)
            {
                builder.Append(CreatePageLink(_pagination.PageNumber - 1, _paginationPrev));
            }
            else
            {
                builder.Append(_paginationPrev);
            }

            builder.Append("</span>");

	    }
		protected virtual void RenderRightSideOfPager(StringBuilder builder)
		{

            builder.Append("<span class='paginationRight'>");

			//Only render a link to the next page if there is another page after the current page.
			if(_pagination.HasNextPage)
			{
				builder.Append(CreatePageLink(_pagination.PageNumber + 1, _paginationNext));
			}
			else
			{
				builder.Append(_paginationNext);
			}

			builder.Append(" | ");

            //If we're on page N or later, then render a link to the previous page. 
            //If we're on the first page, then there is no need to render a link to the previous page. 
            if (_pagination.HasNextN)
            {
                builder.Append(CreatePageLink((_pagination.PageNumber % _pagination.PagesOnPage != 0 ? _pagination.PagesOnPage * (_pagination.PageNumber / _pagination.PagesOnPage + 1) + 1 : _pagination.PagesOnPage * (_pagination.PageNumber / _pagination.PagesOnPage) + 1), string.Format(_paginationNextN, _pagination.PagesOnPage)));
            }
            else
            {
                builder.AppendFormat(_paginationNextN, _pagination.PagesOnPage);
            }

            builder.Append(" | ");

			int lastPage = _pagination.TotalPages;

			//Only render a link to the last page if we're not on the last page already.
			if(_pagination.PageNumber < lastPage)
			{
				builder.Append(CreatePageLink(lastPage, _paginationLast));
			}
			else
			{
				builder.Append(_paginationLast);
			}

			builder.Append("</span>");
		}

		protected virtual void RenderNumberOfItemsWhenThereIsOnlyOneItemPerPage(StringBuilder builder) 
		{
			builder.AppendFormat(_paginationSingleFormat, _pagination.FirstItem, _pagination.TotalItems);
		}

		protected virtual void RenderNumberOfItemsWhenThereAreMultipleItemsPerPage(StringBuilder builder) 
		{
			builder.AppendFormat(_paginationFormat, _pagination.FirstItem, _pagination.LastItem, _pagination.TotalItems);
		}

		private string CreatePageLink(int pageNumber, string text)
		{
            var dict = _helper.ViewContext.RouteData.Values;
            foreach (var queryString in _helper.ViewContext.HttpContext.Request.QueryString.AllKeys)
            {
                dict[queryString] = _helper.ViewContext.HttpContext.Request.QueryString[queryString];
            }
            dict.Remove("options");
            dict["page"] = pageNumber;
            string action = _helper.ViewContext.RouteData.Values["action"].ToString();
            return _helper.RouteLink(text, dict).ToString();
		}

		private string CreateQueryString(NameValueCollection values)
		{
			var builder = new StringBuilder();

			foreach(string key in values.Keys)
			{
				if(key == _pageQueryName)
					//Don't re-add any existing 'page' variable to the querystring - this will be handled in CreatePageLink.
				{
					continue;
				}

				foreach(var value in values.GetValues(key))
				{
					builder.AppendFormat("&amp;{0}={1}", key, HttpUtility.HtmlEncode(value));
				}
			}

			return builder.ToString();
		}
	}
}