using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenoExpress.Backend.MOD.MOD
{
    public class Usuario
    {
        public string usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public Rol Rol { get; set; }
        public bool Estado { get; set; }
        
    }
}
