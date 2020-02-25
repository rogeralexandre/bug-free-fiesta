using Microsoft.EntityFrameworkCore;

namespace cmdAPI.Models
{
    //NOTAS
    // Depois de criado o contexto, configurado o StartUp e appSettings.Json com a string de conexão
    // Executar no Terminal:
    //          -> dotnet ef migrations add AddCommandToDB
    
    public class CommandContext : DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options) : base (options)
        {

        }

        public DbSet<Command> CommandItems {get; set;}
    }
}