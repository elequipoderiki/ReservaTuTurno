using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Turnos.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Turnos.Controllers
{
  public class LoginController : Controller
  {
    private readonly TurnosContext _context;

    public LoginController(TurnosContext context)
    {
      _context = context;
    }

    public IActionResult Index()
    {
      return View();
    }

     // GET: Medico/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create( [Bind("LoginId, Usuario, Password")] Login user)
    {
        if (ModelState.IsValid)
        {
          //chequear que el nombre de usuario no exista en la base,
          //hacer validaciones al password antes de guardar
          var userFinder = await _context.Login.FirstOrDefaultAsync(m => m.Usuario == user.Usuario);
          if  (userFinder != null)
          {
            ViewData["errorLogin"] = "El nombre de usuario ya existe.";
            return View("Create");
          }else{
            string passwordEncriptado = Encriptar(user.Password);
            user.Password = passwordEncriptado;
            _context.Add(user);
            await _context.SaveChangesAsync();
            HttpContext.Session.SetString("usuario", user.Usuario);
            return RedirectToAction("Index", "Home");
          }
        }
        return View(user);
    }


    public IActionResult Login(Login login)
    {
      if(ModelState.IsValid)
      {
        //encriptarPassword
        string passwordEncriptado = Encriptar(login.Password);
        
        var loginUsuario = _context.Login.Where(lg => lg.Usuario == login.Usuario && lg.Password == passwordEncriptado)
        .FirstOrDefault();

        if(loginUsuario != null)
        {
          HttpContext.Session.SetString("usuario", loginUsuario.Usuario);
          return RedirectToAction("Index", "Home");
        } else {
          ViewData["errorLogin"] = "Los datos ingresados son incorrectos.";
          return View("Index");
        }
      }
      return View("Index");
    }

    public string Encriptar(string password)
    {
      using (SHA256 sha256Hash = SHA256.Create())
      {
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

        StringBuilder stringBuilder = new StringBuilder();
        for (int i =0; i < bytes.Length; i++)
        {
          stringBuilder.Append(bytes[i].ToString("x2"));
        }
        return stringBuilder.ToString();
      }
    }

    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return View("Index");
    }
  }
}