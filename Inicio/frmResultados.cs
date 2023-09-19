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
    public partial class frmResultados : Form
    {
        private Label[] UltimoResultado = new Label[15];
        public frmResultados()
        {
            InitializeComponent();
            GeraControles();
        }
        //Metodo para arrastar o aplicativo
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void GeraControles()
        {
            int i, esquerda = 30, topo = 90;

            for (i = 0; i < 15; i++)
            {
                UltimoResultado[i] = new Label();
                //UltimoResultado[i].Name = "label" + i;
                //UltimoResultado[i].Text = ResultadoLabel[i].Name;
                //UltimoResultado[i].Anchor = AnchorStyles.Right;
                //UltimoResultado[i].Location = new Point(UltimoResultado[i].Location.X + 20, UltimoResultado[i].Location.Y + 300);
                UltimoResultado[i].Left = esquerda;
                UltimoResultado[i].Top = topo;
                UltimoResultado[i].Anchor = new AnchorStyles();
                //UltimoResultado[i].BackColor = Color.Gray;
                UltimoResultado[i].ForeColor = Color.Silver;
                UltimoResultado[i].Size = new Size(40, 30);
                UltimoResultado[i].Font = new Font("", 14, FontStyle.Bold);
                //UltimoResultado[i].TextAlign = ContentAlignment.BottomCenter;
                UltimoResultado[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(UltimoResultado[i]);
                esquerda += (UltimoResultado[i].Width + 5);

            }

        }
        private void CarregaResultado()
        {
            var url = @"https://servicebus2.caixa.gov.br/portaldeloterias/api/lotofacil";
            HttpClient lotofacil = new HttpClient();
            var resposta = lotofacil.GetAsync(url).Result;

            if (resposta.IsSuccessStatusCode)
            {
                var listaDezenas = resposta.Content.ReadAsStringAsync().Result;
                var listaDezenass = JsonConvert.DeserializeObject<ResultadoAtual>(listaDezenas);

                Control[] controles = UltimoResultado;

                for (int i = 0; i < controles.Length; i++)
                {
                    int valor = Convert.ToInt32(listaDezenass.listaDezenas[i]);
                    controles[i].Text = valor.ToString("D2");
                }
                string[] lines = File.ReadAllLines(Path.Combine(Program.BoaSorteDir, "Aposta.txt"));
                string[] line, contents;
                int count;
                //result = UltimoResultado[i].Text.Split(); //if I change the UltimoRsultado by a TextBox.Text and type the result it works just fine
                for (int i = 0; i < lines.Length; i++)
                {
                    contents = lines[i].Split(' ');
                    count = contents.Intersect(listaDezenass.listaDezenas).Count();
                    line = new string[16];
                    for (int j = 0; j < 15; j++)
                        line[j] = contents[j];
                    line[15] = count.ToString();
                    dgHits.Rows.Add(line);
                }
            }
            if (resposta.IsSuccessStatusCode)
            {
                var numero = resposta.Content.ReadAsStringAsync().Result;
                var sorteio = JsonConvert.DeserializeObject<ResultadoAtual>(numero);

                txtConcurso.Text = sorteio.numero.ToString();
                lblData.Text = sorteio.dataApuracao.ToString();
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmResultados_Load(object sender, EventArgs e)
        {
            CarregaResultado();
        }
    }
}
