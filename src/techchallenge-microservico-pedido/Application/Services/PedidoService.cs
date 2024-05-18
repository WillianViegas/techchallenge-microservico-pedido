using Amazon.S3;
using Amazon.SQS;
using Amazon.SQS.ExtendedClient;
using Amazon.SQS.Model;
using Infra.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Repositories.Interfaces;
using techchallenge_microservico_pedido.Services.Interfaces;

namespace techchallenge_microservico_pedido.Services
{
    public class PedidoService : IPedidoService
    {

        private readonly ILogger<PedidoService> _logger;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly bool _useLocalStack;
        private AmazonSQSClient _sqs;
        private readonly string _queueUrl;
        private readonly bool _criarFila;
        private readonly bool _enviarMensagem;
        private readonly string _queueName;
        private readonly string _bucketName;
        private  IAmazonSQS _amazonSQS;
        private  IAmazonS3 _amazonS3;

        public PedidoService(ILogger<PedidoService> logger, IPedidoRepository pedidoRepository, IConfiguration config, IAmazonS3 s3, IAmazonSQS sqs)
        {
            var criarFila = config.GetSection("SQSConfig").GetSection("CreateTestQueue").Value;
            var enviarMensagem = config.GetSection("SQSConfig").GetSection("SendTestMessage").Value;
            var useLocalStack = config.GetSection("SQSConfig").GetSection("useLocalStack").Value;

            _logger = logger;
            _pedidoRepository = pedidoRepository;
            _useLocalStack = Convert.ToBoolean(useLocalStack);
            _criarFila = Convert.ToBoolean(criarFila);
            _queueUrl = config.GetSection("QueueUrl").Value;
            _enviarMensagem = Convert.ToBoolean(enviarMensagem);
            _queueName = config.GetSection("SQSConfig").GetSection("TestQueueName").Value;
            _bucketName = config.GetSection("SQSExtendedClient").GetSection("S3Bucket").Value;
            _amazonS3 = s3;
            _amazonSQS = sqs;

            //config SQS AWS
            _sqs = new AmazonSQSClient(Amazon.RegionEndpoint.GetBySystemName(
                    string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MY_SECRET")) || Environment.GetEnvironmentVariable("MY_SECRET").Equals("{MY_SECRET}")
                    ? "us-east-1"
                    : Environment.GetEnvironmentVariable("MY_SECRET")));

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


                var messageJson = Newtonsoft.Json.JsonConvert.SerializeObject(pedido);
                await EnviarMessageSQS(messageJson);

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


                var messageJson = Newtonsoft.Json.JsonConvert.SerializeObject(pedido);
                await EnviarMessageSQS(messageJson);

                return novoPedido;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        async Task EnviarMessageSQS(string messageJson)
        {
            if (!_useLocalStack)
            {
                var sqsConfiguration = new SQSConfiguration();
                _sqs = await sqsConfiguration.ConfigurarSQS();

                await sqsConfiguration.EnviarParaSQS(messageJson, _sqs);
            }
            else
            {
                var configSQS = new AmazonSQSExtendedClient(_amazonSQS, new ExtendedClientConfiguration().WithLargePayloadSupportEnabled(_amazonS3, _bucketName));

                if (_criarFila)
                    await CreateMessageInQueueWithStatusASyncLocalStack(configSQS);

                if (_enviarMensagem)
                    await SendTestMessageAsyncLocalStack(_queueUrl, configSQS);

                await configSQS.SendMessageAsync(_queueUrl, messageJson);
            }
        }

        async Task SendTestMessageAsyncLocalStack(string queue, AmazonSQSExtendedClient sqs)
        {
            var messageBody = new MessageBody();
            messageBody.IdTransacao = Guid.NewGuid().ToString();
            messageBody.idPedido = "65a315fadb1f522d916d9361";
            messageBody.Status = "OK";
            messageBody.DataTransacao = DateTime.Now;

            var jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(messageBody);

            await sqs.SendMessageAsync(queue, jsonObj);
        }

        async Task CreateMessageInQueueWithStatusASyncLocalStack(AmazonSQSExtendedClient sqs)
        {
            var responseQueue = await sqs.CreateQueueAsync(new CreateQueueRequest(_queueName));

            if (responseQueue.HttpStatusCode != HttpStatusCode.OK)
            {
                var erro = $"Failed to CreateQueue for queue {_queueName}. Response: {responseQueue.HttpStatusCode}";
                //log.LogError(erro);
                throw new AmazonSQSException(erro);
            }
        }
    }
}
