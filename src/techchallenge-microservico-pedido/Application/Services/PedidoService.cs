using Microsoft.Extensions.Logging;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Repositories.Interfaces;
using techchallenge_microservico_pedido.Services.Interfaces;

namespace techchallenge_microservico_pedido.Services
{
    public class PedidoService : IPedidoService
    {

        private readonly ILogger<PedidoService> _logger;
        private readonly IPedidoRepository _pedidoRepository;


        public PedidoService(ILogger<PedidoService> logger, IPedidoRepository pedidoRepository)
        {
            _logger = logger;
            _pedidoRepository = pedidoRepository;
        }


        public async Task<Pedido> CreatePedidoFromCarrinho(Carrinho carrinho)
        {
            try
            {
                var numeroPedido = await _pedidoRepository.GetAllPedidos();

                var pedido = new Pedido
                {
                    Produtos = carrinho.Produtos,
                    Total = carrinho.Total,
                    Status = 0,
                    DataCriacao = DateTime.Now,
                    Numero = numeroPedido.Count + 1,
                    Usuario = carrinho.Usuario,
                    IdCarrinho = carrinho.Id,
                    Pagamento = new Pagamento() 
                };

                await _pedidoRepository.CreatePedido(pedido);

                return pedido;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> CreatePedido(Pedido pedido)
        {
            try
            {
                var numeroPedido = await _pedidoRepository.GetAllPedidos();

                var novoPedido = new Pedido
                {
                    Produtos = pedido.Produtos,
                    Total = pedido.Produtos.Sum(x => x.Preco),
                    Status = 0,
                    DataCriacao = DateTime.Now,
                    Numero = numeroPedido.Count + 1,
                    Usuario = pedido.Usuario,
                    IdCarrinho = pedido.IdCarrinho
                };

                await _pedidoRepository.CreatePedido(novoPedido);

                return novoPedido;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
