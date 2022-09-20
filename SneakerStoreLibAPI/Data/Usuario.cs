using SneakerStoreLibAPI.Data;
using System.Drawing;

namespace SneakerStoreLibAPI.Data
{
    public class Usuario
    {
        public long idUsuario { get; set; }
        public string Senha { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public string  Telefone { get; set; }
        public string Endereco { get; set; }
        public string CNPJ_CPF { get; set; }
        public Int16 Controle_Acesso { get; set; }

    }
}
