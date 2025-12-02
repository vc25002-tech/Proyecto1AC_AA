using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1A.CapaDatos
{
    public class ReporteDAL
    {

        /// Obtiene un DataTable con las ventas entre dos fechas.
        public DataTable ReporteVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable tabla = new DataTable();

            // Usamos using para asegurar el Dispose de la conexión y el comando
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_reporte_ventas_periodo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parametrizamos las fechas
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.Date);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Date);

                    conn.Open();

                    // Ejecutamos el lector y cargamos el DataTable
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        tabla.Load(dr);
                    }
                }
            }

            return tabla;
        }

        public static DataTable ObtenerVentaPorPeriodo(DateTime inicio, DateTime fin)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_reporte_ventas_periodo", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FechaInicio", inicio);
                cmd.Parameters.AddWithValue("@FechaFin", fin);

                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }


    }
}
