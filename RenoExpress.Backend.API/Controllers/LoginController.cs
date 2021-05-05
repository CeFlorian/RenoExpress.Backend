using RenoExpress.Backend.BLL.BLL;
using RenoExpress.Backend.MOD.MOD;
using RenoExpress.Backend.MOD.MOD.Auxiliar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RenoExpress.Backend.API.Controllers
{
    public class LoginController : ApiController
    {

        [HttpPost]
        [CustomErrorFilter]
        public IHttpActionResult Post(Usuario usuario)
        {

            Error error = null;
            LoginBLL userlogic = new LoginBLL();
            Respuesta result = new Respuesta();
            try
            {
                result = userlogic.SignIn(usuario, ref error);
                
            }
            catch (Exception ex)
            {
                error = new Error
                {
                    Resultado = "0",
                    Descripcion = "Ocurrió un problema al tratar de iniciar sesion, " + ex.Message
                };
            }

            if (error != null)
            {
                return BadRequest(error.Descripcion);
            }

            if (result.Response)
            {
                return Ok(TokenGenerator.GenerateTokenJwt(usuario.usuario));
            }
            else
            {
                return Unauthorized();
            }

        }


    }
}
