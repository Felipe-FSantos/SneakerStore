using Microsoft.Data.SqlClient;
using System.Data;

namespace SneakerStoreLibAPI.Data
{
    public class DbSession : IDisposable
    {
        //Gerencia a conexão
        public IDbConnection Connection { get; }

        public DbSession(IConfiguration configuration)
        {
            //Connection string esta no arquivo appsetings.Json
            //Altere antes de rodar o projeto
            Connection = new SqlConnection(configuration
                .GetConnectionString("SneakerStoreDB"));

            //Abre conexão
            Connection.Open();
        }
        //Fecha a conexão
        public void Dispose() => Connection?.Dispose();
    }
}
