using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda
{
    public partial class FormCadastroContato : Form
    {
        public string operacao = "";// esta propriedade serve para informar que tipo de operacao o usuario ira fazer
                                    // quando clicar no botao salvar..se e para inserir ou alterar um cadastro.

        public FormCadastroContato()
        {
            InitializeComponent();
        }

        public void AlterarBotoes( int op)
        {
            

            panelDados.Enabled = false; //desativa acesso do painel de dados
            buttonInserir.Enabled = false; // desativa o acesso aos botoes
            buttonLocalizar.Enabled = false;
            buttonAlterar.Enabled = false;
            buttonExcluir.Enabled = false;
            buttonSalvar.Enabled = false;
            buttonCancelar.Enabled = false;

            if (op == 1)
            {
                buttonInserir.Enabled = true;
                buttonLocalizar.Enabled = true;
            }

            if (op == 2)
            {
                panelDados.Enabled = true;
                buttonSalvar.Enabled = true;
                buttonCancelar.Enabled = true;
            }

            if (op ==3)
            {
                buttonExcluir.Enabled = true;
                buttonAlterar.Enabled = true;
                buttonCancelar.Enabled = true;
            }

        }

        public void LimpaCampos() // metodo para limpar os campos de texto
        {
            textCodigo.Clear(); // limpa o campo de texto Codigo
            textNome.Clear(); // limpa o campo de texto Nome
            textEmail.Clear(); // limpa o campo de texto Email
            textTelefone.Clear(); // limpa o campo de texto Telefone
            textRua.Clear(); // limpa o campo de texto Rua
            textBairro.Clear(); // limpa o campo de texto Bairro
            textCidade.Clear(); // limpa o campo de texto Cidade
            textEstado.Clear(); // limpa o campo de texto Estado
            textCep.Clear(); // limpa o campo de texto Cep
        }

        private void FormCadastroContato_Load(object sender, EventArgs e)
        {
            this.AlterarBotoes(1); // executar o metodo AlterarBotoes
        }

        private void buttonInserir_Click(object sender, EventArgs e)
        {
            this.operacao = "inserir";
            this.AlterarBotoes(2);// executar o metodo AlterarBotoes com op == 2
                                  // vai liberar o painel, botoes salvar e cancelar
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.AlterarBotoes(1);
            this.LimpaCampos();
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {

            try //excessao TRY/CATCH
            {

           
            Contato contato = new Contato();

                if (textNome.Text.Length <= 0) //validacao
                {
                    MessageBox.Show("Nome obrigatório!");
                    return;
                }

            //aqui vai pegar todos os dados dos campos de texto e inseri-los nas propriedades da classe Contato

            contato.Nome = textNome.Text; //A propriedade Nome vai receber vai receber valores do textNome
            contato.Email = textEmail.Text; //A propriedade Email vai receber vai receber valores do textEmail
            contato.Fone = textTelefone.Text;
            contato.Rua = textRua.Text;
            contato.Bairro = textBairro.Text;
            contato.Cidade = textCidade.Text;
            contato.Estado = textEstado.Text;
            contato.Cep = textCep.Text;

            String strConexao = "Data Source=MARILANE-PC;Initial Catalog=Agenda;Integrated Security=True";
            Conexao conexao = new Conexao(strConexao);
            DALContato dal = new DALContato(conexao);

            if (this.operacao == "inserir")
            {

                dal.Incluir(contato);
                MessageBox.Show("O Código gerado foi : " + contato.Codigo.ToString());

            
                
            }
            else
            {
                contato.Codigo = Convert.ToInt32(textCodigo.Text);
                dal.Alterar(contato);
                MessageBox.Show("Registro alterado!");
                //alterar o contato que esta na tela

            }

            this.AlterarBotoes(1);
            this.LimpaCampos();

            }
            catch(Exception erro)//excessao TRY/CCATCH
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void buttonLocalizar_Click(object sender, EventArgs e)
        {
            FormConsultaContatos f = new FormConsultaContatos();
            f.ShowDialog();
            if (f.codigo != 0)
            {
                String strConexao = "Data Source=MARILANE-PC;Initial Catalog=Agenda;Integrated Security=True";
                Conexao conexao = new Conexao(strConexao);
                DALContato dal = new DALContato(conexao);
                Contato contato = dal.carrregaContato(f.codigo);

                textCodigo.Text = contato.Codigo.ToString();
                textNome.Text = contato.Nome;
                textEmail.Text = contato.Email;
                textTelefone.Text = contato.Fone;
                textRua.Text = contato.Rua;
                textBairro.Text = contato.Bairro;
                textCidade.Text = contato.Cidade;
                textEstado.Text = contato.Estado;
                textCep.Text = contato.Cep;
                this.AlterarBotoes(3);
                
            }


            f.Dispose();

        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            this.operacao = "alterar";
            this.AlterarBotoes(2);
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Deseja excluir o registro?", "Aviso", MessageBoxButtons.YesNo);

            if (d.ToString() == "Yes")
            {
                String strConexao = "Data Source=MARILANE-PC;Initial Catalog=Agenda;Integrated Security=True";
                Conexao conexao = new Conexao(strConexao);
                DALContato dal = new DALContato(conexao);
                dal.Excluir(Convert.ToInt32(textCodigo.Text));
                this.AlterarBotoes(1);
                this.LimpaCampos();
            }
        }
    }
}
