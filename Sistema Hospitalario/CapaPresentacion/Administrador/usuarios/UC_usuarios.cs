using Sistema_Hospitalario.CapaNegocio.DTOs.UsuarioDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sistema_Hospitalario.CapaNegocio.Servicios.UsuarioService;

namespace Sistema_Hospitalario.CapaPresentacion.Administrador.usuarios
{
    public partial class UC_usuarios : UserControl
    {
        private static readonly UsuarioService usuarioService = new UsuarioService();
        private readonly UsuarioService _service = usuarioService;
        public UC_usuarios()
        {
            InitializeComponent();
            CargarComboOrdenamiento();
            RefrescarGrilla(null, null);
            ConfigurarEstilosGrilla();
            ActualizarContadoresRoles();
        }

        private void RefrescarGrilla(string campo = null, string valor = null)
        {
            try
            {
                var lista = _service.ObtenerUsuarios(campo, valor);
                dgvUsuarios.DataSource = lista;

                if (dgvUsuarios.Columns["IdUsuario"] != null)
                {
                    dgvUsuarios.Columns["IdUsuario"].Visible = false;
                }
                if (dgvUsuarios.Columns["Password"] != null)
                {
                    dgvUsuarios.Columns["Password"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarEstilosGrilla()
        {
            // Puedes copiar los mismos estilos que usaste para dgvMedicos
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.BackgroundColor = Color.White;
            dgvUsuarios.BorderStyle = BorderStyle.None;
            dgvUsuarios.EnableHeadersVisualStyles = false;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvUsuarios.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvUsuarios.DefaultCellStyle.ForeColor = Color.Black;
            dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 9);
        }

        private void CargarComboOrdenamiento()
        {
            var tipoDelDto = typeof(MostrarUsuariosDTO);
            var propiedades = tipoDelDto.GetProperties();
            var listaDeNombres = propiedades.Select(p => p.Name)
                                            .Where(name => name != "Password") // No mostrar Password en el combo
                                            .ToList();
            cboCampo.DataSource = listaDeNombres; // Asume que tienes un ComboBox llamado cboCampo
        }
        private void BtnNuevoUsuario_Click(object sender, EventArgs e)
        {
            MenuModer parentForm = this.FindForm() as MenuModer;

            parentForm.AbrirUserControl(new UC_registrarUsuario());
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string campo = (string)cboCampo.SelectedItem;
            string valor = txtBuscar.Text.Trim(); // Asume que tienes un TextBox llamado txtBuscar

            if (string.IsNullOrEmpty(campo))
            {
                MessageBox.Show("Por favor, seleccione un campo para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RefrescarGrilla(campo, valor);
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            RefrescarGrilla(null, null);
            txtBuscar.Focus();
        }

        private void DgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvUsuarios.Rows[e.RowIndex];
            int idUsuario = Convert.ToInt32(row.Cells["IdUsuario"].Value);

            string nombreUsuario = row.Cells["NombreUsuario"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"¿Desea eliminar al usuario '{nombreUsuario}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    _service.EliminarUsuario(idUsuario);
                    RefrescarGrilla(cboCampo.Text, txtBuscar.Text);
                    ActualizarContadoresRoles();
                    MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ActualizarContadoresRoles()
        {
            try
            {
                var conteos = _service.ObtenerConteoUsuariosPorRol();
               
                if (conteos.ContainsKey("medico"))
                {
                    lblTotalMedicos.Text = conteos["medico"].ToString(); 
                }
                else
                {
                    lblTotalMedicos.Text = "0";
                }

                if (conteos.ContainsKey("administrativo")) 
                {
                    lblTotalAdministrativos.Text = conteos["administrativo"].ToString();
                }
                else
                {
                    lblTotalAdministrativos.Text = "0";
                }

                if (conteos.ContainsKey("gerente")) 
                {
                    lblTotalGerentes.Text = conteos["gerente"].ToString();
                }
                else
                {
                    lblTotalGerentes.Text = "0";
                }

                if (conteos.ContainsKey("administrador")) 
                {
                    lblTotalAdministradores.Text = conteos["administrador"].ToString();
                }
                else
                {
                    lblTotalAdministradores.Text = "0";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los contadores de roles: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblTotalMedicos.Text = "N/A";
                lblTotalAdministrativos.Text = "N/A";
                lblTotalGerentes.Text = "N/A";
                lblTotalAdministradores.Text = "N/A";
            }
        }
    }
}
