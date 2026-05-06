using CP2_CS.Domain.Entities;
using CP2_CS.Domain.Interfaces;
using CP2_CS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CP2_CS.Infrastructure.Repositories;

public class AgenciaRepository : IAgenciaRepository
{
	private readonly AppDbContext _context;

	public AgenciaRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Agencia?> ConsultarPorId(int id)
	{
		return await _context.Agencias.FindAsync(id);
	}

	public async Task<bool> NumeroJaCadastrado(int numero)
	{
		return await _context.Agencias.AnyAsync(a => a.Numero == numero);
	}

	public async Task<Agencia> Adicionar(Agencia agencia)
	{
		await _context.Agencias.AddAsync(agencia);
		await _context.SaveChangesAsync();
		return agencia;
	}
}

public class ProdutoRepository : IProdutoRepository
{
	private readonly AppDbContext _context;

	public ProdutoRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Produto?> ConsultarPorId(int id)
	{
		return await _context.Produtos.FindAsync(id);
	}
}

public class ContratacaoRepository : IContratacaoRepository
{
	private readonly AppDbContext _context;

	public ContratacaoRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Contratacao?> ConsultarPorId(int id)
	{
		return await _context.Contratacoes.FindAsync(id);
	}

	public async Task<Contratacao?> ConsultarPorIdComDetalhes(int id)
	{
		return await _context.Contratacoes
			.Include(c => c.Cliente)
			.Include(c => c.Produto)
			.FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task<IEnumerable<Contratacao>> ConsultarPorClienteId(int clienteId)
	{
		return await _context.Contratacoes
			.Include(c => c.Produto)
			.Where(c => c.ClienteId == clienteId)
			.ToListAsync();
	}

	public async Task<Contratacao> Adicionar(Contratacao contratacao)
	{
		await _context.Contratacoes.AddAsync(contratacao);
		await _context.SaveChangesAsync();
		return contratacao;
	}

	public async Task Atualizar(Contratacao contratacao)
	{
		_context.Contratacoes.Update(contratacao);
		await _context.SaveChangesAsync();
	}
}