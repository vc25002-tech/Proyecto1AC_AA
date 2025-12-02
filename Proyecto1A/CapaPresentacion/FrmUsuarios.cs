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
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();

            cmdRol.Items.AddRange(new string[] { "Admin", "Cajero", "Mesero" });

        }
        private void CargarUsuarios()
        {
            dvgUsuarios.DataSource = UsuarioBLL.Listar().Select(u => new {
                u.IdUsuario,
                u.NombreUsuario,
                u.Rol,
                Estado = u.Estado ? "Activo" : "Inactivo"
            }).ToList();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = UsuarioBLL.Insertar(txtNombreUsuario.Text.Trim(), txtClave.Text.Trim(), cmdRol.Text);
                MessageBox.Show("Usuario creado ID: " + id);
                Limpiar();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                bool estado = chkEstado.Checked;
                bool ok = UsuarioBLL.Actualizar(id, txtNombreUsuario.Text.Trim(), cmdRol.Text, estado);
                MessageBox.Show(ok ? "Actualizado" : "No se actualizó");
                CargarUsuarios();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                var r = MessageBox.Show("¿Eliminar usuario?", "Confirmar", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    bool ok = UsuarioBLL.Eliminar(id);
                    MessageBox.Show(ok ? "Eliminado" : "No eliminado");
                    CargarUsuarios();
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void dvgUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = dvgUsuarios.Rows[e.RowIndex].Cells["IdUsuario"].Value.ToString();
                txtNombreUsuario.Text = dvgUsuarios.Rows[e.RowIndex].Cells["NombreUsuario"].Value.ToString();
                cmdRol.Text = dvgUsuarios.Rows[e.RowIndex].Cells["Rol"].Value.ToString();
                chkEstado.Checked = dvgUsuarios.Rows[e.RowIndex].Cells["Estado"].Value.ToString() == "Activo";
            }

        }

        private void Limpiar()
        {
            txtId.Text = "";
            txtNombreUsuario.Text = "";
            txtClave.Text = "";
            cmdRol.SelectedIndex = -1;
            chkEstado.Checked = false;
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();

        }
    }
}
