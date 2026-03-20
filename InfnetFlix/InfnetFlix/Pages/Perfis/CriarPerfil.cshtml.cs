using System.Security.Claims;
using InfnetFlix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfnetFlix.Pages.Usuarios;

public class CriarPerfil : PageModel
{
    
    private readonly Contexto _contexto;

    public CriarPerfil(Contexto contexto)
    {
        _contexto = contexto;
    }
    
    [BindProperty]
    public Perfil Perfil { get; set; }
    
    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        ModelState.Remove("Perfil.emailLogado");
        ModelState.Remove("Perfil.isInfantil");
        ModelState.Remove("Perfil.idPerfil");
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var emailUsuario = User.FindFirst(ClaimTypes.Email)?.Value;
        Perfil.emailLogado = emailUsuario;
        Perfil.isInfantil = Perfil.idadePerfil <= 11;

        _contexto.Perfis.Add(Perfil);
        _contexto.SaveChanges();
        
        return RedirectToPage("EscolherPerfil");
    }
}