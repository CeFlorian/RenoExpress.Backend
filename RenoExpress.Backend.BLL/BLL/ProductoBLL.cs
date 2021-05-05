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
    public class ProductoBLL
    {
        public List<Producto> GetProducto(int codigo, ref Error error)
        {

            ProductoDAL db = new ProductoDAL();
            List<Producto> list = new List<Producto>();

            try
            {
                list = db.GetProducto(codigo, ref error);
            }
            catch (Exception ex)
            {
                error = new Error()
                {
                    Descripcion = "Error al tratar obtener los datos, " + ex.Message,
                    Resultado = "0"
                };
            }

            return list;

        }


        public Respuesta PostProducto(Producto producto, string usuario, ref Error error)
        {

            ProductoDAL db = new ProductoDAL();
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = db.PostProducto(producto, usuario, ref error);
            }
            catch (Exception ex)
            {
                error = new Error()
                {
                    Descripcion = "Error al tratar de crear el registro, " + ex.Message,
                    Resultado = "0"
                };
            }

            return respuesta;

        }


        public Respuesta PutProducto(int codigo, int cantidad, int opcion, string usuario, ref Error error)
        {

            ProductoDAL db = new ProductoDAL();
            Respuesta list = new Respuesta();

            try
            {
                list = db.PutProducto(codigo, cantidad, opcion, usuario, ref error);
            }
            catch (Exception ex)
            {
                error = new Error()
                {
                    Descripcion = "Error al tratar de actualizar el registro, " + ex.Message,
                    Resultado = "0"
                };
            }

            return list;

        }


    }
}
