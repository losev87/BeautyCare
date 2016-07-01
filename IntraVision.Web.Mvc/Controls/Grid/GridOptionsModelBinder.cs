using System;
using System.Web.Mvc;
using Autofac.Integration.Mvc;

namespace IntraVision.Web.Mvc.Controls
{
    [ModelBinderType(typeof(IGridOptions))]
    public class GridOptionsModelBinder : DefaultModelBinder, IFilteredModelBinder
    {
        public static string GridKey(ControllerContext controllerContext)
        {
            string action = controllerContext.RouteData.Values["action"].ToString().ToUpper();
            //TO FIX: Ugly workaround to make grid and filter actions use the same Session key.
            action =
                action.Replace("GRID", "")
                      .Replace("CLEARFILTER", "")
                      .Replace("LOADFILTER", "")
                      .Replace("SAVEFILTER", "")
                      .Replace("FILTER", "")
                      .Replace("XLSX", "")
                      .Replace("INDEX", "");

            var key = controllerContext.Controller.GetType().Name + "_";

            if (controllerContext.RouteData.Values["controllerGenericType"] != null)
                key += key + controllerContext.RouteData.Values["controller"];

            return key + "_" + action;
        }

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            IGridOptions options = null;
            string key = GridOptionsModelBinder.GridKey(controllerContext);

            if (bindingContext.ValueProvider.GetValue("options") != null && typeof(IGridOptions).IsAssignableFrom(bindingContext.ValueProvider.GetValue("options").RawValue.GetType()))
                options = bindingContext.ValueProvider.GetValue("options").RawValue as IGridOptions;
            else if (controllerContext.HttpContext.Session!=null&&controllerContext.HttpContext.Session[key] != null && typeof(IGridOptions).IsAssignableFrom(controllerContext.HttpContext.Session[key].GetType()))
                options = controllerContext.HttpContext.Session[key] as IGridOptions;
            else if (bindingContext.ModelType.IsClass && !bindingContext.ModelType.IsAbstract)
                options = Activator.CreateInstance(bindingContext.ModelType, null) as IGridOptions;
            else
                options = new GridOptions();

            //Если применен фильтр или выполнен поиск по подстроке, то нужно показать первую страницу
            if (!string.IsNullOrEmpty(controllerContext.HttpContext.Request["SearchString"]) || !string.IsNullOrEmpty(controllerContext.HttpContext.Request["ApplyFilter"]))
                options.Page = 1;

            return options;
        }

        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            base.OnModelUpdated(controllerContext, bindingContext);
            SaveGridOptions(controllerContext, bindingContext.Model as GridOptions);
        }

        public bool IsMatch(ModelBindingContext context)
        {
            return typeof(IGridOptions).IsAssignableFrom(context.ModelType);
        }

        /// <summary>
        /// Принудительно заменить текущее состояние таблицы в сессии. Используется при загрузке сохраненных фильтров.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="options"></param>
        public static void SaveGridOptions(ControllerContext controllerContext, GridOptions options)
        {
            string key = GridOptionsModelBinder.GridKey(controllerContext);
            if (controllerContext.HttpContext.Session!=null)
                controllerContext.HttpContext.Session[key] = options;
        }

        /// <summary>
        /// Принудительно очистить текущее состояние таблицы в сессии. Используется при сбросе фильтра.
        /// </summary>
        /// <param name="controllerContext"></param>
        public static void ClearGridOptions(ControllerContext controllerContext)
        {
            string key = GridOptionsModelBinder.GridKey(controllerContext);
            if (controllerContext.HttpContext.Session != null)
                controllerContext.HttpContext.Session.Remove(key);
        }

        /*
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var helper = new UrlHelper(controllerContext.RequestContext);
            string key = helper.RouteUrl(controllerContext.RequestContext.RouteData.Values);
            GridOptions options = null;

            if (bindingContext.ValueProvider.GetValue("options") != null && bindingContext.ValueProvider.GetValue("options").RawValue is GridOptions)
                options = bindingContext.ValueProvider.GetValue("options").RawValue as GridOptions;
            else if (controllerContext.HttpContext.Session[key] != null && controllerContext.HttpContext.Session[key] is GridOptions)
                options = controllerContext.HttpContext.Session[key] as GridOptions;
            else
                options = new GridOptions();

            bindingContext.Model  = options;

            return base.BindModel(controllerContext, bindingContext);
            
            var helper = new UrlHelper(controllerContext.RequestContext);
            string key = helper.RouteUrl(controllerContext.RequestContext.RouteData.Values);
            GridOptions options = null;

            if (bindingContext.ValueProvider.GetValue("options") != null && bindingContext.ValueProvider.GetValue("options").RawValue is GridOptions)
                options = bindingContext.ValueProvider.GetValue("options").RawValue as GridOptions;
            else if (controllerContext.HttpContext.Session[key] != null && controllerContext.HttpContext.Session[key] is GridOptions)
                options = controllerContext.HttpContext.Session[key] as GridOptions;
            else
                options = new GridOptions();

            if (bindingContext.ValueProvider.GetValue("Page") != null)
                options.Page = (int)bindingContext.ValueProvider.GetValue("Page").ConvertTo(typeof(int));
            if (bindingContext.ValueProvider.GetValue("PageSize") != null)
                options.PageSize = (int)bindingContext.ValueProvider.GetValue("PageSize").ConvertTo(typeof(int));
            if (bindingContext.ValueProvider.GetValue("Column") != null)
            {
                options.SortOptions.Column = (string)bindingContext.ValueProvider.GetValue("Column").ConvertTo(typeof(string));
                options.SortOptions.Direction = SortDirection.Ascending;
            }
            if (bindingContext.ValueProvider.GetValue("Direction") != null)
                options.SortOptions.Direction = (SortDirection)Enum.Parse(typeof(SortDirection), (string)bindingContext.ValueProvider.GetValue("Direction").ConvertTo(typeof(string)));
            if (bindingContext.ValueProvider.GetValue("ClearFilter") != null)
                options.FilterConditions.Clear();

            //Bind FilterConditions
            var form = controllerContext.RequestContext.HttpContext.Request.Form;
            foreach (var fc_key in form.AllKeys.Where(k => k.StartsWith("fct_")))
            {
                string condition = form[fc_key];
                if (string.IsNullOrEmpty(condition)) continue;

                string column = fc_key.Remove(0, 4);
                string strValue = form["fcv_" + column];
                var values = !string.IsNullOrEmpty(strValue) ? strValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList() : null;

                options.FilterConditions.Add(new FilterConditionValue { Column = column, Condition = condition, Values = values });
            }

            controllerContext.HttpContext.Session[key] = options;
            return options;
        }*/
    }
}
