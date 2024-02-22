using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using Microsoft.Playwright.Services.Authorization.Models;

namespace AuthorizationService.Extensions
{
    public class CustomEntitySetSegment : EntitySetSegmentTemplate
    {
        public CustomEntitySetSegment(IEdmEntitySet entitySet) : base(entitySet)
        {
        }

        public override IEnumerable<string> GetTemplates(ODataRouteOptions options)
        {
            yield return "access-tokens";
        }
    }
}
