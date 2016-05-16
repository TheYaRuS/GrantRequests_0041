using GrantRequests.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrantRequests.WEB.Filters
{
    public class CustomRoleAuthorizeAttribute : AuthorizeAttribute
    {
        Role[] roles;
        public CustomRoleAuthorizeAttribute(params Role[] roles)
        {
            this.roles = roles; 
        }        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return roles.Contains(httpContext.User.Identity.GetRole());             
        }
    }
}