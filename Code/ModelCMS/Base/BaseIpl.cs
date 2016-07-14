﻿using LibCore.Caching;
using LibCore.EF;
using LibCore.Helper.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelCMS.Base
{
    public class BaseIpl<T>
    {
        public T unitOfWork;
        protected ICacheProvider cache;
        protected CacheHelper cacheHelper;
        protected string _schema;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseIpl{T}" /> class.
        /// </summary>
        /// <param name="schema">The schema.</param>
        public BaseIpl(string schema = "dbo")
        {
            _schema = schema;
            cache = new MemcachedProvider(schema);
            cacheHelper = new CacheHelper(schema);
            unitOfWork = (T)SingletonIpl.GetInstance<T>(schema);
            //unitOfWork.CacheHelper = cacheHelper;
        }
    }
}