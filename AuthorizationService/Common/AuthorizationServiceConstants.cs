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
        public const string ActionRouteKey = "action";

        // APIs action names
        public const string GetAllAccessTokensV2Action = "GetAllAccessTokenV2";

        // ODATA Constants
        public const string AccessTokenControllerResourceName = "access-tokens";
        public const string AccessTokenControllerName = "AccessToken";
        public const string ODataPrefix = "accounts/{accountId}";
        public const string ODataResponseContextKey = "@odata.context";
        public const string ODataResponseDefaultNextLinkKey = "@odata.nextLink";
        public const string ODataResponseNextLinkKey = "nextLink";

        // Swagger Action Summary
        public const string CreateAccessTokenSwaggerSummary = "Create an access token for the workspace.";
        public const string DeleteAccessTokenSwaggerSummary = "Delete an access token for the workspace.";
        public const string DeleteAllAccessTokensSwaggerSummary = "Delete all access token for the workspace.";
        public const string GetAccessTokenSwaggerSummary = "Get an access token for the workspace.";
        public const string GetAllAccessTokensSwaggerSummary = "Get all access tokens for the workspace.";
        public const string ValidateAccessTokenSwaggerSummary = "Validate access token for the workspace.";
        public const string GetAccountSwaggerSummary = "Get details of a workspace.";
        public const string GetAccountBrowserSwaggerSummary = "Get browsers for the workspace.";

        // Open Telemetry Constants
        public const string PutAccessTokenOperationId = "AccessToken_PUT";
        public const string DeleteAccessTokenOperationId = "AccessToken_DELETE";
        public const string GetAccessTokenOperationId = "AccessToken_GET";
        public const string GetAllAccessTokenOperationId = "AllAccessTokens_GET";
        public const string DeleteAllAccessTokenOperationId = "AllAccessTokens_DELETE";
        public const string ValidateAccessTokenOperationId = "AccessTokenValidate_POST";
        public const string GetAccountOperationId = "Account_GET";
        public const string GetAccountBrowserOperationId = "Account_Browser_GET";

        public const string APIVersion1_0 = "1.0";
        public const string APIVersion2023_10_01_preview = "2023-10-01-preview";
        public const string APIVersion2024_06_01_preview = "2024-06-01-preview";
        public const string APIVersion2_0 = "2.0";

        public const string APISessionAccountsIdRoute = "accounts/{accountId}";
        public const string APISessionAccountBrowsersRoute = $"{APISessionAccountsIdRoute}/browsers";
        public const string APISessionConnectRouteDeprecated = "api/authorize/connectSession";
        public const string APISessionSubscriptionsRoute = "sessions/subscriptions";
        public const string APISessionSubscriptionsIdRoute = APISessionSubscriptionsRoute + "/{subscriptionId}";

        public const string APIAccessTokenRoute = "access-tokens";
        public const string APIAccessTokenIdRoute = "{accessTokenId}";
        public const string ValidateTokenRoute = "validate";
        
        public const string ServiceName = "AuthorizationService";
        public const string AuthServiceDbName = "AuthCosmosDB";
        public const int ActiveAccessTokensLimit = 10;
        public const int SemaphoreCacheKeyExpiryTimeInMinutes = 15;
        public const int AzureCacheKeyTTLInMin = 15;

        public const string BrowserRedisConnectionString = "BrowserRedisConnectionString";

        /// <summary>
        /// AccessTokenMetadata Container ID
        /// </summary>
        public const string AccessTokenMetadataContainerId = "AccessTokenMetadata";

        /// <summary>
        /// AccessTokenMetadata Container Partition key
        /// </summary>
        public const string AccessTokenMetadataPartitionKey = "userId";

        public const string PrivateKeyStartIdentifier = "-----BEGIN PRIVATE KEY-----";
        public const string PrivateKeyEndIdentifier = "-----END PRIVATE KEY-----";
        public const string TokenSigningKeyName = "TokenSigningKey";
        public const int LocalCacheTTLInMin = 15; //default cache ttl in min.
        public const int SessionTokenValidityHours = 1;
        

        // geneva metric names 
        public const string RunInfoCacheMetric = "RunInfoCache_Metric";
        public const string AccountMetadataCacheMetric = "AccountMetadataCache_Metric";
        public const string TestRunInfoLatencyForReportingMetric = "TestRunInfoLatencyForReporting_Metric";
        public const string GetAccountMetadataFromRPMetric = "GetAccountMetadataFromRP_Metric";
        public const string ReportingPutRunInfoMetric = "ReportingPutRunInfo_Metric";

        // geneva metrics dimensions name
        public const string CacheEventDimensionName = "CacheEvent";
        public const string RunIdCreatedInReporting = "RunIdCreatedInReporting";
        public const string RunIdKey = "RunId";

        /// <summary>
        /// Query to get all AccessTokenMetadata for given accountId
        /// Currently used for deleting all tokens on account deletion
        /// </summary>
        public const string ListAccessTokenMetadataByAccountId = "SELECT * FROM AccessTokenMetadata ai where ai.accountId = @accountId ORDER BY ai._ts DESC";

        /// <summary>
        /// Query to get all AccessTokenMetadata for given accountId created by given userId
        /// Currently used for listing all tokens for given user and account
        /// </summary>
        public const string ListAccessTokenMetadataByAccountAndUserId = "SELECT * FROM AccessTokenMetadata ai where ai.accountId = @accountId and ai.userId = @userId ORDER BY ai._ts DESC";

        /// <summary>
        /// Query to get count of Active AccessTokens created by given userId
        /// Currently used for getting ACTIVE token count for given user and account
        /// </summary>
        public const string ListActiveAccessTokenMetadataByUserId = "SELECT count(ai.id) as count FROM AccessTokenMetadata ai where ai.accountId = @accountId and ai.userId = @userId and ai.state = 'Active'";

        public const string CosmosExceptionMessage = "AuthorizationService_CosmosException";


        /// <summary>
        /// Lua script to check if run id has been created in reporting service or not. This script do following things:<br/>
        /// 1) Check in response set if we have any requests which has succeeded (status 200). If yes then cease further<br/>
        /// attempt to create runId at reporting side<br/>
        /// 2) If we have exhausted K (threshold) requests without any 200 reponse code, then again cease reporting service call<br/>
        /// Response returned by script: Json string containing a json object with following fields:<br/>
        /// { CeaseReportingServiceCall: true/false, RunIdCreatedInReporting: true/false }<br/>
        /// Since lua script does not have json string converter, so we have to write our own method to convert json object to json string.
        /// </summary>
        public const string GetReportingCallStatusScript = @"
            local ceaseReportingServiceCall = false
            local runIdCreatedInReporting = false
            local searchStr = '200'

            -- Function to convert Lua table to JSON string
            local function toJSON(obj)
                local jsonString = '{'
                local isFirst = true

                for key, value in pairs(obj) do
                    if not isFirst then
                        jsonString = jsonString .. ', '
                    else
                        isFirst = false
                    end

                    -- Quote keys and values appropriately
                    jsonString = jsonString .. '\""' .. key .. '\"":' .. (type(value) == 'boolean' and tostring(value) or '\""' .. tostring(value) .. '\""')
                end

                jsonString = jsonString..'}'

                return jsonString
            end

            -- Check if @responseSetKey exists
            if tonumber(redis.call('exists', @responseSetKey)) == 1 then
                -- Check if searchStr is not a member of @responseSetKey
                if tonumber(redis.call('sismember', @responseSetKey, searchStr)) == 0 then
                    -- Check if logListKey length is greater than or equal to @requestThreshold
                    if tonumber(redis.call('exists', @logListKey)) == 1 and
                        tonumber(redis.call('LLEN', @logListKey)) >= tonumber(@requestThreshold) then
                            ceaseReportingServiceCall = true
                    end
                else
                    -- If searchStr is a member of @responseSetKey
                    ceaseReportingServiceCall = true
                    runIdCreatedInReporting = true
                end
            end

            -- Construct Lua table(response object)
            local responseObject = {
                CeaseReportingServiceCall = ceaseReportingServiceCall,
                RunIdCreatedInReporting = runIdCreatedInReporting
            }

            -- Convert Lua table to JSON string
            local responseString = toJSON(responseObject)

            return responseString
        ";

        /// <summary>
        /// Lua script to update status of runId creation in reporting service.<br/>
        /// Whatever may be status of a give request, add it in response set.<br/>
        /// Keep latest K (request threshold) entries in the list.<br/>
        /// Response from Script: RunIdCreatedInReporting - containing status value true/false. Value will be<br/> true if we have any requests with status 200
        /// Otherwise return the value as false.
        /// </summary>
        public const string UpdateReportingCallStatusScript = @"
            redis.call('sadd', @responseSetKey, @requestStatusCode)
            if tonumber(redis.call('exists', @logListKey)) == 1 and 
                tonumber(redis.call('LLEN', @logListKey)) >= tonumber(@requestThreshold) then
	                redis.call('rpop', @logListKey)
			end
            redis.call('lpush', @logListKey, @logString)
            redis.call('expire', @responseSetKey, @keyExpiryTimeInSec)
            redis.call('expire', @logListKey, @keyExpiryTimeInSec)

            if tonumber(redis.call('sismember', @responseSetKey, '200')) == 1 then
                return 'RunIdCreatedInReporting:true'
            else
                return 'RunIdCreatedInReporting:false'
            end
        ";

        public enum AccessTokenState
        {
            [EnumMember]
            Active,

            [EnumMember]
            Expired
        }

        public enum CacheEvents
        {
            FirstShotCacheHit = 1,
            SecondShotCacheHit = 2,
            CacheMiss = 3
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
        public const string AzureCacheKeyTTLInMin = "AzureCacheKeyTTLInMin";
        public const string ReportingAzureCacheKeyTTLInMin = "ReportingAzureCacheKeyTTLInMin";
        public const string ReportingSemaphoreCountKey = "ReportingSemaphoreCount";
        public const string ReportingRunIdRequestsThreshold = "ReportingRunIdRequestsThreshold";

        public const string AuthPublicKeyCert = "AuthPublicKeyCert";
        public const string AuthPublicKey = "AuthPublicKey-{0}";

        //Local Cache Key Separator
        public const string AccountMetadataKey = "AccountMetadataKey";
        public const string AccessTokenMetadataKey = "AccessTokenMetadataKey";
        public const string SubscriptionMetadataKey = "SubscriptionMetadataKey";
        public const string CacheKeySeparator = "_";
    }
}
