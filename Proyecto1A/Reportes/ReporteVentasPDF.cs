using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1A.Reportes
{
    public class ReporteVentasPDF
    {

    }


namespace Prueba_XYZ.Reportes
    {
        // Esta clase actúa como "fachada": es la que usará el formulario.
        // Recibe la información que el usuario seleccionó (tabla, fechas, ruta)
        // y se encarga de generar el modelo y el documento PDF.
        public class ReporteVentasPDF
        {
            // Método estático: se puede llamar sin crear instancias
            public static void GenerarPDF(DataTable tabla, DateTime inicio, DateTime fin, string rutaArchivo)
            {
                // QuestPDF requiere declarar la licencia (Community es gratuita)
                QuestPDF.Settings.License = LicenseType.Community;

                // 1) Creamos el modelo de datos (DataTable y rango de fechas)
                var modelo = new ReporteVentasModel(tabla, inicio, fin);

                // 2) Creamos el documento PDF usando ese modelo
                var documento = new ReporteVentasDocumento(modelo);

                // 3) Generamos el archivo PDF en disco
                documento.GeneratePdf(rutaArchivo);
            }
        }


    }
}
