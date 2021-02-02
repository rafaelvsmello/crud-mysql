using MySql.Data.MySqlClient;

namespace Cadastro.Models
{
    public class Database
    {
        public MySqlConnection ObterConexao()
        {
            string conexao = "Server=remotemysql.com;Port=3306;Database=h4ZDYebj9Y;Uid=h4ZDYebj9Y;Pwd=fxNkPfX8bm;"; // Banco de dados de testes
            return new MySqlConnection(conexao);
        }
    }
}
