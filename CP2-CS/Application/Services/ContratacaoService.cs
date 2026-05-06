using CP2_CS.Application.DTOs;
using CP2_CS.Application.Interfaces;
using CP2_CS.Application.Mappings;
using CP2_CS.Domain.Constants;
using CP2_CS.Domain.Exceptions;
using CP2_CS.Domain.Interfaces;

namespace CP2_CS.Application.Services;

public class ContratacaoService : IContratacaoService
{
    private readonly IContratacaoRepository _contratacaoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProdutoRepository _produtoRepository;

    public ContratacaoService(
        IContratacaoRepository contratacaoRepository,
        IClienteRepository clienteRepository,
        IProdutoRepository produtoRepository)
    {
        _contratacaoRepository = contratacaoRepository;
        _clienteRepository = clienteRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<ContratacaoDTO> Solicitar(CriarContratacaoDTO dto)
    {
        DomainExceptionValidation.When(dto.ClienteId <= 0, Errors.CONTRATACAO_CLIENTE_ID_INVALIDO);
        DomainExceptionValidation.When(dto.ProdutoId <= 0, Errors.CONTRATACAO_PRODUTO_ID_INVALIDO);

        var cliente = await _clienteRepository.ConsultarPorId(dto.ClienteId);
        if (cliente is null)
            throw new NotFoundException(Errors.CLIENTE_NAO_ENCONTRADO);

        var produto = await _produtoRepository.ConsultarPorId(dto.ProdutoId);
        if (produto is null)
            throw new NotFoundException(Errors.PRODUTO_NAO_ENCONTRADO);

        var entidade = dto.MapearParaEntidade();
        var criada = await _contratacaoRepository.Adicionar(entidade);

        var contratacoesDoCliente = await _contratacaoRepository.ConsultarPorClienteId(dto.ClienteId);

        var (aprovada, observacao) = produto.ValidarContratacao(cliente, contratacoesDoCliente);

        if (aprovada)
            criada.Aprovar(observacao);
        else
            criada.Rejeitar(observacao);

        await _contratacaoRepository.Atualizar(criada);

        return criada.MapearParaDto();
    }

    public async Task<ContratacaoDTO> ConsultarPorId(int id)
    {
        var entidade = await _contratacaoRepository.ConsultarPorIdComDetalhes(id);

        if (entidade is null)
            throw new NotFoundException(Errors.CONTRATACAO_NAO_ENCONTRADA);

        return entidade.MapearParaDto();
    }
}