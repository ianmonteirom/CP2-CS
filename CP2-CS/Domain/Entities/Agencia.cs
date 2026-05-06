using CP2_CS.Domain.Constants;
using CP2_CS.Domain.Exceptions;

namespace CP2_CS.Domain.Entities;

public class Agencia
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public int Numero { get; private set; }
    public string Cidade { get; private set; } = null!;
    public string Estado { get; private set; } = null!;
    public DateTime DataCriacao { get; private set; }
    public ICollection<Cliente> Clientes { get; private set; } = new List<Cliente>();

    protected Agencia() { }

    public Agencia(string nome, int numero, string cidade, string estado)
    {
        ValidarDominio(nome, numero);
        Nome = nome;
        Numero = numero;
        Cidade = cidade;
        Estado = estado;
        DataCriacao = DateTime.UtcNow;
    }

    private static void ValidarDominio(string nome, int numero)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(nome), Errors.AGENCIA_NOME_OBRIGATORIO);
        DomainExceptionValidation.When(nome.Length > Valores.AGENCIA_NOME_TAMANHO_MAX, Errors.AGENCIA_NOME_TAMANHO_MAX);
        DomainExceptionValidation.When(numero <= 0, Errors.AGENCIA_NUMERO_INVALIDO);
    }
}
