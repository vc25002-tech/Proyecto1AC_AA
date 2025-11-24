using Proyecto1A.CapaDatos;
using Proyecto1A.CapaEdentidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1A.CapaDatos
{
    public class ClienteDAL
    {
        public DataTable Listar()

        {


            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))

            {

                string sql = "SELECT Id, Nombre, Dui, Telefono, Correo, Estado from Cliente";
                //Consulta SQL que devuelve todos los registros de la tabla cliente
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                //sqlCommand: Prepara el comando sql que se enviara al servidor
                //Sever usando la cadena de conexion
                {

                    cn.Open(); //Abre la conexion
                    new SqlDataAdapter(cmd).Fill(dt);
                    //SqlDataAdapter: Ejecuta el SELECT y llena el DataTable con los resultados

                }

            }

            return dt;//Retorna la tabla con los datos
        }

        public int Insertar(Clientes2 c)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"INSERT INTO Cliente(Nombre, Dui, Telefono, Correo, Estado) VALUES (@nombre, @dui,@telefono,@correo,@estado); SELECT SCOPE_IDENTITY();";
                //string sql = @"INSERT INTO Cliente(Nombre, Dui, Telefono, Correo, Estado) VALUES (@nombre, @dui,@telefono,@correo,@estado); SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {

                    cmd.Parameters.AddWithValue("@id", c.Id);
                    cmd.Parameters.AddWithValue("@nombre", c.Nombre);
                    cmd.Parameters.AddWithValue("@dui", c.Dui);
                    cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                    cmd.Parameters.AddWithValue("@correo", c.Correo);
                    cmd.Parameters.AddWithValue("@estado", c.Estado);
                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());

                }

            }

        }


        public bool Actualizar(Clientes2 c)

        {

            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"UPDATE Cliente SET Nombre=@nombre, Dui=@dui, Telefono=@telefono, Correo=@correo, Estado=@estado WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {

                    cmd.Parameters.AddWithValue("@nombre", c.Nombre);
                    cmd.Parameters.AddWithValue("@dui", c.Dui);
                    cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                    cmd.Parameters.AddWithValue("@correo", c.Correo);
                    cmd.Parameters.AddWithValue("@estado", c.Estado);

                    cn.Open();
                    return cmd.ExecuteNonQuery() > 0;

                }

            }


        }

        public bool Eliminar(int id)

        {

            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {

                String sql = "DELETE FROM Cliente WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {

                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }

            }
        }

        public DataTable Buscar(string filtro)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))

            {

                string sql = @"SELECT Id, Nombre, Telefono, Dui, Correo Estado FROM Cliente WHERE Nombre LIKE @filtro OR Telefono LIKE @filtro";
                using (SqlCommand cmd = new SqlCommand(sql, cn))

                {

                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%" + "%");
                    cn.Open();
                    new SqlDataAdapter(cmd).Fill(dt);

                }

            }

            return dt;
        }

    }

}