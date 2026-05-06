using CP2_CS.Domain.Constants;
using CP2_CS.Domain.Exceptions;

namespace CP2_CS.Domain.Entities;

public abstract class Cliente
{
    public int Id { get; protected set; }
    public string Nome { get; protected set; } = null!;
    public string Email { get; protected set; } = null!;
    public string Telefone { get; protected set; } = null!;
    public int AgenciaId { get; protected set; }
    public Agencia Agencia { get; protected set; } = null!;
    public DateTime DataCriacao { get; protected set; }
    public ICollection<Contratacao> Contratacoes { get; protected set; } = new List<Contratacao>();

    protected Cliente() { }

    protected Cliente(string nome, string email, string telefone, int agenciaId)
    {
        ValidarDominio(nome, email, telefone, agenciaId);
        Nome = nome;
        Email = email;
        Telefone = telefone;
        AgenciaId = agenciaId;
        DataCriacao = DateTime.UtcNow;
    }

    protected void ValidarDominio(string nome, string email, string telefone, int agenciaId)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(nome), Errors.NOME_OBRIGATORIO);
        DomainExceptionValidation.When(nome.Length > Valores.NOME_TAMANHO_MAX, Errors.NOME_TAMANHO_MAX);
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(email), Errors.EMAIL_OBRIGATORIO);
        DomainExceptionValidation.When(email.Length > Valores.EMAIL_TAMANHO_MAX, Errors.EMAIL_TAMANHO_MAX);
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(telefone), Errors.TELEFONE_OBRIGATORIO);
        DomainExceptionValidation.When(telefone.Length > Valores.TELEFONE_TAMANHO_MAX, Errors.TELEFONE_TAMANHO_MAX);
        DomainExceptionValidation.When(agenciaId <= 0, Errors.AGENCIA_ID_INVALIDO);
    }
}
