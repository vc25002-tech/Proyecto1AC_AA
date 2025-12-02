using Proyecto1A.CapaEdentidad;
using Proyecto1A.CapaPresentacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1A
{
    public partial class Sabor : Form
    {
        public Sabor()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            //creo una instancia del formulario frmProductos
            FrmProductos frm = new FrmProductos();
          
            frm.ShowDialog();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {

            //creo una instancia del formulario frmProductos
            FrmClientes2 frm = new FrmClientes2();

            frm.ShowDialog();
        }

        private void Sabor_Load(object sender, EventArgs e)
        {
            lblUsuarios.Text = $"Usuario: {SesionActual.NombreUsuario} - Rol: {SesionActual.Rol}";

            /// Control básico por rol
//Con este codigo deshabilitamos un botón de prueba para el usuario cajero, por ejemplo que no pueda Registrar Cliente(ojo esto es solo prueba)
            switch (SesionActual.Rol)
            {
                case "Admin":
                    // todo habilitado
                    break;
                case "Cajero":
                    btnClientes.Enabled = false;
                    btnUsuario.Enabled = false;
                    break;
                default:
                    btnClientes.Enabled = false;
                    btnUsuario.Enabled = false;
                    break;

            }


        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            FrmUsuarios frm = new FrmUsuarios();
            frm.ShowDialog();

        }

        private void cambiarClaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCambiarClave frm = new FrmCambiarClave();
            frm.ShowDialog();
        }
    }
}
