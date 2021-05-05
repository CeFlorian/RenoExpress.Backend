using RenoExpress.Backend.MOD.MOD;
using RenoExpress.Backend.MOD.MOD.Auxiliar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenoExpress.Backend.DAL.DAL
{
    public class ProductoDAL
    {
        public List<Producto> GetProducto(int codigo, ref Error error)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet dataSet = new DataSet();
            List<Producto> result = new List<Producto>();

            try
            {

                cmd.Connection = new SqlConnection(ConfigurationManager.AppSettings["db"].ToString());
                cmd.CommandText = "[SP_GetProducto]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductoID", codigo);

                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    SqlData.Fill(dataSet);
                }


                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow data in dataSet.Tables[0].Rows)
                    {
                        DateTime date = new DateTime();
                        result.Add(new Producto
                        {
                            Codigo = int.Parse(data["Producto_Codigo"].ToString()),
                            Descripcion = data["Producto_Descripcion"].ToString(),
                            Marca = data["Producto_Marca"].ToString(),
                            Precio_Unitario = decimal.Parse(data["Producto_Precio_Unitario"].ToString()),
                            Cantidad = int.Parse(data["Producto_Cantidad"].ToString()),
                            Categoria = new Categoria()
                            {
                                Id = int.Parse(data["Categoria_Id"].ToString()),
                                Descripcion = data["Categoria_Descripcion"].ToString(),
                            },
                            Fecha_Ultimo_Abastecimiento = DateTime.Parse(data["Producto_Fecha_Ultimo_Abastecimiento"].ToString()),
                            Cantidad_Ultimo_Abastecimiento = int.Parse(data["Producto_Cantidad_Ultimo_Abastecimiento"].ToString()),

                        });
                    }

                    cmd.Dispose();
                }
                else
                {
                    error = new Error
                    {
                        Resultado = "0",
                        Descripcion = "No se encontró ningún registro"
                    };
                }

            }
            catch (Exception ex)
            {
                error = new Error()
                {
                    Descripcion = "Error al intentar recuperar los datos, " + ex.Message,
                    Resultado = "0"
                };
            }
            finally
            {
                cmd.Dispose();
                dataSet.Dispose();
            }

            return result;
        }


        public Respuesta PostProducto(Producto producto, string usuario, ref Error error)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet dataSet = new DataSet();
            Respuesta result = new Respuesta();

            try
            {
                cmd.Connection = new SqlConnection(ConfigurationManager.AppSettings["db"].ToString());
                cmd.CommandText = "[SP_PostProducto]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo", producto.Codigo);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@Marca", producto.Marca);
                cmd.Parameters.AddWithValue("@PrecioU", producto.Precio_Unitario);
                cmd.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
                cmd.Parameters.AddWithValue("@Categoria", producto.Categoria.Id);
                cmd.Parameters.AddWithValue("@Usuario", usuario);


                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    SqlData.Fill(dataSet);
                }


                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow data = dataSet.Tables[0].Rows[0];
                    result.Message = data["RespMessage"].ToString();
                    result.Response = int.Parse(data["Result"].ToString()) > 0 ? true : false;

                    cmd.Dispose();
                }

            }
            catch (Exception ex)
            {
                error = new Error()
                {
                    Descripcion = "Error al intentar registrar los datos, " + ex.Message,
                    Resultado = "0"
                };
            }
            finally
            {
                cmd.Dispose();
                dataSet.Dispose();
            }

            return result;
        }


        public Respuesta PutProducto(int codigo, int cantidad, int opcion, string usuario, ref Error error)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet dataSet = new DataSet();
            Respuesta result = new Respuesta();

            try
            {
                cmd.Connection = new SqlConnection(ConfigurationManager.AppSettings["db"].ToString());
                cmd.CommandText = "[SP_PutProducto]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@Opcion", opcion);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Usuario", usuario);


                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    SqlData.Fill(dataSet);
                }


                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow data = dataSet.Tables[0].Rows[0];
                    result.Message = data["RespMessage"].ToString();
                    result.Response = int.Parse(data["Result"].ToString()) > 0 ? true : false;

                    cmd.Dispose();
                }

            }
            catch (Exception ex)
            {
                error = new Error()
                {
                    Descripcion = "Error al intentar actualizar los datos, " + ex.Message,
                    Resultado = "0"
                };
            }
            finally
            {
                cmd.Dispose();
                dataSet.Dispose();
            }

            return result;
        }

    }
}
