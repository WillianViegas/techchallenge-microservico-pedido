using MongoDB.Bson.IO;
using Moq;
using NUnit.Framework;
using System;
using techchallenge_microservico_pedido.Enums;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Services.Interfaces;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class PedidoStepDefinitions
    {
        private string _idCarrinho;
        private string _pedidoJson;
        private Pedido _pedido;
        private Carrinho _carrinho;


        #region CreatePedido
        [Given(@"Que devo criar um pedido")]
        public void GivenQueDevoCriarUmPedido()
        {
            _pedidoJson = "{\r\n    \"Produtos\": [\r\n        {\r\n            \"id\": \"65a315a4db1f522d916d935a\",\r\n            \"nome\": \"Hamburguer especial da casa\",\r\n            \"descricao\": \"Hamburguer artesanal da casa com maionese caseira e molho secreto\",\r\n            \"preco\": 35.99,\r\n            \"categoriaId\": \"65a315a4db1f522d916d9357\"\r\n        }\r\n    ],\r\n    \"Usuario\": {\r\n        \"id\": \"65a315a4db1f522d916d9355\",\r\n        \"nome\": \"Marcos\",\r\n        \"email\": \"marcao@gmail.com\",\r\n        \"cpf\": \"65139370000\"\r\n    },\r\n    \"Total\": 35.99,\r\n    \"Pagamento\":{}\r\n}";
        }

        [When(@"Recebo as informacoes do meu pedido")]
        public void WhenReceboAsInformacoesDoMeuPedido()
        {
            var pedido = Newtonsoft.Json.JsonConvert.DeserializeObject<Pedido>(_pedidoJson);
            _pedido = pedido;
        }

        [Then(@"Valido o objeto")]
        public void ThenValidoOObjeto()
        {
            ValidarPedido(_pedido);
        }

        [Then(@"Crio o pedido")]
        public async Task ThenCrioOPedido()
        {
            //arrange
            var pedidoService = new Mock<IPedidoService>().Object;

            Mock.Get(pedidoService)
                .Setup(service => service.CreatePedido(It.IsAny<Pedido>()))
                .ReturnsAsync(_pedido);

            //act
            var result =  pedidoService.CreatePedido(_pedido);

            //assert
            Assert.NotNull(result);
        }
        #endregion

        #region CreatePedidoFromCarrinho
        [Given(@"Que devo criar um pedido a partir de um id de carrinho")]
        public void GivenQueDevoCriarUmPedidoAPartirDeUmIdDeCarrinho()
        {
            _idCarrinho = Guid.NewGuid().ToString();
        }

        [When(@"Recebo um id de carrinho")]
        public void WhenReceboUmIdDeCarrinho()
        {
            _carrinho = GetCarrinhoObj(_idCarrinho);
        }

        [Then(@"Valido se o id corresponde a um carrinho")]
        public async Task<bool> ThenValidoSeOIdCorrespondeAUmCarrinho()
        {
            var carrinhoService = new Mock<ICarrinhoService>().Object;

            Mock.Get(carrinhoService)
              .Setup(rep => rep.GetCarrinhoById(_carrinho.Id))
                .ReturnsAsync(_carrinho);

            var result = await carrinhoService.GetCarrinhoById(_carrinho.Id);

            if (result == null)
                return false;

            return true;
        }

        [Then(@"Crio o pedido com base nas informações do objeto de carrinho retornado")]
        public async Task ThenCrioOPedidoComBaseNasInformacoesDoObjetoDeCarrinhoRetornado()
        {
            //arrange
            var pedido = GetPedidoObj();
            pedido.IdCarrinho = _idCarrinho;
            var carrinhoService = new Mock<ICarrinhoService>().Object;
            var pedidoService = new Mock<IPedidoService>().Object;

            Mock.Get(pedidoService)
                .Setup(service => service.CreatePedidoFromCarrinho(_carrinho))
                .ReturnsAsync(pedido);

            //act
            var result = await pedidoService.CreatePedidoFromCarrinho(_carrinho);

            //assert
            Assert.NotNull(result);
        }

        #endregion


        private bool ValidarPedido(Pedido pedido)
        {
            if (pedido.Total <= 0)
                return false;

            if (!pedido.Produtos.Any())
                return false;

            return true;
        }

        private Carrinho GetCarrinhoObj(string idCarrinho)
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
                Id = idCarrinho,
                Usuario = null,
                Ativo = true,
                Produtos = produtos,
                Total = 10.00m
            };

            return carrinho;
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
