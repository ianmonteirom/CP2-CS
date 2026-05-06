using CP2_CS.Domain.Entities;

namespace CP2_CS.Domain.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> ConsultarPorId(int id);
    Task<bool> CpfJaCadastrado(string cpf);
    Task<bool> CnpjJaCadastrado(string cnpj);
    Task<PessoaFisica> AdicionarPessoaFisica(PessoaFisica pf);
    Task<PessoaJuridica> AdicionarPessoaJuridica(PessoaJuridica pj);
}
