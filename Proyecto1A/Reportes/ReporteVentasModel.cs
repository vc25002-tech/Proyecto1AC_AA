using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1A.Reportes
{
    public class ReporteVentasModel
    {
        public object Inicio { get; internal set; }
        public object Fin { get; internal set; }
        public object Tabla { get; internal set; }
    }
    namespace Prueba_XYZ.Reportes
    {
        // Clase sencilla que encapsula toda la información necesaria
        // para generar el reporte PDF.
        public class ReporteVentasModel
        {
            // Tabla con la información de ventas (producto, cantidad, etc.)
            public DataTable Tabla { get; }

            // Fechas de inicio y fin del período consultado
            public DateTime Inicio { get; }

            public DateTime Fin { get; }

            // Constructor que recibe los valores y los asigna
            public ReporteVentasModel(DataTable tabla, DateTime inicio, DateTime fin)
            {
                Tabla = tabla;   // Datos del DataTable
                Inicio = inicio; // Fecha inicial
                Fin = fin;       // Fecha final
            }
        }


    }
}
