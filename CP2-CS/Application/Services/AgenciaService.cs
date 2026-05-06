using CP2_CS.Application.DTOs;
using CP2_CS.Application.Interfaces;
using CP2_CS.Application.Mappings;
using CP2_CS.Domain.Constants;
using CP2_CS.Domain.Exceptions;
using CP2_CS.Domain.Interfaces;

namespace CP2_CS.Application.Services;

public class AgenciaService : IAgenciaService
{
    private readonly IAgenciaRepository _repository;

    public AgenciaService(IAgenciaRepository repository)
    {
        _repository = repository;
    }

    public async Task<AgenciaDTO> Adicionar(CriarAgenciaDTO dto)
    {
        var numeroDuplicado = await _repository.NumeroJaCadastrado(dto.Numero);
        DomainExceptionValidation.When(numeroDuplicado, Errors.AGENCIA_NUMERO_JA_CADASTRADO);

        var entidade = dto.MapearParaEntidade();
        var criada = await _repository.Adicionar(entidade);

        return criada.MapearParaDto();
    }

    public async Task<AgenciaDTO> ConsultarPorId(int id)
    {
        var entidade = await _repository.ConsultarPorId(id);

        if (entidade is null)
            throw new NotFoundException(Errors.AGENCIA_NAO_ENCONTRADA);

        return entidade.MapearParaDto();
    }
}
