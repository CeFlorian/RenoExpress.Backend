using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenoExpress.Backend.API
{
    public class Token
    {
        [JsonProperty("access_token", Required = Required.Always)]
        public string AccessToken { get; set; }

        [JsonProperty("token_type", Required = Required.Always)]
        public string TokenType { get; set; }

        [JsonProperty("expires_in", Required = Required.Always)]
        public int ExpiresIn { get; set; }

    }
}