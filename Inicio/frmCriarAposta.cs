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
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inicio
{
    public partial class frmCriarAposta : Form
    {
        public string diretorio;
        private Label[] EntradaLabel = new Label[5];
        private Label[] EntradaLabel2 = new Label[5];
        private Label[] EntradaText = new Label[5];
        private Label[] Removido = new Label[3];
        private Label[] ResultadoLabel = new Label[15];
        private Label[] UltimoResultado = new Label[15];
        public frmCriarAposta()
        {
            InitializeComponent();
            GeraControles();
        }
        ////Aqui começa o resgate dos numeros mais sorteados----------------------------------------------
        public frmCriarAposta(Dictionary<int, int> dict) : this()
        {
            DisplayResultado(dict);
        }
        private void DisplayResultado(Dictionary<int, int> dict)
        {
            foreach (var lbl in gbControleApostas.Controls.OfType<Label>())
            {
                int outKey;

                if (int.TryParse(lbl.Tag?.ToString(), out outKey) &&
                    dict.ContainsKey(outKey))
                {
                    lbl.Text = dict[outKey].ToString();
                }
            }
        }
        //Metodo para arrastar o aplicativo
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //Metodo para Movimentar Form
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void GeraControles()
        {
            int i, esquerda = 575, topo = 155;

            for (i = 0; i < 5; i++)
            {
                EntradaLabel[i] = new Label();
                //EntradaLabel[i].Enabled = false;
                //EntradaLabel[i].Name = "" + i;
                //EntradaLabel[i].Text = EntradaLabel[i].Name;                
                EntradaLabel[i].Left = esquerda; //Defina a posição do controle no formulário
                EntradaLabel[i].Top = topo;
                EntradaLabel[i].Anchor = new AnchorStyles();
                EntradaLabel[i].Size = new Size(40, 25);
                EntradaLabel[i].Font = new Font("", 15, FontStyle.Bold);
                EntradaLabel[i].BorderStyle = BorderStyle.Fixed3D;
                this.Controls.Add(EntradaLabel[i]); // Adicione os controle ao formulário
                esquerda += (EntradaLabel[i].Width + 5);
            }
            esquerda = 800;
            topo = 155;
            for (i = 0; i < 5; i++)
            {
                EntradaLabel2[i] = new Label();
                //EntradaLabel2[i].Name = "" + i;
                //EntradaLabel2[i].Text = EntradaLabel2[i].Name;                
                EntradaLabel2[i].Left = esquerda; //Defina a posição do controle no formulário
                EntradaLabel2[i].Top = topo;
                EntradaLabel2[i].Anchor = new AnchorStyles();
                EntradaLabel2[i].Size = new Size(40, 25);
                EntradaLabel2[i].Font = new Font("", 15, FontStyle.Bold);
                //EntradaLabel2[i].BorderStyle = BorderStyle.FixedSingle;
                EntradaLabel2[i].BorderStyle = BorderStyle.Fixed3D;
                this.Controls.Add(EntradaLabel2[i]); // Adicione os controle ao formulário
                esquerda += (EntradaLabel2[i].Width + 5);
            }
            esquerda = 40;
            topo = 240;
            for (i = 0; i < 5; i++)
            {
                EntradaText[i] = new Label();
                //EntradaText[i].Enabled = false;
                //EntradaText[i].Name = " " + i;
                //EntradaText[i].Text = EntradaText[i].Name;
                EntradaText[i].Left = esquerda;
                EntradaText[i].Top = topo;
                EntradaText[i].Anchor = new AnchorStyles();
                //EntradaText[i].Leave += new EventHandler(TextBox.Leave);
                EntradaText[i].Size = new Size(40, 25);
                EntradaText[i].Font = new Font("", 15, FontStyle.Bold);
                EntradaText[i].BackColor = Color.LightGreen;
                EntradaText[i].BorderStyle = BorderStyle.Fixed3D;
                this.Controls.Add(EntradaText[i]);
                esquerda += EntradaText[i].Width + 3;
            }

            esquerda = 40;
            topo = 115;
            for (i = 0; i < 3; i++)
            {
                Removido[i] = new Label();
                //Removido[i].Enabled = false;
                //EntradaText[i].Name = " " + i;
                //EntradaText[i].Text = EntradaText[i].Name;
                Removido[i].Left = esquerda;
                Removido[i].Top = topo;
                Removido[i].Anchor = new AnchorStyles();
                Removido[i].Size = new Size(40, 25);
                Removido[i].Font = new Font("", 15, FontStyle.Bold);
                Removido[i].BackColor = Color.LightCoral;
                Removido[i].BorderStyle = BorderStyle.Fixed3D;
                this.Controls.Add(Removido[i]);
                esquerda += Removido[i].Width + 10;
            }
            esquerda = 500;
            topo = 250;
            for (i = 0; i < 15; i++)
            {
                ResultadoLabel[i] = new Label();
                //ResultadoLabel[i].Name = "label" + i;
                //ResultadoLabel[i].Text = ResultadoLabel[i].Name;
                //ResultadoLabel[i].Anchor = AnchorStyles.Right;
                //ResultadoLabel[i].Location = new Point(ResultadoLabel[i].Location.X + 20, ResultadoLabel[i].Location.Y + 300);
                ResultadoLabel[i].Left = esquerda;
                ResultadoLabel[i].Top = topo;
                ResultadoLabel[i].Anchor = new AnchorStyles();
                ResultadoLabel[i].BackColor = Color.Gray;
                ResultadoLabel[i].ForeColor = Color.White;
                ResultadoLabel[i].Size = new Size(40, 30);
                ResultadoLabel[i].Font = new Font("", 15, FontStyle.Bold);
                //ResultadoLabel[i].TextAlign = ContentAlignment.BottomCenter;
                ResultadoLabel[i].BorderStyle = BorderStyle.Fixed3D;
                this.Controls.Add(ResultadoLabel[i]);
                esquerda += (ResultadoLabel[i].Width + 5);
                ResultadoLabel[i].Tag = i;
            }
            esquerda = 490;
            topo = 80;
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
                UltimoResultado[i].Size = new Size(42, 30);
                UltimoResultado[i].Font = new Font("", 15, FontStyle.Bold);
                UltimoResultado[i].TextAlign = ContentAlignment.BottomCenter;
                UltimoResultado[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(UltimoResultado[i]);
                esquerda += (UltimoResultado[i].Width + 3);

            }
        }
        private void MostraResultado()
        {

            int i, j = 0;
            int x = 0;
            int[] numerosOrganizados = new int[15];
            //int[] numerosOrganizados2 = new int[15];

            for (i = 0; i < numerosOrganizados.Length; i++)
            {
                for (i = 0; i < 5; i++)
                    numerosOrganizados[i] = Convert.ToInt32(EntradaLabel[i].Text);

                for (; i < 10; i++)
                    numerosOrganizados[i] = Convert.ToInt32(EntradaLabel2[j++].Text);

                for (; i < 15; i++)
                    numerosOrganizados[i] = Convert.ToInt32(EntradaText[x++].Text);
            }

            Array.Sort(numerosOrganizados);

            for (int y = 0; y < 15; y++)
            {
                ResultadoLabel[y].Text = numerosOrganizados[y].ToString("D2");
            }
        }

        private void Gerador()
        {
            Aleatorios ale = new Aleatorios();

            EntradaText[0].Text = EntradaText[0].Text;
            EntradaText[1].Text = EntradaText[1].Text;
            EntradaText[2].Text = EntradaText[2].Text;
            EntradaText[3].Text = EntradaText[3].Text;
            EntradaText[4].Text = EntradaText[4].Text;
            Removido[0].Text = Removido[0].Text;
            Removido[1].Text = Removido[1].Text;
            Removido[2].Text = Removido[2].Text;


            int[] numerosSorteados = ale.GeradorNaoRepetidos(5, 1, 12, EntradaText, Removido);
            int[] numerosSorteados2 = ale.GeradorNaoRepetidos(5, 13, 25, EntradaText, Removido);

            Array.Sort(numerosSorteados);
            Array.Sort(numerosSorteados2);

            Control[] controles = EntradaLabel;
            Control[] controles2 = EntradaLabel2;

            for (int i = 0; i < controles.Length; i++)
            {
                int valor = numerosSorteados[i];
                controles[i].Text = valor.ToString("D2");
            }
            for (int x = 0; x < controles2.Length; x++)
            {
                int valor = numerosSorteados2[x];
                controles2[x].Text = valor.ToString("D2");
            }
        }
        private void Gerador2()
        {
            Aleatorios ale = new Aleatorios();

            EntradaText[0].Text = EntradaText[0].Text;
            EntradaText[1].Text = EntradaText[1].Text;
            EntradaText[2].Text = EntradaText[2].Text;
            EntradaText[3].Text = EntradaText[3].Text;
            EntradaText[4].Text = EntradaText[4].Text;



            int[] numerosSorteados = ale.GeradorNaoRepetidos2(5, 1, 12, EntradaText);
            int[] numerosSorteados2 = ale.GeradorNaoRepetidos2(5, 13, 25, EntradaText);

            Array.Sort(numerosSorteados);
            Array.Sort(numerosSorteados2);

            Control[] controles = EntradaLabel;
            Control[] controles2 = EntradaLabel2;

            for (int i = 0; i < controles.Length; i++)
            {
                int valor = numerosSorteados[i];
                controles[i].Text = valor.ToString("D2");
            }
            for (int x = 0; x < controles2.Length; x++)
            {
                int valor = numerosSorteados2[x];
                controles2[x].Text = valor.ToString("D2");
            }
        }
        private bool VerificaCaixaDeTexto(string StrTexto)
        {
            if (String.IsNullOrEmpty(StrTexto)) return true;

            if (!(Convert.ToInt32(StrTexto) > 0 && Convert.ToInt32(StrTexto) < 26)) return true;

            return false;
        }
        private void GeradorFixos()
        {
            Aleatorios ale = new Aleatorios();
            Removido[0].Text = Removido[0].Text;
            Removido[1].Text = Removido[1].Text;
            Removido[2].Text = Removido[2].Text;

            int[] numerosSorteadosfixos = ale.GeradorAleatoriosFixos(5, 1, 25, Removido);

            Array.Sort(numerosSorteadosfixos);

            Control[] controles = EntradaText;

            for (int i = 0; i < controles.Length; i++)
            {
                int valor = numerosSorteadosfixos[i];
                controles[i].Text = valor.ToString("D2");
            }
        }
        private void GeradorFixos2()
        {
            Aleatorios ale = new Aleatorios();

            int[] numerosSorteadosfixos = ale.GeradorAleatoriosFixos2(5, 1, 25);

            Array.Sort(numerosSorteadosfixos);

            Control[] controles = EntradaText;

            for (int i = 0; i < controles.Length; i++)
            {
                int valor = numerosSorteadosfixos[i];
                controles[i].Text = valor.ToString("D2");
            }
        }
        private void GeradorExcluidos()
        {
            Aleatorios ale = new Aleatorios();

            int[] numerosSorteadosRemovidos = ale.GeradorAleatoriosExcluidos(3, 1, 25);

            Array.Sort(numerosSorteadosRemovidos);

            Control[] controles = Removido;

            for (int i = 0; i < controles.Length; i++)
            {
                int valor = numerosSorteadosRemovidos[i];
                controles[i].Text = valor.ToString("D2");
            }
        }
        private void LoadData(/*Label[] ResultadoLabel*/)
        {
            for (int i = 0; i < ResultadoLabel.Length; i++)
            {
                dgResultado.ColumnCount = 15;
                dgResultado.Columns[0].Name = "01";
                dgResultado.Columns[1].Name = "02";
                dgResultado.Columns[2].Name = "03";
                dgResultado.Columns[3].Name = "04";
                dgResultado.Columns[4].Name = "05";
                dgResultado.Columns[5].Name = "06";
                dgResultado.Columns[6].Name = "07";
                dgResultado.Columns[7].Name = "08";
                dgResultado.Columns[8].Name = "09";
                dgResultado.Columns[9].Name = "10";
                dgResultado.Columns[10].Name = "11";
                dgResultado.Columns[11].Name = "12";
                dgResultado.Columns[12].Name = "13";
                dgResultado.Columns[13].Name = "14";
                dgResultado.Columns[14].Name = "15";

                foreach (DataGridViewColumn column in dgResultado.Columns)
                {
                    if (column.DataPropertyName == "primeiraColuna")
                        column.Width = 10; //tamanho fixo da primeira coluna

                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            object[] row = {
                ResultadoLabel[0].Text,
                ResultadoLabel[1].Text,
                ResultadoLabel[2].Text,
                ResultadoLabel[3].Text,
                ResultadoLabel[4].Text,
                ResultadoLabel[5].Text,
                ResultadoLabel[6].Text,
                ResultadoLabel[7].Text,
                ResultadoLabel[8].Text,
                ResultadoLabel[9].Text,
                ResultadoLabel[10].Text,
                ResultadoLabel[11].Text,
                ResultadoLabel[12].Text,
                ResultadoLabel[13].Text,
                ResultadoLabel[14].Text,
            };
            dgResultado.Rows.Add(row);
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

                Control[] controles = UltimoResultado;

                for (int i = 0; i < controles.Length; i++)
                {
                    int valor = Convert.ToInt32(listaDezenass.listaDezenas[i]);
                    controles[i].Text = valor.ToString("D2");
                }
            }
            if (resposta.IsSuccessStatusCode)
            {
                var numero = resposta.Content.ReadAsStringAsync().Result;
                var sorteio = JsonConvert.DeserializeObject<ResultadoAtual>(numero);

                txtConcurso.Text = sorteio.numero.ToString();
            }
        }
        public void exportarDataGridViewParaTxt(DataGridView dgv)
        {
            System.IO.StreamWriter sw = null;
            //Caractere delimitador
            string delimitador = "\t"; //tab

            //Escolher onde salvar o arquivo
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sfd.Filter = "arquivos txt (*.txt)|*.txt";

            //Se usuário escolher nome corretamente e clicar em salvar
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Pega o caminho do arquivo
                    string caminho = sfd.FileName;
                    //Cria um StreamWriter no local
                    sw = new System.IO.StreamWriter(caminho);

                    int qtdColunas = dgv.Columns.Count;
                    //Loop em todas as linhas para escrever na stream já com o delimitador.
                    foreach (DataGridViewRow dgvLinha in dgv.Rows)
                    {
                        string linha = null;
                        for (int i = 0; i < qtdColunas; i++)
                        {
                            linha += dgvLinha.Cells[i].Value.ToString() + delimitador;
                        }
                        sw.WriteLine(linha);
                    }

                    //Mensagem de confirmação
                    MessageBox.Show("Jogo Salvo com sucesso", "Jogo Salvo com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    //Fechar stream SEMPRE
                    sw.Close();
                }
            }
        }
        public void botao()
        {
            if (Removido[2].Text != "")
            {
                EntradaText[0].Focus();
            }
        }
        private void frmCriarAposta_Load(object sender, EventArgs e)
        {
            CarregaResultado();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ckBoxNao_CheckStateChanged(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                btnSorteFora.Enabled = true;
                LimparExcluidos.Enabled = true;

            }
            else
            {
                btnSorteFora.Enabled = false;
                LimparExcluidos.Enabled = false;
                Removido[0].Text = "";
                Removido[1].Text = "";
                Removido[2].Text = "";
            }
        }

        private void btnSorteFora_Click(object sender, EventArgs e)
        {
            if (EntradaText[0].Text != "" || EntradaText[1].Text != "" || EntradaText[2].Text != "" || EntradaText[3].Text != "" || EntradaText[4].Text != "")
            {
                MessageBox.Show("Por favor, O Gerador de números Fixos deve estar Vasio.", "Advertência", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                GeradorExcluidos();
                ColorButon();
            }
        }

        private void LimparExcluidos_Click(object sender, EventArgs e)
        {
            Removido[0].Text = "";
            Removido[1].Text = "";
            Removido[2].Text = "";
            ColorButon();
        }

        private void btnSorteFixo_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (VerificaCaixaDeTexto(Removido[0].Text) || VerificaCaixaDeTexto(Removido[1].Text) || VerificaCaixaDeTexto(Removido[2].Text))
                {
                    MessageBox.Show("Por favor, escolha os tres Números para excluir  do jogo entre 1 e 25.", "Advertência", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    GeradorFixos();
                    ColorButon();
                }
            }
            else
            {
                GeradorFixos2();
                ColorButon();
            }
        }

        private void btnLimpaFixos_Click(object sender, EventArgs e)
        {
            if (EntradaText[4].Text != "")
            {
                EntradaText[4].Text = "";
            }
            else if (EntradaText[3].Text != "")
            {
                EntradaText[3].Text = "";
            }
            else if (EntradaText[2].Text != "")
            {
                EntradaText[2].Text = "";
            }
            else if (EntradaText[1].Text != "")
            {
                EntradaText[1].Text = "";
            }
            else
            {
                EntradaText[0].Text = "";
            }
            ColorButon();
        }

        private void btnGeraAleatorios_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (VerificaCaixaDeTexto(Removido[0].Text) || VerificaCaixaDeTexto(Removido[1].Text) || VerificaCaixaDeTexto(Removido[2].Text))
                {
                    MessageBox.Show("Por favor, escolha os tres Números para excluir  do jogo entre 1 e 25. ou escolha a opção 'Não Utilizar deste Recurso'", "Advertência", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (VerificaCaixaDeTexto(EntradaText[0].Text) || VerificaCaixaDeTexto(EntradaText[1].Text) || VerificaCaixaDeTexto(EntradaText[2].Text) || VerificaCaixaDeTexto(EntradaText[2].Text) || VerificaCaixaDeTexto(EntradaText[4].Text))
                {
                    MessageBox.Show("Por favor, escolha os 5 Números Fixos entre 1 e 25. ", "Advertência", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    Gerador();
                    ColorButon();
                }
            }
            else
            {
                if (VerificaCaixaDeTexto(EntradaText[0].Text) || VerificaCaixaDeTexto(EntradaText[1].Text) || VerificaCaixaDeTexto(EntradaText[2].Text) || VerificaCaixaDeTexto(EntradaText[2].Text) || VerificaCaixaDeTexto(EntradaText[4].Text))
                {
                    MessageBox.Show("Por favor, escolha os 5 Números Fixos entre 1 e 25.", "Advertência", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    Gerador2();
                    ColorButon();
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < EntradaLabel.Length; i++)
            {
                EntradaLabel[i].ResetText();
                EntradaLabel2[i].ResetText();
            }
            ColorButon();
        }    

        private void btnGerarCombinacao_Click(object sender, EventArgs e)
        {
            if (VerificaCaixaDeTexto(EntradaLabel[0].Text))
            {
                MessageBox.Show("Os Nmeros Aleatórios Precisam ser Gerados", "Advertência", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                if (VerificaCaixaDeTexto(EntradaText[0].Text) || VerificaCaixaDeTexto(EntradaText[1].Text) || VerificaCaixaDeTexto(EntradaText[2].Text) || VerificaCaixaDeTexto(EntradaText[2].Text) || VerificaCaixaDeTexto(EntradaText[4].Text))
                {
                    MessageBox.Show("Por favor, Os Campos do Gerador de 5 Números Fixos devem esta totalmente preenchidos.", "Advertência", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MostraResultado();
                }
            }
            Logica logica = new Logica();
            int outVal;

            if (ResultadoLabel.Any(lbl => !int.TryParse(lbl.Text, out outVal)))
            {
                MessageBox.Show("Valores Invalidos...");
                return;
            }

            var arr = ResultadoLabel.Select(lbl => int.Parse(lbl.Text));
            var result = logica.GetResult(arr).ToList();

            //foreach (var kvp in result.ToList())
            //Console.WriteLine($"{kvp.Key:D2}: {kvp.Value}");
            txt11Acertos.Text = result[0].Value.ToString();
            txt12Acertos.Text = result[1].Value.ToString();
            txt13Acertos.Text = result[2].Value.ToString();
            txt14Acertos.Text = result[3].Value.ToString();
            txt15Acertos.Text = result[4].Value.ToString();
        }

        private void btnSalvarGrid_Click(object sender, EventArgs e)
        {
            //A property abaixo tem que ser False, caso contrario os comando de style serão ignorados
            dgResultado.EnableHeadersVisualStyles = false;
            dgResultado.ClearSelection();
            dgResultado.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgResultado.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            // AO font vem do componete pai, no caso o form
            dgResultado.ColumnHeadersDefaultCellStyle.Font = new Font(dgResultado.Font.Name, dgResultado.Font.Size + 1, FontStyle.Regular);
            //A altura dalinha de cabeçalho, primeiro habilita resize depois a altura 2.4 vezes a altura da font
            dgResultado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgResultado.ColumnHeadersHeight = Convert.ToInt16(2.4 * dgResultado.ColumnHeadersDefaultCellStyle.Font.Height);
            //Define a style da linha divisória entre os headers
            dgResultado.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            //tira as linhas da coluna
            // dataGridLabel.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            int i = 0;

            if (ResultadoLabel[i].Text != "")
            {
                LoadData();
            }
            else
            {
                MessageBox.Show(" Deve Gerar uma Combinação!");
            }
            double Valor = 3;
            int total = 1;
            // lblValor.Text = "0,00";
            foreach (DataGridViewRow row in dgResultado.Rows)
            {
                lblTotal.Text = total.ToString();
                total = Convert.ToInt32(dgResultado.Rows.Count);
                //lblValor.Text = "R$ " + Convert.ToString((total * Valor));
                string formattedNum = "R$ " + string.Format("{0:f2}", Valor * total);
                lblValor.Text = formattedNum;
            }
        }
        Bitmap bmp;

        private void btnImprimirJogo_Click(object sender, EventArgs e)
        {
            if (dgResultado.RowCount != 0)
            {
                int height = dgResultado.Height;
                dgResultado.Height = dgResultado.RowCount * dgResultado.RowTemplate.Height * 2;
                bmp = new Bitmap(dgResultado.Width, dgResultado.Height);
                dgResultado.DrawToBitmap(bmp, new Rectangle(0, 0, dgResultado.Width, dgResultado.Height));
                dgResultado.Height = height;
                //printPreviewDialog1.ShowDialog();
            }
            else
            {
                MessageBox.Show("  Crie uma sequencia para imprimir");
            }
        }

        private void btnLimpaGrid_Click(object sender, EventArgs e)
        {
            if (dgResultado.Rows.Count <= 0)
            {
                // Se entrar aqui é porque  não tem linha selecionado no grid
                MessageBox.Show("O Grid esta Limpo", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                for (int i = 0; i < dgResultado.RowCount; i++)
                {
                    dgResultado.Rows[i].DataGridView.Columns.Clear();
                }
                lblValor.Text = "0,00";
                lblTotal.Text = "0";
            }
        }

        private void btnExcluirLinha_Click(object sender, EventArgs e)
        {
            // Primeiro vamos ver se tem alguma linha selecionado no Grid
            if (dgResultado.SelectedRows.Count == 0)
            {
                // Se entrar aqui é porque  não tem linha selecionado no grid
                MessageBox.Show("Nenhum Item Selecionado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                // Se cair no else é porque tem uma linha selecionada no grid
                // Removendo a linha selecionada  
                dgResultado.Rows.RemoveAt(dgResultado.CurrentRow.Index);
                double Valor = 3;
                int total = 1;
                // Se cair no else é porque ainda tem linha no grid
                // Zerando os valores 
                if (dgResultado.SelectedRows.Count == 0)
                {
                    lblTotal.Text = "0";
                    lblValor.Text = "0,00";
                }
                else
                {
                    foreach (DataGridViewRow row in dgResultado.Rows)
                    {
                        lblTotal.Text = total.ToString();
                        total = Convert.ToInt32(dgResultado.Rows.Count);
                        //lblValor.Text = "R$ " + Convert.ToString((total * Valor));
                        string formattedNum = "R$ " + string.Format("{0:f2}", Valor * total);
                        lblValor.Text = formattedNum;
                    }
                }
            }
        }

        private void btnSalvarJogo_Click(object sender, EventArgs e)
        {
            if (dgResultado.RowCount != 0)
            {
                if (cbResposta.Checked == true)
                {
                    for (int i = 0; i < dgResultado.RowCount; i++)
                    {
                        const string nomeArquivo = @"\BoaSorte\Aposta.txt";
                        List<string> linhas = File.ReadLines(nomeArquivo).ToList(); // Passo 1

                        if (linhas.IndexOf(
                        dgResultado.Rows[i].Cells["01"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["02"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["03"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["04"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["05"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["06"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["07"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["08"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["09"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["10"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["11"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["12"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["13"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["14"].Value.ToString() + " " +
                        dgResultado.Rows[i].Cells["15"].Value.ToString()) == 0)
                        { }
                        else
                        {
                            linhas.Insert(0,
                            dgResultado.Rows[i].Cells["01"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["02"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["03"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["04"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["05"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["06"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["07"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["08"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["09"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["10"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["11"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["12"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["13"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["14"].Value.ToString() + " " +
                            dgResultado.Rows[i].Cells["15"].Value.ToString()); // Passo 2
                            File.WriteAllLines(nomeArquivo, linhas);
                        }
                    }
                    MessageBox.Show("Numeros incluidos ao jogo salvo!");
                    dgResultado.Rows.Clear();
                }
                else
                {
                    string file = @"C:\BoaSorte\Aposta.txt";
                    using (TextWriter tw = new StreamWriter(file))
                    {
                        int i, j = 0;
                        for (i = 0; i <= dgResultado.Rows.Count - 1; i++)
                        {
                            for (j = 0; j < dgResultado.Columns.Count; j++)
                            {

                                tw.Write($"{dgResultado.Rows[i].Cells[j].Value.ToString()}");
                                tw.Write(" ");
                            }

                            tw.WriteLine("");
                        }
                    }
                    MessageBox.Show("Novo jogo salvo com Sucesso!");
                    dgResultado.Rows.Clear();
                }
            }
            else
            {
                MessageBox.Show(" O Grid não contem um jogo para salvar");
            }

        }

        private void btnSalvarDocumentos_Click(object sender, EventArgs e)
        {
            exportarDataGridViewParaTxt(dgResultado);
            //StringBuilder sb = new StringBuilder();
            //try
            //{
            //    foreach (DataGridViewRow row in this.dgResultado.Rows)
            //    {
            //        foreach (DataGridViewCell cell in row.Cells)
            //        {
            //            if (cell.Value != null)
            //            {
            //                sb.Append(cell.Value.ToString() + "\t");
            //            }
            //            else
            //            {
            //                sb.Append("" + "\t");
            //            }
            //        }
            //        sb.Append(Environment.NewLine);
            //    }

            //    if (dgResultado.RowCount != 0)
            //    {
            //        SaveFileDialog sfd = new SaveFileDialog();
            //        sfd.ShowDialog();
            //        string strCaminho = sfd.FileName;

            //        using (StreamWriter writer = new StreamWriter(strCaminho))
            //        {
            //            writer.Write(sb.ToString());
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show(" O Grid não contem um jogo para salvar");
            //    }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("  Nenhum jogo foi salvo!");
            //}
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

        private void btn_01_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "01";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "01";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "01";
                }
                if (Removido[0].Text == "01" || Removido[1].Text == "01" || Removido[2].Text == "01")
                {
                    if (Removido[0].Text != "")
                    {
                        Removido[1].Focus();
                    }
                    //btn_01.Enabled = false;
                    //btn_01.BackColor = Color.Red;
                    //lbl01.Text = "Fora";
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "01";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "01";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "01";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "01";
                }
                else
                {
                    EntradaText[4].Text = "01";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "01";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "01";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "01";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "01";
                }
                else
                {
                    EntradaText[4].Text = "01";
                }
            }
            ColorButon();
        }

        private void btn_02_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "02";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "02";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "02";
                }
                if (Removido[0].Text == "02" || Removido[1].Text == "02" || Removido[2].Text == "02")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "02";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "02";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "02";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "02";
                }
                else
                {
                    EntradaText[4].Text = "02";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "02";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "02";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "02";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "02";
                }
                else
                {
                    EntradaText[4].Text = "02";
                }
            }
            ColorButon();
        }

        private void btn_03_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "03";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "03";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "03";
                }
                if (Removido[0].Text == "03" || Removido[1].Text == "03" || Removido[2].Text == "03")
                {
                    ColorButon();
                }

                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "03";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "03";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "03";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "03";
                }
                else
                {
                    EntradaText[4].Text = "03";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "03";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "03";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "03";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "03";
                }
                else
                {
                    EntradaText[4].Text = "03";
                }
            }
            ColorButon();
        }

        private void btn_04_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "04";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "04";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "04";
                }
                if (Removido[0].Text == "04" || Removido[1].Text == "04" || Removido[2].Text == "04")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "04";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "04";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "04";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "04";
                }
                else
                {
                    EntradaText[4].Text = "04";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "04";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "04";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "04";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "04";
                }
                else
                {
                    EntradaText[4].Text = "04";
                }
            }
            ColorButon();
        }

        private void btn_05_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "05";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "05";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "05";
                }
                if (Removido[0].Text == "05" || Removido[1].Text == "05" || Removido[2].Text == "05")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "05";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "05";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "05";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "05";
                }
                else
                {
                    EntradaText[4].Text = "05";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "05";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "05";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "05";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "05";
                }
                else
                {
                    EntradaText[4].Text = "05";
                }
            }
            ColorButon();
        }

        private void btn_06_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "06";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "06";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "06";
                }
                if (Removido[0].Text == "06" || Removido[1].Text == "06" || Removido[2].Text == "06")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "06";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "06";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "06";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "06";
                }
                else
                {
                    EntradaText[4].Text = "06";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "06";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "06";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "06";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "06";
                }
                else
                {
                    EntradaText[4].Text = "06";
                }
            }
            ColorButon();
        }

        private void btn_07_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "07";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "07";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "07";
                }
                if (Removido[0].Text == "07" || Removido[1].Text == "07" || Removido[2].Text == "07")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "07";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "07";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "07";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "07";
                }
                else
                {
                    EntradaText[4].Text = "07";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "07";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "07";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "07";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "07";
                }
                else
                {
                    EntradaText[4].Text = "07";
                }
            }
            ColorButon();
        }

        private void btn_08_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "08";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "08";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "08";
                }
                if (Removido[0].Text == "08" || Removido[1].Text == "08" || Removido[2].Text == "08")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "08";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "08";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "08";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "08";
                }
                else
                {
                    EntradaText[4].Text = "08";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "08";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "08";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "08";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "08";
                }
                else
                {
                    EntradaText[4].Text = "08";
                }
            }
            ColorButon();
        }

        private void btn_09_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "09";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "09";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "09";
                }
                if (Removido[0].Text == "09" || Removido[1].Text == "09" || Removido[2].Text == "09")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "09";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "09";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "09";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "09";
                }
                else
                {
                    EntradaText[4].Text = "09";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "09";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "09";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "09";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "09";
                }
                else
                {
                    EntradaText[4].Text = "09";
                }
            }
            ColorButon();
        }

        private void btn_10_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "10";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "10";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "10";
                }
                if (Removido[0].Text == "10" || Removido[1].Text == "10" || Removido[2].Text == "10")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "10";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "10";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "10";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "10";
                }
                else
                {
                    EntradaText[4].Text = "10";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "10";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "10";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "10";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "10";
                }
                else
                {
                    EntradaText[4].Text = "10";
                }
            }
            ColorButon();
        }

        private void btn_11_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "11";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "11";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "11";
                }
                if (Removido[0].Text == "11" || Removido[1].Text == "11" || Removido[2].Text == "11")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "11";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "11";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "11";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "11";
                }
                else
                {
                    EntradaText[4].Text = "11";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "11";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "11";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "11";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "11";
                }
                else
                {
                    EntradaText[4].Text = "11";
                }
            }
            ColorButon();
        }

        private void btn_12_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "12";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "12";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "12";
                }
                if (Removido[0].Text == "12" || Removido[1].Text == "12" || Removido[2].Text == "12")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "12";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "12";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "12";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "12";
                }
                else
                {
                    EntradaText[4].Text = "12";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "12";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "12";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "12";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "12";
                }
                else
                {
                    EntradaText[4].Text = "12";
                }
            }
            ColorButon();
        }

        private void btn_13_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "13";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "13";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "13";
                }
                if (Removido[0].Text == "13" || Removido[1].Text == "13" || Removido[2].Text == "13")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "13";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "13";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "13";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "13";
                }
                else
                {
                    EntradaText[4].Text = "13";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "13";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "13";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "13";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "13";
                }
                else
                {
                    EntradaText[4].Text = "13";
                }
            }
            ColorButon();
        }

        private void btn_14_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "14";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "14";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "14";
                }
                if (Removido[0].Text == "14" || Removido[1].Text == "14" || Removido[2].Text == "14")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "14";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "14";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "14";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "14";
                }
                else
                {
                    EntradaText[4].Text = "14";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "14";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "14";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "14";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "14";
                }
                else
                {
                    EntradaText[4].Text = "14";
                }
            }
            ColorButon();
        }

        private void btn_15_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "15";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "15";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "15";
                }
                if (Removido[0].Text == "15" || Removido[1].Text == "15" || Removido[2].Text == "15")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "15";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "15";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "15";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "15";
                }
                else
                {
                    EntradaText[4].Text = "15";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "15";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "15";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "15";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "15";
                }
                else
                {
                    EntradaText[4].Text = "15";
                }
            }
            ColorButon();
        }

        private void btn_16_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "16";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "16";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "16";
                }
                if (Removido[0].Text == "16" || Removido[1].Text == "16" || Removido[2].Text == "16")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "16";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "16";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "16";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "16";
                }
                else
                {
                    EntradaText[4].Text = "16";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "16";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "16";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "16";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "16";
                }
                else
                {
                    EntradaText[4].Text = "16";
                }
            }
            ColorButon();
        }

        private void btn_17_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "17";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "17";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "17";
                }
                if (Removido[0].Text == "17" || Removido[1].Text == "17" || Removido[2].Text == "17")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "17";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "17";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "17";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "17";
                }
                else
                {
                    EntradaText[4].Text = "17";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "17";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "17";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "17";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "17";
                }
                else
                {
                    EntradaText[4].Text = "17";
                }
            }
            ColorButon();
        }

        private void btn_18_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "18";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "18";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "18";
                }
                if (Removido[0].Text == "18" || Removido[1].Text == "18" || Removido[2].Text == "18")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "18";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "18";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "18";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "18";
                }
                else
                {
                    EntradaText[4].Text = "18";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "18";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "18";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "18";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "18";
                }
                else
                {
                    EntradaText[4].Text = "18";
                }
            }
            ColorButon();
        }

        private void btn_19_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "19";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "19";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "19";
                }
                if (Removido[0].Text == "19" || Removido[1].Text == "19" || Removido[2].Text == "19")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "19";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "19";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "19";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "19";
                }
                else
                {
                    EntradaText[4].Text = "19";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "19";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "19";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "19";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "19";
                }
                else
                {
                    EntradaText[4].Text = "19";
                }
            }
            ColorButon();
        }

        private void btn_20_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "20";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "20";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "20";
                }
                if (Removido[0].Text == "20" || Removido[1].Text == "20" || Removido[2].Text == "20")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "20";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "20";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "20";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "20";
                }
                else
                {
                    EntradaText[4].Text = "20";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "20";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "20";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "20";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "20";
                }
                else
                {
                    EntradaText[4].Text = "20";
                }
            }
            ColorButon();
        }

        private void btn_21_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "21";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "21";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "21";
                }
                if (Removido[0].Text == "21" || Removido[1].Text == "21" || Removido[2].Text == "21")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "21";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "21";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "21";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "21";
                }
                else
                {
                    EntradaText[4].Text = "21";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "21";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "21";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "21";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "21";
                }
                else
                {
                    EntradaText[4].Text = "21";
                }
            }
            ColorButon();
        }

        private void btn_22_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "22";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "22";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "22";
                }
                if (Removido[0].Text == "22" || Removido[1].Text == "22" || Removido[2].Text == "22")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "22";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "22";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "22";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "22";
                }
                else
                {
                    EntradaText[4].Text = "22";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "22";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "22";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "22";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "22";
                }
                else
                {
                    EntradaText[4].Text = "22";
                }
            }
            ColorButon();
        }

        private void btn_23_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "23";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "23";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "23";
                }
                if (Removido[0].Text == "23" || Removido[1].Text == "23" || Removido[2].Text == "23")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "23";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "23";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "23";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "23";
                }
                else
                {
                    EntradaText[4].Text = "23";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "23";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "23";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "23";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "23";
                }
                else
                {
                    EntradaText[4].Text = "23";
                }
            }
            ColorButon();
        }

        private void btn_24_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "24";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "24";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "24";
                }
                if (Removido[0].Text == "24" || Removido[1].Text == "24" || Removido[2].Text == "24")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "24";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "24";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "24";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "24";
                }
                else
                {
                    EntradaText[4].Text = "24";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "24";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "24";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "24";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "24";
                }
                else
                {
                    EntradaText[4].Text = "24";
                }
            }
            ColorButon();
        }
        private void btn_25_Click(object sender, EventArgs e)
        {
            if (ckBoxNao.Checked == false)
            {
                if (Removido[0].Text == "")
                {
                    Removido[0].Text = "25";
                }
                else if (Removido[1].Text == "")
                {
                    Removido[1].Text = "25";
                }
                else if (Removido[2].Text == "")
                {
                    Removido[2].Text = "25";
                }
                if (Removido[0].Text == "25" || Removido[1].Text == "25" || Removido[2].Text == "25")
                {
                    ColorButon();
                }
                else if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "25";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "25";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "25";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "25";
                }
                else
                {
                    EntradaText[4].Text = "25";
                }
            }
            else
            {
                if (EntradaText[0].Text == "")
                {
                    EntradaText[0].Text = "25";
                }
                else if (EntradaText[1].Text == "")
                {
                    EntradaText[1].Text = "25";
                }
                else if (EntradaText[2].Text == "")
                {
                    EntradaText[2].Text = "25";
                }
                else if (EntradaText[3].Text == "")
                {
                    EntradaText[3].Text = "25";
                }
                else
                {
                    EntradaText[4].Text = "25";
                }
            }
            ColorButon();
        }
        private void ColorButon()
        {
            if (EntradaText[0].Text == "01" || EntradaText[1].Text == "01" || EntradaText[2].Text == "01" || EntradaText[3].Text == "01" || EntradaText[4].Text == "01"
                || EntradaText[4].Text == "01")
            {
                btn_01.BackColor = Color.YellowGreen;
                btn_01.Enabled = false;
                _01.Text = "Fixed";
            }
            else if (EntradaLabel[0].Text == "01")
            {
                btn_01.BackColor = Color.LightGreen;
                btn_01.Enabled = false;
                _01.Text = "Luck";
            }
            else if (Removido[0].Text == "01" || Removido[1].Text == "01" || Removido[2].Text == "01")
            {
                btn_01.Enabled = false;
                btn_01.BackColor = Color.Red;
                _01.Text = "Out";
            }
            else
            {
                btn_01.Enabled = true;
                btn_01.BackColor = Color.LightBlue;
                _01.Text = "";
            }
            if (EntradaText[0].Text == "02" || EntradaText[1].Text == "02" || EntradaText[2].Text == "02" || EntradaText[3].Text == "02" || EntradaText[4].Text == "02")
            {
                btn_02.BackColor = Color.YellowGreen;
                btn_02.Enabled = false;
                _02.Text = "Fixed";
            }
            else if (EntradaLabel[0].Text == "02" || EntradaLabel[1].Text == "02")
            {
                btn_02.BackColor = Color.LightGreen;
                btn_02.Enabled = false;
                _02.Text = "Luck";
            }
            else if (Removido[0].Text == "02" || Removido[1].Text == "02" || Removido[2].Text == "02")
            {
                btn_02.Enabled = false;
                btn_02.BackColor = Color.Red;
                _02.Text = "Out";
            }
            else
            {
                btn_02.Enabled = true;
                btn_02.BackColor = Color.LightBlue;
                _02.Text = "";
            }
            if (EntradaText[0].Text == "03" || EntradaText[1].Text == "03" || EntradaText[2].Text == "03" || EntradaText[3].Text == "03" || EntradaText[4].Text == "03")
            {
                btn_03.BackColor = Color.YellowGreen;
                btn_03.Enabled = false;
                _03.Text = "Fixed";
            }
            else if (EntradaLabel[0].Text == "03" || EntradaLabel[1].Text == "03" || EntradaLabel[2].Text == "03")
            {
                btn_03.BackColor = Color.LightGreen;
                btn_03.Enabled = false;
                _03.Text = "Luck";
            }
            else if (Removido[0].Text == "03" || Removido[1].Text == "03" || Removido[2].Text == "03")
            {
                btn_03.Enabled = false;
                btn_03.BackColor = Color.Red;
                _03.Text = "Out";
            }
            else
            {
                btn_03.Enabled = true;
                btn_03.BackColor = Color.LightBlue;
                _03.Text = "";
            }

            if (EntradaText[0].Text == "04" || EntradaText[1].Text == "04" || EntradaText[2].Text == "04" || EntradaText[3].Text == "04" || EntradaText[4].Text == "04")
            {
                btn_04.BackColor = Color.YellowGreen;
                btn_04.Enabled = false;
                _04.Text = "Fixed";
            }
            else if (EntradaLabel[0].Text == "04" || EntradaLabel[1].Text == "04" || EntradaLabel[2].Text == "04" || EntradaLabel[3].Text == "04")
            {
                btn_04.BackColor = Color.LightGreen;
                btn_04.Enabled = false;
                _04.Text = "Luck";
            }
            else if (Removido[0].Text == "04" || Removido[1].Text == "04" || Removido[2].Text == "04")
            {
                btn_04.Enabled = false;
                btn_04.BackColor = Color.Red;
                _04.Text = "Out";
            }
            else
            {
                btn_04.Enabled = true;
                btn_04.BackColor = Color.LightBlue;
                _04.Text = "";
            }

            if (EntradaText[0].Text == "05" || EntradaText[1].Text == "05" || EntradaText[2].Text == "05" || EntradaText[3].Text == "05" || EntradaText[4].Text == "05")
            {
                btn_05.Enabled = false;
                btn_05.BackColor = Color.YellowGreen;
                _05.Text = "Fixed";
            }
            else if (EntradaLabel[0].Text == "05" || EntradaLabel[1].Text == "05" || EntradaLabel[2].Text == "05" || EntradaLabel[3].Text == "05" || EntradaLabel[4].Text == "05")
            {
                btn_05.Enabled = false;
                btn_05.BackColor = Color.LightGreen;
                _05.Text = "Luck";
            }
            else if (Removido[0].Text == "05" || Removido[1].Text == "05" || Removido[2].Text == "05")
            {
                btn_05.Enabled = false;
                btn_05.BackColor = Color.Red;
                _05.Text = "Out";
            }
            else
            {
                btn_05.Enabled = true;
                btn_05.BackColor = Color.LightBlue;
                _05.Text = "";
            }

            if (EntradaText[0].Text == "06" || EntradaText[1].Text == "06" || EntradaText[2].Text == "06" || EntradaText[3].Text == "06" || EntradaText[4].Text == "06")
            {
                btn_06.Enabled = false;
                btn_06.BackColor = Color.YellowGreen;
                _06.Text = "Fixed";

            }
            else if (EntradaLabel[0].Text == "06" || EntradaLabel[1].Text == "06" || EntradaLabel[2].Text == "06" || EntradaLabel[3].Text == "06"
            || EntradaLabel[4].Text == "06")
            {
                btn_06.Enabled = false;
                btn_06.BackColor = Color.LightGreen;
                _06.Text = "Luck";
            }
            else if (Removido[0].Text == "06" || Removido[1].Text == "06" || Removido[2].Text == "06")
            {
                btn_06.Enabled = false;
                btn_06.BackColor = Color.Red;
                _06.Text = "Out";
            }
            else
            {
                btn_06.Enabled = true;
                btn_06.BackColor = Color.LightBlue;
                _06.Text = "";
            }

            if (EntradaText[0].Text == "07" || EntradaText[1].Text == "07" || EntradaText[2].Text == "07" || EntradaText[3].Text == "07" || EntradaText[4].Text == "07")
            {
                btn_07.Enabled = false;
                btn_07.BackColor = Color.YellowGreen;
                _07.Text = "Fixed";
            }
            else if (EntradaLabel[0].Text == "07" || EntradaLabel[1].Text == "07" || EntradaLabel[2].Text == "07" || EntradaLabel[3].Text == "07"
                || EntradaLabel[4].Text == "07")
            {
                btn_07.Enabled = false;
                btn_07.BackColor = Color.LightGreen;
                _07.Text = "Luck";
            }
            else if (Removido[0].Text == "07" || Removido[1].Text == "07" || Removido[2].Text == "07")
            {
                btn_07.Enabled = false;
                btn_07.BackColor = Color.Red;
                _07.Text = "Out";
            }
            else
            {
                btn_07.Enabled = true;
                btn_07.BackColor = Color.LightBlue;
                _07.Text = "";
            }

            if (EntradaText[0].Text == "08" || EntradaText[1].Text == "08" || EntradaText[2].Text == "08" || EntradaText[3].Text == "08" || EntradaText[4].Text == "08")
            {
                btn_08.Enabled = false;
                btn_08.BackColor = Color.YellowGreen;
                _08.Text = "Fixed";
            }
            else if (EntradaLabel[0].Text == "08" || EntradaLabel[1].Text == "08" || EntradaLabel[2].Text == "8" || EntradaLabel[3].Text == "08"
                || EntradaLabel[4].Text == "08")
            {
                btn_08.Enabled = false;
                btn_08.BackColor = Color.LightGreen;
                _08.Text = "Luck";
            }
            else if (Removido[0].Text == "08" || Removido[1].Text == "08" || Removido[2].Text == "08")
            {
                btn_08.Enabled = false;
                btn_08.BackColor = Color.Red;
                _08.Text = "Out";
            }
            else
            {
                btn_08.Enabled = true;
                btn_08.BackColor = Color.LightBlue;
                _08.Text = "";
            }

            if (EntradaText[0].Text == "09" || EntradaText[1].Text == "09" || EntradaText[2].Text == "09" || EntradaText[3].Text == "09" || EntradaText[4].Text == "09")
            {
                btn_09.Enabled = false;
                btn_09.BackColor = Color.YellowGreen;
                _09.Text = "Fixed";
            }
            else if (EntradaLabel[0].Text == "09" || EntradaLabel[1].Text == "09" || EntradaLabel[2].Text == "09" || EntradaLabel[3].Text == "09"
                || EntradaLabel[4].Text == "09")
            {
                btn_09.Enabled = false;
                btn_09.BackColor = Color.LightGreen;
                _09.Text = "Luck";
            }
            else if (Removido[0].Text == "09" || Removido[1].Text == "09" || Removido[2].Text == "09")
            {
                btn_09.Enabled = false;
                btn_09.BackColor = Color.Red;
                _09.Text = "Out";
            }
            else
            {
                btn_09.Enabled = true;
                btn_09.BackColor = Color.LightBlue;
                _09.Text = "";
            }

            if (EntradaText[0].Text == "10" || EntradaText[1].Text == "10" || EntradaText[2].Text == "10" || EntradaText[3].Text == "10" || EntradaText[4].Text == "10")
            {
                btn_10.Enabled = false;
                btn_10.BackColor = Color.YellowGreen;
                _10.Text = "Fixed";
            }
            else if (EntradaLabel[0].Text == "10" || EntradaLabel[1].Text == "10" || EntradaLabel[2].Text == "10" || EntradaLabel[3].Text == "10" || EntradaLabel[4].Text == "10")
            {
                btn_10.Enabled = false;
                btn_10.BackColor = Color.LightGreen;
                _10.Text = "Luck";
            }
            else if (Removido[0].Text == "10" || Removido[1].Text == "10" || Removido[2].Text == "10")
            {
                btn_10.Enabled = false;
                btn_10.BackColor = Color.Red;
                _10.Text = "Out";
            }
            else
            {
                btn_10.Enabled = true;
                btn_10.BackColor = Color.LightBlue;
                _10.Text = "";
            }

            if (EntradaText[0].Text == "11" || EntradaText[1].Text == "11" || EntradaText[2].Text == "11" || EntradaText[3].Text == "11" || EntradaText[4].Text == "11")
            {
                btn_11.BackColor = Color.YellowGreen;
                btn_11.Enabled = false;
                _11.Text = "Fixed";
            }
            else if (EntradaLabel[0].Text == "11" || EntradaLabel[1].Text == "11" || EntradaLabel[2].Text == "11" || EntradaLabel[3].Text == "11" || EntradaLabel[4].Text == "11")
            {
                btn_11.BackColor = Color.LightGreen;
                btn_11.Enabled = false;
                _11.Text = "Luck";
            }
            else if (Removido[0].Text == "11" || Removido[1].Text == "11" || Removido[2].Text == "11")
            {
                btn_11.Enabled = false;
                btn_11.BackColor = Color.Red;
                _11.Text = "Out";
            }
            else
            {
                btn_11.Enabled = true;
                btn_11.BackColor = Color.LightBlue;
                _11.Text = "";
            }

            if (EntradaText[0].Text == "12" || EntradaText[1].Text == "12" || EntradaText[2].Text == "12" || EntradaText[3].Text == "12" || EntradaText[4].Text == "12")
            {
                btn_12.Enabled = false;
                btn_12.BackColor = Color.YellowGreen;
                _12.Text = "Fixed";
            }
            else if (EntradaLabel[0].Text == "12" || EntradaLabel[1].Text == "12" || EntradaLabel[2].Text == "12" || EntradaLabel[3].Text == "12" || EntradaLabel[4].Text == "12")
            {
                btn_12.Enabled = false;
                btn_12.BackColor = Color.LightGreen;
                _12.Text = "Luck";
            }
            else if (Removido[0].Text == "12" || Removido[1].Text == "12" || Removido[2].Text == "12")
            {
                btn_12.Enabled = false;
                btn_12.BackColor = Color.Red;
                _12.Text = "Out";
            }
            else
            {
                btn_12.Enabled = true;
                btn_12.BackColor = Color.LightBlue;
                _12.Text = "";
            }

            if (EntradaText[0].Text == "13" || EntradaText[1].Text == "13" || EntradaText[2].Text == "13" || EntradaText[3].Text == "13" || EntradaText[4].Text == "13")
            {
                btn_13.Enabled = false;
                btn_13.BackColor = Color.YellowGreen;
                _13.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "13" || EntradaLabel2[1].Text == "13" || EntradaLabel2[2].Text == "13" || EntradaLabel2[3].Text == "13" || EntradaLabel2[4].Text == "13")
            {
                btn_13.Enabled = true;
                btn_13.BackColor = Color.LightGreen;
                _13.Text = "Luck";
            }
            else if (Removido[0].Text == "13" || Removido[1].Text == "13" || Removido[2].Text == "13")
            {
                btn_13.Enabled = false;
                btn_13.BackColor = Color.Red;
                _13.Text = "Out";
            }
            else
            {
                btn_13.Enabled = true;
                btn_13.BackColor = Color.LightBlue;
                _13.Text = "";
            }

            if (EntradaText[0].Text == "14" || EntradaText[1].Text == "14" || EntradaText[2].Text == "14" || EntradaText[3].Text == "14" || EntradaText[4].Text == "14")
            {
                btn_14.Enabled = false;
                btn_14.BackColor = Color.YellowGreen;
                _14.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "14" || EntradaLabel2[1].Text == "14" || EntradaLabel2[2].Text == "14" || EntradaLabel2[3].Text == "14" || EntradaLabel2[4].Text == "14")
            {
                btn_14.Enabled = false;
                btn_14.BackColor = Color.LightGreen;
                _14.Text = "Luck";
            }
            else if (Removido[0].Text == "14" || Removido[1].Text == "14" || Removido[2].Text == "14")
            {
                btn_14.Enabled = false;
                btn_14.BackColor = Color.Red;
                _14.Text = "Out";
            }
            else
            {
                btn_14.Enabled = true;
                btn_14.BackColor = Color.LightBlue;
                _14.Text = "";
            }

            if (EntradaText[0].Text == "15" || EntradaText[1].Text == "15" || EntradaText[2].Text == "15" || EntradaText[3].Text == "15" || EntradaText[4].Text == "15")
            {
                btn_15.Enabled = false;
                btn_15.BackColor = Color.YellowGreen;
                _15.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "15" || EntradaLabel2[1].Text == "15" || EntradaLabel2[2].Text == "15" || EntradaLabel2[3].Text == "15" || EntradaLabel2[4].Text == "15")
            {
                btn_15.Enabled = false;
                btn_15.BackColor = Color.LightGreen;
                _15.Text = "Luck";
            }
            else if (Removido[0].Text == "15" || Removido[1].Text == "15" || Removido[2].Text == "15")
            {
                btn_15.Enabled = false;
                btn_15.BackColor = Color.Red;
                _15.Text = "Out";
            }
            else
            {
                btn_15.Enabled = true;
                btn_15.BackColor = Color.LightBlue;
                _15.Text = "";
            }

            if (EntradaText[0].Text == "16" || EntradaText[1].Text == "16" || EntradaText[2].Text == "16" || EntradaText[3].Text == "16" || EntradaText[4].Text == "16")
            {
                btn_16.Enabled = false;
                btn_16.BackColor = Color.YellowGreen;
                _16.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "16" || EntradaLabel2[1].Text == "16" || EntradaLabel2[2].Text == "16" || EntradaLabel2[3].Text == "16"
                || EntradaLabel2[4].Text == "16")
            {
                btn_16.Enabled = false;
                btn_16.BackColor = Color.LightGreen;
                _16.Text = "Luck";
            }
            else if (Removido[0].Text == "16" || Removido[1].Text == "16" || Removido[2].Text == "16")
            {
                btn_16.Enabled = false;
                btn_16.BackColor = Color.Red;
                _16.Text = "Out";
            }
            else
            {
                btn_16.Enabled = true;
                btn_16.BackColor = Color.LightBlue;
                _16.Text = "";
            }
            if (EntradaText[0].Text == "17" || EntradaText[1].Text == "17" || EntradaText[2].Text == "17" || EntradaText[3].Text == "17" || EntradaText[4].Text == "17")
            {
                btn_17.Enabled = false;
                btn_17.BackColor = Color.YellowGreen;
                _17.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "17" || EntradaLabel2[1].Text == "17" || EntradaLabel2[2].Text == "17" || EntradaLabel2[3].Text == "17" || EntradaLabel2[4].Text == "17")
            {
                btn_17.Enabled = false;
                btn_17.BackColor = Color.LightGreen;
                _17.Text = "Luck";
            }
            else if (Removido[0].Text == "17" || Removido[1].Text == "17" || Removido[2].Text == "17")
            {
                btn_17.Enabled = false;
                btn_17.BackColor = Color.Red;
                _17.Text = "Out";
            }
            else
            {
                btn_17.Enabled = true;
                btn_17.BackColor = Color.LightBlue;
                _17.Text = "";
            }

            if (EntradaText[0].Text == "18" || EntradaText[1].Text == "18" || EntradaText[2].Text == "18" || EntradaText[3].Text == "18" || EntradaText[4].Text == "18")
            {
                btn_18.Enabled = false;
                btn_18.BackColor = Color.YellowGreen;
                _18.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "18" || EntradaLabel2[1].Text == "18" || EntradaLabel2[2].Text == "18" || EntradaLabel2[3].Text == "18"
            || EntradaLabel2[4].Text == "18")
            {
                btn_18.Enabled = false;
                btn_18.BackColor = Color.LightGreen;
                _18.Text = "Sorte";
            }
            else if (Removido[0].Text == "18" || Removido[1].Text == "18" || Removido[2].Text == "18")
            {
                btn_18.Enabled = false;
                btn_18.BackColor = Color.Red;
                _18.Text = "Out";
            }
            else
            {
                btn_18.Enabled = true;
                btn_18.BackColor = Color.LightBlue;
                _18.Text = "";
            }

            if (EntradaText[0].Text == "19" || EntradaText[1].Text == "19" || EntradaText[2].Text == "19" || EntradaText[3].Text == "19" || EntradaText[4].Text == "19")
            {
                btn_19.Enabled = false;
                btn_19.BackColor = Color.YellowGreen;
                _19.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "19" || EntradaLabel2[1].Text == "19" || EntradaLabel2[2].Text == "19" || EntradaLabel2[3].Text == "19"
               || EntradaLabel2[4].Text == "19")
            {
                btn_19.Enabled = false;
                btn_19.BackColor = Color.LightGreen;
                _19.Text = "Sorte";
            }
            else if (Removido[0].Text == "19" || Removido[1].Text == "19" || Removido[2].Text == "19")
            {
                btn_19.Enabled = false;
                btn_19.BackColor = Color.Red;
                _19.Text = "Out";
            }
            else
            {
                btn_19.Enabled = true;
                btn_19.BackColor = Color.LightBlue;
                _19.Text = "";
            }

            if (EntradaText[0].Text == "20" || EntradaText[1].Text == "20" || EntradaText[2].Text == "20" || EntradaText[3].Text == "20" || EntradaText[4].Text == "20")
            {
                btn_20.Enabled = false;
                btn_20.BackColor = Color.YellowGreen;
                _20.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "20" || EntradaLabel2[1].Text == "20" || EntradaLabel2[2].Text == "20"
                || EntradaLabel2[3].Text == "20" || EntradaLabel2[4].Text == "20")
            {
                btn_20.Enabled = false;
                btn_20.BackColor = Color.LightGreen;
                _20.Text = "Luck";
            }
            else if (Removido[0].Text == "20" || Removido[1].Text == "20" || Removido[2].Text == "20")
            {
                btn_20.Enabled = false;
                btn_20.BackColor = Color.Red;
                _20.Text = "Out";
            }
            else
            {
                btn_20.Enabled = true;
                btn_20.BackColor = Color.LightBlue;
                _20.Text = "";
            }

            if (EntradaText[0].Text == "21" || EntradaText[1].Text == "21" || EntradaText[2].Text == "21" || EntradaText[3].Text == "21" || EntradaText[4].Text == "21")
            {
                btn_21.Enabled = false;
                btn_21.BackColor = Color.YellowGreen;
                _21.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "21" || EntradaLabel2[1].Text == "21"
            || EntradaLabel2[2].Text == "21" || EntradaLabel2[3].Text == "21" || EntradaLabel2[4].Text == "21")
            {
                btn_21.Enabled = false;
                btn_21.BackColor = Color.LightGreen;
                _21.Text = "Luck";
            }
            else if (Removido[0].Text == "21" || Removido[1].Text == "21" || Removido[2].Text == "21")
            {
                btn_21.Enabled = false;
                btn_21.BackColor = Color.Red;
                _21.Text = "Out";
            }
            else
            {
                btn_21.Enabled = true;
                btn_21.BackColor = Color.LightBlue;
                _21.Text = "";
            }

            if (EntradaText[0].Text == "22" || EntradaText[1].Text == "22" || EntradaText[2].Text == "22" || EntradaText[3].Text == "22" || EntradaText[4].Text == "22")
            {
                btn_22.Enabled = false;
                btn_22.BackColor = Color.YellowGreen;
                _22.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "22" || EntradaLabel2[1].Text == "22" || EntradaLabel2[2].Text == "22" || EntradaLabel2[3].Text == "22" || EntradaLabel2[3].Text == "22")
            {
                btn_22.Enabled = false;
                btn_22.BackColor = Color.LightGreen;
                _22.Text = "Luck";
            }
            else if (Removido[0].Text == "22" || Removido[1].Text == "22" || Removido[2].Text == "22")
            {
                btn_22.Enabled = false;
                btn_22.BackColor = Color.Red;
                _22.Text = "Out";
            }
            else
            {
                btn_22.Enabled = true;
                btn_22.BackColor = Color.LightBlue;
                _22.Text = "";
            }

            if (EntradaText[0].Text == "23" || EntradaText[1].Text == "23" || EntradaText[2].Text == "23" || EntradaText[3].Text == "23" || EntradaText[4].Text == "23")
            {
                btn_23.Enabled = false;
                btn_23.BackColor = Color.YellowGreen;
                _23.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "23" || EntradaLabel2[1].Text == "23" || EntradaLabel2[2].Text == "23" || EntradaLabel2[3].Text == "23" || EntradaLabel2[4].Text == "23")
            {
                btn_23.Enabled = false;
                btn_23.BackColor = Color.LightGreen;
                _23.Text = "Luck";
            }
            else if (Removido[0].Text == "23" || Removido[1].Text == "23" || Removido[2].Text == "23")
            {
                btn_23.Enabled = false;
                btn_23.BackColor = Color.Red;
                _23.Text = "Out";
            }
            else
            {
                btn_23.Enabled = true;
                btn_23.BackColor = Color.LightBlue;
                _23.Text = "";
            }

            if (EntradaText[0].Text == "24" || EntradaText[1].Text == "24" || EntradaText[2].Text == "24" || EntradaText[3].Text == "24" || EntradaText[4].Text == "24")
            {
                btn_24.Enabled = false;
                btn_24.BackColor = Color.YellowGreen;
                _24.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "24" || EntradaLabel2[1].Text == "24" || EntradaLabel2[2].Text == "24" || EntradaLabel2[3].Text == "24" || EntradaLabel2[4].Text == "24")
            {
                btn_24.Enabled = false;
                btn_24.BackColor = Color.LightGreen;
                _24.Text = "Luck";
            }
            else if (Removido[0].Text == "24" || Removido[1].Text == "24" || Removido[2].Text == "24")
            {
                btn_24.Enabled = false;
                btn_24.BackColor = Color.Red;
                _24.Text = "Out";
            }
            else
            {
                btn_24.Enabled = true;
                btn_24.BackColor = Color.LightBlue;
                _24.Text = "";
            }

            if (EntradaText[0].Text == "25" || EntradaText[1].Text == "25" || EntradaText[2].Text == "25" || EntradaText[3].Text == "25" || EntradaText[4].Text == "25")
            {
                btn_25.Enabled = false;
                btn_25.BackColor = Color.YellowGreen;
                _25.Text = "Fixed";
            }
            else if (EntradaLabel2[0].Text == "25" || EntradaLabel2[1].Text == "25" || EntradaLabel2[2].Text == "25" || EntradaLabel2[3].Text == "25" || EntradaLabel2[4].Text == "25")
            {
                btn_25.Enabled = false;
                btn_25.BackColor = Color.LightGreen;
                _25.Text = "Luck";
            }
            else if (Removido[0].Text == "25" || Removido[1].Text == "25" || Removido[2].Text == "25")
            {
                btn_25.Enabled = false;
                btn_25.BackColor = Color.Red;
                _25.Text = "Out";
            }
            else
            {
                btn_25.Enabled = true;
                btn_25.BackColor = Color.LightBlue;
                _25.Text = "";
            }

        }

       
    }
}
