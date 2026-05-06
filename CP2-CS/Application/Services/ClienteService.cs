using CP2_CS.Application.DTOs;
using CP2_CS.Application.Interfaces;
using CP2_CS.Application.Mappings;
using CP2_CS.Domain.Constants;
using CP2_CS.Domain.Exceptions;
using CP2_CS.Domain.Interfaces;

namespace CP2_CS.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IAgenciaRepository _agenciaRepository;

    public ClienteService(IClienteRepository clienteRepository, IAgenciaRepository agenciaRepository)
    {
        _clienteRepository = clienteRepository;
        _agenciaRepository = agenciaRepository;
    }

    public async Task<ClienteDTO> AdicionarPessoaFisica(CriarPessoaFisicaDTO dto)
    {
        var agenciaExiste = await _agenciaRepository.ConsultarPorId(dto.AgenciaId);
        if (agenciaExiste is null)
            throw new NotFoundException(Errors.AGENCIA_NAO_ENCONTRADA);

        var cpfDuplicado = await _clienteRepository.CpfJaCadastrado(dto.Cpf);
        DomainExceptionValidation.When(cpfDuplicado, Errors.CPF_JA_CADASTRADO);

        var entidade = dto.MapearParaEntidade();
        var criado = await _clienteRepository.AdicionarPessoaFisica(entidade);

        return criado.MapearParaDto();
    }

    public async Task<ClienteDTO> AdicionarPessoaJuridica(CriarPessoaJuridicaDTO dto)
    {
        var agenciaExiste = await _agenciaRepository.ConsultarPorId(dto.AgenciaId);
        if (agenciaExiste is null)
            throw new NotFoundException(Errors.AGENCIA_NAO_ENCONTRADA);

        var cnpjDuplicado = await _clienteRepository.CnpjJaCadastrado(dto.Cnpj);
        DomainExceptionValidation.When(cnpjDuplicado, Errors.CNPJ_JA_CADASTRADO);

        var entidade = dto.MapearParaEntidade();
        var criado = await _clienteRepository.AdicionarPessoaJuridica(entidade);

        return criado.MapearParaDto();
    }

    public async Task<ClienteDTO> ConsultarPorId(int id)
    {
        var entidade = await _clienteRepository.ConsultarPorId(id);

        if (entidade is null)
            throw new NotFoundException(Errors.CLIENTE_NAO_ENCONTRADO);

        return entidade.MapearParaDto();
    }
}
