using IntraVision.Data;

namespace IntraVision.Web.Mvc.Services
{
    public interface ISortableDictionaryService<TEntity,TViewModel> : IDictionaryService<TEntity,TViewModel>
        where TEntity : IEntity, new()
        where TViewModel : new()
    {
        void Sort(long[] ids);
    }
}
