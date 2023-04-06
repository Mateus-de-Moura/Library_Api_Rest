using Biblioteca_Api.Models;

namespace Biblioteca_Api.Interfaces
{
    public interface ICart
    {
        List<Livro> GetCarrinho(int Id);
        Task<int> AddCarrinho(CarinhoCompra carrinho);
        void DeleteItenCart(int Id);
        void UpdateQuantIten(int Id, int Amount);
    }
}
