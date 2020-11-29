using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CompanyInventory.Common.Consts;
using CompanyInventory.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CompanyInventory.WebApi.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private static readonly Dictionary<string, string> _users = new Dictionary<string, string>
        {
            {"user", "user"}
        };

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = Context.GetEndpoint();

            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey(Headers.authorizationHeaderName))
                return AuthenticateResult.Fail("Missing Authorization Header");

            UserClaim user = null;
            try
            {
                var credentials = DecodeAuthHeader();

                user = GetUser(credentials[0], credentials[1]);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (user == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            var ticket = CreateAuthenticationTicket(user.UserName, user.Password);

            return AuthenticateResult.Success(ticket);
        }

        private string[] DecodeAuthHeader()
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[Headers.authorizationHeaderName]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);

            return Encoding.UTF8.GetString(credentialBytes).Split(new[] {':'}, 2);
        }

        private UserClaim GetUser(string userName, string password)
        {
            return _users.Where(w => w.Key.ToLower() == userName.ToLower()
                                     && w.Value.ToLower() == password.ToLower())
                .Select(s => new UserClaim
                {
                    UserName = s.Key,
                    Password = s.Value
                }).FirstOrDefault();
        }

        private AuthenticationTicket CreateAuthenticationTicket(string userName, string password)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim("Password", password),
            };

            var identity = new ClaimsIdentity(claims, Schemes.basicAuth);
            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationTicket(principal, Schemes.basicAuth);
        }
    }
}