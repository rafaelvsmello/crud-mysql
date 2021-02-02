using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Cadastro.Models;

namespace Cadastro
{
    public partial class Form : System.Windows.Forms.Form
    {
        Database instanciaMySql = new Database();
        MySqlCommand cmd;
        MySqlDataAdapter da;        
        string sql;

        private void Read()
        {
            try
            {
                using (MySqlConnection conexao = instanciaMySql.ObterConexao())
                {
                    sql = "SELECT * FROM cliente;";
                    DataSet ds = new DataSet();
                    da = new MySqlDataAdapter(sql, conexao);
                    conexao.Open();
                    da.Fill(ds);
                    dgvCadastro.DataSource = ds.Tables[0];
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);                
            }            
        }

        private void Create()
        {
            try
            {
                using (MySqlConnection conexao = instanciaMySql.ObterConexao())
                {
                    sql = "INSERT INTO cliente VALUES (NULL, @NomeCliente, @EnderecoCliente, @TelefoneCliente);";
                    cmd = new MySqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("NomeCliente", txtNomeCliente.Text);
                    cmd.Parameters.AddWithValue("EnderecoCliente", txtEnderecoCliente.Text);
                    cmd.Parameters.AddWithValue("TelefoneCliente", txtTelefoneCliente.Text);
                    conexao.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        MessageBox.Show("Cadastro realizado com sucesso");                    
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void Update()
        {
            try
            {
                using (MySqlConnection conexao = instanciaMySql.ObterConexao())
                {
                    sql = "UPDATE cliente SET NomeCliente=@NomeCliente, EnderecoCliente=@EnderecoCliente, TelefoneCliente=@TelefoneCliente WHERE IdCliente=@IdCliente;";
                    cmd = new MySqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("IdCliente", txtId.Text);
                    cmd.Parameters.AddWithValue("NomeCliente", txtNomeCliente.Text);
                    cmd.Parameters.AddWithValue("EnderecoCliente", txtEnderecoCliente.Text);
                    cmd.Parameters.AddWithValue("TelefoneCliente", txtTelefoneCliente.Text);
                    conexao.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        MessageBox.Show("Cadastro alterado com sucesso");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void Delete()
        {
            try
            {
                using (MySqlConnection conexao = instanciaMySql.ObterConexao())
                {
                    sql = "DELETE FROM cliente WHERE IdCliente=@IdCliente;";
                    cmd = new MySqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("IdCliente", txtId.Text);                    
                    conexao.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        MessageBox.Show("Cadastro excluído com sucesso");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        public Form()
        {
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            Read();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            Create();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
                MessageBox.Show("Selecione um registro para alterar");            
            else
                Update();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
                MessageBox.Show("Selecione um registro para excluir");
            else
                Delete();
        }

        private void dgvCadastro_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCadastro.Rows.Count > 0)
            {
                txtId.Text = dgvCadastro.SelectedCells[0].Value.ToString();
                txtNomeCliente.Text = dgvCadastro.SelectedCells[1].Value.ToString();
                txtEnderecoCliente.Text = dgvCadastro.SelectedCells[2].Value.ToString();
                txtTelefoneCliente.Text = dgvCadastro.SelectedCells[3].Value.ToString();
            }
        }
    }
}
