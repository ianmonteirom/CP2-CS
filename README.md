# Projeto Banco — API

## 1. Identificação

| Nome | RM |
|---|---|
| [Nome Integrante 1] | RM000000 |
| [Nome Integrante 2] | RM000000 |
| [Nome Integrante 3] | RM000000 |

---

## 2. Produto bancário escolhido e justificativa

Foram implementados **dois produtos** (escopo de trio):

### MaquinaDeCartao
Produto voltado a clientes que necessitam de equipamento para receber pagamentos. A regra de negócio implementada define a **Taxa MDR (Merchant Discount Rate)** de forma variável com base no tipo de pagamento:

| Tipo | Taxa MDR |
|---|---|
| Débito | 1,2% |
| Crédito | 2,5% |
| Crediário Parcelado | 3,8% |

Regra de negócio adicional: um cliente não pode ter duas máquinas aprovadas para o mesmo tipo de pagamento.

### Emprestimo
Produto de crédito pessoal. A aprovação é baseada em um **score de crédito** calculado dinamicamente no domínio:

| Critério | Pontos |
|---|---|
| Score base | 300 |
| Idade entre 25–60 anos (PF) | +100 |
| Idade acima de 60 anos (PF) | +50 |
| Por contratação aprovada anterior | +50 cada |

Score mínimo para aprovação: **500 pontos**. Taxa de juros: **1,5% a.m.** O valor da parcela é calculado via fórmula Price.

---

## 4. Diagrama de classes

> Veja `/docs/diagrama-classes.png`

```
┌─────────────┐         ┌──────────────┐
│   Agencia   │1      N │   Cliente    │ <<abstract>>
│─────────────│◄────────│──────────────│
│ Id          │         │ Id           │
│ Nome        │         │ Nome         │
│ Numero      │         │ Email        │
│ Cidade      │         │ Telefone     │
│ Estado      │         │ AgenciaId    │
└─────────────┘         └──────┬───────┘
                               │
               ┌───────────────┴──────────────┐
               ▼                              ▼
    ┌──────────────────┐           ┌───────────────────┐
    │  PessoaFisica    │           │  PessoaJuridica   │
    │──────────────────│           │───────────────────│
    │ Cpf              │           │ Cnpj              │
    │ DataNascimento   │           │ RazaoSocial       │
    └──────────────────┘           └───────────────────┘

┌───────────────┐        ┌─────────────────┐
│  Contratacao  │N     1 │    Produto      │ <<abstract>>
│───────────────│────────│─────────────────│
│ Id            │        │ Id              │
│ ClienteId     │        │ Nome            │
│ ProdutoId     │        │ Descricao       │
│ Status        │        └────────┬────────┘
│ DataSolicit.  │                 │
│ DataProcess.  │     ┌───────────┴──────────┐
│ Observacao    │     ▼                      ▼
└───────────────┘ ┌──────────────┐  ┌──────────────┐
                  │MaquinaDeCartao│  │  Emprestimo  │
                  │──────────────│  │──────────────│
                  │TipoPagamento │  │ValorSolicit. │
                  │TaxaMDR       │  │PrazoMeses    │
                  └──────────────┘  │TaxaJuros     │
                                    └──────────────┘
```

---

## 6. Endpoints disponíveis

### POST /api/agencias
```json
// Request
{
  "nome": "Agência Centro",
  "numero": 1001,
  "cidade": "São Paulo",
  "estado": "SP"
}
// Response 201
{
  "id": 1,
  "nome": "Agência Centro",
  "numero": 1001,
  "cidade": "São Paulo",
  "estado": "SP",
  "dataCriacao": "2026-05-05T10:00:00Z"
}
```

### GET /api/agencias/{id}
```json
// Response 200
{
  "id": 1,
  "nome": "Agência Centro",
  "numero": 1001,
  "cidade": "São Paulo",
  "estado": "SP",
  "dataCriacao": "2026-05-05T10:00:00Z"
}
```

### POST /api/clientes/pf
```json
// Request
{
  "nome": "João da Silva",
  "email": "joao@email.com",
  "telefone": "11999999999",
  "agenciaId": 1,
  "cpf": "12345678901",
  "dataNascimento": "1990-01-15"
}
// Response 201
{
  "id": 1,
  "tipo": "PF",
  "nome": "João da Silva",
  "email": "joao@email.com",
  "telefone": "11999999999",
  "agenciaId": 1,
  "dataCriacao": "2026-05-05T10:01:00Z",
  "cpf": "12345678901",
  "dataNascimento": "1990-01-15T00:00:00"
}
```

