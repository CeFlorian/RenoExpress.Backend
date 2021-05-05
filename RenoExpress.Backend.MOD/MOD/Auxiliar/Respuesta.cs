using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenoExpress.Backend.MOD.MOD.Auxiliar
{
    public class Respuesta
    {
        public string Message { get; set; }
        public bool Response { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }


        public Respuesta()
        {
            this.Message = string.Empty;
            this.Response = false;
            this.Type = string.Empty;
            this.Id = 0;
            this.Title = string.Empty;
        }
    }
}
