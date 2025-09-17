using System;
using Npgsql;
using System.Threading.Tasks;
using DotNetEnv;

class Program
{
    static async Task Main(string[] args)
    {
        // Carrega variáveis do .env apenas em ambiente local
        try { Env.Load(); } catch { /* Ignora se não existir .env */ }

        var host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "db";
        var db = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "mydatabase";
        var user = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "user";
        var pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "password";
        var connString = $"Host={host};Port=5432;Database={db};Username={user};Password={pass}";

        try
        {
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            Console.WriteLine("Conexão com PostgreSQL realizada com sucesso!");
            await conn.CloseAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar: {ex.Message}");
        }
    }
}
