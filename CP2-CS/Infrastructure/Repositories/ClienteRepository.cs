using CP2_CS.Domain.Entities;
using CP2_CS.Domain.Interfaces;
using CP2_CS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CP2_CS.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
	private readonly AppDbContext _context;

	public ClienteRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Cliente?> ConsultarPorId(int id)
	{
		return await _context.Clientes
			.Include(c => c.Agencia)
			.FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task<bool> CpfJaCadastrado(string cpf)
	{
		return await _context.PessoasFisicas.AnyAsync(p => p.Cpf == cpf);
	}

	public async Task<bool> CnpjJaCadastrado(string cnpj)
	{
		return await _context.PessoasJuridicas.AnyAsync(p => p.Cnpj == cnpj);
	}

	public async Task<PessoaFisica> AdicionarPessoaFisica(PessoaFisica pf)
	{
		await _context.PessoasFisicas.AddAsync(pf);
		await _context.SaveChangesAsync();
		return pf;
	}

	public async Task<PessoaJuridica> AdicionarPessoaJuridica(PessoaJuridica pj)
	{
		await _context.PessoasJuridicas.AddAsync(pj);
		await _context.SaveChangesAsync();
		return pj;
	}
}