using CP2_CS.Domain.Constants;
using CP2_CS.Domain.Enums;
using CP2_CS.Domain.Exceptions;

namespace CP2_CS.Domain.Entities;

public class Emprestimo : Produto
{
    public decimal ValorSolicitado { get; private set; }
    public int PrazoMeses { get; private set; }
    public decimal TaxaJurosMensal { get; private set; }

    protected Emprestimo() { }

    public Emprestimo(string nome, string descricao, decimal valorSolicitado, int prazoMeses)
        : base(nome, descricao)
    {
        DomainExceptionValidation.When(valorSolicitado <= 0, Errors.EMPRESTIMO_VALOR_INVALIDO);
        DomainExceptionValidation.When(prazoMeses <= 0, Errors.EMPRESTIMO_PRAZO_INVALIDO);
        ValorSolicitado = valorSolicitado;
        PrazoMeses = prazoMeses;
        TaxaJurosMensal = Valores.TAXA_JUROS_EMPRESTIMO;
    }

    public override (bool aprovada, string observacao) ValidarContratacao(Cliente cliente, IEnumerable<Contratacao> contratacoesDoCliente)
    {
        var score = CalcularScore(cliente, contratacoesDoCliente);

        if (score < Valores.SCORE_MINIMO_EMPRESTIMO)
            return (false, $"{Errors.EMPRESTIMO_SCORE_INSUFICIENTE} Score obtido: {score}. Mínimo: {Valores.SCORE_MINIMO_EMPRESTIMO}.");

        var valorParcela = CalcularParcela();
        return (true, $"Aprovado. Score: {score}. Taxa: {TaxaJurosMensal}% a.m. Parcela: R$ {valorParcela:F2} x {PrazoMeses} meses.");
    }

    private int CalcularScore(Cliente cliente, IEnumerable<Contratacao> contratacoes)
    {
        var score = Valores.SCORE_BASE;

        if (cliente is PessoaFisica pf)
        {
            var idade = CalcularIdade(pf.DataNascimento);
            if (idade >= Valores.IDADE_MINIMA_PRIME && idade <= Valores.IDADE_MAXIMA_PRIME)
                score += Valores.SCORE_IDADE_PRIME;
            else if (idade > Valores.IDADE_MAXIMA_PRIME)
                score += Valores.SCORE_IDADE_SENIOR;
        }

        var aprovadas = contratacoes.Count(c => c.Status == EStatusContratacao.Aprovada);
        score += aprovadas * Valores.SCORE_POR_CONTRATACAO_APROVADA;

        return score;
    }

    private static int CalcularIdade(DateTime dataNascimento)
    {
        var hoje = DateTime.Today;
        var idade = hoje.Year - dataNascimento.Year;
        if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
        return idade;
    }

    private decimal CalcularParcela()
    {
        var taxa = TaxaJurosMensal / 100;
        return ValorSolicitado * (taxa * (decimal)Math.Pow((double)(1 + taxa), PrazoMeses))
               / ((decimal)Math.Pow((double)(1 + taxa), PrazoMeses) - 1);
    }
}
