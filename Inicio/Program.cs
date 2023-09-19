using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inicio
{
    static class Program
    {
        public static string BoaSorteDir { get; private set; }
        public static string ConcursosDir { get; private set; }
        public static string ResultadosDir { get; private set; }
        public static string DetalhesDoSorteioDir { get; private set; }

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BoaSorteDir = Path.Combine(Environment.CurrentDirectory, "BoaSorte");
            ConcursosDir = Path.Combine(BoaSorteDir, "Concursos");
            ResultadosDir = Path.Combine(BoaSorteDir, "Resultados");
            DetalhesDoSorteioDir = Path.Combine(BoaSorteDir, "DetalhesDoSorteio");
            EnsureDir(BoaSorteDir);
            EnsureDir(ConcursosDir);
            EnsureDir(ResultadosDir);
            EnsureDir(DetalhesDoSorteioDir);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmInicial());
        }

        public static void IntNumber(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        static void EnsureDir(string pathName)
        {
            if (!Directory.Exists(pathName))
                Directory.CreateDirectory(pathName);
        }
    }
}
