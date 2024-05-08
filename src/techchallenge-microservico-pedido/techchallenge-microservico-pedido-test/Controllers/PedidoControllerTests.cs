using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;
using techchallenge_microservico_pedido.Controllers;
using techchallenge_microservico_pedido.Enums;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Services;
using techchallenge_microservico_pedido.Services.Interfaces;

namespace techchallenge_microservico_pedido_test.Controllers
{
    public class PedidoServiceTests
    {
        [Fact]
        public async Task CreatePedidoFromCarrinho()
        {
            //arrange
            var carrinho = GetCarrinhoObj();
            var pedido = GetPedidoObj();

            var carrinhoService = new Mock<ICarrinhoService>().Object;
            var pedidoService = new Mock<IPedidoService>().Object;

            Mock.Get(carrinhoService)
                .Setup(service => service.GetCarrinhoById(carrinho.Id))
                .ReturnsAsync(carrinho);

            Mock.Get(pedidoService)
                .Setup(service => service.CreatePedidoFromCarrinho(carrinho))
                .ReturnsAsync(pedido);


            var mock = new Mock<ILogger<PedidoController>>();
            ILogger<PedidoController> logger = mock.Object;

            var controller = new PedidoController(logger, pedidoService, carrinhoService);

            //act
            var result = await controller.CreatePedidoFromCarrinho(carrinho.Id);

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreatePedido()
        {
            //arrange
            var pedido = GetPedidoObj();
            var pedidoService = new Mock<IPedidoService>().Object;
            var carrinhoService = new Mock<ICarrinhoService>().Object;

            Mock.Get(pedidoService)
                .Setup(service => service.CreatePedido(It.IsAny<Pedido>()))
                .ReturnsAsync(pedido);


            var mock = new Mock<ILogger<PedidoController>>();
            ILogger<PedidoController> logger = mock.Object;

            var controller = new PedidoController(logger, pedidoService, carrinhoService);

            //act
            var result = await pedidoService.CreatePedido(pedido);

            //assert
            Assert.NotNull(result);
        }


        private Pedido GetPedidoObj()
        {
            var produtos = new List<Produto>();
            var produto = new Produto()
            {
                Id = "",
                Nome = "",
                Descricao = "",
                CategoriaId = "",
                Preco = 10.00m
            };

            produtos.Add(produto);


            var pedido = new Pedido();
            pedido.Id = "";
            pedido.IdCarrinho = pedido.IdCarrinho;
            pedido.Numero = 1;
            pedido.DataCriacao = DateTime.Now;
            pedido.Produtos = produtos;
            pedido.Status = EPedidoStatus.Novo;

            return pedido;
        }

        private Carrinho GetCarrinhoObj()
        {
            var produtos = new List<Produto>();
            var produto = new Produto()
            {
                Id = "",
                Nome = "",
                Descricao = "",
                CategoriaId = "",
                Preco = 10.00m
            };

            produtos.Add(produto);


            var carrinho = new Carrinho()
            {
                Id = Guid.NewGuid().ToString(),
                Usuario = null,
                Ativo = true,
                Produtos = produtos,
                Total = 10.00m
            };

            return carrinho;
        }
    }
}