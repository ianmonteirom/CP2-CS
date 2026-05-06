namespace CP2_CS.Domain.Entities;

public abstract class Produto
{
    public int Id { get; protected set; }
    public string Nome { get; protected set; } = null!;
    public string Descricao { get; protected set; } = null!;
    public ICollection<Contratacao> Contratacoes { get; protected set; } = new List<Contratacao>();

    protected Produto() { }

    protected Produto(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
    }

    public abstract (bool aprovada, string observacao) ValidarContratacao(Cliente cliente, IEnumerable<Contratacao> contratacoesDoCliente);
}
