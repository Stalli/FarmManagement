using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Routing;

namespace FarmManagement.Lib
{
    public static class HelperMethod
    {
        public static MvcHtmlString GetCustomLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return GetCustomLabelFor<TModel, TValue>(helper, expression, null);
        }

        public static MvcHtmlString GetCustomLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metaData.DisplayName ?? metaData.PropertyName ?? htmlFieldName.Split('.').Last();

            if (String.IsNullOrEmpty(labelText))
                return MvcHtmlString.Empty;

            var label = new TagBuilder("label");
            label.Attributes.Add("for", helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            label.InnerHtml = labelText;

            if (htmlAttributes != null)
            {
                var routeValues = new RouteValueDictionary(htmlAttributes);
                label.MergeAttributes(routeValues);
            }

            return MvcHtmlString.Create(label.ToString());
        }

        public static MvcHtmlString CustomLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, bool isAddControlLabelClass = true)
        {
            return CustomLabelFor<TModel, TValue>(helper, expression, null, isAddControlLabelClass);
        }

        public static MvcHtmlString CustomLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes, bool isAddControlLabelClass = true)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metaData.DisplayName ?? metaData.PropertyName ?? htmlFieldName.Split('.').Last();

            if (metaData.IsRequired)
            {
                labelText += " <span class=\"required\">*</span>";
            }

            if (String.IsNullOrEmpty(labelText))
                return MvcHtmlString.Empty;

            var label = new TagBuilder("label");
            label.Attributes.Add("for", helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            label.InnerHtml = labelText;

            var routeValues = new RouteValueDictionary(htmlAttributes);
            if (routeValues.Keys.Contains("class") && isAddControlLabelClass)
            {
                routeValues["class"] += " control-label";
            }
            else if (isAddControlLabelClass)
            {
                label.AddCssClass("control-label");
            }

            label.MergeAttributes(routeValues);

            return MvcHtmlString.Create(label.ToString());
        }

        public static MvcHtmlString GetDisplayName<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            string value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));
            return MvcHtmlString.Create(value);
        }

        public static MvcHtmlString IsDisabled(this MvcHtmlString htmlString, bool disabled)
        {
            string rawstring = htmlString.ToString();
            if (disabled)
            {
                rawstring = rawstring.Insert(rawstring.Length - 2, "disabled=\"disabled\"");
            }
            return new MvcHtmlString(rawstring);
        }

        public static MvcHtmlString IsReadonly(this MvcHtmlString htmlString, bool @readonly)
        {
            string rawstring = htmlString.ToString();
            if (@readonly)
            {
                rawstring = rawstring.Insert(rawstring.Length - 2, "readonly=\"readonly\"");
            }
            return new MvcHtmlString(rawstring);
        }
    }
}