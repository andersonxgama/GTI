using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using WCFServiceHost;

[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
public class ClienteService : IClienteService
{
    // Altere a connection string pro seu banco local
    private string connectionString = @"Server=.\SQLEXPRESS;Database=GTI;Trusted_Connection=True;";

    public void Inserir(Cliente cliente)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            // Insere cliente
            string sqlCliente = @"
                INSERT INTO Clientes 
                (Nome, CPF, RG, DataExpedicao, OrgaoExpedicao, UFExpedicao, DataNascimento, Sexo, EstadoCivil)
                VALUES (@Nome, @CPF, @RG, @DataExpedicao, @OrgaoExpedicao, @UFExpedicao, @DataNascimento, @Sexo, @EstadoCivil);
                SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(sqlCliente, con);
            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
            cmd.Parameters.AddWithValue("@RG", (object)cliente.RG ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DataExpedicao", cliente.DataExpedicao);
            cmd.Parameters.AddWithValue("@OrgaoExpedicao", (object)cliente.OrgaoExpedicao ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UFExpedicao", (object)cliente.UFExpedicao ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
            cmd.Parameters.AddWithValue("@Sexo", (object)cliente.Sexo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EstadoCivil", (object)cliente.EstadoCivil ?? DBNull.Value);

            int clienteId = Convert.ToInt32(cmd.ExecuteScalar());

            // Insere endereço
            string sqlEndereco = @"
                INSERT INTO Endereco 
                (ClienteId, CEP, Logradouro, Numero, Complemento, Bairro, Cidade, UF)
                VALUES (@ClienteId, @CEP, @Logradouro, @Numero, @Complemento, @Bairro, @Cidade, @UF)";

            SqlCommand cmdEnd = new SqlCommand(sqlEndereco, con);
            cmdEnd.Parameters.AddWithValue("@ClienteId", clienteId);
            cmdEnd.Parameters.AddWithValue("@CEP", (object)cliente.Endereco.CEP ?? DBNull.Value);
            cmdEnd.Parameters.AddWithValue("@Logradouro", (object)cliente.Endereco.Logradouro ?? DBNull.Value);
            cmdEnd.Parameters.AddWithValue("@Numero", (object)cliente.Endereco.Numero ?? DBNull.Value);
            cmdEnd.Parameters.AddWithValue("@Complemento", (object)cliente.Endereco.Complemento ?? DBNull.Value);
            cmdEnd.Parameters.AddWithValue("@Bairro", (object)cliente.Endereco.Bairro ?? DBNull.Value);
            cmdEnd.Parameters.AddWithValue("@Cidade", (object)cliente.Endereco.Cidade ?? DBNull.Value);
            cmdEnd.Parameters.AddWithValue("@UF", (object)cliente.Endereco.UF ?? DBNull.Value);

            cmdEnd.ExecuteNonQuery();
        }
    }

    public void Atualizar(Cliente cliente)
    {
        using (var con = new SqlConnection(connectionString))
        {
            con.Open();

            // Atualiza os dados do cliente
            using (var cmdCli = new SqlCommand(
                @"UPDATE Clientes SET 
                Nome=@Nome, CPF=@CPF, RG=@RG, DataExpedicao=@DataExpedicao,
                OrgaoExpedicao=@OrgaoExpedicao, UFExpedicao=@UFExpedicao, 
                DataNascimento=@DataNascimento, Sexo=@Sexo, EstadoCivil=@EstadoCivil
              WHERE Id=@Id", con))
            {
                cmdCli.Parameters.AddWithValue("@Id", cliente.Id);
                cmdCli.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmdCli.Parameters.AddWithValue("@CPF", cliente.CPF);
                cmdCli.Parameters.AddWithValue("@RG", cliente.RG);
                cmdCli.Parameters.AddWithValue("@DataExpedicao", cliente.DataExpedicao);
                cmdCli.Parameters.AddWithValue("@OrgaoExpedicao", cliente.OrgaoExpedicao);
                cmdCli.Parameters.AddWithValue("@UFExpedicao", cliente.UFExpedicao);
                cmdCli.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                cmdCli.Parameters.AddWithValue("@Sexo", cliente.Sexo);
                cmdCli.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);

                cmdCli.ExecuteNonQuery();
            }

            // Atualiza os dados do endereço
            using (var cmdEnd = new SqlCommand(
                @"UPDATE Endereco SET 
                CEP=@CEP, Logradouro=@Logradouro, Numero=@Numero, Complemento=@Complemento,
                Bairro=@Bairro, Cidade=@Cidade, UF=@UF
              WHERE ClienteId=@Id", con))
            {
                cmdEnd.Parameters.AddWithValue("@Id", cliente.Id);
                cmdEnd.Parameters.AddWithValue("@CEP", cliente.Endereco.CEP);
                cmdEnd.Parameters.AddWithValue("@Logradouro", cliente.Endereco.Logradouro);
                cmdEnd.Parameters.AddWithValue("@Numero", cliente.Endereco.Numero);
                cmdEnd.Parameters.AddWithValue("@Complemento", cliente.Endereco.Complemento);
                cmdEnd.Parameters.AddWithValue("@Bairro", cliente.Endereco.Bairro);
                cmdEnd.Parameters.AddWithValue("@Cidade", cliente.Endereco.Cidade);
                cmdEnd.Parameters.AddWithValue("@UF", cliente.Endereco.UF);

                cmdEnd.ExecuteNonQuery();
            }
        }
    }


    public void Excluir(int id)
    {
        using (var con = new SqlConnection(connectionString))
        {
            con.Open();

            // Deleta os endereços primeiro
            using (var cmdEnd = new SqlCommand("DELETE FROM Endereco WHERE ClienteId=@Id", con))
            {
                cmdEnd.Parameters.AddWithValue("@Id", id);
                cmdEnd.ExecuteNonQuery();
            }

            // Depois deleta o cliente
            using (var cmdCli = new SqlCommand("DELETE FROM Clientes WHERE Id=@Id", con))
            {
                cmdCli.Parameters.AddWithValue("@Id", id);
                cmdCli.ExecuteNonQuery();
            }
        }
    }


    public List<Cliente> Listar()
    {
        List<Cliente> lista = new List<Cliente>();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            string sql = @"
                SELECT c.Id, c.Nome, c.CPF, c.RG, c.DataExpedicao, c.OrgaoExpedicao, c.UFExpedicao, 
                       c.DataNascimento, c.Sexo, c.EstadoCivil,
                       e.CEP, e.Logradouro, e.Numero, e.Complemento, e.Bairro, e.Cidade, e.UF
                FROM Clientes c
                LEFT JOIN Endereco e ON c.Id = e.ClienteId";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var cliente = new Cliente
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nome = reader["Nome"].ToString(),
                    CPF = reader["CPF"].ToString(),
                    RG = reader["RG"].ToString(),
                    DataExpedicao = reader["DataExpedicao"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["DataExpedicao"]),
                    OrgaoExpedicao = reader["OrgaoExpedicao"].ToString(),
                    UFExpedicao = reader["UFExpedicao"].ToString(),
                    DataNascimento = reader["DataNascimento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["DataNascimento"]),
                    Sexo = reader["Sexo"].ToString(),
                    EstadoCivil = reader["EstadoCivil"].ToString(),

                    Endereco = new Endereco
                    {
                        CEP = reader["CEP"].ToString(),
                        Logradouro = reader["Logradouro"].ToString(),
                        Numero = reader["Numero"].ToString(),
                        Complemento = reader["Complemento"].ToString(),
                        Bairro = reader["Bairro"].ToString(),
                        Cidade = reader["Cidade"].ToString(),
                        UF = reader["UF"].ToString()
                    }
                };

                lista.Add(cliente);
            }
        }

        return lista;
    }
}
