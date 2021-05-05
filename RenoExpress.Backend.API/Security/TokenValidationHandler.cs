using Microsoft.IdentityModel.Tokens;
using RenoExpress.Backend.MOD.MOD.Auxiliar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Windows;

namespace RenoExpress.Backend.API
{

    internal class TokenValidationHandler : DelegatingHandler
    {

        public HttpConfiguration Config { get; set; }
        public TokenValidationHandler(HttpConfiguration config)
        {
            Config = config;
        }

        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;
            Error error = null;

            // determine whether a jwt exists or not
            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return await base.SendAsync(request, cancellationToken);
            }

            try
            {

                var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));

                SecurityToken securityToken;
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audienceToken,
                    ValidateIssuer = true,         
                    ValidIssuer = issuerToken,      
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey,

                };

                // Extract and assign Current Principal and user
                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                HttpContext.Current.User = tokenHandler.ValidateToken(token, validationParameters, out securityToken);


                //Agregamos el token a los claims del identity
                ((ClaimsIdentity)HttpContext.Current.User.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.Hash, token)
                });

                return await base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException stvex)
            {
                error = new Error()
                {
                    Descripcion = "Token no válido, " + stvex.Message,
                    Resultado = "0",
                };

                statusCode = HttpStatusCode.Unauthorized;

            }
            catch (Exception ex)
            {
                error = new Error()
                {
                    Descripcion = "Ha ocurrido un problema al validar autorización, " + ex.Message,
                    Resultado = "0",
                };

                statusCode = HttpStatusCode.InternalServerError;

            }

            return await Task<HttpResponseMessage>.Factory.StartNew(() => request.CreateResponse(statusCode, error, Config.Formatters.JsonFormatter, "application/json"));
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}