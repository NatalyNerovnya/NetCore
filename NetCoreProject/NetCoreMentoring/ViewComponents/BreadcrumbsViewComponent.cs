using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreMentoring.Models.Components;
using System;
using System.Collections.Generic;

namespace NetCoreMentoring.ViewComponents
{
    public class BreadcrumbsViewComponent : ViewComponent
    {
        private const string defaultActionName = "Index";

        public IViewComponentResult Invoke()
        {
            var model = new BreadcrumbsViewModel()
            {
                Breadcrumbs = BuildBreadcrumbs()
            };

            return View(model);
        }

        private Dictionary<string, string> BuildBreadcrumbs()
        {
            var breadcrumbs = new Dictionary<string, string>();

            var controller = ViewContext.RouteData.Values["controller"].ToString();
            var action = ViewContext.RouteData.Values["action"].ToString();

            breadcrumbs.Add(controller, Url.Action(defaultActionName, controller));

            if (!action.Equals(defaultActionName, StringComparison.InvariantCultureIgnoreCase))
            {
                breadcrumbs.Add(action, HttpContext.Request.Path + HttpContext.Request.QueryString);
            }           

            return breadcrumbs;
        }
    }
}
