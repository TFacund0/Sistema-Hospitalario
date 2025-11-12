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
        private PacienteListadoMedicoDto Paciente;

        public UC_DetallePaciente(PacienteListadoMedicoDto paciente) // crea el user control con los datos del paciente
        {
            InitializeComponent();
            this.Paciente = paciente; 
            this.TBNombre.Text = paciente.Nombre + " " + paciente.Apellido;
            this.txtDni.Text = paciente.Dni.ToString();
            this.TBDireccion.Text = paciente.Direccion;
            int diasNacimiento = ((DateTime.Now - paciente.FechaNacim).Days);
            if (diasNacimiento < 365) this.TBEdad.Text = (diasNacimiento / 30).ToString() + " " + "meses";
            else this.TBEdad.Text = (diasNacimiento / 365).ToString() + " " + "años";
            this.TBContacton.Text = paciente.Telefono.ToString();
            this.TBHabitacion.Text = paciente.Habitacion.ToString();
            this.TBEstado.Text = paciente.Estado;
            CargarHistorial();
        }
        private void CargarHistorial()
        {
            // 1. Recolectamos los filtros
            int idPaciente = this.Paciente.IdPaciente;
            int _idMedicoLogueado = (int)SesionUsuario.IdMedicoAsociado;
            
            try
            {
                var listaHistorial = service.ObtenerHistorial(idPaciente, _idMedicoLogueado);
                txtHistorialDetalle.Clear();
                // 3. Mostramos el resultado en el TextBox grande
                if (!listaHistorial.Any())
                {
                    AppendHeader("--- No se encontraron registros para este paciente o filtros seleccionados ---");
                    return;
                }

                // 4. Recorremos la lista y aplicamos los estilos
                foreach (var item in listaHistorial)
                {
                    // Título (ej: CONSULTA - 12/10/2025 09:30 hs.)
                    AppendHeader($"{item.Tipo.ToUpper()} - {item.Fecha.ToString("dd/MM/yyyy HH:mm")} hs.");

                    // Médico
                    AppendLabel("Médico");
                    AppendContent($"{item.NombreMedico} (DNI: {item.DniMedico})");

                    // Motivo
                    AppendLabel("Motivo/Obs");
                    AppendContent(item.Motivo);

                    // Diagnóstico
                    AppendLabel("Diagnóstico/Proced");
                    AppendContent(item.Diagnostico);

                    // Tratamiento
                    AppendLabel("Tratamiento");
                    AppendContent(item.Tratamiento);

                    // Separador
                    AppendSeparator();
                }
            }
            catch (Exception ex)
            {
                txtHistorialDetalle.Text = "Error al cargar el historial: " + ex.Message;
            }
        }
        private void AppendTextWithStyle(string text, Font font, Color color, HorizontalAlignment alignment)
        {
            txtHistorialDetalle.SelectionStart = txtHistorialDetalle.TextLength;
            txtHistorialDetalle.SelectionLength = 0;

            // ¡NUEVO! Aplicamos la alineación
            txtHistorialDetalle.SelectionAlignment = alignment;

            txtHistorialDetalle.SelectionFont = font;
            txtHistorialDetalle.SelectionColor = color;

            txtHistorialDetalle.AppendText(text);
        }

        /// <summary>
        /// Escribe un TÍTULO (ej: CONSULTA - FECHA)
        /// </summary>
        private void AppendHeader(string text)
        {
            Font headerFont = new Font("Segoe UI", 16, FontStyle.Bold);
            // ¡NUEVO! Le pasamos 'HorizontalAlignment.Center'
            AppendTextWithStyle(text + "\n", headerFont, Color.FromArgb(0, 90, 150), HorizontalAlignment.Center);
        }

        private void AppendLabel(string text)
        {
            Font labelFont = new Font("Segoe UI", 14, FontStyle.Bold);
            // ¡NUEVO! Le pasamos 'HorizontalAlignment.Left'
            AppendTextWithStyle(text + ": ", labelFont, Color.Black, HorizontalAlignment.Left);
        }


        private void AppendContent(string text)
        {
            Font contentFont = new Font("Segoe UI", 15, FontStyle.Regular);
            AppendTextWithStyle(text + "\n", contentFont, Color.FromArgb(64, 64, 64), HorizontalAlignment.Left);
        }


        private void AppendSeparator()
        {
            Font separatorFont = new Font("Segoe UI", 14, FontStyle.Regular);
            // ¡NUEVO! Le pasamos 'HorizontalAlignment.Center'
            AppendTextWithStyle("==================================================================\n\n", separatorFont, Color.LightGray, HorizontalAlignment.Center);
        }

    }
}
