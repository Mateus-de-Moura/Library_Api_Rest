using Biblioteca_Api.Interfaces;
using Biblioteca_Api.Models;
using Biblioteca_Api.Services;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Biblioteca_Api.Repositories
{
    public class CartRepository : ICart
    {
        private IDbConnection _connection;
        public CartRepository()
        {
            var conect = new GetConection();
            _connection = new SqlConnection(conect.conection());
        }

        public async Task<int> AddCarrinho(CarinhoCompra carrinho)
        {
            var Livro = VerifyIternCart(carrinho.IdUsuario, carrinho.IdLivro);

            if (Livro == null || Livro.Id != 0)
            {
                UpdateQuantIten(Livro.Id, Livro.quantidade + 1);
                return Livro.Id;
            }

            var id = await _connection.QueryFirstOrDefaultAsync<string>("INSERT INTO CARRINHOCOMPRA OUTPUT INSERTED.ID VALUES(@IdUsuario, @IdLivro, 1)", new { IdUsuario = carrinho.IdUsuario, IdLivro = carrinho.IdLivro });
            return Convert.ToInt32(id);
        }

        public void DeleteItenCart(int Id)
        {
            _connection.Execute("DELETE CARRINHOCOMPRA WHERE ID = @Id", new { Id = Id });

        }

        public List<Livro> GetCarrinho(int Id)
        {
            return _connection.Query<Livro>("SELECT COMPRA.ID, LIVRO.TITULO,LIVRO.EDITORA,LIVRO.ANO,LIVRO.URLIMAGEM,LIVRO.VALOR, COMPRA.QUANTIDADE FROM LIVROS AS LIVRO INNER JOIN CARRINHOCOMPRA AS COMPRA ON COMPRA.IDLIVRO = LIVRO.ID WHERE COMPRA.IDUSUARIO  = @IdUsuario", new { IdUsuario = Id }).ToList();

        }

        public void UpdateQuantIten(int Id, int Amount)
        {
            _connection.Execute("UPDATE CARRINHOCOMPRA SET QUANTIDADE = @Amount WHERE ID = @Id", new { Id = Id, Amount = Amount });
        }

        public CarinhoCompra VerifyIternCart(int IdUsuario, int IdLivro)
        {
            return _connection.QueryFirstOrDefault<CarinhoCompra>("SELECT Id,quantidade FROM CARRINHOCOMPRA WHERE IDUSUARIO = @IdUsuario AND IDLIVRO = @IdLivro ", new { IdUsuario = IdUsuario, IdLivro = IdLivro });
        }
    }
}
