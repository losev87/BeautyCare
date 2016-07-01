using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using IntraVision.Web.Mvc.Controls.Filter;
using IntraVision.Web.Mvc.Services;

namespace IntraVision.Web.Mvc.Controls
{
    public class Filter<TEntity> : IFilter<TEntity>
        where TEntity: class
    {
        protected IFilterService _FilterService;
        protected IList<IFilterCondition<TEntity>> _Conditions;

        public bool AllowSave { get { return _FilterService != null; } }
        public long FilterId { get; private set; }
        public FilterEdit FilterEdit { get; private set; }
        public IPrincipal User { get; set; }

        public IEnumerable<SavedFilter> SavedFilters { get; private set; }
        public IEnumerable<IFilterCondition> Conditions { get { return _Conditions.Cast<IFilterCondition>(); } }

        /// <summary>
        /// Передайте FilterService 
        /// </summary>
        /// <param name="filterService"></param>
        public Filter(IFilterService filterService) : this()
        {
            _FilterService = filterService;
        }

        public Filter()
        {
            _Conditions = new List<IFilterCondition<TEntity>>();
        }

        public Filter<TEntity> Configure(IPrincipal user)
        {
            User = user;
            return Configure();
        }

        protected virtual Filter<TEntity> Configure()
        {
            return this;
        }

        public void AddCondition(IFilterCondition<TEntity> condition)
        {
            _Conditions.Add(condition);
        }

        public Filter<TEntity> Init(IGridOptions options)
        {
            return Init(options, null);
        }

        public Filter<TEntity> Init(IGridOptions options, string gridKey)
        {
            if (options != null)
            {
                foreach (var fc in _Conditions)
                    fc.Value = options.GetFilterConditionValue(fc.Column);

                FilterId = options.FilterId;
            }

            if (AllowSave && !string.IsNullOrEmpty(gridKey) && User != null)
            {
                SavedFilters = _FilterService.GetSavedFilters(gridKey, User);

                var filters = SavedFilters.ToSelectList(f => f.Id, f => f.Name, f => f.Id == FilterId).ToList();
                filters.Insert(0, new SelectListItem { Text = "Новый фильтр", Value = "0" });

                FilterEdit = new FilterEdit { Filters = filters };
            }
            return this;
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            if (_Conditions != null)
            {
                foreach (IFilterCondition<TEntity> condition in _Conditions)
                    query = condition.Apply(query);
            }
            return query;
        }
    }
}
