using Afs.SearchTerms.Web.Authentication;
using Afs.SearchTerms.Web.Options;
using Microsoft.AspNetCore.Authentication;

namespace Afs.SearchTerms.Web.Externsions;

public static class Extension
{
    
    public static IServiceCollection AddAfsPrivateKeyAuth(this IServiceCollection services)
    {
        services.AddAuthentication(CommonConstants.AuthScheme.PrivateKey)
            .AddScheme<AuthenticationSchemeOptions, PrivateAuthHandler>(CommonConstants.AuthScheme.PrivateKey, null);

        return services;
    }
}