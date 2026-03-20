using System.ComponentModel.DataAnnotations;

namespace InfnetFlix.Models;

public class Titulo
{
    [Key]
    public int idTitulo { get; set; }
    public string nomeTitulo { get; set; }
    public int idadeMinima { get; set; }
    public string imagemCapa { get; set; }
    public int duracao { get; set; }
    public string descricao { get; set; }
    public string categoria { get; set; }
    public string qualidade { get; set; }
    
    public string conteudo { get; set; }
}