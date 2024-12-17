//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.Playwright.Services.Authorization.Models
{
    [DataContract]
    public class AccessTokenResponse : AccessTokenBase
    {
        [DataMember(Name = "jwtToken")]
        [JsonProperty(PropertyName = "jwtToken")]
        public string JwtToken { get; set; }

    }
}
