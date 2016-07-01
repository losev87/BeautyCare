using System.Collections.Generic;
using IntraVision.Data;

namespace IntraVision.Web.Mvc.Services
{
    public interface IDictionaryService<TEntity,TViewModel>
        where TEntity : IEntity, new()
        where TViewModel : new()
    {
        TEntity Get(long id);
        IEnumerable<TEntity> GetAll();
        TEntity Create(TViewModel entity);
        void Update(TViewModel entity);
        void Delete(long id);
    }
}
