namespace CP2_CS.Domain.Constants;

public static class Errors
{
    public const string NOME_OBRIGATORIO = "Nome é obrigatório.";
    public const string NOME_TAMANHO_MAX = "Nome deve ter no máximo 150 caracteres.";
    public const string EMAIL_OBRIGATORIO = "E-mail é obrigatório.";
    public const string EMAIL_TAMANHO_MAX = "E-mail deve ter no máximo 100 caracteres.";
    public const string TELEFONE_OBRIGATORIO = "Telefone é obrigatório.";
    public const string TELEFONE_TAMANHO_MAX = "Telefone deve ter no máximo 20 caracteres.";
    public const string AGENCIA_ID_INVALIDO = "AgenciaId deve ser maior que zero.";

    public const string CPF_OBRIGATORIO = "CPF é obrigatório.";
    public const string CPF_FORMATO_INVALIDO = "CPF deve ter 11 dígitos numéricos.";
    public const string CPF_JA_CADASTRADO = "CPF já está cadastrado.";

    public const string CNPJ_OBRIGATORIO = "CNPJ é obrigatório.";
    public const string CNPJ_FORMATO_INVALIDO = "CNPJ deve ter 14 dígitos numéricos.";
    public const string CNPJ_JA_CADASTRADO = "CNPJ já está cadastrado.";
    public const string RAZAO_SOCIAL_OBRIGATORIA = "Razão social é obrigatória.";
    public const string RAZAO_SOCIAL_TAMANHO_MAX = "Razão social deve ter no máximo 200 caracteres.";

    public const string AGENCIA_NOME_OBRIGATORIO = "Nome da agência é obrigatório.";
    public const string AGENCIA_NOME_TAMANHO_MAX = "Nome da agência deve ter no máximo 100 caracteres.";
    public const string AGENCIA_NUMERO_INVALIDO = "Número da agência deve ser maior que zero.";
    public const string AGENCIA_NAO_ENCONTRADA = "Agência não encontrada.";
    public const string AGENCIA_NUMERO_JA_CADASTRADO = "Número de agência já está cadastrado.";

    public const string CLIENTE_NAO_ENCONTRADO = "Cliente não encontrado.";
    public const string CLIENTE_JA_POSSUI_MAQUINA_CARTAO = "Cliente já possui uma máquina de cartão aprovada para este tipo de pagamento.";

    public const string PRODUTO_NAO_ENCONTRADO = "Produto não encontrado.";

    public const string CONTRATACAO_NAO_ENCONTRADA = "Contratação não encontrada.";
    public const string CONTRATACAO_CLIENTE_ID_INVALIDO = "ClienteId deve ser maior que zero.";
    public const string CONTRATACAO_PRODUTO_ID_INVALIDO = "ProdutoId deve ser maior que zero.";
    public const string CONTRATACAO_APROVADA = "Contratação aprovada.";

    public const string EMPRESTIMO_VALOR_INVALIDO = "Valor solicitado deve ser maior que zero.";
    public const string EMPRESTIMO_PRAZO_INVALIDO = "Prazo deve ser maior que zero.";
    public const string EMPRESTIMO_SCORE_INSUFICIENTE = "Score de crédito insuficiente para aprovação.";

    public const string MDR_TAXA_INVALIDA = "Taxa MDR deve ser maior que zero.";
    public const string DATA_NASCIMENTO_INVALIDA = "Data de nascimento inválida.";
}
