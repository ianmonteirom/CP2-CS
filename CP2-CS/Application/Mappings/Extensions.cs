using CP2_CS.Application.DTOs;
using CP2_CS.Domain.Entities;

namespace CP2_CS.Application.Mappings;

public static class AgenciaExtension
{
    public static Agencia MapearParaEntidade(this CriarAgenciaDTO dto)
    {
        return new Agencia(dto.Nome, dto.Numero, dto.Cidade, dto.Estado);
    }

    public static AgenciaDTO MapearParaDto(this Agencia entidade)
    {
        return new AgenciaDTO
        {
            Id = entidade.Id,
            Nome = entidade.Nome,
            Numero = entidade.Numero,
            Cidade = entidade.Cidade,
            Estado = entidade.Estado,
            DataCriacao = entidade.DataCriacao
        };
    }
}

public static class ContratacaoExtension
{
    public static Contratacao MapearParaEntidade(this CriarContratacaoDTO dto)
    {
        return new Contratacao(dto.ClienteId, dto.ProdutoId);
    }

    public static ContratacaoDTO MapearParaDto(this Contratacao entidade)
    {
        return new ContratacaoDTO
        {
            Id = entidade.Id,
            ClienteId = entidade.ClienteId,
            NomeCliente = entidade.Cliente?.Nome ?? string.Empty,
            ProdutoId = entidade.ProdutoId,
            NomeProduto = entidade.Produto?.Nome ?? string.Empty,
            Status = entidade.Status.ToString(),
            DataSolicitacao = entidade.DataSolicitacao,
            DataProcessamento = entidade.DataProcessamento,
            Observacao = entidade.Observacao
        };
    }
}
