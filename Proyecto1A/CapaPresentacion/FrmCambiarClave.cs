using Proyecto1A.CapaDatos;
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
    public partial class FrmCambiarClave : Form
    {
        public FrmCambiarClave()
        {
            InitializeComponent();
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            try
            {
                int id = SesionActual.IdUsuario;
                if (id == 0) throw new Exception("No hay sesión activa.");

                // Verificar clave actual
                var user = UsuarioDAL.Login(SesionActual.NombreUsuario, PruebaHash(txtClaveActual.Text));
                if (user == null)
                {
                    MessageBox.Show("La contraseña actual es incorrecta.");
                    return;
                }

                if (txtNuevaClave.Text != txtConfirma.Text)
                {
                    MessageBox.Show("La nueva contraseña y su confirmación no coinciden.");
                    return;
                }

                bool ok = UsuarioBLL.CambiarClave(id, txtNuevaClave.Text);
                MessageBox.Show(ok ? "Contraseña actualizada." : "No se pudo actualizar.");
                if (ok) this.Close();
                SesionActual.Cerrar();
                FrmLogin frm = new FrmLogin();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        // Método privado para generar hash temporal y verificar (evita duplicar Seguridad en UI)
        private string PruebaHash(string pass)
        {
            return Seguridad.Hash_SHA256(pass);
        }

        private void btnCancerla_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmLogin frm = new FrmLogin();
            frm.ShowDialog();
        }

        private void FrmCambiarClave_Load(object sender, EventArgs e)
        {

        }
    }
}

