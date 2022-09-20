using SneakerStoreLibAPI.Data;

namespace SneakerStoreLibAPI.Repositories
{
    public interface IUsuarioRepository
    {
        //Listar Usuarios
        Task<List<Usuario>> GetUsuariosAsync();

        //Busca por ID
        Task<Usuario> GetUsuarioByIdAsync(int id);
        //Retorna mais de um resultado
        Task<UsuarioContainer> GetUsuariosEContadorAsync();
        //Cria um novo Usuario
        Task<int> SaveAsync(Usuario novoUsuario);
        //Atualiza Usuario
        Task<int> UpdateUsuarioAsync(Usuario atualizaUsuario);
        //Deleta Usuário passando id
        Task<int> DeleteAsync(int id);

    }
}
