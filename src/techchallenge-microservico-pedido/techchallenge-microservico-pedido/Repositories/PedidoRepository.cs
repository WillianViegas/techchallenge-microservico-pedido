using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Repositories.Interfaces;

namespace techchallenge_microservico_pedido.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        public Task<Pedido> CreatePedido(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Pedido>> GetAllPedidos()
        {
            throw new NotImplementedException();
        }
    }
}