### POST /api/clientes/pj
```json
// Request
{
  "nome": "Empresa ABC",
  "email": "contato@abc.com",
  "telefone": "1133334444",
  "agenciaId": 1,
  "cnpj": "12345678000195",
  "razaoSocial": "Empresa ABC LTDA"
}
// Response 201
{
  "id": 2,
  "tipo": "PJ",
  "nome": "Empresa ABC",
  "email": "contato@abc.com",
  "telefone": "1133334444",
  "agenciaId": 1,
  "dataCriacao": "2026-05-05T10:02:00Z",
  "cnpj": "12345678000195",
  "razaoSocial": "Empresa ABC LTDA"
}
```

### GET /api/clientes/{id}
```json
// Response 200 — mesmo formato do POST acima
```

### POST /api/contratacoes
```json
// Request
{
  "clienteId": 1,
  "produtoId": 1
}
// Response 202
{
  "id": 1,
  "clienteId": 1,
  "nomeCliente": "João da Silva",
  "produtoId": 1,
  "nomeProduto": "Maquininha Débito",
  "status": "Pendente",
  "dataSolicitacao": "2026-05-05T10:03:00Z",
  "dataProcessamento": null,
  "observacao": null
}
```

### GET /api/contratacoes/{id}
```json
// Response 200 — após processamento pelo consumer
{
  "id": 1,
  "clienteId": 1,
  "nomeCliente": "João da Silva",
  "produtoId": 1,
  "nomeProduto": "Maquininha Débito",
  "status": "Aprovada",
  "dataSolicitacao": "2026-05-05T10:03:00Z",
  "dataProcessamento": "2026-05-05T10:03:02Z",
  "observacao": "Aprovado. Taxa MDR: 1.2% para Debito."
}
```

---

## 7. Como executar os testes

```bash
dotnet test CP2_CS.Tests/CP2_CS.Tests.csproj --verbosity normal
```

> Inserir print do resultado aqui

---

## 8. Print do painel RabbitMQ

> Inserir print do painel RabbitMQ mostrando a fila `contratacoes` com mensagens processadas

---

## 9. Print da API no Swagger

> Inserir print do Swagger com pelo menos uma contratação aprovada

---

## Como executar

### Pré-requisitos
- .NET 8 SDK
- Oracle Client (conexão com oracle.fiap.com.br)
- RabbitMQ rodando em localhost:5672

### Configurar credenciais Oracle
Edite `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=...;User Id=RM000000;Password=ddmmyyyy;"
}
```

### Aplicar migrations
```bash
dotnet ef migrations add Initial --project CP2_CS.Api
dotnet ef database update --project CP2_CS.Api
```

### Executar a API
```bash
dotnet run --project CP2_CS.Api
```

Acesse: `https://localhost:5001/swagger`

### Inserir produtos de exemplo (SQL)
```sql
INSERT INTO TB_PRODUTO (NOME, DESCRICAO, TIPO, TIPOPAGAMENTO, TAXAMDR)
VALUES ('Maquininha Débito', 'Máquina para pagamentos no débito', 'MAQUINA_CARTAO', 1, 1.2);

INSERT INTO TB_PRODUTO (NOME, DESCRICAO, TIPO, TIPOPAGAMENTO, TAXAMDR)
VALUES ('Maquininha Crédito', 'Máquina para pagamentos no crédito', 'MAQUINA_CARTAO', 2, 2.5);

INSERT INTO TB_PRODUTO (NOME, DESCRICAO, TIPO, VALORSOLICITADO, PRAZOMESES, TAXAJUROSMENSSAL)
VALUES ('Empréstimo Pessoal 12x', 'Crédito pessoal em 12 parcelas', 'EMPRESTIMO', 10000, 12, 1.5);

INSERT INTO TB_PRODUTO (NOME, DESCRICAO, TIPO, VALORSOLICITADO, PRAZOMESES, TAXAJUROSMENSSAL)
VALUES ('Empréstimo Capital de Giro', 'Crédito para capital de giro em 24 parcelas', 'EMPRESTIMO', 50000, 24, 1.5);

COMMIT;
```
