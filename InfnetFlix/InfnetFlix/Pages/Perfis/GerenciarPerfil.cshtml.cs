using InfnetFlix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace InfnetFlix.Pages.Perfis;

public class GerenciarPerfil : PageModel
{
    private readonly Contexto _contexto;

    public GerenciarPerfil(Contexto contexto)
    {
        _contexto = contexto;
    }

    [BindProperty]
    public Perfil Perfil { get; set; }
    
    public IActionResult OnGet(int id)
    {
        var emailLogado = User.FindFirstValue(ClaimTypes.Email);
        
        var perfilDoBanco = _contexto.Perfis.FirstOrDefault(p => p.idPerfil == id && p.emailLogado == emailLogado);
        
        if (perfilDoBanco == null)
        {
            return RedirectToPage("/Perfis/EscolherPerfil");
        }

        Perfil = perfilDoBanco;
        return Page();
    }

    public IActionResult OnPost()
    {
        ModelState.Remove("Perfil.emailLogado");
        ModelState.Remove("Perfil.isInfantil");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var emailLogado = User.FindFirstValue(ClaimTypes.Email);

        var perfilExistente = _contexto.Perfis.FirstOrDefault(p => p.idPerfil == Perfil.idPerfil && p.emailLogado == emailLogado);

        if (perfilExistente != null)
        {
            perfilExistente.nomePerfil = Perfil.nomePerfil;
            perfilExistente.idadePerfil = Perfil.idadePerfil;
            perfilExistente.pin = Perfil.pin;
            
            perfilExistente.isInfantil = Perfil.idadePerfil <= 11;

            _contexto.Perfis.Update(perfilExistente);
            _contexto.SaveChanges();
        }

        return RedirectToPage("/Perfis/EscolherPerfil");
    }
}