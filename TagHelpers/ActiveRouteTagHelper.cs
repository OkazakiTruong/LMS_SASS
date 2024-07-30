using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LMS_SASS.TagHelpers;

[HtmlTargetElement(Attributes = "asp-active-route")]
public class ActiveRouteTagHelper : TagHelper {
	[HtmlAttributeNotBound]
	[ViewContext]
	public required ViewContext ViewContext { get; set; }

	[HtmlAttributeName("asp-controller")]
	public required string Controller { get; set; }

	[HtmlAttributeName("asp-action")]
	public required string Action { get; set; }

	[HtmlAttributeNotBound]
	public string Class { get; set; } = "active";

	public override void Process(TagHelperContext context, TagHelperOutput output) {
		base.Process(context, output);

        var currentController = (string?) ViewContext.RouteData.Values["controller"];
		var currentAction = (string?) ViewContext.RouteData.Values["action"];

		if (string.Equals(Controller, currentController, StringComparison.OrdinalIgnoreCase) &&
			string.Equals(Action, currentAction, StringComparison.OrdinalIgnoreCase)) {

			var existingClasses = output.Attributes.ContainsName("class")
				? output.Attributes["class"].Value.ToString()
				: "";
			
			output.Attributes.SetAttribute("class", $"{existingClasses} {Class}".Trim());
		}
	}
}
