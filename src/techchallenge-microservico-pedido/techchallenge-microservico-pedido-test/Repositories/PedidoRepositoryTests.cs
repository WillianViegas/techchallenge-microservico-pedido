using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using techchallenge_microservico_pedido.Enums;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Repositories.Interfaces;
using techchallenge_microservico_pedido.Services.Interfaces;

namespace techchallenge_microservico_pedido_test.Repositories
{
    public class PedidoRepositoryTests
    {
        [Fact]
        public async Task CreatePedido()
        {
            //arrange
            var pedido = GetPedidoObj();
            var pedidoRepository = new Mock<IPedidoRepository>().Object;

            Mock.Get(pedidoRepository)
                .Setup(repo => repo.CreatePedido(It.IsAny<Pedido>()))
                .ReturnsAsync(pedido);

            //act
            var result = await pedidoRepository.CreatePedido(pedido);

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllPedidos()
        {
            //arrange
            var pedidoRepository = new Mock<IPedidoRepository>().Object;
            var pedido = GetPedidoObj();
            var pedido2 = GetPedidoObj();
            var pedido3 = GetPedidoObj();

            var listaPedidos = new List<Pedido>();
            listaPedidos.Add(pedido);
            listaPedidos.Add(pedido2);
            listaPedidos.Add(pedido3);

            Mock.Get(pedidoRepository)
              .Setup(rep => rep.GetAllPedidos())
                .ReturnsAsync(listaPedidos);

            //act
            var result = await pedidoRepository.GetAllPedidos();

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
    }
}