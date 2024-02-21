//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Microsoft.Playwright.Services.Authorization.Models
{
    public class AccessTokenResponse : AccessTokenBase
    {
        [JsonProperty(PropertyName = "jwtToken")]
        public string JwtToken { get; set; }
    }
}
