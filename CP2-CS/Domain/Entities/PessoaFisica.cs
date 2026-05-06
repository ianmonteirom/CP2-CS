using CP2_CS.Domain.Constants;
using CP2_CS.Domain.Exceptions;

namespace CP2_CS.Domain.Entities;

public class PessoaFisica : Cliente
{
    public string Cpf { get; private set; } = null!;
    public DateTime DataNascimento { get; private set; }

    protected PessoaFisica() { }

    public PessoaFisica(string nome, string email, string telefone, int agenciaId, string cpf, DateTime dataNascimento)
        : base(nome, email, telefone, agenciaId)
    {
        ValidarPessoaFisica(cpf, dataNascimento);
        Cpf = cpf;
        DataNascimento = dataNascimento;
    }

    private static void ValidarPessoaFisica(string cpf, DateTime dataNascimento)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(cpf), Errors.CPF_OBRIGATORIO);
        DomainExceptionValidation.When(cpf.Length != Valores.CPF_TAMANHO || !cpf.All(char.IsDigit), Errors.CPF_FORMATO_INVALIDO);
        DomainExceptionValidation.When(dataNascimento == default || dataNascimento >= DateTime.UtcNow, Errors.DATA_NASCIMENTO_INVALIDA);
    }
}
