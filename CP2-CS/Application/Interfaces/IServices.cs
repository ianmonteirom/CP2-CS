using CP2_CS.Application.DTOs;

namespace CP2_CS.Application.Interfaces;

public interface IClienteService
{
    Task<ClienteDTO> AdicionarPessoaFisica(CriarPessoaFisicaDTO dto);
    Task<ClienteDTO> AdicionarPessoaJuridica(CriarPessoaJuridicaDTO dto);
    Task<ClienteDTO> ConsultarPorId(int id);
}

public interface IAgenciaService
{
    Task<AgenciaDTO> Adicionar(CriarAgenciaDTO dto);
    Task<AgenciaDTO> ConsultarPorId(int id);
}

public interface IContratacaoService
{
    Task<ContratacaoDTO> Solicitar(CriarContratacaoDTO dto);
    Task<ContratacaoDTO> ConsultarPorId(int id);
}
