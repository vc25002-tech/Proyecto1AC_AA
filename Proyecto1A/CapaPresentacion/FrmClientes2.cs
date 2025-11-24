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

        ClienteBLL bill = new ClienteBLL();
        public FrmClientes2()
        {
            InitializeComponent();
        }

        private void FrmClientes2_Load(object sender, EventArgs e)
        {
            CargarDatos();
           

        }

        public void CargarDatos()
        {

            dgvClientes.DataSource = bill.Listar();
        }

    }
}
