using Biblioteca_Api.Interfaces;
using Biblioteca_Api.Models;
using Biblioteca_Api.Services;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using System.Collections;
using System.Data.SqlClient;

namespace Biblioteca_Api.Repositories
{
    public class BookRepository : IClient
    {
        private string conn;
        public BookRepository()
        {
            var conect = new GetConection();
            conn = conect.conection();
        }

        public Task DeleteLivro(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable> GetCollection()
        {
            var query = "SELECT * FROM LIVROS";

            using (var con = new SqlConnection(conn))
            {
                con.Open();

                try
                {
                    var Livros = await con.QueryAsync(query);

                    if (Livros != null)
                    {
                        return Livros;
                    }
                    else
                    {
                        return new List<LivrosModel>();
                    }
                }
                catch (Exception)
                {

                    return new List<LivrosModel>();
                }
            }
        }

        public List<Livro> GetLivro(int Id)
        {
            var parameters = new { Id = Id };

            string query = "SELECT * FROM LIVROS WHERE Id = @Id";

            using (var con = new SqlConnection(conn))
            {
                con.Open();

                try
                {
                    var Livro = con.Query<Livro>(query, parameters).ToList();

                    if (Livro != null)
                    {
                        return Livro;
                    }
                    else
                    {
                        return new List<Livro>();
                    }
                }
                catch (Exception)
                {
                    return new List<Livro>();
                }
            }
        }

        public List<Livro> getPorCategoria(int Id)
        {
            var parameters = new { Id = Id };

            string query = "SELECT * FROM LIVROS WHERE CATEGORIA = (SELECT CATEGORIA FROM LIVROS WHERE ID = @Id) AND Id <> @Id";

            using (var con = new SqlConnection(conn))
            {
                con.Open();

                try
                {
                    var Livro = con.Query<Livro>(query, parameters).ToList();

                    if (Livro != null)
                    {
                        return Livro;
                    }
                    else
                    {
                        return new List<Livro>();
                    }
                }
                catch (Exception)
                {

                    return new List<Livro>();
                }
            }
        }

        public Task UpdateLivroAsync(Livro livro)
        {
            throw new NotImplementedException();
        }
    }
}

