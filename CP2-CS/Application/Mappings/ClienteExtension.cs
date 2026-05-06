using CP2_CS.Application.DTOs;
using CP2_CS.Domain.Entities;

namespace CP2_CS.Application.Mappings;

public static class ClienteExtension
{
    public static PessoaFisica MapearParaEntidade(this CriarPessoaFisicaDTO dto)
    {
        return new PessoaFisica(dto.Nome, dto.Email, dto.Telefone, dto.AgenciaId, dto.Cpf, dto.DataNascimento);
    }

    public static PessoaJuridica MapearParaEntidade(this CriarPessoaJuridicaDTO dto)
    {
        return new PessoaJuridica(dto.Nome, dto.Email, dto.Telefone, dto.AgenciaId, dto.Cnpj, dto.RazaoSocial);
    }

    public static ClienteDTO MapearParaDto(this Cliente entidade)
    {
        var dto = new ClienteDTO
        {
            Id = entidade.Id,
            Nome = entidade.Nome,
            Email = entidade.Email,
            Telefone = entidade.Telefone,
            AgenciaId = entidade.AgenciaId,
            DataCriacao = entidade.DataCriacao
        };

        if (entidade is PessoaFisica pf)
        {
            dto.Tipo = "PF";
            dto.Cpf = pf.Cpf;
            dto.DataNascimento = pf.DataNascimento;
        }
        else if (entidade is PessoaJuridica pj)
        {
            dto.Tipo = "PJ";
            dto.Cnpj = pj.Cnpj;
            dto.RazaoSocial = pj.RazaoSocial;
        }

        return dto;
    }
}
