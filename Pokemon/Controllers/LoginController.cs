using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Pokemon.Models;
using System.Text;

namespace Pokemon.Controllers
{
    public class LoginController : Controller
    {
        private readonly PokemonDbContext _context;

        public LoginController(PokemonDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return LocalRedirect("/Identity/Account/Login");
        }

        [HttpPost]
        public IActionResult Index(Pokemon.Models.LoginModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = _context.Users.Where(u => u.Username == model.Username).FirstOrDefault();
            //    if (user != null)
            //    {
            //        byte[] salt = Encoding.ASCII.GetBytes(user.Salt);
            //        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            //            password: model.Password,
            //            salt: salt,
            //            prf: KeyDerivationPrf.HMACSHA256,
            //            iterationCount: 100000,
            //            numBytesRequested: 256 / 8
            //        ));
            //        if (hash == user.HashedPassword)
            //        {
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }
            //}
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pokemon.Models.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UsersModel user = new UsersModel();
            }
            return View(model);
        }

        public IActionResult ForgotPassword()
        {

            return View();
        }
    }
}
