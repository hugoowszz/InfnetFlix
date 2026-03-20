using System.Security.Claims;
using InfnetFlix.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfnetFlix.Pages.Perfis;

[Authorize]
public class EscolherPerfil : PageModel
{
    
    private readonly Contexto _contexto;

    public EscolherPerfil(Contexto contexto)
    {
        _contexto = contexto;
    }
    public List<Perfil> Perfis { get; set; } = new List<Perfil>();

    public bool IsPremium { get; set; }
    public int LimitePerfis => IsPremium ? 5 : 3;

    public void OnGet()
    {
        var isPremiumClaim = User.FindFirst("IsPremium")?.Value;
        if (isPremiumClaim != null && bool.TryParse(isPremiumClaim, out bool isPremium))
        {
            IsPremium = isPremium;
        }

        CarregarPerfis();
    }

    private void CarregarPerfis()
    {
        var emailLogado = User.FindFirst(ClaimTypes.Email)?.Value;
        Perfis = _contexto.Perfis.Where(p => p.emailLogado == emailLogado).ToList();
    }

    public async Task<IActionResult> OnPostAsync(int idPerfilSelecionado)
    {
        var userPrincipal = User;
        if (userPrincipal.Identity != null && userPrincipal.Identity.IsAuthenticated)
        {
            CarregarPerfis();
            var perfilSelecionado = Perfis.FirstOrDefault(p => p.idPerfil == idPerfilSelecionado);

            if (perfilSelecionado == null)
            {
                return RedirectToPage();
            }

            var identity = userPrincipal.Identity as ClaimsIdentity;
            
            if (!identity.HasClaim(c => c.Type == "PerfilSelecionado"))
            {
                identity.AddClaim(new Claim("PerfilSelecionado", "true"));
            }
            
            var oldProfileClaim = identity.FindFirst("NomePerfil");
            if (oldProfileClaim != null)
            {
                identity.RemoveClaim(oldProfileClaim);
            }
            identity.AddClaim(new Claim("NomePerfil", perfilSelecionado.nomePerfil));

            var oldProfileIdClaim = identity.FindFirst("IdPerfil");
            if (oldProfileIdClaim != null)
            {
                identity.RemoveClaim(oldProfileIdClaim);
            }
            identity.AddClaim(new Claim("IdPerfil", perfilSelecionado.idPerfil.ToString()));

            var oldProfileIdadeClaim = identity.FindFirst("IdadePerfil");
            if (oldProfileIdadeClaim != null)
            {
                identity.RemoveClaim(oldProfileIdadeClaim);
            }
            identity.AddClaim(new Claim("IdadePerfil", perfilSelecionado.idadePerfil.ToString()));

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return RedirectToPage("/Catalogo");
        }

        return RedirectToPage("/Index");
    }
    
}