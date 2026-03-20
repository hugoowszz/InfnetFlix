using System.ComponentModel.DataAnnotations;

namespace InfnetFlix.Models;

public class Usuario
{
    [Key]
    public int idUsuario { get; set; }
    public string nomeUsuario { get; set; }
    public int idadeUsuario { get; set; }
    public string emailUsuario { get; set; }
    public string senhaUsuario { get; set; }
    public bool isPremium { get; set; }
}
