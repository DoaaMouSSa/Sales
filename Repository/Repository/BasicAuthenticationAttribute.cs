using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using Dto.Dto;
using System.Threading;
using System.Security.Principal;

namespace Repository.Repository
{
  public  class BasicAuthenticationAttribute:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization==null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string token = actionContext.Request.Headers.Authorization.Parameter;
                string decodeToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                string[] usernameAndpassword = decodeToken.Split(":");
                string user_name = usernameAndpassword[0];
                string password = usernameAndpassword[1];
                dtoLogin creditinal = new dtoLogin() { 
                    user_name=user_name,
                    password=password
                };
                LoginRepository login = new LoginRepository();
                if (login.Login(creditinal).code=="200")
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(user_name), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            base.OnAuthorization(actionContext);
        }
    }
}
