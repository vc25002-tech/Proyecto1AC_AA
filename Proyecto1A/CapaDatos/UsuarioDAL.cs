using Proyecto1A.CapaEdentidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1A.CapaDatos
{
    public class UsuarioDAL
    {

        //Metodo que valida nombre de usuario, clave y estado para logearnos
        public static Usuario Login(string nombreUsuario, string claveHash)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT IdUsuario, NombreUsuario, Rol, Estado FROM Usuario WHERE NombreUsuario=@u AND ClaveHash=@h AND Estado=1", cn))
                {
                    cmd.Parameters.AddWithValue("@u", nombreUsuario);
                    cmd.Parameters.AddWithValue("@h", claveHash);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                Rol = dr["Rol"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Listar todos los usuarios
        public static List<Usuario> Listar()
        {
            var lista = new List<Usuario>();
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT IdUsuario, NombreUsuario, Rol, Estado FROM Usuario", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Usuario
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            NombreUsuario = dr["NombreUsuario"].ToString(),
                            Rol = dr["Rol"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        // Insertar usuario (recibe hash ya calculado)
        public static int Insertar(string nombreUsuario, string claveHash, string rol)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Usuario (NombreUsuario, ClaveHash, Rol, Estado) VALUES (@u, @h, @r, 1); SELECT SCOPE_IDENTITY();", cn))
                {
                    cmd.Parameters.AddWithValue("@u", nombreUsuario);
                    cmd.Parameters.AddWithValue("@h", claveHash);
                    cmd.Parameters.AddWithValue("@r", rol);
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

        // Actualizar (no actualiza clave)
        public static bool Actualizar(int id, string nombreUsuario, string rol, bool estado)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Usuario SET NombreUsuario=@u, Rol=@r, Estado=@e WHERE IdUsuario=@id", cn))
                {
                    cmd.Parameters.AddWithValue("@u", nombreUsuario);
                    cmd.Parameters.AddWithValue("@r", rol);
                    cmd.Parameters.AddWithValue("@e", estado);
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        //Eliminar Usuario
        public static bool Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE IdUsuario=@id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Cambiar clave (recibe hash nuevo)
        public static bool CambiarClave(int id, string claveHashNueva)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Usuario SET ClaveHash=@h WHERE IdUsuario=@id", cn))
                {
                    cmd.Parameters.AddWithValue("@h", claveHashNueva);
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


    }
}
