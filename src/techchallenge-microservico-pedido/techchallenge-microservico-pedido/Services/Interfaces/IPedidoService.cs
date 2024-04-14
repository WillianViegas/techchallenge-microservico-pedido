using techchallenge_microservico_pedido.Models;

namespace techchallenge_microservico_pedido.Services.Interfaces
{
    public interface IPedidoService
    {
        public Task<Pedido> CreatePedidoFromCarrinho(Carrinho carrinho);
        public Task<Pedido> CreatePedido(Pedido pedido);
    }
}
