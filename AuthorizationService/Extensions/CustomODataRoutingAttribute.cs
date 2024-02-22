using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.OData.Edm;
using Microsoft.Playwright.Services.Authorization.Models;

namespace AuthorizationService.Extensions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CustomODataRoutingAttribute : Attribute, IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            var attributeRoute = action.Selectors
                    .Where(selector => selector.AttributeRouteModel != null).FirstOrDefault();

            if (attributeRoute == null)
            {
                return;
            }

            if (!attributeRoute.AttributeRouteModel.Template.StartsWith("accounts/{accountId}"))
            {
                return;
            }

            IEdmModel model = EdmModelProvider.GetEdmModel();
            ODataPathTemplate template = BuildTemplate(model, attributeRoute);

            if (template != null)
            {
                ODataRoutingMetadata newMetadata = new ODataRoutingMetadata("accounts/{accountId}", model, template)
                {
                    IsConventional = false
                };

                attributeRoute.EndpointMetadata.Add(newMetadata);
            }
        }

        private ODataPathTemplate BuildTemplate(IEdmModel model, SelectorModel selector)
        {
            IEdmEntitySet entitySet = model.FindDeclaredEntitySet("AccessToken");
            if (selector.AttributeRouteModel.Template == "accounts/{accountId}/access-tokens")
            {
                return new ODataPathTemplate(new CustomEntitySetSegment(entitySet));
            }

            if (selector.AttributeRouteModel.Template == "accounts/{accountId}/access-tokens/{accessTokenId}")
            {
                return new ODataPathTemplate(new CustomEntitySetSegment(entitySet), new KeySegmentTemplate(
                    new Dictionary<string, string>
                    {
                        { "Id", "{accessTokenId}" }
                    }, entitySet.EntityType(), entitySet));
            }

            return null;
        }
    }
}
