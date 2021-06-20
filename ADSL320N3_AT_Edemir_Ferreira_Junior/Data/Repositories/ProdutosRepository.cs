using System;
using System.Collections.Generic;
using System.Data;
using Data.Models;
using Microsoft.Data.SqlClient;

namespace Data.Repositories
{
    public class ProdutoRepository
    {
        private const string ConnectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Developer\Documents\2-Prof-Felipe\Aulas\Only TPs\AT-19-06-21\ADSL320N3_AT_Edemir_Ferreira_Junior\Data\Script\Mercado_Clientes.mdf;Integrated Security=True";
        public IEnumerable<ProdutoModel> GetAll(
            bool orderAscendant,
            string search = null)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            var commandText = "SELECT * FROM Produto";

            if (!string.IsNullOrWhiteSpace(search))
            {
                commandText += " WHERE NomeProduto LIKE @search";

                command.Parameters
                    .Add("@search", SqlDbType.NVarChar)
                    .Value = $"%{search}%";
            }

            var order = orderAscendant ? "ASC" : "DESC";
            commandText += $" ORDER BY NomeProduto {order}";

            command.CommandType = CommandType.Text;
            command.CommandText = commandText;

            var reader = command.ExecuteReader();

            var produtos = new List<ProdutoModel>();
            while (reader.Read())
            {
                var produto = new ProdutoModel
                {
                    IdProduto = reader.GetFieldValue<int>("IdProduto"),
                    NomeProduto = reader.GetFieldValue<string>("NomeProduto"),
                    CaracteristicasProduto = reader.GetFieldValue<string>("CaracteristicasProduto"),
                    QuantidadeProduto = reader.GetFieldValue<int>("QuantidadeProduto"),
                    DataFabricacao = reader.GetFieldValue<DateTime>("DataFabricacao"),
                };
                produtos.Add(produto);
            }

            return produtos;
        }

        

        public ProdutoModel GetById(int id)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT IdProduto, NomeProduto, CaracteristicasProduto, QuantidadeProduto, DataFabricacao FROM Produto WHERE IdProduto = @id;";

            command.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = id;

            var reader = command.ExecuteReader();

            var canRead = reader.Read();
            if (!canRead)
            {
                return null;
            }

            var produto = new ProdutoModel
            {
                IdProduto = reader.GetFieldValue<int>("IdProduto"),
                NomeProduto = reader.GetFieldValue<string>("NomeProduto"),
                CaracteristicasProduto = reader.GetFieldValue<string>("CaracteristicasProduto"),
                QuantidadeProduto = reader.GetFieldValue<int>("QuantidadeProduto"),
                DataFabricacao = reader.GetFieldValue<DateTime>("DataFabricacao"),
            };

            return produto;
        }

        public ProdutoModel Create(ProdutoModel produtoModel)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText =
                @"INSERT INTO produto" +
                "	(NomeProduto, CaracteristicasProduto, QuantidadeProduto, DataFabricacao)" +
                "	OUTPUT INSERTED.IdProduto" +
                "	VALUES (@NomeProduto, @CaracteristicasProduto, @QuantidadeProduto, @DataFabricacao);";

            command.Parameters
                .Add("@NomeProduto", SqlDbType.NVarChar)
                .Value = produtoModel.NomeProduto;
            command.Parameters
                .Add("@CaracteristicasProduto", SqlDbType.NVarChar)
                .Value = produtoModel.CaracteristicasProduto;
            command.Parameters
                .Add("@QuantidadeProduto", SqlDbType.Int)
                .Value = produtoModel.QuantidadeProduto;
            command.Parameters
                .Add("@DataFabricacao", SqlDbType.DateTime2)
                .Value = produtoModel.DataFabricacao;
            
            var scalarResult = command.ExecuteScalar();

            produtoModel.IdProduto = (int)scalarResult;

            return produtoModel;
        }

        public ProdutoModel Edit(ProdutoModel produtoModel)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = @"UPDATE Produto SET NomeProduto = @nomeProduto, CaracteristicasProduto = @caracteristicasProduto, QuantidadeProduto = @quantidadeProduto, DataFabricacao = @dataFabricacao WHERE IdProduto = @idProduto;";
                        
            command.Parameters
               .Add("@nomeProduto", SqlDbType.NVarChar)
               .Value = produtoModel.NomeProduto;
            command.Parameters
                .Add("@caracteristicasProduto", SqlDbType.NVarChar)
                .Value = produtoModel.CaracteristicasProduto;
            command.Parameters
                .Add("@quantidadeProduto", SqlDbType.Int)
                .Value = produtoModel.QuantidadeProduto;
            command.Parameters
                .Add("@dataFabricacao", SqlDbType.DateTime)
                .Value = produtoModel.DataFabricacao;
            command.Parameters
               .Add("@idProduto", SqlDbType.Int)
               .Value = produtoModel.IdProduto;

            command.ExecuteScalar();

            return produtoModel;
        }

        public void Delete(int id)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Produto WHERE IdProduto = @id;";

            command.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = id;

            command.ExecuteScalar();
        }
    }
}