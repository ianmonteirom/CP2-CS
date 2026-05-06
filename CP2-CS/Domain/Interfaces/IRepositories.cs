using CP2_CS.Domain.Entities;

namespace CP2_CS.Domain.Interfaces;

public interface IAgenciaRepository
{
    Task<Agencia?> ConsultarPorId(int id);
    Task<bool> NumeroJaCadastrado(int numero);
    Task<Agencia> Adicionar(Agencia agencia);
}

public interface IProdutoRepository
{
    Task<Produto?> ConsultarPorId(int id);
}

public interface IContratacaoRepository
{
    Task<Contratacao?> ConsultarPorId(int id);
    Task<Contratacao?> ConsultarPorIdComDetalhes(int id);
    Task<IEnumerable<Contratacao>> ConsultarPorClienteId(int clienteId);
    Task<Contratacao> Adicionar(Contratacao contratacao);
    Task Atualizar(Contratacao contratacao);
}
