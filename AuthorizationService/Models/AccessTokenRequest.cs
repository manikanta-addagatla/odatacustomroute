//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace Microsoft.Playwright.Services.Authorization.Models
{
    public class AccessTokenRequest
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "expiryAt")]
        public DateTime ExpiryAt { get; set; }

        public string validate()
        {
            Regex rg = new(@"^[a-zA-Z]{1}[a-zA-Z0-9]{2,63}$");

            if (!rg.IsMatch(Name))
            {
                return "Access Token Name should be alpha-numeric, starting with alphabet with length from 3 upto 64.";
            }

            if (ExpiryAt > DateTime.UtcNow.AddYears(1))
            {
                return "Expiry should not exceed 1 year from now.";
            }
            return string.Empty;
        }
    }
}
