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

        public List<Livro> GetCarrinho(int Id)
        {
            var parameters = new { IdUsuario = Id };

            string query = "SELECT COMPRA.ID, LIVRO.TITULO,LIVRO.EDITORA,LIVRO.ANO,LIVRO.URLIMAGEM,LIVRO.VALOR, COMPRA.QUANTIDADE FROM LIVROS AS LIVRO INNER JOIN CARRINHOCOMPRA AS COMPRA ON COMPRA.IDLIVRO = LIVRO.ID WHERE COMPRA.IDUSUARIO  = @IdUsuario";


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

        public int AddCarrinho(CarinhoCompra carrinho)
        {
            var parameters = new { IdUsuario = carrinho.IdUsuario, IdLivro = carrinho.IdLivro };
            if (!VerifyIternCart(carrinho.IdUsuario, carrinho.IdLivro))
            {
                return 0;
            } 

            string query = "INSERT INTO CARRINHOCOMPRA OUTPUT INSERTED.ID VALUES(@IdUsuario, @IdLivro, 1)";

            using (var con = new SqlConnection(conn))
            {
                con.Open();

                try
                {
                    var id = con.QueryFirstOrDefault<string>(query, parameters);
                    return Convert.ToInt32(id);
                }
                catch (Exception)
                {

                    return 0;
                }
            }
        }

        public void DeleteItenCart(int Id)
        {
            var parameters = new { Id = Id };

            string query = "DELETE CARRINHOCOMPRA WHERE ID = @Id";

            using (var con = new SqlConnection(conn))
            {
                con.Open();
                try
                {
                    con.Execute(query,parameters);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void UpdateQuantIten(int Id, int Amount)
        {
            var parameters = new { Id = Id, Amount = Amount };

            string query = "UPDATE CARRINHOCOMPRA SET QUANTIDADE = @Amount WHERE ID = @Id";

            using (var con = new SqlConnection(conn))
            {
                con.Open();
                try
                {
                    con.Execute(query, parameters);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }    
        public bool VerifyIternCart(int IdUsuario, int IdLivro)
        {
            var parameters = new { IdUsuario = IdUsuario, IdLivro = IdLivro };

            string query = "SELECT * FROM CARRINHOCOMPRA WHERE IDUSUARIO = @IdUsuario AND IDLIVRO = @IdLivro ";

            using (var con = new SqlConnection(conn))
            {
                con.Open();
                try
                {
                    var livro = con.QueryFirstOrDefault(query, parameters);
                    if (livro == null || livro.Count == 0 )
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
    }
}
