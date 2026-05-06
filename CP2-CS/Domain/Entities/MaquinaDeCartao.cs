using CP2_CS.Domain.Constants;
using CP2_CS.Domain.Enums;
using CP2_CS.Domain.Exceptions;

namespace CP2_CS.Domain.Entities;

public class MaquinaDeCartao : Produto
{
    public ETipoPagamento TipoPagamento { get; private set; }
    public decimal TaxaMDR { get; private set; }

    protected MaquinaDeCartao() { }

    public MaquinaDeCartao(string nome, string descricao, ETipoPagamento tipoPagamento)
        : base(nome, descricao)
    {
        TipoPagamento = tipoPagamento;
        TaxaMDR = CalcularTaxaMDR(tipoPagamento);
    }

    public override (bool aprovada, string observacao) ValidarContratacao(Cliente cliente, IEnumerable<Contratacao> contratacoesDoCliente)
    {
        var possuiMaquinaDoMesmoTipo = contratacoesDoCliente
            .Any(c => c.Status == EStatusContratacao.Aprovada
                   && c.Produto is MaquinaDeCartao m
                   && m.TipoPagamento == TipoPagamento);

        if (possuiMaquinaDoMesmoTipo)
            return (false, Errors.CLIENTE_JA_POSSUI_MAQUINA_CARTAO);

        return (true, $"Aprovado. Taxa MDR: {TaxaMDR}% para {TipoPagamento}.");
    }

    private static decimal CalcularTaxaMDR(ETipoPagamento tipo) => tipo switch
    {
        ETipoPagamento.Debito => 1.2m,
        ETipoPagamento.Credito => 2.5m,
        ETipoPagamento.CrediarioParcelado => 3.8m,
        _ => throw new DomainExceptionValidation(Errors.MDR_TAXA_INVALIDA)
    };
}
