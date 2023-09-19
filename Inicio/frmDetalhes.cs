using Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inicio
{
    public partial class frmDetalhes : Form
    {
        public frmDetalhes()
        {
            InitializeComponent();
        }
        //Metodo para arrastar o aplicativo
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //Metodo para Movimentar Form
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetalhes_Load(object sender, EventArgs e)
        {
            ReadFile();
            CarregaResultado();
        }
        public static void LerNumero()
        {
            string arquivo = @"C:\BoaSorte\resultadoAtual.txt";

            if (File.Exists(arquivo))
            {
                try
                {
                    string[] listTxt = File.ReadAllLines(arquivo);
                    var strList = listTxt[15];
                    var values = strList.Split();
                    var result = values[1];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private void CarregaResultado()
        {
            var url = @"https://servicebus2.caixa.gov.br/portaldeloterias/api/lotofacil";
            HttpClient lotofacil = new HttpClient();
            var resposta = lotofacil.GetAsync(url).Result;

            if (resposta.IsSuccessStatusCode)
            {
                //int i;
                var listaDezenas = resposta.Content.ReadAsStringAsync().Result;
                var listaDezenass = JsonConvert.DeserializeObject<ResultadoAtual>(listaDezenas);

            }
            if (resposta.IsSuccessStatusCode)
            {
                var numero = resposta.Content.ReadAsStringAsync().Result;
                var sorteio = JsonConvert.DeserializeObject<ResultadoAtual>(numero);

                lblConcurso.Text = sorteio.numero.ToString();
            }
        }
        private void ReadFile()
        {
            var file = new StreamReader(@"C:\BoaSorte\resultadoAtual.txt", Encoding.UTF8);
            string line;

            while ((line = file.ReadLine()) != null)
                contestlist.Items.Add(line);

            file.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            contestlist.Items.Clear();
            //contestlist.Items.AddRange(new DirectoryInfo(@"c:\BoaSorte").EnumerateFiles($"{txtSearch.Text}.txt").Select(f => f.Name).ToArray());           
            //contestlist.Items.AddRange(new DirectoryInfo(@"c:\BoaSorte\Resultados").EnumerateFileSystemInfos($"*{txtSearch.Text}*.txt").Select(f => File.ReadAllText(f.FullName)).ToArray());
            //populateList();
            FileInfo file = new FileInfo(@"c:\BoaSorte\DetalhesDoSorteio\" + txtSearch.Text + ".txt");
            if (txtSearch.Text != "")
            {
                if (file.Exists)
                {
                    contestlist.Items.AddRange(new DirectoryInfo(@"c:\BoaSorte\DetalhesDoSorteio").EnumerateFiles($"{txtSearch.Text}.txt").SelectMany(f => File.ReadAllLines(f.FullName)).ToArray());
                    lblConcurso.Text = $"{txtSearch.Text}".ToString();
                }
                else
                {
                    lblConcurso.Text = "Não Encontrado";
                    MessageBox.Show("Os Dados salvos começam em 2846");
                }
            }
            else
            {
                MessageBox.Show("Digite um Concurso!");
            }
        }

        private void pnTop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
