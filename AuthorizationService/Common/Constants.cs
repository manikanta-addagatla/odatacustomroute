//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace Microsoft.Playwright.Common.Utilities
{
    public class Constants
    {
        // Configuration/Env vars
        public const string ServiceSettings = "ServiceSettings";
        public const string MdmHost = "MdmHost";
        public const string PublicKeyVaultUrl = "PublicKeyVaultUrl";
        public const string AdminKeyVaultUrl = "AdminKeyVaultUrl";
        public const string CommonKeyVaultUrl = "CommonKeyVaultUrl";
        public const string KeyVaultUrl = "KeyVaultUrl";
        public const string EastusKeyVaultUrl = "EastusKeyVaultUrl";
        public const string Westus3KeyVaultUrl = "Westus3KeyVaultUrl";
        public const string WesteuropeKeyVaultUrl = "WesteuropeKeyVaultUrl";
        public const string EastasiaKeyVaultUrl = "EastasiaKeyVaultUrl";
        public const string GenevaMetricSettings = "GenevaMetricSettings";
        public const string CommonPublicKVIdentity = "CommonPublicKVIdentity";
        public const string CosmosDBUrl = "CosmosDBUrl";
        public const string Azure = "Azure";
        public const string UserRPRegion = "UserRPRegion";
        public const string FESQueueSizeMultiplier = "FESQueueSizeMultiplier";
        public const string MPTS2SUAMIClientId = "MPTS2SUAMIClientId";
        public const string MPTS2SSPNClientId = "Auth:MPTS2SSPNClientId";
        public const string MinWorkerThreads = "MinWorkerThreads";
        public const string MinCompletionPortThreads = "MinCompletionPortThreads";

        //ARM + AAD constants
        public const string PlaywrightAccountResourceType = "Microsoft.AzurePlaywrightService/accounts";
        public const string PlaywrightServiceProviderNamespace = "Microsoft.AzurePlaywrightService";

        //AAD and MISE Config keys
        public const string AzureAd = "AzureAd";
        public const string Audience = "Audience";
        public const string MiseConfig = "Mise";
        public const string CacheSizeLimit = "CacheSizeLimit";
        public const string AzureAuthorizationDataProvider = "AzureAuthorizationDataProvider";
        public const string BaselineMPTPolicyName = "baseline-mpt-internal";
        public const string AuthorizePolicy_MPT = "MPT";
        public const string AuthorizePolicy_MPT_Session = "Session";
        public const string AuthorizePolicy_MPT_Admin = "Admin";
        public const string AuthorizePolicy_MPT_AdminOrSession = "AdminOrSession";

        public const string DefaultDimensionRegion = "REGION";
        public const string DefaultDimensionInstance = "INSTANCE";
        public const string DefaultDimensionEnvironment = "ENVIRONMENT";
        public const string Region = "Region";
        public const string Instance = "Instance";
        public const string Environment = "Environment";
        public const string CorrelationId = "CorrelationId";

        //Headers
        public const string CorrelationIdHeader = "x-correlation-id";
        public const string ClientRequestId = "x-ms-client-request-id";
        public const string ARMCorrelationIdHeader = "x-ms-correlation-request-id";
        public const string AccessKeyHeader = "x-mpt-access-key";

        // Playwright OSS headers
        public const string XPlaywrightDebugLog = "x-playwright-debug-log";
        public const string XPlaywrightAttachment = "x-playwright-attachment";
        public const string PlaywrightAttachmentSessionIdName = "SessionId";

        // API Param
        public const string ApiVersionApiParam = "api-version";
        public const string AccessKey_Deprecated = "accessKey";

        // Keys
        public const string AccountIdKey = "accountId";
        public const string ResourceIdKey = "resourceId";
        public const string SubscriptionIdKey = "subscriptionId";

        // Token related
        public const string BearerPrefix = "Bearer ";
        public const string AccountIdSeparator = "_";

        // Authorization Actions
        public const string AzurePlaywrightService_Accounts_Read = "Microsoft.AzurePlaywrightService/accounts/read";
        public const string AzurePlaywrightService_Accounts_Write = "Microsoft.AzurePlaywrightService/accounts/write";
        public const string AzurePlaywrightService_Accounts_Delete = "Microsoft.AzurePlaywrightService/accounts/delete";
        public const string Authorization_RoleAssignment_Read = "Microsoft.Authorization/roleAssignments/read";
        public const string Authorization_RoleAssignment_Write = "Microsoft.Authorization/roleAssignments/write";
        public const string AzurePlaywrightService_Quotas_Read = "Microsoft.AzurePlaywrightService/locations/quotas/read";
        public const string Resources_Subscriptions_Read = "Microsoft.Resources/subscriptions/read";

        // Feature flag keys
        public const string FeatureFlag_ActivityLog_Key = "ActivityLog";
        public const string FeatureFlag_RunUpdateDelete_Key = "RunUpdateDeleteEnabled";
        public const int AllBrowserQuotaLimit = 20; // Default quota limit for all browser quota if not passed from auth service in session token
        public const int MacBrowserQuotaLimit = 0; // Default quota limit for macOS browser if not passed in session token.
        public const int RateLimitThreshold = 400; //Default value of rate limit threshold in 5 min windows.
        public const int DefaultFESQueueSizeMultiplier = 1;

        // Message strings
        public const string ServerErrorMessage = "A server error has occurred.";
        public const string FailedString = "Failed";

        // Request Path Strings
        public const string APIVersion1_0ConnectSessionPath = "/sessions/connect";
        public const string DeprecatedConnectSessionPath = "/api/authorize/connectSession";

        //Free trial Keys
        public const string AccountKeyForPrivatePreviewCustomer = "NOT_APPLICABLE";

        public const string LocalhostString = "localhost";

        public enum EnablementStatus
        {
            [EnumMember]
            Disabled,

            [EnumMember]
            Enabled
        }
        public enum ResourceState
        {
            [EnumMember]
            Active,

            [EnumMember]
            Inactive
        }
        public enum SubscriptionState
        {
            [EnumMember]
            Registered,

            [EnumMember]
            Warned,

            [EnumMember]
            Suspended,

            [EnumMember]
            Deleted,

            [EnumMember]
            Unregistered
        }
    }
}
