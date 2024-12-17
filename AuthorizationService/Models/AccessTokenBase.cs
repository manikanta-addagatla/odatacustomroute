//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.Playwright.Services.Authorization.Models
{
    [DataContract]
    public class AccessTokenBase
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [DataMember(Name = "createdAt")]
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "expiryAt")]
        [JsonProperty(PropertyName = "expiryAt")]
        public DateTime ExpiryAt { get; set; }

        [DataMember(Name = "state")]
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
    }
}
