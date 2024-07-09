# techchallenge-microservico-pedido

Repositório relacionado a Fase 4 do techChallenge FIAP. Refatoração do projeto de totem em três microsserviços (Pedido, Pagamento e Produção);

Repositório bloqueado para push na main, é necessário abrir um PullRequest;

Este microsserviço tem como objetivo permitir o cadastro de um pedido na base de dados e envio das informações para uma fila SQS na AWS.

Estrutura
 - Banco de dados = MongoDB
 - Simulação de ambiente AWS = Localstack
 - Implementação de fila na AWS = SQS
 - Containers = Docker + Docker-Compose
 - Orquestração de containers = Kubernetes
 - Testes unitários e com BDD utilizando a extensão SpecFlow
 - Cobertura de código = SonarCloud
 - Pipeline = Github Actions
 - Deploy = Terraform


![image](https://github.com/WillianViegas/techchallenge-microservico-pedido/assets/58482678/17ad3351-d081-4504-b18b-8818b1111605)


Fluxograma:
https://www.figma.com/board/foY2Q9t6aj6Gzv9WK8actk/Documenta%C3%A7%C3%A3o-Sistema-DDD?node-id=0%3A1&t=oY6vBdqPodcM5LMR-1

Video explicando a estrutura: https://youtu.be/-OZgHsUoLkM

Links para repositórios relacioados: 

- techchallenge-microservico-pagamento (Microsserviço de Pagamento):
  - https://github.com/WillianViegas/techchallenge-microservico-pagamento
- techchallenge-microservico-producao (Microsserviço de Produção)
  - https://github.com/WillianViegas/techchallenge-microservico-producao
- TechChallenge-LanchoneteTotem (Repositório com o projeto que originou os microsserviços e histórico das fases):
  - https://github.com/WillianViegas/TechChallenge-LanchoneteTotem
