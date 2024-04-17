using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PeopleApp.Api.Attributes;

[AttributeUsage(validOn: AttributeTargets.Class)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    private const string? APIKEYNAME = "ApiKey";

    private ContentResult GetContentResult(int statusCode, string content)
    {
        var result = new ContentResult();
        result.StatusCode = statusCode;
        result.Content = content;
        return result;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        try
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Result = GetContentResult(401, "Api Key was not provided");
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            if (appSettings == null)
            {
                context.Result = GetContentResult(401, "Appsettings not found");
                return;
            }

            var apiKey = appSettings.GetValue<string>(APIKEYNAME);
            if (apiKey == null)
            {
                context.Result = GetContentResult(401, "Appsettings - ApiKey - Not Found");
                return;
            }
        }
        catch (Exception e)
        {
            context.Result = GetContentResult(401, e.Message);
            return;
        }
    }
}