using Biblioteca_Api.Interfaces;
using Biblioteca_Api.Models;
using System.Globalization;
using Dapper;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Biblioteca_Api.Services;

namespace Biblioteca_Api.Repositories
{
    public class UserRepository : IAuth
    {
        private string conn;
        public UserRepository()
        {
            var conect = new GetConection();
            conn = conect.conection();
        }       

        public async Task<int> loginAsync(UserLogin user)
        {
            var parameters = new { Email = user.Email, Senha = user.Senha };

            UserModel usuario;
            string query = "SELECT * FROM USUARIOS WHERE Email = @Email AND senha = @Senha AND Ativo = 1";
            var con = new SqlConnection(conn);

            try
            {               
                usuario = await con.QueryFirstAsync<UserModel>(query, parameters);

                if (usuario != null)
                {
                    return usuario.Id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
               return 0;    
            }           
        }

        public async Task<(string,UserModel)> RegistreAsync(UserModel user)
        {
            if (user == null)
                return ("Erro,Usuario Inválido !", new UserModel());

            var parameters = new { Nome = user.Nome, Email = user.Email, Senha = user.Senha };

            string query = "INSERT USUARIOS (NOME,EMAIL,SENHA, ATIVO) OUTPUT INSERTED.ID, INSERTED.Nome, INSERTED.Email VALUES (@Nome, @Email, @Senha, 1)";
           var con = new SqlConnection(conn);
           con.Open();

            try
            {
               var retorno = await con.QueryFirstAsync<UserModel>(query, parameters);
                if (retorno != null)
                {
                    return ("Usuario cadastrado",retorno);
                }
                else
                {
                    return ("Usuario ja existente", new UserModel());
                }
            }
            catch (Exception)
            {
                return ("Erro !", new UserModel());
            }
        }

        public async Task<bool> UpdateAsync(UserModel user)
        {
            var parameters = new {ID = user.Id, Nome = user.Nome, Email = user.Email, Perfil = user.Perfil };

            string query = "UPDATE Usuarios SET Nome = @Nome, Email =@Email ,Perfil = @Perfil OUTPUT INSERTED.ID  WHERE ID = @ID";

            using (var con = new SqlConnection(conn))
            {
                con.Open();
                try
                {
                    var retorno = await con.QueryFirstOrDefault(query, parameters);

                    if (retorno != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }
        public async Task<bool> DeleteUserAsync(int Id)
        {
            var parameters = new { ID = Id };

            string query = "UPDATE FROM USUARIOS WHERE ID = @ID";

            using (var con = new SqlConnection(conn))
            {
                con.Open();
                try
                {
                    await con.ExecuteAsync(query, parameters);
                    return  true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
