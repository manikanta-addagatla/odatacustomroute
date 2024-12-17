//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

//using Microsoft.AspNetCore.Mvc;
using AuthorizationService.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.Playwright.Services.Authorization.Common;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Playwright.Services.Authorization.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using ILogger = Serilog.ILogger;

#nullable enable

namespace Microsoft.Playwright.Services.Authorization.Controllers
{

    /// <summary>
    /// Access Token Controller
    /// </summary>
    //[ODataAttributeRouting]
    [Route("/")]
    [ApiVersion(AuthorizationServiceConstants.APIVersion1_0)]
    [ApiVersion(AuthorizationServiceConstants.APIVersion2_0)]
    public class AccessTokenController : ControllerBase
    {
        private readonly ILogger _logger;
        private List<AccessTokenResponse> accesstokens;

        /// <summary>
        /// Authorization controller constructor 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="authorizeService"></param>
        /// <param name="accessTokenMetadataDbService"></param>
        /// <param name="tokenHandlerService"></param>
        /// <param name="openTelemetryAuditLogger"></param>
        public AccessTokenController(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            accesstokens = new List<AccessTokenResponse>();
            accesstokens.Add(new AccessTokenResponse
            {
                CreatedAt = DateTime.Now,
                ExpiryAt = DateTime.Now.AddHours(1),
                Id = "1",
                JwtToken = "abc",
                Name = "test"
            });
            accesstokens.Add(new AccessTokenResponse
            {
                CreatedAt = DateTime.Now,
                ExpiryAt = DateTime.Now.AddHours(1),
                Id = "2",
                JwtToken = "def",
                Name = "test2"
            });
            accesstokens.Add(new AccessTokenResponse
            {
                CreatedAt = DateTime.Now,
                ExpiryAt = DateTime.Now.AddHours(1),
                Id = "3",
                JwtToken = "ghi",
                Name = "test3"
            });
            accesstokens.Add(new AccessTokenResponse
            {
                CreatedAt = DateTime.Now,
                ExpiryAt = DateTime.Now.AddHours(1),
                Id = "4",
                JwtToken = "jkl",
                Name = "test4"
            });
            accesstokens.Add(new AccessTokenResponse
            {
                CreatedAt = DateTime.Now,
                ExpiryAt = DateTime.Now.AddHours(1),
                Id = "5",
                JwtToken = "mno",
                Name = "test5"
            });
        }

        #region Actions

        /// <summary>
        /// Create AccessToken for given accountId
        /// </summary>
        [HttpPut("accounts/{accountId}/access-tokens/{accessTokenId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AccessTokenResponse))]
        [SwaggerOperation(OperationId = AuthorizationServiceConstants.PutAccessTokenOperationId, Summary = AuthorizationServiceConstants.CreateAccessTokenSwaggerSummary)]
        public async Task<ActionResult> CreateAccessTokenAsync([FromRoute] string accountId, [FromRoute] string accessTokenId, [FromBody] AccessTokenRequest accessTokenRequest)
        {
        //    _logger.Information($"CreateAccessTokenAsync Request entry for accountId: {accountId}, accessTokenId: {accessTokenId}.");
            AccessTokenResponse accessTokenResponse = new AccessTokenResponse();
            accesstokens.Add(accessTokenResponse);
            return Created("uri",accessTokenResponse);
        }

        /// <summary>
        /// Get AccessToken for given acccessKeyId
        /// </summary>
        [HttpGet("accounts/{accountId}/access-tokens/{accessTokenId}")]
        [ODataIgnored]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccessTokenResponse))]
        [SwaggerOperation(OperationId = AuthorizationServiceConstants.GetAccessTokenOperationId, Summary = AuthorizationServiceConstants.GetAccessTokenSwaggerSummary)]
        public async Task<ActionResult> GetAccessTokenAsync([FromRoute] string accountId, [FromRoute] string accessTokenId)
        {
        //    _logger.Information($"GetAccessTokenAsync Request entry for accountId: {accountId}, accessTokenId: {accessTokenId}.");
            return Ok(new AccessTokenResponse
            {
                JwtToken = $"GetAccessTokenAsync Request entry for accountId: {accountId}, accessTokenId: {accessTokenId}."
            });
        }

        [HttpGet("accounts/{accountId}/access-tokens")]
        [MapToApiVersion(AuthorizationServiceConstants.APIVersion1_0)]
        // User AAD Token
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AccessTokenResponse>))]
        [SwaggerOperation(OperationId = "GetAllAccessTokenV1")]
        public async Task<ActionResult> GetAllAccessTokenAsync([FromRoute] string accountId)
        {
            _logger.Information($"GetAllAccessTokenAsync Request entry for accountId: {accountId}.");
            return Ok(accesstokens);
        }

        /// <summary>
        /// Get all AccessToken for given accountId
        /// </summary>
        [EnableQuery(PageSize = 2)]
        [CustomODataRouting]
        [MapToApiVersion(AuthorizationServiceConstants.APIVersion2_0)]
        [HttpGet("accounts/{accountId}/access-tokens")]
        [SwaggerOperation(OperationId = "GetAllAccessTokenV2")]
        public async Task<ActionResult> GetAllAccessTokenV2Async([FromRoute] string accountId)
        {
    //      _logger.Information($"GetAllAccessTokenAsync Request entry for accountId: {accountId}.");
            return Ok(accesstokens.AsQueryable());
        }

        /// <summary>
        /// Delete AccessToken for given accountId
        /// </summary>
        [HttpDelete("accounts/{accountId}/access-tokens/{accessTokenId}")]
        [SwaggerOperation(OperationId = AuthorizationServiceConstants.DeleteAccessTokenOperationId, Summary = AuthorizationServiceConstants.DeleteAccessTokenSwaggerSummary)]
        public async Task<ActionResult> DeleteAccessTokenAsync([FromRoute] string accountId, [FromRoute] string accessTokenId)
        {
       //     _logger.Information($"DeleteAccessTokenAsync Request entry for accountId: {accountId}, accessTokenId: {accessTokenId}.");
            return NoContent();
        }

        /// <summary>
        /// Delete all AccessTokens for given accountId
        /// </summary>
        [HttpDelete("accounts/{accountId}/access-tokens")]
        [SwaggerOperation(OperationId = AuthorizationServiceConstants.DeleteAllAccessTokenOperationId, Summary = AuthorizationServiceConstants.DeleteAllAccessTokensSwaggerSummary)]
        public async Task<ActionResult> DeleteAllAccessTokensAsync([FromRoute] string accountId)
        {
      //      _logger.Information($"DeleteAllAccessTokensAsync Request entry for accountId: {accountId}.");
            return Accepted();
        }
        #endregion
    }
}
