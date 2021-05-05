using RenoExpress.Backend.DAL.DAL;
using RenoExpress.Backend.MOD.MOD;
using RenoExpress.Backend.MOD.MOD.Auxiliar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenoExpress.Backend.BLL.BLL
{
    public class LoginBLL
    {
        public Respuesta SignIn(Usuario usuario, ref Error error)
        {

            LoginDAL db = new LoginDAL();
            bool isAutenticated = false;
            var userData = new Usuario();
            string contrasenia = usuario.Contrasenia;
            Respuesta response = new Respuesta();

            try
            {
                userData = db.SignIn(usuario, ref error);

                if (userData == null)
                {
                    response.Response = false;
                    response.Message = "Usuario no encontrado";
                    return response;
                }

                if (!string.IsNullOrEmpty(contrasenia))
                {
                    if (Encriptador.Desencripta(userData.Contrasenia) == contrasenia)
                    {
                        response.Response = true;
                        response.Message = "OK";
                    }
                    else
                    {
                        response.Response = false;
                        response.Message = "Contraseña no coincide";
                    }
                }
                else
                {
                    response.Response = false;
                    response.Message = "Contraseña no coincide";
                }

            }
            catch (Exception ex)
            {
                error = new Error()
                {
                    Descripcion = "Error al tratar obtener los datos del usuario, " + ex.Message,
                    Resultado = "0"
                };
            }

            return response;

        }

    }
}
