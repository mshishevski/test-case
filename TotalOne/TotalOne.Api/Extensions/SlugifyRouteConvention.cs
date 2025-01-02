using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Text.RegularExpressions;

namespace TotalOne.Api.Extensions;

public class SlugifyRouteConvention : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            controller.ControllerName = ToSlug(controller.ControllerName);

            foreach (var action in controller.Actions)
            {
                action.ActionName = ToSlug(action.ActionName);
            }
        }
    }

    private string ToSlug(string value)
    {
        return Regex.Replace(value, "([a-z])([A-Z])", "$1-$2").ToLowerInvariant();
    }
}
