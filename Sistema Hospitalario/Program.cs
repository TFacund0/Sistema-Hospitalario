using Sistema_Hospitalario.CapaPresentacion;
using Sistema_Hospitalario.CapaPresentacion.Gerente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsInicio_de_sesion;

namespace Sistema_Hospitalario

{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += (s, exArgs) =>
            {
                MessageBox.Show($"ThreadException:\n{exArgs.Exception}", "Error global",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (s, exArgs) =>
            {
                MessageBox.Show($"UnhandledException:\n{exArgs.ExceptionObject}", "Error global dominio",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            Application.Run(new Login());
        }
    }
}
