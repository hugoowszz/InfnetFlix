using InfnetFlix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace InfnetFlix.Pages;

[Authorize]
public class Catalogo : PageModel
{
    private readonly Contexto _contexto;

    public Catalogo(Contexto contexto)
    {
        _contexto = contexto;
    }

    public List<Titulo> Titulos = new List<Titulo>();
    public List<Progresso> Progressos { get; set; } = new List<Progresso>();
    public int IdadePerfil { get; set; }
    
    public IActionResult OnGet()
    {
        if (!User.HasClaim(c => c.Type == "PerfilSelecionado"))
        {
            return RedirectToPage("/Perfis/EscolherPerfil");
        }
        
        var idadeClaim = User.FindFirst("IdadePerfil")?.Value;
        if (idadeClaim != null && int.TryParse(idadeClaim, out int idade))
        {
            IdadePerfil = idade;
        }

        CarregarTitulos();
        CarregarProgressos();

        return Page();
    }

    private void CarregarTitulos()
    {
        Titulos = _contexto.Titulos.ToList();
    }

    private void CarregarProgressos()
    {
        var idPerfilClaim = User.FindFirst("IdPerfil")?.Value;
        if (idPerfilClaim != null && int.TryParse(idPerfilClaim, out int idPerfil))
        {
            Progressos = _contexto.Progressos.Where(p => p.idPerfil == idPerfil).ToList();
        }
    }
    
    public IActionResult OnPostSalvarProgresso(int idTitulo, double minutoParada)
    {
        var idPerfilClaim = User.FindFirst("IdPerfil")?.Value;
        if (idPerfilClaim != null && int.TryParse(idPerfilClaim, out int idPerfil))
        {
            var progressoExistente = _contexto.Progressos.FirstOrDefault(p => p.idPerfil == idPerfil && p.idTitulo == idTitulo);

            if (progressoExistente != null)
            {
                progressoExistente.minutoParada = minutoParada;
                _contexto.Progressos.Update(progressoExistente);
            }
            else
            {
                var novoProgresso = new Progresso
                {
                    idPerfil = idPerfil,
                    idTitulo = idTitulo,
                    minutoParada = minutoParada
                };
                _contexto.Progressos.Add(novoProgresso);
            }

            _contexto.SaveChanges();
        }
        return Page();
    }
}