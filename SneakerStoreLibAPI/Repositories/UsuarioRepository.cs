using Dapper;
using SneakerStoreAPI.Data;
using SneakerStoreLib.Entidades;

namespace SneakerStoreAPI.Repositories
{

    public class UsuarioRepository : IUsuarioRepository
    {
    //Recebe a connection string 

        private DbSession _db;

        public UsuarioRepository(DbSession dbSession)
        {
            _db = dbSession;
        }
        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            //Abre conexão
            using (var conn = _db.Connection)
            {
                string query = "SELECT * FROM tbUsuario_teste";
                List<Usuario> usuarios = (await conn.QueryAsync<Usuario>(sql: query)).ToList();
                return usuarios;
            }
        }
        public async Task<Usuario> GetUsuarioByIdAsync(int idUsuario)
        {
            using (var conn = _db.Connection)
            {
                string query = "SELECT * FROM tbUsuario_teste WHERE idUsuario = @idUsuario";

                Usuario usuario = await conn.QueryFirstOrDefaultAsync<Usuario> 
                    (sql: query, param: new { idUsuario });
                return usuario;
            }
        }     

        //Implementar**
        //public async Task<UsuarioContainer> GetUsuariosEContadorAsync()
        //{
        //    using (var conn = _db.Connection)
        //    {
        //        string query = @"SELECT COUNT(*) FROM tbUsuario_teste
        //                         SELECT * FROM tbUsuario_teste";
        //        var reader = await conn.QueryMultipleAsync(sql: query);
        //        return new UsuarioContainer
        //        {
        //            Contador = (await reader.ReadAsync<int>()).FirstOrDefault(),
        //            Usuarios = (await reader.ReadAsync<Usuario>()).ToList()
        //        };
        //    }
        //}
        public async Task<int> SaveAsync(Usuario novoUsuario)
        {
            using (var conn = _db.Connection)
            {
                string command = @"
                    INSERT INTO tbUsuario_teste(Senha,Username,Nome,Telefone,Endereco,CNPJ_CPF,Controle_Acesso)
                    VALUES (@Senha, @Username,@Nome,@Telefone,@Endereco,@CNPJ_CPF,@Controle_Acesso)";
                var result = await conn.ExecuteAsync(sql: command, param: novoUsuario);                
                return result;
            }
        }

        public async Task<int> UpdateUsuarioAsync(Usuario atualizaUsuario)
        {
            using (var conn = _db.Connection)
            {
                string command = @"
                    UPDATE Usuarios SET tbUsuario_teste = @Username WHERE idUsuario = @idUsuario";

                var result = await conn.ExecuteAsync(sql: command, param: atualizaUsuario);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int idUsuario)
        {
            using (var conn = _db.Connection)
            {
                string command = @"DELETE FROM tbUsuario_teste WHERE idUsuario = @idUsuario";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { idUsuario });
                return resultado;
            }
        }
    }
}
