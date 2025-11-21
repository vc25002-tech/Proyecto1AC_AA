using Proyecto1A.CapaEdentidad;
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
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
        }

        //Creacion de una lista estatica que simulara la DB

        public static List<Producto> listaProductos = new List<Producto>();
        private void DeshabilitarBotones() { 

        btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnLimpiar.Enabled = false;
            btnNuevo.Enabled = true;

        }

        private void HabilitarBotones() {

            btnNuevo.Enabled = false;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnLimpiar.Enabled = true;
             }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            DeshabilitarBotones();
            //Cargar los datos iniciales
            if (!listaProductos.Any())
            {// cada vez que se cargue el formulario, si la lista esta vacia,
             // se agregan los productos iniciales
                listaProductos.Add(new Producto
                {
                    Id = 1,
                    Nombre = "Café Gourmet",
                    Descripcion = "Importado",
                    Precio = 10.5m,
                   Stock = 100,
                    Estado = true
                });
                listaProductos.Add(new Producto
                {
                    Id = 2,
                    Nombre = "Café Borbon",
                    Descripcion = "De altura",
                    Precio = 20.0m,
                    Stock = 50,
                    Estado = true
                });
                listaProductos.Add(new Producto
                {
                    Id = 3,
                   Nombre = "Cheescake",
                    Descripcion = "Dulce sabor",
                    Precio = 15.75m,
                    Stock = 75,
                    Estado = true
                });
            }
            RefrescarGrid();//mando a llamar el metodo para refrescar el datagridview
        }
        //asignar la lista como DataSOurce al datagridview
        private void RefrescarGrid()
        {
            dgnProductos.DataSource = null; // Limpiar el DataSource antes de reasignarlo
            dgnProductos.DataSource = listaProductos; // Asignar la lista como DataSource
        }

        private void dgnProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Boton Guardar

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //Validaciones basicas
            //valida que el nombre no este vacio
            if (string.IsNullOrWhiteSpace(Nombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Nombre.Focus();
                return;
            }//valida que el precio ingresado sea un decimal
            if (!Valiraciones.EsDecimal(Precio.Text))
            {
                MessageBox.Show("El precio del producto debe ser un valor numérico.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Precio.Focus();
                return;
            }//valida que el stock ingresado sea un entero
            if (!Valiraciones.EsEntero(Stock.Text))
            {
                MessageBox.Show("el stock del producto debe ser un valor entero.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Stock.Focus();
                return;
            }

            //crear objeto producto y asignat Id incremetal
            int nuevoIdo = listaProductos.Any() ? listaProductos.Max(x => x.Id) + 1 : 1;
            var P = new Producto
            {

                Id = nuevoIdo,
                Nombre = Nombre.Text,
                Descripcion = Descripcion.Text,
                Precio = decimal.Parse(Precio.Text),
                Stock = int.Parse(Stock.Text),
                Estado = Estado.Checked
            };

            //Agregar a la lista
            listaProductos.Add(P);
            RefrescarGrid();
            LimpiarCampos();
            

            }

        private void LimpiarCampos()
        {

            Nombre.Clear();
            Descripcion.Clear();
            Precio.Clear();
            Stock.Clear();
            Estado.Checked = true;
            Nombre.Focus();

        }

        private void dgnProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgnProductos.CurrentRow == null) return;
            //Obtener el producto seleccionado
            id.Text = dgnProductos.CurrentRow.Cells["Id"].Value.ToString();
            Nombre.Text = dgnProductos.CurrentRow.Cells["Nombre"].Value.ToString();
            Descripcion.Text = dgnProductos.CurrentRow.Cells["Descripcion"].Value.ToString();
            Precio.Text = dgnProductos.CurrentRow.Cells["Precio"].Value.ToString();
            Stock.Text = dgnProductos.CurrentRow.Cells["Stock"].Value.ToString();
            Estado.Checked = (bool)dgnProductos.CurrentRow.Cells["Estado"].Value;

            HabilitarBotones();
        }
        
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Eliminar producto

            if (!int.TryParse(id.Text, out int Id)) {

                MessageBox.Show("Seleccione un producto valido para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            
            }
            var proud = listaProductos.FirstOrDefault(x => x.Id == Id);
            if (proud == null) {

                MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            
            }

            if (MessageBox.Show("¿Está seguro de eliminar el producto seleccionado?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listaProductos.Remove(proud);//con remove elimino el producto de la lista
                RefrescarGrid();//refrescar el datagridview
                LimpiarCampos();//limpiar los controles
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //valido que el id sea un entero
            if (!int.TryParse(id.Text, out int Id))
            {
                MessageBox.Show("Seleccione un producto válido para eliminar.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //busco el producto en la lista
            var prod = listaProductos.FirstOrDefault(x => x.Id == Id);
            //si lo encuentro, actualizo sus propiedades
            if (prod == null)
            {
                MessageBox.Show("Producto no encontrado.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Validaciones identicas a las del boton guardar
            if (string.IsNullOrWhiteSpace(Nombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Nombre.Focus();
                return;
            }//valida que el precio ingresado sea un decimal
            if (!Valiraciones.EsDecimal(Precio.Text))
            {
                MessageBox.Show("El precio del producto debe ser un valor numérico.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Precio.Focus();
                return;
            }//valida que el stock ingresado sea un entero
            if (!Valiraciones.EsEntero(Stock.Text))
            {
                MessageBox.Show("el stock del producto debe ser un valor entero.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Stock.Focus();
                return;
            }
            //Actualizar los campos en memoria
            prod.Nombre = Nombre.Text.Trim();
            prod.Descripcion = Descripcion.Text.Trim();
            prod.Precio = decimal.Parse(Precio.Text);
            prod.Stock = int.Parse(Stock.Text);
            prod.Estado = Estado.Checked;
            MessageBox.Show("Producto actualizado correctamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefrescarGrid();//refrescar el datagridview
            LimpiarCampos();//limpiar los controles
            HabilitarBotones();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
