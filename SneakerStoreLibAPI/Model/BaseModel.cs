using Dapper;
using Microsoft.Data.SqlClient;
using SneakerStoreAPI.Data;
using SneakerStoreAPI.Repositories;
using SneakerStoreLib.Entidades;
using System.Drawing.Text;

namespace SneakerStoreAPI.Model
{
    public class BaseModel : IUsuarioRepository
    {

        public DbSession _db;

        public BaseModel(DbSession dbSession)
        {
            _db = dbSession;
        }

        public static Usuario ObterUsuario (string username, string senha)
        {
            dynamic result = null;

            string sql = $"SELECT * FROM tbUsuario WITH(NOLOCK) WHERE Usuario = '{username}' AND Senha = '{senha}'";
            


            using (var conn = _db.Connection)
            {
                
                result = conn.QueryFirstOrDefault<dynamic>(sql, commandType: System.Data.CommandType.Text);
            }
           

            if (result == null) return (null);
            

            return (new Usuario()
            {
                idUsuario = result.idUsuario,
                controleAcesso = result.ControleAcesso,
                username = result.Login,
                senha = result.Senha,
                sexo = result.Sexo
            });
            

    
        }

    }
}
