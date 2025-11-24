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
            FrmClientes frm = new FrmClientes();

            frm.ShowDialog();
        }

        private void Sabor_Load(object sender, EventArgs e)
        {

        }
    }
}
