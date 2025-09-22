using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Turnos;

using Sistema_Hospitalario.CapaNegocio.DTOs;
using Sistema_Hospitalario.CapaNegocio.Servicios;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    public partial class UC_Pacientes : UserControl
    {
        private readonly BindingSource _bs = new BindingSource();
        private readonly PacienteService _pacienteService = new PacienteService();
        private List<PacienteListadoDto> _items = new List<PacienteListadoDto>(); // datos para el grid

        public event EventHandler RegistrarPacienteSolicitado;
        public event EventHandler<PacienteDetalleDto> VerPacienteSolicitado;

        public UC_Pacientes()
        {
            InitializeComponent();

            ConfigurarActividad();
            CargarOpcionesDeFiltro();
            ConfigurarEnlazadoDeColumnas();
            CargarDesdeServicio();

            dgvPacientes.CellContentClick += dgvPacientes_CellContentClick;
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            RegistrarPacienteSolicitado?.Invoke(this, EventArgs.Empty);
        }

        private void ConfigurarActividad()
        {
            dgvPacientes.ReadOnly = true;
            dgvPacientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPacientes.RowHeadersVisible = false;
            dgvPacientes.AllowUserToResizeRows = false;

            dgvPacientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPacientes.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvPacientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);

            dgvPacientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvPacientes.ColumnHeadersHeight = 35;
            dgvPacientes.EnableHeadersVisualStyles = false;
            dgvPacientes.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        private void CargarDesdeServicio()
        {
            // Trae la lista “plana” para el grid desde la capa de negocio (BD)
            _items = _pacienteService.ListarPacientes(); // List<PacienteListadoDto>
            _bs.DataSource = _items;
            dgvPacientes.DataSource = _bs;
        }

        private void ConfigurarEnlazadoDeColumnas()
        {
            dgvPacientes.AutoGenerateColumns = false;

            dgvPacientes.Columns["colPaciente"].DataPropertyName = "Paciente"; 
            dgvPacientes.Columns["colDNI"].DataPropertyName = "DNI";      
            dgvPacientes.Columns["colEdad"].DataPropertyName = "Edad";    
            dgvPacientes.Columns["colEstado"].DataPropertyName = "Estado";
        }

        private void dgvPacientes_CellContentClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvPacientes.Columns[e.ColumnIndex].Name == "colAccion")
            {
                var item = _bs[e.RowIndex] as PacienteListadoDto;
                if (item == null) return;

                // Traer el detalle desde negocio/datos
                var detalle = _pacienteService.ObtenerDetalle(item.Id);
                if (detalle == null)
                {
                    MessageBox.Show("No se encontró el paciente seleccionado.", "Atención",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Disparar el evento hacia el contenedor (Form/Padre) para que lo muestre
                VerPacienteSolicitado?.Invoke(this, detalle);
            }
        }

        private void CargarOpcionesDeFiltro()
        {
            if (cboCampo == null) return;

            cboCampo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo.Items.Clear();
            cboCampo.Items.AddRange(new[] { "Todos", "Paciente", "DNI", "Estado" });
            cboCampo.SelectedIndex = 0;
        }

        private void AplicarFiltro(string campo, string texto)
        {
            string q = (texto ?? "").Trim().ToLowerInvariant();
            IEnumerable<PacienteListadoDto> query = _items;

            if (!string.IsNullOrEmpty(q))
            {
                switch (campo)
                {
                    case "Paciente":
                        query = query.Where(t => (t.Paciente ?? "").ToLower().Contains(q));
                        break;
                    case "DNI":
                        int dniBuscado = int.Parse(q);
                        query = query.Where(t => t.DNI == dniBuscado);
                        break;  
                    case "Estado":
                        query = query.Where(t => (t.Estado ?? "").ToLower().Contains(q));
                        break;
                    default:
                        query = query.Where(t =>
                            (t.Paciente ?? "").ToLower().Contains(q) ||
                            t.DNI.ToString().Contains(q) ||
                            (t.Estado ?? "").ToLower().Contains(q));
                        break;
                }
            }

            _bs.DataSource = query.OrderBy(t => t.Paciente).ToList();
            _bs.ResetBindings(false);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            if (cboCampo != null) cboCampo.SelectedIndex = 0;

            _bs.DataSource = _items.OrderBy(t => t.Paciente).ToList();
            _bs.ResetBindings(false);
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            var campo = cboCampo.SelectedItem?.ToString() ?? "Todos";
            var texto = txtBuscar.Text;
            AplicarFiltro(campo, texto);
        }
    }
}
