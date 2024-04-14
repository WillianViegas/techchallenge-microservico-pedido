using techchallenge_microservico_pedido.Models;

namespace techchallenge_microservico_pedido.Services.Interfaces
{
    public interface ICarrinhoService
    {
        public Task<Carrinho> GetCarrinhoById(string id);
    }
}
