using InfnetFlix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace InfnetFlix.Pages;

public class IndexModel : PageModel
{
    private readonly Contexto _contexto;

    public IndexModel(Contexto contexto)
    {
        _contexto = contexto;
    }

    [BindProperty]
    public Usuario Usuario { get; set; }

    [BindProperty]
    public string CodigoRecuperacao { get; set; }

    [BindProperty]
    public string EmailParaRedefinir { get; set; }

    [BindProperty]
    public string NovaSenha { get; set; }

    public bool ExibirModalRedefinir { get; set; }

    public string MensagemErro { get; set; }
    
    public string MensagemSucesso { get; set; }
    
    public void OnGet() {}

    public async Task<IActionResult> OnPostAsync()
    {
        string emailUsuario = Usuario.emailUsuario;
        string senhaUsuario = Usuario.senhaUsuario;

        if (string.IsNullOrEmpty(emailUsuario) || string.IsNullOrEmpty(senhaUsuario))
        {
            MensagemErro = "Preencha o e-mail e a senha.";
            return Page();
        }

        var usuarioEncontrado = _contexto.Usuarios.FirstOrDefault(u => u.emailUsuario == emailUsuario && u.senhaUsuario == senhaUsuario);

        if (usuarioEncontrado != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuarioEncontrado.nomeUsuario),
                new Claim(ClaimTypes.Email, usuarioEncontrado.emailUsuario),
                new Claim("IsPremium", usuarioEncontrado.isPremium.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToPage("/Perfis/EscolherPerfil");
        }

        MensagemErro = "E-mail ou senha incorretos.";
        return Page();
    }

    public async Task<IActionResult> OnGetLogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostRecuperarSenhaAsync()
    {
        if (CodigoRecuperacao == "12345" && !string.IsNullOrEmpty(Usuario?.emailUsuario))
        {
            var emailEncontrado = _contexto.Usuarios.Any(u => u.emailUsuario == Usuario.emailUsuario);

            if (emailEncontrado)
            {
                ExibirModalRedefinir = true;
                EmailParaRedefinir = Usuario.emailUsuario;
                return Page();
            }
        }

        MensagemErro = "Código ou e-mail incorretos.";
        return Page();
    }

    public async Task<IActionResult> OnPostRedefinirSenhaAsync()
    {
        if (!string.IsNullOrEmpty(EmailParaRedefinir) && !string.IsNullOrEmpty(NovaSenha))
        {
            var usuarioParaRedefinir = _contexto.Usuarios.FirstOrDefault(u => u.emailUsuario == EmailParaRedefinir);
            
            if (usuarioParaRedefinir != null)
            {
                usuarioParaRedefinir.senhaUsuario = NovaSenha;
                _contexto.SaveChanges();

                MensagemSucesso = "Senha foi redefinida!";
                return Page();
            }
        }
        
        MensagemErro = "Erro ao redefinir a senha.";
        return Page();
    }
}
