﻿using System.Web;
using System.Web.Mvc;

namespace DatabaseWithC__CRUD
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}