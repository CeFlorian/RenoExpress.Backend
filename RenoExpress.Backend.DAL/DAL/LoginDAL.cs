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
    public class LoginDAL
    {

        public Usuario SignIn(Usuario usuario, ref Error error)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet dataSet = new DataSet();
            Usuario userData = new Usuario();

            try
            {

                cmd.Connection = new SqlConnection(ConfigurationManager.AppSettings["db"].ToString()); 
                cmd.CommandText = "[SP_InicioSesion]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@usuario", usuario.usuario);

                using (SqlDataAdapter SqlData = new SqlDataAdapter(cmd))
                {
                    SqlData.Fill(dataSet);
                }


                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow data = dataSet.Tables[0].Rows[0];
                    userData.CorreoElectronico = data["usuario"].ToString();
                    userData.Contrasenia = data["contrasenia"].ToString();
                    userData.Rol = new Rol()
                    {
                        Id = int.Parse(data["idrol"].ToString()),
                        Descripcion = data["rol"].ToString(),
                    };

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

            return userData;
        }

    }
}
