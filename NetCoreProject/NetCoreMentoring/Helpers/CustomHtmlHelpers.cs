using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace NetCoreMentoring.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlContent NorthwindImageLink(this IHtmlHelper helper, int imageId, string linkText)
        {
            return new HtmlString($"<a href='/images/{imageId}'>{linkText}</a>");
        }
    }
}
