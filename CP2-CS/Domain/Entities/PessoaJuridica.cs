using CP2_CS.Domain.Constants;
using CP2_CS.Domain.Exceptions;

namespace CP2_CS.Domain.Entities;

public class PessoaJuridica : Cliente
{
    public string Cnpj { get; private set; } = null!;
    public string RazaoSocial { get; private set; } = null!;

    protected PessoaJuridica() { }

    public PessoaJuridica(string nome, string email, string telefone, int agenciaId, string cnpj, string razaoSocial)
        : base(nome, email, telefone, agenciaId)
    {
        ValidarPessoaJuridica(cnpj, razaoSocial);
        Cnpj = cnpj;
        RazaoSocial = razaoSocial;
    }

    private static void ValidarPessoaJuridica(string cnpj, string razaoSocial)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(cnpj), Errors.CNPJ_OBRIGATORIO);
        DomainExceptionValidation.When(cnpj.Length != Valores.CNPJ_TAMANHO || !cnpj.All(char.IsDigit), Errors.CNPJ_FORMATO_INVALIDO);
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(razaoSocial), Errors.RAZAO_SOCIAL_OBRIGATORIA);
        DomainExceptionValidation.When(razaoSocial.Length > Valores.RAZAO_SOCIAL_TAMANHO_MAX, Errors.RAZAO_SOCIAL_TAMANHO_MAX);
    }
}
