﻿using LibCore.EF;
using MainShop.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ModelCMS.Account;

namespace MainShop.Controllers.ApiControllers
{
    public class CategoryApiController : BaseApiController
    {
        // GET api/categoryApi
        public IEnumerable<AccountEntity> Get()
        {
            var ipl = SingletonIpl.GetInstance<IplAccount>();
            var data = ipl.ListAll();
            return data;
        }

        // GET api/categoryApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/categoryApi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/categoryApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/categoryApi/5
        public void Delete(int id)
        {
        }
    }
}
