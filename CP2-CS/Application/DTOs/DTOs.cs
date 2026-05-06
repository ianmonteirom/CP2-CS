namespace CP2_CS.Application.DTOs;

public class CriarPessoaFisicaDTO
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public int AgenciaId { get; set; }
    public string Cpf { get; set; } = null!;
    public DateTime DataNascimento { get; set; }
}

public class CriarPessoaJuridicaDTO
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public int AgenciaId { get; set; }
    public string Cnpj { get; set; } = null!;
    public string RazaoSocial { get; set; } = null!;
}

public class ClienteDTO
{
    public int Id { get; set; }
    public string Tipo { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public int AgenciaId { get; set; }
    public DateTime DataCriacao { get; set; }
    public string? Cpf { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? Cnpj { get; set; }
    public string? RazaoSocial { get; set; }
}

public class CriarAgenciaDTO
{
    public string Nome { get; set; } = null!;
    public int Numero { get; set; }
    public string Cidade { get; set; } = null!;
    public string Estado { get; set; } = null!;
}

public class AgenciaDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public int Numero { get; set; }
    public string Cidade { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public DateTime DataCriacao { get; set; }
}

public class CriarContratacaoDTO
{
    public int ClienteId { get; set; }
    public int ProdutoId { get; set; }
}

public class ContratacaoDTO
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string NomeCliente { get; set; } = null!;
    public int ProdutoId { get; set; }
    public string NomeProduto { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime DataSolicitacao { get; set; }
    public DateTime? DataProcessamento { get; set; }
    public string? Observacao { get; set; }
}
