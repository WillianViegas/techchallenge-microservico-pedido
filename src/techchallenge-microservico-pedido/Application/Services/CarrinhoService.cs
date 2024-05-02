using Microsoft.Extensions.Logging;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Repositories.Interfaces;
using techchallenge_microservico_pedido.Services.Interfaces;

namespace techchallenge_microservico_pedido.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ILogger<CarrinhoService> _logger;
        private readonly ICarrinhoRepository _carrinhoRepository;
        public CarrinhoService(ILogger<CarrinhoService> log, ICarrinhoRepository carrinhoRepository)
        {
            _logger = log;
            _carrinhoRepository = carrinhoRepository;
        }

        public async Task<Carrinho> GetCarrinhoById(string id)
        {
            try
            {
                return await _carrinhoRepository.GetCarrinhoById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
