using Proyecto1A.CapaEdentidad;
using Proyecto1A.CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1A.CapaPresentacion
{
    public partial class frmCategoriaGestion : Form
    {
        public string Modo { get; set; }        // Define la acción
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        CategoriaBLL bll = new CategoriaBLL();

        public frmCategoriaGestion()
        {
            InitializeComponent();
        }


        private void frmCategoriaGestion_Load(object sender, EventArgs e)
        {
            if (Modo == "Nuevo")
            {
                label1.Text = "AGREGAR NUEVA CATEGORÍA";
            }
            else
            {
                label1.Text = "MODIFICAR CATEGORÍA";
                // Cargar datos en controles
                txtNombre.Text = Nombre;
                txtDescripcion.Text = Descripcion;
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show(
                        "Debe ingresar un nombre para la categoría.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                // Creamos objeto categoría
                Categoria c = new Categoria
                {
                    Id= Id,
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim()
                };

                bll.Guardar(c);

                MessageBox.Show(
                    Modo == "Nuevo"
                        ? "La categoría ha sido registrada correctamente."
                        : "Los cambios han sido guardados correctamente.",
                    "Operación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1
                );
                Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error al interactuar con la base de datos.\n\nDetalles técnicos:\n" + ex.Message,
                    "Error SQL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado:\n" + ex.Message,
                    "Error general",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
    }



