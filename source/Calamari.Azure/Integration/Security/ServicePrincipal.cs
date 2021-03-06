﻿using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Calamari.Azure.Integration.Security
{
    public class ServicePrincipal
    {
        public static string GetAuthorizationToken(string tenantId, string applicationId, string password, string managementEndPoint, string activeDirectoryEndPoint)
        {
            var authContext = GetContextUri(activeDirectoryEndPoint, tenantId);
            Log.Verbose($"Authentication Context: {authContext}");
            var context = new AuthenticationContext(authContext);
            var result = context.AcquireToken(managementEndPoint, new ClientCredential(applicationId, password));
            return result.AccessToken;
        }

        static string GetContextUri(string activeDirectoryEndPoint, string tenantId)
        {
            if (!activeDirectoryEndPoint.EndsWith("/"))
            {
                return $"{activeDirectoryEndPoint}/{tenantId}";
            }
            return $"{activeDirectoryEndPoint}{tenantId}";
        }
    }
}