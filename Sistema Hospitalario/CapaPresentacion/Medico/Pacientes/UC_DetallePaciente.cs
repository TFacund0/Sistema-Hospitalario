using Sistema_Hospitalario.CapaNegocio;
using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO;
using Sistema_Hospitalario.CapaNegocio.Servicios.MedicoService;
using Sistema_Hospitalario.CapaPresentacion.Medico.Pacientes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Medico
{
    public partial class UC_DetallePaciente : UserControl
    {
        private readonly MedicoService service = new MedicoService();
        private PacienteDTO paciente;
        public UC_DetallePaciente(PacienteDTO paciente) // crea el user control con los datos del paciente
        {
            InitializeComponent();
            CargarHistorial();
            this.paciente = paciente; 
            this.TBNombre.Text = paciente.nombre + " " + paciente.apellido;
            this.txtDni.Text = paciente.dni.ToString();
            this.TBDireccion.Text = paciente.direccion;
            this.TBObraSocial.Text = paciente.obraSocial;
            int diasNacimiento = ((DateTime.Now - paciente.FechaNacimiento).Days);
            // Calcula la edad en años o meses según corresponda
            if (diasNacimiento < 365) this.TBEdad.Text = (diasNacimiento / 30).ToString() + " " + "meses";
            else this.TBEdad.Text = (diasNacimiento / 365).ToString() + " " + "años";
            this.TBContacton.Text = paciente.telefono.ToString();
            this.TBHabitacion.Text = paciente.habitacion.ToString();
            this.TBAfiliado.Text = paciente.nroAfiliado.ToString();
            this.TBEstado.Text = paciente.Estado;
        }
        private void CargarHistorial()
        {
            // 1. Recolectamos los filtros
            string dni = txtDni.Text.Trim();
            int _idMedicoLogueado = SesionUsuario.IdMedicoAsociado.Value;
            DateTime? fecha = null;
            if (dtpFechaFiltro.Checked)
            {
                fecha = dtpFechaFiltro.Value.Date;
            }

            // 2. Llamamos al servicio para que haga la magia
            try
            {
                string historial = service.ObtenerHistorialFormateado(dni, _idMedicoLogueado, fecha);

                // 3. Mostramos el resultado en el TextBox grande
                txtHistorialDetalle.Text = historial; // Asumo txtHistorialDetalle
            }
            catch (Exception ex)
            {
                txtHistorialDetalle.Text = "Error al cargar el historial: " + ex.Message;
            }
        }
    }
}
