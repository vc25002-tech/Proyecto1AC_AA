using Proyecto1A.CapaNegocio;
using Proyecto1A.Reportes.Prueba_XYZ.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1A.CapaPresentacion
{
    public partial class FrmReportes : Form
    {
        public FrmReportes()
        {
            InitializeComponent();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DateTime inicio = dtpIncio.Value.Date;
            DateTime fin = dtpFin.Value.Date;

            // Validación de fechas
            if (fin < inicio)
            {
                MessageBox.Show("La fecha final no puede ser menor a la inicial.",
                    "Error de Fechas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ============================
            // 1) OBTENER LAS VENTAS DEL BLL
            // ============================
            DataTable tabla = ReporteBLL.ObtenerVentasPorPeriodo(inicio, fin);

            if (tabla.Rows.Count == 0)
            {
                MessageBox.Show("No existen ventas registradas en ese período.",
                    "Sin Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ============================
            // 2) SELECCIONAR DONDE GUARDAR PDF
            // ============================
            saveFileDialog1.Title = "Guardar Reporte en PDF";
            saveFileDialog1.Filter = "Archivo PDF (*.pdf)|*.pdf";
            saveFileDialog1.FileName = $"ReporteVentas_{inicio:dd-MM-yyyy}_a_{fin:dd-MM-yyyy}.pdf";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            string ruta = saveFileDialog1.FileName;

            try
            {
                // ============================
                // 3) GENERAR PDF CON QuestPDF
                // ============================
                ReporteVentasPDF.GenerarPDF(tabla, inicio, fin, ruta);

                // Mensaje de éxito
                MessageBox.Show("Reporte generado correctamente.\n\n" +
                                "Ubicación:\n" + ruta,
                                "PDF Generado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el PDF:\n" + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
