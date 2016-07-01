using System.Linq;
using IntraVision.Data;
using IntraVision.Web.Mvc.Controls;

namespace IntraVision.Web.Mvc
{
    public class GridOptionsBase<TEntity> : GridOptions
        where TEntity : class, INamedEntityBase
    {
        public GridOptionsBase()
        {
            foreach (var option in typeof(TEntity)
                                    .GetProperties()
                                    .Select(p => p.GetCustomAttributes(false).OfType<GridOptionsAttribute>().FirstOrDefault())
                                    .Where(a => a != null).OrderBy(a => a.Order))
            {
                SortOptions.Add(new GridSortOptions { Column = option.Column, Direction = option.Direction });
            }

            foreach (var condition in typeof(TEntity)
                                    .GetProperties()
                                    .Select(p => p.GetCustomAttributes(false).OfType<FilterConditionsAttribute>().FirstOrDefault())
                                    .Where(a => a != null))
            {
                DefaultFilterConditions.Add(new FilterConditionValue { Column = condition.Column, Condition = condition.Condition, Values = condition.Values });
            }
        }
    }
}
