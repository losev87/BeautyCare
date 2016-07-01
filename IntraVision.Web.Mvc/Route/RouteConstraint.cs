using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Routing;

namespace IntraVision.Web.Mvc.Route
{
    public class RouteConstraint : IRouteConstraint
    {
        private Expression<Func<bool>> _expression;

        public RouteConstraint(Expression<Func<bool>> expression)
        {
            _expression = expression;
        }

        public bool Match(HttpContextBase httpContext, System.Web.Routing.Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return _expression.Compile().Invoke();
        }
    }
}
