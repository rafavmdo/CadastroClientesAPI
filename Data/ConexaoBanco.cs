
using Npgsql;

namespace cadastro_clientesAPI.Data;

public class ConexaoBanco : IAsyncDisposable {


private readonly NpgsqlDataSource _fonteDados;

public ConexaoBanco(IConfiguration configuration)
{

    string stringConexao =

    configuration.GetConnectionString("Postgres")

    ?? throw new InvalidOperationException(

    "A conexão Postgres não foi configurada."
    );

    _fonteDados = NpgsqlDataSource.Create(stringConexao);
}

public async Task<NpgsqlConnection> CriarConexaoAsync()
{

    return await _fonteDados.OpenConnectionAsync();
}

public async Task<bool> TestarAsync()
{

    await using NpgsqlConnection conexao =
    await CriarConexaoAsync();

    await using NpgsqlCommand comando =
    new NpgsqlCommand("SELECT 1;", conexao);

    object? resultado =
    await comando.ExecuteScalarAsync();

    return Convert.ToInt32(resultado) == 1;
}


public async ValueTask DisposeAsync()
{
    await _fonteDados.DisposeAsync();
}
}