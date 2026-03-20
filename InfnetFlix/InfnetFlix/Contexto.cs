using InfnetFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace InfnetFlix;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<Titulo> Titulos { get; set; }
    public DbSet<Progresso> Progressos { get; set; }
}