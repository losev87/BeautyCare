using System.Collections.Generic;

namespace IntraVision.Web.Mvc
{
    public class ListViewModel<TEntity> : ListViewModelBase
        where TEntity : class
    {
        public IEnumerable<TEntity> Data { get; set; }

        public ListViewModel(IEnumerable<TEntity> data)
            : base()
        {
            Data = data;
        }
    }
}
