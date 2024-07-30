using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LMS_SASS.Models;
using LMS_SASS.Databases;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;
using LMS_SASS.Services;
using LMS_SASS.Interfaces;

namespace LMS_SASS.Controllers;

public class UserController(DatabaseContext databaseContext) : BaseController(databaseContext) {

	[Route("/user/{Id:int}/image")]
	public IActionResult Image(int Id) {
		var user = DB.Users.Find(Id);

		if (user == null)
			return NotFound();

		return Content(SvgAvatar(user.Name), "image/svg+xml", Encoding.UTF8);
	}

	protected static string SvgAvatar(string name, int size = 200) {
        var color = new Dictionary<char, string> {
            {'A', "#5A876F"}, {'B', "#B2B7BB"}, {'C', "#6FA9AB"}, {'D', "#F5AF29"}, 
            {'E', "#0088B9"}, {'F', "#F18536"}, {'G', "#D93A37"}, {'H', "#B3BC50"}, 
            {'I', "#5B9BBD"}, {'J', "#F5878C"}, {'K', "#9B89B5"}, {'L', "#407887"}, 
            {'M', "#9B89B5"}, {'N', "#5A876F"}, {'O', "#D33F33"}, {'P', "#D33F33"}, 
            {'Q', "#F1B126"}, {'R', "#0087BF"}, {'S', "#F18536"}, {'T', "#0087BF"}, 
            {'U', "#B2B7BB"}, {'V', "#72ACAE"}, {'W', "#9B89B5"}, {'X', "#5A876F"}, 
            {'Y', "#EEB424"}, {'Z', "#407887"}
        };

        char letter = char.ToUpper(name[0]);

        if (!color.TryGetValue(letter, out string? letterColor))
            letterColor = "#846B32";

        StringBuilder svgBuilder = new();
        
        svgBuilder.AppendLine($"<svg width=\"{size}\" height=\"{size}\" xmlns=\"http://www.w3.org/2000/svg\">");
        svgBuilder.AppendLine($"    <rect fill=\"{letterColor}\" height=\"{size}\" width=\"{size}\" />");
        
        int fontSize = size / 2;
        string displayText = name.Length <= 2 ? name : letter.ToString();
        
        svgBuilder.AppendLine($"    <text xml:space=\"preserve\" text-anchor=\"middle\" dominant-baseline=\"central\"");
		svgBuilder.AppendLine($"          font-family=\"Segoe UI, Arial, sans-serif\" font-size=\"{fontSize}\"");
        svgBuilder.AppendLine($"          font-weight=\"bold\" y=\"47%\" x=\"50%\" fill=\"#FFF\">{displayText}</text>");
        svgBuilder.AppendLine("</svg>");

        return svgBuilder.ToString();
	}
}