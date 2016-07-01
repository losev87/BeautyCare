﻿using IntraVision.Core.Pagination;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc.Controls
{
    public interface IGridViewModel
    {
        string Render(HtmlHelper html);
    }

    public interface IGridViewModel<TEntity> : IGridViewModel
        where TEntity : class
    {
        GridOptions GridOptions { get; set; }
        IGridModel<TEntity> PrepareModel(HtmlHelper helper, GridOptions options);
        IFilter GetFilter();
        IGridViewModel<TEntity> Prepare();
        IPagination<TEntity> Data { get; }
    }
}
