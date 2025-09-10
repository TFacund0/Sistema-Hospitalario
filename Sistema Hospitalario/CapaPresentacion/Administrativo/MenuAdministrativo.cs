using Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    public partial class MenuAdministrativo : Form
    {
        public MenuAdministrativo()
        {
            InitializeComponent();
            
            this.Text = "Sistema Hospitalario"; // el título del formulario se sigue mostrando
        }

        private void AbrirUserControl(UserControl uc)
        {
            panelContenedor.Controls.Clear();   // Limpia el panel
            uc.Dock = DockStyle.Fill;           // Que ocupe todo el espacio disponible
            panelContenedor.Controls.Add(uc);   // Lo agrega al panel
            uc.BringToFront();                  // Lo trae al frente
        }

        private void btn_pacientes_Click(object sender, EventArgs e)
        {
            var uc_pacientes = new UC_Pacientes();
            
            // Escucha el evento y ejecuta el metodo
            uc_pacientes.RegistrarPacienteSolicitado += (_, __) => AbrirRegistrarPaciente();
            
            //uc_pacientes.ExportarPacientesSolicitado += (_, __) => AbrirExportarPacientes();
            
            AbrirUserControl(uc_pacientes);
        }

        private void AbrirRegistrarPaciente()
        {
            var ucRegistrar = new UC_RegistrarPaciente();

            // Cuando el UC pida cancelar → volver a lista de pacientes
            ucRegistrar.CancelarRegistroSolicitado += (_, __) => AbrirUserControl(new UC_Pacientes());

            AbrirUserControl(ucRegistrar);
        }

        private void AbrirExportarPacientes()
        {
            var ucExportar = new UC_Exportar();
            
            // Cuando el UC pida cancelar → volver a lista de pacientes
            //ucExportar.CancelarExportacionSolicitado += (_, __) => AbrirListaPacientes();
            AbrirUserControl(ucExportar);
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_Home());
        }

        private void btnTurnos_Click(object sender, EventArgs e)
        {
            UC_Turnos ucTurnos = new UC_Turnos();
            // Escucha el evento y ejecuta el metodo
            ucTurnos.RegistrarTurnoSolicitado += (_, __) => AbrirRegistrarTurno();

            AbrirUserControl(ucTurnos);
        }

        private void AbrirRegistrarTurno()
        {
            var ucRegistrar = new Turnos.UC_RegistrarTurno();
            
            // Cuando el UC pida cancelar → volver a lista de turnos
            ucRegistrar.CancelarTurnoSolicitado += (_, __) => AbrirUserControl(new UC_Turnos());

            AbrirUserControl(ucRegistrar);
        }

        private void btnHospitalizacion_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_Hospitalizacion());
        }

        private void btnProcedimientos_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_Procedimientos());
        }

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
