using Sistema_Hospitalario.CapaPresentacion.Administrativo.Hospitalización;
using Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes;
using System;
using System.Windows.Forms;

using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Turnos;
using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Pacientes;
using Sistema_Hospitalario.CapaNegocio.DTOs;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    public partial class MenuAdministrativo : Form
    {
        // ======================= CONSTRUCTOR DEL FORM =======================
        public MenuAdministrativo()
        {
            InitializeComponent();
            this.Text = "Sistema Hospitalario";
        }

        // ======================= NAVEGACIÓN CENTRAL =======================
        // Método común para mostrar un UserControl en el panel contenedor.
        private void AbrirUserControl(UserControl uc)
        {
            foreach (Control c in panelContenedor.Controls) c.Dispose(); // elimina el panel anterior
            panelContenedor.Controls.Clear();

            uc.Dock = DockStyle.Fill;    // ocupar todo el contenedor
            panelContenedor.Controls.Add(uc);
            uc.BringToFront();
        }

        // ======================= HOME =======================
        private void btn_home_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_HomeGerente());
        }

        // ======================= PACIENTES =======================
        private void btn_pacientes_Click(object sender, EventArgs e)
        {
            AbrirUserControl(CrearUCPaciente());
        }

        private UC_Pacientes CrearUCPaciente()
        {
            var ucPacientes = new UC_Pacientes();

            ucPacientes.RegistrarPacienteSolicitado += (_, __) => AbrirRegistrarPaciente();
            ucPacientes.VerPacienteSolicitado += (_, p) => AbrirVisualizarPaciente(p);

            return ucPacientes;
        }

        private void AbrirRegistrarPaciente()
        {
            var ucRegistrar = new UC_RegistrarPaciente();

            ucRegistrar.CancelarRegistroSolicitado += (_, __) => AbrirUserControl(CrearUCPaciente());

            AbrirUserControl(ucRegistrar);
        }

        private void AbrirVisualizarPaciente(PacienteDetalleDto pacienteDetalle)
        {
            var ucVisualizar = new UC_VisualizarPaciente(pacienteDetalle);

            ucVisualizar.CancelarVisualizacionSolicitada += (_, __) =>
                AbrirUserControl(CrearUCPaciente());

            AbrirUserControl(ucVisualizar);
        }

        // ======================= TURNOS =======================
        private void btnTurnos_Click(object sender, EventArgs e)
        {
            AbrirUserControl(CrearUCTurnos());
        }

        private UC_Turnos CrearUCTurnos()
        {
            var ucTurnos = new UC_Turnos();

            ucTurnos.RegistrarTurnoSolicitado += (_, __) => AbrirRegistrarTurno();
            ucTurnos.VerTurnoSolicitado += (_, t) => AbrirVisualizarTurnos(t);

            return ucTurnos;
        }

        private void AbrirRegistrarTurno()
        {
            var ucRegistrar = new Turnos.UC_RegistrarTurno();

            ucRegistrar.CancelarTurnoSolicitado += (_, __) => AbrirUserControl(CrearUCTurnos());

            AbrirUserControl(ucRegistrar);
        }

        private void AbrirVisualizarTurnos(TurnoDTO turno)
        {
            var ucVisualizar = new Turnos.UC_VisualizarTurno(turno);

            ucVisualizar.CancelarVisualizacionSolicitada += (_, __) => AbrirUserControl(CrearUCTurnos());

            AbrirUserControl(ucVisualizar);
        }

        // ======================= HOSPITALIZACIÓN =======================
        private void btnHospitalizacion_Click(object sender, EventArgs e)
        {
            AbrirUserControl(CrearUCHospitalizacion());
        }

        private UC_Hospitalizacion CrearUCHospitalizacion()
        {
            var uc = new UC_Hospitalizacion();
            uc.RegistrarInternacionSolicitada += (_, __) => AbrirRegistrarInternacion();
            return uc;
        }

        private void AbrirRegistrarInternacion()
        {
            var ucRegistrar = new UC_RegistrarInternacion();

            ucRegistrar.CancelarInternacionSolicitada += (_, __) =>
                AbrirUserControl(CrearUCHospitalizacion());

            AbrirUserControl(ucRegistrar);
        }

        // ======================= PROCEDIMIENTOS =======================
        private void btnProcedimientos_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_Procedimientos());
        }

        // ======================= SALIR =======================
        private void btn_salir_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Seguro que desea salir?",
                                              "Confirmación",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
