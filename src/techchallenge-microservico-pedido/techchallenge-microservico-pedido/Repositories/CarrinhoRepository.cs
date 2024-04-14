using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Repositories.Interfaces;
using techchallenge_microservico_pedido.Services.Interfaces;

namespace techchallenge_microservico_pedido.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        public Task<Carrinho> GetCarrinhoById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
