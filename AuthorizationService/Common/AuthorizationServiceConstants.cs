//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace Microsoft.Playwright.Services.Authorization.Common
{
    public class AuthorizationServiceConstants
    {
        // Route constants
        public const string AccountIdRoute = "accountId";

        // Swagger Action Summary
        public const string CreateAccessTokenSwaggerSummary = "Create an access token for the workspace";
        public const string DeleteAccessTokenSwaggerSummary = "Delete an access token for the workspace";
        public const string DeleteAllAccessTokensSwaggerSummary = "Delete all access token for the workspace";
        public const string GetAccessTokenSwaggerSummary = "Get an access token for the workspace";
        public const string GetAllAccessTokensSwaggerSummary = "Get all access tokens for the workspace";

        // Open Telemetry Constants
        public const string PutAccessTokenOperationId = "AccessToken_PUT";
        public const string DeleteAccessTokenOperationId = "AccessToken_DELETE";
        public const string GetAccessTokenOperationId = "AccessToken_GET";
        public const string GetAllAccessTokenOperationId = "AllAccessTokens_GET";
        public const string DeleteAllAccessTokenOperationId = "AllAccessTokens_DELETE";

        public const string APIVersion1_0 = "1.0";
        public const string APIVersion2023_10_01_preview = "2023-10-01-preview";

        public const string APISessionConnectRouteDeprecated = "api/authorize/connectSession";
        public const string APISessionConnectRoute = "sessions/connect";
        public const string APISessionSubscriptionsRoute = "sessions/subscriptions";
        public const string APISessionAccountsIdRoute = "accounts/{accountId}";
        public const string FreeTrialStatesEndpoint = "free-trial-states";
        public const string APISessionSubscriptionsFreeTrialStateRoute = APISessionSubscriptionsRoute + "/{subscriptionId}/" + FreeTrialStatesEndpoint;

        public const string APIAccessTokenRoute = "accounts/{accountId}/access-tokens";
        public const string APIAccessTokenIdRoute = "{accessTokenId}";

        public const string ServiceName = "AuthorizationService";
        public const string AuthServiceDbName = "AuthCosmosDB";
        public const int ActiveAccessTokensLimit = 10;

        /// <summary>
        /// AccessKeyInfo Container ID
        /// </summary>
        public const string AccessKeyInfoContainerId = "AccessKeyInfo";

        /// <summary>
        /// AccessTokenMetadata Container ID
        /// </summary>
        public const string AccessTokenMetadataContainerId = "AccessTokenMetadata";

        /// <summary>
        /// AccessKeyInfo Container Partition key
        /// </summary>
        public const string AccessKeyInfoPartitionKey = "id";

        /// <summary>
        /// AccessTokenMetadata Container Partition key
        /// </summary>
        public const string AccessTokenMetadataPartitionKey = "userId";

        public const string PrivateKeyStartIdentifier = "-----BEGIN PRIVATE KEY-----";
        public const string PrivateKeyEndIdentifier = "-----END PRIVATE KEY-----";
        public const string TokenSigningKeyName = "TokenSigningKey";
        public const int LocalCacheTTLInMin = 15; //default cache ttl in min.
        public const int SessionTokenValidityHours = 1;

        /// <summary>
        /// Query to get all AccessKeyInfo for given accountId
        /// </summary>
        public const string ListAccessKeyInfoByAccountId = "SELECT * FROM AccessKeyInfo ai where ai.accountId = @accountId ORDER BY ai._ts DESC";

        /// <summary>
        /// Query to get all Active AccessKeyInfo for given accountId
        /// </summary>
        public const string ListActiveAccessKeyInfoByAccountId = "SELECT * FROM AccessKeyInfo ai where ai.accountId = @accountId and ai.state = 0";

        /// <summary>
        /// Query to get all AccessTokenMetadata for given accountId
        /// Currently used for deleting all tokens on account deletion
        /// </summary>
        public const string ListAccessTokenMetadataByAccountId = "SELECT * FROM AccessTokenMetadata ai where ai.accountId = @accountId ORDER BY ai._ts DESC";

        /// <summary>
        /// Query to get all AccessKeyMetadata for given accountId created by given userId
        /// Currently used for listing all tokens for given user and account
        /// </summary>
        public const string ListAccessTokenMetadataByAccountAndUserId = "SELECT * FROM AccessTokenMetadata ai where ai.accountId = @accountId and ai.userId = @userId ORDER BY ai._ts DESC";

        /// <summary>
        /// Query to get count of Active AccessTokens created by given userId
        /// Currently used for getting ACTIVE token count for given user and account
        /// </summary>
        public const string ListActiveAccessTokenMetadataByUserId = "SELECT count(ai.id) as count FROM AccessTokenMetadata ai where ai.accountId = @accountId and ai.userId = @userId and ai.state = 'Active'";

        public const string CosmosExceptionMessage = "AuthorizationService_CosmosException";

        public enum AccessTokenState
        {
            [EnumMember]
            Active,

            [EnumMember]
            Expired
        }
    }

    public class AuthorizationServiceKeys
    {
        // Service App Settings
        public const string ServiceEndpoint = "ServiceEndpoint";
        public const string RPEndpoint = "RPEndpoint";
        public const string FesRedirectUrl = "FESRedirectUrl";
        public const string FesRedirectUrlScheme = "FESRedirectUrlScheme";
        public const string FesATMRedirectUrl = "FESATMRedirectUrl";
        public const string FesATMRedirectUrlScheme = "FESATMRedirectUrlScheme";
        public const string LocalCacheTTLInMinKey = "LocalCacheTTLInMinKey";

        public const string AuthPublicKeyCert = "AuthPublicKeyCert";
        public const string AuthPublicKey = "AuthPublicKey-{0}";
        public static string AuthAdminKey = "AuthAdminKey";

        //Local Cache Key Separator
        public const string AccountMetadataKey = "AccountMetadataKey";
        public const string AccessTokenMetadataKey = "AccessTokenMetadataKey";
        public const string AccessKeyInfoKey = "AccessKeyInfoKey";
        public const string SubscriptionMetadataKey = "SubscriptionMetadataKey";
        public const string LocalCacheKeySeparator = "_";
    }
}
