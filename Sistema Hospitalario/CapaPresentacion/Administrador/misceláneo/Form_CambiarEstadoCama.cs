using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrador.misceláneo
{
    public partial class Form_CambiarEstadoCama : Form
    {
        public int NuevoEstadoIdSeleccionado { get; private set; }

        private string _estadoActual; 

        // 1. Modificamos el "Constructor" para que acepte el estado actual
        public Form_CambiarEstadoCama(string estadoActual)
        {
            InitializeComponent();
            _estadoActual = estadoActual;
        }

        // 2. Hacemos doble clic en el evento "Load" del formulario
        private void Form_CambiarEstadoCama_Load(object sender, EventArgs e)
        {
            CargarComboBox();
        }

        private void CargarComboBox()
        {
            // Usamos la misma lógica que ya conocés para cargar el combo
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                var estados = db.estado_cama
                                .Select(e => new {
                                    Id = e.id_estado_cama,
                                    Nombre = e.disponibilidad
                                })
                                .ToList();

                comboBox1.DataSource = estados;
                comboBox1.DisplayMember = "Nombre";
                comboBox1.ValueMember = "Id";

                // ¡Importante! Dejamos seleccionado el estado actual
                comboBox1.Text = _estadoActual;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            this.NuevoEstadoIdSeleccionado = (int)comboBox1.SelectedValue;

            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var valorSeleccionado = (int)comboBox1.SelectedValue;

            if (valorSeleccionado == 0) // '0' es el ID que le dimos a "ninguna"
            {
                MessageBox.Show("Por favor, seleccione un estado válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detenemos el guardado
            }

            // 2. Guardamos el ID seleccionado en nuestra propiedad pública
            this.NuevoEstadoIdSeleccionado = valorSeleccionado;

            // 3. ¡IMPORTANTE! Asignamos el DialogResult manualmente
            this.DialogResult = DialogResult.OK;

            // 4. Cerramos el formulario
            this.Close();
        }
    }
}
