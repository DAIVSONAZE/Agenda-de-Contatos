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
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void btContato_Click(object sender, EventArgs e)
        {
            FormCadastroContato f = new FormCadastroContato();
            f.ShowDialog();
            f.Dispose();
        }
    }
}
