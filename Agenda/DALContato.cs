using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Agenda
{
    class DALContato
    {
        private Conexao objconexao;

        public DALContato(Conexao conexao)
        {
            this.objconexao = conexao;
        }

        public void Incluir(Contato contato)
        {
            //O sqlCommand serve para executar um comando no sql
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = objconexao.ObjetoConexao;
            cmd.CommandText = "insert into contato(con_nome, con_email, con_fone, con_rua, con_bairro,"+
                "con_cidade, con_estado, con_cep) values (@nome, @email, @fone, @rua, @bairro, @cidade, @estado, @cep); " +
                "select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@nome", contato.Nome);
            cmd.Parameters.AddWithValue("@email", contato.Email);
            cmd.Parameters.AddWithValue("@fone", contato.Fone);
            cmd.Parameters.AddWithValue("@rua", contato.Rua);
            cmd.Parameters.AddWithValue("@bairro", contato.Bairro);
            cmd.Parameters.AddWithValue("@cidade", contato.Cidade);
            cmd.Parameters.AddWithValue("@estado", contato.Estado);
            cmd.Parameters.AddWithValue("@cep", contato.Cep);
            objconexao.Conectar();
            contato.Codigo = Convert.ToInt32(cmd.ExecuteScalar());
            objconexao.Desconectar();

        }

        public void Alterar(Contato contato)
        {
            //O sqlCommand serve para executar um comando no sql
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = objconexao.ObjetoConexao;
            cmd.CommandText = "update contato set con_nome=@nome, con_email=@email, con_fone=@fone, con_rua=@rua, con_bairro=@bairro," +
                "con_cidade=@cidade, con_estado=@estado, con_cep=@cep where con_cod = @cod";
            cmd.Parameters.AddWithValue("@nome", contato.Nome);
            cmd.Parameters.AddWithValue("@email", contato.Email);
            cmd.Parameters.AddWithValue("@fone", contato.Fone);
            cmd.Parameters.AddWithValue("@rua", contato.Rua);
            cmd.Parameters.AddWithValue("@bairro", contato.Bairro);
            cmd.Parameters.AddWithValue("@cidade", contato.Cidade);
            cmd.Parameters.AddWithValue("@estado", contato.Estado);
            cmd.Parameters.AddWithValue("@cep", contato.Cep);
            cmd.Parameters.AddWithValue("@cod",contato.Codigo);
            objconexao.Conectar();
            cmd.ExecuteNonQuery();
            objconexao.Desconectar();

        }

        public void Excluir( int codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = objconexao.ObjetoConexao;
            cmd.CommandText = "delete from contato where con_cod = @cod";
            cmd.Parameters.AddWithValue("@cod", codigo);
            objconexao.Conectar();
            cmd.ExecuteNonQuery();
            objconexao.Desconectar();
        }

        public DataTable Localizar( String valor)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from contato where con_nome like '%" + valor + "%'", objconexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public Contato carrregaContato( int codigo)
        {
            Contato modelo = new Contato();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = objconexao.ObjetoConexao;
            cmd.CommandText = "select * from contato where con_cod =" + codigo.ToString();
            objconexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.Codigo = Convert.ToInt32(registro["con_cod"]);
                modelo.Nome = Convert.ToString(registro["con_nome"]);
                modelo.Email = Convert.ToString(registro["con_email"]);
                modelo.Fone = Convert.ToString(registro["con_fone"]);
                modelo.Rua = Convert.ToString(registro["con_rua"]);
                modelo.Bairro = Convert.ToString(registro["con_bairro"]);
                modelo.Cidade = Convert.ToString(registro["con_cidade"]);
                modelo.Estado = Convert.ToString(registro["con_estado"]);
                modelo.Cep = Convert.ToString(registro["con_cep"]);
                
            }

            return modelo;
        }

       
    }
}
