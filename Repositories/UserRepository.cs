using Biblioteca_Api.Interfaces;
using Biblioteca_Api.Models;
using System.Globalization;
using Dapper;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Biblioteca_Api.Services;
using System.Data;

namespace Biblioteca_Api.Repositories
{
    public class UserRepository : IAuth
    {
        private IDbConnection _connection;
        public UserRepository()
        {
            var conect = new GetConection();
            _connection = new SqlConnection(conect.conection());
        }

        public async Task<int> loginAsync(UserLogin user)
        {
            UserModel usuario;

            usuario = await _connection.QueryFirstAsync<UserModel>("SELECT * FROM USUARIOS WHERE Email = @Email AND senha = @Senha AND Ativo = 1", new { Email = user.Email, Senha = user.Senha });

            if (usuario != null)
            {
                return usuario.Id;
            }
            else
            {
                return 0;
            }
        }


        public async Task<(string, UserModel)> RegistreAsync(UserModel user)
        {
            if (user == null)
                return ("Erro,Usuario Inválido !", new UserModel());

            var retorno = await _connection.QueryFirstAsync<UserModel>("INSERT USUARIOS (NOME,EMAIL,SENHA, ATIVO, BASE64PHOTO) OUTPUT INSERTED.ID, INSERTED.Nome, INSERTED.Email VALUES (@Nome, @Email, @Senha, 1, @Base64Photo)", new { Nome = user.Nome, Email = user.Email, Senha = user.Senha, Base64Photo = user.Base64Photo });
            if (retorno != null)
            {
                return ("Usuario cadastrado", retorno);
            }
            else
            {
                return ("Usuario ja existente", new UserModel());
            }
        }

        public async Task<bool> UpdateAsync(UserModel user)
        {
            var retorno = await _connection.QueryFirstOrDefault("UPDATE Usuarios SET Nome = @Nome, Email =@Email ,Perfil = @Perfil OUTPUT INSERTED.ID  WHERE ID = @ID", new { ID = user.Id, Nome = user.Nome, Email = user.Email, Perfil = user.Perfil });

            if (retorno != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteUserAsync(int Id)
        {
            await _connection.ExecuteAsync("UPDATE FROM USUARIOS WHERE ID = @ID", new { ID = Id });
            return true;
        }

        public async Task<UserModel> GetUser(int Id)
        {
            return await _connection.QueryFirstAsync<UserModel>("SELECT BASE64PHOTO FROM  USUARIOS WHERE ID =  @ID", new { ID = Id });
        }
    }
}
