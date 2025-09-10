using Sistema_Hospitalario.CapaPresentacion.Administrativo.Hospitalización;
using Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes;
using System;
using System.Windows.Forms;
using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Turnos;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    public partial class MenuAdministrativo : Form
    {
        // ======================= CONSTRUCTOR DEL FORM =======================
        public MenuAdministrativo()
        {
            InitializeComponent();
            this.Text = "Sistema Hospitalario"; // título del formulario
        }

        // ======================= NAVEGACIÓN CENTRAL =======================
        // Método común para mostrar un UserControl en el panel contenedor.
        // Se hace Dispose() del control anterior para evitar:
        // - Fugas de memoria
        // - Suscripciones de eventos “colgando” (handlers duplicados)
        private void AbrirUserControl(UserControl uc)
        {
            foreach (Control c in panelContenedor.Controls) c.Dispose(); // elimina correctamente lo anterior
            panelContenedor.Controls.Clear();

            uc.Dock = DockStyle.Fill;    // ocupar todo el contenedor
            panelContenedor.Controls.Add(uc);
            uc.BringToFront();
        }

        // ======================= HOME =======================
        private void btn_home_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_Home());
        }

        // ======================= PACIENTES =======================
        private void btn_pacientes_Click(object sender, EventArgs e)
        {
            var uc_pacientes = new UC_Pacientes();

            // Patrón “pub-sub”: el UC avisa y el contenedor decide qué hacer.
            // Suscripción a evento: cuando el UC de Pacientes pida registrar, abrimos la vista para registrar.
            uc_pacientes.RegistrarPacienteSolicitado += (_, __) => AbrirRegistrarPaciente();

            // uc_pacientes.ExportarPacientesSolicitado += (_, __) => AbrirExportarPacientes();

            AbrirUserControl(uc_pacientes);
        }

        // Abre el formulario de registrar paciente.
        private void AbrirRegistrarPaciente()
        {
            var ucRegistrar = new UC_RegistrarPaciente();

            // Cuando el UC pida CANCELAR, volvemos a la lista de pacientes.
            ucRegistrar.CancelarRegistroSolicitado += (_, __) => AbrirUserControl(new UC_Pacientes());

            AbrirUserControl(ucRegistrar);
        }

        // ======================= TURNOS =======================
        // Botón del menú: abre SIEMPRE un UC_Turnos “cableado” con sus eventos.
        // Usamos un “factory” para NO olvidar suscribir eventos cada vez que volvemos.
        private void btnTurnos_Click(object sender, EventArgs e)
        {
            AbrirUserControl(CrearUCTurnos());
        }

        // “Factory centralizado” que crea un UC_Turnos y suscribe todos sus eventos.
        // Esto evita el problema de “funciona sólo la primera vez” cuando volvés a la lista.
        private UC_Turnos CrearUCTurnos()
        {
            var ucTurnos = new UC_Turnos();

            // El UC de Turnos avisa que quiere registrar un nuevo turno → abrimos la pantalla de registro
            ucTurnos.RegistrarTurnoSolicitado += (_, __) => AbrirRegistrarTurno();

            // El UC de Turnos avisa que el usuario quiere “ver” un turno → abrimos la pantalla de detalle
            ucTurnos.VerTurnoSolicitado += (_, t) => AbrirVisualizarTurnos(t);

            return ucTurnos;
        }

        // Abre la vista de registrar turno.
        // Importante: al “volver”, volvemos a la lista INSTANCIADA POR EL FACTORY (para no perder eventos).
        private void AbrirRegistrarTurno()
        {
            var ucRegistrar = new Turnos.UC_RegistrarTurno();

            // Cancelar → volvemos a la lista de turnos con el factory (eventos garantizados)
            ucRegistrar.CancelarTurnoSolicitado += (_, __) =>
                AbrirUserControl(CrearUCTurnos());

            AbrirUserControl(ucRegistrar);
        }

        // Abre la vista de visualizar/modificar turno, en base a un TurnoDTO (el objeto seleccionado).
        private void AbrirVisualizarTurnos(TurnoDTO turno)
        {
            var ucVisualizar = new Turnos.UC_VisualizarTurno(turno);

            // Al cancelar visualización → volvemos a la lista con factory
            ucVisualizar.CancelarVisualizacionSolicitada += (_, __) =>
                AbrirUserControl(CrearUCTurnos());

            // También podrías escuchar eventos como “TurnoActualizado” o “TurnoEliminado”
            // para persistir en DB y refrescar la lista al volver.

            AbrirUserControl(ucVisualizar);
        }

        // ======================= HOSPITALIZACIÓN =======================
        private void btnHospitalizacion_Click(object sender, EventArgs e)
        {
            var ucHospitalizacion = new UC_Hospitalizacion();

            // Registrar internación → abrir pantalla de alta de internación
            ucHospitalizacion.RegistrarInternacionSolicitada += (_, __) => AbrirRegistrarInternacion();

            AbrirUserControl(ucHospitalizacion);
        }

        private void AbrirRegistrarInternacion()
        {
            var ucRegistrarInternacion = new UC_RegistrarInternacion();

            // Al cancelar → volver a la lista de hospitalización (aquí no hay factory porque no hay eventos dependientes)
            ucRegistrarInternacion.CancelarInternacionSolicitada += (_, __) =>
                AbrirUserControl(new UC_Hospitalizacion());

            AbrirUserControl(ucRegistrarInternacion);
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
