using System.Globalization;
using System.Text.Json.Serialization;

namespace Biblioteca_Api.Models
{
    public class UserModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public PerfilUsuario Perfil { get; set; }
        public int Ativo { get; set; }
        public string Base64Photo { get; set; }
    }
}
