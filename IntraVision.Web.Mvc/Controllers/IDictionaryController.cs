using System.Web.Mvc;
using IntraVision.Data;

namespace IntraVision.Web.Mvc
{
    /// <summary>
    /// Интерфейс для контроллера, поддерживающего операции просмотра списка, создания, редактирования и удаления записей.
    /// Такой контроллер рекомендуется использовать для словарей. 
    /// Создание и редактирование сущностей осуществляется через всплывающие окна, форма сабмитится Ajax'ом.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public interface IDictionaryController<TEntity,TViewModel>
        where TEntity : IEntity, new()
        where TViewModel : class, new()
    {
        ActionResult Index();
        ActionResult List();
        
        ActionResult Create();
        JsonResult Create(TViewModel create);

        ActionResult Edit(long id);
        JsonResult Edit(TViewModel edit);

        JsonResult Delete(long id);
    }

    /// <summary>
    /// Интерфейс для контроллера, поддерживающего операции просмотра списка, создания, редактирования, сортировки и удаления записей.
    /// Такой контроллер рекомендуется использовать для словарей. 
    /// Создание и редактирование сущностей осуществляется через всплывающие окна, форма сабмитится Ajax'ом.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public interface ISortableDictionaryController<TEntity,TViewModel> : IDictionaryController<TEntity,TViewModel>
        where TEntity : IEntity, new()
        where TViewModel : class, new()
    {
        JsonResult Sort(long[] ids);
    }
}
