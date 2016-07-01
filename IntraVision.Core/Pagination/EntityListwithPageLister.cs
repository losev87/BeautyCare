using System.Collections.Generic;

namespace IntraVision.Core.Pagination
{
	public class EntityListwithPageLister<TEntity>
	{
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int CountPage { get; set; }

        public IEnumerable<TEntity> TEntityList { get; set; }
	}
}