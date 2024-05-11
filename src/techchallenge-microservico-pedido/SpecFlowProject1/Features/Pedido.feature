Feature: Pedido

@mytag
Scenario: CreatePedido
	Given Que devo criar um pedido
	When Recebo as informacoes do meu pedido 
	Then Valido o objeto
	And Crio o pedido


@mytag
Scenario: CreatePedidoFromCarrinho
	Given Que devo criar um pedido a partir de um id de carrinho
	When Recebo um id de carrinho 
	Then Valido se o id corresponde a um carrinho
	And Crio o pedido com base nas informações do objeto de carrinho retornado