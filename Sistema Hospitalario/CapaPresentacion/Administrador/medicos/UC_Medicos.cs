using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;
using Sistema_Hospitalario.CapaPresentacion.Administrador.usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrador.medicos
{
    public partial class UC_Medicos : UserControl
    {
        private static readonly MedicoService medicoService = new MedicoService(new MedicoRepository());
        private readonly MedicoService _service = medicoService;

        public UC_Medicos()
        {
            InitializeComponent();
            CargarComboOrdenamiento();
            RefrescarGrilla(null, null); 
            ConfigurarEstilosGrilla();
        }

        private void RefrescarGrilla(string campo = null, string valor = null)
        {

            var lista = _service.ObtenerMedicos(campo, valor);

            dgvMedicos.DataSource = lista;

            if (dgvMedicos.Columns["IdMedico"] != null)
            {
                dgvMedicos.Columns["IdMedico"].Visible = false;
            }
        }

        private void ConfigurarEstilosGrilla()
        {

            dgvMedicos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMedicos.RowHeadersVisible = false;
            dgvMedicos.BackgroundColor = Color.White;
            dgvMedicos.BorderStyle = BorderStyle.None;
            dgvMedicos.EnableHeadersVisualStyles = false;
            dgvMedicos.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvMedicos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMedicos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvMedicos.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvMedicos.DefaultCellStyle.ForeColor = Color.Black;
            dgvMedicos.DefaultCellStyle.Font = new Font("Segoe UI", 9);
        }

        private void CargarComboOrdenamiento()
        {
 
            var tipoDelDto = typeof(MostrarMedicoDTO);
            var propiedades = tipoDelDto.GetProperties();
            var listaDeNombres = propiedades.Select(p => p.Name).ToList();
            cboCampo.DataSource = listaDeNombres;
        }

         private void  BtnBuscar_Click(object sender, EventArgs e)
        {
 
            string campo = (string)cboCampo.SelectedItem;
            string valor = txtBuscar.Text.Trim();

            RefrescarGrilla(campo, valor);
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();

            RefrescarGrilla(null, null);

            txtBuscar.Focus();
        }

        private void DgvMedicos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;

            var row = dgvMedicos.Rows[e.RowIndex];
            int idMedico = Convert.ToInt32(row.Cells["IdMedico"].Value);
            string nombre = row.Cells["Nombre"].Value.ToString();
            string apellido = row.Cells["Apellido"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"¿Desea eliminar al médico {nombre} {apellido}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    _service.EliminarMedico(idMedico);
                    RefrescarGrilla(cboCampo.Text, txtBuscar.Text); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el médico: " + ex.Message);
                }
            }
        }

        private void BtnNuevoPaciente_Click(object sender, EventArgs e)
        {
            MenuModer parentForm = this.FindForm() as MenuModer;
            parentForm.AbrirUserControl(new UC_agregarMedico());
        }
    }
}