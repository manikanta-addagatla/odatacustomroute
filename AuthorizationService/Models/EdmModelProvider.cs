//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Newtonsoft.Json;
using System;

namespace Microsoft.Playwright.Services.Authorization.Models
{
    public static class EdmModelProvider
    {
        private static IEdmModel _EdmModel;

        public static IEdmModel GetEdmModel()
        {
            if (_EdmModel == null)
            {
                var builder = new ODataConventionModelBuilder();
                builder.EnableLowerCamelCase();
                builder.EntitySet<AccessTokenResponse>("AccessToken");
                _EdmModel = builder.GetEdmModel();
            }

            return _EdmModel;
        }
    }
}
