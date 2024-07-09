using techchallenge_microservico_pedido.Models;

namespace techchallenge_microservico_pedido.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        public Task<Pedido> CreatePedido(Pedido pedido);
        public Task DeletePedido(string pedidoId);
        public Task<IList<Pedido>> GetAllPedidos();
    }
}
