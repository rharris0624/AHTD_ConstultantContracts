using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ConsultantContractsInternal.Helpers
{
    public static class HtmlHelpers
    {
/// <summary>
        /// Knockout JS Autocomplete HTML Input
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">Model Propert Name</param>
        /// <param name="jqAutoSource">The observableArray Name</param>
        /// <param name="jqAutoValue">The observable peroperty to store Value for Selected Option</param>
        /// <param name="postUrl">The post URL.</param>
        /// <param name="ajaxMethod">The ajax method Name. Default :autoCompleteCall</param>
        /// <param name="jqAutoSourceLabel">The Select List Label. Default:lbl</param>
        /// <param name="jqAutoSourceInputValue">The input display value. Default:lbl</param>
        /// <param name="jqAutoSourceValue">The value to store in observable. Default:vl</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="validate">if set to <c>true</c> [Apply the Model Validation Properties.].</param>
        /// <param name="extraAjaxparam">The extra parameter to pass in Ajax Method.</param>
        /// <returns></returns>
        public static MvcHtmlString KoAuto<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string jqAutoSource, string jqAutoValue, string postUrl, IDictionary<string, object> extraAjaxparam=null, string ajaxMethod = "autoCompleteCall", string jqAutoSourceLabel = "'lbl'", string jqAutoSourceInputValue = "'lbl'", string jqAutoSourceValue = "'vl'", IDictionary<string, object> htmlAttributes = null, bool validate = false)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var fullHtmlFieldId = ExpressionHelper.GetExpressionText(expression);
            var jqAuto = string.Empty;
            if (!string.IsNullOrWhiteSpace(postUrl))
            {
                jqAuto = "{ autoFocus: true,postUrl:'"+postUrl;
                if (extraAjaxparam!=null)
                {
                    jqAuto += "',extraParam:'";
                    jqAuto = extraAjaxparam.Aggregate(jqAuto, (current, pair) => current + string.Format("{0}:{1},", pair.Key, pair.Value));
                }
                jqAuto += "'}";
 
            }
            var tag = new TagBuilder("input");
            tag.MergeAttribute("type","search");
            var databind = string.Format("jqAuto2: {0}, jqAutoSource: {1}, jqAutoQuery: {2}, jqAutoValue: {3}, jqAutoSourceLabel: {4}, jqAutoSourceInputValue: {5}, jqAutoSourceValue: {6}",
                jqAuto,jqAutoSource,ajaxMethod,jqAutoValue,jqAutoSourceLabel,jqAutoSourceInputValue,jqAutoSourceValue);
            var attributes =htmlAttributes ?? new Dictionary<string,object>();
            attributes.Add("data-bind", databind);
            attributes.Add("id", fullHtmlFieldId);
            attributes.Add("name", fullHtmlFieldName);
            attributes.Add("autocomplete","off");
            tag.MergeAttributes(attributes, true);
            
            if (!validate) return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
            var validationAttributed = htmlHelper.GetUnobtrusiveValidationAttributes(metadata.ContainerType.FullName+'.'+fullHtmlFieldName, metadata);
            tag.MergeAttributes(validationAttributed);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
        }
    }
}