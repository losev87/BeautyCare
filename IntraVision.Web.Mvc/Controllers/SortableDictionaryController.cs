namespace IntraVision.Web.Mvc
{
    //public class SortableDictionaryController<TEntity,TViewModel> : DictionaryController<TEntity,TViewModel>, ISortableDictionaryController<TEntity,TViewModel>
    //    where TEntity : INamedEntity, ISortableEntity, new()
    //    where TViewModel : class, new ()
    //{
    //    private ISortableDictionaryService<TEntity, TViewModel> _sortableService { get { return _service as ISortableDictionaryService<TEntity, TViewModel>; } }

    //    public SortableDictionaryController(ISortableDictionaryService<TEntity, TViewModel> service) : base(service) { }

    //    [HttpGet]
    //    public virtual ActionResult Sort()
    //    {
    //        return View(_service.GetAll().Cast<INamedEntity>());
    //    }

    //    [HttpPost]
    //    public virtual JsonResult Sort(long[] ids)
    //    {
    //        return ExecuteCommand(() => _sortableService.Sort(ids));
    //    }

    //    protected override IListViewModel GetListViewModel()
    //    {
    //        var model = base.GetListViewModel();
    //        model.Sortable = true;
    //        return model;
    //    }
    //}
}
