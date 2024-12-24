using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Common.Common
{
    public class RequireQueryParameterAttribute : ActionFilterAttribute
    {
        private readonly string _parameterName;

        public RequireQueryParameterAttribute(string parameterName)
        {
            _parameterName = parameterName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var hasParameter = context.HttpContext.Request.Query.ContainsKey(_parameterName);

            if (!hasParameter)
            {
                context.Result = new BadRequestObjectResult($"The '{_parameterName}' query parameter is required.");
            }

            base.OnActionExecuting(context);
        }
    }
}
