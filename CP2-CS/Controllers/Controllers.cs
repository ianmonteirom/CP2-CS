using Microsoft.AspNetCore.Mvc;
using CP2_CS.Application.DTOs;
using CP2_CS.Application.Interfaces;

namespace CP2_CS.Controllers;

[Route("api/clientes")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;

    public ClientesController(IClienteService service)
    {
        _service = service;
    }

    [HttpPost("pf")]
    public async Task<IActionResult> AdicionarPessoaFisica([FromBody] CriarPessoaFisicaDTO dto)
    {
        var criado = await _service.AdicionarPessoaFisica(dto);
        return CreatedAtAction(nameof(ConsultarPorId), new { id = criado.Id }, criado);
    }

    [HttpPost("pj")]
    public async Task<IActionResult> AdicionarPessoaJuridica([FromBody] CriarPessoaJuridicaDTO dto)
    {
        var criado = await _service.AdicionarPessoaJuridica(dto);
        return CreatedAtAction(nameof(ConsultarPorId), new { id = criado.Id }, criado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ConsultarPorId(int id)
    {
        var cliente = await _service.ConsultarPorId(id);
        return Ok(cliente);
    }
}

[Route("api/agencias")]
[ApiController]
public class AgenciasController : ControllerBase
{
    private readonly IAgenciaService _service;

    public AgenciasController(IAgenciaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] CriarAgenciaDTO dto)
    {
        var criada = await _service.Adicionar(dto);
        return CreatedAtAction(nameof(ConsultarPorId), new { id = criada.Id }, criada);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ConsultarPorId(int id)
    {
        var agencia = await _service.ConsultarPorId(id);
        return Ok(agencia);
    }
}

[Route("api/contratacoes")]
[ApiController]
public class ContratacoesController : ControllerBase
{
    private readonly IContratacaoService _service;

    public ContratacoesController(IContratacaoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Solicitar([FromBody] CriarContratacaoDTO dto)
    {
        var criada = await _service.Solicitar(dto);
        return AcceptedAtAction(nameof(ConsultarPorId), new { id = criada.Id }, criada);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ConsultarPorId(int id)
    {
        var contratacao = await _service.ConsultarPorId(id);
        return Ok(contratacao);
    }
}
