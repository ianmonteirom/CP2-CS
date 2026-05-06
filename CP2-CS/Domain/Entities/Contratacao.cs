using CP2_CS.Domain.Enums;

namespace CP2_CS.Domain.Entities;

public class Contratacao
{
    public int Id { get; private set; }
    public int ClienteId { get; private set; }
    public Cliente Cliente { get; private set; } = null!;
    public int ProdutoId { get; private set; }
    public Produto Produto { get; private set; } = null!;
    public EStatusContratacao Status { get; private set; }
    public DateTime DataSolicitacao { get; private set; }
    public DateTime? DataProcessamento { get; private set; }
    public string? Observacao { get; private set; }

    protected Contratacao() { }

    public Contratacao(int clienteId, int produtoId)
    {
        ClienteId = clienteId;
        ProdutoId = produtoId;
        Status = EStatusContratacao.Pendente;
        DataSolicitacao = DateTime.UtcNow;
    }

    public void IniciarProcessamento()
    {
        Status = EStatusContratacao.Processando;
    }

    public void Aprovar(string observacao)
    {
        Status = EStatusContratacao.Aprovada;
        DataProcessamento = DateTime.UtcNow;
        Observacao = observacao;
    }

    public void Rejeitar(string observacao)
    {
        Status = EStatusContratacao.Rejeitada;
        DataProcessamento = DateTime.UtcNow;
        Observacao = observacao;
    }
}
