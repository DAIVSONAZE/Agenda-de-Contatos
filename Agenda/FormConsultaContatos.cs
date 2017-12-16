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
    public partial class FormConsultaContatos : Form
    {

        public int codigo = 0; //Propriedade utilizada para armazenar o código do cliente
                               // que for selecionado pelo usuario

        public FormConsultaContatos()
        {
            InitializeComponent();
        }

        private void buttonExecutar_Click(object sender, EventArgs e)
        {
            Conexao cx = new Conexao("Data Source=MARILANE-PC;Initial Catalog=Agenda;Integrated Security=True");
            DALContato dal = new DALContato(cx);
            dgvDados.DataSource = dal.Localizar(textValor.Text);
        }

        private void dgvDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0) // A propriedade 'ColumnIndex' do objeto 'e' indica 
                                   // qual a coluna foi selecionada pelo usuario
            {
                this.codigo = Convert.ToInt32(dgvDados.Rows[e.RowIndex].Cells[0].Value);
                this.Close();

            }
        }
    }
}
