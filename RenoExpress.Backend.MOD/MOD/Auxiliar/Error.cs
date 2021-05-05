using System;
using System.Collections.Generic;

namespace RenoExpress.Backend.MOD.MOD.Auxiliar
{
    public class Error
    {
        public string Resultado { get; set; }

        public string Descripcion { get; set; }

        public List<ClientException> ClientExceptions { get; set; }
    }

    public sealed class ClientException
    {
        public ClientException(string uid, string message) {
            UID = uid;
            Message = message;
            DateTime = DateTime.Now;
        }

        public string UID { get; }
        public string Message { get; }
        public DateTime DateTime { get; }
    }
}
