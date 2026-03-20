using InfnetFlix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfnetFlix.Pages.Login;

public class CadastroUsuario : PageModel
{
    private readonly Contexto _contexto;

    public CadastroUsuario(Contexto contexto)
    {
        _contexto = contexto;
    }

    [BindProperty]
    public Usuario Usuario { get; set; }

    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Usuario.isPremium = false;
        
        _contexto.Usuarios.Add(Usuario);
        _contexto.SaveChanges();

        return RedirectToPage("../Index");
    }
}
