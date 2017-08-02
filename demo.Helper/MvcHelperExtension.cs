﻿

namespace demo.Helper
{
	using System;
	using System.Web.Mvc;
	using System.Web.Mvc.Ajax;
	using System.Web.Mvc.Html;

	public static class MvcHelperExtension
	{
		public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper,
			string linkText,
			string actionName,
			string controllerName,
			object routeValues,
			AjaxOptions ajaxOptions,
			object htmlAttributes)
		{
			var repID = Guid.NewGuid().ToString();
			var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
			return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
		}
	}
}
