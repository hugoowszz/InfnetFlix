using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
namespace InfnetFlix.Pages.Perfis;

public class GerenciarAssinatura : PageModel
{
    private readonly Contexto _contexto;

    public GerenciarAssinatura(Contexto contexto)
    {
        _contexto = contexto;
    }

    public bool IsPremium { get; set; }

    public void OnGet()
    {
        CarregarStatusAssinatura();
    }

    public IActionResult OnPost(string acao)
    {
        var emailLogado = User.FindFirstValue(ClaimTypes.Email);

        var usuario = _contexto.Usuarios.FirstOrDefault(u => u.emailUsuario == emailLogado);

        if (usuario != null)
        {
            usuario.isPremium = acao == "assinar";
            
            _contexto.Usuarios.Update(usuario);
            _contexto.SaveChanges();

            IsPremium = acao == "assinar";
        }

        return Page();
    }

    private void CarregarStatusAssinatura()
    {
        var emailLogado = User.FindFirstValue(ClaimTypes.Email);

        var usuario = _contexto.Usuarios.FirstOrDefault(u => u.emailUsuario == emailLogado);
        if (usuario != null)
        {
            IsPremium = usuario.isPremium;
        }
    }
}