using Microsoft.EntityFrameworkCore;

namespace InfnetFlix.Models;

[PrimaryKey(nameof(idPerfil), nameof(idTitulo))]
public class Progresso
{
    public int idPerfil { get; set; }
    public int idTitulo { get; set; }
    public double minutoParada { get; set; }
}