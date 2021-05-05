using RenoExpress.Backend.BLL.BLL;
using RenoExpress.Backend.MOD.MOD;
using RenoExpress.Backend.MOD.MOD.Auxiliar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace RenoExpress.Backend.API.Controllers
{
    public class ProductoController : ApiController
    {

        [HttpGet]
        [CustomErrorFilter]
        [Authorize]
        public IHttpActionResult Get(int codigo = 0)
        {

            Error error = null;
            ProductoBLL bl = new ProductoBLL();
            List<Producto> result = new List<Producto>();
            try
            {
                result = bl.GetProducto(codigo, ref error);
            }
            catch (Exception ex)
            {
                error = new Error
                {
                    Resultado = "0",
                    Descripcion = "Ocurrió un problema al intentar recuperar los datos, " + ex.Message
                };
            }

            if (error != null)
            {
                return BadRequest(error.Descripcion);
            }

            return Ok(result);

        }


        [HttpPost]
        [CustomErrorFilter]
        [Authorize]
        public IHttpActionResult Post(Producto producto)
        {

            Error error = null;
            ProductoBLL bl = new ProductoBLL();
            Respuesta result = new Respuesta();

            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            UserIdentity userProvider = new UserIdentity(identity);

            string usuario = userProvider.Usuario.Usuario_nombre.ToString();

            try
            {
                result = bl.PostProducto(producto, usuario, ref error);
            }
            catch (Exception ex)
            {
                error = new Error
                {
                    Resultado = "0",
                    Descripcion = "Ocurrió un problema al intentar procesar los datos, " + ex.Message
                };
            }

            if (error != null)
            {
                return BadRequest(error.Descripcion);
            }

            return Ok(result);

        }

        [HttpPut]
        [CustomErrorFilter]
        [Authorize]
        public IHttpActionResult Put(int codigo, int cantidad, int opcion)
        {

            Error error = null;
            ProductoBLL bl = new ProductoBLL();
            Respuesta result = new Respuesta();

            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            UserIdentity userProvider = new UserIdentity(identity);

            string usuario = userProvider.Usuario.Usuario_nombre.ToString();

            try
            {
                result = bl.PutProducto(codigo, cantidad, opcion, usuario, ref error);
            }
            catch (Exception ex)
            {
                error = new Error
                {
                    Resultado = "0",
                    Descripcion = "Ocurrió un problema al intentar procesar los datos, " + ex.Message
                };
            }

            if (error != null)
            {
                return BadRequest(error.Descripcion);
            }

            return Ok(result);

        }

    }
}
