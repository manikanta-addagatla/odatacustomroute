//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.Playwright.Services.Authorization.Models
{
    //[DataContract]
    public class AccessTokenBase
    {
        //[DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "expiryAt")]
        public DateTime ExpiryAt { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
    }
}
