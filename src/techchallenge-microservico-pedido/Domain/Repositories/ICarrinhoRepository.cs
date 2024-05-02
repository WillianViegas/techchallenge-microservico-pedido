using techchallenge_microservico_pedido.Models;

namespace techchallenge_microservico_pedido.Repositories.Interfaces
{
    public interface ICarrinhoRepository
    {
        public Task<Carrinho> GetCarrinhoById(string id);

    }
}
