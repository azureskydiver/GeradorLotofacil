using Controle;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inicio
{
    public partial class frmSalvaResultados : Form
    {
        private TextBox[] ResultText = new TextBox[15];
        public frmSalvaResultados()
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
            int i, esquerda = 105, topo = 230;
            for (i = 0; i < 15; i++)
            {
                ResultText[i] = new TextBox();
                //ResultText[i].Name = "label" + i;
                //ResultText[i].Text = ResultText[i].Name;
                //ResultText[i].Anchor = AnchorStyles.Right;
                //ResultText[i].Location = new Point(ResultText[i].Location.X + 20, ResultText[i].Location.Y + 300);
                ResultText[i].Left = esquerda;
                ResultText[i].Top = topo;
                ResultText[i].Anchor = new AnchorStyles();
                ResultText[i].BackColor = Color.Gray;
                ResultText[i].ForeColor = Color.White;
                ResultText[i].Size = new Size(45, 30);
                ResultText[i].Font = new Font("", 15, FontStyle.Bold);
                ResultText[i].TabIndex = 2;
                ResultText[i].MaxLength = 2;
                ResultText[i].BorderStyle = BorderStyle.FixedSingle;
                ResultText[i].KeyPress += new KeyPressEventHandler(keypressed);
                this.Controls.Add(ResultText[i]);
                esquerda += (ResultText[i].Width + 5);
                ResultText[i].Tag = i;
            }
        }
        private void keypressed(Object o, KeyPressEventArgs e)
        {
            Program.IntNumber(e);
        }
        private void FormataTexto()
        {
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvarGrid.Enabled = false;
            btnBuscar.Enabled = false;
        }
        private void LimpaTexto()
        {
            txtConcurso.Text = "";
            ResultText[0].Text = "";
            ResultText[1].Text = "";
            ResultText[2].Text = "";
            ResultText[3].Text = "";
            ResultText[4].Text = "";
            ResultText[5].Text = "";
            ResultText[6].Text = "";
            ResultText[7].Text = "";
            ResultText[8].Text = "";
            ResultText[9].Text = "";
            ResultText[10].Text = "";
            ResultText[11].Text = "";
            ResultText[12].Text = "";
            ResultText[13].Text = "";
            ResultText[14].Text = "";
            cbDB.Checked = false;
            cbEditar.Checked = false;
        }
        private Resultado PreencherInformacoes()
        {
            Resultado SorteioBase = new Resultado();

            SorteioBase.Concurso = Convert.ToInt32(txtConcurso.Text);
            SorteioBase._01 = Convert.ToInt32(ResultText[0].Text);
            SorteioBase._02 = Convert.ToInt32(ResultText[1].Text);
            SorteioBase._03 = Convert.ToInt32(ResultText[2].Text);
            SorteioBase._04 = Convert.ToInt32(ResultText[3].Text);
            SorteioBase._05 = Convert.ToInt32(ResultText[4].Text);
            SorteioBase._06 = Convert.ToInt32(ResultText[5].Text);
            SorteioBase._07 = Convert.ToInt32(ResultText[6].Text);
            SorteioBase._08 = Convert.ToInt32(ResultText[7].Text);
            SorteioBase._09 = Convert.ToInt32(ResultText[8].Text);
            SorteioBase._10 = Convert.ToInt32(ResultText[9].Text);
            SorteioBase._11 = Convert.ToInt32(ResultText[10].Text);
            SorteioBase._12 = Convert.ToInt32(ResultText[11].Text);
            SorteioBase._13 = Convert.ToInt32(ResultText[12].Text);
            SorteioBase._14 = Convert.ToInt32(ResultText[13].Text);
            SorteioBase._15 = Convert.ToInt32(ResultText[14].Text);

            return SorteioBase;
        }

        private void salvar()
        {
            if (!string.IsNullOrWhiteSpace(txtConcurso.Text))
            {
                if (txtConcurso.TextLength != 4)
                {
                    MessageBox.Show("Insira um Concurso Valido para salvar no Banco!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    
                    if (ResultText[1].Text != "" && ResultText[2].Text != "" && ResultText[3].Text != "" && ResultText[4].Text != "" &&
                        ResultText[5].Text != "" && ResultText[6].Text != "" && ResultText[7].Text != "" && ResultText[8].Text != "" && ResultText[9].Text != "" &&
                        ResultText[10].Text != "" && ResultText[11].Text != "" && ResultText[12].Text != "" && ResultText[13].Text != "" && ResultText[14].Text != "" )
                    {
                        string Saida = Logica.Add(PreencherInformacoes());
                        MessageBox.Show(Saida, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Insira um Resultado Valido para salvar no Banco!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                   
                }
               
            }
            else
            {
                MessageBox.Show("Insira um Concurso para salvar no Banco!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void editar()
        {
            if (!string.IsNullOrWhiteSpace(txtConcurso.Text))
            {
                if (txtConcurso.TextLength != 4)
                {
                    MessageBox.Show("Insira um Concurso Valido para salvar no Banco!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    if (ResultText[0].Text != ""  && ResultText[1].Text != "" && ResultText[2].Text != "" && ResultText[3].Text != "" && ResultText[4].Text != "" &&
                        ResultText[5].Text != "" && ResultText[6].Text != "" && ResultText[7].Text != "" && ResultText[8].Text != "" && ResultText[9].Text != "" &&
                        ResultText[10].Text != "" && ResultText[11].Text != "" && ResultText[12].Text != "" && ResultText[13].Text != "" && ResultText[14].Text != "")
                    {
                        string Saida = Logica.Update(PreencherInformacoes());

                        MessageBox.Show(Saida, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Insira um Resultado Valido para Editar!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
               
            }
            else
            {
                MessageBox.Show("Insira um Concurso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private string Excluir(string Concurso)
        {
           
            string Saida = string.Format("O Concuso {0} foi excluido com sucesso!", Concurso);
            try
            {
                Logica.Delete(txtConcurso.Text);
            }
            catch (Exception exc)
            {
                Saida = string.Format("Ocorreu um erro ao excluir o Concurso {0}", Concurso, exc.Message);  //no momento de excluir esta entrando nesta parte
            }
            return Saida;
            //ResultText[0].Text = "";
            //ResultText[1].Text = "";
            //ResultText[2].Text = "";
            //ResultText[3].Text = "";
            //ResultText[4].Text = "";
            //ResultText[5].Text = "";
            //ResultText[6].Text = "";
            //ResultText[7].Text = "";
            //ResultText[8].Text = "";
            //ResultText[9].Text = "";
            //ResultText[10].Text = "";
            //ResultText[11].Text = "";
            //ResultText[12].Text = "";
            //ResultText[13].Text = "";
            //ResultText[14].Text = "";
            //if (!string.IsNullOrWhiteSpace(txtConcurso.Text))
            //{
            //    string Saida = Logica.Delete(txtConcurso.Text);
            //    MessageBox.Show(Saida, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Insira um Concurso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            
        }
        //private void resultUltimat()// salva lista de numeros do sorteio (Resultado)
        //{
        //    // salva ultimo sorteio no arquivo texto na primeira linha e vai acrescentando linhas
        //    const string nomeArquivo = @"C:\BoaSorte\Resultados\ResultadosConcurso.txt";
        //    List<string> linhas = File.ReadLines(nomeArquivo).ToList(); // Passo 1

        //    if (linhas.IndexOf(txtConcurso.Text + "," + ResultadoLabel[0].Text + "," + ResultadoLabel[1].Text + "," + ResultadoLabel[2].Text + "," + ResultadoLabel[3].Text + "," + ResultadoLabel[4].Text
        //       + "," + ResultadoLabel[5].Text + "," + ResultadoLabel[6].Text + "," + ResultadoLabel[7].Text + "," + ResultadoLabel[8].Text + "," + ResultadoLabel[9].Text
        //       + "," + ResultadoLabel[10].Text + "," + ResultadoLabel[11].Text + "," + ResultadoLabel[12].Text + "," + ResultadoLabel[13].Text + "," + ResultadoLabel[14].Text) >= 0)
        //    { }
        //    else
        //    {
        //        linhas.Insert(0, txtConcurso.Text + "," + ResultadoLabel[0].Text + "," + ResultadoLabel[1].Text + "," + ResultadoLabel[2].Text + "," + ResultadoLabel[3].Text + "," + ResultadoLabel[4].Text
        //     + "," + ResultadoLabel[5].Text + "," + ResultadoLabel[6].Text + "," + ResultadoLabel[7].Text + "," + ResultadoLabel[8].Text + "," + ResultadoLabel[9].Text
        //     + "," + ResultadoLabel[10].Text + "," + ResultadoLabel[11].Text + "," + ResultadoLabel[12].Text + "," + ResultadoLabel[13].Text + "," + ResultadoLabel[14].Text); // Passo 2
        //        File.WriteAllLines(nomeArquivo, linhas);
        //    }
        //}
       
        private void frmSalvaResultados_Load(object sender, EventArgs e)
        {
            FormataTexto();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void cbDB_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDB.Checked == true)
            {
                cbEditar.Enabled = false;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                btnSalvarGrid.Enabled = true;
                btnBuscar.Enabled = false;
            }
            else
            {
                cbEditar.Enabled = true;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                btnSalvarGrid.Enabled = false;
                btnBuscar.Enabled = false;
            }
        }

        private void cbEditar_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEditar.Checked == true)
            {
                cbDB.Enabled = false;
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
                btnSalvarGrid.Enabled = false;
                btnBuscar.Enabled = true;
            }
            else
            {
                cbDB.Enabled = true;
                //btnEditar.Enabled = false;
                //btnExcluir.Enabled = false;
                btnSalvarGrid.Enabled = false;
                btnBuscar.Enabled = false;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpaTexto();
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtConcurso.Text))
            {
                if (MessageBox.Show("Você deseja mesmo excluir o cliente?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string saida = Excluir(txtConcurso.Text);

                    MessageBox.Show(saida, "Saida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
              
            }
            else
            {
                MessageBox.Show("Insira um valor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                cbEditar.Checked = false;
                cbDB.Enabled = true;
            }

            //if (!string.IsNullOrWhiteSpace(txtConcurso.Text))
            //{
            //    if (MessageBox.Show("Você deseja mesmo excluir o cliente?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        Excluir(txtConcurso.Text);
            //        MessageBox.Show("Concurso Excluido com Sucesso!");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Insira um Concurso para Excluir!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        btnEditar.Enabled = false;
            //        btnExcluir.Enabled = false;
            //        cbEditar.Checked = false;
            //        cbDB.Enabled = true;
            //    }

            //}

            LimpaTexto();       
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtConcurso.Text))
            {
                editar();
            }
            else
            {
                MessageBox.Show("Insira um Concurso para Editar!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                cbEditar.Checked = false;
                cbDB.Checked = false;
                cbDB.Enabled = true;
            }
        }

        private void btnSalvarGrid_Click(object sender, EventArgs e)
        {


            if (cbDB.Checked == true)
            {
                salvar();
            }

            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            cbEditar.Checked = false;
            cbDB.Checked = false;
            btnSalvarGrid.Enabled = false;
            cbDB.Checked = false;
            cbEditar.Checked = false;
            LimpaTexto();
        }
        private void SearchResult()
        {
            var contest = txtConcurso.Text;
            var result = new Resultado();
            if (!string.IsNullOrWhiteSpace(result.ToString()))
            {
                result = Logica.SearchResult(Convert.ToInt32(contest));

                ResultText[0].Text = result._01.ToString("D2");
                ResultText[1].Text = result._02.ToString("D2");
                ResultText[2].Text = result._03.ToString("D2");
                ResultText[3].Text = result._04.ToString("D2");
                ResultText[4].Text = result._05.ToString("D2");
                ResultText[5].Text = result._06.ToString("D2");
                ResultText[6].Text = result._07.ToString("D2");
                ResultText[7].Text = result._08.ToString("D2");
                ResultText[8].Text = result._09.ToString("D2");
                ResultText[9].Text = result._10.ToString("D2");
                ResultText[10].Text = result._11.ToString("D2");
                ResultText[11].Text = result._12.ToString("D2");
                ResultText[12].Text = result._13.ToString("D2");
                ResultText[13].Text = result._14.ToString("D2");
                ResultText[14].Text = result._15.ToString("D2");
            }
            else
            {
                result = null;
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtConcurso.Text))
            {
                if (Logica.VerificarExistencia(Convert.ToInt32(txtConcurso.Text)) == false)
                {
                    MessageBox.Show("Numero de Concurso não existe");
                    btnEditar.Enabled = false;
                    btnExcluir.Enabled = false;
                    cbEditar.Checked = false;
                    cbDB.Checked = false;
                    cbDB.Enabled = true;
                    LimpaTexto();
                    LimpaTexto();
                }
                else
                {
                   SearchResult();
                }
            }
            else
            {
                MessageBox.Show("Insira um Concurso para buscar!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                cbEditar.Checked = false;
                cbDB.Checked = false;
                cbDB.Enabled = true;
                LimpaTexto();
            }
        }

        private void txtConcurso_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.IntNumber(e);
        }
    }
}
