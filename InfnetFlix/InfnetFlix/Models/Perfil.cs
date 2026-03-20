using System.ComponentModel.DataAnnotations;

namespace InfnetFlix.Models;

public class Perfil
{
    [Key]
    public int idPerfil { get; set; }
    public string nomePerfil { get; set; }
    public int idadePerfil { get; set; }
    public string? pin { get; set; }
    public bool isInfantil { get; set; }
    public string emailLogado { get; set; }
    
}