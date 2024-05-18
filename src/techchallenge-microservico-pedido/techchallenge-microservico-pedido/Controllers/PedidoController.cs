using Amazon.S3;
using Amazon.SQS;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Services.Interfaces;

namespace techchallenge_microservico_pedido.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;
        private IPedidoService _pedidoService;
        private ICarrinhoService _carrinhoService;


        public PedidoController(ILogger<PedidoController> logger, IPedidoService pedidoService, ICarrinhoService carrinhoService)
        {
            _logger = logger;
            _pedidoService= pedidoService;
            _carrinhoService = carrinhoService;
        }


        [HttpGet("/teste")]
        public  IResult Teste()
        {
            try
            {
                return TypedResults.Ok("Teste");
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [HttpPost("/fromCarrinho")]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
        Summary = "Criar pedido a partir do carrinho",
        Description = "Cria um novo pedido utilizando o id de um carrinho")]
        public async Task<IResult> CreatePedidoFromCarrinho(string idCarrinho)
        {
            try
            {
                if (string.IsNullOrEmpty(idCarrinho))
                    return TypedResults.BadRequest("idCarrinho inválido");

                if (await _carrinhoService.GetCarrinhoById(idCarrinho) is Carrinho carrinho)
                {
                    var pedido = await _pedidoService.CreatePedidoFromCarrinho(carrinho);
                    return TypedResults.Created($"/pedido/{pedido.Id}", pedido);
                }

                return TypedResults.NotFound();
            }
            catch (Exception ex)
            {
                var erro = $"Erro ao criar pedido a partir do carrinho. IdCarrinho: {idCarrinho}";
                _logger.LogError(erro, ex);
                return TypedResults.Problem(erro);
            }
        }

        [HttpPost("/createPedido")]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
        Summary = "Criar pedido",
        Description = "Cria um novo pedido")]
        public async Task<IResult> CreatePedido([FromBody] Pedido pedidoInput )
        {
            try
            {
                var pedido = await _pedidoService.CreatePedido(pedidoInput);
                return TypedResults.Created($"/pedido/{pedido.Id}", pedido);
            }
            catch (Exception ex)
            {
                var erro = $"Erro ao criar o pedido.";
                _logger.LogError(erro, ex);
                return TypedResults.Problem(erro);
            }
        }

    }
}
