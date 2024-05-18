using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using techchallenge_microservico_pedido.Controllers;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Repositories.Interfaces;
using techchallenge_microservico_pedido.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PedidoLambdaFunction;

public class Function : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
{

    //protected override void Init(IWebHostBuilder builder)
    //{
    //    builder
    //        .UseStartup<Program>();
    //}

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public string Handler(string input, ILambdaContext context)
    {
        try
        {
            var pedido = JsonConvert.DeserializeObject<Pedido>(input);

            //// Cria uma instância do serviço da API
            //var msPedidoController = new PedidoService();

            //// Chama o método da API
            //string resultado = msPedidoController.CreatePedido();

            // Retorna o resultado
            return "";
        }
        catch (Exception ex)
        {
            // Se houver um erro, registra-o no log e retorna uma mensagem de erro
            context.Logger.LogLine($"Ocorreu um erro: {ex.Message}");
            return "Erro ao processar a solicitação.";
        }
    }
}
