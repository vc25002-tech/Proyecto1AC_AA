using Proyecto1A.CapaEdentidad;
using Proyecto1A.CapaNegocio;
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
    public partial class FrmClientes2 : Form
    {

        int ClienteId = 0;

        ClienteBLL bill = new ClienteBLL();
        public FrmClientes2()
        {
            InitializeComponent();
        }

        private void FrmClientes2_Load(object sender, EventArgs e)
        {
            CargarDatos();
            Limpiar();

        }

        public void CargarDatos()
        {

            dgvClientes.DataSource = bill.Listar();
        }

        private void Limpiar()
        {

            txtNombre.Clear();
            txtDui.Clear();
            txtTelefono.Clear();
            btnCorreo.Clear();
            chkEstado.Checked = true;
            txtBuscar.Clear();
            txtNombre.Clear();

            ClienteId = 0;


        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try

            {
                Clientes2 c = new Clientes2
                {

                    Id = ClienteId,
                    Nombre = txtNombre.Text,
                    Dui = txtDui.Text,
                    Telefono = txtTelefono.Text,
                    Correo = btnCorreo.Text,
                    Estado = chkEstado.Checked,

                };

                int id = bill.Guardar(c);
                MessageBox.Show("Cliente guardado con exito", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CargarDatos();
                Limpiar();

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
                Limpiar();


                {
                }

            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ClienteId = Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells["Id"].Value);
                txtNombre.Text = dgvClientes.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                txtDui.Text = dgvClientes.Rows[e.RowIndex].Cells["Dui"].Value.ToString();
                txtTelefono.Text = dgvClientes.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                btnCorreo.Text = dgvClientes.Rows[e.RowIndex].Cells["Correo"].Value.ToString();
                chkEstado.Checked = Convert.ToBoolean(dgvClientes.CurrentRow.Cells["Estado"].Value);


            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (ClienteId == 0)
            {
                
                MessageBox.Show("Seleccione un cliente para eliminar.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;


            }

            if (MessageBox.Show("Esta seguro de eliminar el cliente seleccionado?","Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bill.Eliminar(ClienteId);
                CargarDatos();
                Limpiar();



            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvClientes.DataSource = bill.Buscar(txtBuscar.Text);
        }
    }

}