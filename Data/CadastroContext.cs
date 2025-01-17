using Cadastro.Models;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Data;

public class CadastroContext : DbContext
{

    public DbSet<CadastroModel> CadastroM { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString: "Data Source=cadastro.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}