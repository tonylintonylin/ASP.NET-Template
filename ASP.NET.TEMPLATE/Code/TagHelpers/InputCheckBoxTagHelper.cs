﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.IO;
using System.Text.Encodings.Web;

namespace ASP.NET.TEMPLATE.TagHelpers
{
    [HtmlTargetElement("inputCheckBox", Attributes = ValueAttributeName)]
    public class InputCheckBoxTagHelper : TagHelper
    {
        private const string LabelAttributeName = "label";
        private const string ValueAttributeName = "value";

        [HtmlAttributeName(LabelAttributeName)]
        public string Label { get; set; }

        [HtmlAttributeName(ValueAttributeName)]
        public ModelExpression Value { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        public InputCheckBoxTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string input = "";

            string labelCss = "col-sm-4";
            string controlCss = "col-sm-8";

            var helper = new InputTagHelper(Generator);
            helper.ViewContext = ViewContext;
            helper.For = Value;

            helper.Init(context);
            helper.Process(context, output);

            using (var writer = new StringWriter())
            {
                output.TagName = "input";
                if (output.Attributes.ContainsName("Value"))
                {
                    var val = output.Attributes["Value"];
                    if (val.Value?.ToString().ToLower() == "true")
                        output.Attributes.Add("checked", "");
                }

                output.WriteTo(writer, HtmlEncoder.Default);
                input = writer.ToString().Replace("text", "checkbox");
            }

            output.Attributes.Clear();
            output.PostContent.Clear();
            output.Content.Clear();

            var label = Label ?? Value?.Metadata?.DisplayName ?? Value?.Name ?? "";
            var labelContent = $"<label for='{Value.Name}' class='{labelCss} col-form-label'>{label}</label>";

            var validateBuilder = Generator.GenerateValidationMessage(
                    ViewContext,
                    Value.ModelExplorer,
                    Value.Name,
                    message: null,
                    tag: null,
                    htmlAttributes: null);

            var divBuilder = new TagBuilder("div");
            divBuilder.AddCssClass(controlCss);
            divBuilder.AddCssClass("col-form-checkbox");
            divBuilder.InnerHtml.AppendHtml(input);
            divBuilder.InnerHtml.AppendHtml(validateBuilder);

            output.TagName = "div";
            output.Attributes.Add("class", "form-group row");

            output.Content.AppendHtml(labelContent);
            output.Content.AppendHtml(divBuilder);
        }
    }
}
