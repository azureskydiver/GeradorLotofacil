using Controle;
using Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inicio
{
    public partial class frmInicial : Form
    {
        public Resultado _Resultado { get; set; }
        public string diretorio; // parte da procedimento Detalhes
        private Dictionary<int, int> _resultadoNumeros;// tras a quantidade de numeros sorteados detalhados
        private Label[] ResultadoAtual = new Label[15]; // gera um array de 15 inces
        public frmInicial()
        {
            InitializeComponent();
            GeraControles(); //Imprime o array ResultadoAtual na tela
            CarregaResultado();
            Logica.CriarBancoSQLite();
            Logica.CriarTabelaSQLite();
            //AtualizarInformacoesDoGrid();
        }
        //Metodo para arrastar o aplicativo
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //Metodo para Movimentar Form
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void GeraControles()//gera os controles do array ResultadoAtual
        {
            int i, esquerda = 175, topo = 55;

            for (i = 0; i < 15; i++)
            {
                ResultadoAtual[i] = new Label();
                //ResultadoAtual[i].Name = "label" + i;
                //ResultadoAtual[i].Text = ResultadoLabel[i].Name;
                //ResultadoAtual[i].Anchor = AnchorStyles.Right;
                //ResultadoAtual[i].Location = new Point(ResultadoAtual[i].Location.X + 20, ResultadoAtual[i].Location.Y + 300);
                ResultadoAtual[i].Left = esquerda;
                ResultadoAtual[i].Top = topo;
                ResultadoAtual[i].Anchor = new AnchorStyles();
                //UltimoResultado[i].BackColor = Color.Gray;
                ResultadoAtual[i].ForeColor = Color.Silver;
                ResultadoAtual[i].Size = new Size(45, 30);
                ResultadoAtual[i].Font = new Font("", 15, FontStyle.Bold);
                ResultadoAtual[i].TextAlign = ContentAlignment.BottomCenter;
                ResultadoAtual[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(ResultadoAtual[i]);
                esquerda += (ResultadoAtual[i].Width + 5);
            }
        }
        //private void ExibirDados()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt = Logica.GetResults();
        //        dgResultadoGeral.DataSource = dt;
        //        dt.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR: " + ex.Message);
        //    }

        //}
        private void Form1_Load(object sender, EventArgs e)
        {            
            Detalhes();
            //SalvaResultList();
            AtualizarInformacoesDoGrid();
            txtDataHoje.Text = DateTime.Now.ToShortDateString();
        }
        internal Dictionary<int, int> RresultadoNumeros
        {
            get
            {
                if (_resultadoNumeros == null)
                {
                    int outVal = 0;
                    _resultadoNumeros = dgResultadoGeral.Rows.Cast<DataGridViewRow>()
                        .SelectMany(r => r.Cells.Cast<DataGridViewCell>()
                        .Where(c => c.ColumnIndex > 0 && int.TryParse(c.Value?.ToString(), out outVal)))
                        .GroupBy(c => int.Parse(c.Value.ToString()))
                        .ToDictionary(x => x.Key, x => x.Count());
                }

                return _resultadoNumeros;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Load the data and populate the DataGridView then call...
            DisplayResultado();
        }
        private void DisplayResultado()
        {
            foreach (var lbl in GetChildren(this).OfType<Label>())
            {
                int outKey;

                if (int.TryParse(lbl.Tag?.ToString(), out outKey) &&
                    RresultadoNumeros.ContainsKey(outKey))
                {
                    lbl.Text = RresultadoNumeros[outKey].ToString();
                }
            }
        }

        private IEnumerable<Control> GetChildren(Control parent)
        {
            var controls = parent.Controls.Cast<Control>();
            return controls.SelectMany(c => GetChildren(c)).Concat(controls);
        }
        private void WhereYouCreateAndShowForm1()
        {
            using (var frm = new frmCriarAposta(RresultadoNumeros))
            {
                frm.ShowDialog();
                // ...
            }
        }
        // parte do salva o arquivo com o nome da variavel desejada
        internal static class Utils
        {
            internal static string GetUniqueFile(
            string dir,
            string fileName,
            string suffix,
            int seed,
            string extension)
            {
                string outFile;

                while (true)
                {
                    outFile = Path
                        .ChangeExtension(Path
                        .Combine(dir, $"{fileName}{suffix}{seed}"), extension);
                    if (!File.Exists(outFile)) break;
                    return seed.ToString();
                }

                return outFile;
            }
        }
        //salva o arquivo com o nome da variavel desejada
        private void Detalhes()
        {
            //int i = 0;
            ResultadoAtual resultado = new ResultadoAtual();
            resultado.numero = Convert.ToInt32(txtConcurso.Text);
            var lastSorteio = new List<string>();
            var siteLotofacil = @"https://servicebus2.caixa.gov.br/portaldeloterias/api/lotofacil";
            var file = Path.Combine(@"c:\BoaSorte\Concursos", $"{resultado.numero}.txt"); // aqui ele salva com o numero do sorteio        

            var requisition = (HttpWebRequest)WebRequest.Create(siteLotofacil);
            requisition.MaximumAutomaticRedirections = 1;
            requisition.AllowAutoRedirect = true;
            requisition.CookieContainer = new CookieContainer();

            using (var response = (HttpWebResponse)requisition.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                responseStream.CopyTo(fileStream);
            }
            var requisicao = (HttpWebRequest)WebRequest.Create(siteLotofacil);// salva o resultado geral em txt
            requisicao.MaximumAutomaticRedirections = 2;
            requisicao.AllowAutoRedirect = true;
            requisicao.CookieContainer = new CookieContainer();
            var resposta = (HttpWebResponse)requisicao.GetResponse();
            using (var responseStream = resposta.GetResponseStream())
            {
                using (var fileStream = new FileStream(Path.Combine(diretorio + "C:\\BoaSorte\\resultadoAtual.txt"), FileMode.Create))
                {
                    responseStream.CopyTo(fileStream);
                }
            }
        }
        //private void SalvaResultList()// salva lista de numeros do sorteio (Resultado)
        //{
        //    // salva ultimo sorteio no arquivo texto na primeira linha e vai acrescentando linhas
        //    const string nomeArquivo = @"C:\BoaSorte\Resultados\ResultadosConcurso.txt";
        //    List<string> linhas = File.ReadLines(nomeArquivo).ToList(); // Passo 1

        //    if (linhas.IndexOf(txtConcurso.Text + "," + ResultadoAtual[0].Text + "," + ResultadoAtual[1].Text + "," + ResultadoAtual[2].Text + "," + ResultadoAtual[3].Text + "," + ResultadoAtual[4].Text
        //       + "," + ResultadoAtual[5].Text + "," + ResultadoAtual[6].Text + "," + ResultadoAtual[7].Text + "," + ResultadoAtual[8].Text + "," + ResultadoAtual[9].Text
        //       + "," + ResultadoAtual[10].Text + "," + ResultadoAtual[11].Text + "," + ResultadoAtual[12].Text + "," + ResultadoAtual[13].Text + "," + ResultadoAtual[14].Text) >= 0)
        //    { }
        //    else
        //    {
        //        linhas.Insert(0, txtConcurso.Text + "," + ResultadoAtual[0].Text + "," + ResultadoAtual[1].Text + "," + ResultadoAtual[2].Text + "," + ResultadoAtual[3].Text + "," + ResultadoAtual[4].Text
        //     + "," + ResultadoAtual[5].Text + "," + ResultadoAtual[6].Text + "," + ResultadoAtual[7].Text + "," + ResultadoAtual[8].Text + "," + ResultadoAtual[9].Text
        //     + "," + ResultadoAtual[10].Text + "," + ResultadoAtual[11].Text + "," + ResultadoAtual[12].Text + "," + ResultadoAtual[13].Text + "," + ResultadoAtual[14].Text); // Passo 2
        //        File.WriteAllLines(nomeArquivo, linhas);
        //        //MessageBox.Show("Gravndo!");
        //    }
        //}
        private void CarregaResultado()
        {
            var url = @"https://servicebus2.caixa.gov.br/portaldeloterias/api/lotofacil";
            HttpClient lotofacil = new HttpClient();
            var resposta = lotofacil.GetAsync(url).Result;

            if (resposta.IsSuccessStatusCode)
            {
                var listaDezenas = resposta.Content.ReadAsStringAsync().Result;
                var listaDezenass = JsonConvert.DeserializeObject<ResultadoAtual>(listaDezenas);

                Control[] controles = ResultadoAtual;

                for (int i = 0; i < controles.Length; i++)
                {
                    int valor = Convert.ToInt32(listaDezenass.listaDezenas[i]);
                    controles[i].Text = valor.ToString("D2");
                }      
            }
            if(resposta.IsSuccessStatusCode)
            {
                var numero = resposta.Content.ReadAsStringAsync().Result;
                var sorteio = JsonConvert.DeserializeObject<ResultadoAtual>(numero);

                txtConcurso.Text = sorteio.numero.ToString();
                lblData.Text = sorteio.dataApuracao.ToString();
                txtDataAnterior.Text = sorteio.numeroConcursoAnterior.ToString();
                txtDataAtual.Text = sorteio.numero.ToString();
                txtDataProximo.Text = sorteio.numeroConcursoProximo.ToString();
                txtProximoConcurso.Text = sorteio.dataProximoConcurso.ToString();
            }
        }
       
        private void AtualizarInformacoesDoGrid()
        {
            dgResultadoGeral.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgResultadoGeral.DataSource = Logica.GetResultados();
            if (dgResultadoGeral.Rows.Count != 0)
            {
                dgResultadoGeral.Columns[0].HeaderText = "Concurso";
                dgResultadoGeral.Columns[0].Width = 97;
                dgResultadoGeral.Columns[1].HeaderText = "01";
                dgResultadoGeral.Columns[1].Width = 50;
                dgResultadoGeral.Columns[2].HeaderText = "02";
                dgResultadoGeral.Columns[2].Width = 50;
                dgResultadoGeral.Columns[3].HeaderText = "03";
                dgResultadoGeral.Columns[3].Width = 50;
                dgResultadoGeral.Columns[4].HeaderText = "04";
                dgResultadoGeral.Columns[4].Width = 50;
                dgResultadoGeral.Columns[5].HeaderText = "05";
                dgResultadoGeral.Columns[5].Width = 50;
                dgResultadoGeral.Columns[6].HeaderText = "06";
                dgResultadoGeral.Columns[6].Width = 50;
                dgResultadoGeral.Columns[7].HeaderText = "07";
                dgResultadoGeral.Columns[7].Width = 50;
                dgResultadoGeral.Columns[8].HeaderText = "08";
                dgResultadoGeral.Columns[8].Width = 50;
                dgResultadoGeral.Columns[9].HeaderText = "09";
                dgResultadoGeral.Columns[9].Width = 50;
                dgResultadoGeral.Columns[10].HeaderText = "10";
                dgResultadoGeral.Columns[10].Width = 50;
                dgResultadoGeral.Columns[11].HeaderText = "11";
                dgResultadoGeral.Columns[11].Width = 50;
                dgResultadoGeral.Columns[12].HeaderText = "12";
                dgResultadoGeral.Columns[12].Width = 50;
                dgResultadoGeral.Columns[13].HeaderText = "13";
                dgResultadoGeral.Columns[13].Width = 50;
                dgResultadoGeral.Columns[14].HeaderText = "14";
                dgResultadoGeral.Columns[14].Width = 50;
                dgResultadoGeral.Columns[15].HeaderText = "15";
                dgResultadoGeral.Columns[15].Width = 50;
            }
        }

      

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Fechar a aplicação?", "warning",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)

                Application.Exit();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {

        }

        private void btnNormalizar_Click(object sender, EventArgs e)
        {

        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvarConcurso_Click(object sender, EventArgs e)
        {
            salvar();
            AtualizarInformacoesDoGrid(); 
        }
        private Resultado PreencherInformacoes()
        {
            Resultado SorteioBase = new Resultado();

            SorteioBase.Concurso = Convert.ToInt32(txtConcurso.Text);
            SorteioBase._01 = Convert.ToInt32(ResultadoAtual[0].Text);
            SorteioBase._02 = Convert.ToInt32(ResultadoAtual[1].Text);
            SorteioBase._03 = Convert.ToInt32(ResultadoAtual[2].Text);
            SorteioBase._04 = Convert.ToInt32(ResultadoAtual[3].Text);
            SorteioBase._05 = Convert.ToInt32(ResultadoAtual[4].Text);
            SorteioBase._06 = Convert.ToInt32(ResultadoAtual[5].Text);
            SorteioBase._07 = Convert.ToInt32(ResultadoAtual[6].Text);
            SorteioBase._08 = Convert.ToInt32(ResultadoAtual[7].Text);
            SorteioBase._09 = Convert.ToInt32(ResultadoAtual[8].Text);
            SorteioBase._10 = Convert.ToInt32(ResultadoAtual[9].Text);
            SorteioBase._11 = Convert.ToInt32(ResultadoAtual[10].Text);
            SorteioBase._12 = Convert.ToInt32(ResultadoAtual[11].Text);
            SorteioBase._13 = Convert.ToInt32(ResultadoAtual[12].Text);
            SorteioBase._14 = Convert.ToInt32(ResultadoAtual[13].Text);
            SorteioBase._15 = Convert.ToInt32(ResultadoAtual[14].Text);

            //SorteioBase.id = IDCarregado;
            return SorteioBase;
        }
        private void salvar()
        {
            if (!string.IsNullOrWhiteSpace(txtConcurso.Text))
            {
                string Saida = Logica.Add(PreencherInformacoes());
                MessageBox.Show(Saida, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Um novo Resultado Foi salvo no Banco!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnCriarJogo_Click(object sender, EventArgs e)
        {
            WhereYouCreateAndShowForm1();
        }

        private void btnConferir_Click(object sender, EventArgs e)
        {
            frmResultados SResult = new frmResultados();
            SResult.ShowDialog();
        }

        private void btnSalvarConcursoAtrazado_Click(object sender, EventArgs e)
        {
            frmSalvaResultados SResult = new frmSalvaResultados();
            SResult.ShowDialog();
        }

        private void btnDetalhes_Click(object sender, EventArgs e)
        {
            frmDetalhes fm = new frmDetalhes();
            fm.ShowDialog();
        }

        private void txtPesquiza_TextChanged(object sender, EventArgs e)
        {
            dgResultadoGeral.DataSource = Logica.GetResultados(txtPesquiza.Text);
        }

        private void pnTop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label30_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void dgResultadoGeral_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgResultadoGeral.Columns[e.ColumnIndex].Name == "Concurso")
            {
                if (e.Value != null && (string)e.Value != "")
                {
                    // dgResultadoGeral.ClearSelection();
                    //DataGridViewRow row = dgReasultado.Rows[e.RowIndex];
                    ////row.DefaultCellStyle.BackColor = Color.Yellow;
                    //e.CellStyle.ForeColor = Color.Green;                    
                    this.dgResultadoGeral.DefaultCellStyle.Font = new Font("Tahoma", 12);
                    e.CellStyle.ForeColor = Color.Red;
                    dgResultadoGeral.ColumnHeadersDefaultCellStyle.Font = new Font(dgResultadoGeral.Font.Name, dgResultadoGeral.Font.Size + 1, FontStyle.Bold);
                    dgResultadoGeral.Columns[0].DefaultCellStyle.Font = new Font(dgResultadoGeral.DefaultCellStyle.Font, FontStyle.Bold);
                }
                else
                {
                    //e.CellStyle.ForeColor = Color.Red;
                }
                // this.dgReasultado.DefaultCellStyle.ForeColor = Color.Blue;
            }

            if (e.Value != null && (string)e.Value == "01")
            {
                e.CellStyle.BackColor = Color.Olive;
            }
            else if (e.Value != null && (string)e.Value == "02")
            {
                e.CellStyle.BackColor = Color.YellowGreen;
            }
            else if (e.Value != null && (string)e.Value == "03")
            {
                e.CellStyle.BackColor = Color.DarkSeaGreen;
            }
            else if (e.Value != null && (string)e.Value == "04")
            {
                e.CellStyle.BackColor = Color.LightSeaGreen;
            }
            else if (e.Value != null && (string)e.Value == "05")
            {
                e.CellStyle.BackColor = Color.DarkTurquoise;
            }
            else if (e.Value != null && (string)e.Value == "06")
            {
                e.CellStyle.BackColor = Color.Aqua;
            }
            else if (e.Value != null && (string)e.Value == "07")
            {
                e.CellStyle.BackColor = Color.PowderBlue;
            }
            else if (e.Value != null && (string)e.Value == "08")
            {
                e.CellStyle.BackColor = Color.SkyBlue;
            }
            else if (e.Value != null && (string)e.Value == "09")
            {
                e.CellStyle.BackColor = Color.SteelBlue;
            }
            else if (e.Value != null && (string)e.Value == "10")
            {
                e.CellStyle.BackColor = Color.CornflowerBlue;
            }
            else if (e.Value != null && (string)e.Value == "11")
            {
                e.CellStyle.BackColor = Color.SlateBlue;
            }
            else if (e.Value != null && (string)e.Value == "12")
            {
                e.CellStyle.BackColor = Color.MediumPurple;
            }
            else if (e.Value != null && (string)e.Value == "13")
            {
                e.CellStyle.BackColor = Color.Plum;
            }
            else if (e.Value != null && (string)e.Value == "14")
            {
                e.CellStyle.BackColor = Color.Orchid;
            }
            else if (e.Value != null && (string)e.Value == "15")
            {
                e.CellStyle.BackColor = Color.PaleVioletRed;
            }
            else if (e.Value != null && (string)e.Value == "16")
            {
                e.CellStyle.BackColor = Color.Khaki;
            }
            else if (e.Value != null && (string)e.Value == "17")
            {
                e.CellStyle.BackColor = Color.Gold;
            }
            else if (e.Value != null && (string)e.Value == "18")
            {
                e.CellStyle.BackColor = Color.Cornsilk;
            }
            else if (e.Value != null && (string)e.Value == "19")
            {
                e.CellStyle.BackColor = Color.Peru;
            }
            else if (e.Value != null && (string)e.Value == "20")
            {
                e.CellStyle.BackColor = Color.Tan;
            }
            else if (e.Value != null && (string)e.Value == "21")
            {
                e.CellStyle.BackColor = Color.DarkKhaki;
            }
            else if (e.Value != null && (string)e.Value == "22")
            {
                e.CellStyle.BackColor = Color.Olive;
            }
            else if (e.Value != null && (string)e.Value == "23")
            {
                e.CellStyle.BackColor = Color.DarkSalmon;
            }
            else if (e.Value != null && (string)e.Value == "24")
            {
                e.CellStyle.BackColor = Color.MistyRose;
            }
            else if (e.Value != null && (string)e.Value == "25")
            {
                e.CellStyle.BackColor = Color.Sienna;
            }
        }

        private void txtPesquiza_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.IntNumber(e);
        }
    }
}
