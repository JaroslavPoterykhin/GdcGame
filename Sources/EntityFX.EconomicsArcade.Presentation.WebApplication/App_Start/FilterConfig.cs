﻿using System.Web.Mvc;

namespace EntityFX.EconomicsArcade.Presentation.WebApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
