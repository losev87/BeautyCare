using System.Linq;

namespace IntraVision.Web.Mvc.Controls
{
    public interface IFilterCondition
    {
        string Caption { get; }
        string Column { get; }
        FilterConditionValue Value { get; set; }
    }

    public interface IFilterCondition<TEntity> : IFilterCondition where TEntity : class
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> query);
    }
}
