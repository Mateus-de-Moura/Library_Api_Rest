using Biblioteca_Api.Interfaces;
using Biblioteca_Api.Models;
using Biblioteca_Api.Services;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Biblioteca_Api.Repositories
{
    public class BookRepository : IClient
    {
        private IDbConnection _connection;   
        public BookRepository()
        {
            var conect = new GetConection();            
            _connection = new SqlConnection(conect.conection());
        }

        public Task DeleteLivro(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable> GetCollection()
        {
            return await _connection.QueryAsync("SELECT * FROM LIVROS");
        }

        public List<Livro> GetLivro(int Id)
        {
            return _connection.Query<Livro>("SELECT * FROM LIVROS WHERE Id = @Id", new { Id = Id }).ToList();
        }

        public List<Livro> getPorCategoria(int Id)
        {
            return _connection.Query<Livro>("SELECT * FROM LIVROS WHERE CATEGORIA = (SELECT CATEGORIA FROM LIVROS WHERE ID = @Id) AND Id <> @Id", new { Id = Id }).ToList();
        }

        public Task UpdateLivroAsync(Livro livro)
        {
            throw new NotImplementedException();
        }
    }
}

