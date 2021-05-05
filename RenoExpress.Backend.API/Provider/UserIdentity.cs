using RenoExpress.Backend.MOD.MOD.Auxiliar;
using System.Linq;
using System.Security.Claims;

namespace RenoExpress.Backend.API
{
    public class UserIdentity
    {
        public SesionUsuario Usuario { get; } = new SesionUsuario();

        public UserIdentity(ClaimsIdentity user)
        {
            try
            {
                //string name = user.Name;
                string name = user.Claims.FirstOrDefault().Value;


                var lstUsuarioNombre = user.Claims.Where(c => c.Type == ClaimTypes.Name)
                                  .Select(c => c.Value);
                if (lstUsuarioNombre.Count() < 1)
                {
                    Usuario.Usuario_nombre = "ND";
                }
                else
                {
                    var usuarionombre = lstUsuarioNombre.First();
                    Usuario.Usuario_nombre = (usuarionombre == null ? "" : usuarionombre.ToString());
                }


                var lstip = user.Claims.Where(c => c.Type == ClaimTypes.System)
                   .Select(c => c.Value);
                if (lstip.Count() < 1)
                {
                    Usuario.Ip = "ND";
                }
                else
                {
                    var ip = lstip.First();
                    Usuario.Ip = (ip == null ? "" : ip.ToString());
                }



            }
            catch
            {
                throw;
            }
        }

    }
}