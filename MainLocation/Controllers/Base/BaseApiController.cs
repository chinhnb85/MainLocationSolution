using MainLocation.Code;
using ModelCMS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MainLocation.Controllers.Base
{
    public class BaseApiController : ApiController
    {        
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            if (controllerContext.Request.Headers.Host != "localhost:2016")
            {
                throw new HttpResponseException(controllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Access denied!"));
            }
            base.Initialize(controllerContext);
        }        
    }
}
