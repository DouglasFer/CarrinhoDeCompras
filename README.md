# Carrinho De Compras

como não recebi os arquivos json, fiz a criação de produtos e cupons ficticios, já inseridos no seed, então automaticamente vai inserir no banco quando iniciar o projeto.

## Stacks
- **Backend**: C#, .Net 10, Asp.Net Core Web API
- **Banco de dados**: PostgreSQL com Entity Framework
- **Frontend**: Angular e Angular Material
- **Arquitetura**: camadas (API, Application, Domain, Infrastructure), inspirada em Clean Architecture

## Como Rodar

**Preferência**
Pré-requisito: Docker Desktop instalado e em execução.

Na raiz do projeto:

```bash
docker compose up --build
```
com ele vai rodar, db, api e o frontend

Swagger disponível em http://localhost:5282/swagger.

Para parar:

```bash
docker compose down
```
Para parar e apagar também os dados do banco:

```bash
docker compose down -v
```

**Opção 2**

com o postgressql rodando, altere a connection string em: backend/CarrinhoCompras.Api/appsettings.json
para o usuario e senha que foi configurada do postgresql
depois rode os comandos:

```bash
cd backend/CarrinhoCompras.Api
dotnet restore
dotnet run
```
a api vai aplicar as migrations e o seed vai automatico quando iniciar. Por padrão sobe em http://localhost:5282.

em outro terminal, rode:

```bash
cd frontend
npm install
npm start
```

vai rodar em: http://localhost:4200

## Decisões de design e premissas
- **Camadas**: Api (controllers), Application (DTOs, serviços/casos de uso, interfaces), Domain (entidades e regras de negócio), Infrastructure (EF Core, repositórios, migrations). O domínio (Carrinho, ItemCarrinho, Produto, Cupom) não depende de nada de infraestrutura — todas as regras de negócio (soma de quantidade, validação de estoque, bloqueio de carrinho finalizado, cálculo de subtotal/desconto/total) vivem nas próprias entidades.

- **Catálogo e cupons**: persistidos via seed na migration inicial (InsertData), com os mesmos valores do produtos.json/cupons.json fornecidos, usando nomenclatura de banco (ID, DescricaoProduto, PrecoUnitario, QuantidadeEstoque, CodigoCupom, PercentualDesconto).

- **Cupom extra**: além de 10OFF e 15OFF exigidos, foi criado um terceiro cupom 20OFF (20% de desconto) só para ampliar a cobertura de testes de troca de cupom.

- **Cálculos**: Subtotal, Desconto e Total são propriedades computadas na entidade Carrinho (não persistidas), sempre recalculadas a partir dos itens e do cupom aplicado — evita inconsistência entre valor salvo e valor real.

- **Tratamento de erros**: os controllers convertem exceções de domínio em respostas HTTP tratadas via ProblemDetails:
KeyNotFoundException → 404 (carrinho/produto/cupom não encontrado)
ArgumentOutOfRangeException → 400 (quantidade ≤ 0)
ArgumentException → 400 (quantidade acima do estoque disponível)
InvalidOperationException → 409 (alteração em carrinho já finalizado)

- **Tipos monetários**: decimal em toda a modelagem de preços e descontos, para evitar erros de arredondamento de ponto flutuante.

- **Histórico de carrinhos**: adicionado endpoint GET /api/carrinhos/historico para listar carrinhos já finalizados — não era exigido, mas ajuda a validar o fluxo de checkout no front-end.

- **Variável**: PrecoLiquido alterei para PrecoUnitario, pois acredito que é mais facil de compreender.


Qualquer dúvida sobre o projeto ou as decisões tomadas, fico à disposição.