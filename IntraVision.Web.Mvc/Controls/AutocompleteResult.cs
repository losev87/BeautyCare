using System;
using System.Collections.Generic;
using System.Linq;

namespace IntraVision.Web.Mvc
{
    [Serializable]
    public class AutocompleteResult<TEntity>
        where TEntity : class
    {
        public string query { get; set; }
        public string[] suggestions { get; set; }
        public long[] data { get; set; }

        public AutocompleteResult(string search, IEnumerable<TEntity> entities, Func<TEntity, string> nameAccessor, Func<TEntity, long> valueAccessor)
        {
            query = search;
            if (entities != null)
            {
                suggestions = entities.Select(nameAccessor).ToArray();
                data = entities.Select(valueAccessor).ToArray();
            }
        }
    }
}
