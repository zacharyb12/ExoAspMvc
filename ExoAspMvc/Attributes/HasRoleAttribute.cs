
using ExoAspMvc.Models.RoleEnum;
using ExoAspMvc.SessionExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ExoAspMvc.Attributes
{
    public class HasRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Role[] _roles = [];

        public HasRoleAttribute(params Role[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Role? role = context.HttpContext.Session.GetRole();
            if (role == null || !_roles.Contains((Role)role))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
