using Biblioteca_Api.Models;
using System.Collections;

namespace Biblioteca_Api.Interfaces
{
    public interface IClient
    {
        Task<IEnumerable> GetCollection();
        List<Livro> GetLivro(int Id);
        List<Livro> getPorCategoria(int Id);
        List<Livro> GetCarrinho(int Id);
        public int AddCarrinho(CarinhoCompra carrinho);
        public void DeleteItenCart(int Id);
        public void UpdateQuantIten(int Id, int Amount);
    }
}
