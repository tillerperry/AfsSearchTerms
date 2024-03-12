using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Afs.SearchTerms.Web.Authentication
{
    public class PrivateAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IConfiguration _configuration;
        public PrivateAuthHandler
        (
            IConfiguration configuration,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOptionsMonitor<AuthenticationSchemeOptions> options


        ) : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            
            
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            await Task.Delay(0);

            bool passed = false;

            try
            {
                var auth = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

                if (!"privatekey".Equals(auth.Scheme, StringComparison.OrdinalIgnoreCase))
                {
                    return AuthenticateResult.NoResult();
                }

                var authKey = auth.Parameter;
                var privateAuthKey = _configuration["PrivateKeyConfig:Key"];
                
                passed = authKey ==privateAuthKey;

            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (!passed)
                return AuthenticateResult.Fail("Invalid auth parameter");

            var claims = new[]
            {
                //by convention, .NET Core authenticated user expects Name and NameIdentifier claims
                new Claim(ClaimTypes.Name, "Hubtel.LendScore"),
                new Claim(ClaimTypes.AuthenticationMethod,"PrivateKey"),
                new Claim(ClaimTypes.Authentication,"5758wed67uydf56ytrdgf")

            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            

            return AuthenticateResult.Success(ticket);
        }
        
       
    }
}