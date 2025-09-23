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

        private PacienteDTO paciente;
        public UC_DetallePaciente(PacienteDTO paciente)
        {
            InitializeComponent();
            this.paciente = paciente;
            this.TBNombre.Text = paciente.nombre + " " + paciente.apellido;
            this.TBDNI.Text = paciente.dni.ToString();
            this.TBDireccion.Text = paciente.direccion;
            this.TBObraSocial.Text = paciente.obraSocial;
            int diasNacimiento = ((DateTime.Now - paciente.FechaNacimiento).Days);
            if (diasNacimiento < 365) this.TBEdad.Text = (diasNacimiento / 30).ToString() + " " + "meses";
            else this.TBEdad.Text = (diasNacimiento / 365).ToString() + " " + "años";
            this.TBContacton.Text = paciente.telefono.ToString();
            this.TBHabitacion.Text = paciente.habitacion.ToString();
            this.TBAfiliado.Text = paciente.nroAfiliado.ToString();
            this.TBEstado.Text = paciente.Estado;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {
                
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuMedicos menu = this.FindForm() as MenuMedicos;
            menu.AbrirUserControl(new ConsultaMedica());
        }
    }
}
