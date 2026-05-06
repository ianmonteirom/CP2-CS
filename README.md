# CP2-CS — Banco Digital API

## 1. Identificação

| Nome | RM |
|---|---|
| Ian Monteiro Moreira | RM558652 |
| Bruno Silva Bastos | RM550416 |
| João Boht | RM558690 |

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

Regra adicional: um cliente não pode ter duas máquinas aprovadas para o mesmo tipo de pagamento.

### Emprestimo
Produto de crédito pessoal com aprovação baseada em **score de crédito** calculado dinamicamente no domínio:

| Critério | Pontos |
|---|---|
| Score base | 300 |
| Idade entre 25–60 anos (PF) | +100 |
| Idade acima de 60 anos (PF) | +50 |
| Por contratação aprovada anterior | +50 cada |

Score mínimo para aprovação: **500 pontos**. Taxa de juros: **1,5% a.m.** Parcela calculada via fórmula Price.

---

## 4. Diagrama de classes

> Veja `/docs/diagrama-classes.png`

```
┌─────────────┐         ┌──────────────────────────┐
│   Agencia   │1      N │   Cliente  <<abstract>>  │
│─────────────│◄────────│──────────────────────────│
│ Id          │         │ Id                       │
│ Nome        │         │ Nome                     │
│ Numero      │         │ Email                    │
│ Cidade      │         │ Telefone                 │
│ Estado      │         │ AgenciaId                │
└─────────────┘         └────────────┬─────────────┘
                                     │
                      ┌──────────────┴──────────────┐
                      ▼                             ▼
          ┌───────────────────┐        ┌───────────────────────┐
          │   PessoaFisica    │        │    PessoaJuridica     │
          │───────────────────│        │───────────────────────│
          │ Cpf               │        │ Cnpj                  │
          │ DataNascimento    │        │ RazaoSocial           │
          └───────────────────┘        └───────────────────────┘

┌──────────────────┐        ┌─────────────────────────────┐
│   Contratacao    │N     1 │   Produto   <<abstract>>    │
│──────────────────│────────│─────────────────────────────│
│ Id               │        │ Id                          │
│ ClienteId        │        │ Nome                        │
│ ProdutoId        │        │ Descricao                   │
│ Status           │        └──────────────┬──────────────┘
│ DataSolicitacao  │                       │
│ DataProcessamento│        ┌──────────────┴──────────────┐
│ Observacao       │        ▼                             ▼
└──────────────────┘  ┌─────────────────┐   ┌─────────────────────┐
                      │ MaquinaDeCartao │   │     Emprestimo      │
                      │─────────────────│   │─────────────────────│
                      │ TipoPagamento   │   │ ValorSolicitado     │
                      │ TaxaMDR         │   │ PrazoMeses          │
                      └─────────────────┘   │ TaxaJurosMensal     │
                                            └─────────────────────┘
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
  "nome": "João Silva",
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
  "nome": "João Silva",
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
  "nomeCliente": "João Silva",
  "produtoId": 1,
  "nomeProduto": "Maquininha Débito",
  "status": "Aprovada",
  "dataSolicitacao": "2026-05-05T10:03:00Z",
  "dataProcessamento": "2026-05-05T10:03:00Z",
  "observacao": "Aprovado. Taxa MDR: 1.2% para Debito."
}
```

### GET /api/contratacoes/{id}
```json
// Response 200
{
  "id": 1,
  "clienteId": 1,
  "nomeCliente": "João Silva",
  "produtoId": 1,
  "nomeProduto": "Maquininha Débito",
  "status": "Aprovada",
  "dataSolicitacao": "2026-05-05T10:03:00Z",
  "dataProcessamento": "2026-05-05T10:03:00Z",
  "observacao": "Aprovado. Taxa MDR: 1.2% para Debito."
}
```

---

## 7. Como executar

### Pré-requisitos
- .NET 8 SDK
- Oracle Client (conexão com oracle.fiap.com.br)

### Configurar credenciais Oracle
Edite `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SID=ORCL)));User Id=RM558652;Password=SUA_SENHA;"
}
```

### Aplicar migrations
```bash
Add-Migration Initial
Update-Database
```

### Inserir produtos de exemplo (SQL)
```sql
INSERT INTO "TB_PRODUTO" ("Nome", "Descricao", "Tipo", "TipoPagamento", "TaxaMDR")
VALUES ('Maquininha Débito', 'Pagamentos no débito', 'MAQUINA_CARTAO', 1, 1.2);

INSERT INTO "TB_PRODUTO" ("Nome", "Descricao", "Tipo", "TipoPagamento", "TaxaMDR")
VALUES ('Maquininha Crédito', 'Pagamentos no crédito', 'MAQUINA_CARTAO', 2, 2.5);

INSERT INTO "TB_PRODUTO" ("Nome", "Descricao", "Tipo", "ValorSolicitado", "PrazoMeses", "TaxaJurosMensal")
VALUES ('Empréstimo Pessoal 12x', 'Crédito pessoal em 12 parcelas', 'EMPRESTIMO', 10000, 12, 1.5);

INSERT INTO "TB_PRODUTO" ("Nome", "Descricao", "Tipo", "ValorSolicitado", "PrazoMeses", "TaxaJurosMensal")
VALUES ('Empréstimo Capital de Giro', 'Crédito para capital de giro', 'EMPRESTIMO', 50000, 24, 1.5);

COMMIT;
```

### Executar a API
```bash
dotnet run
```
Acesse: `https://localhost:{porta}/swagger`

---

## 8. Print do painel RabbitMQ
> *(a ser inserido)*

## 9. Print da API no Swagger com contratação aprovada
> *(a ser inserido)*